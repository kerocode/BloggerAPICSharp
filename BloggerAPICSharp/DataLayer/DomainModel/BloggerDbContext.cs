using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggerAPICSharp.DataLayer.DomainModel
{
    public class BloggerDbContext :IdentityDbContext<ApplicationUser>
    {
        public BloggerDbContext(DbContextOptions<BloggerDbContext>options):base(options)
        {
        }
		public DbSet<Post> Posts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Topic> Topics { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
			modelBuilder.Entity<Topic>().ToTable("Topic");

			modelBuilder.Entity<Topic>()
                        .HasIndex(t => t.Name)
	                    .IsUnique();
            modelBuilder.Entity<PostTopic>()
                        .HasKey(pt => new { pt.PostId, pt.TopicId });

			modelBuilder.Entity<PostTopic>()
                        .HasOne(pt => pt.Post)
                        .WithMany(p => p.PostTopics)
                        .HasForeignKey(pt => pt.PostId);

			modelBuilder.Entity<PostTopic>()
                        .HasOne(pt => pt.Topic)
                        .WithMany(t => t.PostTopics)
                        .HasForeignKey(pt => pt.TopicId);

		}
    }
}
