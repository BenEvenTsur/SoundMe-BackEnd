using BackSoundMe.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackSoundMe.DAL
{
    public class CreatedByDal : IDal<CreatedBy, int>
    {
        public int Create(CreatedBy model)
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                model = db.CreatedByArtists.Add(model);
                db.SaveChanges();
            }

            return 1;
        }

        public void Delete(int id)
        {
            /*using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                Artist artist = db.Artists.FirstOrDefault(x => x.ID == id);

                if (artist == null)
                    throw new NullReferenceException("Attempt to remove item that doesn't exist in database.");

                db.Artists.Remove(artist);

                db.Entry(artist).State = EntityState.Deleted;
                db.SaveChanges();
            }*/
        }

        public IEnumerable<CreatedBy> GetAll()
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach (CreatedBy ar in db.CreatedByArtists)
                {
                    yield return ar;
                }
            }
        }

        public CreatedBy GetByID(int ID)
        {
            CreatedBy c = null;
            /*
             using (ChordsDBEntities1 db = new ChordsDBEntities1())
             {
                 artist = db.Artists.FirstOrDefault(s => s.ID == ID);
             }

             return artist;*/
            return c;
        }

        public void Update(CreatedBy model)
        {
            /* if (model == null || model.ID < 1)
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
             }*/
        }

        public IEnumerable<CreatedBy> GetAllArtistsOfSong(int songID)
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach (CreatedBy createdby in db.CreatedByArtists)
                {
                    if (createdby.Song_ID == songID)
                    {
                        yield return createdby;
                    }
                }
            }

        }
    }
}