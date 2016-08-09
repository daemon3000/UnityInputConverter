using UnityInputConverter;

namespace UnityInputConverter.Tests
{
	public class Program
	{
		public static void Main(string[] args)
		{
			InputConverter converter = new InputConverter();
			string sourceFile = "InputManager.asset";
			string destinationFile = "InputManager.xml";
			string keyConverterFile = "KeyCodeConverter.cs";
			
			converter.ConvertUnityInputManager(sourceFile, destinationFile);
			KeyNameGenerator.GenerateKeyNames(keyConverterFile);
		}
	}
}
