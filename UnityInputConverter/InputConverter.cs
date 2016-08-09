using UnityEngine;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace UnityInputConverter
{
	public class InputConverter
	{
		public void ConvertUnityInputManager(string sourceFile, string destinationFile)
		{
			List<InputConfiguration> inputConfigurations = new List<InputConfiguration>();
			IDictionary<object, object> deserializedData = null;
			InputConfiguration inputConfig = new InputConfiguration("Unity-Imported");

			using(StreamReader reader = File.OpenText(sourceFile))
			{
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
				inputConfig.axes.Add(ConvertUnityInputAxis(axis));
			}

			inputConfigurations.Add(inputConfig);

			InputSaverXML inputSaver = new InputSaverXML(destinationFile);
			inputSaver.Save(inputConfigurations);
		}

		private AxisConfiguration ConvertUnityInputAxis(IDictionary<object, object> axisData)
		{
			AxisConfiguration axisConfig = new AxisConfiguration();

			axisConfig.name = axisData["m_Name"].ToString();
			axisConfig.gravity = ParseFloat(axisData["gravity"].ToString());
			axisConfig.deadZone = ParseFloat(axisData["dead"].ToString());
			axisConfig.sensitivity = ParseFloat(axisData["sensitivity"].ToString());
			axisConfig.snap = ParseInt(axisData["snap"].ToString()) != 0;
			axisConfig.invert = ParseInt(axisData["invert"].ToString()) != 0;

			axisConfig.positive = axisData["positiveButton"] != null ? KeyCodeConverter.Map[axisData["positiveButton"].ToString()] : KeyCode.None;
			axisConfig.altPositive = axisData["altPositiveButton"] != null ? KeyCodeConverter.Map[axisData["altPositiveButton"].ToString()] : KeyCode.None;
			axisConfig.negative = axisData["negativeButton"] != null ? KeyCodeConverter.Map[axisData["negativeButton"].ToString()] : KeyCode.None;
			axisConfig.altNegative = axisData["altNegativeButton"] != null ? KeyCodeConverter.Map[axisData["altNegativeButton"].ToString()] : KeyCode.None;
			
			int axisType = ParseInt(axisData["type"].ToString());
			int axisID = ParseInt(axisData["axis"].ToString());
			int joystickID = ParseInt(axisData["joyNum"].ToString(), 1) - 1;

			axisConfig.type = ParseInputType(axisType);
			if(axisConfig.type == InputType.Button)
			{
				if((axisConfig.positive != KeyCode.None || axisConfig.altPositive != KeyCode.None) &&
					(axisConfig.negative != KeyCode.None || axisConfig.altNegative != KeyCode.None))
				{
					axisConfig.type = InputType.DigitalAxis;
				}
			}

			axisConfig.axis = axisID >= 0 && axisID < AxisConfiguration.MaxJoystickAxes ? axisID : 0;
			axisConfig.joystick = joystickID >= 0 && joystickID < AxisConfiguration.MaxJoysticks ? joystickID : 0;

			return axisConfig;
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
			if(type == 0)
			{
				return InputType.Button;
			}
			else if(type == 1)
			{
				return InputType.MouseAxis;
			}
			else if(type == 2)
			{
				return InputType.AnalogAxis;
			}

			return defValue;
		}
	}
}
