namespace Bunny.Net.TechTest.Core
{
	public interface IDockerService
	{
		Task Create(string image);

		Task Start(string containerId);

		Task Stop(string containerId);

		Task Delete(string containerId);
	}
}