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

                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == swedish && gloss.word_eng == english)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
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
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (!exit);
        }
        static void LoadDictionary(string[] argument, string defaultFile)
        {
            string file = (argument.Length == 2) ? argument[1] : defaultFile;

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
    }
}