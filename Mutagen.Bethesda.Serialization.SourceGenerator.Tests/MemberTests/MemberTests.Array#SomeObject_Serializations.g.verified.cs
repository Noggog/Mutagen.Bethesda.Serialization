﻿//HintName: SomeObject_Serializations.g.cs
using Loqui;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Serialization;
using Mutagen.Bethesda.Serialization.SourceGenerator.Tests;
using Noggog;

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
        where TWriteObject : IContainStreamPackage
    {
        SerializeFields<TKernel, TWriteObject>(
            writer: writer,
            item: item,
            kernel: kernel,
            metaData: metaData);
    }

    public static void SerializeFields<TKernel, TWriteObject>(
        TWriteObject writer,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter item,
        MutagenSerializationWriterKernel<TKernel, TWriteObject> kernel,
        SerializationMetaData metaData)
        where TKernel : ISerializationWriterKernel<TWriteObject>, new()
        where TWriteObject : IContainStreamPackage
    {
        if (item.SomeArray is {} checkedSomeArray
            && checkedSomeArray.Length > 0)
        {
            kernel.StartListSection(writer, "SomeArray");
            foreach (var listItem in checkedSomeArray)
            {
                kernel.WriteString(writer, null, listItem, default(string), checkDefaults: false);
            }
            kernel.EndListSection(writer);
        }
        if (item.SomeArray2 is {} checkedSomeArray2)
        {
            kernel.StartListSection(writer, "SomeArray2");
            foreach (var listItem in checkedSomeArray2)
            {
                kernel.WriteString(writer, null, listItem, default(string), checkDefaults: false);
            }
            kernel.EndListSection(writer);
        }
        if (item.SomeArray3 is {} checkedSomeArray3
            && checkedSomeArray3.Length > 0)
        {
            kernel.StartListSection(writer, "SomeArray3");
            foreach (var listItem in checkedSomeArray3)
            {
                kernel.WriteString(writer, null, listItem, default(string), checkDefaults: false);
            }
            kernel.EndListSection(writer);
        }
    }

    public static bool HasSerializationItems(
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter? item,
        SerializationMetaData metaData)
    {
        if (item == null) return false;
        if (item.SomeArray.Length > 0) return true;
        if (item.SomeArray2?.Length > 0) return true;
        if (item.SomeArray3.Length > 0) return true;
        return false;
    }

    public static Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject Deserialize<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        SerializationMetaData metaData)
        where TReadObject : IContainStreamPackage
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
        where TReadObject : IContainStreamPackage
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
        where TReadObject : IContainStreamPackage
    {
        switch (name)
        {
            case "SomeArray":
                {
                    obj.SomeArray = SerializationHelper.ReadArray(
                        reader: reader,
                        kernel: kernel,
                        metaData: metaData,
                        itemReader: (r, k, m) =>
                        {
                            return SerializationHelper.StripNull(kernel.ReadString(r), name: "SomeArray");
                        });
                }
                break;
            case "SomeArray2":
                {
                    obj.SomeArray2 = SerializationHelper.ReadArray(
                        reader: reader,
                        kernel: kernel,
                        metaData: metaData,
                        itemReader: (r, k, m) =>
                        {
                            return SerializationHelper.StripNull(kernel.ReadString(r), name: "SomeArray2");
                        });
                }
                break;
            case "SomeArray3":
                {
                    SerializationHelper.ReadIntoArray(
                        reader: reader,
                        arr: obj.SomeArray3,
                        kernel: kernel,
                        metaData: metaData,
                        itemReader: (r, k, m) =>
                        {
                            return SerializationHelper.StripNull(kernel.ReadString(r), name: "SomeArray3");
                        });
                }
                break;
            default:
                break;
        }
    }

}

