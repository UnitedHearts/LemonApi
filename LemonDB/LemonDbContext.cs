using LemonDB;
using Microsoft.EntityFrameworkCore;

namespace LemonDB;

public partial class LemonDbContext : DbContext
{
    private readonly string _connectionString;

    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Cash> Cashs { get; set; }
    public virtual DbSet<Map> Maps { get; set; }
    public virtual DbSet<PlayerSessionStat> PlayersSessionsStats { get; set; }
    public virtual DbSet<Session> Sessions { get; set; }
    public virtual DbSet<Stuff> Stuffs { get; set; }

    public LemonDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public LemonDbContext(DbContextOptions<LemonDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
