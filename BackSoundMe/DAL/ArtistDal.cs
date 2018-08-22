using BackSoundMe.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackSoundMe.DAL
{
    public class ArtistDal: IDal<Artist,int>
    {
        public int Create(Artist model)
        {
            int id = MaxIDInTable() + 1;
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                model.ID = id;
                model = db.Artists.Add(model);
                db.SaveChanges();
            }

            return model.ID;
        }

        public void Delete(int id)
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                Artist artist = db.Artists.FirstOrDefault(x => x.ID == id);

                if (artist == null)
                    throw new NullReferenceException("Attempt to remove item that doesn't exist in database.");

                db.Artists.Remove(artist);

                db.Entry(artist).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public IEnumerable<Artist> GetAll()
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach (Artist ar in db.Artists)
                {
                    yield return ar;
                }
            }
        }

        public Artist GetByID(int ID)
        {
            Artist artist = null;

            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                artist = db.Artists.FirstOrDefault(s => s.ID == ID);
            }

            return artist;
        }

        public void Update(Artist model)
        {
            if (model == null || model.ID < 1)
                throw new ArgumentException("Mode is not valid.");

            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                Artist modelFromDB = db.Artists.FirstOrDefault(e => e.ID == model.ID);

                if (modelFromDB == null)
                    throw new NullReferenceException("Model by given ID doesn't exist at database.");

                modelFromDB.First_Name = model.First_Name;
                modelFromDB.Last_Name = model.Last_Name;

                db.Entry(modelFromDB).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private int MaxIDInTable()
        {
            int id = 0;
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach (Artist ar in db.Artists)
                {
                    if (ar.ID > id)
                    {
                        id = ar.ID;
                    }
                }
            }

            return id;
        }
    }
}