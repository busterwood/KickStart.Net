﻿using System.Collections.Generic;

namespace KickStart.Net.Extensions
{
    public static class DictionarySafeExtensions
    {
        /// <summary>
        /// Returns false when <paramref name="key"/> is null, otherwise forward call to <see cref="T:IDictionary{TK, TV}.ContainsKey"/> 
        /// </summary>
        public static bool SafeContainsKey<TK, TV>(this IDictionary<TK, TV> dictionary, TK key)
        {
            if (key == null)
                return false;
            return dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Returns false when <paramref name="key"/> is null, otherwise forward call to <see cref="T:IDictionary{TK, TV}.Remove"/> 
        /// </summary>
        public static bool SafeRemove<TK, TV>(this IDictionary<TK, TV> dictionary, TK key)
        {
            if (key == null)
                return false;
            return dictionary.Remove(key);
        }

        /// <summary>
        /// Returns false when <paramref name="key"/> is null, or <paramref name="dictionary"/> doesn't contains <paramref name="key"/>, 
        /// otherwise forward call to <see cref="T:IDictionary{TK, TV}"/>'s indexer.
        /// </summary>
        public static TV SafeGet<TK, TV>(this IDictionary<TK, TV> dictionary, TK key)
        {
            if (key == null)
                return default(TV);
            if (!dictionary.ContainsKey(key))
                return default(TV);
            return dictionary[key];
        }

        /// <summary>
        /// Returns false when <paramref name="key"/> is null, or <paramref name="dictionary"/> is readonly.
        /// Otherwise set the <paramref name="value"/> using <paramref name="key"/> via indexer.
        /// </summary>
        public static void SafeSet<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, TV value)
        {
            if (key == null)
                return;
            if (dictionary.IsReadOnly)
                return;
            dictionary[key] = value;
        }

        /// <summary>
        /// Tries to add <paramref name="key"/> and <paramref name="value"/> into <paramref name="dictionary"/> and do nothing
        /// if <paramref name="key"/> is null, or <paramref name="dictionary"/> is readonly, or <paramref name="dictionary"/>
        /// already contains <paramref name="key"/>. 
        /// </summary>
        public static void SafeAdd<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, TV value)
        {
            if (key == null)
                return;
            if (dictionary.IsReadOnly)
                return;
            if (dictionary.ContainsKey(key))
                return;
            dictionary.Add(key, value);
        }
    }
}
