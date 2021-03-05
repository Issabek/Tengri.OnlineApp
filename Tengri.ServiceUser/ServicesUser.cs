using System;
using Tengri.DAL;
using System.Linq;
using System.Collections.Generic;
namespace Tengri.ServiceUser
{
    public class ServicesUser
    {
        private DAL.LiteDbEntity db = null;
        public ServicesUser(string connectionString)
        {
            db = new DAL.LiteDbEntity(connectionString);
        }
        public void showUsers()
        {
            foreach(User user in db.getCollection<User>())
            {
                Console.WriteLine(user.fullname+" "+user.userData.userIin);
            }
        }
        //1.User registration
        public bool userRegistration(User user)
        {
            string tempStr = null;
            if (user.userData == null)
            {
                return false;
            }
            else
            {
                if(!db.userCreate<User>(user,out tempStr))
                {
                    throw new Exception(tempStr);
                }
                else
                {
                    user.fullname = user.userData.lastName + " " + user.userData.lastName;
                    //user.id = user.userData.DocNumber;
                    user.createdate = DateTime.Now;
                    //user.status temp user status
                    return true;
                }
            }
        }

        //2.Does this user exist
        public User userDoesExist(string Iin)
        {
            List<User> myUsers = db.getCollection<User>();
            return myUsers.Where(w => w.userData.userIin == Iin ).FirstOrDefault();
        }
        //3.User authorization
        public bool userAuthentication(string Iin, string password)
        {
            if (userDoesExist(Iin) != null && userDoesExist(Iin).password==password)
            {
                return true;
            }
            else 
                return false;
        }

        //4.Password change
        public bool changeUserPassword(string Iin, string newPassword, string oldPassword)
        {
            User user = userDoesExist(Iin);
            if (userAuthentication(Iin, oldPassword))
            {
                user.password = newPassword;
                return true;
            }
            return false;
        }
        //5.Password vosst

        //5.User block
        //public accStatusUpdate(int statusId)
        //{
            
        //}
    }
}
