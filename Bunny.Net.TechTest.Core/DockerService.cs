using Bunny.Net.TechTest.Models;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.InteropServices;

namespace Bunny.Net.TechTest.Core
{
	public class DockerService : IDockerService
	{
		private readonly DockerClient dockerClient;
		private readonly ILogger<DockerService> logger;

		public DockerService(ILogger<DockerService> logger)
		{
			this.logger = logger;

			dockerClient = new DockerClientConfiguration(GetDockerApiUri()).CreateClient();
		}

		public async Task<string> Create(string image, PortExposureBinding ports)
		{
			await PullImage(image);
			logger.LogInformation($"Pulled image: {image}");

			var portBindings = ports.PortBindings.ToDictionary(
				k => $"{k.Key}/tcp", 
				v => new List<PortBinding> { new PortBinding { HostIP = "0.0.0.0", HostPort = v.Value} } as IList<PortBinding>);

			var response = await dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
			{
				Image = image,
				ExposedPorts = ports.ExposedPorts.ToDictionary(k => k, v => default(EmptyStruct)),
				HostConfig = new HostConfig
				{
					PortBindings = portBindings,
					PublishAllPorts = true,
				}
			});
			logger.LogInformation($"Created container with ID: {response.ID}");

			return response.ID;
		}

		public async Task Start(string containerId)
		{
			await dockerClient.Containers.StartContainerAsync(containerId, new ContainerStartParameters());
			logger.LogInformation($"Starting container with ID: {containerId}");
		}

		public async Task Stop(string containerId)
		{
			await dockerClient.Containers.StopContainerAsync(containerId, new ContainerStopParameters());
			logger.LogInformation($"Stopping container with ID: {containerId}");
		}

		public async Task Delete(string containerId)
		{
			await dockerClient.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters());
			logger.LogInformation($"Deleting container with ID: {containerId}");
		}

		private Uri GetDockerApiUri(string? dockerAddress = null)
		{
			if (dockerAddress != null)
			{
				return new Uri(dockerAddress);
			}

			var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

			if (isWindows)
			{
				return new Uri("npipe://./pipe/docker_engine");
			}

			var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

			if (isLinux)
			{
				return new Uri("unix:/var/run/docker.sock");
			}

			throw new Exception("Was unable to determine what OS this is running on, does not appear to be Windows or Linux!?");
		}

		private async Task PullImage(string image, string tag = "latest")
		{
			await dockerClient.Images
				.CreateImageAsync(new ImagesCreateParameters
				{
					FromImage = image,
					Tag = tag
				},
				new AuthConfig(),
				new Progress<JSONMessage>());
		}
	}
}
