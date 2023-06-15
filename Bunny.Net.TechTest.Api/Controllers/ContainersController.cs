using Bunny.Net.TechTest.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bunny.Net.TechTest.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContainersController : ControllerBase
	{
		private readonly ILogger<ContainersController> logger;
		private readonly IDockerService dockerService;

		public ContainersController(ILogger<ContainersController> logger, IDockerService dockerService)
        {
			this.logger = logger;
			this.dockerService = dockerService;
		}

		public IActionResult Create()
		{
			try
			{

			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return Ok();
		}

		public IActionResult Start()
		{
			try
			{

			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return Ok();
		}

		public IActionResult Stop()
		{
			try
			{

			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return Ok();
		}

		public IActionResult Delete()
		{
			try
			{

			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return Ok();
		}
    }
}
