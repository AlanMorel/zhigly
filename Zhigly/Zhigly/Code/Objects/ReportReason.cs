namespace Zhigly.Code.Objects
{
    public class ReportReason
    {
        private static readonly ReportReason[] Reasons = {
            new ReportReason(0, "This listing seems fake or sketchy."),
            new ReportReason(1, "This listing contains hate speech."),
            new ReportReason(2, "This listing promotes illegal activities."),
            new ReportReason(3, "This listing contains violating images.")
        };

        public int Id { get; set; }
        public string Reason { get; set; }

        public ReportReason(int Id, string Reason)
        {
            this.Id = Id;
            this.Reason = Reason;
        }

        public static ReportReason[] Get()
        {
            return Reasons;
        }
    }
}