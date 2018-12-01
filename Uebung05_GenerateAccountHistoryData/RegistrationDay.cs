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

    class ChooseRandom
    {
        bool lastLogin;
        bool characterName;
        bool levelpercentage;

        public ChooseRandom(Random rnd)
        {
            if (rnd.NextDouble() < 0.5d)
            {
                LastLogin = true;
                while (!characterName && !levelpercentage)
                {
                    if (rnd.NextDouble() < 0.001d)
                    {
                        CharacterName = true;
                    }
                    if (rnd.NextDouble() < 0.95d)
                    {
                        Levelpercentage = true;
                    }
                }
            }
        }

        public bool LastLogin { get => lastLogin; set => lastLogin = value; }
        public bool CharacterName { get => characterName; set => characterName = value; }
        public bool Levelpercentage { get => levelpercentage; set => levelpercentage = value; }
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

        public List<Account> GenerateAccountData(List<Account> accounts)
        {
            List<Account> changedData = new List<Account>();
            List<Account> newAccounts = new List<Account>();
            #if DEBUG
            Account accMin = new Account(registrationDate, rnd);
            Account accMax = new Account(registrationDate, rnd);
            double min = Double.MaxValue;
            double max = Double.MinValue;
            #endif
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
                newAccounts.Add(temp);
            }
            //random choice whether an account should be changed or not
            ChooseRandom random;
            Account newAccount;
            foreach (Account account in accounts)
            {
                random = new ChooseRandom(rnd);
                if (rnd.NextDouble() < 0.001d)
                {
                    var testUsers = new Faker<User>()
                    //Optional: Call for objects that have complex initialization
                        .CustomInstantiator(f => new User(account.Id))
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
                    newAccount = account;
                    if (random.LastLogin)
                    {
                        if (random.CharacterName)
                        {
                            newAccount.Loginname = user.Loginname;
                        }
                        if (random.Levelpercentage)
                        {
                            double maxLevelPerDay = account.GetMaxLevelPerDay();
                            if (maxLevelPerDay >= 1.0d)
                            {
                                int level = rnd.Next(0, (int)maxLevelPerDay);
                                newAccount.Level = account.Level + level;
                                maxLevelPerDay -= (int)maxLevelPerDay;
                            }
                            newAccount.Levelpercentage = account.Levelpercentage + rnd.NextDouble() * (maxLevelPerDay * 100);
                            if (newAccount.Levelpercentage > 99.99d)
                            {
                                newAccount.Level++;
                                newAccount.Levelpercentage -= 99.99d;
                            }
                        }
                    }
                    changedData.Add(newAccount);
                }
            }
            Console.WriteLine("File: " + (index+1));
            accounts.Sort((x, y) => DateTime.Compare(x.RegistrationDate, y.RegistrationDate));
            List<Account> merged = new List<Account>();
            //merge should happen here
            foreach (Account account in accounts)
            {
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
                if (rnd.NextDouble() > 0.001d)
                {
                    if (changedData.Exists(a => a.Id == account.Id))
                    {
                        Account changed = changedData.Find(a => a.Id == account.Id);
                        merged.Add(changed);
                        file.Write(changed.Id + "\t" + changed.Loginname + "\t" + changed.Password + "\t" + changed.RegistrationDate.ToString("yyyy-MM-dd hh:mm:ss.fff") + "\t" + changed.LastLoginDate.ToString(/*"yyyyMMddHHmmss" yyyy-MM-dd hh:mm:ss.fff yyyy-MM-ddTHH:mm:ss*/"yyyy-MM-dd hh:mm:ss.fff") + "\t" +
                            changed.CharacterName + "\t" + changed.Nation + "\t" + changed.Geartype + "\t" + changed.Level + "\t" + changed.Levelpercentage.ToString("0.##") + "\t" + changed.Spi +
                            "\t" + changed.Credits + "\t" + changed.Fame + "\t" + changed.Brigade + "\t" + changed.Attack + "\t" + changed.Defence + "\t" + changed.Evasion + "\t" +
                            changed.Fuel + "\t" + changed.Spirit + "\t" + changed.Shield + "\t" + changed.UnusedStatpoints + "\n");
                    }
                    else
                    {
                        merged.Add(account);
                        file.Write(account.Id + "\t" + account.Loginname + "\t" + account.Password + "\t" + account.RegistrationDate.ToString("yyyy-MM-dd hh:mm:ss.fff") + "\t" + account.LastLoginDate.ToString(/*"yyyyMMddHHmmss" yyyy-MM-dd hh:mm:ss.fff yyyy-MM-ddTHH:mm:ss*/"yyyy-MM-dd hh:mm:ss.fff") + "\t" +
                            account.CharacterName + "\t" + account.Nation + "\t" + account.Geartype + "\t" + account.Level + "\t" + account.Levelpercentage.ToString("0.##") + "\t" + account.Spi +
                            "\t" + account.Credits + "\t" + account.Fame + "\t" + account.Brigade + "\t" + account.Attack + "\t" + account.Defence + "\t" + account.Evasion + "\t" +
                            account.Fuel + "\t" + account.Spirit + "\t" + account.Shield + "\t" + account.UnusedStatpoints + "\n");
                    }
                }
            }
            int id = index * rows;
            //merge should happen here
            foreach (Account account in newAccounts)
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
                merged.Add(account);
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
            return merged;
        }
    }
}
