using Ficha_11;

Engine engine = new Engine(70, 25, 90);

Car nissan = new Car(4,5, "Azul", "Nissan", "Almera", 800, engine, Vehicle.Travel.LAND);

Console.WriteLine(nissan);

Motorcycle motinha = new Motorcycle(Motorcycle.MotorType.CRUISER, 50.3f, "Dourada", "Vespa", "Abelhinha", 450.3f, engine, Vehicle.Travel.WATER);

Console.WriteLine(motinha);

