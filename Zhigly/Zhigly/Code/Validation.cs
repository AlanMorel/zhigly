using System.Linq;
using System.Net.Mail;

namespace Zhigly.Code
{
    public class Validation
    {

        private static bool IsValidEmail(string email)
        {
            try
            {
                return new MailAddress(email).Address == email;
            }
            catch
            {
                // ignored
            }
            return false;
        }

        private static bool IsValidLength(string data, int min, int max)
        {
            return min <= data.Length && data.Length <= max;
        }

        private static bool IsValidEmailLength(string email)
        {
            return IsValidLength(email, Constants.MinEmailLength, Constants.MaxEmailLength);
        }

        private static bool IsValidName(string name)
        {
            return name.All(char.IsLetter);
        }

        private static bool IsValidNameLength(string name)
        {
            return IsValidLength(name, Constants.MinNameLength, Constants.MaxNameLength);
        }

        private static bool IsValidPasswordLength(string password)
        {
            return IsValidLength(password, Constants.MinPasswordLength, Constants.MaxPasswordLength);
        }

        private static bool IsValidTitleLength(string title)
        {
            return IsValidLength(title, Constants.MinTitleLength, Constants.MaxTitleLength);
        }

        private static bool IsValidDescriptionLength(string description)
        {
            return IsValidLength(description, Constants.MinDescriptionLength, Constants.MaxDescriptionLength);
        }

        private static bool IsValidOrganizationLength(string organization)
        {
            return IsValidLength(organization, Constants.MinOrganizationLength, Constants.MaxOrganizationLength);
        }

        private static bool IsValidMessageLength(string message)
        {
            return IsValidLength(message, Constants.MinMessageLength, Constants.MaxMessageLength);
        }

        private static bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length != 10)
            {
                return false;
            }

            int n;

            return int.TryParse(phone, out n);
        }

        public static string ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "Please enter an email address.";
            }

            if (!IsValidEmailLength(email))
            {
                return LengthErrorMessage("email address", Constants.MinEmailLength, Constants.MaxEmailLength);
            }

            if (!IsValidEmail(email))
            {
                return "Please enter a valid email address.";
            }

            return null;
        }

        public static string ValidateName(string primary)
        {
            if (string.IsNullOrEmpty(primary))
            {
                return "Please enter a name.";
            }

            if (!IsValidNameLength(primary))
            {
                return LengthErrorMessage("name", Constants.MinNameLength, Constants.MaxNameLength);
            }

            return null;
        }

        public static string ValidatePrimaryName(string primary, bool organization)
        {
            if (organization)
            {
                if (string.IsNullOrEmpty(primary))
                {
                    return "Please enter an organization name.";
                }

                if (!IsValidOrganizationLength(primary))
                {
                    return "Your organization name must be " + Constants.MinOrganizationLength + " to " + Constants.MaxOrganizationLength + " characters long.";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(primary))
                {
                    return "Please enter a first name.";
                }

                if (!IsValidNameLength(primary))
                {
                    return LengthErrorMessage("first name", Constants.MinNameLength, Constants.MaxNameLength);
                }

                if (!IsValidName(primary))
                {
                    return "Your first name can only contain letters.";
                }
            }

            return null;
        }

        public static string ValidateSecondaryName(string secondary, bool organization)
        {
            if (organization)
            {
                return null;
            }

            if (string.IsNullOrEmpty(secondary))
            {
                return "Please enter a last name.";
            }

            if (!IsValidName(secondary))
            {
                return "Your last name can only contain letters.";
            }

            if (!IsValidNameLength(secondary))
            {
                return LengthErrorMessage("last name", Constants.MinNameLength, Constants.MaxNameLength);
            }

            return null;
        }

        public static string ValidatePasswords(string password, string confirmation)
        {
            if (string.IsNullOrEmpty(password))
            {
                return ("Please enter a password.");
            }

            if (!password.Equals(confirmation))
            {
                return ("The passwords you entered do not match.");
            }

            if (!IsValidPasswordLength(password))
            {
                return LengthErrorMessage("password", Constants.MinPasswordLength, Constants.MaxPasswordLength);
            }
            return null;
        }

        public static string ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return "Please enter a title.";
            }

            if (!IsValidTitleLength(title))
            {
                return LengthErrorMessage("title", Constants.MinTitleLength, Constants.MaxTitleLength);
            }

            return null;
        }

        public static string ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return "Please enter a description.";
            }

            if (!IsValidDescriptionLength(description))
            {
                return LengthErrorMessage("description", Constants.MinDescriptionLength, Constants.MaxDescriptionLength);
            }

            return null;
        }

        public static string ValidatePhoneNumber(string phone)
        {
            if (phone.Length > 0 && !IsValidPhoneNumber(phone))
            {
                return "A valid phone number must be 10 numbers.";
            }

            return null;
        }

        public static string ValidateReason(string reason)
        {
            if (string.IsNullOrEmpty(reason))
            {
                return "Please enter a reason.";
            }

            return null;
        }

        public static string ValidateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return ("Please enter a message.");
            }

            if (!IsValidMessageLength(message))
            {
                return LengthErrorMessage("message", Constants.MinMessageLength, Constants.MaxMessageLength);
            }

            return null;
        }

        public static string ValidateChangePassword(string userPassword, string currentPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(currentPassword))
            {
                return "Please enter your current password.";
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                return "Please enter a new password.";
            }

            if (string.IsNullOrEmpty(confirmPassword))
            {
                return "Please confirm your new password.";
            }

            if (!currentPassword.Equals(userPassword))
            {
                return "Your current password is not correct.";
            }

            if (!IsValidPasswordLength(newPassword))
            {
                return LengthErrorMessage("password", Constants.MinPasswordLength, Constants.MaxPasswordLength);
            }

            if (!newPassword.Equals(confirmPassword))
            {
                return "Your new passwords do not match.";
            }

            return null;
        }

        public static string LengthErrorMessage(string data, int min, int max)
        {
            return "Your " + data + " must be " + min + " to " + max + " characters long.";
        }
    }
}