using ConsoleApp1;

Robot robot = new Robot("Robo", 2020, "Stiliyan");
Vehicle vehicle = new Vehicle("BMW", 2001, 4);

robot.Greet("Ivan");
robot.MakeSound();
robot.Work();

vehicle.MakeSound();
vehicle.Greet("Kiro");
vehicle.ChangeOil();


