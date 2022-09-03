﻿using Microsoft.CodeAnalysis;

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Generator;

public record SerializationItems(
    string Namespace,
    string TermName)
{
    public string SerializationHousingClassName => $"{TermName}_Serialization";
    public string SerializationHousingFileName => $"{TermName}_Serializations.g.cs";

    public string SerializationCall(bool serialize, bool withCheck = false)
    {
        return $"{Namespace}.{SerializationHousingClassName}.{(serialize ? "Serialize" : "Deserialize")}{(withCheck ? "WithCheck" : null)}";
    }
}

public class LoquiSerializationNaming
{
    private readonly LoquiNameRetriever _nameRetriever;

    public LoquiSerializationNaming(
        LoquiNameRetriever nameRetriever)
    {
        _nameRetriever = nameRetriever;
    }
    
    public bool TryGetSerializationItems(ITypeSymbol obj, out SerializationItems items)
    {
        var names = _nameRetriever.GetNames(obj);

        items = new(obj.ContainingNamespace.ToString(), names.Direct);
        return true;
    }
}