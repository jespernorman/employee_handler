using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class UserFile
    {
        public static readonly object userList;

        public UserFile()
        {
        }

        public static void ExportCsv<T>(List<T> userList, string listOfEmployees)
        {
            var sb = new StringBuilder();
            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var basePath = "/Users/jespernorman/Projects/workspace/oop_csharp/employee_handler/CSVDataFile";
            var finalPath = Path.Combine(basePath, listOfEmployees + ".csv");
            var header = "";
            var info = typeof(T).GetProperties();
            if (File.Exists(finalPath))
            {
                File.Delete(finalPath);
            }

            var file = File.Create(finalPath);
            file.Close();

            foreach (var prop in typeof(T).GetProperties())
            {
                header += prop.Name + ";";
            }
            header = header.Substring(0, header.Length - 2);
            sb.AppendLine(header);
            TextWriter swHeader = new StreamWriter(finalPath, true);
            swHeader.Write(sb.ToString());
            swHeader.Close();

            foreach (var obj in userList)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += prop.GetValue(obj, null) + ";";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                TextWriter swData = new StreamWriter(finalPath, true);
                swData.Write(sb.ToString());
                swData.Close();
            }
        }

        public static List<UserLibrary> ImportCsv(string listOfEmployees)
        {
            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var basePath = "/Users/jespernorman/Projects/workspace/oop_csharp/employee_handler/CSVDataFile";
            var finalPath = Path.Combine(basePath, listOfEmployees + ".csv");
            List<UserLibrary> users = null;
            if (File.Exists(finalPath))
            {
                users = File.ReadAllLines(finalPath)
                .Skip(1)
                .Select(v => FromCsv(v))
                .ToList();
            }
            return users;
        }

        public static UserLibrary FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            if (values.Count() > 0)
            {
                UserLibrary user = new UserLibrary();
                user.IsAdmin = Convert.ToBoolean(values[0]);
                user.UserId = Convert.ToString(values[1]);
                user.Name = Convert.ToString(values[2]);
                user.Password = Convert.ToString(values[3]);
                user.Adress = Convert.ToString(values[4]);
                return user;
            }

            return null;
        }
    }
}
