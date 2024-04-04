using Microsoft.EntityFrameworkCore;


namespace TrainTicket.Models
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options) { }
        public DbSet<UserDetails> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<TrainDetails> Trains { get; set; }
        public DbSet<SeatDetails> Seats { get; set; }
        public DbSet<TicketTable> TicketTables { get; set; }
        public object SeatDetails { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeatDetails>()
                .Property(s => s.SeatId)
                .ValueGeneratedOnAdd();
        }
    }
}
