using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        Direction direction = new Direction();
        Train train = new Train();
        while (true)
        {
            Console.WriteLine("______________Информация о существующих поездах______________");
            direction.ShowAllDirections();
            direction.ShowAllTrains();
            Console.WriteLine();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("___________________Конфигуратор пассажирских поездов___________________");
            Console.Write("Отправление (название города) :");
            string userInputDeparture = Console.ReadLine();
            Console.Write("Прибытие (название города) :");
            string userInputArrive = Console.ReadLine();
            direction.AddNewDirection(userInputDeparture, userInputArrive);


            direction.AddNewTrain();
            Console.ReadKey();
            Console.Clear();
        }
    }

    public class Direction
    {

        public string PointOfDeparturing { get; private set; }
        public string PointOfArriving { get; private set; }


        private List<Direction> _allDirections = new List<Direction>();

        private List<Train> _allTrains = new List<Train>();

        public Direction() { }
        public Direction(string poingOfDeparturing, string pointOfArriving)
        {
            PointOfArriving = pointOfArriving;
            PointOfDeparturing = poingOfDeparturing;
        }

        public void AddNewDirection(string userInputDeparture, string userInputArrive)
        {
            Direction direction = new Direction(userInputDeparture, userInputArrive);
            _allDirections.Add(direction);
        }

        public void AddNewTrain()
        {

            Random rand = new Random();
            int numberOfPassengers = rand.Next(180, 200);
            Train train = new Train(numberOfPassengers, null);
            train.AddCarriages(numberOfPassengers);
            Console.Write("\nОтправить поезд по заданному направлению? (да/нет)");
            while (true)
            {
                string userDepartureChoise = Console.ReadLine();
                userDepartureChoise.ToLower();
                if (userDepartureChoise == "да")
                {
                    train.Departure();
                    break;
                }
                else if (userDepartureChoise == "нет")
                {
                    train.Wait();
                    break;
                }
                else
                {
                    Console.Write("Вы ввели неправильную команду. Повторите ввод: ");
                }
            }
            _allTrains.Add(train);
        }

        public void ShowAllTrains()
        {
            int column = 1;
            foreach (Train t in _allTrains)
            {
                Console.SetCursorPosition(55, column);
                Console.WriteLine($"Пассажиров : {t.NumberOfPassengers} Статус: {t.DepartureStatus}");
                Console.SetCursorPosition(40, column);
                t.ShowNumberOfCarriages();
                column++;
            }

        }

        public void ShowAllDirections()
        {
            if (_allDirections.Count == 0)
            {
                Console.WriteLine("Информация отсутствует!");
            }
            else
            {
                foreach (Direction d in _allDirections)
                {
                    Console.WriteLine($"Направление поезда: {d.PointOfDeparturing + " - " + d.PointOfArriving }");
                }
            }
        }
    }

    public class Train
    {
        public string DepartureStatus { get; private set; }
        public int NumberOfPassengers { get; private set; }

        private List<Carriage> _allCarriages = new List<Carriage>();

        public Train() { }
        public Train(int numberOfPassengers, string departureStatus)
        {
            NumberOfPassengers = numberOfPassengers;

            DepartureStatus = departureStatus;
        }

        public void Departure()
        {
            DepartureStatus = "Отправлен";
        }

        public void Wait()
        {
            DepartureStatus = "Ожидает отправления";
        }

        public void ShowNumberOfCarriages()
        {
            Console.WriteLine($"Вагонов : {_allCarriages.Count}");
        }

        public void AddCarriages(int numberOfPassengers)
        {
            int luxCappacity = 18;
            int economCappacity = 56;
            Console.WriteLine($"Было куплено {numberOfPassengers} билетов." +
                $" \nВ вагонах класса Люкс может разместиться {luxCappacity} человек, Эконом - {economCappacity}.");
            Console.Write("\nСколько людей разметится в вагонах Люкс (остальные разместятся в вагонах Эконом) :");
            while (true)
            {
                int userInputNumber = Convert.ToInt32(Console.ReadLine());
                if (userInputNumber > numberOfPassengers)
                {
                    Console.WriteLine("Нельзя разместить больше пассажиров, чем было куплено билетов!");
                }
                else
                {
                    numberOfPassengers -= userInputNumber;
                    int numberOfLux = userInputNumber / luxCappacity;
                    if (userInputNumber % luxCappacity > 0)
                    {
                        numberOfLux++;
                    }
                    for (int i = 0; i < numberOfLux; i++)
                    {
                        if (userInputNumber < luxCappacity)
                        {
                            Lux lux = new Lux(userInputNumber);
                            _allCarriages.Add(lux);
                        }
                        else
                        {
                            Lux lux = new Lux(luxCappacity);
                            _allCarriages.Add(lux);
                        }
                        userInputNumber -= luxCappacity;
                    }
                }
                int numberOfEconom = numberOfPassengers / economCappacity;
                if (numberOfPassengers % economCappacity > 0)
                {
                    numberOfEconom++;
                }
                for (int i = 0; i < numberOfEconom; i++)
                {
                    if (numberOfPassengers < economCappacity)
                    {
                        Econom econom = new Econom(numberOfPassengers);
                        _allCarriages.Add(econom);
                    }
                    else
                    {
                        Econom econom = new Econom(economCappacity);
                        _allCarriages.Add(econom);
                    }
                    numberOfPassengers -= economCappacity;
                }
                break;
            }
        }
    }

    class Carriage
    {
        public int Capacity { get; private set; }
        public Carriage() { }
        public Carriage(int capacity)
        {
            Capacity = capacity;
        }
    }

    class Lux : Carriage
    {
        public Lux(int capacity) : base(capacity) { }
    }
    class Econom : Carriage
    {
        public Econom(int capacity) : base(capacity) { }
    }
}