using UnityEngine;

namespace UnityInputConverter
{
	internal class InputBinding
	{
		public const float AXIS_NEUTRAL = 0.0f;
		public const float AXIS_POSITIVE = 1.0f;
		public const float AXIS_NEGATIVE = -1.0f;
		public const int MAX_MOUSE_AXES = 3;
		public const int MAX_JOYSTICK_AXES = 28;
		public const int MAX_JOYSTICKS = 11;

		public InputType Type;
		public KeyCode Positive;
		public KeyCode Negative;
		public float DeadZone;
		public float Gravity;
		public float Sensitivity;
		public bool Snap;
		public bool Invert;
		public int Axis;
		public int Joystick;

		public InputBinding()
		{
			Type = InputType.Button;
			Positive = KeyCode.None;
			Negative = KeyCode.None;
			DeadZone = 0.0f;
			Gravity = 0.0f;
			Sensitivity = 0.0f;
			Snap = false;
			Invert = false;
			Axis = 0;
			Joystick = 0;
		}

		public static InputBinding Duplicate(InputBinding source)
		{
			InputBinding duplicate = new InputBinding
			{
				Positive = source.Positive,
				Negative = source.Negative,
				DeadZone = source.DeadZone,
				Gravity = source.Gravity,
				Sensitivity = source.Sensitivity,
				Snap = source.Snap,
				Invert = source.Invert,
				Type = source.Type,
				Axis = source.Axis,
				Joystick = source.Joystick
			};

			return duplicate;
		}
	}
}
