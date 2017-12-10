﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_API_Final.Models
{
	public partial class DBForumContext : DbContext
	{
		public static string ConnectionString { get; internal set; }
		public virtual DbSet<Auth> Auth { get; set; }
		public virtual DbSet<Posts> Posts { get; set; }
		public virtual DbSet<Threads> Threads { get; set; }
		public virtual DbSet<Users> Users { get; set; }

		public DBForumContext(DbContextOptions<DBForumContext> options) : base(options)  { }

		//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//        {
		//            if (!optionsBuilder.IsConfigured)
		//            {
		//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
		//                optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=DBForum;Integrated Security=False;User Id=dev;Password=dev;MultipleActiveResultSets=True;Connection Timeout=1200");
		//            }
		//        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Auth>(entity =>
			{
				entity.HasKey(e => e.Email);

				entity.Property(e => e.Email)
					.HasColumnName("email")
					.HasMaxLength(50)
					.ValueGeneratedNever();

				entity.Property(e => e.Password)
					.IsRequired()
					.HasColumnName("password")
					.HasMaxLength(50);

				entity.Property(e => e.UserId).HasColumnName("user_id");

				entity.HasOne(d => d.User)
					.WithMany(p => p.Auth)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Auth_Users");
			});

			modelBuilder.Entity<Posts>(entity =>
			{
				entity.HasKey(e => e.PostId);

				entity.Property(e => e.PostId)
					.HasColumnName("post_id")
					.ValueGeneratedNever();

				entity.Property(e => e.Message)
					.IsRequired()
					.HasColumnName("message");

				entity.Property(e => e.PostedOn)
					.HasColumnName("posted_on")
					.HasColumnType("datetime");

				entity.Property(e => e.ThreadId).HasColumnName("thread_id");

				entity.Property(e => e.UserId).HasColumnName("user_id");

				entity.HasOne(d => d.Thread)
					.WithMany(p => p.Posts)
					.HasForeignKey(d => d.ThreadId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Posts_Threads");

				entity.HasOne(d => d.User)
					.WithMany(p => p.Posts)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Posts_Users");
			});

			modelBuilder.Entity<Threads>(entity =>
			{
				entity.HasKey(e => e.ThreadId);

				entity.Property(e => e.ThreadId)
					.HasColumnName("thread_id")
					.ValueGeneratedNever();

				entity.Property(e => e.PostedOn)
					.HasColumnName("posted_on")
					.HasColumnType("datetime");

				entity.Property(e => e.Subject)
					.IsRequired()
					.HasColumnName("subject")
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.UserId).HasColumnName("user_id");

				entity.HasOne(d => d.User)
					.WithMany(p => p.Threads)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Threads_Users");
			});

			modelBuilder.Entity<Users>(entity =>
			{
				entity.HasKey(e => e.UserId);

				entity.Property(e => e.UserId)
					.HasColumnName("user_id")
					.ValueGeneratedNever();

				entity.Property(e => e.DateCreated)
					.HasColumnName("date_created")
					.HasColumnType("datetime");

				entity.Property(e => e.FirstName)
					.IsRequired()
					.HasColumnName("first_name")
					.HasMaxLength(50);

				entity.Property(e => e.LastName)
					.IsRequired()
					.HasColumnName("last_name")
					.HasMaxLength(50);
			});
		}
	}
}
