using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

            return result;
        }

        static void AddStrokeToList(List<string> collection, string[] stroke)
        {
            for (int i = 0; i < stroke.Length; i++)
            {
                if (collection.Contains(stroke[i]))
                {
                    continue;
                }
                else
                {
                    collection.Add(stroke[i]);
                }
            }
        }
    }
}
