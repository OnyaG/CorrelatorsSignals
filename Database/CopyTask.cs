namespace Database
{
    public partial class CopyTask
    {
        public Guid Id { get; set; }
        public Guid CorrelatorSenderId { get; set; }
        public Guid SignalFragmentId { get; set; }
        public float? AverageComputingSpeed { get; set; }
        public Guid CorrelatorReceiverId { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeStop { get; set; }

        public virtual Correlator CorrelatorReceiver { get; set; } = null!;
        public virtual Correlator CorrelatorSender { get; set; } = null!;
        public virtual SignalFragment SignalFragment { get; set; } = null!;
    }
}
