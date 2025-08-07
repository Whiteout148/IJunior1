using System;
using System.Threading;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            Random random = new Random();

            const string UseNormalDamage = "1";
            const string UseFireBall = "2";
            const string UseExplode = "3";
            const string UseMedKit = "4";

            int playerHealth = 500;
            int maxPlayerHealth = 500;
            int playerEnergy = 300;
            int maxPlayerEnergy = 300;

            int normalAttackDamage = 10;
            int energyForNormalAttack = 30;

            int fireBallDamage = 50;
            int energyForFireBall = 70;

            int explodeDamage = 100;
            int energyForExplode = 100;

            int quantityOfMedkit = 6;
            int medKitHealthGain = 50;
            int medKitEnergyGain = 40;

            int actualHealthGain;
            int actualEnergyGain;

            bool isUsingFireBall = false;

            int minBossDamage = 30;
            int maxBossDamage = 70;

            int bossHealth = 1000;
            int bossDamage = random.Next(5, 10 + 1);

            string userInput;

            int oneSecondWithMs = 1000;

            Console.WriteLine("** Бой с противником **");

            while(bossHealth > 0 && playerHealth > 0)
            {
                Console.WriteLine($"Здоровье босса: {bossHealth} ваше здоровье: {playerHealth}\n");
                Console.WriteLine($"Ваша энегрия: {playerEnergy}\n");
                Console.WriteLine($"Доступные способности:\n");
                Console.WriteLine("** Обычная атака **");
                Console.WriteLine($"Урон: {normalAttackDamage} Затраты энергии: {energyForNormalAttack}\n команда для юза: {UseNormalDamage}\n");
                Console.WriteLine("** Огненный шар **");
                Console.WriteLine($"Урон: {fireBallDamage} Затраты энергии: {energyForFireBall}\n команда для юза: {UseFireBall}\n");
                Console.WriteLine("** Взрыв **");
                Console.WriteLine($"Урон: {explodeDamage} Затраты энергии: {energyForExplode}\n команда для юза: {UseExplode}");
                Console.WriteLine("Важно: Взрыв доступен только тогда когда бы использован огненный шар");
                Console.WriteLine("При повторном использовании взрыва нужно до этого опять использовать огненный шар\n");
                Console.WriteLine("** аптечка **");
                Console.WriteLine($"Восстановление здоровья: {medKitHealthGain} восстановление энергии: {medKitEnergyGain}\n команда для юза: {UseMedKit}");
                Console.WriteLine($"Всего аптечек: {quantityOfMedkit}\n");

                Console.WriteLine("Первый атакует босс...");
                Thread.Sleep(oneSecondWithMs);

                playerHealth -= bossDamage;
                
                Console.WriteLine($"Босс нанес вам {bossDamage} урона, ваш ход: ");
                Console.Write("Напишите команду для нужной способности: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case UseNormalDamage:

                        if (playerEnergy >= energyForNormalAttack)
                        {
                            bossHealth -= normalAttackDamage;
                            playerEnergy -= energyForNormalAttack;

                            Console.WriteLine($"Вы нанесли боссу: {normalAttackDamage} потрачено энергии: {energyForNormalAttack}");
                            Console.WriteLine("Ход за боссом...");
                            Thread.Sleep(oneSecondWithMs);
                        }
                        else
                        {
                            Console.WriteLine("Не хватает энергии!\n ход за боссом...");
                            Thread.Sleep(oneSecondWithMs);
                        }

                        break;

                    case UseFireBall:

                        if (playerEnergy >= energyForFireBall)
                        {
                            bossHealth -= fireBallDamage;
                            playerEnergy -= energyForFireBall;

                            isUsingFireBall = true;

                            Console.WriteLine($"Вы нанесли боссу: {fireBallDamage} потрачено энергии: {energyForFireBall}");
                            Console.WriteLine("Ход за боссом...");
                            Thread.Sleep(oneSecondWithMs);
                        }
                        else
                        {
                            Console.WriteLine("Не хватает энергии!\n ход за боссом...");
                            Thread.Sleep(oneSecondWithMs);
                        }

                        break;

                    case UseExplode:

                        if (isUsingFireBall)
                        {
                            if(playerEnergy >= energyForExplode)
                            {
                                bossHealth -= explodeDamage;
                                playerEnergy -= energyForExplode;

                                isUsingFireBall = false;

                                Console.WriteLine($"Вы нанесли боссу: {explodeDamage} потрачено энергии: {energyForExplode}");
                                Console.WriteLine("Ход за боссом...");
                                Thread.Sleep(oneSecondWithMs);
                            }
                            else
                            {
                                Console.WriteLine("Не хватает энергии!");
                                Console.WriteLine("Ход за боссом...");
                                Thread.Sleep(oneSecondWithMs);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Для использовании взрыва нужно сначала использовать огненный шар");
                            Console.WriteLine("Ход за боссом...");
                            Thread.Sleep(oneSecondWithMs);
                        }

                        break;

                    case UseMedKit:

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

                            Console.WriteLine("\nХод за боссом...");
                            Thread.Sleep(oneSecondWithMs);
                        }
                        else
                        {
                            Console.WriteLine("У вас больше нету аптечек)");
                            Console.WriteLine("Ход за боссом...");
                            Thread.Sleep(oneSecondWithMs);
                        }

                        break;

                    default:
                        Console.WriteLine("\nНеверная команда!");
                        Console.WriteLine("Ход за боссом...");
                        Thread.Sleep(oneSecondWithMs);
                        Console.Clear();
                        continue;
                        break;
                }

                if(playerHealth <= 0 && bossHealth > 0)
                {
                    Console.WriteLine("Игрок проиграл а босс выиграл!");
                    break;
                }
                if(bossHealth <= 0 && playerHealth > 0)
                {
                    Console.WriteLine("Босс проиграл а игрок выиграл!");
                    break;
                }

                Console.Clear();
            }
        }
    }
}
