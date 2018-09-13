using System;

namespace simple_ioc_tests
{
	public interface ITestObject
	{
		void SomeMethod();
	}

	public class TestObject : ITestObject
	{
		public void SomeMethod()
		{
			Console.WriteLine("TestObject.SomeMethod() called.");
		}
	}
}
