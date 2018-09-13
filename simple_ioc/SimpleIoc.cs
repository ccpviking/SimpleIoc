using System;
using System.Collections.Generic;
using System.Threading;

namespace simple_ioc
{
	public interface ISimpleIoc
	{
		int Count { get; }
		void Clear<T>() where T : class;
		bool IsRegistered(Type registeredType);
		void Register<I, T>(T value, LifecycleScope scope) where I : class where T : class;
		void Register<I, T>(Func<T> factory, LifecycleScope scope) where I : class where T : class;
		T Resolve<T>() where T : class;
	}

	// Inversion of control injection engine uses with factories to register
	//    interface types with objects that implements that interface.
	// This class is the key to enable unit testsing.
	public class SimpleIoc : ISimpleIoc
	{
		// The dictionary that stores the factory entries
		private readonly IDictionary<Type, IocEntry> _factoryTable;

		public SimpleIoc()
		{
			_factoryTable = new Dictionary<Type, IocEntry>();
		}

		public int Count => _factoryTable.Count;

		public void Clear<T>() where T : class => _factoryTable.Remove(typeof(T));

		public bool IsRegistered(Type registeredType) => _factoryTable.ContainsKey(registeredType);

		public void Register<I, T>(T value, LifecycleScope scope) where I : class where T : class => this.Register<I, T>(() => value, scope);

		public void Register<I, T>(Func<T> factory, LifecycleScope scope) where I : class where T : class
		{
			if (factory == null)
				throw new ArgumentNullException("factory");

			var type = typeof(I);
			if (IsRegistered(type))
				throw new InvalidOperationException($"{type} object type is already registered.");

			_factoryTable[type] = new IocEntry(factory, scope);
		}

		// Gets the object registered to the type 'T'
		public T Resolve<T>() where T : class
		{
			var type = typeof(T);
			IocEntry entry;
			if (!_factoryTable.TryGetValue(type, out entry))
			{
				throw new ApplicationException($"{type} is not registered.");
				// Personally I would rather just return a null than throw an exception
				// because a requesting a non-existing element should be a null and not an error.
				//return default(T);
			}
			var value = entry.Scope == LifecycleScope.Transient ? entry.GetValue<T>() : (T)entry.GetValue();
			return value;
		}
	}
}
