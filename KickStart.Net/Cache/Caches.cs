﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace KickStart.Net.Cache
{
    class LocalManualCache<K, V> : ICache<K, V>
    {
        [VisibleForTesting]
        internal readonly LocalCache<K, V> _localCache;

        public LocalManualCache(CacheBuilder<K, V> builder)
        {
            _localCache = new LocalCache<K, V>(builder, null);
        }

        protected LocalManualCache(CacheBuilder<K, V> builder, ICacheLoader<K, V> loader)
        {
            _localCache = new LocalCache<K, V>(builder, loader);
        } 

        public V GetIfPresent(K key)
        {
            return _localCache.GetIfPresent(key);
        }

        public V Get(K key, Func<V> loader)
        {
            Contract.Assert(loader != null);
            return _localCache.Get(key, CacheLoaders.From<K, V>(loader));
        }

        public IReadOnlyDictionary<K, V> GetAllPresents(IEnumerable<K> keys)
        {
            return _localCache.GetAllPresent(keys);
        }

        public void Put(K key, V value)
        {
            _localCache.Put(key, value);
        }

        public void PutAll(IReadOnlyDictionary<K, V> map)
        {
            _localCache.PutAll(map);
        }

        public void Invalidate(K key)
        {
            Contract.Assert(key != null);
            _localCache.Remove(key);
        }

        public void InvalidateAll(IEnumerable<K> keys)
        {
            _localCache.InvalidateAll(keys);
        }

        public void InvalidateAll()
        {
            _localCache.Clear();
        }

        public long Size()
        {
            return _localCache.LongSize();
        }

        public IReadOnlyDictionary<K, V> AsMap()
        {
            throw new NotImplementedException();
        }

        public void CleanUp()
        {
            _localCache.CleanUp();
        }

        public CacheStats Stats()
        {
            var aggregator = new SimpleStatsCounter();
            aggregator.IncrementBy(_localCache.GlobalStatsCounter);
            foreach (var segment in _localCache.Segments)
                aggregator.IncrementBy(segment.StatsCounter);
            return aggregator.Snapshot();
        }
    }

    class LocalLoadingCache<K, V> : LocalManualCache<K, V>, ILoadingCache<K, V>
    {
        public LocalLoadingCache(CacheBuilder<K, V> builder, ICacheLoader<K, V> loader) 
            : base(builder, loader)
        {
        }

        public V Get(K key)
        {
            return _localCache.GetOrLoad(key);
        }

        public IReadOnlyDictionary<K, V> GetAll(IEnumerable<K> keys)
        {
            return _localCache.GetAll(keys);
        }

        public void Refresh(K key)
        {
            _localCache.Refresh(key);
        }
    }
}
