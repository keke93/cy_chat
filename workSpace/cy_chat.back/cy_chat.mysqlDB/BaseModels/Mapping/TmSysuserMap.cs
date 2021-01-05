using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cy_chat.mysqlDB.BaseModels.Mapping
{
    public class TmSysuserMap : EntityTypeConfiguration<TmSysuser>
    {
        public override void Map(EntityTypeBuilder<TmSysuser> entity)
        {
            entity.HasKey(e => e.SysUserId)
                .HasName("PRIMARY");

            entity.ToTable("tm_sysuser");

            entity.Property(e => e.SysUserId).HasColumnName("SysUserID");

            entity.Property(e => e.CreateTime).HasColumnType("datetime");

            entity.Property(e => e.SysPassword)
                .HasColumnType("varchar(200)")
                .HasComment("密码")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_bin");

            entity.Property(e => e.SysUserName)
                .HasColumnType("varchar(50)")
                .HasComment("用户名称")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_bin");

            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        }
    }
}
