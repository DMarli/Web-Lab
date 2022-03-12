using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_4
{
    public class Triangle //composto por 3 pontos, não pomos Triangle: Point, porque um triângulo não é um ponto, não é herança
    {
        private Point a; //colocamos private porque vamos expor o atributo no A
        public Point b { get; set; } //atributo em forma de propriedade
        public Point c { get; set; }

        public Triangle()

        {
            a = new Point(); //este construtor por omissão já está feito na outra classe, só invocamos
            b = new Point();
            c = new Point();
        }

        public Triangle(Point a, Point b, Point c)

        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Point A
            { get { return a; } //propriedades
            set { a = value; }
        }   

         
        public double CalculateWidth()

        {
            return a.DistanceTo(b);
        }

        public double CalculateHeight()

        {
            return a.DistanceTo(c);
        }

        public double CalculateArea()
        {
            double width = CalculateWidth();
            double height = CalculateHeight();
            double area = (width * height) / 2;
            return area;

        }

    }


    

}