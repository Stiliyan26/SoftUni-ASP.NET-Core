using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Robot : Machine
    {
        public Robot(string name, int yearOfCreation, string creator)
            : base(name, yearOfCreation)
        {
            Creator = creator;
        }
        public string Creator { get; set; }
        public override void Greet(string personToGreet)
        {
            Console.WriteLine($"Hello {personToGreet}, my name is {Name}, I was created in {YearOfCreation} by {Creator} and I will become terminator one day.");
        }

        public override void MakeSound()
        {
            Console.WriteLine("Yes, master");
        }

        public void Work()
        {
            Console.WriteLine("Going to work");
        }
    }
}
