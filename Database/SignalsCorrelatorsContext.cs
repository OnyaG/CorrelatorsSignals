using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class SignalsCorrelatorsContext : DbContext
    {
        public SignalsCorrelatorsContext() => Database.EnsureCreated();

        public SignalsCorrelatorsContext(DbContextOptions<SignalsCorrelatorsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CopyTask> CopyTasks { get; set; } = null!;
        public virtual DbSet<CorrelationTask> CorrelationTasks { get; set; } = null!;
        public virtual DbSet<Correlator> Correlators { get; set; } = null!;
        public virtual DbSet<Signal> Signals { get; set; } = null!;
        public virtual DbSet<SignalFragment> SignalFragments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5433;Database=SignalsCorrelators;Username=postgres;Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CopyTask>(entity =>
            {
                entity.ToTable("CopyTask");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.CorrelatorReceiver)
                    .WithMany(p => p.CopyTaskCorrelatorReceivers)
                    .HasForeignKey(d => d.CorrelatorReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CorrelatorReceiverId");

                entity.HasOne(d => d.CorrelatorSender)
                    .WithMany(p => p.CopyTaskCorrelatorSenders)
                    .HasForeignKey(d => d.CorrelatorSenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CorrelatorSenderId");

                entity.HasOne(d => d.SignalFragment)
                    .WithMany(p => p.CopyTasks)
                    .HasForeignKey(d => d.SignalFragmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SignalFragmentId");
            });

            modelBuilder.Entity<CorrelationTask>(entity =>
            {
                entity.ToTable("CorrelationTask");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Correlator)
                    .WithMany(p => p.CorrelationTasks)
                    .HasForeignKey(d => d.CorrelatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CorrelatorId");

                entity.HasOne(d => d.SignalFragment)
                    .WithMany(p => p.CorrelationTasks)
                    .HasForeignKey(d => d.SignalFragmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SignalFragmentId");
            });

            modelBuilder.Entity<Correlator>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Signal>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Correlator)
                    .WithMany(p => p.Signals)
                    .HasForeignKey(d => d.CorrelatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CorrelatorId");
            });

            modelBuilder.Entity<SignalFragment>(entity =>
            {
                entity.ToTable("SignalFragment");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Signal)
                    .WithMany(p => p.SignalFragments)
                    .HasForeignKey(d => d.SignalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SignalId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
