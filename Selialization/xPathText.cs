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

			var nodes = xml.SelectNodes("//Company");
			foreach (XmlNode i in nodes)
			{
				var nd = i.SelectSingleNode("..");
				Trace.Write(nd?.InnerXml);
			}

			var sw = new Stopwatch();
			sw.Start();
			
			foreach(XmlNode i in xml.SelectNodes("//User/Company"))
			{
				Trace.Write(i.Attributes[0].Value + ", ");
			}
			Trace.WriteLine("");
			Trace.WriteLine(sw.ElapsedTicks);
			sw.Stop();
		}
	}
}
