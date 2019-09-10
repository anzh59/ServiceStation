using ServiceStation.Domain.Abstract;
using ServiceStation.Domain.Entities;
using System.Linq;

namespace ServiceStation.Domain.Implementation
{
    public class EFCarRepository : ICarRepository
    {
        private EFDbContext _context;

        public EFCarRepository(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<Car> Cars
        {
            get { return _context.Cars; }
        }

        public IQueryable<Model> Models
        {
            get { return _context.Models; }
        }

        public IQueryable<Make> Makes
        {
            get { return _context.Makes; }
        }

        public void SaveCar(Car car)
        {
            if (car.Id == 0)
            {
                _context.Cars.Add(car);
            }
            else
            {
                Car dbEntry = _context.Cars.Find(car.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = car.Id;
                    dbEntry.ClientId = car.ClientId;
                    dbEntry.ModelId = car.ModelId;
                    dbEntry.VIN = car.VIN;
                    dbEntry.Year = car.Year;
                }
            }
            _context.SaveChanges();
        }

        public Car DeleteCar(int carId)
        {
            Car dbEntry = _context.Cars.Find(carId);
            if (dbEntry != null)
            {
                _context.Cars.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
