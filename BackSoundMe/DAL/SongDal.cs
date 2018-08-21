using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BackSoundMe.Abstracts;

namespace BackSoundMe.DAL
{
    public class SongDal : IDal<Song, int>
    {
        public int Create(Song model)
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                model = db.Songs.Add(model);
                db.SaveChanges();
            }

            return model.ID;
        }

        public void Delete(int id)
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                Song song = db.Songs.FirstOrDefault(x => x.ID == id);

                if (song == null)
                    throw new NullReferenceException("Attempt to remove item that doesn't exist in database.");

                db.Songs.Remove(song);

                db.Entry(song).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public IEnumerable<Song> GetAll()
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach(Song song in db.Songs)
                {
                    yield return song;
                }
            }
        }

        public Song GetByID(int ID)
        {
            Song song = null;

            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                song = db.Songs.FirstOrDefault(s => s.ID == ID);
            }

            return song;
        }

        public void Update(int id, Song model)
        {
            throw new NotImplementedException();
        }
    }
}