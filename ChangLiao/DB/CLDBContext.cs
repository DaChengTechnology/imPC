using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SQLite.CodeFirst;
using System.Data.Common;

namespace ChangLiao.DB
{
    class CLDBContext: DbContext
    {
        public CLDBContext():base("CLDB") {}
        public CLDBContext(DbConnection con) : base(con, true) { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<CLDBContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
        public DbSet<FriendModel> friend { get; set; }
        public DbSet<GroupTable> group { get; set; }
        public DbSet<GroupCache> groupCaches { get; set; }
        public DbSet<StrongerModel> strongers { get; set; }
        public DbSet<FaceTable> faces { get; set; }
        public DbSet<GroupUser> groupUsers { get; set; }
        public DbSet<FocusTable> focus { get; set; }
        public DbSet<ChatBK> chatBKs { get; set; }
    }
}
