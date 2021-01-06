namespace HV2020.Eservice.WCF.Felanmalan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adress")]
    public partial class Adress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Postnummer { get; set; }

        [Required]
        [StringLength(50)]
        public string Gata { get; set; }

        public int PersonID { get; set; }

        public virtual Person Person { get; set; }

        public virtual Ort Ort { get; set; }
    }
}
