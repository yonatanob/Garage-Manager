namespace Ex3.GarageUI
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            GarageHandler garageHandler = new GarageHandler();
            garageHandler.StartGarage();
            Console.ReadLine();
        }
    }
}
