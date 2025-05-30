using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = Domain.Entities;

namespace Data
{
    public class GuessConfiguration : IEntityTypeConfiguration<Entities.Guess>
    {
        public void Configure(EntityTypeBuilder<Entities.Guess> builder)
        {
            builder.HasKey(e => e.Id);
            builder.OwnsOne(e => e.DocumentId)
                .Property(e => e.IdNumber);
            builder.OwnsOne(e => e.DocumentId)
                .Property(e => e.DocumentType);
        }
    }
}
