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
        kernel.WriteFloat(writer, "SomeMember0", item.SomeMember0, default(float));
        kernel.WriteFloat(writer, "SomeMember1", item.SomeMember1, default(Single));
        kernel.WriteFloat(writer, "SomeMember2", item.SomeMember2, default(float?));
        kernel.WriteFloat(writer, "SomeMember3", item.SomeMember3, default(Single?));
        kernel.WriteFloat(writer, "SomeMember4", item.SomeMember4, default(Nullable<float>));
        kernel.WriteFloat(writer, "SomeMember5", item.SomeMember5, default(Nullable<Single>));
        kernel.WriteFloat(writer, "SomeMember6", item.SomeMember6, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember6Default);
        kernel.WriteFloat(writer, "SomeMember7", item.SomeMember7, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember7Default);
        kernel.WriteFloat(writer, "SomeMember8", item.SomeMember8, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember8Default);
        kernel.WriteFloat(writer, "SomeMember9", item.SomeMember9, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember9Default);
        kernel.WriteFloat(writer, "SomeMember10", item.SomeMember10, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember10Default);
        kernel.WriteFloat(writer, "SomeMember11", item.SomeMember11, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember11Default);
    }

    public static bool HasSerializationItems(
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ISomeObjectGetter item,
        SerializationMetaData metaData)
    {
        if (!EqualityComparer<float>.Default.Equals(item.SomeMember0, default(float))) return true;
        if (!EqualityComparer<Single>.Default.Equals(item.SomeMember1, default(Single))) return true;
        if (!EqualityComparer<float?>.Default.Equals(item.SomeMember2, default(float?))) return true;
        if (!EqualityComparer<Single?>.Default.Equals(item.SomeMember3, default(Single?))) return true;
        if (!EqualityComparer<Nullable<float>>.Default.Equals(item.SomeMember4, default(Nullable<float>))) return true;
        if (!EqualityComparer<Nullable<Single>>.Default.Equals(item.SomeMember5, default(Nullable<Single>))) return true;
        if (!EqualityComparer<float>.Default.Equals(item.SomeMember6, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember6Default)) return true;
        if (!EqualityComparer<Single>.Default.Equals(item.SomeMember7, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember7Default)) return true;
        if (!EqualityComparer<float?>.Default.Equals(item.SomeMember8, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember8Default)) return true;
        if (!EqualityComparer<Single?>.Default.Equals(item.SomeMember9, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember9Default)) return true;
        if (!EqualityComparer<Nullable<float>>.Default.Equals(item.SomeMember10, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember10Default)) return true;
        if (!EqualityComparer<Nullable<Single>>.Default.Equals(item.SomeMember11, Mutagen.Bethesda.Serialization.SourceGenerator.Tests.SomeObject.SomeMember11Default)) return true;
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
                    item.SomeMember0 = kernel.ReadFloat(writer);
                case: "SomeMember1":
                    item.SomeMember1 = kernel.ReadFloat(writer);
                case: "SomeMember2":
                    item.SomeMember2 = kernel.ReadFloat(writer);
                case: "SomeMember3":
                    item.SomeMember3 = kernel.ReadFloat(writer);
                case: "SomeMember4":
                    item.SomeMember4 = kernel.ReadFloat(writer);
                case: "SomeMember5":
                    item.SomeMember5 = kernel.ReadFloat(writer);
                case: "SomeMember6":
                    item.SomeMember6 = kernel.ReadFloat(writer);
                case: "SomeMember7":
                    item.SomeMember7 = kernel.ReadFloat(writer);
                case: "SomeMember8":
                    item.SomeMember8 = kernel.ReadFloat(writer);
                case: "SomeMember9":
                    item.SomeMember9 = kernel.ReadFloat(writer);
                case: "SomeMember10":
                    item.SomeMember10 = kernel.ReadFloat(writer);
                case: "SomeMember11":
                    item.SomeMember11 = kernel.ReadFloat(writer);
                default:
                    break;
            }
        }
    }

}

