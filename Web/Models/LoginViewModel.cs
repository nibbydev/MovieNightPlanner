using System;
using System.Linq;
using System.Text.RegularExpressions;
using DAL;
using DAL.Domain;

namespace MovieNight.Models {
    public class LoginViewModel {
        private static readonly Regex UsernameNameRegex = new Regex(@"^[a-zA-Z_]+$");
        
        public string Username { get; set; }
        public string Password { get; set; }
        
        public LoginViewModel() {}
        
        public bool Login(MlContext ctx, out User user, out string msg) {
            user = null;
            
            if (!VerifyInput(out msg)) {
                return false;
            }

            user = ctx.Users.FirstOrDefault(t => t.Username.Equals(Username));
            
            if (user == null) {
                msg = "No user by that name";
                return false;
            }

            // If user has no password set, treat is as registration
            if (string.IsNullOrEmpty(user.Secret)) {
                user.Secret = Utility.Security.HashPassword(Password);
                
                try {
                    ctx.Users.Update(user);
                    ctx.SaveChanges();
                } catch (Exception ex) {
                    msg = ex.Message;
                    return false;
                }
                
                msg = "Account created";
                return true;
            }

            if (!Utility.Security.VerifyHashedPassword(user.Secret, Password)) {
                msg = "Invalid login credentials";
                return false;
            }
            
            msg = "Successfully logged in";
            return true;
        }

        private bool VerifyInput(out string msg) {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Username)) {
                msg = "Missing username";
                return false;
            }

            if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password)) {
                msg = "Missing password";
                return false;
            }

            if (Username.Length < 3) {
                msg = "Username must be longer than 3 characters";
                return false;
            }

            if (Username.Length > 32) {
                msg = "Username must not be longer than 32 characters";
                return false;
            }
            
            if (Password.Length < 3) {
                msg = "Password must be longer than 3 characters";
                return false;
            }

            if (Password.Length > 64) {
                msg = "Password must not be longer than 64 characters";
                return false;
            }

            if (!UsernameNameRegex.IsMatch(Username)) {
                msg = "Username contains invalid characters";
                return false;
            }

            msg = "All good";
            return true;
        }
    }
}