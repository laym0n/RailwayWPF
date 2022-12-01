namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("CellStructureVan")]
    public class CellStructureVan
    {
        public int NumberOfCell { get; set; }
        public int NumberOfRow { get; set; }
        public int TypeOfVanId { get; set; }
        public int? NumberOfSeatInVan { get; set; }
        public TypeOfVan TypeOfVan { get; set; }
    }
}
