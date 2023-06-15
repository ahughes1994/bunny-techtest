using Docker.DotNet;
using Docker.DotNet.Models;
using System.Runtime.InteropServices;

namespace Bunny.Net.TechTest.Core
{
	public class DockerService : IDockerService
	{
		private readonly DockerClient dockerClient;

        public DockerService()
        {
			dockerClient = new DockerClientConfiguration(GetDockerApiUri()).CreateClient();
		}

        public async Task Create(string image)
		{
			throw new NotImplementedException();
		}

		public async Task Start(string containerId)
		{
			throw new NotImplementedException();
		}

		public async Task Stop(string containerId)
		{
			throw new NotImplementedException();
		}

		public async Task Delete(string containerId)
		{
			throw new NotImplementedException();
		}

		private Uri GetDockerApiUri()
		{
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
