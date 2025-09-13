using Microsoft.EntityFrameworkCore;

namespace ProjectTracker.Models;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options);