using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BlazorSozluk.Api.Infrastructure.Persistance.Context;

namespace BlazorSozluk.Api.Infrastructure.Persistance.EntityConfigurations.Entry
{
    public class EntryEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Entry>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
        {
            base.Configure(builder);

            builder.ToTable("entry", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.CreatedBy)
                .WithMany(i => i.Entries)
                .HasForeignKey(i => i.CreatedById);
        }

    }
}
