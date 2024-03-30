using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using real_time_db.Models;

namespace real_time_db.Db
{
    public class FooContext : DbContext
    {
        public DbSet<Foo> Foos {get; set;}
        private readonly string _dbPath;

        public FooContext() {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = System.IO.Path.Join(path, "app.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSqlite($"Data Source={_dbPath}");
        }
    }
}