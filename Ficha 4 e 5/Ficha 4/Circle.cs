using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_4
{
    public class Circle : Shape //herança da Classe Circle

   
    {
        private double radius;

        public Circle() //pedimos atributo raio, porque o shape já tem atributo de ponto
        {
            this.radius = 0;
            this.position = new Point();
        }

        public Circle(Point position, double radius) //os atributos da classe, position herdei da Shape, radius é daqui

        {
            this.position = position;    
            this.radius=radius;
        }

        public override double Area()
        {
            return Math.PI * Math.Pow(radius, 2);
            //  throw new NotImplementedException(); - só para superarmos erros de sistema que não estamos à espera que aconteçam
        }

        public override double Perimeter()
        {
            return 2 * (Math.PI * radius);
           //throw new NotImplementedException();
        }
        public override string ToString()
        {
            // return "X: " + Position.GetX() + ", Y: " + Position.GetY() + ", Radius: " + this.radius; //vai mostrar x seu valor, y seu valor
           
            return Position.ToString() + this.radius; //vamos buscar o mesmo código e adicionamos o raio
        }

    }
}
