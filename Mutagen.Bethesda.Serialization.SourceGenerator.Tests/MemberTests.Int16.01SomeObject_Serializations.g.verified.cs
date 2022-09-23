﻿//HintName: SomeObject_Serializations.g.cs
using Mutagen.Bethesda.Serialization;
using Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

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
        kernel.WriteInt16(writer, "SomeMember0", item.SomeMember0, default(short));
        kernel.WriteInt16(writer, "SomeMember1", item.SomeMember1, default(Int16));
        kernel.WriteInt16(writer, "SomeMember2", item.SomeMember2, default(short?));
        kernel.WriteInt16(writer, "SomeMember3", item.SomeMember3, default(Int16?));
        kernel.WriteInt16(writer, "SomeMember4", item.SomeMember4, default(Nullable<short>));
        kernel.WriteInt16(writer, "SomeMember5", item.SomeMember5, default(Nullable<Int16>));
        kernel.WriteInt16(writer, "SomeMember6", item.SomeMember6, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember6Default);
        kernel.WriteInt16(writer, "SomeMember7", item.SomeMember7, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember7Default);
        kernel.WriteInt16(writer, "SomeMember8", item.SomeMember8, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember8Default);
        kernel.WriteInt16(writer, "SomeMember9", item.SomeMember9, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember9Default);
        kernel.WriteInt16(writer, "SomeMember10", item.SomeMember10, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember10Default);
        kernel.WriteInt16(writer, "SomeMember11", item.SomeMember11, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember11Default);
    }

    public static bool HasSerializationItems(
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter item,
        SerializationMetaData metaData)
    {
        if (!EqualityComparer<short>.Default.Equals(item.SomeMember0, default(short))) return true;
        if (!EqualityComparer<Int16>.Default.Equals(item.SomeMember1, default(Int16))) return true;
        if (!EqualityComparer<short?>.Default.Equals(item.SomeMember2, default(short?))) return true;
        if (!EqualityComparer<Int16?>.Default.Equals(item.SomeMember3, default(Int16?))) return true;
        if (!EqualityComparer<Nullable<short>>.Default.Equals(item.SomeMember4, default(Nullable<short>))) return true;
        if (!EqualityComparer<Nullable<Int16>>.Default.Equals(item.SomeMember5, default(Nullable<Int16>))) return true;
        if (!EqualityComparer<short>.Default.Equals(item.SomeMember6, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember6Default)) return true;
        if (!EqualityComparer<Int16>.Default.Equals(item.SomeMember7, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember7Default)) return true;
        if (!EqualityComparer<short?>.Default.Equals(item.SomeMember8, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember8Default)) return true;
        if (!EqualityComparer<Int16?>.Default.Equals(item.SomeMember9, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember9Default)) return true;
        if (!EqualityComparer<Nullable<short>>.Default.Equals(item.SomeMember10, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember10Default)) return true;
        if (!EqualityComparer<Nullable<Int16>>.Default.Equals(item.SomeMember11, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember11Default)) return true;
        return false;
    }

    public static Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObject Deserialize<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel)
    {
        while (kernel.TryGetNextField(out var name))
        {
            switch (name)
            {
                case: "SomeMember0":
                    item.SomeMember0 = kernel.ReadInt16(writer);
                case: "SomeMember1":
                    item.SomeMember1 = kernel.ReadInt16(writer);
                case: "SomeMember2":
                    item.SomeMember2 = kernel.ReadInt16(writer);
                case: "SomeMember3":
                    item.SomeMember3 = kernel.ReadInt16(writer);
                case: "SomeMember4":
                    item.SomeMember4 = kernel.ReadInt16(writer);
                case: "SomeMember5":
                    item.SomeMember5 = kernel.ReadInt16(writer);
                case: "SomeMember6":
                    item.SomeMember6 = kernel.ReadInt16(writer);
                case: "SomeMember7":
                    item.SomeMember7 = kernel.ReadInt16(writer);
                case: "SomeMember8":
                    item.SomeMember8 = kernel.ReadInt16(writer);
                case: "SomeMember9":
                    item.SomeMember9 = kernel.ReadInt16(writer);
                case: "SomeMember10":
                    item.SomeMember10 = kernel.ReadInt16(writer);
                case: "SomeMember11":
                    item.SomeMember11 = kernel.ReadInt16(writer);
                default:
                    break;
            }
        }
    }

}

