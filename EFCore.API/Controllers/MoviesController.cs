using EFCore.API.Data;
using EFCore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{
    private readonly MoviesContext _context;
    public MoviesController(MoviesContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Movies.ToListAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        // Querris database, returns first match, null if not found
        // var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        // Similar to FirstOrDefault, but throws if more than one match is found.
        // var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
        
        // Serves match from memory if already fetched, otherwise queries DB.
        // var movie = await _context.Movies.FindAsync(id);

        var movie = _context.Movies
            .Include(movie => movie.Genre)
            .SingleOrDefault(movie => movie.Identifier == id);
        
        return movie != null ? Ok(movie) : NotFound();
    }

    [HttpGet("until-age/{ageRating}")]
    [ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUntilAge([FromRoute] AgeRating ageRating)
    {
        var filteredTitles = await _context.Movies
            .Where(movie => movie.AgeRating <= ageRating)
            .Select(movie => new MovieTitle{ Id = movie.Identifier, Title = movie.Title})
            .ToListAsync();
        
        return Ok(filteredTitles);
    }

    [HttpGet("by-year/{year:int}")]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByYear([FromRoute] int year)
    {
        
        var filteredTitles = await _context.Movies
            .Where(m => m.ReleaseDate.Year == year)
            .Select(m => new MovieTitle{ Id = m.Identifier, Title = m.Title })
            .ToListAsync();
        
        // List<MovieTitle> filteredTitles = new();
        //
        // foreach (var movie in filteredMovies)
        // {
        //     filteredTitles.Add(new MovieTitle{ Id = movie.Id, Title = movie.Title });
        // }
        
        return Ok(filteredTitles);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        
        // movie has no ID
        await _context.SaveChangesAsync();
        // movie has an ID
        
        return CreatedAtAction(nameof(Get), new { id = movie.Identifier }, movie);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Movie movie)
    {
        var existingMovie = await _context.Movies.FindAsync(id);

        if (existingMovie == null)
        {
            return NotFound();
        }
        // we have movie we are going to update it
        existingMovie.Title = movie.Title;
        existingMovie.ReleaseDate = movie.ReleaseDate;
        existingMovie.Synopsis = movie.Synopsis;
        await _context.SaveChangesAsync();
        
        return Ok(existingMovie);
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var existingMovie = await _context.Movies.FindAsync(id);

        if (existingMovie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(existingMovie);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}