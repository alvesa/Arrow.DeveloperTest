namespace Arrow.DeveloperTest.Types
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }

        public bool IsValid()
        {
            if (this.Status != AccountStatus.Live)
                return false;

            return true;
        }

    }
}
