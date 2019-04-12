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

        public bool Register(MlContext ctx, out string msg) {
            if (!VerifyInput(out msg)) {
                return false;
            }

            var accountExists = ctx.Users.Where(t => t.Username.Equals(Username)).ToList().Any();
            if (accountExists) {
                msg = "Account already exists";
                return false;
            }

            try {
                ctx.Users.Add(new User {
                    Username = Username,
                    Secret = Utility.Security.HashPassword(Password)
                });
                ctx.SaveChanges();
            } catch (Exception ex) {
                msg = ex.Message;
                return false;
            }

            msg = "Account created";
            return true;
        }
        
        public bool Login(MlContext ctx, out string msg) {
            if (!VerifyInput(out msg)) {
                return false;
            }
            
            var dbPass = ctx.Users.FirstOrDefault(t => t.Username.Equals(Username))?.Secret;
            if (dbPass == null) {
                msg = "No user by that name";
                return false;
            }

            if (!Utility.Security.VerifyHashedPassword(dbPass, Password)) {
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