using System.Collections.Generic;

namespace UnityInputConverter
{
	internal class InputAction
	{
		public string Name;
		public string Description;
		public List<InputBinding> Bindings;

		public InputAction()
		{
			Name = "New Action";
			Description = "";
			Bindings = new List<InputBinding>();
		}

		public InputBinding CreateNewBinding()
		{
			InputBinding binding = new InputBinding();
			Bindings.Add(binding);

			return binding;
		}

		public InputBinding CreateNewBinding(InputBinding source)
		{
			InputBinding binding = InputBinding.Duplicate(source);
			Bindings.Add(binding);

			return binding;
		}
	}
}