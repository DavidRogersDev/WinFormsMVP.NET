﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsMVP.NET
{
    public class TypeListComparer<T> : IEqualityComparer<IEnumerable<T>>
        where T : class
    {
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            return x.SetEqual(y);
        }

        public int GetHashCode(IEnumerable<T> obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var result = obj
                .Aggregate<T, int?>(null, (current, o) =>
                    current == null ? o.GetHashCode() : current.Value | o.GetHashCode());

            return result ?? 0;
        }
    }
}