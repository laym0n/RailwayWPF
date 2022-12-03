namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimesForStation")]
    public partial class TimesForStation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimesForStation()
        {
            Ticket = new HashSet<Ticket>();
            Ticket1 = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public DateTime ArrivalTime { get; set; }

        public DateTime DepartureTime { get; set; }

        public int IdStationTrainSchedule { get; set; }
        [ForeignKey("IdStationTrainSchedule")]
        public virtual StationTrainSchedule StationTrainSchedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket1 { get; set; }
    }
}
