using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mkr1
{//Колекція «Компослуги» містить інформацію про нарахування за послуги у такому форматі: прізвище
 //жильця, адреса, дата, вид послуги, сума, нарахована за послугу.Елементи колекції «Оплата» містять
 //інформацію про сплату за послуги у такій формі: прізвище жильця, вид послуги, сплачена сума, дата
 //сплати.
 //a) Знайти середні сплати за задану послугу у поточному місяці усіх жильців, які проживають у
 //заданому місті.
 //b) Вивести назву послуги, за яку загалом нараховано найбільшу суму за останній квартал.
 //c) Зберегти дані однієї з колекцій в xml-файлі.
    internal class Program
    {
        static string file = @"C:\Users\ASUS\Desktop\Service.xml";
        static void Main(string[] args)
        {
            string address = "adress1";
            string type = "type1";
            double a = A(Payment, address, type);
            Console.WriteLine(a);
            string b = B(Service);
            Console.WriteLine(b);
            SaveToFile(Service);
        }
        public static List<Service> Service = new List<Service>() {
        new Service()
        {
            Surname="Ivanova",
            Address ="adress1",
            Date = new DateTime(2023,1,1),
            Type="type1",
            Price=2000,
        },
         new Service()
        {
            Surname="Smith",
            Address ="adress1",
            Date = new DateTime(2023,1,1),
            Type="type3",
            Price=20006,
        },
          new Service()
        {
            Surname="Semenko",
            Address ="adress1",
            Date = new DateTime(2023,1,1),
            Type="type1",
            Price=200,
        },
           new Service()
        {
            Surname="Ivanova1",
            Address ="adress5",
            Date = new DateTime(2023,9,1),
            Type="type5",
            Price=200990,
        },
            new Service()
        {
            Surname="Young",
            Address ="adress89",
            Date = new DateTime(2023,8,9),
            Type="type1",
            Price=5000000,
        },
        };
        public static List<Payment> Payment = new List<Payment>()
        {
         new Payment()
         {
           Surname="Ivanova",
           Type="type1",
           Paid = 2000,
           Paymentdate=new DateTime(2023,10,3),
         },
          new Payment()
         {
           Surname="Smith",
           Type="type3",
           Paid = 20007,
           Paymentdate=new DateTime(2023,10,9),
         },
           new Payment()
         {
           Surname="Semenko",
           Type="type7",
           Paid = 20,
           Paymentdate=new DateTime(2023,10,3),
         },
            new Payment()
         {
           Surname="Ivanova1",
           Type="type5",
           Paid = 2000,
           Paymentdate=new DateTime(2023,8,3),
         },
             new Payment()
         {
           Surname="Young",
           Type="type1",
           Paid = 70,
           Paymentdate=new DateTime(2023,9,3),
         },

        };
        //a) Знайти середні сплати за задану послугу у поточному місяці усіх жильців, які проживають у
        //заданому місті.
        public static double A(List<Payment> Payment, string address, string type)
        {
            int date = DateTime.Now.Month;
            var filtered = Service.Where(f => f.Address == address && f.Type == type).Select(s => s.Surname).Distinct();
            var date1 = Payment.Where(p => p.Paymentdate.Month == date && filtered.Contains(p.Surname)).Average(p => p.Paid);
            return date1;

        }
        //b) Вивести назву послуги, за яку загалом нараховано найбільшу суму за останній квартал.
        public static string B(List<Service> Service)
        {
            var quaeter = DateTime.Now.Month - 3;
            var filtered = Service.Where(s => s.Date.Month >= quaeter).GroupBy(
                s => s.Type).OrderByDescending(s => s.Sum(s1 => s1.Price)).First();
            return filtered.Key;
        }
        //c) Зберегти дані однієї з колекцій в xml-файлі.
        public static void SaveToFile(List<Service> Service)
        {
            using (XmlTextWriter writer = new XmlTextWriter(file,null))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 3;
                writer.WriteStartDocument();
                writer.WriteStartElement("Service");

                foreach (Service service in Service)
                {
                    writer.WriteStartElement("Service");
                    writer.WriteAttributeString("Surname", service.Surname);
                    writer.WriteAttributeString("Address", service.Address);
                    writer.WriteAttributeString("Date", service.Date.ToString());
                    writer.WriteAttributeString("Type", service.Type);
                    writer.WriteAttributeString("Price", service.Price.ToString());


                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }


        }
    }
}
