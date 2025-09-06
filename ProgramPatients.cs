using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

class Program
{
    static void Main()
    {
        Dictionary<string, string> words = new Dictionary<string, string>();

        Console.WriteLine("Напишите нужное слово чтобы узнать значение: ");
        words.Add("Монитор", "устройство для отображения информации.");
        words.Add("Клавиатура", "устройство для ввода текста.");
        words.Add("Мышь", "устройство для управления курсором.");

        foreach (var key in words.Keys)
        {
            Console.WriteLine(key);
        }

        PrintMeaning(words);
    }

    static void PrintMeaning(Dictionary<string, string> words)
    {
        string userInput = Console.ReadLine();
        bool isGetWord = IsGetWordInvasion(words, userInput);

        if (isGetWord)
        {
            Console.WriteLine($"Значение слова ({userInput}): {words[userInput]}");
        }
        else
        {
            Console.WriteLine("Нету такого слова в списке");
        }
    }

    static bool IsGetWordInvasion(Dictionary<string, string> words, string userInput)
    {
        foreach (var key in words.Keys)
        {
            if (userInput == key)
            {
                return true;
            }
        }

        return false;
    }
}
