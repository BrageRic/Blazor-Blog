using ServerBlazor.Models;
using System.Collections.Generic;
using ServerBlazor.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ServerBlazor.Data
{
    // Tatt utgangspunkt i Oblig2 innlevering
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Tag> Tag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Blog Seeding
            modelBuilder.Entity<Blog>().ToTable("Blog");
            modelBuilder.Entity<Blog>().HasData(new Blog
            { BlogId = 1, Name = "Fisking" });
            modelBuilder.Entity<Blog>().HasData(new Blog
            { BlogId = 2, Name = "Klatring" });

            // Post Seeding
            modelBuilder.Entity<Post>().ToTable("Post")
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .UsingEntity<PostTag>();
            modelBuilder.Entity<Post>().HasData(new Post
            { PostId = 1, Title = "Fisking i Straumen", Content = "Kort historie om fisking i Straumen", BlogId = 1 });
            modelBuilder.Entity<Post>().HasData(new Post
            { PostId = 2, Title = "Fiskekort til salgs", Content = "Kontakt meg for mer info", BlogId = 1 });
            modelBuilder.Entity<Post>().HasData(new Post
            { PostId = 3, Title = "Klatre Konkurranse i Narvik", Content = "Arrangeres Sommeren 2048", BlogId = 2 });

            // Comment Seeding
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Comment>().HasData(new Comment
            { CommentId = 1, CommentText = "Så kult, tenker jeg også må ta turen dit!", PostId = 1 });
            modelBuilder.Entity<Comment>().HasData(new Comment
            { CommentId = 2, CommentText = "Hvordan var parkeringen ved elva?",  PostId = 1 });
            modelBuilder.Entity<Comment>().HasData(new Comment
            { CommentId = 3, CommentText = "Har sendt direktemelding",  PostId = 2 });
            modelBuilder.Entity<Comment>().HasData(new Comment
            { CommentId = 4, CommentText = "Klarer ikke vente!", PostId = 3 });
            modelBuilder.Entity<Comment>().HasData(new Comment
            { CommentId = 5, CommentText = "Det gledes!", PostId = 3 });

            // Tag Seeding
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Tag>().HasData(new Tag
            { TagId = 1, TagText = "Fiske" });
            modelBuilder.Entity<Tag>().HasData(new Tag
            { TagId = 2, TagText = "Klatring" });

            // PostTag Seeding
            modelBuilder.Entity<PostTag>().HasData(new PostTag
            { PostId = 1, TagId = 1 });
            modelBuilder.Entity<PostTag>().HasData(new PostTag
            { PostId = 2, TagId = 1 });
            modelBuilder.Entity<PostTag>().HasData(new PostTag
            { PostId = 3, TagId = 2 });

            // User Seeding
            var hasher = new PasswordHasher<IdentityUser>();
            
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                UserName = "bot",
                NormalizedUserName = "BOT",
                PasswordHash = hasher.HashPassword(null, "b0tSittPass0rd!")
            });
        }
    }


}