using System;
using System.Collections.Generic;

namespace simple_ioc
{
	internal class IocEntry
	{
		// Lifecycle for the entry
		public LifecycleScope Scope { get; private set; }

		// Stack to keep track of which items are in use.
		private readonly Stack<object> _stack;

		// Cached value
		private object _singltonValue;

		// Initialize a new instance of IocEntry class
		// factory: the factory
		public IocEntry(object factory, LifecycleScope scope = LifecycleScope.Singleton)
		{
			Scope = scope;
			_stack = new Stack<object>();
			_stack.Push(factory);
		}

		// Returns the registered value for the given type T
		public T GetValue<T>()
		{
			if (_singltonValue != null)
				return (T)_singltonValue;

			Func<T> factory = (Func<T>)_stack.Peek();
			T value = factory();
			if (Scope == LifecycleScope.Singleton)
			{
				_singltonValue = value;
			}
			return value;
		}

		// Returns registered value
		public object GetValue()
		{
			if (_singltonValue != null)
				return _singltonValue;

			var factory = _stack.Peek() as Delegate;
			if (factory == null)
				return null;

			var value = factory.DynamicInvoke(); // todo: add parameters here!
			if (Scope == LifecycleScope.Singleton)
				_singltonValue = value;

			return value;
		}

		public void Push(object factory)
		{
			_singltonValue = null;
			_stack.Push(factory);
		}

		public void Pop()
		{
			_singltonValue = null;
			_stack.Pop();
		}
	}

	public enum LifecycleScope
	{
		Singleton,
		Transient
	}
}