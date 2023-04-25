﻿//HintName: SomeObject_Serializations.g.cs
using Loqui;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Serialization;
using Mutagen.Bethesda.Serialization.SourceGenerator.Tests;
using Mutagen.Bethesda.Serialization.Utility;
using Noggog;
using Noggog.WorkEngine;
using System.Threading.Tasks;

#nullable enable

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

internal static class SomeObject_Serialization
{
    public static async Task Serialize<TKernel, TWriteObject>(
        TWriteObject writer,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter item,
        MutagenSerializationWriterKernel<TKernel, TWriteObject> kernel,
        SerializationMetaData metaData)
        where TKernel : ISerializationWriterKernel<TWriteObject>, new()
        where TWriteObject : IContainStreamPackage
    {
        await SerializeFields<TKernel, TWriteObject>(
            writer: writer,
            item: item,
            kernel: kernel,
            metaData: metaData);
    }

    public static async Task SerializeFields<TKernel, TWriteObject>(
        TWriteObject writer,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter item,
        MutagenSerializationWriterKernel<TKernel, TWriteObject> kernel,
        SerializationMetaData metaData)
        where TKernel : ISerializationWriterKernel<TWriteObject>, new()
        where TWriteObject : IContainStreamPackage
    {
        if (item.SomeFormKeys is {} checkedSomeFormKeys
            && checkedSomeFormKeys.Count > 0)
        {
            kernel.StartListSection(writer, "SomeFormKeys");
            foreach (var listItem in checkedSomeFormKeys)
            {
                kernel.WriteFormKey(writer, null, listItem.FormKeyNullable, default(FormKey), checkDefaults: false);
            }
            kernel.EndListSection(writer);
        }
        if (item.SomeFormKeys2 is {} checkedSomeFormKeys2
            && checkedSomeFormKeys2.Count > 0)
        {
            kernel.StartListSection(writer, "SomeFormKeys2");
            foreach (var listItem in checkedSomeFormKeys2)
            {
                kernel.WriteFormKey(writer, null, listItem.FormKeyNullable, default(FormKey), checkDefaults: false);
            }
            kernel.EndListSection(writer);
        }
    }

    public static bool HasSerializationItems(
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter? item,
        SerializationMetaData metaData)
    {
        if (item == null) return false;
        if (item.SomeFormKeys.Count > 0) return true;
        if (item.SomeFormKeys2.Count > 0) return true;
        return false;
    }

    public static async Task<Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject> Deserialize<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        SerializationMetaData metaData)
        where TReadObject : IContainStreamPackage
    {
        var obj = new Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject();
        await DeserializeInto<TReadObject>(
            reader: reader,
            kernel: kernel,
            obj: obj,
            metaData: metaData);
        return obj;
    }

    public static async Task DeserializeSingleFieldInto<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObject obj,
        SerializationMetaData metaData,
        string name)
        where TReadObject : IContainStreamPackage
    {
        switch (name)
        {
            case "SomeFormKeys":
                kernel.StartListSection(reader);
                while (kernel.TryHasNextItem(reader))
                {
                    var item = SerializationHelper.StripNull(kernel.ReadFormKey(reader), "SomeFormKeys").ToLink<Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ITestMajorRecordGetter>();
                    obj.SomeFormKeys.Add(item);
                }
                kernel.EndListSection(reader);
                break;
            case "SomeFormKeys2":
                kernel.StartListSection(reader);
                while (kernel.TryHasNextItem(reader))
                {
                    var item = SerializationHelper.StripNull(kernel.ReadFormKey(reader), "SomeFormKeys2").ToLink<Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ITestMajorRecordGetter>();
                    obj.SomeFormKeys2.Add(item);
                }
                kernel.EndListSection(reader);
                break;
            default:
                break;
        }
    }
    
    public static async Task DeserializeInto<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObject obj,
        SerializationMetaData metaData)
        where TReadObject : IContainStreamPackage
    {
        while (kernel.TryGetNextField(reader, out var name))
        {
            await DeserializeSingleFieldInto(
                reader: reader,
                kernel: kernel,
                obj: obj,
                metaData: metaData,
                name: name);
        }

    }

}

