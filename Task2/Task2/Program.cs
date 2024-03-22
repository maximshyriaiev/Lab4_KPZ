using System;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Створення командного центру
            ICommandCentre commandCentre = new CommandCentre();

            // Створення аеропортної доріжки
            Runway runway = new Runway();

            // Створення літака
            Aircraft aircraft = new Aircraft("Boeing 747");

            // Реєстрація аеропортної доріжки в командному центрі
            commandCentre.RegisterRunway(runway);

            // Реєстрація літака в командному центрі
            commandCentre.RegisterAircraft(aircraft);

            // Посадка літака
            commandCentre.Land(aircraft);

            // Зліт літака
            commandCentre.TakeOff(aircraft);
        }
    }

    interface ICommandCentre
    {
        void RegisterRunway(Runway runway);
        void RegisterAircraft(Aircraft aircraft);
        bool CanLand();
        void Land(Aircraft aircraft);
        void TakeOff(Aircraft aircraft);
    }

    class CommandCentre : ICommandCentre
    {
        private Runway _runway;
        private Aircraft _aircraft;

        public void RegisterRunway(Runway runway)
        {
            _runway = runway;
        }

        public void RegisterAircraft(Aircraft aircraft)
        {
            _aircraft = aircraft;
        }

        public bool CanLand()
        {
            return _runway.IsFree();
        }

        public void Land(Aircraft aircraft)
        {
            if (CanLand())
            {
                Console.WriteLine($"Aircraft {aircraft.Name} is landing.");
                Console.WriteLine($"Checking runway.");
                Console.WriteLine($"Aircraft {aircraft.Name} has landed.");
                _runway.Occupy();
            }
            else
            {
                Console.WriteLine($"Could not land, the runway is busy.");
            }
        }

        public void TakeOff(Aircraft aircraft)
        {
            Console.WriteLine($"Aircraft {aircraft.Name} is taking off.");
            _runway.Vacate();
            Console.WriteLine($"Aircraft {aircraft.Name} has taken off.");
        }
    }

    class Aircraft
    {
        public string Name { get; }

        public Aircraft(string name)
        {
            Name = name;
        }
    }

    class Runway
    {
        private bool _isOccupied = false;

        public bool IsFree()
        {
            return !_isOccupied;
        }

        public void Occupy()
        {
            _isOccupied = true;
            Console.WriteLine($"Runway is now occupied.");
        }

        public void Vacate()
        {
            _isOccupied = false;
            Console.WriteLine($"Runway is now vacant.");
        }
    }
}
