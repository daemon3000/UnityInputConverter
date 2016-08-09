using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace UnityInputConverter
{
	internal class InputSaverXML
	{
		private string _filename;
		private Stream _outputStream;
		private StringBuilder _output;
		
		public InputSaverXML(string filename)
		{
			if(filename == null)
				throw new ArgumentNullException("filename");
			
			_filename = filename;
			_outputStream = null;
			_output = null;
		}

		public InputSaverXML(Stream stream)
		{
			if(stream == null)
				throw new ArgumentNullException("stream");
			
			_filename = null;
			_output = null;
			_outputStream = stream;
		}
		
		public InputSaverXML(StringBuilder output)
		{
			if(output == null)
				throw new ArgumentNullException("output");
			
			_filename = null;
			_outputStream = null;
			_output = output;
		}
		
		public void Save(List<InputConfiguration> inputConfigurations)
		{
			XmlWriterSettings xmlSettings = new XmlWriterSettings();
			xmlSettings.Encoding = System.Text.Encoding.UTF8;
			xmlSettings.Indent = true;
			
			using(XmlWriter writer = CreateXmlWriter(xmlSettings))
			{
				writer.WriteStartDocument(true);
				writer.WriteStartElement("Input");
				writer.WriteAttributeString("playerOneDefault", "");
                writer.WriteAttributeString("playerTwoDefault", "");
                writer.WriteAttributeString("playerThreeDefault", "");
                writer.WriteAttributeString("playerFourDefault", "");
                foreach (InputConfiguration inputConfig in inputConfigurations)
				{
					WriteInputConfiguration(inputConfig, writer);
				}
				
				writer.WriteEndElement();
				writer.WriteEndDocument();
			}
		}
		
		private XmlWriter CreateXmlWriter(XmlWriterSettings settings)
		{
			if(_filename != null)
			{
		        return XmlWriter.Create(_filename, settings);
			}
			else if(_outputStream != null)
			{
				return XmlWriter.Create(_outputStream, settings);
			}
			else if(_output != null)
			{
				return XmlWriter.Create(_output, settings);
			}
			
			return null;
		}
		
		private void WriteInputConfiguration(InputConfiguration inputConfig, XmlWriter writer)
		{
			writer.WriteStartElement("InputConfiguration");
			writer.WriteAttributeString("name", inputConfig.name);
			foreach(AxisConfiguration axisConfig in inputConfig.axes)
			{
				WriteAxisConfiguration(axisConfig, writer);
			}
			
			writer.WriteEndElement();
		}
		
		private void WriteAxisConfiguration(AxisConfiguration axisConfig, XmlWriter writer)
		{
			writer.WriteStartElement("AxisConfiguration");
			writer.WriteAttributeString("name", axisConfig.name);
			writer.WriteElementString("description", axisConfig.description);
			writer.WriteElementString("positive", axisConfig.positive.ToString());
			writer.WriteElementString("altPositive", axisConfig.altPositive.ToString());
			writer.WriteElementString("negative", axisConfig.negative.ToString());
			writer.WriteElementString("altNegative", axisConfig.altNegative.ToString());
			writer.WriteElementString("deadZone", axisConfig.deadZone.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("gravity", axisConfig.gravity.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("sensitivity", axisConfig.sensitivity.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("snap", axisConfig.snap.ToString().ToLower());
			writer.WriteElementString("invert", axisConfig.invert.ToString().ToLower());
			writer.WriteElementString("type", axisConfig.type.ToString());
			writer.WriteElementString("axis", axisConfig.axis.ToString());
			writer.WriteElementString("joystick", axisConfig.joystick.ToString());
			
			writer.WriteEndElement();
		}
	}
}
