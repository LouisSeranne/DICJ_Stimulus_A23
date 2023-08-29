using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StimulusAPI.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StimulusAPI.LoginContext
{
    public partial class _2022_Projet_StimulusLoginContext : IdentityDbContext<UtilisateurApplication>
    {
        public _2022_Projet_StimulusLoginContext()
        {
        }

        public _2022_Projet_StimulusLoginContext(DbContextOptions<_2022_Projet_StimulusLoginContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=dicjwin01;Initial Catalog=2022_Projet_Stimulus-Login;User ID=P2022-Dev;Password=9jj96wqwoFYSj6Dxw26w;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
