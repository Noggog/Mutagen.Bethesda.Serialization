﻿using Microsoft.CodeAnalysis;
using Mutagen.Bethesda.Serialization.SourceGenerator.Customizations;
using Noggog.StructuredStrings;
using Noggog.StructuredStrings.CSharp;

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Serialization.Fields;

public class FormLinkFieldGenerator : ISerializationForFieldGenerator
{
    public IEnumerable<string> AssociatedTypes => Array.Empty<string>();

    private static readonly HashSet<string> _expectedStrings = new()
    {
        "FormLink",
        "FormLinkNullable",
        "IFormLink",
        "IFormLinkNullable",
        "IFormLinkGetter",
        "IFormLinkNullableGetter",
    };

    public IEnumerable<string> RequiredNamespaces(
        LoquiTypeSet obj,
        CompilationUnit compilation,
        ITypeSymbol typeSymbol)
    {
        yield return "Mutagen.Bethesda.Plugins";
    }

    public bool ShouldGenerate(IPropertySymbol propertySymbol) => true;

    private bool IsNullable(ITypeSymbol field) => field.Name.Contains("Nullable");

    public bool HasVariableHasSerialize => true;

    public bool Applicable(
        LoquiTypeSet obj, 
        CustomizationSpecifications customization, 
        ITypeSymbol typeSymbol, 
        string? fieldName)
    {
        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol) return false;
        var typeMembers = namedTypeSymbol.TypeArguments;
        if (typeMembers.Length != 1) return false;
        return _expectedStrings.Contains(typeSymbol.Name);
    }

    public void GenerateForSerialize(
        CompilationUnit compilation,
        LoquiTypeSet obj, 
        ITypeSymbol field,
        string? fieldName,
        string fieldAccessor,
        string? defaultValueAccessor,
        string writerAccessor,
        string kernelAccessor, 
        string metaAccessor,
        bool isInsideCollection,
        StructuredStringBuilder sb,
        CancellationToken cancel)
    {
        var nullable = IsNullable(field);
        using (var c = sb.Call($"{kernelAccessor}.WriteFormKey", linePerArgument: false))
        {
            c.Add(writerAccessor);
            c.Add($"{(fieldName == null ? "null" : $"\"{fieldName}\"")}");
            c.Add($"{fieldAccessor}.FormKeyNullable");
            if (defaultValueAccessor != null)
            {
                c.Add($"{defaultValueAccessor}.FormKeyNullable");
            }
            else
            {
                c.Add($"default(FormKey{Utility.NullChar(nullable)})");
            }

            if (isInsideCollection)
            {
                c.Add("checkDefaults: false");
            }
        }
    }

    public string? GetDefault(ITypeSymbol field)
    {
        if (field is not INamedTypeSymbol namedTypeSymbol) return null;
        var arg = namedTypeSymbol.TypeArguments[0];
        var n = field.Name.Contains("Nullable");
        return $"FormLink{(n ? "Nullable" : null)}<{arg}>.Null";
    }

    public void GenerateForHasSerialize(
        CompilationUnit compilation,
        LoquiTypeSet obj,
        ITypeSymbol field,
        string? fieldName,
        string fieldAccessor,
        string? defaultValueAccessor,
        string metaAccessor,
        StructuredStringBuilder sb,
        CancellationToken cancel)
    {
        var named = (INamedTypeSymbol)field;
        var nullable = IsNullable(field);
        var sub = named.TypeArguments[0];
        if (!compilation.Mapping.TryGetTypeSet(sub, out var typeSet))
        {
            throw new NotImplementedException();
        }

        var linkStr = $"IFormLink{(nullable ? "Nullable" : null)}Getter<{typeSet.Getter}>";
        sb.AppendLine($"if (!EqualityComparer<{linkStr}>.Default.Equals({fieldAccessor}, {defaultValueAccessor ?? $"default({linkStr})"})) return true;");
    }

    public void GenerateForDeserializeSingleFieldInto(
        CompilationUnit compilation,
        LoquiTypeSet obj,
        ITypeSymbol field,
        string? fieldName,
        string fieldAccessor,
        string readerAccessor,
        string kernelAccessor,
        string metaAccessor,
        bool insideCollection,
        bool canSet,
        StructuredStringBuilder sb,
        CancellationToken cancel)
    {
        var nullable = IsNullable(field);
        if (insideCollection)
        {
            if (field is not INamedTypeSymbol named
                || !named.IsGenericType
                || named.TypeArguments.Length != 1)
            {
                throw new NotImplementedException();
            }

            if (nullable)
            {
                sb.AppendLine($"{fieldAccessor}{kernelAccessor}.ReadFormKey({readerAccessor}).ToNullableLink<{named.TypeArguments[0]}>();");
            }
            else
            {
                sb.AppendLine($"{fieldAccessor}{kernelAccessor}.ReadFormKey({readerAccessor}).StripNull(\"{fieldName}\").ToLink<{named.TypeArguments[0]}>();");
            }
        }
        else
        {
            if (nullable)
            {
                sb.AppendLine($"{fieldAccessor}.SetTo({kernelAccessor}.ReadFormKey({readerAccessor}).StripNull(\"{fieldName}\"));");
            }
            else
            {
                sb.AppendLine($"{fieldAccessor}.SetTo({kernelAccessor}.ReadFormKey({readerAccessor}));");   
            }
        }
    }

    public void GenerateForDeserializeSection(CompilationUnit compilation, LoquiTypeSet obj, ITypeSymbol field, string? fieldName,
        string fieldAccessor, string readerAccessor, string kernelAccessor, string metaAccessor, bool insideCollection,
        bool canSet, StructuredStringBuilder sb, CancellationToken cancel)
    {
    }
}