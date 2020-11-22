using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class Admin
    {
        public List<UserLibrary> userList = new List<UserLibrary>();

        public Admin()
        {
        }

        public bool Login()
        {
            Console.WriteLine("Skriv in ditt user id.");
            string userIdInput = Console.ReadLine();

            Console.WriteLine("skriv in ditt lösenord.");
            string passwordInput = Console.ReadLine();

            if (userList.Any(x => x.UserId == userIdInput))
            {
                var user = userList.FirstOrDefault((x => x.UserId == userIdInput));
                if (user.Password == passwordInput)
                {
                    if (user.IsAdmin)
                    {
                        Console.WriteLine("Du är nu inloggad");
                        return true;
                    }

                    Console.WriteLine("Du har inte admin behörigheter, prova en annan inlogg.");
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

        public void CreateUser()
        {
            var user = new UserLibrary();

            Console.WriteLine("skriv in ditt anställningsnummer");
            user.UserId = Console.ReadLine();

            Console.WriteLine("skriv in ett namn");
            user.Name = Console.ReadLine();

            Console.WriteLine("skriv in ett lösenord");
            user.Password = Console.ReadLine();

            Console.WriteLine("skriv in din adress");
            user.Adress = Console.ReadLine();

            Console.WriteLine("Ska användaren ha admin behörigheter? (Ja/Nej)");
            string isAdmin = Console.ReadLine();

            user.IsAdmin = isAdmin == "Ja" ? true : false;

            if (user.IsAdmin)
            {
                Console.WriteLine("Du har nu lagt till en admin användare.");
            }
            else
            {
                Console.WriteLine("Du har nu lagt till en vanlig användare.");
            }

            userList.Add(user);
            UserFile.ExportCsv(userList, "List_of_employees");
        }

        public void Deleteuser()
        {
            Console.WriteLine("Skriv in user id på användaren du vill radera");
            string deleteUserId = Console.ReadLine();

            if (userList.Any(x => x.UserId == deleteUserId))
            {
                var usertodelete = userList.FirstOrDefault((x => x.UserId == deleteUserId));

                userList.Remove(usertodelete);

                Console.WriteLine("Användaren" + usertodelete.Name + " är nu raderad.");
            }
            else
            {
                Console.WriteLine("Namnet du matade in existerade inte.");
            }

            UserFile.ExportCsv(userList, "List_of_employees");
        }

        public void ListUsers()
        {
            foreach (var user in userList)
            {
                var hasAdmin = user.IsAdmin ? "Ja" : "Nej";
                Console.WriteLine("Användar id:" + user.UserId + " Namn:" + user.Name + " lösenord:" + user.Password + " adress:" + user.Adress + " användaren har admin behörighet:" + hasAdmin);
            }
        }

        public void Edituser()
        {
            string breakCommand = "";
            var user = new UserLibrary();
            Console.WriteLine("vilken användare vill du redigera? mata in användar id");
            string inputUserId = Console.ReadLine();

            if (userList.Any(x => x.UserId == inputUserId))
            {
                var userToEdit = userList.FirstOrDefault((x => x.UserId == inputUserId));
                if (userToEdit == null)
                {
                    Console.WriteLine("Användaren du valde existerade inte.");
                    return;
                }
                Console.WriteLine("Du valde användaren " + userToEdit.Name);

                while (breakCommand != "b")
                {
                    Console.WriteLine("vilken information vill du redigera?");
                    Console.WriteLine("Tryck 1 namn");
                    Console.WriteLine("Tryck 2 för lösenord");
                    Console.WriteLine("Tryck 3 för adress ändring");
                    Console.WriteLine("Tryck 4 för ändra en användare till en admin användare");
                    Console.WriteLine("Tryck b för att gå tillbaka till ursprungliga menyn");

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
                        userToEdit.Name = newName;
                        continue;
                    }
                    if (editinfo == "2")
                    {
                        Console.WriteLine("skriv in ditt nya lösenord");
                        string newPassword = Console.ReadLine();
                        userToEdit.Password = newPassword;
                        continue;
                    }
                    if (editinfo == "3")
                    {
                        Console.WriteLine("Skriv in din nya adress");
                        string newAdress = Console.ReadLine();
                        userToEdit.Adress = newAdress;
                        continue;
                    }
                    if (editinfo == "4")
                    {
                        Console.WriteLine("Ändra användaren till admin användare? skriv Ja isåfall");
                        string newAdmin = Console.ReadLine();
                        newAdmin = user.IsAdmin ? "Ja" : "Nej";
                        userToEdit.IsAdmin = true;
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
            Console.WriteLine("Programmet stängs nu av hejdå! :)");
        }
    }
}
