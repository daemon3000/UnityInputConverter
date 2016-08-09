using UnityInputConverter;

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
			
			converter.ConvertUnityInputManager(convertSourceFile, convertDestinationFile);
			converter.GenerateDefaultUnityInputManager(defaultInputManagerDestinationFile);
			KeyNameGenerator.GenerateKeyNames(keyConverterFile);
		}
	}
}
