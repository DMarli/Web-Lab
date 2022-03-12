using Ficha_4;

//Nome Classe nome da instância = new (significa que estou a criar uma nova instância
//deste topo com este nome) Nome Classe (...)

//só usamos nome dos tipos quando estamos a declarar

Point p1 = new Point(1,1); //invocar o construtor por omissão
Point p2 = new Point(10,20); //valores
double d = p1.DistanceTo(p2);

//Console.WriteLine(p1.GetX());
//p1.SetX(100);
//Console.WriteLine(p1.GetX());
//Console.WriteLine(p1.GetY());
//Console.WriteLine(p2.GetX());
//Console.WriteLine(p2.GetY());
//p1.SetXY(9, 9);
//Console.WriteLine(p1.GetX());
//Console.WriteLine(p1.GetY());

Triangle triangle = new Triangle();

Console.WriteLine(d);   

Rectangle r1 = new Rectangle(); 
Rectangle r2 = new Rectangle(new Point (0,5), 5, 5); //o primeiro é top left point, tem 2 valores

double areaRect = r2.Area();
double perimeRect = r2.Perimeter();

Console.WriteLine(perimeRect);
Console.WriteLine(areaRect);

Point point = new Point(1,4); //Não podemos por o Contains

bool contains = r2.Contains(point); //para saber se o triângulo contém o ponto

Console.WriteLine(contains);

Point point2 = new Point(6,6);

bool contains2 = r2.Contains(point2);

Console.WriteLine(contains2);

Circle c1 = new Circle(); //está na posição 0,0
Circle c2 = new Circle(new Point (5,5), 5);



double areaCircle1 = c1.Area();
double perimenterCircle1 = c1.Perimeter();

double areaCircle2 = c2.Area();
double perimenterCircle2 = c2.Perimeter();

Console.WriteLine("Área do Círculo " + areaCircle2);
Console.WriteLine("Posição do Círculo " + c2.Position);

Console.WriteLine(c2.ToString());
