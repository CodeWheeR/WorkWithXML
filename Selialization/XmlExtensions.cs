using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Selialization
{
	static class XmlExtensions
	{
		public static XmlAttribute CreateNewAttribute(this XmlDocument doc, string name, object value)
		{
			var attr = doc.CreateAttribute(name);
			attr.Value = value.ToString();
			return attr;
		}

		public static XmlNode CreateNewNode(this XmlDocument doc, string name, string value = "", params XmlAttribute[] attrs)
		{
			XmlNode node = doc.CreateNode(XmlNodeType.Element, name, doc.NamespaceURI);
			if (!String.IsNullOrEmpty(value))
				//не node.Value = value;!!
				node.AppendChild(doc.CreateTextNode(value));
			foreach (var i in attrs)
				node.Attributes.Append(i);
			return node;
		}

		public static XmlNode CreateNewNode(this XmlDocument doc, string name, params XmlAttribute[] attrs)
		{
			XmlNode node = doc.CreateNode(XmlNodeType.Element, name, doc.NamespaceURI);
			foreach (var i in attrs)
				node.Attributes.Append(i);
			return node;
		}

		public static XmlNode AppendNodes(this XmlNode root, params XmlNode[] nodes)
		{
			foreach (var i in nodes)
				root.AppendChild(i);
			return root;
		}

		public static XmlNode AppendNewChild(this XmlNode root, XmlNode node)
		{
			root.AppendChild(node);
			return root;
		}
	}
}
