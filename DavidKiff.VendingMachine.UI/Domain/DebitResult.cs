namespace DavidKiff.VendingMachine.UI.Domain
{
    internal sealed class DebitResult
    {
        public DebitResult(bool isDebited, string debitFailedReason)
        {
            this.IsDebited = isDebited;
            this.DebitFailedReason = debitFailedReason;
        }

        public bool IsDebited { get; private set; }

        public string DebitFailedReason { get; private set; }
    }
}