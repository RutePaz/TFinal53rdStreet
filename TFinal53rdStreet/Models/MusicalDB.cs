using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TFinal53rdStreet.Models
{
    public class MusicalDB : DbContext
    {
        public MusicalDB() : base("MusicalDBConnectionString") { }

        //descrever os nomes das tabelas na Base de Dados
        public virtual DbSet<Musical> Musical { get; set; }
        public virtual DbSet<Cast> Actor { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Soundtrack> Song { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<CastMusical> CastMusical { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}