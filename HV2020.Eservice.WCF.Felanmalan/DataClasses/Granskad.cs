namespace HV2020.Eservice.WCF.Felanmalan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Granskad")]
    public partial class Granskad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DokumentNr { get; set; }

        public DateTime? DatumGranskad { get; set; }

        public int SynpunktID { get; set; }

        public int? LoginID { get; set; }

        public virtual Synpunkt Synpunkt { get; set; }
    }
}
