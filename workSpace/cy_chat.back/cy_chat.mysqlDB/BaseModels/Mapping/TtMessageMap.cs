using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cy_chat.mysqlDB.BaseModels.Mapping
{
    public class TtMessageMap : EntityTypeConfiguration<TtMessage>
    {
        public override void Map(EntityTypeBuilder<TtMessage> entity)
        {

            entity.HasKey(e => e.MessageId)
                .HasName("PRIMARY");

            entity.ToTable("tt_message");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");

            entity.Property(e => e.CreateTime)
                        .HasColumnType("datetime")
                        .HasComment("消息创建时间（服务器时间）");

            entity.Property(e => e.MessageContent)
                        .HasColumnType("varchar(2000)")
                        .HasComment("消息内容")
                        .HasCharSet("utf8mb4")
                        .HasCollation("utf8mb4_bin");

            entity.Property(e => e.MessageTypeId)
                        .HasColumnName("MessageTypeID")
                        .HasComment("消息类型ID");

            entity.Property(e => e.ReceivedBy).HasComment("消息接收人");

            entity.Property(e => e.SendBy).HasComment("消息发送人");

            entity.Property(e => e.SendStatus).HasComment("0:未发送成功 1：发送成功");

            entity.Property(e => e.SendType).HasComment("0:客户发送 1：客服发送");

        }

    }
}
