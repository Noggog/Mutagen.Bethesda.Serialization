﻿//HintName: TestMod_Serializations.g.cs
using Mutagen.Bethesda.Serialization;
using Mutagen.Bethesda.Serialization.SourceGenerator.Tests;
namespace Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

internal static class TestMod_Serialization
{
    public static void Serialize<TWriteObject>(
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ITestModGetter item,
        TWriteObject writer,
        ISerializationWriterKernel<TWriteObject> kernel)
    {
        kernel.WriteBytes(writer, "SomeBytes", item.SomeBytes);
        kernel.WriteBytes(writer, "SomeBytes2", item.SomeBytes2);
        kernel.WriteBytes(writer, "SomeBytes3", item.SomeBytes3);
        kernel.WriteBytes(writer, "SomeBytes4", item.SomeBytes4);
        kernel.WriteBytes(writer, "SomeBytes5", item.SomeBytes5);
    }

    public static Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ITestModGetter Deserialize<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel)
    {
        throw new NotImplementedException();
    }

}
