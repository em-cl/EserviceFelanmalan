namespace HV2020.Eservice.WCF.Felanmalan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ort")]
    public partial class Ort
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Postnummer { get; set; }

        [Column("Ort")]
        [Required]
        [StringLength(50)]
        public string Ort1 { get; set; }

        public virtual Adress Adress { get; set; }
    }
}
