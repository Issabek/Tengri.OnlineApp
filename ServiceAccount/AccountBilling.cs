using System;
using System.Globalization;

namespace Tengri.ServiceAccount
{
    public class AccountBilling
    {
        public enum BillType
        {
            IN,
            OUT
        }
        public int AccId { get; set; }
        public int UserId { get; set; }
        public int id { get; set; }
        public BillType TransactionType { get; set; }
        public double MoneyDelta { get; set; }
        public DateTime TransactionDate { get; set; } 

        public AccountBilling()
        {
            TransactionDate = DateTime.Now;
        }
        public override string ToString()
        {
            return string.Format("Transaction type: {0} | Transaction date: {1} | Amount: {2}",TransactionType,TransactionDate.ToString("g",
                  CultureInfo.CreateSpecificCulture("en-us")),MoneyDelta);
        }
    }
}
