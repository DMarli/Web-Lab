using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_11
{
    public class Car : Vehicle
    {
        public int door;
        public int seats;

        public Car (int door, int seats, string color, string brand, string model, float weight, Engine engine, Travel travel) : base (color, brand, model, weight,engine, travel)
        {
            this.door = door;
            this.seats = seats;
        }

        public override void Start()
        {
            Console.WriteLine("Colocar chave na ignição do carro.");
        }

        public override string ToString()
        {  
            string str = "";
            str += "Door Number:" + door + ", ";
            str += "Seat Number:" + seats + ", ";  
            return base.ToString() + str;
        }

    }
}
