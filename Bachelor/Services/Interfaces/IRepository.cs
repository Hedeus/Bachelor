using Bachelor.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Bachelor.Services.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T Get(int id)
        {
            return GetAll().FirstOrDefault(item => item.Id == id);
        }

        bool Remove(T item);
        void Update(int id, T item);
    }
}
