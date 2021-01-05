using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cy_chat.mysqlDB.BaseModels.Mapping
{
    public class TtOfflinemsglogMap : EntityTypeConfiguration<TtOfflinemsglog>
    {
        public override void Map(EntityTypeBuilder<TtOfflinemsglog> entity)
        {
            entity.HasKey(e => e.OffLineUserId)
                .HasName("PRIMARY");

            entity.ToTable("tt_offlinemsglog");

            entity.Property(e => e.OffLineUserId).HasColumnName("OffLineUserID");

            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasComment("创建时间");

            entity.Property(e => e.Status).HasComment("状态，0：上次离线信息已处理；1：离线信息未处理");

            entity.Property(e => e.UpdateBy).HasComment("更新人，客服ID");

            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasComment("更新时间");

            entity.Property(e => e.UserId)
                .HasColumnName("UserID")
                .HasComment("用户ID");
        }
    }
}
