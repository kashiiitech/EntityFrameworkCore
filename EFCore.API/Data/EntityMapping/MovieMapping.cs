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
        
        // mapping relationship
        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasPrincipalKey(genre => genre.Id)
            .HasForeignKey(movie => movie.MainGenreId);
        
        
        
        
        
        // Seed - Data that needs to created always
        builder.HasData(new Movie
        {
            Identifier = 1,
            Title = "Fight Club",
            ReleaseDate = new DateTime(1999, 9, 10),
            Synopsis = "Ed Norton and Brad Pitt have a couple of fist fights with each other.",
            MainGenreId = 1
        });
    }
}