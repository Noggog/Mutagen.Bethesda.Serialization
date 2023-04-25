﻿//HintName: SomeMod_Serializations.g.cs
using Loqui;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Serialization;
using Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests;
using Mutagen.Bethesda.Serialization.Utility;
using Noggog;
using Noggog.WorkEngine;
using System.Threading.Tasks;

#nullable enable

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

internal static class SomeMod_Serialization
{
    public static async Task Serialize<TKernel, TWriteObject>(
        TWriteObject writer,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeModGetter item,
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
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeModGetter item,
        MutagenSerializationWriterKernel<TKernel, TWriteObject> kernel,
        SerializationMetaData metaData)
        where TKernel : ISerializationWriterKernel<TWriteObject>, new()
        where TWriteObject : IContainStreamPackage
    {
        await SerializationHelper.WriteGroup<TKernel, TWriteObject, IGroupGetter<ISomeMajorRecordGetter>, ISomeMajorRecordGetter>(
            writer: writer,
            group: item.SomeGroup1,
            fieldName: "SomeGroup1",
            metaData: metaData,
            kernel: kernel,
            groupWriter: static (w, i, k, m) => <global namespace>.Group_Serialization.Serialize<TKernel, TWriteObject, ISomeMajorRecordGetter>(w, i, k, m),
            itemWriter: static (w, i, k, m) => Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeMajorRecord_Serialization.Serialize<TKernel, TWriteObject>(w, i, k, m));
    }

    public static bool HasSerializationItems(
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeModGetter? item,
        SerializationMetaData metaData)
    {
        if (item == null) return false;
        if (item.SomeGroup1.Count > 0) return true;
        return false;
    }

    public static async Task<Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeMod> Deserialize<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        SerializationMetaData metaData)
        where TReadObject : IContainStreamPackage
    {
        var obj = new Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeMod();
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
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeMod obj,
        SerializationMetaData metaData,
        string name)
        where TReadObject : IContainStreamPackage
    {
        switch (name)
        {
            case "SomeGroup1":
                await SerializationHelper.ReadIntoGroup<ISerializationReaderKernel<TReadObject>, TReadObject, IGroup<SomeMajorRecord>, SomeMajorRecord>(
                    reader: reader,
                    group: obj.SomeGroup1,
                    metaData: metaData,
                    kernel: kernel,
                    groupReader: static (r, i, k, m, n) => <global namespace>.Group_Serialization.DeserializeSingleFieldInto<TReadObject, SomeMajorRecord>(r, k, i, m, n),
                    itemReader: static (r, k, m) => k.ReadLoqui(r, m, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeMajorRecord_Serialization.Deserialize<TReadObject>));
                break;
            default:
                break;
        }
    }
    
    public static async Task DeserializeInto<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeMod obj,
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

