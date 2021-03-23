using System;
using Tengri.ServiceUser;
using log4net;
using log4net.Config;
using ServiceAccount;
using System.Collections.Generic;
using Tengri.ServiceAccount;
using System.Threading;

namespace Tengri.OnlineApp
{
    public class ServiceMenu
    {
        private static ILog log = LogManager.GetLogger("LOGGER");
        public static void tengriUI()
        {
            ServicesUser service = new ServicesUser(@"tengriBank.db");
            Console.Clear();
            string Iin = null;
            string password = null;
            string tempStr = null;
            int tempInt = 0;
            char yesNo = ' ';
            Random rnd = new Random();
            User user = null;
            Console.WriteLine("Welcome to tengri bank!");
            Console.WriteLine("Enter your IIN");
            Iin = Console.ReadLine();
            Console.WriteLine("Enter your password");
            password = Console.ReadLine();
            Console.Clear();
            if (service.userAuthentication(Iin, password) && service.userDoesExist(Iin).status == 1)
            {
                user = service.userDoesExist(Iin);
                Console.WriteLine("Welcome {0}", service.userDoesExist(Iin).firstName);
                Console.WriteLine("================ MENU =================");
                Console.WriteLine("1. Change password");
                Console.WriteLine("2. Block your account");
                Console.WriteLine("3. Leave account");
                Console.WriteLine("4. Open/Create bank account");
                Console.WriteLine("5. Show user accounts");

                SettingsAccount accService = new SettingsAccount(@"tengriBank.db");
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
                        service.userBlock(user);
                        break;
                    case 3:
                        tengriUI();
                        break;
                    case 4:
                        Console.Clear();
                        List<Account> userAccs = accService.GetUserAccounts(user.id);
                        if (userAccs.Count > 0 && userAccs!=null)
                        {
                            Console.WriteLine("Хотите создать новый счет?");
                            yesNo = char.Parse(Console.ReadLine());
                            if (yesNo == 'y')
                            {
                                Console.Clear();
                                Account tempAccount = new Account();
                                accService.CreateAccount(user.id, out tempAccount);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Данный пользователь не имеет банковских счетов, хотите создать новый?");
                            yesNo = char.Parse(Console.ReadLine());
                            if (yesNo == 'y')
                            {
                                try
                                {
                                    Console.Clear();
                                    Account tempAccount = new Account();
                                    accService.CreateAccount(user.id, out tempAccount);
                                    Console.WriteLine("Новый счет успешно создан!");
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine("Произошла ошибка! Новый счет не создан");
                                    log.Error(ex.Message);
                                }
                            }
                        }
                        break;
                    case 5:
                        foreach (var account in accService.GetUserAccounts(user.id))
                        {
                            //Console.WriteLine("{0}. {1} --- {2}TENGE --- STATUS ID {3} ", listCounter++, account.IBAN, account.balance, account.status);
                            Console.WriteLine(account.ToString());
                        }
                        Console.WriteLine("1. Пополнить счет\n2. Cнять деньги со счета\n3. Показать историю счета");
                        yesNo =char.Parse(Console.ReadLine());
                        if (yesNo == '1')
                        {
                            Console.WriteLine("Введите номер счета для пополнения  :");
                            tempInt = Int32.Parse(Console.ReadLine());
                            if(tempInt>accService.GetUserAccounts(user.id).Count)
                                throw new Exception("Произошла ошибка! Аккаунт не создан или не существует!");
                            Account tempAcc = accService.GetUserAccounts(user.id)[tempInt - 1];
                            if (tempAcc != null)
                            {
                                Console.Clear();
                                accService.AddMoney(user.id, tempAcc);
                            }
                            else
                            {
                                throw new Exception("Произошла ошибка! АккауHт не создан или не существует!");
                            }
                        }
                        else if (yesNo == '2')
                        {
                            Console.WriteLine("Введите номер счета для обналичивания денег  :");
                            tempInt = Int32.Parse(Console.ReadLine());
                            Account tempAcc = accService.GetUserAccounts(user.id)[tempInt-1];
                            if (tempAcc != null)
                            {
                                Console.Clear();
                                accService.CashOut(user.id, tempAcc);
                            }
                            else
                            {
                                throw new Exception("Произошла ошибка! АккауHт не создан или не существует!");
                            }
                        }
                        else if (yesNo == '3')
                        {
                            Console.WriteLine("Введите номер счета: ");
                            tempInt = Int32.Parse(Console.ReadLine());
                            Account tempAcc = accService.GetUserAccounts(user.id)[tempInt - 1];
                            if (tempAcc != null)
                            {
                                Console.Clear();
                                tempAcc.ToString();
                                List<AccountBilling> templist = accService.Transactions(tempAcc);
                                foreach(AccountBilling bill in templist)
                                {
                                    Console.WriteLine("\n\n\t{0}",bill.ToString());

                                }
                                Console.ReadKey();
                            }
                            else
                            {
                                throw new Exception("Произошла ошибка! АккауHт не создан или не существует!");
                            }
                        }
                            break;
                    default:
                        break;
                }
                Console.Clear();
                tengriUI();
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
                        try {
                            Console.WriteLine("Введите ИИН и фамилию через пробел, для того чтобы разблокировать пользователя!");
                            tempStr = Console.ReadLine();
                            User tempUser = service.userDoesExist(tempStr.Split(' ')[0]);
                            if (tempUser.lastName == (tempStr.Split(' ')[1]).ToUpper()) //Сравниваем данные имени и иина
                            {
                                Console.Clear();
                                Console.WriteLine("Данные верны!\nВведите новый пароль:");
                                password = Console.ReadLine();
                                if (service.changeUserPassword(tempUser.userIin, password, tempUser.password))
                                {
                                    tempUser = service.userDoesExist(tempStr.Split(' ')[0]);
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
                        catch
                        {
                            log.Error("Неверный иин");
                            Console.WriteLine("Вы ввели неверный ИИН!");
                            Thread.Sleep(3000);
                            Console.Clear();
                            Console.WriteLine("Перенаправляю в главное меню");
                            Thread.Sleep(2000);
                            tengriUI();

                        }
                    }
                }
                else
                {
                    Console.WriteLine("You have entered wrong password, in order to change it press Y, otherwise N");
                    //service.userDoesExist(Iin).wrongpasscounter++;
                    //service.u
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
                        service.showUsers();
                        Console.WriteLine("Перехожу в главное меню!\nНажмите Enter");
                        Console.ReadKey();
                        tengriUI();
                    }


                }
                
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
