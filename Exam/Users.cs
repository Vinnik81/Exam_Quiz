using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam
{
    [Serializable]
    public class Users: List<User>
    {
        public bool SingUp(string login, string password, string birthday)
        {
            if (CheckUserExists(login)) return false;
            this.Add(new User(login, password, DateTime.Parse(birthday)));
            return true;           
        }

        public bool SingIn(string login, string password)
        {
            User user = FindUser(login);
            if (!CheckUserExists(login)) return false;
            return user.Password == password;
        }

        public void ChangeUserPassword(string login, string newPassword)
        {
            User user = FindUser(login);
            this.Add(new User(login, newPassword, user.Birthday));
            this.Remove(user);
        }

        public void ChangeUserBirthday(string login, string newBirthday)
        {
            User user = FindUser(login);
            this.Add(new User(login, user.Password, DateTime.Parse(newBirthday)));
            this.Remove(user);
        }

        public bool CheckPassword(string login, string password) => FindUser(login).Password == password;

        private bool CheckUserExists(string login)=> FindUser(login) != null;


        public User FindUser(string login) => this.FirstOrDefault(user => user.Login == login);
        
    }
}
