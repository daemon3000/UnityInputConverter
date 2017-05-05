using UnityEngine;
using System.IO;
using System.Text;


namespace UnityInputConverter.Tests
{
	public class KeyNameGenerator
	{
		public static void GenerateKeyNames(string filename)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("using UnityEngine;");
			builder.AppendLine("using System.Collections.Generic;");
			builder.AppendLine();
			builder.AppendLine("namespace UnityInputConverter");
			builder.AppendLine("{");
			builder.AppendLine("\tinternal static class KeyCodeConverter");
			builder.AppendLine("\t{");
			builder.AppendLine("\t\tpublic static Dictionary<string, KeyCode> Map = new Dictionary<string, KeyCode>()");
			builder.AppendLine("\t\t{");

			for(int key = (int)KeyCode.A; key <= (int)KeyCode.Z; key++)
			{
				builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", ((KeyCode)key).ToString().ToLower(), ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Alpha0; key <= (int)KeyCode.Alpha9; key++)
			{
				builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", key - (int)KeyCode.Alpha0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Keypad0; key <= (int)KeyCode.Keypad9; key++)
			{
				builder.AppendFormat("\t\t\t{{ \"[{0}]\", KeyCode.{1} }},\n", key - (int)KeyCode.Keypad0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Mouse0; key <= (int)KeyCode.Mouse6; key++)
			{
				builder.AppendFormat("\t\t\t{{ \"mouse {0}\", KeyCode.{1} }},\n", key - (int)KeyCode.Mouse0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.F1; key <= (int)KeyCode.F15; key++)
			{
				builder.AppendFormat("\t\t\t{{ \"f{0}\", KeyCode.{1} }},\n", (key - (int)KeyCode.F1) + 1, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.JoystickButton0; key <= (int)KeyCode.JoystickButton19; key++)
			{
				builder.AppendFormat("\t\t\t{{ \"joystick button {0}\", KeyCode.{1} }},\n", key - (int)KeyCode.JoystickButton0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Joystick1Button0; key <= (int)KeyCode.Joystick8Button19; key++)
			{
				int joystick = (key - (int)KeyCode.JoystickButton0) / 20;
				int button = (key - (int)KeyCode.JoystickButton0) % 20;
				builder.AppendFormat("\t\t\t{{ \"joystick {0} button {1}\", KeyCode.{2} }},\n", joystick, button, ((KeyCode)key).ToString());
			}

			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "up", KeyCode.UpArrow);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "down", KeyCode.DownArrow);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left", KeyCode.LeftArrow);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right", KeyCode.RightArrow);

			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left shift", KeyCode.LeftShift);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right shift", KeyCode.RightShift);

			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left ctrl", KeyCode.LeftControl);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right ctrl", KeyCode.RightControl);

			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left alt", KeyCode.LeftAlt);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right alt", KeyCode.RightAlt);

			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right cmd", KeyCode.LeftCommand);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left cmd", KeyCode.RightCommand);

			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "backspace", KeyCode.Backspace);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "tab", KeyCode.Tab);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "return", KeyCode.Return);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "escape", KeyCode.Escape);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "space", KeyCode.Space);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "delete", KeyCode.Delete);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "enter", KeyCode.Return);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "insert", KeyCode.Insert);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "home", KeyCode.Home);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "end", KeyCode.End);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "page up", KeyCode.PageUp);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "page down", KeyCode.PageDown);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "pause", KeyCode.Pause);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", ".", KeyCode.Period);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "/", KeyCode.Slash);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "=", KeyCode.Equals);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "[", KeyCode.LeftBracket);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "]", KeyCode.RightBracket);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "`", KeyCode.BackQuote);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", ";", KeyCode.Semicolon);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "'", KeyCode.Quote);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", ",", KeyCode.Comma);
			builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "-", KeyCode.Minus);

			builder.AppendLine("\t\t};");
			builder.AppendLine("\t}");
			builder.AppendLine("}");

			using(var writer = File.CreateText(filename))
			{
				writer.Write(builder.ToString());
			}
		}
	}
}
