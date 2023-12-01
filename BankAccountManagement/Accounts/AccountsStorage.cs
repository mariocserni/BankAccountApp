using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Accounts
{
    internal class AccountsStorage
    {
        private string directory = @"C:\C#\BankAccountManagement\";
        private string fileName = "accounts.txt";

        public void CheckExistingFile()
        {
            string path = $"{directory}{fileName}";
            if (!File.Exists(path))
            {
                using FileStream fs = File.Create(path);
            }
        }

        public List<Account> LoadAccounts() 
        {
            List<Account> accounts = new List<Account>();

            string path = $"{directory}{fileName}";
            try
            {
                CheckExistingFile();

                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    //string result = line.Remove(line.Length - 1);
                    string[] parts = line.Split(';');
                    string username = GetValue(parts, "username");
                    string name = GetValue(parts, "name");
                    string password = GetValue(parts, "password");
                    string email = GetValue(parts, "email");
                    string age = GetValue(parts, "age");
                    int ageNumber = int.Parse(age);
                    string money = GetValue(parts, "money");
                    int moneyNumber = int.Parse(money);

                    Account account = new Account(username, name, password, email, ageNumber, moneyNumber);
                    accounts.Add(account);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Something went wrong while opening the file! {ex}");
            }
            return accounts;
        }

        public void SaveAccount(List<Account> accounts)
        {
            StringBuilder sb = new StringBuilder();
            string path = $"{directory}{fileName}";
            foreach(Account account in accounts) 
            {
                sb.Append("username:").Append(account.Username).Append(";");
                sb.Append("name:").Append(account.Name).Append(";");
                sb.Append("password:").Append(account.Password).Append(";");
                sb.Append("email:").Append(account.Email).Append(";");
                sb.Append("age:").Append(account.Age).Append(";");
                sb.Append("money:").Append(account.Money).Append(";\n");
                File.WriteAllText(path, sb.ToString());
            }

        }

        public string GetValue(string[] parts, string key)
        {
            foreach (string part in parts)
            {
                string[] keyValue = part.Split(':');
                if (keyValue.Length == 2 && keyValue[0] == key)
                {
                    return keyValue[1];
                }
            }
            return null;
        }
    }
}
