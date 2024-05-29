using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tafs.Activities.MiniMessage.Models.Tag
{
    /// <summary>
    /// Defines a tag for the MiniMessage language.
    /// </summary>
    /// <remarks>
    /// All implementations of <see cref="ITag"/> must implement one of <see cref="IInserting"/>, <see cref="IModifying"/>,
    /// <see cref="IParserDirective"/>, or <see cref="IPreProcess"/>.
    /// </remarks>
    [PublicAPI]
    public interface ITag
    {

    }
}
