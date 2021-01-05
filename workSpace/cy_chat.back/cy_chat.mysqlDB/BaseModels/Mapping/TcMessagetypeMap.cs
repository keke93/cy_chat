using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cy_chat.mysqlDB.BaseModels.Mapping
{
    public class TcMessagetypeMap : EntityTypeConfiguration<TcMessagetype>
    {
        public override void Map(EntityTypeBuilder<TcMessagetype> entity)
        {
            entity.HasKey(e => e.MessageTypeId)
                .HasName("PRIMARY");

            entity.ToTable("tc_messagetype");

            entity.Property(e => e.MessageTypeId).HasColumnName("MessageTypeID");

            entity.Property(e => e.MessageTypeName)
                .HasColumnType("varchar(20)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_bin");
        }
    }
}
