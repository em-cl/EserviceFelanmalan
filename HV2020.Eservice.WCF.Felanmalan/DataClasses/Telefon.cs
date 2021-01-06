namespace HV2020.Eservice.WCF.Felanmalan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Telefon")]
    public partial class Telefon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Telefonnummer { get; set; }

        public int PersonID { get; set; }

        public virtual Person Person { get; set; }
    }
}
