using System;
using System.Collections.Generic;
using ClassLibrary;

namespace UserApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var user = new User();
            var importedList = UserFile.ImportCsv("List_of_employees");

            if (importedList != null)
            {
                user.userList = importedList;
            }

            Console.WriteLine("Skriv in ditt user id.");
            string userIdInput = Console.ReadLine();

            Console.WriteLine("skriv in ditt lösenord.");
            string passwordInput = Console.ReadLine();

            var successfullLogin = false;
            while (!successfullLogin)
            {
                if (user.Login(userIdInput,passwordInput))
                {
                    successfullLogin = true;
                    continue;
                }
            }

            Console.WriteLine("Som användare kan du endast redigera din egen information");
            
            user.Edituser(userIdInput);
        }
    }
}
