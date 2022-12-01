namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        [Key]
        public int Id { get; set; }

        public int Cost { get; set; }

        public int SeatId { get; set; }

        public int PassengerId { get; set; }

        public int IdTimesForStationSource { get; set; }

        public int IdTimesForStationDestiny { get; set; }

        public virtual Passenger Passenger { get; set; }

        public virtual Seat Seat { get; set; }

        public virtual TimesForStation TimesForStation { get; set; }

        public virtual TimesForStation TimesForStation1 { get; set; }
    }
}
