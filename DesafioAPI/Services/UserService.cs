using DesafioAPI.Models;
using DesafioAPI.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Services
{
    public static class UserService
    {

        public static bool RegisterUser(string name, string email, string password)
        {
            var checkUser = ReturnUser(email);

            if (checkUser == null)
            {
                string query = "INSERT INTO TbUser(Name, Email, Password) VALUES ('" + name + "', '" + email + "', '" + Cryptography.GeneratePasswordMD5(password) + "')";
                bool insert = ConnectionBD.InsertUpdateDB(query);
                if (insert == true)
                {
                    Email.ConfirmRegistration(email);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

       public static User Login(string email, string password)
       {
            User user = ReturnUser(email);

            if (user != null)
            {
                if (user.Password == Cryptography.GeneratePasswordMD5(password))
                {
                    user.Password = null;
                    return user;
                }
                else
                    return null;
            }
            else
                return null;
       }

        public static bool UpdatePassword(string email, string password)
        {
            var checkUser = ReturnUser(email);
            if(checkUser != null)
            {
                string query = "UPDATE TbUser SET Password = '" + Cryptography.GeneratePasswordMD5(password) + "' WHERE Email = '"+email+"'";
                bool updatePass = ConnectionBD.InsertUpdateDB(query);
                if (updatePass == true)
                    return true;

                return false;
            }
            return false;
        }

        public static User ReturnUser(string email)
        {
            User user = new User();
            string query = "SELECT IdUser, Name, Email, Password FROM TbUser WHERE Email = '" + email + "'";
            DataTable dt = ConnectionBD.ReturnDataDB(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    user.Id = Convert.ToInt32(row["IdUser"].ToString());
                    user.Name = row["Name"].ToString();
                    user.Email = row["Email"].ToString();
                    user.Password = row["Password"].ToString();
                }
                return user;
            }
            return null;
        }

        public static bool SendEmailRecoveryPassword(string email)
        {
            var checkUser = ReturnUser(email);
            if (checkUser != null)
            {
                Email.RedefinePassword(email);
                return true;
            }
            
            return false;    
        }

        public static User RecoveryPassLinkEmail(string emailBase64)
        {
            try
            {
                string emailDecode = Encoding.UTF8.GetString(Convert.FromBase64String(emailBase64));
                var user = ReturnUser(emailDecode);
                if (user != null)
                {
                    user.Id = 0;
                    user.Password = null;
                    return user;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
