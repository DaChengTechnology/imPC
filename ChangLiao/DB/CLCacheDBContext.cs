using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.DB
{
    class CLCacheDBContext: DbContext
    {
        public CLCacheDBContext() : base("TempDB")
        {

        }
        public CLCacheDBContext(DbConnection con) : base(con, true) { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<CLCacheDBContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
        public DbSet<LocalTempModel> localTemps { get; set; }
    }
}
