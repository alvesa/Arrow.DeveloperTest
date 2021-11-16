using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Types;
using System;
using System.Configuration;

namespace Arrow.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;

        public PaymentService(IAccountDataStore accountDataStore)
        {
            _accountDataStore = accountDataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            try
            {
                // Lookup the account the payment is being made from.
                Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

                if (account == null)
                    throw new Exception("The account does not exists.");

                // Check that the account is in a valid state to make the payment
                if (!account.IsValid())
                    throw new Exception("The account is not valid to make this payment.");

                switch (request.PaymentScheme)
                {
                    case PaymentScheme.Bacs:
                        if (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                            result.Success = true;
                        break;

                    case PaymentScheme.FasterPayments:
                        if(account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && account.Balance > request.Amount)
                            result.Success = true;
                        break;

                    case PaymentScheme.Chaps:
                        if (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && account.Status == AccountStatus.Live)
                            result.Success = true;
                        break;
                }

                if (result.Success)
                {
                    // Deduct the payment amount from the account's balance and update the account in the database
                    account.Balance -= request.Amount;

                    _accountDataStore.UpdateAccount(account);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
