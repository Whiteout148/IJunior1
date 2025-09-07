using System;
using System.Collections.Generic;
using System.Collections;

class Program
{
    static void Main()
    {
        Dictionary<string, string> words = new Dictionary<string, string>();

        Console.WriteLine("Напишите нужное слово чтобы узнать значение: ");
        AddWord(words ,"Монитор", "устройство для отображения информации.");
        AddWord(words ,"Клавиатура", "устройство для ввода текста.");
        AddWord(words ,"Мышь", "устройство для управления курсором.");

        foreach (var key in words.Keys)
        {
            Console.WriteLine(key);
        }

        PrintMeaning(words);
    }

    static void AddWord(Dictionary<string, string> words, string key, string value)
    {
        words.Add(key, value);
    }

    static void PrintMeaning(Dictionary<string, string> words)
    {
        string userInput = Console.ReadLine();

        if (words.ContainsKey(userInput))
        {
            Console.WriteLine($"Значение слова ({userInput}): {words[userInput]}");
        }
        else
        {
            Console.WriteLine("Нету такого слова в списке");
        }
    }
}
