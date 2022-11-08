﻿using Microsoft.EntityFrameworkCore;
using PterodactylPavlovServerDomain.Models;

namespace PterodactylPavlovServerController.Contexts
{
    public class PavlovServerContext : DbContext
    {
        private readonly IConfiguration configuration;

        public PavlovServerContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = configuration.GetConnectionString("PavlovServers")!;
            switch (configuration["db_type"])
            {
                case "sqlserver":
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case "mysql":
                    optionsBuilder.UseMySQL(connectionString);
                    break;
                case "sqlite":
                    optionsBuilder.UseSqlite(connectionString);
                    break;
                default:
                    throw new Exception("Invalid database type provided. Valid DB types are: sqlserver, mysql, sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerListPlayerModel>().HasKey(p => new { p.UniqueId, p.ServerId });
        }

        public DbSet<PlayerListPlayerModel> Players { get; set; }
    }
}
