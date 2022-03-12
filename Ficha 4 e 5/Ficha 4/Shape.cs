using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha_4
{
    public abstract class Shape //tem pelo menos 1 método abstrato, ou seja, não é concreto, não tem implementação e corpo.

    //ao colocarmos internal, classe Circle ia ter acesso a todos os atributos, private só tem acesso na classe
    {
        internal Point position; //atributo do tipo point - private por omissão

        public Shape() //construtor sempre public e nome da classe

        {
            position = new Point();        
        }

        public Shape(Point position)
        {
            this.position = position;
        }
        public Point Position 
        { 
            get { return position; }
            set { position = value; } //position igual ao value que for atribuído nesta propriedade
        }

        //Métodos abstratos só providenciam a assinatura do método
        public abstract double Area();
        public abstract double Perimeter();

        //método vazio para calcular a área da forma, todas as classes que extenderem esta classe vão ter de a implementar
        //faz sentido porque a forma de um rectângulo, triângulo, círculo, são diferentes



    }
}
