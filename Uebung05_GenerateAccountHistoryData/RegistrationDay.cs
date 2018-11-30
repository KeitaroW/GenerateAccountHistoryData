using Bogus;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace Uebung05_GenerateAccountHistoryData
{

    class User
    {
        int userId;

        Gender gender;

        string firstName;

        string lastName;

        string loginname;

        string password;

        public User(int userId)
        {
            this.UserId = userId;
        }

        public Gender Gender { get => gender; set => gender = value; }
        public int UserId { get => userId; set => userId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Loginname { get => loginname; set => loginname = value; }
        public string Password { get => password; set => password = value; }

        public override string ToString()
        {
            return UserId + ", " + Gender + ", " + FirstName + ", " + LastName + ", " + Loginname + ", " + Password;
        }
    }

    class RegistrationDay
    {
        int rows;
        int index;
        DateTime registrationDate;
        Random rnd;
        string path;

        public RegistrationDay(int rows, int index, DateTime registrationDate, Random rnd)
        {
            this.rows = rows;
            this.index = index;
            this.registrationDate = registrationDate;
            this.rnd = rnd;
        }

        public RegistrationDay(int rows, int index, DateTime registrationDate, Random rnd, string path)
        {
            this.rows = rows;
            this.index = index;
            this.registrationDate = registrationDate;
            this.path = path;
            this.rnd = rnd;
        }

        public void GenerateAccountData()
        {
            #if DEBUG
            Account accMin = new Account(registrationDate, rnd);
            Account accMax = new Account(registrationDate, rnd);
            double min = Double.MaxValue;
            double max = Double.MinValue;
            #endif
            List<Account> accounts = new List<Account>();
            Account temp;
            registrationDate = new DateTime(registrationDate.Year, registrationDate.Month, registrationDate.Day, 0, 0, 0);
            StreamWriter file; 
            if (path != null)
            {
                file = new StreamWriter(Path.Combine(path, "accounts_" + registrationDate.ToString("yyyyMMdd") + ".txt"));
            } else
            {
                file = new StreamWriter("accounts_" + registrationDate.ToString("yyyyMMdd") + ".txt");
            }
            file.Write("Id\tLoginname\tPassword\tRegistrationDate\tLastLoginDate\tCharactername\tNation\tGeartype\tLevel\tLevelpercentage\tSpi\tCredits\tFame" + 
                "\tBrigade\tAttack\tDefence\tEvasion\tFuel\tSpirit\tShield\tUnusedStatpoints\n");
            for (int i = index * rows; i < (index+1) * rows; i++)
            {
                temp = new Account(registrationDate, rnd);
                var testUsers = new Faker<User>()
                    //Optional: Call for objects that have complex initialization
                    .CustomInstantiator(f => new User(i))
                    //Use an enum outside scope.
                    .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                    //Basic rules using built-in generators
                    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
                    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
                    .RuleFor(u => u.Loginname, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                    .RuleFor(u => u.Password, (f, u) => f.Internet.Password())
                    //Optional: After all rules are applied finish with the following action
                    #if DEBUG
                    .FinishWith((f, u) =>
                    {
                        Console.WriteLine("User Created! Id={0}", u.UserId);
                    })
                    #endif
                    ;
                var user = testUsers.Generate();
                Console.WriteLine(user);
                temp.Loginname = user.Loginname;
                temp.Password = user.Password;
                temp.CharacterName = user.Loginname;
                accounts.Add(temp);
            }
            Console.WriteLine("File: " + (index+1));
            accounts.Sort((x, y) => DateTime.Compare(x.RegistrationDate, y.RegistrationDate));
            int id = index * rows;
            foreach (Account account in accounts)
            {
                account.Id = id;
                #if DEBUG
                if (account.Levelpercentage > max)
                {
                    max = account.Levelpercentage;
                    accMax = account;
                }
                if (account.Levelpercentage < min)
                {
                    min = account.Levelpercentage;
                    accMin = account;
                }
                #endif
                file.Write(account.Id + "\t" + account.Loginname + "\t" + account.Password + "\t" + account.RegistrationDate.ToString("yyyy-MM-dd hh:mm:ss.fff") + "\t" + account.LastLoginDate.ToString(/*"yyyyMMddHHmmss" yyyy-MM-dd hh:mm:ss.fff yyyy-MM-ddTHH:mm:ss*/"yyyy-MM-dd hh:mm:ss.fff") + "\t" + 
                    account.CharacterName + "\t" + account.Nation + "\t" + account.Geartype + "\t" + account.Level + "\t" + account.Levelpercentage.ToString("0.##") + "\t" + account.Spi + 
                    "\t" + account.Credits + "\t" + account.Fame + "\t" + account.Brigade + "\t" + account.Attack + "\t" + account.Defence + "\t" + account.Evasion + "\t" + 
                    account.Fuel + "\t" + account.Spirit + "\t" + account.Shield + "\t" + account.UnusedStatpoints + "\n");
                id++;
            }
            file.Close();
            #if DEBUG
            Console.WriteLine("Id: " + accMin.Id + " Min: " + min.ToString("0.##"));
            Console.WriteLine("Id: " + accMax.Id + " Max: " + max.ToString("0.##"));
            #endif
        }
    }
}
