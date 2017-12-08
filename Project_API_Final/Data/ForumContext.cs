using Microsoft.EntityFrameworkCore;
using Project_API_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_API_Final.Data
{
    public class ForumContext: DbContext
	{
	
			public ForumContext(DbContextOptions<ForumContext> options) : base(options)
			{
			}

			public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("Users");
		}

	}
	}

