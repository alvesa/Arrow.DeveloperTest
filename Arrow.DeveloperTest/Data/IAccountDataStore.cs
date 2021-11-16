using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arrow.DeveloperTest.Data
{
    public interface IAccountDataStore
    {
        Account GetAccount(string accountNumber);

        void UpdateAccount(Account account);
    }
}
