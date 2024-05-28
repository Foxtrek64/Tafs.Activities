using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tafs.Activities.MiniMessage.Models.Text
{
    /// <summary>
    /// A configuration for how a series of components can be joined.
    /// </summary>
    /// <remarks>
    /// A join configuration consists of the following parts:
    /// <list type="table">
    /// <item>
    ///     <term>Prefix (optional)</term>
    ///     <description>A component to be prepended to the resulting component.</description>
    /// </item>
    /// <item>
    ///     <term>Separator (optional)</term>
    ///     <description>A component to be placed between each component.</description>
    /// </item>
    /// <item>
    ///     <term>Last Separator (optional)</term>
    ///     <description>A component to be placed between the last two components.</description>
    /// </item>
    /// <item>
    ///     <term>Suffix (optional)</term>
    ///     <description>A component to be appended to the resulting component.</description>
    /// </item>
    /// <item>
    ///     <term>Converter (required, defaults to <see cref="IComponentLike.AsComponent"/></term>
    ///     <description>A function to change each <see cref="IComponentLike"/> that is being joined into a <see cref="IComponent"/>.</description>
    /// </item>
    /// <item>
    ///     <term>Filter (required, defaults to <see langword="true"/>.</term>
    ///     <description>A function that specifies if a given component should be included in the join process.</description>
    /// </item>
    /// <item>
    ///     <term>Root <see cref="Style"/> (required, defaults to <see cref="Style.Empty"/>.</term>
    ///     <description>The style of the parent component that contains the joined components.</description>
    /// </item>
    /// </list>
    /// </remarks>
    [PublicAPI]
    public sealed class JoinConfiguration : Examinable
    {
        
    }
}
