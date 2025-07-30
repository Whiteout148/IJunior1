using System;
using System.Text;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            const string CommandForExit = "Выход";
            const string CommandForDollarUsd = "USD";
            const string CommandForRuble = "RUB";
            const string CommandForLira = "TL";
            string userInput;

            bool isWork = true;

            float rubleBalance;
            float dollarBalance;
            float liraBalance;

            float desiredMoney;

            float dollarRubleCourse = 82f;
            float liraDollarCouse = 40f;
            float rubleLiraCouse = 2f;

            Console.WriteLine($"Добро пожаловать в нашу программу по конвертации валют");
            Console.WriteLine($"\nВведите ваш баланс из всех доступных валют:");
            Console.WriteLine($"\nСколько у вас {CommandForDollarUsd}?");
            dollarBalance = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nСколько у вас {CommandForRuble}?");
            liraBalance = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nСколько у вас {CommandForLira}?");
            rubleBalance = Convert.ToInt32(Console.ReadLine());

            while (isWork)
            {
                Console.WriteLine($"\nБаланс {CommandForDollarUsd}: {dollarBalance}");
                Console.WriteLine($"\nБаланс {CommandForRuble}: {rubleBalance}");
                Console.WriteLine($"\nБаланс {CommandForLira}: {liraBalance}");

                Console.WriteLine("\nКакую валюту хотите конвертировать?");
                Console.WriteLine($"\nкоманда {CommandForDollarUsd} на Пиндос доллар");
                Console.WriteLine($"\nкоманда {CommandForRuble} на Российский рубль");
                Console.WriteLine($"\nкоманда {CommandForLira} на Турецкий лир");
                Console.WriteLine($"\nкоманда {CommandForExit} на выход из программы");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandForDollarUsd:
                        Console.WriteLine($"\nСколько {CommandForDollarUsd} хотите конвертировать?");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if (desiredMoney > dollarBalance)
                        {
                            Console.WriteLine("Желаемое количество денег превышает баланс");
                        }
                        else
                        {
                            Console.WriteLine($"В какую валюту хотите конвертировать? {CommandForRuble}/{CommandForLira}");
                            userInput = Console.ReadLine();

                            if (userInput == CommandForRuble)
                            {
                                dollarBalance -= desiredMoney;
                                desiredMoney *= dollarRubleCourse;
                                rubleBalance += desiredMoney;

                                Console.WriteLine($"\nНа баланс {CommandForRuble} зачисленно {desiredMoney}");
                            }
                            else if (userInput == CommandForLira)
                            {
                                dollarBalance -= desiredMoney;
                                desiredMoney *= liraDollarCouse;
                                liraBalance += desiredMoney;

                                Console.WriteLine($"\nНа баланс {CommandForLira} зачисленно {desiredMoney}");
                            }
                        }
                        break;

                    case CommandForRuble:
                        Console.WriteLine($"\nСколько {CommandForRuble} хотите конвертировать?");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if (desiredMoney > rubleBalance)
                        {
                            Console.WriteLine("Желаемое количество денег превышает баланс");
                        }
                        else
                        {
                            Console.WriteLine($"В какую валюту хотите конвертировать? {CommandForDollarUsd}/{CommandForLira}");
                            userInput = Console.ReadLine();

                            if(userInput == CommandForDollarUsd)
                            {
                                rubleBalance -= desiredMoney;
                                desiredMoney /= dollarRubleCourse;
                                dollarBalance += desiredMoney;

                                Console.WriteLine($"\nНа баланс {CommandForDollarUsd} зачисленно {desiredMoney}");
                            }
                            else if(userInput == CommandForLira)
                            {
                                rubleBalance -= desiredMoney;
                                desiredMoney /= rubleLiraCouse;
                                liraBalance += desiredMoney;

                                Console.WriteLine($"\nНа баланс {CommandForLira} зачисленно {desiredMoney}");
                            }
                        }
                        break;

                    case CommandForLira:
                        Console.WriteLine($"\nСколько {CommandForLira} хотите конвертировать?");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if(desiredMoney > liraBalance)
                        {
                            Console.WriteLine("Желаемое количество денег превышает баланс");
                        }
                        else
                        {
                            Console.WriteLine($"В какую валюту хотите конвертировать? {CommandForDollarUsd}/{CommandForRuble}");
                            userInput = Console.ReadLine();

                            if(userInput == CommandForDollarUsd)
                            {
                                liraBalance -= desiredMoney;
                                desiredMoney /= liraDollarCouse;
                                dollarBalance += desiredMoney;

                                Console.WriteLine($"\nНа баланс {CommandForDollarUsd} зачисленно {desiredMoney}");
                            }
                            else if(userInput == CommandForRuble)
                            {
                                liraBalance -= desiredMoney;
                                desiredMoney *= rubleLiraCouse;
                                rubleLiraCouse += desiredMoney;

                                Console.WriteLine($"\nНа баланс {CommandForRuble} зачисленно {desiredMoney}");
                            }
                        }
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
