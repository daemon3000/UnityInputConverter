using System.Collections.Generic;

namespace UnityInputConverter
{
	internal class InputConfiguration
	{
		public string name;
		public List<AxisConfiguration> axes;
		
		public InputConfiguration() :
			this("New Configuration") { }
		
		public InputConfiguration(string name)
		{
			axes = new List<AxisConfiguration>();
			this.name = name;
		}
	}
}