using System;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            int patient;
            int patientReceptionTimeWithMinutes = 10;
            int oneHour = patientReceptionTimeWithMinutes * 6;

            Console.WriteLine("Введите количество пациентов");
            patient = Convert.ToInt32(Console.ReadLine());

            int allPatientsReceptionTime = patient * patientReceptionTimeWithMinutes;
            int HoursRemaining = allPatientsReceptionTime / oneHour;
            int minutesRemaining = allPatientsReceptionTime % oneHour;

            Console.WriteLine($"Время прибытие одного пациента: {patientReceptionTimeWithMinutes}" +
                $"\nвам стоят в очереди {HoursRemaining} часов {minutesRemaining} минут.");

            Console.ReadLine();
        }
    }
}
