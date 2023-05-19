using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Vehicle : Machine
    {
        public Vehicle(string name, int yearOfCreation, int numberOfTires)
            : base(name, yearOfCreation)
        {
            NumberOfTires = numberOfTires;
        }

        public int NumberOfTires { get; set; }

        public override void Greet(string personToGreet)
        {
            Console.WriteLine($"Hello {personToGreet}, my name is {Name}, I was created in {YearOfCreation}, have {NumberOfTires} and I am very fast");
        }

        public override void MakeSound()
        {
            Console.WriteLine("Go brrrrrrrrr");
        }

        public void ChangeOil()
        {
            Console.WriteLine("Change oil");
        }
    }
}
