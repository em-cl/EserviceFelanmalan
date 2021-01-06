namespace HV2020.Eservice.WCF.Felanmalan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Synpunkt")]
    public partial class Synpunkt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Synpunkt()
        {
            Granskad = new HashSet<Granskad>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SynpunktID { get; set; }

        [Required]
        [StringLength(50)]
        public string Enhet { get; set; }

        public DateTime DatumMottagit { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string SynpunkText { get; set; }

        public short Avseende { get; set; }

        public int PersonID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Granskad> Granskad { get; set; }

        public virtual Person Person { get; set; }
    }
}
