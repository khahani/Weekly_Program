using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WP_Web.Models
{
    public class WPDB : DbContext
    {
        public WPDB() : base("Server=WIN-SPK3MC24AA4\\DEV;Database=WPDB;Integrated Security=true;") { }
        public DbSet<User> Users { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<CanTeach> CanTeaches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_NameAndAcademicYear", 1) { IsUnique = true })
                );

            modelBuilder.Entity<Teacher>()
                .Property(p => p.AcademicYearId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_NameAndAcademicYear", 2) { IsUnique = true }));

            modelBuilder.Entity<Teacher>()
                .HasRequired(t => t.AcademicYear)
                .WithMany()
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}