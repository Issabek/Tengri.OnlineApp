using System;
using System.Linq;
using System.Collections.Generic;
using Tengri.DAL;
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
            List<User> tempList = db.getCollection<User>();
            foreach (User user in tempList)
            {
                Console.WriteLine(user.firstName+" "+user.lastName+" "+user.userIin+" "+user.password);
            }
        }
        //1.User registration
        public bool userRegistration(User user)
        {
            string tempStr = null;
            if (user.firstName == null)
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
                    user.fullname = user.firstName + " " + user.lastName;
                    user.id = 100000+db.getCollection<User>().Count();
                    user.createdate = DateTime.Now;
                    db.update<User>(user, out tempStr);
                    return true;
                }
            }
        }

        //2.Does this user exist
        public User userDoesExist(string Iin)
        {
            List<User> myUsers = db.getCollection<User>();
            if (myUsers != null) 
                return myUsers.Where(w => w.userIin == Iin ).FirstOrDefault();
            return null;
        }
        public bool userDoesExist(int userID)
        {
            List<User> myUsers = db.getCollection<User>();
            return myUsers.Where(w => w.id == userID).Any(); ;
        }

        //3.User authorization
        public bool userAuthentication(string Iin, string password)
        {
            if (userDoesExist(Iin) != null && userDoesExist(Iin).password == password)
            {
                if(userDoesExist(Iin).wrongpasscounter!=3)
                    return true;
                else
                {
                    userBlock(userDoesExist(Iin));
                    Console.WriteLine("Пользователь заблокирован!");
                }
            }

            else if (userDoesExist(Iin) != null && userDoesExist(Iin).password != password)
                userDoesExist(Iin).wrongpasscounter += 1;

            return false;
        }

        //4.Password change
        public bool changeUserPassword(string Iin, string newPassword, string oldPassword)
        {
            string tempStr = null;
            User user = userDoesExist(Iin);
            if (userAuthentication(Iin, oldPassword))
            {

                user.password = newPassword;
                db.update<User>(user, out tempStr);
                return true;
            }
            return false;
        }
        //5.Password restore
        public bool passwordRestore(string Iin)
        {
            Console.WriteLine("Enter your first and last name in order to get your account's password restored:\n Ex: Vasya Pupkin\n");
            string FirstLast = Console.ReadLine();
            string oldPassword = null, newPassword = null;
            if(userDoesExist(Iin).firstName+" " + userDoesExist(Iin).lastName == FirstLast.ToUpper())
            {
                Console.Clear();
                Console.WriteLine("Enter new password!");
                newPassword= Console.ReadLine();
                Console.WriteLine("Enter previous password!");
                oldPassword = Console.ReadLine();
                changeUserPassword(Iin,newPassword,oldPassword);
            }
            return false;
        }

        //5.User block
        public bool userBlock(User user, int defineStatus = 2)
        {
            Console.WriteLine(defineStatus==2?"Пользователь {0} заблокирован":"Пользователь {0} разбокирован", user.fullname);
            user.status = defineStatus;
            string errMsg = null;
            db.update(user,out errMsg);
            Console.ReadKey();
            return true;
        }
    }
}
