using BackSoundMe.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackSoundMe.DAL
{
    public class UserDal : IDal<User, int>
    {
        public int Create(User model)
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                model = db.Users.Add(model);
                db.SaveChanges();
            }

            return model.ID;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach (User user in db.Users)
                {
                    yield return user;
                }
            }
        }

        public User GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, User model)
        {
            throw new NotImplementedException();
        }
    }
}