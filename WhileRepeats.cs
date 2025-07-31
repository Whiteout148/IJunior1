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
            const string TlName = "TL";

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

            float UsdToRubCourse = 80.31f;
            float UsdToTlCourse = 40.53f;

            float RubToUsdCourse = 0.0125f;
            float rubToTlCouse = 0.504f;

            float TlToUsdCourse = 0.0247f;
            float TlToRubCourse = 1.98f;

            Console.WriteLine($"Добро пожаловать в нашу программу по конвертации валют");
            Console.WriteLine($"\nВведите ваш баланс из всех доступных валют:");
            Console.WriteLine($"\nВведите баланс валюты: {UsdName}");
            usdBalance = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nВведите баланс валюты: {Rubname}");
            rubBalance = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nВведите баланс валюты: {TlName}");
            tlBalance = Convert.ToInt32(Console.ReadLine());

            while (isWork)
            {
                Console.WriteLine($"\nБаланс {UsdName}: {usdBalance}");
                Console.WriteLine($"\nБаланс {Rubname}: {rubBalance}");
                Console.WriteLine($"\nБаланс {TlName}: {tlBalance}");

                Console.WriteLine($"\nКурс {UsdName} к {Rubname}: {UsdToRubCourse}\nкоманда {CommandUsdToRub} для обмена");
                Console.WriteLine($"\nКурс {UsdName} к {TlName}: {UsdToTlCourse}\nкоманда {CommandUsdToTl} для обмена");
                Console.WriteLine($"\nКурс {Rubname} к {UsdName}: {RubToUsdCourse}\nкоманда {CommandRubToUsd} для обмена");
                Console.WriteLine($"\nКурс {Rubname} к {TlName}: {rubToTlCouse}\nкоманда {CommandRubToTl} для обмена");
                Console.WriteLine($"\nКурс {TlName} к {UsdName}: {TlToUsdCourse}\nкоманда {CommandTlToUsd} для обмена");
                Console.WriteLine($"\nКурс {TlName} к {Rubname}: {TlToRubCourse}\nкоманда {CommandTlToRub} для обмена");
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
                            desiredMoney *= UsdToRubCourse;
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
                            desiredMoney *= UsdToTlCourse;
                            tlBalance += desiredMoney;

                            Console.WriteLine($"На баланс валюты: {TlName} зачисленно {desiredMoney}");
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
                            desiredMoney *= RubToUsdCourse;
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

                            Console.WriteLine($"На баланс валюты: {TlName} зачисленно {desiredMoney}");
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
                            desiredMoney *= TlToUsdCourse;
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
                            desiredMoney *= TlToRubCourse;
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
