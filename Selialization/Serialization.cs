using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;

namespace Selialization
{
	public class User
	{
		[XmlAttribute("Name")]
		public string Name { get; set; }
		[XmlAttribute("Age")]
		public int Age { get; set; }

		public Company Company { get; set; }

	}

	public class Company
	{
		[XmlAttribute("Name")]
		public string Name { get; set; }
		//Аттрибут не создаем, т.к. в будущем может появиться класс Founder
		public string Founder { get; set; }
	}

	public class Serialization
	{
		public void MyMain()
		{
			var oneUser = new User
			{
				Name = "Вася",
				Age = 15,
				Company = new Company
				{
					Name = "Macrosoft",
					Founder = "Bill Hades"
				}
			};

			var s = new List<User>()
			{
				oneUser,
				new User {
				Name = "Сережа",
				Age = 17,
				Company = new Company
					{
						Name = "Gamazon",
						Founder = "Jeff Pesos"
					}
				}
			};

			var xmlSerializer = new XmlSerializer(oneUser.GetType());
			using (var fs = new FileStream("user.xml", FileMode.Create))
			{
				xmlSerializer.Serialize(fs, oneUser);
				Debug.Print("Объект сериализован");
			}

			using (var fs = new FileStream("user.xml", FileMode.Open))
			{
				List<User> deserList = xmlSerializer.Deserialize(fs) as List<User>;
				Debug.Print("Объект сериализован");
			}

			Serialize("users.xml", s);
			var list = (Deserialize("users.xml", typeof(List<User>)) as List<User>);
			Debug.Print(list?.Count.ToString());
		}
		
		void Serialize(string s, object o)
		{
			try
			{
				var xmlSerializer = new XmlSerializer(o.GetType());
				using (var fs = new FileStream(s, FileMode.Create))
				{
					xmlSerializer.Serialize(fs, o);
					Debug.Print("Объект сериализован");
				}
			}
			catch
			{
				MessageBox.Show("Ошибка во время сериализации");
			}
		}

		object Deserialize(string s, Type o)
		{
			object ans = null;
			try
			{
				var xmlSerializer = new XmlSerializer(o);
				using (var fs = new FileStream(s, FileMode.Open))
				{
					ans = xmlSerializer.Deserialize(fs);
					Debug.Print("Объект десериализован");
				}
			}
			catch(Exception e)
			{
				MessageBox.Show("Ошибка во время сериализации: " + e.ToString());
			}
			return ans;
		}
	}
}
