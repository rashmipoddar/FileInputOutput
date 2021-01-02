using System;
using System.IO;
using System.Xml;

namespace FileInputOutput
{
    struct User
    {
        public string FirstName;
        public string LastName;
        public string Profession;
        public Address Address;
    };

    struct Address
    {
        public string Street;
        public string City;
        public string State;
        public string Zip;
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"/Users/rashmipoddar/Documents/Test.txt";

            // Returns an array of string where each element represents a single line in the file
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("--------");
            // Returns a single string for the contents of a file
            string text = File.ReadAllText(filePath);
            Console.WriteLine(text);
            Console.WriteLine("--------");

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string textFromFile = reader.ReadLine();
                    Console.WriteLine(textFromFile);
                }
            }

            string filePathForWriting = @"/Users/rashmipoddar/Documents/Hello.txt";
            string greeting = "Hello World!";

            // Creates the file if it does not exist. Overwrites the content
            File.WriteAllText(filePathForWriting, greeting);

            // Creates the file if it does not exist. Appends the content without overwriting the existing contents
            File.AppendAllText(filePathForWriting, "\nAdded a new sentence to the file");

            string[] words = { "\nAdded yet another line", "Hello", "Happy New Year 2021" };
            File.AppendAllLines(filePathForWriting, words);

            // Appends to the current content
            using (var streamWriter = new StreamWriter(filePathForWriting, true))
            {
                streamWriter.WriteLine("Appended using a stream writer");
            }

            // Overwrites the current content
            using (var streamWriter = new StreamWriter(@"/Users/rashmipoddar/Documents/TestWrite.txt"))
            {
                streamWriter.WriteLine("Added using a stream writer");
            }

            // Overwrites the current content
            using (var streamWriter = new StreamWriter(@"/Users/rashmipoddar/Documents/TestWrite.txt"))
            {
                streamWriter.WriteLine("Overwritten contents with the new content");
            }

            // Create an XML document and read the xml file
            XmlDocument doc = new XmlDocument();
            doc.Load(@"/Users/rashmipoddar/Documents/Users.txt");

            // Display the document element
            Console.WriteLine(doc.DocumentElement.OuterXml);

            // get all the user nodes
            XmlNodeList userNodesList = doc.SelectNodes("//User");

            // create an array to hold all the users
            User[] users = new User[userNodesList.Count];

            // parse each user into a user object
            int userIndex = 0;
            foreach (XmlElement element in userNodesList)
            {
                // parse the user fields
                User user = new User();
                user.FirstName = element.GetAttribute("FirstName");
                if (element.HasAttribute("LastName"))
                {
                    user.LastName = element.GetAttribute("LastName");
                }
                user.Profession = element.GetAttribute("Profession");

                // parse the address fields
                var addressElement = (XmlElement)element.FirstChild;

                Address address = new Address();
                address.Street = addressElement.GetAttribute("Street");
                address.City = addressElement.GetAttribute("City");
                address.State = addressElement.GetAttribute("State");
                address.Zip = addressElement.GetAttribute("Zip");

                // assign the address to the current user
                user.Address = address;

                // add the current user to the array
                users[userIndex] = user;
                userIndex++;
            }

            foreach (var user in users)
            {
                Console.WriteLine(user.FirstName);
                Console.WriteLine(user.LastName);
                Console.WriteLine(user.Profession);
                Console.WriteLine(user.Address.Street);
                Console.WriteLine(user.Address.City);
                Console.WriteLine(user.Address.State);
                Console.WriteLine(user.Address.Zip);
            }

            users[0].LastName = "Teacher";
            users[1].Address.City = "Batland";

            // Create an XmlWriterSettings object with the correct options
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            //settings.OmitXmlDeclaration = true;

            // Create the XmlWriter object and write some content
            using (XmlWriter writer = XmlWriter.Create(@"/Users/rashmipoddar/Documents/UserDetails.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Users");

                foreach (User user in users)
                {
                    writer.WriteStartElement("User");

                    writer.WriteElementString("FirstName", user.FirstName);
                    if (!String.IsNullOrEmpty(user.LastName))
                    {
                        writer.WriteElementString("LastName", user.LastName);
                    }
                    
                    writer.WriteElementString("Profession", user.Profession);

                    writer.WriteStartElement("Address");

                    writer.WriteElementString("Street", user.Address.Street);
                    writer.WriteElementString("City", user.Address.City);
                    if (!String.IsNullOrEmpty(user.Address.State))
                    {
                        writer.WriteElementString("State", user.Address.State);
                    }
                    if (!String.IsNullOrEmpty(user.Address.Zip))
                    {
                        writer.WriteElementString("State", user.Address.Zip);
                    }

                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }
    }
    
}
