using EFCore.API.Data.ValueGenerators;
using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {

        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CraetedAt")
            // .HasDefaultValueSql("getdate()");
            .HasValueGenerator<CreatedDateTimeGenerator>();
        
        builder.HasData(new Genre{ Id = 1, Name = "Drama" });  
    }
}