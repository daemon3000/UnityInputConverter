using System;
using System.IO;
using UnityEngine;

namespace UnityInputConverter.Tests
{
	public class Program
	{
		public static void Main(string[] args)
		{
			InputConverter converter = new InputConverter();
			string convertSourceFile = "InputManager.asset";
			string convertDestinationFile = "InputManager.xml";
			string defaultInputManagerDestinationFile = "InputManager_Generated.asset";
			string keyConverterFile = "KeyCodeConverter.cs";
			string keyConverterTemplate = File.ReadAllText("key_code_converter_template.txt");

			converter.ConvertUnityInputManager(convertSourceFile, convertDestinationFile);
			converter.GenerateDefaultUnityInputManager(defaultInputManagerDestinationFile);
			KeyNameGenerator.GenerateKeyNames(keyConverterTemplate, keyConverterFile);

			Console.WriteLine("Your key is: {0}", KeyCodeConverter.KeyToString(KeyCode.Joystick1Button11));
		}
	}
}
