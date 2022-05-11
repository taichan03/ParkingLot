using System;
using System.Collections.Generic;

namespace ParkingLotApp
{
    public class ParkingSystem
    {
        private readonly Dictionary<int, IParking> _parkings;

        public ParkingSystem(int big, int medium, int small)
        {
            _parkings = new Dictionary<int, IParking>();

            var bigParking = ParkingFactory.CreateBig(big);
            var mediumParking = ParkingFactory.CreateMedium(medium);
            var smallParking = ParkingFactory.CreateSmall(small);

            _parkings[bigParking.Type] = bigParking;
            _parkings[mediumParking.Type] = mediumParking;
            _parkings[smallParking.Type] = smallParking;
        }

        public bool AddCar(int carType)
        {
            IParking parking;

            if (_parkings == null || !_parkings.TryGetValue(carType, out parking))
                return false;

            return parking.AddCar();
        }
    }

    public class ParkingFactory
    {
        public static IParking CreateBig(int capacity) => new BigParking(capacity);
        public static IParking CreateMedium(int capacity) => new MediumParking(capacity);
        public static IParking CreateSmall(int capacity) => new SmallParking(capacity);
    }

    public interface IParking
    {
        int Type { get; }
        int Capacity { get; }
        int Available { get; }

        bool AddCar();
    }

    public abstract class ParkingBase : IParking
    {
        public ParkingBase(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("capacity must be positive");

            Capacity = capacity;
            Available = capacity;
        }

        public abstract int Type { get; }
        public int Capacity { get; private set; }
        public int Available { get; set; }

        public virtual bool AddCar()
        {
            if (Available == 0)
                return false;

            Available--;
            return true;
        }
    }

    public class BigParking : ParkingBase
    {
        public BigParking(int capacity) : base(capacity) { }

        public override int Type => 1;
    }

    public class MediumParking : ParkingBase
    {
        public MediumParking(int capacity) : base(capacity) { }

        public override int Type => 2;
    }

    public class SmallParking : ParkingBase
    {
        public SmallParking(int capacity) : base(capacity) { }

        public override int Type => 3;
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
