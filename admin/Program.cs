using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassLibrary;

namespace AdminApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string exitCommand = "";
            var admin = new Admin();
            var importedList = UserFile.ImportCsv("List_of_employees");
            
            if(importedList != null)
            {
                admin.userList = importedList;
            }

            var successfullLogin = false;
            while(!successfullLogin)
            {
                if(admin.Login())
                {
                    successfullLogin = true;
                    continue;
                }
            }
            
            Console.WriteLine("Här kan du som admin skapa och ta bort användare.");
            
            while (exitCommand != "x")
            {
                Console.WriteLine("Tryck 1 för att skapa en användare.");
                Console.WriteLine("Tryck 2 för att ta bort en användare.");
                Console.WriteLine("Tryck 3 för att lista alla användare i systemet.");
                Console.WriteLine("Tryck 4 för att editera en användares information");
                Console.WriteLine("Tryck x för att logga ut.");

                var inmatning1 = Console.ReadLine();

                if (inmatning1 == "x")
                {
                    exitCommand = "x";
                    continue;
                }

                if (inmatning1 == "1")
                {
                    admin.CreateUser();
                    continue;
                }
                else if (inmatning1 == "2")
                {
                    admin.Deleteuser();
                    continue;
                }
                else if (inmatning1 == "3")
                {
                    admin.ListUsers();
                    continue;
                }
                else if (inmatning1 == "4")
                {
                    admin.Edituser();
                    continue;
                }
                else
                {
                    Console.WriteLine("Det du matade in var inte giltigt pröva igen.");
                    continue;
                }
            }
            Console.WriteLine("Nu loggas du ut och programmet stängs av.");
        }
    }
}
