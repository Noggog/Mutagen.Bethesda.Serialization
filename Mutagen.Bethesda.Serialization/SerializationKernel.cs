using System.Drawing;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Serialization.Streams;
using Mutagen.Bethesda.Strings;
using Noggog;
using Noggog.WorkEngine;

namespace Mutagen.Bethesda.Serialization;

public delegate TObject Read<TKernel, TReaderObject, TObject>(
    TReaderObject reader,
    TKernel kernel,
    SerializationMetaData metaData)
    where TKernel : ISerializationReaderKernel<TReaderObject>;

public delegate Task<TObject> ReadAsync<TKernel, TReaderObject, TObject>(
    TReaderObject reader,
    TKernel kernel,
    SerializationMetaData metaData)
    where TKernel : ISerializationReaderKernel<TReaderObject>;

public delegate Task<TObject> ReadNamedAsync<TKernel, TReaderObject, TObject>(
    TReaderObject reader,
    TKernel kernel,
    SerializationMetaData metaData,
    string name)
    where TKernel : ISerializationReaderKernel<TReaderObject>;

public delegate Task ReadInto<TKernel, TReaderObject, TObject>(
    TReaderObject reader,
    TObject obj,
    TKernel kernel,
    SerializationMetaData metaData)
    where TKernel : ISerializationReaderKernel<TReaderObject>;

public delegate Task ReadNamedInto<TKernel, TReaderObject, TObject>(
    TReaderObject reader,
    TObject obj,
    TKernel kernel,
    SerializationMetaData metaData,
    string name)
    where TKernel : ISerializationReaderKernel<TReaderObject>;

public interface ISerializationReaderKernel<TReaderObject>
{
    public string ExpectedExtension { get; }
    public TReaderObject GetNewObject(StreamPackage stream);
    public bool TryGetNextField(TReaderObject reader, out string name);
    public Type GetNextType(TReaderObject reader, string namespaceString);
    public FormKey ExtractFormKey(TReaderObject reader);
    public void Skip(TReaderObject reader);
    public char? ReadChar(TReaderObject reader);
    public bool? ReadBool(TReaderObject reader);
    public TEnum? ReadEnum<TEnum>(TReaderObject reader)
        where TEnum : struct, Enum, IConvertible;
    public string? ReadString(TReaderObject reader);
    public sbyte? ReadInt8(TReaderObject reader);
    public short? ReadInt16(TReaderObject reader);
    public int? ReadInt32(TReaderObject reader);
    public long? ReadInt64(TReaderObject reader);
    public byte? ReadUInt8(TReaderObject reader);
    public ushort? ReadUInt16(TReaderObject reader);
    public uint? ReadUInt32(TReaderObject reader);
    public ulong? ReadUInt64(TReaderObject reader);
    public float? ReadFloat(TReaderObject reader);
    public ModKey? ReadModKey(TReaderObject reader);
    public FormKey? ReadFormKey(TReaderObject reader);
    public Color? ReadColor(TReaderObject reader);
    public RecordType? ReadRecordType(TReaderObject reader);
    public P2Int? ReadP2Int(TReaderObject reader);
    public P2Int16? ReadP2Int16(TReaderObject reader);
    public P2Float? ReadP2Float(TReaderObject reader);
    public P3Float? ReadP3Float(TReaderObject reader);
    public P3UInt8? ReadP3UInt8(TReaderObject reader);
    public P3Int16? ReadP3Int16(TReaderObject reader);
    public P3UInt16? ReadP3UInt16(TReaderObject reader);
    public Percent? ReadPercent(TReaderObject reader);
    public TranslatedString? ReadTranslatedString(TReaderObject reader);
    public MemorySlice<byte>? ReadBytes(TReaderObject reader);
    public Task<TObject?> ReadLoqui<TObject>(
        TReaderObject reader,
        SerializationMetaData serializationMetaData,
        ReadAsync<ISerializationReaderKernel<TReaderObject>, TReaderObject, TObject> readCall);

    #region List
    
    public void StartListSection(TReaderObject reader);
    public void EndListSection(TReaderObject reader);
    public bool TryHasNextItem(TReaderObject reader);

    #endregion

    #region Dict

    public void StartDictionarySection(TReaderObject reader);
    public void EndDictionarySection(TReaderObject reader);
    public bool TryHasNextDictionaryItem(TReaderObject reader);
    public void StartDictionaryKey(TReaderObject reader);
    public void EndDictionaryKey(TReaderObject reader);
    public void StartDictionaryValue(TReaderObject reader);
    public void EndDictionaryValue(TReaderObject reader);
    public void EndDictionaryItem(TReaderObject reader);

    #endregion

    #region Array2d

    public void StartArray2dSection(TReaderObject reader);
    public void EndArray2dSection(TReaderObject reader);
    public bool TryHasNextArray2dXItem(TReaderObject reader);
    public void StartArray2dXItem(TReaderObject reader);
    public void EndArray2dXItem(TReaderObject reader);
    public bool TryHasNextArray2dYSection(TReaderObject reader);
    public void StartArray2dYSection(TReaderObject reader);
    public void EndArray2dYSection(TReaderObject reader);

    #endregion
}

public delegate void Write<TKernel, TWriterObject, TObject>(
    TWriterObject writer,
    TObject obj,
    MutagenSerializationWriterKernel<TKernel, TWriterObject> kernel,
    SerializationMetaData metaData)
    where TKernel : ISerializationWriterKernel<TWriterObject>, new();

public delegate Task WriteAsync<TKernel, TWriterObject, TObject>(
    TWriterObject writer,
    TObject obj,
    MutagenSerializationWriterKernel<TKernel, TWriterObject> kernel,
    SerializationMetaData metaData)
    where TKernel : ISerializationWriterKernel<TWriterObject>, new();

public delegate bool HasSerializationItems<TObject>(TObject? obj, SerializationMetaData metaData);

public interface ISerializationWriterKernel<TWriterObject>
{
    public string ExpectedExtension { get; }
    public TWriterObject GetNewObject(StreamPackage stream);
    public void Finalize(StreamPackage stream, TWriterObject writer);
    public void WriteType(TWriterObject writer, Type type);
    public void WriteChar(TWriterObject writer, string? fieldName, char? item);
    public void WriteBool(TWriterObject writer, string? fieldName, bool? item);
    public void WriteString(TWriterObject writer, string? fieldName, string? item);
    public void WriteInt8(TWriterObject writer, string? fieldName, sbyte? item);
    public void WriteInt16(TWriterObject writer, string? fieldName, short? item);
    public void WriteInt32(TWriterObject writer, string? fieldName, int? item);
    public void WriteInt64(TWriterObject writer, string? fieldName, long? item);
    public void WriteUInt8(TWriterObject writer, string? fieldName, byte? item);
    public void WriteUInt16(TWriterObject writer, string? fieldName, ushort? item);
    public void WriteUInt32(TWriterObject writer, string? fieldName, uint? item);
    public void WriteUInt64(TWriterObject writer, string? fieldName, ulong? item);
    public void WriteFloat(TWriterObject writer, string? fieldName, float? item);
    public void WriteModKey(TWriterObject writer, string? fieldName, ModKey? item);
    public void WriteFormKey(TWriterObject writer, string? fieldName, FormKey? item);
    public void WriteRecordType(TWriterObject writer, string? fieldName, RecordType? item);
    public void WriteP2Int(TWriterObject writer, string? fieldName, P2Int? item);
    public void WriteP2Int16(TWriterObject writer, string? fieldName, P2Int16? item);
    public void WriteP2Float(TWriterObject writer, string? fieldName, P2Float? item);
    public void WriteP3Float(TWriterObject writer, string? fieldName, P3Float? item);
    public void WriteP3UInt8(TWriterObject writer, string? fieldName, P3UInt8? item);
    public void WriteP3Int16(TWriterObject writer, string? fieldName, P3Int16? item);
    public void WriteP3UInt16(TWriterObject writer, string? fieldName, P3UInt16? item);
    public void WritePercent(TWriterObject writer, string? fieldName, Percent? item);
    public void WriteColor(TWriterObject writer, string? fieldName, Color? item);
    public void WriteTranslatedString(TWriterObject writer, string? fieldName, ITranslatedStringGetter? item);
    public void WriteBytes(TWriterObject writer, string? fieldName, ReadOnlyMemorySlice<byte>? item);
    public void WriteEnum<TEnum>(TWriterObject writer, string? fieldName, TEnum? item)
        where TEnum : struct, Enum, IConvertible;
    public void WriteWithName<TKernel, TObject>(
        MutagenSerializationWriterKernel<TKernel, TWriterObject> kernel,
        TWriterObject writer, 
        string? fieldName, 
        TObject item,
        SerializationMetaData serializationMetaData,
        WriteAsync<TKernel, TWriterObject, TObject> writeCall)
        where TKernel : ISerializationWriterKernel<TWriterObject>, new();
    public void WriteLoqui<TKernel, TObject>(
        MutagenSerializationWriterKernel<TKernel, TWriterObject> kernel, 
        TWriterObject writer,
        string? fieldName,
        TObject item,
        SerializationMetaData serializationMetaData,
        Write<TKernel, TWriterObject, TObject> writeCall)
        where TKernel : ISerializationWriterKernel<TWriterObject>, new();
    public Task WriteLoqui<TKernel, TObject>(
        MutagenSerializationWriterKernel<TKernel, TWriterObject> kernel, 
        TWriterObject writer,
        string? fieldName,
        TObject item,
        SerializationMetaData serializationMetaData,
        WriteAsync<TKernel, TWriterObject, TObject> writeCall)
        where TKernel : ISerializationWriterKernel<TWriterObject>, new();

    #region List
    
    public void StartListSection(TWriterObject writer, string? fieldName);
    public void EndListSection(TWriterObject writer);
    
    #endregion

    #region Dict

    public void StartDictionarySection(TWriterObject writer, string? fieldName);
    public void EndDictionarySection(TWriterObject writer);
    public void StartDictionaryItem(TWriterObject writer);
    public void EndDictionaryItem(TWriterObject writer);
    public void StartDictionaryKey(TWriterObject writer);
    public void EndDictionaryKey(TWriterObject writer);
    public void StartDictionaryValue(TWriterObject writer);
    public void EndDictionaryValue(TWriterObject writer);

    #endregion

    #region Array2d

    public void StartArray2dSection(TWriterObject writer, string? fieldName);
    public void EndArray2dSection(TWriterObject writer);
    public void StartArray2dXItem(TWriterObject writer);
    public void EndArray2dXItem(TWriterObject writer);
    public void StartArray2dYSection(TWriterObject writer);
    public void EndArray2dYSection(TWriterObject writer);

    #endregion
}