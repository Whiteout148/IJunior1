using System;
using System.ComponentModel.Design;
using System.Text;
using System.Threading;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            const string UsdName = "USD";
            const string Rubname = "RUB";
            const string TrylName = "Try";

            const string CommandUsdToRub = "1";
            const string CommandUsdToTl = "2";
            const string CommandRubToUsd = "3";
            const string CommandRubToTl = "4";
            const string CommandTlToUsd = "5";
            const string CommandTlToRub = "6";
            const string CommandForExit = "7";

            string userInput;

            bool isWork = true;

            float rubBalance;
            float usdBalance;
            float tlBalance;

            float desiredMoney;

            float usdToRubCourse = 80.31f;
            float usdToTlCourse = 40.53f;

            float rubToUsdCourse = 0.0125f;
            float rubToTlCouse = 0.504f;

            float tryToUsdCourse = 0.0247f;
            float tryToRubCourse = 1.98f;

            Console.WriteLine($"Добро пожаловать в нашу программу по конвертации валют");
            Console.WriteLine($"\nВведите ваш баланс из всех доступных валют:");
            Console.WriteLine($"\nВведите баланс валюты: {UsdName}");
            usdBalance = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nВведите баланс валюты: {Rubname}");
            rubBalance = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nВведите баланс валюты: {TrylName}");
            tlBalance = Convert.ToInt32(Console.ReadLine());

            while (isWork)
            {
                Console.WriteLine($"\nБаланс {UsdName}: {usdBalance}");
                Console.WriteLine($"\nБаланс {Rubname}: {rubBalance}");
                Console.WriteLine($"\nБаланс {TrylName}: {tlBalance}");

                Console.WriteLine($"\nКурс {UsdName} к {Rubname}: {usdToRubCourse}\nкоманда {CommandUsdToRub} для обмена");
                Console.WriteLine($"\nКурс {UsdName} к {TrylName}: {usdToTlCourse}\nкоманда {CommandUsdToTl} для обмена");
                Console.WriteLine($"\nКурс {Rubname} к {UsdName}: {rubToUsdCourse}\nкоманда {CommandRubToUsd} для обмена");
                Console.WriteLine($"\nКурс {Rubname} к {TrylName}: {rubToTlCouse}\nкоманда {CommandRubToTl} для обмена");
                Console.WriteLine($"\nКурс {TrylName} к {UsdName}: {tryToUsdCourse}\nкоманда {CommandTlToUsd} для обмена");
                Console.WriteLine($"\nКурс {TrylName} к {Rubname}: {tryToRubCourse}\nкоманда {CommandTlToRub} для обмена");
                Console.WriteLine($"\nКоманда {CommandForExit} для выхода");
                Console.WriteLine("\n\nВведите нужную вам команду\n");

                userInput = Console.ReadLine();

                switch(userInput)
                {
                    case CommandUsdToRub:
                        Console.WriteLine("Введите желаемое количество денег: ");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if (desiredMoney > usdBalance)
                        {
                            Console.WriteLine($"Желаемое количество денег превышает баланс валюты: {usdBalance}");
                        }
                        else
                        {
                            usdBalance -= desiredMoney;
                            desiredMoney *= usdToRubCourse;
                            rubBalance += desiredMoney;

                            Console.WriteLine($"На баланс валюты: {Rubname} зачисленно {desiredMoney}");
                        }
                        break;

                    case CommandUsdToTl:
                        Console.WriteLine("Введите желаемое количество денег: ");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if(desiredMoney > usdBalance)
                        {
                            Console.WriteLine($"Желаемое количество денег превышает баланс валюты: {usdBalance}");
                        }
                        else
                        {
                            usdBalance -= desiredMoney;
                            desiredMoney *= usdToTlCourse;
                            tlBalance += desiredMoney;

                            Console.WriteLine($"На баланс валюты: {TrylName} зачисленно {desiredMoney}");
                        }
                        break;

                    case CommandRubToUsd:
                        Console.WriteLine("Введите желаемое количество денег: ");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if(desiredMoney > rubBalance)
                        {
                            Console.WriteLine($"Желаемое количество денег превышает баланс валюты: {rubBalance}");
                        }
                        else
                        {
                            rubBalance -= desiredMoney;
                            desiredMoney *= rubToUsdCourse;
                            usdBalance += desiredMoney;

                            Console.WriteLine($"На баланс валюты: {UsdName} зачисленно {desiredMoney}");
                        }
                        break;

                    case CommandRubToTl:
                        Console.WriteLine("Введите желаемое количество денег: ");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if(desiredMoney > rubBalance)
                        {
                            Console.WriteLine($"Желаемое количество денег превышает баланс валюты: {rubBalance}");
                        }
                        else
                        {
                            rubBalance -= desiredMoney;
                            desiredMoney *= rubToTlCouse;
                            tlBalance += desiredMoney;

                            Console.WriteLine($"На баланс валюты: {TrylName} зачисленно {desiredMoney}");
                        }
                        break;

                    case CommandTlToUsd:
                        Console.WriteLine("Введите желаемое количество денег: ");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if(desiredMoney > tlBalance)
                        {
                            Console.WriteLine($"Желаемое количество денег превышает баланс валюты: {tlBalance}");
                        }
                        else
                        {
                            tlBalance -= desiredMoney;
                            desiredMoney *= tryToUsdCourse;
                            usdBalance += desiredMoney;

                            Console.WriteLine($"На баланс валюты: {UsdName} зачисленно {desiredMoney}");
                        }
                        break;

                    case CommandTlToRub:
                        Console.WriteLine("Введите желаемое количество денег: ");
                        desiredMoney = Convert.ToSingle(Console.ReadLine());

                        if (desiredMoney > tlBalance)
                        {
                            Console.WriteLine($"Желаемое количество денег превышает баланс валюты: {tlBalance}");
                        }
                        else
                        {
                            tlBalance -= desiredMoney;
                            desiredMoney *= tryToRubCourse;
                            rubBalance += desiredMoney;

                            Console.WriteLine($"На баланс валюты: {Rubname} зачисленно {desiredMoney}");
                        }
                        break;
                    case CommandForExit:
                        Console.Clear();
                        Console.WriteLine("Выход из программы...");
                        Thread.Sleep(1000);
                        isWork = false;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Нету такой команды!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
