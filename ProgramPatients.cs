using System;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            int patient;
            int patientReceptionTime = 10;
            int hour = 60;

            Console.WriteLine("Введите количество пациентов");
            patient = Convert.ToInt32(Console.ReadLine());

            int allPatientsReceptionTime = patient * patientReceptionTime;
            int allHours = allPatientsReceptionTime / hour;
            int minutes = allPatientsReceptionTime % hour;

            Console.WriteLine($"Время прибытие одного пациента: {patientReceptionTime}\nвам стоят в очереди {allHours} часов {minutes} минут.");

            Console.ReadLine();
        }
    }
}
