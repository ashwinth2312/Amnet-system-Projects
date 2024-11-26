using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    public interface IShortUrlService
    {
        Task<string> CreateShortUrlAsync(string longUrl);
        Task<string> GetLongUrlByShortCodeAsync(string shortCode);
    }

    public class ShortUrlService : IShortUrlService
    {
        private readonly string _baseUrl;
        private readonly UrlShortenerDbContext _dbContext;

        public ShortUrlService(IConfiguration configuration, UrlShortenerDbContext dbContext)
        {
            _baseUrl = configuration["BaseUrl"];
            _dbContext = dbContext;
        }

        public async Task<string> CreateShortUrlAsync(string longUrl)
        {
            var shortCode = GenerateShortCode();
            var shortUrl = new ShortUrl { LongUrl = longUrl, ShortCode = shortCode };
            _dbContext.ShortUrls.Add(shortUrl);
            await _dbContext.SaveChangesAsync();
            return $"{_baseUrl}/{shortCode}";
        }

        public async Task<string> GetLongUrlByShortCodeAsync(string shortCode)
        {
            var shortUrl = await _dbContext.ShortUrls.FirstOrDefaultAsync(u => u.ShortCode == shortCode);
            return shortUrl?.LongUrl;
        }

        private string GenerateShortCode()
        {
            // Implement a robust short code generation algorithm,
            // such as using a base62 encoding or a custom hashing function.
            // Here's a simple example using Base64:
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 6);
        }
    }

    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options) { }

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;

        public ShortUrlController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] string longUrl)
        {
            if (string.IsNullOrEmpty(longUrl))
            {
                return BadRequest("Please provide a long URL");
            }

            var shortUrl = await _shortUrlService.CreateShortUrlAsync(longUrl);
            return Ok(shortUrl);
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToLongUrl(string shortCode)
        {
            var longUrl = await _shortUrlService.GetLongUrlByShortCodeAsync(shortCode);
            if (longUrl == null)
            {
                return NotFound();
            }

            return Redirect(longUrl);
        }
    }
}