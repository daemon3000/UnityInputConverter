using System;
using System.Collections.Generic;

namespace UnityInputConverter
{
	internal class ControlScheme
	{
		public string Name;
		public string UniqueID;
		public List<InputAction> Actions;

		public ControlScheme() :
			this("New Controls Scheme")
		{
		}

		public ControlScheme(string name)
		{
			Name = name;
			UniqueID = Guid.NewGuid().ToString("N");
			Actions = new List<InputAction>();
		}
	}
}