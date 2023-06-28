using LibaryWebApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibaryWebApi.Data
{
    public partial class SchoolLibraryDbContext : DbContext
    {
        public SchoolLibraryDbContext()
        {
        }

        public SchoolLibraryDbContext(DbContextOptions<SchoolLibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<BorrowInfo> BorrowInfos { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C2074E39E7A5");

                entity.Property(e => e.BookAuthor)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.BookPublisher)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.BookTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BorrowInfo>(entity =>
            {
                entity.HasKey(e => e.BorrowId).HasName("PK__BorrowIn__4295F83FC22FA6C3");

                entity.ToTable("BorrowInfo");

                entity.Property(e => e.BorrowDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("date");

                entity.HasOne(d => d.Book).WithMany(p => p.BorrowInfos)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BorrowInf__BookI__29572725");

                entity.HasOne(d => d.Student).WithMany(p => p.BorrowInfos)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BorrowInf__Stude__286302EC");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B99BE6E1F8A");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
