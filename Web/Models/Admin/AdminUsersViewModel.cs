using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DAL;
using DAL.Domain;

namespace MovieNight.Models.Admin {
    public class AdminUsersViewModel {
        private static readonly Regex UsernamePattern = new Regex("^[a-zA-Z0-9_]+$");
        
        public List<User> Users { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public int Id { get; set; }

        public AdminUsersViewModel() {
            
        }

        public void GetUsers(MlContext ctx) {
            Users = ctx.Users.ToList();
        }
        
        public bool DoAction(MlContext ctx, out string msg) {
            switch (Action) {
                case "delete":
                    return DeleteUser(ctx, out msg);
                case "reset":
                    return ResetPassword(ctx, out msg);
                case "create":
                    return CreateUser(ctx, out msg);
                default:
                    msg = "Invalid action";
                    return false;
            }
        }
        
        private bool CreateUser(MlContext ctx, out string msg) {
            if (!VerifyInput(out msg)) {
                return false;
            }
            
            var user = ctx.Users.FirstOrDefault(t => t.Username.Equals(Username));
            if (user != null) {
                msg = "User already exists";
                return false;
            }
            
            user = new User {
                Username = Username
            };

            try {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            } catch (Exception e) {
                msg = e.Message;
                return false;
            }

            msg = "User created";
            return true;
        }

        private bool VerifyInput(out string msg) {
            if (string.IsNullOrEmpty(Username)) {
                msg = "No username provided";
                return false;
            }

            if (Username.Length < 3) {
                msg = "Username must be at least 3 characters";
                return false;
            }
            
            if (Username.Length > 32) {
                msg = "Username must not be more than 32 characters";
                return false;
            }
            
            if (!UsernamePattern.IsMatch(Username)) {
                msg = "Username contains illegal symbols";
                return false;
            }

            msg = "Input verified successfully";
            return true;
        }
        
        private bool DeleteUser(MlContext ctx, out string msg) {
            var user = ctx.Users.FirstOrDefault(t => t.Id.Equals(Id));
            if (user == null) {
                msg = "No such user";
                return false;
            }
            
            if (user.IsAdmin) {
                msg = "Cannot delete an administrative user";
                return false;
            }

            try {
                ctx.Users.Remove(user);
                ctx.SaveChanges();
            } catch (Exception e) {
                msg = e.Message;
                return false;
            }

            msg = "User deleted";
            return true;
        }
        
        private bool ResetPassword(MlContext ctx, out string msg) {
            var user = ctx.Users.FirstOrDefault(t => t.Id.Equals(Id));
            if (user == null) {
                msg = "No such user";
                return false;
            }
            
            // Keep admins from resetting other admins' passwords
            if (user.IsAdmin) {
                msg = "Cannot reset an admin password through the admin panel";
                return false;
            }

            try {
                user.Secret = null;
                ctx.Users.Update(user);
                ctx.SaveChanges();
            } catch (Exception e) {
                msg = e.Message;
                return false;
            }

            msg = "User's password reset";
            return true;
        }
    }
}