using System;
using System.Collections;
using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Collections
{
    internal class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        private readonly HashSet<Type> _types = new HashSet<Type>();

        public IEnumerator<Type> GetEnumerator() => _types.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add<TImplementation>() where TImplementation : TBaseType => _types.Add(typeof(TImplementation));

        public void Add(Type type)
        {
            if (typeof(TBaseType).IsAssignableFrom(type) == false)
                throw new ArgumentException($"type must inherit from {typeof(TBaseType)}", nameof(type));

            _types.Add(type);
        }

        public void Remove<TImplementation>() where TImplementation : TBaseType =>
            _types.Remove(typeof(TImplementation));

        public void Remove(Type type) => _types.Remove(type);

        public void Clear() => _types.Clear();
    }
}
