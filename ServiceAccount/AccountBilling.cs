using System;
namespace Tengri.ServiceAccount
{
    public class AccountBilling
    {
        public enum BillType
        {
            IN,
            OUT
        }
        public BillType TransactionType { get; set; }
        public double MoneyDelta { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public AccountBilling(BillType BillDirection, double Moneydelta)
        {
        }
    }
}
