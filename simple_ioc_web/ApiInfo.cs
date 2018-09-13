namespace simple_ioc_web
{
	public interface IApiInfo
	{
		string GetInfo();
	}

	public class ApiInfo : IApiInfo
	{
		private string _version;
		public ApiInfo(string version)
		{
			_version = version;
		}
		public string GetInfo()
		{
			var info = new
			{
				name = "simple_ioc_web_api",
				version = _version,
			};
			return Newtonsoft.Json.JsonConvert.SerializeObject(info);
		}
	}
}