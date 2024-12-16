namespace Database
{
    public partial class SignalFragment
    {
        public SignalFragment()
        {
            CopyTasks = new HashSet<CopyTask>();
            CorrelationTasks = new HashSet<CorrelationTask>();
        }

        public Guid Id { get; set; }
        public Guid SignalId { get; set; }
        public int FragmentStartAddress { get; set; }
        public int FragmentSize { get; set; }

        public virtual Signal Signal { get; set; } = null!;
        public virtual ICollection<CopyTask> CopyTasks { get; set; }
        public virtual ICollection<CorrelationTask> CorrelationTasks { get; set; }
    }
}
