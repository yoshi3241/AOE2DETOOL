namespace AOE2DETOOL.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// ディクショナリー拡張:keyがなければデフォルト値を返す
        /// </summary>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            => dict.TryGetValue(key, out TValue result) ? result : default(TValue);

        /// <summary>
        /// 指定したKeyがDictionaryにあれば、何もせず、ないなら指定したValueと共に追加し追加を実行したかを返します。
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : new()
            => dict.TryAdd(key, _ => new TValue());

        public static bool TryAddDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            => dict.TryAdd(key, default(TValue));

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue addValue)
        {
            bool canAdd = !dict.ContainsKey(key);

            if (canAdd)
                dict.Add(key, addValue);

            return canAdd;
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> addValueFactory)
        {
            bool canAdd = !dict.ContainsKey(key);

            if (canAdd)
                dict.Add(key, addValueFactory(key));

            return canAdd;
        }


        /// <summary>
        /// 指定したKeyがDictionaryにあれば、そのValueを返し、ないなら値を追加した上で返します。
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : new()
        {
            dict.TryAddNew(key);
            return dict[key];
        }

        public static TValue GetOrAddDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            dict.TryAddDefault(key);
            return dict[key];
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue addValue)
        {
            dict.TryAdd(key, addValue);
            return dict[key];
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> valueFactory)
        {
            dict.TryAdd(key, valueFactory);
            return dict[key];
        }

        /// <summary>
        /// 1つのKeyValuePairを追加します。
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="addPair"></param>
        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> source, KeyValuePair<TKey, TValue> addPair) => source.Add(addPair.Key, addPair.Value);
    }
}
