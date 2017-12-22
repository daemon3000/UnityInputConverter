using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

namespace UnityInputConverter
{
	internal class InputSaverXML
	{
		private const int VERSION = 2;

		private string m_filename;
		private Stream m_outputStream;
		private StringBuilder m_output;

		public InputSaverXML(string filename)
		{
			m_filename = filename ?? throw new ArgumentNullException("filename");
			m_outputStream = null;
			m_output = null;
		}

		public InputSaverXML(Stream stream)
		{
			m_outputStream = stream ?? throw new ArgumentNullException("stream");
			m_filename = null;
			m_output = null;
		}

		public InputSaverXML(StringBuilder output)
		{
			m_output = output ?? throw new ArgumentNullException("output");
			m_filename = null;
			m_outputStream = null;
		}

		private XmlWriter CreateXmlWriter(XmlWriterSettings settings)
		{
			if(m_filename != null)
			{
				return XmlWriter.Create(m_filename, settings);
			}
			else if(m_outputStream != null)
			{
				return XmlWriter.Create(m_outputStream, settings);
			}
			else if(m_output != null)
			{
				return XmlWriter.Create(m_output, settings);
			}

			return null;
		}

		public void Save(List<ControlScheme> controlSchemes)
		{
			XmlWriterSettings xmlSettings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				Indent = true
			};

			using(XmlWriter writer = CreateXmlWriter(xmlSettings))
			{
				writer.WriteStartDocument(true);
				writer.WriteStartElement("Input");
				writer.WriteAttributeString("version", VERSION.ToString());
				writer.WriteElementString("PlayerOneScheme", controlSchemes.Count > 0 ? controlSchemes[0].UniqueID : "");
				writer.WriteElementString("PlayerTwoScheme", "");
				writer.WriteElementString("PlayerThreeScheme", "");
				writer.WriteElementString("PlayerFourScheme", "");
				foreach(ControlScheme scheme in controlSchemes)
				{
					WriteControlScheme(scheme, writer);
				}

				writer.WriteEndElement();
				writer.WriteEndDocument();
			}
		}

		private void WriteControlScheme(ControlScheme scheme, XmlWriter writer)
		{
			writer.WriteStartElement("ControlScheme");
			writer.WriteAttributeString("name", scheme.Name);
			writer.WriteAttributeString("id", scheme.UniqueID);
			foreach(var action in scheme.Actions)
			{
				WriteInputAction(action, writer);
			}

			writer.WriteEndElement();
		}

		private void WriteInputAction(InputAction action, XmlWriter writer)
		{
			writer.WriteStartElement("Action");
			writer.WriteAttributeString("name", action.Name);
			writer.WriteElementString("Description", action.Description);
			foreach(var binding in action.Bindings)
			{
				WriteInputBinding(binding, writer);
			}

			writer.WriteEndElement();
		}

		private void WriteInputBinding(InputBinding binding, XmlWriter writer)
		{
			writer.WriteStartElement("Binding");
			writer.WriteElementString("Positive", binding.Positive.ToString());
			writer.WriteElementString("Negative", binding.Negative.ToString());
			writer.WriteElementString("DeadZone", binding.DeadZone.ToString());
			writer.WriteElementString("Gravity", binding.Gravity.ToString());
			writer.WriteElementString("Sensitivity", binding.Sensitivity.ToString());
			writer.WriteElementString("Snap", binding.Snap.ToString().ToLower());
			writer.WriteElementString("Invert", binding.Invert.ToString().ToLower());
			writer.WriteElementString("Type", binding.Type.ToString());
			writer.WriteElementString("Axis", binding.Axis.ToString());
			writer.WriteElementString("Joystick", binding.Joystick.ToString());

			writer.WriteEndElement();
		}
	}
}
