using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Selialization
{
	class xPathTest
	{
		public void MyMain()
		{
			XmlDocument xml = new XmlDocument();
			xml.Load("users2.xml");

			var nodes = xml.SelectNodes("//Company[Founder = 'Jeff Pesos']");
			foreach (XmlNode i in nodes)
			{
				var nd = i.SelectSingleNode("..");
				Trace.WriteLine(nd?.Attributes[0].Name + " " + nd?.Attributes[0].Value);
			}

			var sw = new Stopwatch();
			sw.Start();
			
			foreach(XmlNode i in xml.SelectNodes("//Company"))
			{
				//Trace.Write(i.InnerText);
				Trace.Write(i.Attributes[0].Value + ", ");
			}
			Trace.WriteLine("");
			Trace.WriteLine(sw.ElapsedTicks);
			sw.Stop();
			Trace.WriteLine("");
			var query = xml.SelectSingleNode("//GlobalVariables");
			foreach(XmlNode i in query)
			{

				Trace.WriteLine($"{i.Name} = {i.LastChild.Value}");
			}
			
		}
	}
}
