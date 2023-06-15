using Bunny.Net.TechTest.Core;
using Bunny.Net.TechTest.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Bunny.Net.TechTest.Api.Controllers
{
	[Route("api/[controller]/[action]")]
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

		[HttpPost]
		public async Task<IActionResult> Create([FromQuery] string image, [FromBody] PortExposureBinding bindings)
		{
			logger.LogInformation($"/api/containers/create - image:{image} - bindings:{JsonConvert.SerializeObject(bindings)}");

			string containerId = string.Empty;

			try
			{
				containerId = await dockerService.Create(image, bindings);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				return Problem(ex.Message);
			}

			return Ok(containerId);
		}

		[HttpGet]
		public async Task<IActionResult> Start([FromQuery] string containerId)
		{
			logger.LogInformation($"/api/containers/start - containerId:{containerId}");

			try
			{
				await dockerService.Start(containerId);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				return Problem(ex.Message);
			}

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Stop([FromQuery] string containerId)
		{
			logger.LogInformation($"/api/containers/stop - containerId:{containerId}");

			try
			{
				await dockerService.Stop(containerId);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				return Problem(ex.Message);
			}

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Delete([FromQuery] string containerId)
		{
			logger.LogInformation($"/api/containers/delete - containerId:{containerId}");

			try
			{
				await dockerService.Delete(containerId);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				return Problem(ex.Message);
			}

			return Ok();
		}
    }
}
