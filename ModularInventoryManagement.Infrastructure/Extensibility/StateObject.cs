using ModularInventoryManagement.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModularInventoryManagement.Infrastructure.Extensibility
{
    public class StateObject : IStateObject
    {
        private readonly IDictionary<string, object> stateDictionary;
        private readonly IEqualityComparer<IDictionary<string, object>> comparer;

        public event EventHandler<object> StateChanged;

        public StateObject()
        {
            stateDictionary = new Dictionary<string, object>();
            comparer = new DictionaryComparer<string, object>();
        }

        private void NotifyStateChanged(string key = "")
        {
            StateChanged?.Invoke(this, key);
        }

        public T GetState<T>(string key) where T : class
        {
            if (!stateDictionary.ContainsKey(key)) return null;
            return (T)stateDictionary[key];
        }

        public void PutState<T>(string key, T state) where T : class
        {
            if (stateDictionary.ContainsKey(key))
            {
                if (stateDictionary[key].Equals(state)) return;
                stateDictionary[key] = state;
            }
            else
            {
                stateDictionary.Add(key, state);
            }
            NotifyStateChanged(key);
        }

        // For debugging only, will be deleted after project finish
        public override string ToString()
        {
            return stateDictionary.AsEnumerable()
                                .Select(pair => $"{pair.Key}=>{pair.Value}")
                                .Aggregate((s1, s2) => $"{s1}\n{s2}");
        }

        public void CopyTo(IStateObject target)
        {
            if (stateDictionary.Count == 0) 
            {
                target.Clear();
                return;
            }
            var castTarget = target as StateObject;
            var targetStateDictionary = castTarget.stateDictionary;
            if (comparer.Equals(stateDictionary, targetStateDictionary)) return;
            foreach (var key in stateDictionary.Keys)
            {
                var value = stateDictionary[key];
                if (targetStateDictionary.ContainsKey(key))
                {
                    targetStateDictionary[key] = value;
                } else
                {
                    targetStateDictionary.Add(key, value);
                }
            }
            castTarget.NotifyStateChanged();
        }

        public void Clear()
        {
            if (!stateDictionary.Any()) return;
            stateDictionary.Clear();
            NotifyStateChanged();
        }
    }
}
