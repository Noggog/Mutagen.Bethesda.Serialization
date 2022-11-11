﻿//HintName: SomeObject_Serializations.g.cs
using Loqui;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Serialization;
using Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

#nullable enable

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

internal static class SomeObject_Serialization
{
    public static void Serialize<TKernel, TWriteObject>(
        TWriteObject writer,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter item,
        MutagenSerializationWriterKernel<TKernel, TWriteObject> kernel,
        SerializationMetaData metaData)
        where TKernel : ISerializationWriterKernel<TWriteObject>, new()
    {
        kernel.WriteString(writer, "SomeGenderedIntMale", item.SomeGenderedInt.Male, default(string));
        kernel.WriteString(writer, "SomeGenderedIntFemale", item.SomeGenderedInt.Female, default(string));
        kernel.WriteString(writer, "SomeGenderedInt2Male", item.SomeGenderedInt2.Male, default(string));
        kernel.WriteString(writer, "SomeGenderedInt2Female", item.SomeGenderedInt2.Female, default(string));
        kernel.WriteString(writer, "SomeGenderedInt3Male", item.SomeGenderedInt3.Male, default(string));
        kernel.WriteString(writer, "SomeGenderedInt3Female", item.SomeGenderedInt3.Female, default(string));
    }

    public static bool HasSerializationItems(
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter item,
        SerializationMetaData metaData)
    {
        if (!EqualityComparer<string>.Default.Equals(item.SomeGenderedInt.Male, default(string))) return true;
        if (!EqualityComparer<string>.Default.Equals(item.SomeGenderedInt.Female, default(string))) return true;
        if (!EqualityComparer<string>.Default.Equals(item.SomeGenderedInt2.Male, default(string))) return true;
        if (!EqualityComparer<string>.Default.Equals(item.SomeGenderedInt2.Female, default(string))) return true;
        if (!EqualityComparer<string>.Default.Equals(item.SomeGenderedInt3.Male, default(string))) return true;
        if (!EqualityComparer<string>.Default.Equals(item.SomeGenderedInt3.Female, default(string))) return true;
        return false;
    }

    public static Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject Deserialize<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        SerializationMetaData metaData)
    {
        var obj = new Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject();
        DeserializeInto<TReadObject>(
            reader: reader,
            kernel: kernel,
            obj: obj,
            metaData: metaData);
        return obj;
    }

    public static void DeserializeInto<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObject obj,
        SerializationMetaData metaData)
    {
        while (kernel.TryGetNextField(reader, out var name))
        {
            DeserializeSingleFieldInto(
                reader: reader,
                kernel: kernel,
                obj: obj,
                metaData: metaData,
                name: name);
        }

    }

    public static void DeserializeSingleFieldInto<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObject obj,
        SerializationMetaData metaData,
        string name)
    {
        switch (name)
        {
            case "SomeGenderedInt":
                obj.SomeGenderedInt.Male = SerializationHelper.StripNull(kernel.ReadString(reader), name: "SomeGenderedIntMale");
                obj.SomeGenderedInt.Female = SerializationHelper.StripNull(kernel.ReadString(reader), name: "SomeGenderedIntFemale");
                break;
            case "SomeGenderedInt2":
                obj.SomeGenderedInt2.Male = SerializationHelper.StripNull(kernel.ReadString(reader), name: "SomeGenderedInt2Male");
                obj.SomeGenderedInt2.Female = SerializationHelper.StripNull(kernel.ReadString(reader), name: "SomeGenderedInt2Female");
                break;
            case "SomeGenderedInt3":
                obj.SomeGenderedInt3.Male = SerializationHelper.StripNull(kernel.ReadString(reader), name: "SomeGenderedInt3Male");
                obj.SomeGenderedInt3.Female = SerializationHelper.StripNull(kernel.ReadString(reader), name: "SomeGenderedInt3Female");
                break;
            default:
                break;
        }
    }

}

