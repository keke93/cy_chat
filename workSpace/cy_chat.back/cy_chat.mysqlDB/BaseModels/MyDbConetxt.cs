using System;
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
            modelBuilder.Entity<TmUser>(entity =>
            {
                entity.HasKey(e => e.IUserId)
                    .HasName("PRIMARY");

                entity.ToTable("tm_user");

                entity.Property(e => e.IUserId).HasColumnName("iUserID");

                entity.Property(e => e.SUserName)
                    .HasColumnName("sUserName")
                    .HasColumnType("varchar(50)")
                    .HasComment("用户名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
