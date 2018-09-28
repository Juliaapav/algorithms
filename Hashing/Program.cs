using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Hashing
{
    class Program
    {
        static string[] Hashtable = new string[766];


        static void Search()
        {
            int i = 0;
            int k = 0;
            Console.WriteLine("Введите слово для поиска");
            string word = Console.ReadLine();
            foreach (string value in Program.Hashtable)
            {
                i++;
                if (value == word)
                {
                    i--;
                    k++;
                    Console.WriteLine("Слово " + word + " найдено. Его хеш: " + i);
                }
            }
            if (k == 0)
            {
                Console.WriteLine("Слово " + word + " не найдено:(");
            }
            Search();

        }
        static void Rehash(ref int cmp, int sum, string word, int i, ref int maxHash, ref int collisions)
        {
            cmp++;
            int newKey = 0;
            newKey = (sum + i) % maxHash;
            if (sum == newKey)
            {
                Console.WriteLine("Таблица заполнена!");

            }
            else
             if (Program.Hashtable[newKey] == null)
            {
                maxHash = Math.Max(newKey, maxHash);
                Program.Hashtable[newKey] = word;
            }
            else
            {
                if (Program.Hashtable[newKey] != word)
                {
                    i++;
                    Rehash(ref cmp, sum, word, i, ref maxHash, ref collisions);
                }
                else
                {
                    cmp--;
                    collisions--;
                }
            }

        }



        static void Main(string[] args)
        {
            string file = @"D:\file.txt";
            string[] Words;
            string word;
            int sum = 0;
            int maxHash = 0;
            int collisions = 0;
            int cmp = 0;
            Words = File.ReadAllText(file).Split(new[] { ' ', '.', ',', '!', '-', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Words.Length; i++)
            {
                NewHash:
                word = Words[i];
                if (word.Length>2)
                { 
                sum = (int)word[word.Length - 1] + (int)word[0] + (int)word[word.Length - 2];


                    if (Program.Hashtable[sum] == null)
                    {
                        maxHash = Math.Max(sum, maxHash);
                        Program.Hashtable[sum] = word;
                    }
                    else
                    {
                        if (Program.Hashtable[sum] == word)
                        {
                            i++;
                            if (i < Words.Length)
                                goto NewHash;
                            else goto Finish;
                        }
                        else
                        {
                            collisions++;
                            Rehash(ref cmp, sum, word, 1, ref maxHash, ref collisions);
                        }
                    }
                }
            }
            Finish:
            Console.WriteLine("Коллизий: {0}", collisions);
            if (collisions > 0)
            {
                Console.WriteLine("В среднем сравнений для поиска свободной ячейки: {0}", cmp/collisions);
            }
            Search();
            Console.Read();
        }
    }
}
