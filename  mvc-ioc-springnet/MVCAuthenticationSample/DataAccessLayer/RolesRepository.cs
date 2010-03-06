using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces.Repositories.Admin;
using log4net;
using System.IO;
using Interfaces.Entities;
using DataAccess;
using EntitiesDefinitions;

namespace DataAccessLayer
{
    public class RolesRepository : IRolesRepository
    {
        private AuthenticationDemoEntities dbContext;

        private readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RolesRepository()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
        }
        #region IRepository<Roles> Members

        public IEnumerable<IRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public IRole GetById(int id)
        {        
            return this.dbContext.Roles.FirstOrDefault(r => r.ID == id);
        }

        public IDBContext DBContext
        {
            get
            {
                return dbContext;
            }
            set
            {
                dbContext = (AuthenticationDemoEntities)value;
            }
        }

        public void Add(IRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IRole entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
