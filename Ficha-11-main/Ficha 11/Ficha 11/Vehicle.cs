using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_11
{
    public abstract class Vehicle
    
    {
        private string color;
        private string brand;
        private string model;
        private float weight;   
        private Engine engine;
        private Travel travel;
        public enum Travel
        {
            LAND,
            WATER,
            AIR
        }

        public Vehicle() { }

        public Vehicle(string color, string brand, string model, float weight, Engine engine, Travel travel)
        { 
            this.color = color;
            this.brand = brand;
            this.model = model; 
            this.weight = weight;   
            this.engine = engine;   
            this.travel = travel;   
        }

        public override string ToString()
        {
            string str = "";
            str += "Color:" + color + ", ";
            str += "Brand:" + brand + ", ";
            str += "Model:" + model + ", ";
            str += "Weight:" + weight + ", ";
            str += "Engine:" + engine + ", ";
            str += "Travel:" + travel + ", ";
            return str;
        }

        public abstract void Start();
    }
}
