using ServiceStation.Domain.Entities;
using System.Linq;

namespace ServiceStation.Domain.Abstract
{
    public interface ICarRepository
    {
        IQueryable<Car> Cars { get; }
        IQueryable<Model> Models { get; }
        IQueryable<Make> Makes { get; }
        void SaveCar(Car car);
        Car DeleteCar(int carId);
    }
}
