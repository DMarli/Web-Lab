using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_4
{
    public class Rectangle
    {
        //Classes têm sempre
        //atributos
        //construtores
        //métodos

        //atributos por norma são privados, se for necessário criamos selector ou modificador
        private Point topLeftPoint;
        private double height;
        private double width;

        //construtor, por omissão não recebe argumentos

        public Rectangle()

        {
            this.topLeftPoint = new Point();
            this.height = 0;
            this.width = 0;
        }
        //construtor com parâmetros, tem de receber alguma coisa
        public Rectangle(Point topLeftPoint, double height, double width)
        {
            this.topLeftPoint = topLeftPoint;   
            this.height = height;   
            this.width = width; 
        }

        //propriedades/seletores para podermos aceder aos atributos
        public Point TopLeftPoint //aqui colocamos em maiúscula
        {
            get { return topLeftPoint; }
            set { topLeftPoint = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }
   
        public double Area()

        {
            return width * height;
        }

        public double Perimeter() //soma de todos os lados

        {
            return (height*2) + (width*2);
        }

        public bool Contains(Point point) //se o ponto está ou não contido no rectângulo
        { 
            Point topRightPoint = new Point(topLeftPoint.GetX() + width, topLeftPoint.GetY()); // (x,y) somar o x do topLeftPoint à largura
            Point bottomLeftPoint = new Point(topLeftPoint.GetX(), topLeftPoint.GetY() - height);
            Point bottomRightPoint = new Point(topRightPoint.GetX(), topLeftPoint.GetY() - height);

            if (point.GetX() > topLeftPoint.GetX() && point.GetX() < topRightPoint.GetX() 
                && point.GetY() > bottomLeftPoint.GetY() && point.GetY() < TopLeftPoint.GetY())
            {
                return true;    
            }
            else
            {
                return false;
            }
        }

    }
}