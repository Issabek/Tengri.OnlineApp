using System;
using System.Linq;
using System.Collections.Generic;
using log4net;
using log4net.Config;
using Tengri.ServiceUser;
using userIin;
namespace Tengri.OnlineApp
{
    class Program
    {
        private static ILog log = LogManager.GetLogger("LOGGER");
        public static void tengriUI()
        {
            ServicesUser service = new ServicesUser(@"bank.db");

            string Iin = null;
            string password = null;
            char yesNo = ' ';
            Random rnd = new Random();
            User user = new User();
            Console.WriteLine("Welcome to tengri bank!");
            Console.WriteLine("Enter your IIN");
            Iin = Console.ReadLine();
            Console.WriteLine("Enter your password");
            password = Console.ReadLine();
            Console.Clear();
            if (service.userAuthentication(Iin, password))
            {
                Console.WriteLine("Welcome {0}", service.userDoesExist(Iin).userData.firstName);
                Console.WriteLine("================ MENU =================");
                Console.WriteLine("1. Change password");
                Console.WriteLine("2. Block your account");
            }
            else
            {
                if(service.userDoesExist(Iin) == null)
                {
                    Console.WriteLine("There is no such user, if you want to register new account press Y, otherwise N");
                    Char.TryParse(Console.ReadLine(), out yesNo);
                    if (yesNo == 'y')
                        userRegistration(service);
                    else
                        Console.WriteLine("Bye!");
                }
                else
                {
                    Console.WriteLine("You have entered wrong password, in order to vosstavit ego press Y, otherwise N");
                    Char.TryParse(Console.ReadLine(), out yesNo);

                }
            }
            service.showUsers();
            Console.ReadKey();
        }
        public static void userRegistration(ServicesUser service)
        {
            string tempStr = null;
            User newUser = new User();
            Console.Clear();
            Console.WriteLine("======Registration=====");
            Console.WriteLine("Enter your IIN");
            tempStr = Console.ReadLine();
            newUser.userData = userParse.getUserData(tempStr);
            if (newUser.userData!=null)
                service.userRegistration(newUser);
            
            else
            {
                Console.WriteLine("There is no such IIN, try again");
                userRegistration(service);
            }

            Console.WriteLine("Enter your password");
            newUser.password = Console.ReadLine();
            Console.WriteLine("Registration is succsessfull!!!");
            Console.ReadKey();
            Console.Clear();
        }


        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            log.Info("Does work");
            tengriUI();


        }
    }
}

