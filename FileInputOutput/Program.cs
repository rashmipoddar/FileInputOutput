using System;
using System.IO;

namespace FileInputOutput
{
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
                while(!reader.EndOfStream)
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
            
        }
    }
}
