using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace ClassLibrary
{
    public class User
    {
        public List<UserLibrary> userList = new List<UserLibrary>();

        public User()
        {
        }
        public bool Login(string userIdInput, string passwordInput)                             
        {
            if (userList.Any(x => x.UserId == userIdInput))
            {
                var user = userList.FirstOrDefault((x => x.UserId == userIdInput));
                if (user.Password == passwordInput)
                {
                    Console.WriteLine("Du är nu inloggad.");
                    return true;
                }
                else
                {
                    Console.WriteLine("lösenordet stämmer inte, försök igen.");
                }
            }
            else
            {
                Console.WriteLine("user id:t du matade in existerade inte, försök igen.");
            }
            return false;
        }

        public void Edituser(string inputUserId)
        {
            string breakCommand = "";

            if (userList.Any(x => x.UserId == inputUserId))
            {
                var editUser = userList.FirstOrDefault((x => x.UserId == inputUserId));
                if (editUser == null)
                {
                    Console.WriteLine("vilken information vill du redigera ?");
                    return;
                }
                while (breakCommand != "b")
                {
                    Console.WriteLine("vilken information vill du redigera?");
                    Console.WriteLine("Tryck 1 namn");
                    Console.WriteLine("Tryck 2 för lösenord");
                    Console.WriteLine("Tryck 3 för adress ändring");
                    Console.WriteLine("Tryck b för att logga ut och avsluta.");

                    string editinfo = Console.ReadLine();

                    if (editinfo == "b")
                    {
                        breakCommand = editinfo;
                        continue;
                    }

                    if (editinfo == "1")
                    {
                        Console.WriteLine("skriv in ditt nya namn");
                        string newName = Console.ReadLine();
                        editUser.Name = newName;
                        continue;
                    }
                    if (editinfo == "2")
                    {
                        Console.WriteLine("skriv in ditt nya lösenord");
                        string newPassword = Console.ReadLine();
                        editUser.Password = newPassword;
                        continue;
                    }
                    if (editinfo == "3")
                    {
                        Console.WriteLine("Skriv in din nya adress");
                        string newAdress = Console.ReadLine();
                        editUser.Adress = newAdress;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Det du matade in var inte giltigt.");
                        continue;
                    }
                }
                UserFile.ExportCsv(userList, "List_of_employees");
            }
        }
    }
}
