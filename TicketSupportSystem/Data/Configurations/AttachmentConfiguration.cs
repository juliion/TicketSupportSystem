﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Data.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.Property(attach => attach.FileName)
                    .IsRequired();
            builder.Property(attach => attach.Path)
                    .IsRequired();
        }
    }
}
