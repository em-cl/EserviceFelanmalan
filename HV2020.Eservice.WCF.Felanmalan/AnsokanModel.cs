using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HV2020.Eservice.WCF.Felanmalan
{
    public partial class AnsokanModel : DbContext
    {
        public AnsokanModel()
            : base("name=AnsokanModel")
        {
        }

        public virtual DbSet<Adress> Adress { get; set; }
        public virtual DbSet<Granskad> Granskad { get; set; }
        public virtual DbSet<Ort> Ort { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonTyp> PersonTyp { get; set; }
        public virtual DbSet<Synpunkt> Synpunkt { get; set; }
        public virtual DbSet<Telefon> Telefon { get; set; }
        public virtual DbSet<Verksamhet> Verksamhet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adress>()
                .Property(e => e.Gata)
                .IsUnicode(false);

            modelBuilder.Entity<Adress>()
                .HasOptional(e => e.Ort)
                .WithRequired(e => e.Adress);

            modelBuilder.Entity<Ort>()
                .Property(e => e.Ort1)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Epost)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Adress)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.PersonTyp)
                .WithRequired(e => e.Person);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Synpunkt)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Telefon)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Synpunkt>()
                .Property(e => e.Enhet)
                .IsUnicode(false);

            modelBuilder.Entity<Synpunkt>()
                .Property(e => e.SynpunkText)
                .IsUnicode(false);

            modelBuilder.Entity<Synpunkt>()
                .HasMany(e => e.Granskad)
                .WithRequired(e => e.Synpunkt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Verksamhet>()
                .Property(e => e.Enhet)
                .IsUnicode(false);

            modelBuilder.Entity<Verksamhet>()
                .Property(e => e.Verksamhet1)
                .IsUnicode(false);
        }
    }
}
