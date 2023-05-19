using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Machine
    {
        protected Machine(string name, int yearOfCreation)
        {
            Name = name;
            YearOfCreation = yearOfCreation;
        }

        public string Name { get; set; }

        public int YearOfCreation { get; set; }

        public abstract void MakeSound();

        public abstract void Greet(string personToGreet);
    }
}
