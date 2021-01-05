using System;
using cy_chat.mysqlDB.BaseModels.Mapping;
using cy_chat.mysqlDB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cy_chat.mysqlDB.BaseModels
{
    public partial class MyDbConetxt :  EntityContextBase<MyDbConetxt>
    {
        public MyDbConetxt(DbContextOptions<MyDbConetxt> options)
            : base(options)
        {
        }

        public virtual DbSet<TmUser> TmUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddConfiguration(new TmUserMap());
            modelBuilder.AddConfiguration(new TtMessageMap());
            modelBuilder.AddConfiguration(new TcMessagetypeMap());
            modelBuilder.AddConfiguration(new TmSysuserMap());
            modelBuilder.AddConfiguration(new TtOfflinemsglogMap());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
