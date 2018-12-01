using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uebung05_GenerateAccountHistoryData
{
    enum Geartype
    {
        A,
        B,
        I,
        M
    }

    class Account
    {
        Random rnd;
        private string[] Brigades_ANI = { "Flyingdragon", "Bulletproof", "BlackEagles", "StarshipTrooper", "ANIsBADBOYs", "Escalation", "StarWarsFighters", "crazy", "xSOULxHUNTERx", "cimbom" };
        private string[] Brigades_BCU = { "2Stoned", "EliteHeroes", "CST", "RapidScout", "FIXundFERTIG", "TheBrothersOfDeath", "Werwölfe", "TurtlePower", "Lantianer", "InFamous" };

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string loginname;

        public string Loginname
        {
            get { return loginname; }
            set { loginname = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private DateTime registrationDate;

        public DateTime RegistrationDate
        {
            get { return registrationDate; }
            set { registrationDate = value; }
        }

        private DateTime lastLoginDate;

        public DateTime LastLoginDate
        {
            get { return lastLoginDate; }
            set { lastLoginDate = value; }
        }

        private string characterName;

        public string CharacterName
        {
            get { return characterName; }
            set { characterName = value; }
        }

        private string nation;

        public string Nation
        {
            get { return nation; }
            set { nation = value; }
        }

        private Geartype geartype;

        public Geartype Geartype
        {
            get { return geartype; }
            set { geartype = value; }
        }

        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private double levelpercentage;

        public double Levelpercentage
        {
            get { return levelpercentage; }
            set { levelpercentage = value; }
        }

        private int spi;

        public int Spi
        {
            get { return spi; }
            set { spi = value; }
        }

        private int credits;

        public int Credits
        {
            get { return credits; }
            set { credits = value; }
        }

        private int fame;

        public int Fame
        {
            get { return fame; }
            set { fame = value; }
        }

        private string brigade;

        public string Brigade
        {
            get { return brigade; }
            set { brigade = value; }
        }

        private short attack;

        public short Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        private short defence;

        public short Defence
        {
            get { return defence; }
            set { defence = value; }
        }

        private short evasion;

        public short Evasion
        {
            get { return evasion; }
            set { evasion = value; }
        }

        private short fuel;

        public short Fuel
        {
            get { return fuel; }
            set { fuel = value; }
        }

        private short spirit;

        public short Spirit
        {
            get { return spirit; }
            set { spirit = value; }
        }

        private short shield;

        public short Shield
        {
            get { return shield; }
            set { shield = value; }
        }

        private short unusedStatpoints;

        public short UnusedStatpoints
        {
            get { return unusedStatpoints; }
            set { unusedStatpoints = value; }
        }

        public Account(int id, string loginname, string password, DateTime registrationDate, DateTime lastLoginDate, string characterName, string nation, Geartype geartype, int level, double levelpercentage, int spi, int credits, int fame, string brigade, short attack, short defence, short evasion, short fuel, short spirit, short shield, short unusedStatpoints)
        {
            Id = id;
            Loginname = loginname;
            Password = password;
            RegistrationDate = registrationDate;
            LastLoginDate = lastLoginDate;
            CharacterName = characterName;
            Nation = nation;
            Geartype = geartype;
            Level = level;
            Levelpercentage = levelpercentage;
            Spi = spi;
            Credits = credits;
            Fame = fame;
            Brigade = brigade;
            Attack = attack;
            Defence = defence;
            Evasion = evasion;
            Fuel = fuel;
            Spirit = spirit;
            Shield = shield;
            UnusedStatpoints = unusedStatpoints;
        }

        public Account(DateTime registrationDate, Random rnd)
        {
            this.rnd = rnd;
            RegistrationDate = GetRandomRegistrationTime(registrationDate);
            LastLoginDate = GetRandomLastLoginTime(registrationDate);
            Nation = GetRandomNation();
            Geartype = (Geartype)rnd.Next(4);
            Level = rnd.Next(1, 116);
            Levelpercentage = rnd.NextDouble() * 99.99d;
            Spi = rnd.Next(Int32.MaxValue);
            Credits = rnd.Next(1000000000);
            Fame = rnd.Next(50000);
            SetRandomStats();
            Brigade = GetRandomBrigade();
        }

        private string GetRandomBrigade()
        {
            return (Nation.Equals("ANI")) ? Brigades_ANI[rnd.Next(Brigades_ANI.Length)] : Brigades_BCU[rnd.Next(Brigades_BCU.Length)];
        }

        private void SetRandomStats()
        {
            double rndStat;
            switch (Geartype)
            {
                case Geartype.A:
                    Attack = 4;
                    Defence = 3;
                    Evasion = 1;
                    Fuel = 3;
                    Spirit = 3;
                    Shield = 4;
                    for (UnusedStatpoints = (short)(Level * 1.33); UnusedStatpoints > 0; UnusedStatpoints--)
                    {
                        rndStat = rnd.NextDouble() * 100;
                        if (rndStat <= 58.05d)
                        {
                            if (Attack < 340)
                            {
                                Attack += 4;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        } else if (rndStat > 58.05d && rndStat <= 86.0d)
                        {
                            if (Defence < 340)
                            {
                                Defence += 3;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        } else if (rndStat > 86.0d && rndStat <= 86.7d)
                        {
                            if (Evasion < 340)
                            {
                                Evasion += 1;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        } else if (rndStat > 86.7d && rndStat <= 91.6d)
                        {
                            if (Fuel < 340)
                            {
                                Fuel += 3;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        } else if (rndStat > 91.6d && rndStat <= 95.8d)
                        {
                            if (Spirit < 340)
                            {
                                Spirit += 3;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        } else if (rndStat > 95.8d)
                        {
                            if (Shield < 340)
                            {
                                Shield += 4;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                    }
                    break;
                case Geartype.B:
                    Attack = 3;
                    Defence = 3;
                    Evasion = 3;
                    Fuel = 3;
                    Spirit = 3;
                    Shield = 3;
                    for (UnusedStatpoints = (short)(Level * 1.33); UnusedStatpoints > 0; UnusedStatpoints--)
                    {
                        rndStat = rnd.NextDouble() * 100;
                        if (rndStat <= 58.9d)
                        {
                            if (Attack < 340)
                            {
                                Attack += 3;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 58.9d && rndStat <= 78.75d)
                        {
                            if (Defence < 340)
                            {
                                Defence += 3;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 78.75d && rndStat <= 89.7d)
                        {
                            if (Evasion < 340)
                            {
                                Evasion += 3;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 89.7d && rndStat <= 93.8d)
                        {
                            if (Fuel < 340)
                            {
                                Fuel += 3;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 93.8d && rndStat <= 99.3d)
                        {
                            if (Spirit < 340)
                            {
                                Spirit += 3;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 99.3d)
                        {
                            if (Shield < 340)
                            {
                                Shield += 3;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        }
                    }
                    break;
                case Geartype.I:
                    Attack = 4;
                    Defence = 2;
                    Evasion = 4;
                    Fuel = 3;
                    Spirit = 3;
                    Shield = 2;
                    for (UnusedStatpoints = (short)(Level * 1.33); UnusedStatpoints > 0; UnusedStatpoints--)
                    {
                        rndStat = rnd.NextDouble() * 100;
                        if (rndStat <= 54.15d)
                        {
                            if (Attack < 340)
                            {
                                Attack += 4;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 54.15d && rndStat <= 54.9d)
                        {
                            if (Defence < 340)
                            {
                                Defence += 2;
                            } else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 54.9d && rndStat <= 96.25d)
                        {
                            if (Evasion < 340)
                            {
                                Evasion += 4;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 96.25d && rndStat <= 98.5d)
                        {
                            if (Fuel < 340)
                            {
                                Fuel += 3;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 98.5d && rndStat <= 99.25d)
                        {
                            if (Spirit < 340)
                            {
                                Spirit += 3;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 99.25d)
                        {
                            if (Shield < 340)
                            {
                                Shield += 2;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                    }
                    break;
                case Geartype.M:
                    Attack = 2;
                    Defence = 4;
                    Evasion = 2;
                    Fuel = 3;
                    Spirit = 4;
                    Shield = 3;
                    for (UnusedStatpoints = (short)(Level * 1.33); UnusedStatpoints > 0; UnusedStatpoints--)
                    {
                        rndStat = rnd.NextDouble() * 100;
                        if (rndStat <= 7.2d)
                        {
                            if (Attack < 340)
                            {
                                Attack += 2;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 7.2d && rndStat <= 62.6d)
                        {
                            if (Defence < 340)
                            {
                                Defence += 4;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 62.6d && rndStat <= 63.3d)
                        {
                            if (Evasion < 340)
                            {
                                Evasion += 2;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 63.3d && rndStat <= 70.5d)
                        {
                            if (Fuel < 340)
                            {
                                Fuel += 3;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 70.5d && rndStat <= 91.35d)
                        {
                            if (Spirit < 340)
                            {
                                Spirit += 4;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                        else if (rndStat > 91.35d)
                        {
                            if (Shield < 340)
                            {
                                Shield += 3;
                            }
                            else
                            {
                                UnusedStatpoints++;
                            }
                        }
                    }
                    break;
            }
        }

        public DateTime GetRandomRegistrationTime(DateTime registrationDate)
        {
            registrationDate = registrationDate.AddHours(rnd.Next(23)).AddMinutes(rnd.Next(59)).AddSeconds(rnd.Next(59));
            return registrationDate;
        }

        private DateTime GetRandomLastLoginTime(DateTime registrationDate)
        {
            registrationDate = registrationDate.AddMonths(rnd.Next(12)).AddDays(rnd.Next(31)).AddHours(rnd.Next(23)).AddMinutes(rnd.Next(59)).AddSeconds(rnd.Next(59));
            return registrationDate;
        }

        private string GetRandomNation()
        {
            return (rnd.Next(2) == 1) ? "ANI" : "BCU";
        }

        public double GetMaxLevelPerDay()
        {
            double diff = 115 - Level;
            if (Level >= 100)
            {
                return diff * 0.05d;
            }
            return diff/10d;
        }
    }
}
