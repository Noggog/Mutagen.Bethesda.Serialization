﻿using Loqui;
using Noggog.StructuredStrings;

namespace Mutagen.Bethesda.Serialization.SourceGenerator.Tests;

public interface ISubclassLoquiB : ISubclassLoquiBGetter, IAbstractBaseLoqui
{
}

public interface ISubclassLoquiBGetter : IAbstractBaseLoquiGetter
{
}

public class SubclassLoquiB : AbstractBaseLoqui, ISubclassLoquiB
{
    public override ILoquiRegistration Registration => new SubclassLoquiB_Registration();
}

internal class SubclassLoquiB_Registration : ARegistration
{
    public override ObjectKey ObjectKey { get; } = new(StaticProtocolKey, 5, 0);
    public override Type ClassType => typeof(SubclassLoquiB);
    public override Type GetterType => typeof(ISubclassLoquiBGetter);
    public override Type SetterType => typeof(ISubclassLoquiB);
    public override string Name => nameof(SubclassLoquiB);
}