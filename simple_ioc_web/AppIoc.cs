using simple_ioc;

namespace simple_ioc_web
{
	public static class AppIoc
	{
		public static ISimpleIoc Ioc { get; } = new SimpleIoc();

		public static void RegisterDependencies()
		{
			AppIoc.Ioc.Register<IApiInfo, ApiInfo>(new ApiInfo("v.10"), LifecycleScope.Singleton);
		}

	}
}