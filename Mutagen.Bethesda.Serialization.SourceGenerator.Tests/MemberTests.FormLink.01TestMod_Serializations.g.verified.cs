﻿//HintName: TestMod_Serializations.g.cs
using Mutagen.Bethesda.Serialization;
using Mutagen.Bethesda.Serialization.SourceGenerator.Tests;
namespace Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

internal static class TestMod_Serialization
{
    public static void Serialize<TWriteObject>(
        TWriteObject writer,
        Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ITestModGetter item,
        ISerializationWriterKernel<TWriteObject> kernel)
    {
        kernel.WriteFormKey(writer, "SomeFormKey", item.SomeFormKey.FormKeyNullable);
        kernel.WriteFormKey(writer, "SomeFormKey2", item.SomeFormKey2.FormKeyNullable);
        kernel.WriteFormKey(writer, "SomeFormKey3", item.SomeFormKey3.FormKeyNullable);
        kernel.WriteFormKey(writer, "SomeFormKey4", item.SomeFormKey4.FormKeyNullable);
        kernel.WriteFormKey(writer, "SomeFormKey5", item.SomeFormKey5.FormKeyNullable);
        kernel.WriteFormKey(writer, "SomeFormKey6", item.SomeFormKey6.FormKeyNullable);
    }

    public static Mutagen.Bethesda.Serialization.SourceGenerator.Tests.ITestMod Deserialize<TReadObject>(
        TReadObject reader,
        ISerializationReaderKernel<TReadObject> kernel)
    {
        throw new NotImplementedException();
    }

}

