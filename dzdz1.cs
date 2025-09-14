using System;
using System.Collections;
using System.Collections.Generic;

namespace dzdzdz
{
    internal class dzdz1
    {
        static void Main(string[] args)
        {
            string[] firstStroke = { "1", "2", "1" };
            string[] secondStroke = { "3", "2" };

            List<string> result = UniteStrokesToList(firstStroke, secondStroke);

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i]);
            }
        }

        static List<string> UniteStrokesToList(string[] firstStroke, string[] secondStroke)
        {
            List<string> result = new List<string>();

            AddStrokeToList(result, firstStroke);
            AddStrokeToList(result, secondStroke);

            DeleteSameElements(result);

            return result;
        }

        static void AddStrokeToList(List<string> collection, string[] stroke)
        {
            for (int i = 0; i < stroke.Length; i++)
            {
                collection.Add(stroke[i]);
            }
        }

        static void DeleteSameElements(List<string> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                for (int j = i + 1; j < collection.Count; j++)
                {
                    if (collection[j] == collection[i])
                    {
                        collection.RemoveAt(j);
                    }
                }
            }
        }
    }
}
