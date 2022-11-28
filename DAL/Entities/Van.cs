namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Van")]
    public partial class Van
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Van()
        {
            Seat = new HashSet<Seat>();
        }

        public int Id { get; set; }

        public int NumberOfAvailableSeats { get; set; }

        public int TypeOfVanId { get; set; }

        public int TrainId { get; set; }

        public int NumberInTrain { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seat> Seat { get; set; }

        public virtual Train Train { get; set; }

        public virtual TypeOfVan TypeOfVan { get; set; }
    }
}
