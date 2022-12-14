namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StationTrainSchedule")]
    public partial class StationTrainSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StationTrainSchedule()
        {
        }

        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Index("UQ_FirstAndSecond", 1, IsUnique = true)]
        public int IdStation { get; set; }
        [Index("UQ_FirstAndSecond", 2, IsUnique = true)]
        public int IdTrain { get; set; }

        public int NumberInTrip { get; set; }

        public virtual Station Station { get; set; }

        public virtual Train Train { get; set; }
    }
}
