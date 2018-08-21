using BackSoundMe.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackSoundMe.DAL
{
    public class UserDal : IDal<User, int>
    {
        public int Create(User model)
        {
            int id = MaxIDInTable() + 1;
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                model.ID = id;
                model = db.Users.Add(model);
                db.SaveChanges();
            }

            return model.ID;
        }

        public void Delete(int id)
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                User user = db.Users.FirstOrDefault(x => x.ID == id);

                if (user == null)
                    throw new NullReferenceException("Attempt to remove item that doesn't exist in database.");

                db.Users.Remove(user);

                db.Entry(user).State = EntityState.Deleted;
                db.SaveChanges();
            }
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
            User user = null;

            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                user = db.Users.FirstOrDefault(s => s.ID == ID);
            }

            return user;
        }

        public User GetByUserName(string userName)
        {
            User user = null;

            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                user = db.Users.FirstOrDefault(s => s.Username == userName);
            }

            return user;
        }

        public void Update(User model)
        {
            if (model == null || model.ID < 1)
                throw new ArgumentException("Mode is not valid.");

            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                User modelFromDB = db.Users.FirstOrDefault(e => e.ID == model.ID);

                if (modelFromDB == null)
                    throw new NullReferenceException("Model by given ID doesn't exist at database.");

                modelFromDB.Email = model.Email;
                modelFromDB.First_Name = model.First_Name;
                modelFromDB.Last_Name = model.Last_Name;
                modelFromDB.Password = model.Password;

                db.Entry(modelFromDB).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private int MaxIDInTable()
        {
            int id = 0;
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach (User user in db.Users)
                {
                    if (user.ID > id)
                    {
                        id = user.ID;
                    }
                }
            }

            return id;
        }
    }
}