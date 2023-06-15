using Bunny.Net.TechTest.Models;

namespace Bunny.Net.TechTest.Core
{
	public interface IDockerService
	{
		Task<string> Create(string image, PortExposureBinding ports);

		Task Start(string containerId);

		Task Stop(string containerId);

		Task Delete(string containerId);
	}
}