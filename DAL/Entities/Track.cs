namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Track")]
    public class Track
    {
        public Track()
        {
            TimesForStation = new HashSet<TimesForStation>();
        }
        [Key]
        public int Id { get; set; }
        public int TrainId { get; set; }
        [ForeignKey("TrainId")]
        public virtual Train Train { get; set; }
        public virtual ICollection<TimesForStation> TimesForStation { get; set; }
    }
}
