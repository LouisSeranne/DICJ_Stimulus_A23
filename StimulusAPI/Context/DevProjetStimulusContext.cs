using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StimulusAPI.Models;

namespace StimulusAPI.Context
{
    public partial class DevProjetStimulusContext : DbContext
    {
        public DevProjetStimulusContext()
        {
        }

        public DevProjetStimulusContext(DbContextOptions<DevProjetStimulusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Code> Codes { get; set; } = null!;
        public virtual DbSet<Cour> Cours { get; set; } = null!;
        public virtual DbSet<Etudiant> Etudiants { get; set; } = null!;
        public virtual DbSet<Exercice> Exercices { get; set; } = null!;
        public virtual DbSet<FichierSauvegarde> FichierSauvegardes { get; set; } = null!;
        public virtual DbSet<FichierSource> FichierSources { get; set; } = null!;
        public virtual DbSet<Graphe> Graphes { get; set; } = null!;
        public virtual DbSet<GrapheView> GrapheViews { get; set; } = null!;
        public virtual DbSet<Groupe> Groupes { get; set; } = null!;
        public virtual DbSet<HoverView> HoverViews { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<LienUtile> LienUtiles { get; set; } = null!;
        public virtual DbSet<Noeud> Noeuds { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<PageComposant> PageComposants { get; set; } = null!;
        public virtual DbSet<Professeur> Professeurs { get; set; } = null!;
        public virtual DbSet<Progression> Progressions { get; set; } = null!;
        public virtual DbSet<Reference> References { get; set; } = null!;
        public virtual DbSet<StatusGraphe> StatusGraphes { get; set; } = null!;
        public virtual DbSet<TexteFormater> TexteFormaters { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=dicjwin01;Initial Catalog=DevProjetStimulus;User ID=P2022-Dev;Password=9jj96wqwoFYSj6Dxw26w;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Code>(entity =>
            {
                entity.ToTable("code");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contenu).HasColumnName("contenu");

                entity.Property(e => e.Titre)
                    .HasMaxLength(20)
                    .HasColumnName("titre");
            });

            modelBuilder.Entity<Cour>(entity =>
            {
                entity.ToTable("cours");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodeCours)
                    .HasMaxLength(10)
                    .HasColumnName("code_cours");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Etudiant>(entity =>
            {
                entity.HasKey(e => e.CodeDa)
                    .HasName("PK__etudiant__9A4862BDCE8C19B2");

                entity.ToTable("etudiant");

                entity.Property(e => e.CodeDa)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("code_da")
                    .IsFixedLength();

                entity.Property(e => e.MotDePasse)
                    .HasMaxLength(50)
                    .HasColumnName("mot_de_passe");

                entity.Property(e => e.Nom)
                    .HasMaxLength(20)
                    .HasColumnName("nom");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(20)
                    .HasColumnName("prenom");
            });

            modelBuilder.Entity<Exercice>(entity =>
            {
                entity.ToTable("exercice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Solution).HasColumnName("solution");
            });

            modelBuilder.Entity<FichierSauvegarde>(entity =>
            {
                entity.ToTable("fichier_sauvegarde");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contenu)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("contenu");

                entity.Property(e => e.ExerciceId).HasColumnName("exercice_id");

                entity.Property(e => e.FichierEtudiantDa)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("fichier_etudiant_da")
                    .IsFixedLength();

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");

                entity.Property(e => e.ProgressionPageId).HasColumnName("progression_page_id");

                entity.Property(e => e.Version)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("version");

                entity.HasOne(d => d.Exercice)
                    .WithMany(p => p.FichierSauvegardes)
                    .HasForeignKey(d => d.ExerciceId)
                    .HasConstraintName("FK__fichier_s__exerc__1E5A75C5");

                entity.HasOne(d => d.Progression)
                    .WithMany(p => p.FichierSauvegardes)
                    .HasForeignKey(d => new { d.ProgressionPageId, d.FichierEtudiantDa })
                    .HasConstraintName("FK_fichier_sauvegarder");
            });

            modelBuilder.Entity<FichierSource>(entity =>
            {
                entity.ToTable("fichier_source");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contenu).HasColumnName("contenu");

                entity.Property(e => e.ExerciceId).HasColumnName("exercice_id");

                entity.Property(e => e.Nom).HasColumnName("nom");

                entity.HasOne(d => d.Exercice)
                    .WithMany(p => p.FichierSources)
                    .HasForeignKey(d => d.ExerciceId)
                    .HasConstraintName("FK__fichier_s__exerc__7FD5EEA5");
            });

            modelBuilder.Entity<Graphe>(entity =>
            {
                entity.ToTable("graphe");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Couleur)
                    .HasMaxLength(15)
                    .HasColumnName("couleur");

                entity.Property(e => e.GroupeId).HasColumnName("groupe_id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(55)
                    .HasColumnName("nom");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("status_code")
                    .IsFixedLength();

                entity.HasOne(d => d.Groupe)
                    .WithMany(p => p.Graphes)
                    .HasForeignKey(d => d.GroupeId)
                    .HasConstraintName("FK__graphe__groupe_i__30AE302A");

                entity.HasOne(d => d.StatusCodeNavigation)
                    .WithMany(p => p.Graphes)
                    .HasForeignKey(d => d.StatusCode)
                    .HasConstraintName("FK__graphe__status_c__31A25463");
            });

            modelBuilder.Entity<GrapheView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("graphe_view");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.Disponibilite)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("disponibilite");

                entity.Property(e => e.GrapheId).HasColumnName("graphe_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LiaisonPrincipal).HasColumnName("liaison_principal");
            });

            modelBuilder.Entity<Groupe>(entity =>
            {
                entity.ToTable("groupe");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoursId).HasColumnName("cours_id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(10)
                    .HasColumnName("nom");

                entity.Property(e => e.ProfesseurId).HasColumnName("professeur_id");

                entity.HasOne(d => d.Cours)
                    .WithMany(p => p.Groupes)
                    .HasForeignKey(d => d.CoursId)
                    .HasConstraintName("FK__groupe__cours_id__2BE97B0D");

                entity.HasOne(d => d.Professeur)
                    .WithMany(p => p.Groupes)
                    .HasForeignKey(d => d.ProfesseurId)
                    .HasConstraintName("FK__groupe__professe__2CDD9F46");

                entity.HasMany(d => d.EtudiantDa)
                    .WithMany(p => p.Groupes)
                    .UsingEntity<Dictionary<string, object>>(
                        "GroupeEtudiant",
                        l => l.HasOne<Etudiant>().WithMany().HasForeignKey("EtudiantDa").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__groupe_et__etudi__17AD7836"),
                        r => r.HasOne<Groupe>().WithMany().HasForeignKey("GroupeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__groupe_et__group__15C52FC4"),
                        j =>
                        {
                            j.HasKey("GroupeId", "EtudiantDa");

                            j.ToTable("groupe_etudiant");

                            j.IndexerProperty<int>("GroupeId").HasColumnName("groupe_id");

                            j.IndexerProperty<string>("EtudiantDa").HasMaxLength(9).IsUnicode(false).HasColumnName("etudiant_da").IsFixedLength();
                        });
            });

            modelBuilder.Entity<HoverView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("hover_view");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.CodeParent)
                    .HasMaxLength(10)
                    .HasColumnName("code_parent");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Disponibilite)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("disponibilite");

                entity.Property(e => e.GrapheId).HasColumnName("graphe_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PtsTotal).HasColumnName("pts_total");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Largeur).HasColumnName("largeur");

                entity.Property(e => e.Longeur).HasColumnName("longeur");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            modelBuilder.Entity<LienUtile>(entity =>
            {
                entity.ToTable("lien_utile");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GrapheId).HasColumnName("graphe_id");

                entity.Property(e => e.NomUrl)
                    .HasMaxLength(256)
                    .HasColumnName("nom_url");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.Graphe)
                    .WithMany(p => p.LienUtiles)
                    .HasForeignKey(d => d.GrapheId)
                    .HasConstraintName("FK__lien_util__graph__347EC10E");
            });

            modelBuilder.Entity<Noeud>(entity =>
            {
                entity.ToTable("noeud");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Disponibilite)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("disponibilite");

                entity.Property(e => e.GrapheId).HasColumnName("graphe_id");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .HasColumnName("image");

                entity.Property(e => e.LiaisonPrincipal).HasColumnName("liaison_principal");

                entity.Property(e => e.Obligatoire).HasColumnName("obligatoire");

                entity.Property(e => e.Pts).HasColumnName("pts");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Graphe)
                    .WithMany(p => p.Noeuds)
                    .HasForeignKey(d => d.GrapheId)
                    .HasConstraintName("FK__noeud__graphe_id__3B2BBE9D");

                entity.HasOne(d => d.LiaisonPrincipalNavigation)
                    .WithMany(p => p.InverseLiaisonPrincipalNavigation)
                    .HasForeignKey(d => d.LiaisonPrincipal)
                    .HasConstraintName("FK__noeud__liaison_p__3C1FE2D6");

                entity.HasMany(d => d.NoeudEnfants)
                    .WithMany(p => p.NoeudParents)
                    .UsingEntity<Dictionary<string, object>>(
                        "LiaisonSecondaire",
                        l => l.HasOne<Noeud>().WithMany().HasForeignKey("NoeudEnfant").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__liaison_s__noeud__5E74FADA"),
                        r => r.HasOne<Noeud>().WithMany().HasForeignKey("NoeudParent").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__liaison_s__noeud__5C8CB268"),
                        j =>
                        {
                            j.HasKey("NoeudParent", "NoeudEnfant");

                            j.ToTable("liaison_secondaire");

                            j.IndexerProperty<int>("NoeudParent").HasColumnName("noeud_parent");

                            j.IndexerProperty<int>("NoeudEnfant").HasColumnName("noeud_enfant");
                        });

                entity.HasMany(d => d.NoeudParents)
                    .WithMany(p => p.NoeudEnfants)
                    .UsingEntity<Dictionary<string, object>>(
                        "LiaisonSecondaire",
                        l => l.HasOne<Noeud>().WithMany().HasForeignKey("NoeudParent").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__liaison_s__noeud__5C8CB268"),
                        r => r.HasOne<Noeud>().WithMany().HasForeignKey("NoeudEnfant").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__liaison_s__noeud__5E74FADA"),
                        j =>
                        {
                            j.HasKey("NoeudParent", "NoeudEnfant");

                            j.ToTable("liaison_secondaire");

                            j.IndexerProperty<int>("NoeudParent").HasColumnName("noeud_parent");

                            j.IndexerProperty<int>("NoeudEnfant").HasColumnName("noeud_enfant");
                        });
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("page");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NoeudId).HasColumnName("noeud_id");

                entity.Property(e => e.Ordre).HasColumnName("ordre");

                entity.Property(e => e.Pts).HasColumnName("pts");

                entity.HasOne(d => d.Noeud)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.NoeudId)
                    .HasConstraintName("FK__page__noeud_id__3FF073BA");
            });

            modelBuilder.Entity<PageComposant>(entity =>
            {
                entity.ToTable("page_composant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdReference).HasColumnName("id_reference");

                entity.Property(e => e.Ordre).HasColumnName("ordre");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.Property(e => e.TypeComposant)
                    .HasMaxLength(20)
                    .HasColumnName("type_composant");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageComposants)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("FK__page_comp__page___43C1049E");
            });

            modelBuilder.Entity<Professeur>(entity =>
            {
                entity.ToTable("professeur");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MotDePasse)
                    .HasMaxLength(50)
                    .HasColumnName("mot_de_passe");

                entity.Property(e => e.Nom)
                    .HasMaxLength(20)
                    .HasColumnName("nom");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(20)
                    .HasColumnName("prenom");
            });

            modelBuilder.Entity<Progression>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.EtudiantDa });

                entity.ToTable("progression");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.Property(e => e.EtudiantDa)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("etudiant_da")
                    .IsFixedLength();

                entity.Property(e => e.Pts).HasColumnName("pts");

                entity.HasOne(d => d.EtudiantDaNavigation)
                    .WithMany(p => p.Progressions)
                    .HasForeignKey(d => d.EtudiantDa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__progressi__etudi__520F23F5");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Progressions)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__progressi__page___511AFFBC");
            });

            modelBuilder.Entity<Reference>(entity =>
            {
                entity.ToTable("reference");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            modelBuilder.Entity<StatusGraphe>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__status_g__357D4CF8B4C3344F");

                entity.ToTable("status_graphe");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("code")
                    .IsFixedLength();

                entity.Property(e => e.Nom)
                    .HasMaxLength(20)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<TexteFormater>(entity =>
            {
                entity.ToTable("texte_formater");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contenu).HasColumnName("contenu");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("video");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Largeur).HasColumnName("largeur");

                entity.Property(e => e.Longeur).HasColumnName("longeur");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
