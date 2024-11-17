using System.Collections;
using System.Collections.Generic;

namespace SInterpreter.ParsingNodes
{
    public class VariablesManager : IDictionary<string, object>
    {
        private readonly IDictionary<string, object> Variables = new Dictionary<string, object>();
        public int Count => Variables.Count;

        public bool IsReadOnly => false;

        public ICollection<string> Keys => Variables.Keys;
        public ICollection<object> Values => Variables.Values;
        public void Add(string key, object value) => Variables[key] = value;
        

        public bool ContainsKey(string key) => Variables.ContainsKey(key);

        public bool TryGetValue(string key, out object value) => Variables.TryGetValue(key, out value!);

        public bool Remove(string key) => Variables.Remove(key);

        public object this[string key]
        {
            get => Variables[key];
            set => Variables[key] = value;
        }

        public void Add(KeyValuePair<string, object> item) => Variables.Add(item.Key, item.Value);

        public bool Contains(KeyValuePair<string, object> item) => Variables.Contains(item);

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => Variables.CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<string, object> item) => Variables.Remove(item.Key);

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => Variables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Variables.GetEnumerator();

        public void Clear() => Variables.Clear();
    }
}
