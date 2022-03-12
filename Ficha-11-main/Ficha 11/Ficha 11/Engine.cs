using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_11
{
    public class Engine
    {
        public int torque;
        public int displacement;
        public int horsepower;

        public Engine() { }
        public Engine(int torque, int displacement, int horsepower)
        {
            this.torque = torque;
            this.displacement = displacement;
            this.horsepower = horsepower;
        }

        public override string ToString()
        {
            string str = "";
            str += "Torque: " + torque + ", ";
            str += "Displacement: " + displacement + ", ";
            str += "HorsePower: " + horsepower + ", "; 
            return str;
        }

    }
}
