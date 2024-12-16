namespace Database
{
    public partial class Signal
    {
        public Signal()
        {
            SignalFragments = new HashSet<SignalFragment>();
        }

        public Guid Id { get; set; }
        public string SignalFilePath { get; set; } = null!;
        public Guid CorrelatorId { get; set; }

        public virtual Correlator Correlator { get; set; } = null!;
        public virtual ICollection<SignalFragment> SignalFragments { get; set; }
    }
}
