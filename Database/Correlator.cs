namespace Database
{
    public partial class Correlator
    {
        public Correlator()
        {
            CopyTaskCorrelatorReceivers = new HashSet<CopyTask>();
            CopyTaskCorrelatorSenders = new HashSet<CopyTask>();
            CorrelationTasks = new HashSet<CorrelationTask>();
            Signals = new HashSet<Signal>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public float CopySpeed { get; set; } = 0;
        public float ComputingSpeed { get; set; } =0;
        public string IpAddress { get; set; } = string.Empty;

        public virtual ICollection<CopyTask> CopyTaskCorrelatorReceivers { get; set; }
        public virtual ICollection<CopyTask> CopyTaskCorrelatorSenders { get; set; }
        public virtual ICollection<CorrelationTask> CorrelationTasks { get; set; }
        public virtual ICollection<Signal> Signals { get; set; }
    }
}
