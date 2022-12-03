using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class TrackRepository : IRepositoryTrack
    {
        private RailWayDB db;
        public TrackRepository(RailWayDB db)
        {
            this.db = db;
        }

        public List<Track> GetList()
        {
            return db.Track.ToList();
        }
        public Track GetItem(int id)
        {
            return db.Track.FirstOrDefault(i => i.Id == id);
        }
        public void Create(Track item)
        {
            db.Track.Add(item);
        }
        public void Update(Track item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Track track = GetItem(id);
            if (track != null)
                db.Track.Remove(track);
        }
    }
}
