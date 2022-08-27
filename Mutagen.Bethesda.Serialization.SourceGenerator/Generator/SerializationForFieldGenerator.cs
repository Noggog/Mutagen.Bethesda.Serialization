using Microsoft.CodeAnalysis;
using Mutagen.Bethesda.Serialization.SourceGenerator.Generator.Fields;
using Noggog.StructuredStrings;

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Generator;

public class SerializationFieldGenerator
{
    private readonly Dictionary<string, ISerializationForFieldGenerator> _fieldGeneratorDict = new();
    private readonly ISerializationForFieldGenerator[] _variableFieldGenerators;

    public SerializationFieldGenerator(ISerializationForFieldGenerator[] fieldGenerators)
    {
        _variableFieldGenerators = fieldGenerators
            .Where(x => !x.AssociatedTypes.Any())
            .ToArray();
        foreach (var f in fieldGenerators)
        {
            foreach (var associatedType in f.AssociatedTypes)
            {
                _fieldGeneratorDict[associatedType] = f;
            }
        }
    }
    
    public void GenerateForField(ITypeSymbol obj, ITypeSymbol fieldType, string accessor, StructuredStringBuilder sb)
    {
        if (_fieldGeneratorDict.TryGetValue(fieldType.ToString(), out var gen))
        {
            gen.GenerateForSerialize(obj, fieldType, accessor, "writer", "kernel", sb);
        }
        else
        {
            foreach (var fieldGenerator in _variableFieldGenerators)
            {
                if (fieldGenerator.Applicable(fieldType))
                {
                    fieldGenerator.GenerateForSerialize(obj, fieldType, accessor, "writer", "kernel", sb);
                    return;
                }
            }
            sb.AppendLine($"throw new NotImplementedException(\"Unknown type: {fieldType}\");");
        }
    }
}