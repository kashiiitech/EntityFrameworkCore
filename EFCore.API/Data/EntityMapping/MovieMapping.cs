using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder) 
    {
        builder
            .ToTable("Pictures")
            .HasKey(movie => movie.Identifier);
        
        // modifying properties movie columns
        builder.Property(movie => movie.Title)
            .IsRequired()
            .HasMaxLength(128)
            .HasColumnType("VARCHAR");
        
        builder.Property(movie => movie.ReleaseDate)
            .HasColumnType("date");
        
        builder.Property(movie => movie.ReleaseDate)
            .HasColumnType("varchar(max)")
            .HasColumnName("Plot");
    }
}