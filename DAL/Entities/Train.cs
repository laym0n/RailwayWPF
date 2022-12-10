namespace DAL
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Train")]
    public partial class Train
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Train()
        {
            StationTrainSchedule = new HashSet<StationTrainSchedule>();
            Van = new HashSet<Van>();
            Track = new HashSet<Track>();
        }

        public int Id { get; set; }

        
        public int? IdUserCreator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StationTrainSchedule> StationTrainSchedule { get; set; }
        public virtual ICollection<Track> Track { get; set; }

        
        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Van> Van { get; set; }
    }
}
