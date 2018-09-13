using System;
using simple_ioc;
using Xunit;

namespace simple_ioc_tests
{
	public class SimpleIocTests
	{
		private readonly ISimpleIoc _ioc;
		private ISimpleIoc Ioc => _ioc;

		public SimpleIocTests()
		{
			_ioc = new SimpleIoc();
		}

		[Fact]
		public void TestClearingIocOfAnObject()
		{
			_ioc.Register<ITestObject, TestObject>(new TestObject(), LifecycleScope.Singleton);
			Assert.True(_ioc.IsRegistered(typeof(ITestObject)));
			_ioc.Clear<ITestObject>();
			Assert.Null(_ioc.Resolve<ITestObject>());
		}

		[Fact]
		public void TestInstanceSingletonScope()
		{
			_ioc.Register<ITestObject, TestObject>(new TestObject(), LifecycleScope.Singleton);
			var obj1 = _ioc.Resolve<ITestObject>();
			var obj2 = Ioc.Resolve<ITestObject>();
			Assert.Same(obj1, obj2);
			_ioc.Clear<ITestObject>();
		}

		[Fact]
		public void TestFactorySingltonScope()
		{
			_ioc.Register<ITestObject, TestObject>(() => new TestObject(), LifecycleScope.Singleton);
			var obj1 = _ioc.Resolve<ITestObject>();
			var obj2 = _ioc.Resolve<ITestObject>();
			Assert.Same(obj1, obj2);
			_ioc.Clear<ITestObject>();
		}

		[Fact]
		public void TestFactoryTransientScope()
		{
			_ioc.Register<ITestObject, TestObject>(() => new TestObject(), LifecycleScope.Transient);
			var obj1 = _ioc.Resolve<ITestObject>();
			var obj2 = _ioc.Resolve<ITestObject>();
			Assert.NotSame(obj1, obj2);
			_ioc.Clear<ITestObject>();
		}

		[Fact]
		public void OnlyCanRegisterOneObjectType()
		{
			try
			{
				_ioc.Register<ITestObject, TestObject>(() => new TestObject(), LifecycleScope.Transient);
				_ioc.Register<ITestObject, TestObject>(() => new TestObject(), LifecycleScope.Transient);
			}
			catch (System.Exception ex)
			{
				Assert.IsType<InvalidOperationException>(ex);
				Assert.EndsWith("object type is already registered.", ex.Message);
			}
			finally
			{
				_ioc.Clear<ITestObject>();
			}
		}
	}
}
