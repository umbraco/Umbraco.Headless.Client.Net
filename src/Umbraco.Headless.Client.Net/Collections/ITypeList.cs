using System;
using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Collections
{
    public interface ITypeList<in TBaseType> : IEnumerable<Type>
    {
        void Add<TImplementation>() where TImplementation : TBaseType;
        void Add(Type type);
        void Remove<TImplementation>() where TImplementation : TBaseType;
        void Remove(Type type);
        void Clear();
    }
}
