using System;
using Umbraco.Headless.Client.Net.Collections;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Collections
{
    public class TypeListFixture
    {
        [Fact]
        public void AddOfT_WhenCalled_AddsTypeToCollection()
        {
            var list = new TypeList<ITest>();

            list.Add<Test>();

            Assert.Contains(typeof(Test), list);
        }

        [Fact]
        public void Add_WhenCalled_AddsTypeToCollection()
        {
            var list = new TypeList<ITest>();

            list.Add(typeof(Test));

            Assert.Contains(typeof(Test), list);
        }

        [Fact]
        public void Add_WhenCalledWithWrongType_ThrowsException()
        {
            var list = new TypeList<ITest>();

            Assert.Throws<ArgumentException>(() => list.Add(typeof(Test2)));
        }

        [Fact]
        public void Add_WhenCalledWithTypeAlreadyExistingInCollection_DoesOnlyItOnce()
        {
            var list = new TypeList<ITest>();

            list.Add<Test>();
            list.Add<Test>();

            Assert.Single(list);
        }

        [Fact]
        public void RemoveOfT_WhenCalled_RemovesTypeFromCollection()
        {
            var list = new TypeList<ITest>();

            list.Add<Test>();
            list.Add<Test3>();
            list.Remove<Test3>();

            Assert.DoesNotContain(typeof(Test3), list);
            Assert.Contains(typeof(Test), list);
        }

        [Fact]
        public void Remove_WhenCalled_RemovesTypeFromCollection()
        {
            var list = new TypeList<ITest>();

            list.Add<Test>();
            list.Add<Test3>();
            list.Remove(typeof(Test));

            Assert.DoesNotContain(typeof(Test), list);
            Assert.Contains(typeof(Test3), list);
        }

        [Fact]
        public void Clear_WhenCalled_ClearsTheCollection()
        {
            var list = new TypeList<ITest>();

            list.Add<Test>();
            list.Add<Test3>();

            list.Clear();

            Assert.Empty(list);
        }


        public interface ITest
        {
        }

        public class Test : ITest
        {
        }

        public class Test3 : ITest
        {
        }

        public class Test2
        {
        }
    }
}
