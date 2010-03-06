using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces.Entities;
namespace Interfaces.Repository
{
    public interface IUsersRepository : IRepository<IUser>
    {
        IUser GetByName(string username);
        IUser GetNewInstance();
    }
}
