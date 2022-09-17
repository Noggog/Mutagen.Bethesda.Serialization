﻿//HintName: Group_Serializations.g.cs
using Mutagen.Bethesda.Serialization;
using Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

internal static class Group_Serialization
{
    public static void Serialize<TKernel, TWriteObject, T>(
        TWriteObject writer,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.IGroupGetter<T> item,
        MutagenSerializationWriterKernel<TKernel, TWriteObject> kernel)
        where T : class, IMajorRecordInternal
        where TKernel : ISerializationWriterKernel<TWriteObject>, new()
    {
        kernel.WriteInt32(writer, "SomeInt", item.SomeInt, default(int));
        kernel.WriteInt32(writer, "SomeInt2", item.SomeInt2, default(int));
    }

    public static Mutagen.Bethesda.Serialization.SourceGenerator.Tests.IGroup<T> Deserialize<TReadObject, T>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel)
        where T : class, IMajorRecordInternal
    {
        throw new NotImplementedException();
    }

}
