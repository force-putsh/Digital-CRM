using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Crm.Models.Crmdata;

namespace Crm.Data
{
  public partial class CrmdataContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public CrmdataContext(DbContextOptions<CrmdataContext> options):base(options)
    {
    }

    public CrmdataContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Crm.Models.Crmdata.Contrat>()
              .HasOne(i => i.Contact)
              .WithMany(i => i.Contrats)
              .HasForeignKey(i => i.ContactId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Crm.Models.Crmdata.Contrat>()
              .HasOne(i => i.StatutContrat)
              .WithMany(i => i.Contrats)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Crm.Models.Crmdata.Tache>()
              .HasOne(i => i.Contrat)
              .WithMany(i => i.Taches)
              .HasForeignKey(i => i.OpportunityId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Crm.Models.Crmdata.Tache>()
              .HasOne(i => i.TypesTache)
              .WithMany(i => i.Taches)
              .HasForeignKey(i => i.TypeId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Crm.Models.Crmdata.Tache>()
              .HasOne(i => i.StatutTache)
              .WithMany(i => i.Taches)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<Crm.Models.Crmdata.Contrat>()
              .Property(p => p.CloseDate)
              .HasColumnType("datetime");

        builder.Entity<Crm.Models.Crmdata.Tache>()
              .Property(p => p.DueDate)
              .HasColumnType("datetime");

        builder.Entity<Crm.Models.Crmdata.Contact>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.Contrat>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.Contrat>()
              .Property(p => p.Amount)
              .HasPrecision(19, 4);

        builder.Entity<Crm.Models.Crmdata.Contrat>()
              .Property(p => p.ContactId)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.Contrat>()
              .Property(p => p.StatusId)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.StatutContrat>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.StatutTache>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.Tache>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.Tache>()
              .Property(p => p.OpportunityId)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.Tache>()
              .Property(p => p.TypeId)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.Tache>()
              .Property(p => p.StatusId)
              .HasPrecision(10, 0);

        builder.Entity<Crm.Models.Crmdata.TypesTache>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<Crm.Models.Crmdata.Contact> Contacts
    {
      get;
      set;
    }

    public DbSet<Crm.Models.Crmdata.Contrat> Contrats
    {
      get;
      set;
    }

    public DbSet<Crm.Models.Crmdata.StatutContrat> StatutContrats
    {
      get;
      set;
    }

    public DbSet<Crm.Models.Crmdata.StatutTache> StatutTaches
    {
      get;
      set;
    }

    public DbSet<Crm.Models.Crmdata.Tache> Taches
    {
      get;
      set;
    }

    public DbSet<Crm.Models.Crmdata.TypesTache> TypesTaches
    {
      get;
      set;
    }
  }
}
