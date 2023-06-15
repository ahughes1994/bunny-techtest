namespace Bunny.Net.TechTest.Models
{
	public class PortExposureBinding
	{
		public List<string> ExposedPorts { get; set; } = new List<string>();

		public Dictionary<string, string> PortBindings { get; set; } = new Dictionary<string, string>();
	}
}
