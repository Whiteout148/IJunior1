using System;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            int patient;
            int patientReceptionTimeWithMinutes = 10;
            int minutesPerHour = 60;

            Console.WriteLine("Введите количество пациентов");
            patient = Convert.ToInt32(Console.ReadLine());

            int allPatientsReceptionTime = patient * patientReceptionTimeWithMinutes;
            int hoursRemaining = allPatientsReceptionTime / minutesPerHour;
            int minutesRemaining = allPatientsReceptionTime % minutesPerHour;

            Console.WriteLine($"Время прибытие одного пациента: {patientReceptionTimeWithMinutes}" +
                $"\nвам стоят в очереди {hoursRemaining} часов {minutesRemaining} минут.");

            Console.ReadLine();
        }
    }
}
