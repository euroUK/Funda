﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Core.Test.Extensions
{
    public static class AsyncExtensions
    {
        public static async Task<ICollection<T>> AllResultsAsync<T>(this IAsyncEnumerable<T> asyncEnumerable)
        {
            if (null == asyncEnumerable)
            {
                throw new ArgumentNullException(nameof(asyncEnumerable));
            }

            var list = new List<T>();
            await foreach (var t in asyncEnumerable)
            {
                list.Add(t);
            }

            return list;
        }
    }
}
