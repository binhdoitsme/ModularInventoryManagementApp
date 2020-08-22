using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ModularInventoryManagement.Infrastructure.Utils
{
    class DictionaryComparer<TKey, TValue>
        : IEqualityComparer<IDictionary<TKey, TValue>>
    {
        private readonly IEqualityComparer<TValue> valueComparer;
        public DictionaryComparer(IEqualityComparer<TValue> valueComparer = null)
        {
            this.valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
        }
        public bool Equals(IDictionary<TKey, TValue> x, IDictionary<TKey, TValue> y)
        {
            if (x.Count != y.Count)
                return false;
            if (x.Keys.Except(y.Keys).Any())
                return false;
            if (y.Keys.Except(x.Keys).Any())
                return false;
            foreach (var pair in x)
            {
                var thisValue = pair.Value;
                var otherValue = y[pair.Key];
                if (thisValue is IEnumerable<object> enumerable)
                {
                    return enumerable.SequenceEqual((IEnumerable<object>)otherValue);
                }
                if (!valueComparer.Equals(pair.Value, y[pair.Key]))
                    return false;
            }
            return true;
        }

        public int GetHashCode(IDictionary<TKey, TValue> obj)
        {
            throw new NotImplementedException();
        }
    }
}
