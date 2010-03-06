using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces.Entities;

namespace Interfaces.Services.Admin
{
    public interface IUsersService : IService<IUser>
    {
        /// <summary>
        /// generates hash and salt for given password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        void GenerateUserPassowordHash(string password, ref string salt, out string hash);

        /// <summary>
        /// gets a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        IUser GetByUserName(string username);

        /// <summary>
        /// perform server side paging
        /// </summary>
        /// <param name="start">page index</param>
        /// <param name="limit">page size</param>
        /// <returns></returns>
        IEnumerable<IUser> GetAllByCrietria(int start, int limit);

        void AddNewUser(string username, string hash, string salt, int roleID);
    }
}
