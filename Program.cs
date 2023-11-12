using System;
using System.Collections.Generic;
using System.IO;

namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;

        class SweEngGloss
        {
            public string word_swe, word_eng;

            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe;
                this.word_eng = word_eng;
            }

            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0];
                this.word_eng = words[1];
            }
        }


        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");

            bool exit = false;

            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];


                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    exit = true;
                }

                else if (command == "load")
                {
                    LoadDictionary(argument, defaultFile);
                }

                else if (command == "list")
                {
                    ListDictionary();
                }

                else if (command == "new")
                {
                    AddNewWord(argument);
                }

                if (command == "delete")
                {
                    DeleteWord(argument);
                }

                else TranslateWord(argument, command);
            }
            while (!exit);
        }
        static void LoadDictionary(string[] argument, string defaultFile)
        {
            string file = (argument.Length == 2) ? argument[1] : defaultFile;

            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    dictionary = new List<SweEngGloss>();
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }

                    Console.WriteLine($"Dictionary loaded from file: {file}");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {file}");

                if (file.ToLower() == "..\\..\\..\\dict\\sweeng.lis")
                {
                    Console.WriteLine("Loading default file failed. Try loading 'computing.lis' or enter a valid file path.");
                }
                else
                {
                    Console.WriteLine("Please enter a valid file path or type 'load computing.lis' to try again with the default file.");
                }

                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "load computing.lis")
                {
                    LoadDictionary(new string[] { "load", "..\\..\\..\\dict\\computing.lis" }, defaultFile);
                }
                else if (userInput.ToLower().StartsWith("load "))
                {
                    // Extract the file path from the user input and try loading the specified file.
                    string newFilePath = userInput.Substring(5);
                    LoadDictionary(new string[] { "load", newFilePath }, defaultFile);
                }
                else if (userInput.ToLower() != "quit")
                {
                    // FIXME: Handle the exception appropriately for other scenarios.
                    Console.WriteLine("Invalid input. Type 'quit' to exit or 'load' to try again with a different file.");
                }
            }
        }
        static void ListDictionary()
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
            }
        }
        static void AddNewWord(string[] argument)
        {
            if (argument.Length == 3)
            {
                dictionary.Add(new SweEngGloss(argument[1], argument[2]));
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                string swedish = Console.ReadLine();
                Console.Write("Write word in English: ");
                string english = Console.ReadLine();
                dictionary.Add(new SweEngGloss(swedish, english));
            }
        }
        static void DeleteWord(string[] argument)
        {
            string swedish, english;

            if (argument.Length != 3 && argument.Length != 1)
            {
                Console.WriteLine("Invalid number of arguments for delete command.");
                // FIXME: Handle the error appropriately, e.g., provide instructions to the user.
                Console.WriteLine("Usage: delete [Swedish] [English]");
                return;
            }

            if (argument.Length == 3)
            {
                swedish = argument[1];
                english = argument[2];
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                swedish = Console.ReadLine();
                Console.Write("Write word in English: ");
                english = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Unexpected error in delete command.");
                return;
            }

            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == swedish && gloss.word_eng == english)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                dictionary.RemoveAt(index);
            }
        }
        static void TranslateWord(string[] argument, string command)
        {
             if (command == "translate")
            {
                    if (argument.Length == 1)
                {
                    Console.WriteLine("Write word to be translated: ");
                    string reply = Console.ReadLine();
                    foreach (SweEngGloss gloss in dictionary)
                    {
                        if (gloss.word_swe == reply)
                            Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                        if (gloss.word_eng == reply)
                            Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                    }
                }
            }
        }
    }
}