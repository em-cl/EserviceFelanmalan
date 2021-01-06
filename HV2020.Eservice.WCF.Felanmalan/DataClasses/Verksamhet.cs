namespace HV2020.Eservice.WCF.Felanmalan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Verksamhet")]
    public partial class Verksamhet
    {
        [Key]
        [StringLength(50)]
        public string Enhet { get; set; }

        [Column("Verksamhet")]
        [Required]
        [StringLength(50)]
        public string Verksamhet1 { get; set; }
    }
}
