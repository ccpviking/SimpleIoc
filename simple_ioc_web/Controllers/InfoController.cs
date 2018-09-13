using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace simple_ioc_web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InfoController : ControllerBase
	{
		private IApiInfo _info;
		public InfoController()
		{
			_info = AppIoc.Ioc.Resolve<IApiInfo>();
		}

		// GET api/info
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { _info.GetInfo() };
		}
	}
}
