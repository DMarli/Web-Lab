using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_4
{
    public class Point //implementação, só este código não faria nada pois não temos invocação, temos de criar instâncias
    {
        private double x;
        private double y;

        public Point()

        {
            this.x = 0;
            this.y = 0; 
        }

        public double DistanceTo(Point other) //no main podemos clicar no nome e criar método 

        {

            //Math é classe de pacote, Square Root, não Classe de Instância

            double dx = Math.Pow((other.x - this.x), 2);
            double dy = Math.Pow((other.y - this.y), 2);

            return Math.Sqrt(dx + dy);
  
        }

        public override string ToString()
        {
            return "X: " + this.x + ", Y: " + this.y; //vai mostrar x seu valor, y seu valor
        }

        public Point(double x, double y) //construtor
        {
            this.x = x;
            this.y = y;
        }

        public void SetX(double x) //getters e setters
        {
            this.x = x;          
        }
        public void SetY(double y) 
        {
            this.y = y;
        }

        public void SetXY(double x, double y) //não podemos fazer get porque só podemos fazer a 1 variável
        {
            this.x = x;
            this.y = y;
        }
        public double GetX()
        {
            return this.x;

        }

        public double GetY()
        {
            return this.y;

        }


    }
}
//todos humanos, mesmas características mas com valores diferentes (ex pele escura, clara)