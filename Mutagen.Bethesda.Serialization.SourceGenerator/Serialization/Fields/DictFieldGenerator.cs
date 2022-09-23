﻿using Microsoft.CodeAnalysis;
using Noggog.StructuredStrings;
using Noggog.StructuredStrings.CSharp;
using StrongInject;

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Serialization.Fields;

public class DictFieldGenerator : ISerializationForFieldGenerator
{
    private readonly IsGroupTester _groupTester;
    
    private readonly Func<IOwned<SerializationFieldGenerator>> _forFieldGenerator;
    public IEnumerable<string> AssociatedTypes => Array.Empty<string>();

    public DictFieldGenerator(
        Func<IOwned<SerializationFieldGenerator>> forFieldGenerator,
        IsGroupTester groupTester)
    {
        _forFieldGenerator = forFieldGenerator;
        _groupTester = groupTester;
    }

    private static readonly HashSet<string> _listStrings = new()
    {
        "Dictionary",
        "IReadOnlyDictionary",
        "IDictionary",
    };

    public IEnumerable<string> RequiredNamespaces(ITypeSymbol typeSymbol, CancellationToken cancel) 
        => Enumerable.Empty<string>();

    public bool Applicable(ITypeSymbol typeSymbol)
    {
        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol) return false;
        var typeMembers = namedTypeSymbol.TypeArguments;
        if (typeMembers.Length != 2) return false;
        return _listStrings.Contains(typeSymbol.Name);
    }

    public void GenerateForSerialize(
        CompilationUnit compilation,
        ITypeSymbol obj, 
        ITypeSymbol field,
        string? fieldName,
        string fieldAccessor,
        string? defaultValueAccessor,
        string writerAccessor,
        string kernelAccessor, 
        string metaAccessor,
        StructuredStringBuilder sb,
        CancellationToken cancel)
    {
        if (_groupTester.IsGroup(obj)) return;
        if (defaultValueAccessor != null)
        {
            throw new NotImplementedException();
        }
        
        ITypeSymbol keyType;
        ITypeSymbol valType;
        if (field is INamedTypeSymbol namedTypeSymbol)
        {
            keyType = namedTypeSymbol.TypeArguments[0];
            valType = namedTypeSymbol.TypeArguments[1];
        }
        else
        {
            return;
        }

        sb.AppendLine($"{kernelAccessor}.StartDictionarySection({writerAccessor}, \"{fieldName}\");");
        sb.AppendLine($"foreach (var kv in {fieldAccessor})");
        using (sb.CurlyBrace())
        {
            sb.AppendLine($"{kernelAccessor}.StartDictionaryItem({writerAccessor});");
            sb.AppendLine($"{kernelAccessor}.StartDictionaryKey({writerAccessor});");
            _forFieldGenerator().Value.GenerateSerializeForField(
                compilation: compilation, 
                obj: obj,
                fieldType: keyType,
                writerAccessor: writerAccessor,
                fieldName: null,
                fieldAccessor: "kv.Key", 
                defaultValueAccessor: null,
                sb: sb, 
                cancel: cancel);
            sb.AppendLine($"{kernelAccessor}.EndDictionaryKey({writerAccessor});");
            sb.AppendLine($"{kernelAccessor}.StartDictionaryValue({writerAccessor});");
            _forFieldGenerator().Value.GenerateSerializeForField(
                compilation: compilation, 
                obj: obj, 
                fieldType: valType,
                writerAccessor: writerAccessor,
                fieldName: null,
                fieldAccessor: "kv.Value",
                defaultValueAccessor: null,
                sb: sb, 
                cancel: cancel);
            sb.AppendLine($"{kernelAccessor}.EndDictionaryValue({writerAccessor});");
            sb.AppendLine($"{kernelAccessor}.EndDictionaryItem({writerAccessor});");
        }
        sb.AppendLine($"{kernelAccessor}.EndDictionarySection({writerAccessor});");
    }

    public bool HasVariableHasSerialize => true;

    public void GenerateForHasSerialize(
        CompilationUnit compilation,
        ITypeSymbol obj,
        ITypeSymbol field,
        string? fieldName,
        string fieldAccessor,
        string? defaultValueAccessor, 
        string metaAccessor,
        StructuredStringBuilder sb, 
        CancellationToken cancel)
    {
        sb.AppendLine($"if ({fieldAccessor}.Count > 0) return true;");
    }

    public void GenerateForDeserialize(
        CompilationUnit compilation,
        ITypeSymbol obj,
        ITypeSymbol field,
        string? fieldName,
        string fieldAccessor,
        string readerAccessor,
        string kernelAccessor,
        string metaAccessor,
        StructuredStringBuilder sb,
        CancellationToken cancel)
    {
        if (_groupTester.IsGroup(obj)) return;
        
        ITypeSymbol keyType;
        ITypeSymbol valType;
        if (field is INamedTypeSymbol namedTypeSymbol)
        {
            keyType = namedTypeSymbol.TypeArguments[0];
            valType = namedTypeSymbol.TypeArguments[1];
        }
        else
        {
            return;
        }
        
        sb.AppendLine($"{kernelAccessor}.StartDictionarySection({readerAccessor});");
        sb.AppendLine($"while ({kernelAccessor}.TryHasNextDictionaryItem({readerAccessor}))");
        using (sb.CurlyBrace())
        {
            _forFieldGenerator().Value.GenerateDeserializeForField(
                compilation: compilation,
                obj: obj,
                fieldType: keyType,
                readerAccessor: readerAccessor, 
                fieldName: null, 
                fieldAccessor: "var key",
                sb: sb,
                cancel: cancel);
            _forFieldGenerator().Value.GenerateDeserializeForField(
                compilation: compilation,
                obj: obj,
                fieldType: valType,
                readerAccessor: readerAccessor, 
                fieldName: null, 
                fieldAccessor: "var val",
                sb: sb,
                cancel: cancel);
            sb.AppendLine($"{fieldAccessor}[key] = val;");
        }
        sb.AppendLine($"{kernelAccessor}.EndDictionarySection({readerAccessor});");
    }
}