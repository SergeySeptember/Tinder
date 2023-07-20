﻿using Tinder.Models;
using Tinder.Models.Requests;

namespace Tinder.Services
{
    public static class ActionUsers
    {
        public static IEnumerable<Users> GetUsers()
        {
            using (Context db = new())
            {
                var allUsers = db.Users.ToList();
                return allUsers;
            };
        }

        public static Users GetUserById(int id)
        {
            using (Context db = new())
            {
                var userById = db.Users.FirstOrDefault(a => a.Id == id);
                return userById;
            }
        }

        public static Users CreatetUser(RequestUserBody body)
        {
            body.UserName = body.UserName.Trim();
            body.Location = body.Location.Trim();

            using (Context db = new())
            {
                db.Users.AddRange(new Users { UserName = body.UserName, Email = body.Email, Age = body.Age, Location = body.Location, Password = body.Password });
                db.SaveChanges();

                var createdUser = db.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Email == body.Email);

                return createdUser;
            };
        }
        public static Users UpdateUser(int id, RequestUserBody body)
        {
            body.UserName = body.UserName.Trim();
            body.Location = body.Location.Trim();

            using (Context db = new())
            {
                var updateUser = db.Users.FirstOrDefault(a => a.Id == id);

                updateUser.UserName = body.UserName;
                updateUser.Email = body.Email;
                updateUser.Age = body.Age;
                updateUser.Location = body.Location;
                updateUser.Password = body.Password;

                db.SaveChanges();

                var updatedUser = db.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Email == body.Email);

                return updatedUser;
            };
        }

        public static string DeleteUser(int id)
        {
            using (Context db = new())
            {
                var item = db.Users.Find(id);
                if (item != null)
                {
                    db.Users.Remove(item);
                    db.SaveChanges();
                    return "User successfully deleted!";
                }
                else
                {
                    return "User not found";
                }
            };
        }
    }
}
