using System;
using Tengri.ServiceUser;
using log4net;
using log4net.Config;

namespace Tengri.OnlineApp
{
    public class ServiceMenu
    {
        private static ILog log = LogManager.GetLogger("LOGGER");
        public static void tengriUI()
        {
            ServicesUser service = new ServicesUser(@"myNewBank2.db");
            Console.Clear();
            string Iin = null;
            string password = null;
            string tempStr = null;
            char yesNo = ' ';
            Random rnd = new Random();
            User user = new User();
            Console.WriteLine("Welcome to tengri bank!");
            Console.WriteLine("Enter your IIN");
            Iin = Console.ReadLine();
            Console.WriteLine("Enter your password");
            password = Console.ReadLine();
            Console.Clear();
            if (service.userAuthentication(Iin, password) && service.userDoesExist(Iin).status == 1)
            {
                Console.WriteLine("Welcome {0}", service.userDoesExist(Iin).firstName);
                Console.WriteLine("================ MENU =================");
                Console.WriteLine("1. Change password");
                Console.WriteLine("2. Block your account");
                Console.WriteLine("3. Leave account");
                tempStr = Console.ReadLine();
                switch (Int32.Parse(tempStr))
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Enter new password!");
                        tempStr = Console.ReadLine();
                        Console.WriteLine("Enter previous password!");
                        password = Console.ReadLine();
                        service.changeUserPassword(Iin, tempStr, password);
                        break;
                    case 2:
                        service.userBlock(service.userDoesExist(Iin));
                        break;
                    case 3:
                        tengriUI();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (service.userDoesExist(Iin) == null)
                {
                    Console.WriteLine("There is no such user, if you want to register new account press Y, otherwise N");
                    Char.TryParse(Console.ReadLine(), out yesNo);
                    if (yesNo == 'y')
                        userRegistrationUI(service);
                    else
                        Console.WriteLine("Bye!");
                }
                else if (service.userDoesExist(Iin).status == 2)
                {
                    Console.WriteLine("Данный пользователь заблокирован, хотите ли Вы его разблокировать?");
                    Char.TryParse(Console.ReadLine(), out yesNo);
                    if (yesNo == 'y')
                    {
                        Console.WriteLine("Введите ИИН и фамилию через пробел, для того чтобы разблокировать пользователя!");
                        tempStr = Console.ReadLine();
                        User tempUser = service.userDoesExist(tempStr.Split(' ')[0]);
                        if (tempUser.lastName == (tempStr.Split(' ')[1]).ToUpper()) //Сравниваем данные имени и иина
                        {
                            Console.Clear();
                            Console.WriteLine("Данные верны!\nВведите новый пароль:");
                            password = Console.ReadLine();
                            if(service.changeUserPassword(tempUser.userIin, password, tempUser.password))
                            {
                                service.userBlock(tempUser, 1);
                                Console.WriteLine("Пароль успешно изменен! Аккаунт разблокирован!");
                                tengriUI();
                            }
                            else
                            {
                                Console.WriteLine("Что-то пошло не так!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Такого пользователя не существует либо данные введены неправильно!");

                        }
                    }
                    else
                    {
                        Console.WriteLine("You have entered wrong password, in order to change it press Y, otherwise N");
                        Char.TryParse(Console.ReadLine(), out yesNo);
                        if (yesNo == 'y')
                        {
                            Console.Clear();
                            Console.WriteLine("Enter new password!");
                            tempStr = Console.ReadLine();
                            Console.WriteLine("Enter previous password!");
                            password = Console.ReadLine();
                            service.changeUserPassword(Iin, tempStr, password);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Перехожу в главное меню!\nНажмите Enter");
                            Console.ReadKey();
                            tengriUI();
                        }


                    }
                }
                service.showUsers();
                Console.ReadKey();
            }
        }
            public static void userRegistrationUI(ServicesUser service)
        {
            try
            {
                string tempStr = null;
                Console.Clear();
                Console.WriteLine("======Registration=====");
                Console.WriteLine("Enter your IIN");
                tempStr = Console.ReadLine();
                if (tempStr != null && tempStr.Length == 12)
                {
                    if (service.userDoesExist(tempStr) == null)
                    {
                        User newUser = userParse.GetUserData(tempStr);
                        Console.WriteLine("Enter your password");
                        newUser.password = Console.ReadLine();
                        service.userRegistration(newUser);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("User with such IIN already exists, try again");
                        Console.ReadKey();
                        userRegistrationUI(service);
                    }
                }
                else
                {
                    Console.WriteLine("There is no such IIN, try again");
                    Console.ReadKey();
                    userRegistrationUI(service);
                }
                Console.WriteLine("Registration is succsessfull!!!");
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

    }
}
