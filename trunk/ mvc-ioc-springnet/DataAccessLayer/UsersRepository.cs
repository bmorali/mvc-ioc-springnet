using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces.Repository;
using DataAccess;
using log4net;
using System.IO;
using Interfaces.Entities;
using EntitiesDefinitions;

namespace DataAccessLayer
{
    public class UsersRepository : IUsersRepository
    {
        private AuthenticationDemoEntities dbContext;

        private readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UsersRepository()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
        }
        #region IUsersRepository Members

        public IUser GetByName(string username)
        {
            if (username == null)
                throw new ArgumentNullException("Username cannot be null.");

            if (username == string.Empty)
                throw new ArgumentException("Username cannot be empty string.");


            return (Users)this.dbContext.Users.FirstOrDefault(u => u.UserName.Equals(username));
        }

        public IUser GetNewInstance()
        {
            return new Users();
        }

        #endregion

        #region IRepository<IUser> Members

        public IEnumerable<IUser> GetAll()
        {
            List<IUser> list = new List<IUser>();
            foreach (Users u in this.dbContext.Users.ToList())
                list.Add(u);

            return list;
        }

        public IUser GetById(int id)
        {
            return this.dbContext.Users.FirstOrDefault( u => u.ID == id);
        }

        public IDBContext DBContext
        {
            get { return dbContext; }
            set { dbContext = (AuthenticationDemoEntities)value; }
        }

        public void Add(IUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity of type Users should be passed.");

            this.dbContext.AddToUsers((Users)entity);
        }

        public void Delete(IUser entity)
        {
            this.dbContext.DeleteObject((Users)entity);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        #endregion
    }
}
