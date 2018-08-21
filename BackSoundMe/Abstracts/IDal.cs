using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackSoundMe.Abstracts
{
    public interface IDal<T, K>
    {
        IEnumerable<T> GetAll();

        T GetByID(K ID);

        K Create(T model);

        void Update(T model);

        void Delete(K id);
    }
}
