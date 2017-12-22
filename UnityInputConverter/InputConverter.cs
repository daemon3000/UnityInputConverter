using UnityEngine;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using UnityInputConverter.YamlDotNet.Serialization;

namespace UnityInputConverter
{
	public class InputConverter
	{
		private const string INPUT_MANAGER_FILE_TEMPLATE = @"%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!13 &1
{0}";

		private const int SERIALIZED_VERSION = 3;
		private const int OBJECT_HIDE_FLAGS = 0;
		private const int BUTTON_TYPE = 0;
		private const int MOUSE_AXIS_TYPE = 1;
		private const int JOYSTICK_AXIS_TYPE = 2;

		public void ConvertUnityInputManager(string sourceFile, string destinationFile)
		{
			IDictionary<object, object> deserializedData = null;
			ControlScheme scheme = new ControlScheme("Unity-Imported");

			using(StreamReader reader = File.OpenText(sourceFile))
			{
				reader.ReadLine();
				reader.ReadLine();
				reader.ReadLine();

				Deserializer deserializer = new Deserializer();
				deserializedData = deserializer.Deserialize<IDictionary<object, object>>(reader);
			}

			if(deserializedData == null || deserializedData.Count == 0)
				throw new System.FormatException();

			IDictionary<object, object> inputManager = (IDictionary<object, object>)deserializedData["InputManager"];
			IList<object> axes = (IList<object>)inputManager["m_Axes"];

			foreach(var item in axes)
			{
				IDictionary<object, object> axis = (IDictionary<object, object>)item;
				scheme.Actions.Add(ConvertUnityInputAxis(axis));
			}

			InputSaverXML inputSaver = new InputSaverXML(destinationFile);
			inputSaver.Save(new List<ControlScheme> { scheme });
		}

		private InputAction ConvertUnityInputAxis(IDictionary<object, object> axisData)
		{
			int axisType = ParseInt(axisData["type"].ToString());
			int axisID = ParseInt(axisData["axis"].ToString());
			int joystickID = ParseInt(axisData["joyNum"].ToString(), 1) - 1;

			InputAction action = new InputAction
			{
				Name = axisData["m_Name"].ToString()
			};

			InputBinding binding = action.CreateNewBinding();
			binding.Gravity = ParseFloat(axisData["gravity"].ToString());
			binding.DeadZone = ParseFloat(axisData["dead"].ToString());
			binding.Sensitivity = ParseFloat(axisData["sensitivity"].ToString());
			binding.Snap = ParseInt(axisData["snap"].ToString()) != 0;
			binding.Invert = ParseInt(axisData["invert"].ToString()) != 0;
			binding.Positive = ConvertUnityKeyCode(axisData["positiveButton"]);
			binding.Negative = ConvertUnityKeyCode(axisData["negativeButton"]);
			binding.Type = ParseInputType(axisType);
			binding.Axis = Clamp(axisID, 0, InputBinding.MAX_JOYSTICK_AXES - 1);
			binding.Joystick = Clamp(joystickID, 0, InputBinding.MAX_JOYSTICKS);

			KeyCode altPositive = ConvertUnityKeyCode(axisData["altPositiveButton"]);
			KeyCode altNegative = ConvertUnityKeyCode(axisData["altNegativeButton"]);
			if(binding.Type == InputType.Button)
			{
				if(binding.Positive != KeyCode.None && binding.Negative != KeyCode.None)
				{
					binding.Type = InputType.DigitalAxis;
				}

				if(altPositive != KeyCode.None && altNegative != KeyCode.None)
				{
					var altBinding = action.CreateNewBinding(binding);
					altBinding.Positive = altPositive;
					altBinding.Negative = altNegative;
					altBinding.Type = InputType.DigitalAxis;
				}
				else if(altPositive != KeyCode.None)
				{
					var altBinding = action.CreateNewBinding(binding);
					altBinding.Positive = altPositive;
					altBinding.Type = InputType.Button;
				}
			}

			return action;
		}

		private KeyCode ConvertUnityKeyCode(object value)
		{
			if(value != null)
			{
				KeyCode keyCode = KeyCode.None;
				if(KeyCodeConverter.Map.TryGetValue(value.ToString(), out keyCode))
					return keyCode;
			}

			return KeyCode.None;
		}

		public void GenerateDefaultUnityInputManager(string destinationFile)
		{
			Dictionary<string, object> data = new Dictionary<string, object>();
			Dictionary<string, object> inputManager = new Dictionary<string, object>();
			List<Dictionary<string, object>> axes = new List<Dictionary<string, object>>();

			data.Add("InputManager", inputManager);
			inputManager.Add("m_ObjectHideFlags", OBJECT_HIDE_FLAGS);
			inputManager.Add("m_Axes", axes);

			for(int i = 0; i < InputBinding.MAX_MOUSE_AXES; i++)
			{
				axes.Add(GenerateUnityMouseAxis(i));
			}

			for(int j = 1; j <= InputBinding.MAX_JOYSTICKS; j++)
			{
				for(int a = 0; a < InputBinding.MAX_JOYSTICK_AXES; a++)
				{
					axes.Add(GenerateUnityJoystickAxis(j, a));
				}
			}

			using(var writer = File.CreateText(destinationFile))
			{
				Serializer serializer = new Serializer();
				StringWriter stringWriter = new StringWriter();

				serializer.Serialize(stringWriter, data);
				writer.Write(INPUT_MANAGER_FILE_TEMPLATE, stringWriter.ToString());
			}
		}

		public Dictionary<string, object> GenerateUnityMouseAxis(int axis)
		{
			return new Dictionary<string, object>
			{
				{ "serializedVersion", SERIALIZED_VERSION },
				{ "m_Name", string.Format("mouse_axis_{0}", axis) },
				{ "descriptiveName", null },
				{ "descriptiveNegativeName", null },
				{ "negativeButton", null },
				{ "positiveButton", null },
				{ "altNegativeButton", null },
				{ "altPositiveButton", null },
				{ "gravity", 0 },
				{ "dead", 0 },
				{ "sensitivity", 1 },
				{ "snap", 0 },
				{ "invert", 0 },
				{ "type", MOUSE_AXIS_TYPE },
				{ "axis", axis },
				{ "joyNum", 0 }
			};
		}

		public Dictionary<string, object> GenerateUnityJoystickAxis(int joystick, int axis)
		{
			return new Dictionary<string, object>
			{
				{ "serializedVersion", SERIALIZED_VERSION },
				{ "m_Name", string.Format("joy_{0}_axis_{1}", joystick - 1, axis) },
				{ "descriptiveName", null },
				{ "descriptiveNegativeName", null },
				{ "negativeButton", null },
				{ "positiveButton", null },
				{ "altNegativeButton", null },
				{ "altPositiveButton", null },
				{ "gravity", 0 },
				{ "dead", 0 },
				{ "sensitivity", 1 },
				{ "snap", 0 },
				{ "invert", 0 },
				{ "type", JOYSTICK_AXIS_TYPE },
				{ "axis", axis },
				{ "joyNum", joystick }
			};
		}

		#region [Helper Methods]
		private int Clamp(int value, int min, int max)
		{
			if(value < min)
				return min;
			if(value > max)
				return max;

			return value;
		}

		private float ParseFloat(string str, float defValue = 0.0f)
		{
			float value = defValue;
			if(float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
				return value;

			return defValue;
		}

		private int ParseInt(string str, int defValue = 0)
		{
			int value = defValue;
			if(int.TryParse(str, out value))
				return value;

			return defValue;
		}

		private InputType ParseInputType(int type, InputType defValue = InputType.Button)
		{
			if(type == BUTTON_TYPE)
			{
				return InputType.Button;
			}
			else if(type == MOUSE_AXIS_TYPE)
			{
				return InputType.MouseAxis;
			}
			else if(type == JOYSTICK_AXIS_TYPE)
			{
				return InputType.AnalogAxis;
			}

			return defValue;
		}
		#endregion
	}
}
