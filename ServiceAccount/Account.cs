using System;
using System.Collections.Generic;
using Tengri.ServiceAccount;
namespace ServiceAccount
{
    public class Account
    {
        public int userID { get; set; }
        public int id { get; set; }
        public string IBAN { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public double balance { get; set; }
        public int status { get; set; }
        public int AccountTypeID { get; set; }


        public Account() { }

        public override string ToString()
        {
            string AppInfo = string.Format("{0}\n{1}\n{2}\n{3} тенге\n{4}\n\n", new String('=', 20), AccTypeName, IBAN, balance,new String('=',20));
            return AppInfo;

        }
        public string AccName { get; set; }
        public string AccTypeName {
            get
            {
                switch (AccountTypeID)
                {
                    case 1:
                        return "Текущий счет";
                    case 2:
                        return "Кредитный щсчет";
                    case 3:
                        return "Депозитный счет";
                    default:
                        return "Неопределен";
                }
            }
        }
    }
}
