using DomianLayar.Entities;
using DomianLayar.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public class ShoraDbContext : IdentityDbContext<BaseUser>
    {
        public ShoraDbContext(DbContextOptions<ShoraDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    

            // Lawyer ↔ LawFirm (One-to-Many) 
            modelBuilder.Entity<Lawyer>()
                .HasOne(l => l.LawFirm)
                .WithMany(f => f.Lawyers)
                .HasForeignKey(l => l.LawFirmId)
                .OnDelete(DeleteBehavior.NoAction); // 🔥 جرب NoAction بدل Restrict


            modelBuilder.Entity<Case>()
    .HasOne(c => c.User)                // الـ User اللي رفع القضية
    .WithMany(u => u.ClientCases)       // القضايا الخاصة بيه كـ Client
    .HasForeignKey(c => c.UserId)
    .OnDelete(DeleteBehavior.Restrict);

            // علاقة الـ Lawyer
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Lawyer)              // المحامي اللي استلم القضية
                .WithMany(u => u.LawyerCases)       // القضايا الخاصة بيه كمحامي
                .HasForeignKey(c => c.LawyerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder); 
        }
        DbSet<BaseUser> client {get; set;}
        DbSet<Lawyer> Lawyers { get; set;}
        DbSet<LawFirm> LawFirm { get; set;}
        DbSet<Case> Cases { get; set;}
        DbSet<Post> posts{ get; set;}
        DbSet<comment> comments{ get; set;}
        DbSet<subscriPstion> subscriPstions{ get; set;}

    }
}
