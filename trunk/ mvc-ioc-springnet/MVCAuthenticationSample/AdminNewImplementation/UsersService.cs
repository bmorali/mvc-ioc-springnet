using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces.Services.Admin;
using Interfaces.Repository;
using Interfaces.Repositories.Admin;
using Interfaces.Entities;

namespace Admin
{
    public class UsersService : IUsersService
    {
        public IUsersRepository UserRepository { get; set; }
        public IRolesRepository RoleRepository { get; set; }


        #region IUsersService Members

        public void GenerateUserPassowordHash(string password, ref string salt, out string hash)
        {
            WebSecurity.HashWithSalt(password, ref salt, out hash);
        }

        public IUser GetByUserName(string username)
        {
            return UserRepository.GetByName(username);
        }

        /// <summary>
        /// perform server side paging
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IEnumerable<IUser> GetAllByCrietria(int start, int limit)
        {
            return UserRepository.GetAll().ToList().Skip(start).Take(limit); 
        }

        public void AddNewUser(string username, string hash, string salt, int roleID)
        {
            var user = UserRepository.GetNewInstance();
            user.UserName = username;
            user.Hash = hash;
            user.Salt = salt;
            //user.Role = RoleRepository.GetById(roleID);
            UserRepository.Add(user);
            UserRepository.SaveChanges();
        }

        #endregion

        #region IService<DataAccess.Users> Members

        public IEnumerable<IUser> GetAll()
        {
            return UserRepository.GetAll();
        }
      
        public IUser GetById(int id)
        {
            return UserRepository.GetById(id);
        }

        public void Delete(IUser entity)
        {
            UserRepository.Delete(entity);
        }

        public void SaveChanges()
        {
            UserRepository.SaveChanges();
        }

        #endregion

      
    }
}
