using MyMVCDashboard.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MyMVCDashboard.CustomAuthentication
{
    public class CustomMembership : MembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return false;
            }

            using (AuthenticationDB adb = new AuthenticationDB())
            {
                var user = (from us in adb.Users
                            where String.Compare(username, us.UserName, StringComparison.OrdinalIgnoreCase) == 0
                        && String.Compare(password, us.Password, StringComparison.OrdinalIgnoreCase) == 0
                        && us.IsActive == true
                            select us).FirstOrDefault();

                return (user != null) ? true : false;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string userName, bool userIsOnline)
        {
            using (AuthenticationDB adb = new AuthenticationDB())
            {
                var user = (from us in adb.Users
                            where String.Compare(userName, us.UserName, StringComparison.OrdinalIgnoreCase) == 0
                            select us).FirstOrDefault();

                if (user == null) return null;

                var selectedUser = new CustomMembershipUser(user);

                return selectedUser;
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (AuthenticationDB adb = new AuthenticationDB())
            {
                var user = (from us in adb.Users
                            where String.Compare(email, us.Email) == 0
                            select us.UserName).FirstOrDefault();

                return !string.IsNullOrEmpty(user) ? user : string.Empty;
            }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string Description
        {
            get
            {
                return base.Description;
            }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override string Name
        {
            get
            {
                return base.Name;
            }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        protected override byte[] DecryptPassword(byte[] encodedPassword)
        {
            return base.DecryptPassword(encodedPassword);
        }
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }
        protected override byte[] EncryptPassword(byte[] password)
        {
            return base.EncryptPassword(password);
        }
        protected override byte[] EncryptPassword(byte[] password, System.Web.Configuration.MembershipPasswordCompatibilityMode legacyPasswordCompatibilityMode)
        {
            return base.EncryptPassword(password, legacyPasswordCompatibilityMode);
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
        }
        protected override void OnValidatingPassword(ValidatePasswordEventArgs e)
        {
            base.OnValidatingPassword(e);
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
    }
}