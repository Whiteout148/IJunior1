using System;
using System.Threading;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            Random random = new Random();

            const string ActionPunch = "Punch";
            const string ActionFireBall = "Fireball";
            const string ActionExplode = "Explode";
            const string ActionMedKit = "Medkit";

            int playerHealth = 500;

            int maxPlayerHealth = 500;
            int playerEnergy = 300;
            int maxPlayerEnergy = 300;

            int punchDamage = 10;
            int energyForPunch = 20;

            int fireBallDamage = 80;
            int energyForFireBall = 40;

            int explodeDamage = 100;
            int energyForExplode = 50;

            int quantityOfMedkit = 6;
            int medKitHealthGain = 50;
            int medKitEnergyGain = 40;

            int actualHealthGain;
            int actualEnergyGain;

            bool isUsingFireBall = false;

            int minBossDamage = 30;
            int maxBossDamage = 70;

            int bossHealth = 1000;
            int bossDamage = random.Next(minBossDamage, maxBossDamage + 1);

            string userInput;

            int oneSecondWithMs = 1000;

            Console.WriteLine("** Бой с противником **");

            while(playerHealth > 0 && bossHealth > 0)
            {
                Console.WriteLine($"Здоровье босса: {bossHealth} ваше здоровье: {playerHealth}\n");
                Console.WriteLine($"Ваша энегрия: {playerEnergy}\n");
                Console.WriteLine($"Доступные способности:\n");
                Console.WriteLine("** Удар **");
                Console.WriteLine($"Урон: {punchDamage} Затраты энергии: {energyForPunch}\n команда для юза: {ActionPunch}\n");
                Console.WriteLine("** Огненный шар **");
                Console.WriteLine($"Урон: {fireBallDamage} Затраты энергии: {energyForFireBall}\n команда для юза: {ActionFireBall}\n");
                Console.WriteLine("** Взрыв **");
                Console.WriteLine($"Урон: {explodeDamage} Затраты энергии: {energyForExplode}\n команда для юза: {ActionExplode}");
                Console.WriteLine("Важно: Взрыв доступен только тогда когда бы использован огненный шар");
                Console.WriteLine("При повторном использовании взрыва нужно до этого опять использовать огненный шар\n");
                Console.WriteLine("** аптечка **");
                Console.WriteLine($"Восстановление здоровья: {medKitHealthGain} восстановление энергии: {medKitEnergyGain}\n команда для юза: {ActionMedKit}");
                Console.WriteLine($"Всего аптечек: {quantityOfMedkit}\n");

                Console.WriteLine("Атакует босс...");

                playerHealth -= bossDamage;
                
                Console.WriteLine($"Босс нанес вам {bossDamage} урона, ваш ход: ");
                Console.Write("Напишите команду для нужной способности: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ActionPunch:
                        if (playerEnergy >= energyForPunch)
                        {
                            bossHealth -= punchDamage;
                            playerEnergy -= energyForPunch;

                            Console.WriteLine($"Вы нанесли боссу: {punchDamage} потрачено энергии: {energyForPunch}");
                        }
                        else
                        {
                            Console.WriteLine("Не хватает энергии!");
                        }
                        break;

                    case ActionFireBall:
                        if (playerEnergy >= energyForFireBall)
                        {
                            bossHealth -= fireBallDamage;
                            playerEnergy -= energyForFireBall;

                            isUsingFireBall = true;

                            Console.WriteLine($"Вы нанесли боссу: {fireBallDamage} потрачено энергии: {energyForFireBall}");     
                        }
                        else
                        {
                            Console.WriteLine("Не хватает энергии!");
                        }
                        break;

                    case ActionExplode:
                        if (isUsingFireBall)
                        {
                            if(playerEnergy >= energyForExplode)
                            {
                                bossHealth -= explodeDamage;
                                playerEnergy -= energyForExplode;

                                isUsingFireBall = false;

                                Console.WriteLine($"Вы нанесли боссу: {explodeDamage} потрачено энергии: {energyForExplode}");    
                            }
                            else
                            {
                                Console.WriteLine("Не хватает энергии!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Для использовании взрыва нужно сначала использовать огненный шар");  
                        }
                        break;

                    case ActionMedKit:
                        if (quantityOfMedkit > 0)
                        {
                            quantityOfMedkit--;

                            if (playerHealth + medKitHealthGain > maxPlayerHealth)
                            {
                                actualHealthGain = maxPlayerHealth - playerHealth; 
                                playerHealth = maxPlayerHealth;
                            }
                            else
                            {
                                actualHealthGain = medKitHealthGain;
                                playerHealth += medKitHealthGain;
                            }

                            Console.Write($"Прибавлено здоровья: {actualHealthGain} ");

                            if (playerEnergy + medKitEnergyGain > maxPlayerEnergy)
                            {
                                actualEnergyGain = maxPlayerEnergy - playerEnergy;
                                playerEnergy = maxPlayerEnergy;
                            }
                            else
                            {
                                actualEnergyGain = medKitEnergyGain;
                                playerEnergy += medKitEnergyGain; 
                            }

                            Console.WriteLine($"Прибавлено энергии: {actualEnergyGain}");     
                        }
                        else
                        {
                            Console.WriteLine("У вас больше нету аптечек)");
                        }
                        break;

                    default:
                        Console.WriteLine("\nНеверная команда!");
                        break;
                }

                Console.WriteLine("Ход за боссом...");
                Thread.Sleep(oneSecondWithMs);

                Console.Clear();
            }

            if (playerHealth > 0 && bossHealth <= 0)
            {
                Console.WriteLine("Игрок выиграл.");
            }
            if (bossHealth > 0 && playerHealth <= 0)
            {
                Console.WriteLine("Босс выиграл.");
            }
            if (playerHealth <= 0 && bossHealth <= 0)
            {
                Console.WriteLine("Ничья.");
            }

            Console.ReadKey();
        }
    }
}
