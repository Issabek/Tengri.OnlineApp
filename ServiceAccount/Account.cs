using System;
namespace ServiceAccount
{
    public class Account
    {
        public int userID { get; set; }
        public int id { get; set; }
        public string IBAN { get; set; }
        public object MyProperty { get; set; }
        public DateTime CreateDate { get; set; }
        public double balance { get; set; }
        public int status { get; set; }
        public Account()
        {
        }
    }
}
