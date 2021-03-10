using System;
using System.Collections.Generic;
using System.Linq;
using Tengri.ServiceUser;

namespace ServiceAccount
{
    public class SettingsAccount
    {
        private Tengri.DAL.LiteDbEntity db = null;
        private ServicesUser service = null;
        public SettingsAccount(string connectionString)
        {
            db = new Tengri.DAL.LiteDbEntity(connectionString);
            service = new ServicesUser(connectionString);
        }
        /// <summary>
        /// Метод возвращает список счетоп пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Account> GetUserAccounts(int userId)
        {
            List<Account> accounts = new List<Account>();
            accounts = db.getCollection<Account>();
            if (accounts != null)
            {
                return accounts.Where(w => w.userID == userId).ToList();
            }
            return accounts.ToList();
        }
        public bool CreateAccount(int userId, out Account account)
        {
            account = new Account();
            if (userId <= 0)
                throw new Exception("Неверные данные userID");
            else if (service.userDoesExist(userId))
            {
                Random rnd = new Random();
                string tempStr = null;
                User user = service.showSingleUser(userId);
                account.userID = user.id;
                account.status = user.status;
                account.IBAN = "KZ" + rnd.Next(1000000, 9999999);
                if(db.userCreate(account, out tempStr))
                {
                    Console.WriteLine("Счет для пользователя {0} успешно создан", service.showSingleUser(userId).firstName);
                    return true;
                }
                else
                {
                    throw new Exception(tempStr);
                }
            }
            return false;
        }
    }
    
}
