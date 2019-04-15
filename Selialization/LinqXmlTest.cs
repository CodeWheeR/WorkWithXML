using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Selialization
{
	class LinqXmlTest
	{
		public void MyMain()
		{
			//Для начала создадим новый xml
			//Старенькой, кривой структурой
			XDocument doc = new XDocument();
			var users = new XElement("Users");
			var user = new XElement("User");
			user.Add(new XAttribute("name", "Вася"));
			user.Add(new XAttribute("age", "16"));
			var company = new XElement("Company");
			company.Add(new XAttribute("Name", "Macrosoft"));
			company.Add(new XElement("Founder", "Bill Hades"));

			user.Add(company);
			users.Add(user);

			//А теперь, воспользуемся истинным LINQ
			users.Add(new XElement("User",
				new XAttribute("Name", "Сережа"),
				new XAttribute("Age", 16),
				new XElement("Company",
					new XAttribute("Name", "Gamazon"),
					new XElement("Founder", "Jeff Pesos")
					)
				)
			);

			users.Add(
				new XElement("User",
					new XAttribute("Name", "Jenya"),
					new XAttribute("Age", 19),
					new XElement("Company",
						new XAttribute("Name", "CHSU Opornyi Vys"),
						new XElement("Founder", "Afanasyev")
					)
				)
			);

			users.Add(
				new XElement("User",
					new XAttribute("Name", "Filya"),
					new XAttribute("Age", 25),
					new XAttribute("LevelOfLazyness", 1547913547841325),
					new XElement("Glasses", true)
					
				)
			);

			doc.Add(users);
			doc.Save("users3.xml");

			//Выборка с помощью LINQ

			XDocument doc2 = XDocument.Load("Users2.xml");
			XElement root = doc2.Root;

			foreach (var i in doc2.Root.Elements("User"))
			{
				Trace.WriteLine($"{i.Attribute("Name").Value} ({i.Attribute("Age").Value} лет) - Компания {i.Element("Company").Attribute("Name").Value}");
			}
			Trace.WriteLine("");

			var sw = new Stopwatch();
			sw.Start();
			string s = doc2.Root
				.XPathSelectElements("//Company")
				.Select(y => y.Attribute("Name").Value)				
				.Distinct()
				.Aggregate((x, y) => $"{x}, {y}");

			Trace.WriteLine(s);

			Trace.WriteLine(sw.ElapsedTicks);
			sw.Stop();

			var groups = doc2.Element("ArrayOfUser").Elements("User").SelectMany(x => x.Attributes())
				.GroupBy(x => x.Name);
			foreach (var i in groups)
			{
				var groupValues = i.Select(y => y.Value).Distinct().Aggregate((x1, y) => $"{x1}, {y}");
				Trace.WriteLine(i.Key + "s: " + groupValues);
			}

			doc2.Elements().ElementAt(0).Add(new XElement("phone",
			new XAttribute("name", "Nokia Lumia 930"),
			new XElement("company", "Nokia"),
			new XElement("price", "19500")));
			doc2.Save("users2edited.xml");
			doc2.Elements().ElementAt(0).Element("phone").Remove();
			doc2.Save("users2edited2.xml");
		}	
	}
}
