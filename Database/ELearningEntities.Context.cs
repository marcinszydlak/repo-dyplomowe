﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KursyELearningDBEntities : DbContext
    {
        public KursyELearningDBEntities()
            : base("name=KursyELearningDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Klasy> Klasy { get; set; }
        public virtual DbSet<Kursy> Kursy { get; set; }
        public virtual DbSet<Nauczyciele> Nauczyciele { get; set; }
        public virtual DbSet<Przedmioty> Przedmioty { get; set; }
        public virtual DbSet<Rozwiązania> Rozwiązania { get; set; }
        public virtual DbSet<Uczniowie> Uczniowie { get; set; }
        public virtual DbSet<Zadania> Zadania { get; set; }
    }
}
