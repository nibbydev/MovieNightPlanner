using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Domain;

namespace MovieNight.Models.Admin {
    public class AdminUsersViewModel {
        public List<User> Users { get; set; }
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
                default:
                    msg = "Invalid action";
                    return false;
            }
        }
        
        private bool DeleteUser(MlContext ctx, out string msg) {
            var user = ctx.Users.FirstOrDefault(t => t.Id.Equals(Id));
            if (user == null) {
                msg = "No such user";
                return false;
            }
            
            if (user.IsAdmin) {
                msg = "Cannot delete an admin";
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