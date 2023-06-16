namespace Bunny.Net.TechTest.Core
{
	public class ContainerRegistry : IContainerRegistry
	{
		public Dictionary<string, string> Registry { get; set; } = new Dictionary<string, string>();
	}
}
