using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		[HttpGet(template: "all")]
		public async Task<IActionResult> Get()
		{
			return Ok(true);
		}
	}
}
