﻿using Microsoft.CodeAnalysis;
using Noggog;

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Generator;

public record LoquiTypeSet(
    ITypeSymbol Direct,
    ITypeSymbol Getter,
    ITypeSymbol Setter);

public class LoquiMapping
{
    private readonly Dictionary<ITypeSymbol, List<ITypeSymbol>> _inheritingClassMapping;
    private readonly Dictionary<ITypeSymbol, LoquiTypeSet> _typeSetMapping;
    private readonly IsLoquiObjectTester _isLoquiObjectTester;

    public LoquiMapping(
        Dictionary<ITypeSymbol, LoquiTypeSet> typeSetMapping,
        Dictionary<ITypeSymbol, List<ITypeSymbol>> inheritingClassMapping, 
        IsLoquiObjectTester isLoquiObjectTester)
    {
        _inheritingClassMapping = inheritingClassMapping;
        _isLoquiObjectTester = isLoquiObjectTester;
        _typeSetMapping = typeSetMapping;
    }

    public ITypeSymbol? TryGetBaseClass(ITypeSymbol typeSymbol)
    {
        if (typeSymbol.BaseType != null
            && _isLoquiObjectTester.IsLoqui(typeSymbol.BaseType))
        {
            return typeSymbol.BaseType;
        }

        return default;
    }

    public IReadOnlyList<ITypeSymbol> TryGetInheritingClasses(ITypeSymbol typeSymbol)
    {
        if (_inheritingClassMapping.TryGetValue(typeSymbol, out var inheriting)) return inheriting;
        return Array.Empty<ITypeSymbol>();
    }

    public bool HasInheritingClasses(ITypeSymbol typeSymbol) => TryGetInheritingClasses(typeSymbol).Count > 0;

    public bool TryGetTypeSet(ITypeSymbol typeSymbol, out LoquiTypeSet typeSet) => _typeSetMapping.TryGetValue(typeSymbol, out typeSet);
}

public class LoquiMapper
{
    private readonly LoquiNameRetriever _nameRetriever;
    private readonly IsLoquiObjectTester _isLoquiObjectTester;

    public LoquiMapper(
        LoquiNameRetriever nameRetriever,
        IsLoquiObjectTester isLoquiObjectTester)
    {
        _nameRetriever = nameRetriever;
        _isLoquiObjectTester = isLoquiObjectTester;
    }
    
    public LoquiMapping GetMappings(Compilation compilation, CancellationToken cancel)
    {
        var inheritingClassMapping = new Dictionary<ITypeSymbol, List<ITypeSymbol>>(SymbolEqualityComparer.Default);
        var typeSets = new Dictionary<ITypeSymbol, LoquiTypeSet>(SymbolEqualityComparer.Default);
        foreach (var symb in IterateAllMutagenSymbols(compilation, cancel)
                     .WhereCastable<ITypeSymbol, INamedTypeSymbol>())
        {
            cancel.ThrowIfCancellationRequested();
            if (!_isLoquiObjectTester.IsLoqui(symb)) continue;

            ProcessInheritance(symb, inheritingClassMapping);

            var names = _nameRetriever.GetNames(symb);
            var generic = symb.TypeArguments.Length > 0 ? $"`{symb.TypeArguments.Length}" : string.Empty;
            var direct = compilation.GetTypeByMetadataName($"{symb.ContainingNamespace}.{names.Direct}{generic}");
            var getter = compilation.GetTypeByMetadataName($"{symb.ContainingNamespace}.{names.Getter}{generic}");
            var setter = compilation.GetTypeByMetadataName($"{symb.ContainingNamespace}.{names.Setter}{generic}");
            if (direct != null
                && getter != null
                && setter != null)
            {
                typeSets[symb] = new(direct, getter, setter);
            }
        }

        return new LoquiMapping(
            typeSets,
            inheritingClassMapping,
            _isLoquiObjectTester);
    }

    private void ProcessInheritance(
        ITypeSymbol type,
        Dictionary<ITypeSymbol, List<ITypeSymbol>> mapping)
    {
        var baseType = type.BaseType;
        while (baseType != null
               && _isLoquiObjectTester.IsLoqui(baseType))
        {
            mapping.GetOrAdd(baseType).Add(type);
            baseType = baseType.BaseType;
        }
    }
    
    private IEnumerable<INamedTypeSymbol> IterateAllMutagenSymbols(Compilation compilation, CancellationToken cancel)
    {
        cancel.ThrowIfCancellationRequested();
        var stack = new Stack<INamespaceSymbol>();
        stack.Push(compilation.GlobalNamespace);

        while (stack.Count > 0)
        {
            cancel.ThrowIfCancellationRequested();
            var @namespace = stack.Pop();

            foreach (var member in @namespace.GetMembers())
            {
                cancel.ThrowIfCancellationRequested();
                if (member is INamespaceSymbol ns)
                {
                    if (ns.ToString().StartsWith("Mutagen"))
                    {
                        stack.Push(ns);
                    }
                }
                else if (member is INamedTypeSymbol named
                         && _isLoquiObjectTester.IsLoqui(named))
                {
                    yield return named;
                }
            }
        }
    }
}