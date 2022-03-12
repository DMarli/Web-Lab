using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_11
{
    public class Motorcycle: Vehicle
    {
        public MotorType motortype;
        public float speed;
        public enum MotorType
        {
            SPORT,
            CRUISER,
            ADVENTURE
        }


        public Motorcycle(MotorType motortype, float speed, string color, string brand, string model, float weight, Engine engine, Travel travel) : base(color, brand, model, weight, engine, travel)
        {
            this.motortype = motortype;
            this.speed = speed;
        }

        public override string ToString()
        {
            string str = "";
            str += "Motortype:" + motortype + ", ";
            str += "Speed:" + speed + ", ";
            return base.ToString() + str;
        }

        public override void Start()
        {
            Console.WriteLine("Colocar chave na ignição da mota.");
        }
    }
}
