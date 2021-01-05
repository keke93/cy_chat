using cy_chat.mysqlDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cy_chat.mysqlDB.BaseModels.Mapping
{
    public class TmUserMap : EntityTypeConfiguration<TmUser>
    {
        public override void Map(EntityTypeBuilder<TmUser> entity)
        {
            entity.HasKey(e => e.IUserId)
                .HasName("PRIMARY");

            entity.ToTable("tm_user");

            entity.Property(e => e.IUserId).HasColumnName("iUserID");

            entity.Property(e => e.DtCreateTime)
                .HasColumnName("dtCreateTime")
                .HasColumnType("datetime");

            entity.Property(e => e.DtUpdateTime)
                .HasColumnName("dtUpdateTime")
                .HasColumnType("datetime");

            entity.Property(e => e.ICreateBy).HasColumnName("iCreateBy");

            entity.Property(e => e.IStatus).HasColumnName("iStatus");

            entity.Property(e => e.IUpdateBy).HasColumnName("iUpdateBy");

            entity.Property(e => e.SUserAccount)
                .IsRequired()
                .HasColumnName("sUserAccount")
                .HasColumnType("varchar(50)")
                .HasComment("用户账户")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_bin");

        }
    }
}
