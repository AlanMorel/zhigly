using System;

namespace Zhigly.Code.Objects
{
    public class User
    {
        private const int EmailVerified = 0;
        private const int AccountVerified = 1;
        private const int Mod = 2;
        private const int Admin = 3;

        public int Id { get; set; }
        public string PrimaryName { get; set; }
        public string SecondaryName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public College School { get; set; }
        public string PhoneNumber { get; set; }
        public int Credit { get; set; }
        public bool Organization { get; set; }
        public int Status { get; set; }

        public string GetDisplayName()
        {
            if (Organization)
            {
                if (string.IsNullOrEmpty(SecondaryName))
                {
                    return PrimaryName;
                }
                else
                {
                    return SecondaryName + " at " + PrimaryName;
                }
            }
            else
            {
                return PrimaryName + " " + SecondaryName;
            }
        }

        public bool IsEmailVerified()
        {
            return Is(EmailVerified);
        }

        public bool IsAccountVerified()
        {
            return Is(AccountVerified);
        }

        public bool IsMod()
        {
            return Is(Mod);
        }

        public bool IsAdmin()
        {
            return Is(Admin);
        }

        public bool IsBanned()
        {
            return Status == 0;
        }

        private bool Is(int position)
        {
            return (Status & (1 << position)) != 0;// && Status < Math.Pow(10, 6);
        }
    }
}