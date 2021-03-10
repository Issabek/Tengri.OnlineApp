﻿using System;
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
            return accounts.Where(w => w.userID == userId).ToList();
        }
        //public bool CreateAccount(int userId, out Account account)
        //{
        //    if (userId <= 0)
        //        throw new Exception("Неверные данные userID");
        //    else if (service.userDoesExist(userId))
        //    {

        //    }


        //}
    }
    
}