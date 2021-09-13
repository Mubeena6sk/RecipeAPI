using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RecipeAPI.Models
{
    public partial class RecipedbContext : DbContext
    {
        public RecipedbContext()
        {
        }

        public RecipedbContext(DbContextOptions<RecipedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<IngredientsIndex> IngredientsIndices { get; set; }
        public virtual DbSet<MyRecipe> MyRecipes { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Table> Tables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Recipedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.Iid)
                    .HasName("PK__Ingredie__C4972BACA350F32E");

                entity.Property(e => e.Iid).HasColumnName("IId");

                entity.Property(e => e.Iname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IName");
            });

            modelBuilder.Entity<IngredientsIndex>(entity =>
            {
                entity.ToTable("Ingredients_Index");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("_ID");

                entity.Property(e => e.Iid).HasColumnName("IId");

                entity.Property(e => e.Rid).HasColumnName("RId");

                entity.HasOne(d => d.IidNavigation)
                    .WithMany(p => p.IngredientsIndices)
                    .HasForeignKey(d => d.Iid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredients__IId__398D8EEE");

                entity.HasOne(d => d.RidNavigation)
                    .WithMany(p => p.IngredientsIndices)
                    .HasForeignKey(d => d.Rid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredients__RId__38996AB5");
            });

            modelBuilder.Entity<MyRecipe>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RId).HasColumnName("RId");

            
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.Rid)
                    .HasName("PK__Recipes__CAFF40D239B2371E");

                entity.Property(e => e.Rid).HasColumnName("RId");

                entity.Property(e => e.Instructions)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Rname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("RName");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.ToTable("Table");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
