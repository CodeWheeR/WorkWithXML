using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Selialization
{
	class XmlDocWork
	{
		public void MyMain()
		{
			XmlDocument xml = new XmlDocument();
			xml.Load("users.xml");

			XmlElement xRoot = xml.DocumentElement;
			ReadRoot(xRoot);

			//Неудобно
			XmlNode newNode = xml.CreateNode(XmlNodeType.Element, "User", xml.NamespaceURI);

			XmlAttribute attr = xml.CreateAttribute("Name");
			attr.Value = "Paul";
			newNode.Attributes.Append(attr);

			attr = xml.CreateAttribute("Age");
			attr.Value = "28";
			newNode.Attributes.Append(attr);

			//Свои методы
			XmlNode newNode2 = xml.CreateNewNode("Company", 
				xml.CreateNewAttribute("Name", "Hookeye"));

			newNode2.AppendChild(xml.CreateNewNode("Founder", "Hawk"));
			newNode.AppendChild(newNode2);				
			xRoot.AppendChild(newNode);

			//Теперь удобно
			xRoot.AppendNewChild(
				xml.CreateNewNode("User", 
					xml.CreateNewAttribute("Name", "Shawn"),
					xml.CreateNewAttribute("Age", 12)
				)
				.AppendNewChild(
					xml.CreateNewNode("Company", 
						xml.CreateNewAttribute("Name", "Foogas"), 
						xml.CreateNewAttribute("City", "Muhosransk")
					)
					.AppendNewChild(xml.CreateNewNode("Founder", "Шиллинг"))
					.AppendNewChild(xml.CreateNewNode("Founder", "Перкер"))
				)
			);

			xml.Save("Users2.xml");

			XmlDocument xDoc2 = new XmlDocument();
			xDoc2.AppendNewChild(
				xDoc2.CreateNewNode("User", 
					xDoc2.CreateNewAttribute("Name", "Аркадий"),
					xDoc2.CreateNewAttribute("Age", 24)
				)
				.AppendNewChild(
					xDoc2.CreateNewNode("Company", 
						xDoc2.CreateNewAttribute("Name", "Palata")
					)
					.AppendNewChild(
						xDoc2.CreateNewNode("Founder", "Billyusha")
					)						
				)
			);
			xDoc2.Save("Company.xml");
		}
		
		void ReadRoot(XmlElement root)
		{
			Trace.WriteLine("");

			foreach (XmlNode node in root)
			{
				ReadNode(node);
				Trace.WriteLine("");
			}
		}

		void ReadNode(XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Element)
			{
				Trace.WriteLine(node.Name);
				GetAttrs(node);

				foreach (XmlNode child in node)
				{
					ReadNode(child);
				}

				if (node.Value != null)
				{
					Trace.WriteLine(node.Value);
					Trace.WriteLine("");
				}
			}
			if (node.NodeType == XmlNodeType.Text)
				Trace.WriteLine("\tValue: " + node.Value);
			
		}

		void GetAttrs(XmlNode node)
		{
			if (node.Attributes != null)
			foreach (XmlAttribute attr in node.Attributes)
			{
				if (attr != null)
					Trace.WriteLine("\t" + " " + attr.Name + ": " + attr.Value);
				
			}
		}
	}
}
