namespace Database
{
    public partial class CorrelationTask
    {
        public Guid Id { get; set; }
        public Guid CorrelatorId { get; set; }
        public Guid SignalFragmentId { get; set; }
        public float AverageComputingSpeed { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeStop { get; set; }

        public virtual Correlator Correlator { get; set; } = null!;
        public virtual SignalFragment SignalFragment { get; set; } = null!;
    }
}
