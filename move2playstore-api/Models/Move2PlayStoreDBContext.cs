using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace move2playstoreAPI.Models
{
    public partial class Move2PlayStoreDBContext : DbContext
    {
        public Move2PlayStoreDBContext()
        {
        }

        public Move2PlayStoreDBContext(DbContextOptions<Move2PlayStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<Purchaseitem> Purchaseitem { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Usergame> Usergame { get; set; }
        public virtual DbSet<Video> Video { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.GameId, e.UserId });

                entity.ToTable("comment");

                entity.HasIndex(e => e.GameId)
                    .HasName("fk_Commentary_Game1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_Commentary_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Recomendation)
                    .HasColumnName("recomendation")
                    .HasColumnType("tinyint(4)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Commentary_Game1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Commentary_User1");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.HasIndex(e => e.DeveloperId)
                    .HasName("fk_Game_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasColumnName("company")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DeveloperId)
                    .HasColumnName("developerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnName("releaseDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.DeveloperId)
                    .HasConstraintName("fk_Game_User1");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.HasIndex(e => e.GameId)
                    .HasName("fk_Image_Game1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Image_Game1");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId });

                entity.ToTable("purchase");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_PurchaseHistory_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaymentMethod)
                    .HasColumnName("paymentMethod")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaymentStatus)
                    .HasColumnName("paymentStatus")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.PaymentStatusMessage)
                    .IsRequired()
                    .HasColumnName("paymentStatusMessage")
                    .HasColumnType("longtext");

                entity.Property(e => e.PaymentToken)
                    .IsRequired()
                    .HasColumnName("paymentToken")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnName("purchaseDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PurchaseHistory_User1");
            });

            modelBuilder.Entity<Purchaseitem>(entity =>
            {
                entity.ToTable("purchaseitem");

                entity.HasIndex(e => e.GameId)
                    .HasName("fk_PurchaseHistory_has_Game_Game1_idx");

                entity.HasIndex(e => new { e.Id, e.PurchaseId })
                    .HasName("fk_PurchaseHistory_has_Game_PurchaseHistory1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.PurchaseId)
                    .HasColumnName("purchaseId")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Purchaseitem)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PurchaseHistory_has_Game_Game1");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.Purchaseitem)
                    .HasForeignKey(d => new { d.Id, d.PurchaseId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PurchaseHistory_has_Game_PurchaseHistory1");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId, e.GameId });

                entity.ToTable("rating");

                entity.HasIndex(e => e.GameId)
                    .HasName("fk_Rating_Game1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_Rating_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rating_Game1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rating_User1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Usergame>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("usergame");

                entity.HasIndex(e => e.PurchaseItemId)
                    .HasName("fk_UserGame_PurchaseItem1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_User_has_Game_User1_idx");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PurchaseItemId)
                    .HasColumnName("purchaseItemId")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.PurchaseItem)
                    .WithMany(p => p.Usergame)
                    .HasForeignKey(d => d.PurchaseItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserGame_PurchaseItem1");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Usergame)
                    .HasForeignKey<Usergame>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_has_Game_User1");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("video");

                entity.HasIndex(e => e.GameId)
                    .HasName("fk_Video_Game1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Video)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Video_Game1");
            });
        }
    }
}
