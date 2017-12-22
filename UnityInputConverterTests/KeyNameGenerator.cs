using UnityEngine;
using System.IO;
using System.Text;


namespace UnityInputConverter.Tests
{
	public class KeyNameGenerator
	{
		private static StringBuilder m_builder = new StringBuilder();

		public static void GenerateKeyNames(string template, string filename)
		{
			using(var writer = File.CreateText(filename))
			{
				writer.Write(template, GenerateStringToKeyMap(), GenerateKeyToStringMap());
			}
		}

		private static string GenerateStringToKeyMap()
		{
			m_builder.Length = 0;
			
			for(int key = (int)KeyCode.A; key <= (int)KeyCode.Z; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", ((KeyCode)key).ToString().ToLower(), ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Alpha0; key <= (int)KeyCode.Alpha9; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", key - (int)KeyCode.Alpha0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Keypad0; key <= (int)KeyCode.Keypad9; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ \"[{0}]\", KeyCode.{1} }},\n", key - (int)KeyCode.Keypad0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Mouse0; key <= (int)KeyCode.Mouse6; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ \"mouse {0}\", KeyCode.{1} }},\n", key - (int)KeyCode.Mouse0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.F1; key <= (int)KeyCode.F15; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ \"f{0}\", KeyCode.{1} }},\n", (key - (int)KeyCode.F1) + 1, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.JoystickButton0; key <= (int)KeyCode.JoystickButton19; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ \"joystick button {0}\", KeyCode.{1} }},\n", key - (int)KeyCode.JoystickButton0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Joystick1Button0; key <= (int)KeyCode.Joystick8Button19; key++)
			{
				int joystick = (key - (int)KeyCode.JoystickButton0) / 20;
				int button = (key - (int)KeyCode.JoystickButton0) % 20;
				m_builder.AppendFormat("\t\t\t{{ \"joystick {0} button {1}\", KeyCode.{2} }},\n", joystick, button, ((KeyCode)key).ToString());
			}

			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "up", KeyCode.UpArrow);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "down", KeyCode.DownArrow);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left", KeyCode.LeftArrow);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right", KeyCode.RightArrow);

			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left shift", KeyCode.LeftShift);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right shift", KeyCode.RightShift);

			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left ctrl", KeyCode.LeftControl);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right ctrl", KeyCode.RightControl);

			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left alt", KeyCode.LeftAlt);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right alt", KeyCode.RightAlt);

			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "right cmd", KeyCode.LeftCommand);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "left cmd", KeyCode.RightCommand);

			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "backspace", KeyCode.Backspace);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "tab", KeyCode.Tab);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "return", KeyCode.Return);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "escape", KeyCode.Escape);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "space", KeyCode.Space);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "delete", KeyCode.Delete);
			//m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "enter", KeyCode.Return);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "insert", KeyCode.Insert);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "home", KeyCode.Home);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "end", KeyCode.End);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "page up", KeyCode.PageUp);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "page down", KeyCode.PageDown);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "pause", KeyCode.Pause);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", ".", KeyCode.Period);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "/", KeyCode.Slash);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "=", KeyCode.Equals);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "[", KeyCode.LeftBracket);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "]", KeyCode.RightBracket);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "`", KeyCode.BackQuote);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", ";", KeyCode.Semicolon);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", "'", KeyCode.Quote);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},\n", ",", KeyCode.Comma);
			m_builder.AppendFormat("\t\t\t{{ \"{0}\", KeyCode.{1} }},", "-", KeyCode.Minus);
			
			return m_builder.ToString();
		}

		private static string GenerateKeyToStringMap()
		{
			m_builder.Length = 0;

			for(int key = (int)KeyCode.A; key <= (int)KeyCode.Z; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\"  }},\n", ((KeyCode)key).ToString().ToLower(), ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Alpha0; key <= (int)KeyCode.Alpha9; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", key - (int)KeyCode.Alpha0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Keypad0; key <= (int)KeyCode.Keypad9; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"[{0}]\" }},\n", key - (int)KeyCode.Keypad0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Mouse0; key <= (int)KeyCode.Mouse6; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"mouse {0}\" }},\n", key - (int)KeyCode.Mouse0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.F1; key <= (int)KeyCode.F15; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"f{0}\" }},\n", (key - (int)KeyCode.F1) + 1, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.JoystickButton0; key <= (int)KeyCode.JoystickButton19; key++)
			{
				m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"joystick button {0}\" }},\n", key - (int)KeyCode.JoystickButton0, ((KeyCode)key).ToString());
			}

			for(int key = (int)KeyCode.Joystick1Button0; key <= (int)KeyCode.Joystick8Button19; key++)
			{
				int joystick = (key - (int)KeyCode.JoystickButton0) / 20;
				int button = (key - (int)KeyCode.JoystickButton0) % 20;
				m_builder.AppendFormat("\t\t\t{{ KeyCode.{2}, \"joystick {0} button {1}\" }},\n", joystick, button, ((KeyCode)key).ToString());
			}

			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "up", KeyCode.UpArrow);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "down", KeyCode.DownArrow);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "left", KeyCode.LeftArrow);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "right", KeyCode.RightArrow);

			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "left shift", KeyCode.LeftShift);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "right shift", KeyCode.RightShift);

			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "left ctrl", KeyCode.LeftControl);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "right ctrl", KeyCode.RightControl);

			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "left alt", KeyCode.LeftAlt);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "right alt", KeyCode.RightAlt);

			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "right cmd", KeyCode.LeftCommand);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "left cmd", KeyCode.RightCommand);

			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "backspace", KeyCode.Backspace);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "tab", KeyCode.Tab);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "return", KeyCode.Return);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "escape", KeyCode.Escape);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "space", KeyCode.Space);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "delete", KeyCode.Delete);
			//m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "enter", KeyCode.Return);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "insert", KeyCode.Insert);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "home", KeyCode.Home);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "end", KeyCode.End);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "page up", KeyCode.PageUp);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "page down", KeyCode.PageDown);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "pause", KeyCode.Pause);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", ".", KeyCode.Period);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "/", KeyCode.Slash);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "=", KeyCode.Equals);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "[", KeyCode.LeftBracket);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "]", KeyCode.RightBracket);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "`", KeyCode.BackQuote);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", ";", KeyCode.Semicolon);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", "'", KeyCode.Quote);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},\n", ",", KeyCode.Comma);
			m_builder.AppendFormat("\t\t\t{{ KeyCode.{1}, \"{0}\" }},", "-", KeyCode.Minus);

			return m_builder.ToString();
		}
	}
}
