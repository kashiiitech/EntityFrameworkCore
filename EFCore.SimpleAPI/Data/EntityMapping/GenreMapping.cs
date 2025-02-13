using EFCore.SimpleAPI.Data.ValueGenerators;
using EFCore.SimpleAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.SimpleAPI.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();

        // builder.Property(genre => genre.Name)
        //     .HasColumnType("varchar(max)");   // this mapping is not compatable
    }
}