//
//  SPDX-FileName: DictionaryExtensions.cs
//  SPDX-FileCopyrightText: Copyright (c) TAFS, LLC.
//  SPDX-License-Identifier: MIT
//

using System.Collections.Generic;

namespace Tafs.Activities.FileChunks.Extensions
{
#if NET461_OR_GREATER
    /// <summary>
    /// Provides a set of extensions for <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Deconstructs a <see cref="KeyValuePair{TKey, TValue}"/> into a discrete key and value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to deconstruct.</typeparam>
        /// <typeparam name="TValue">The type of the value to deconstruct.</typeparam>
        /// <param name="kvp">The kvp to deconstruct.</param>
        /// <param name="key">The resulting key.</param>
        /// <param name="value">The resulting value.</param>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value)
        {
            key = kvp.Key;
            value = kvp.Value;
        }
    }
#endif
}
