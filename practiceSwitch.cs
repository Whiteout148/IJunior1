using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        int minValue = 25;
        int maxValue = 625;

        int randomNumber = random.Next(minValue, maxValue + 1);
        int degreeNumber = 4;
        int degree = 2;
        int result;
        
        for(int i = 0; i < degreeNumber; i++)
        {
            degreeNumber++;
            result = degree *= degree;

            Console.WriteLine(result);

            if(degree > randomNumber)
            {
                Console.WriteLine($"Рандомное число: {randomNumber} степень: {degree}");
                break;
            }
        }
    }
}