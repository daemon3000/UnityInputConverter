using UnityEngine;

namespace UnityInputConverter
{
	public sealed class AxisConfiguration
	{
		public const int MaxMouseAxes = 3;
		public const int MaxJoystickAxes = 10;
		public const int MaxJoysticks = 4;
		
		public string name;
		public string description;
		public KeyCode positive;
		public KeyCode negative;
		public KeyCode altPositive;
		public KeyCode altNegative;
		public float deadZone;
		public float gravity;
		public float sensitivity;
		public bool snap;
		public bool invert;
		public InputType type;
		public int axis;
		public int joystick;
		
		public AxisConfiguration() :
			this("New Axis") { }
		
		public AxisConfiguration(string name)
		{
			this.name = name;
			description = string.Empty;
			positive = KeyCode.None;
			altPositive = KeyCode.None;
			negative = KeyCode.None;
			altNegative = KeyCode.None;
			type = InputType.Button;
			gravity = 1.0f;
			sensitivity = 1.0f;
		}
	}
}