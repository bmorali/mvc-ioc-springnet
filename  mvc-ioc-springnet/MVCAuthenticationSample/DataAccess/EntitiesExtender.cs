using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces.Entities;
using EntitiesDefinitions;
namespace DataAccess
{
    public partial class Users : IUser
    {

        #region IUser Members


        IRole IUser.Role
        {
            get
            {
               return (IRole)((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Roles>("AuthenticationDemoModel.FK_Users_Roles", "Roles").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Roles>("AuthenticationDemoModel.FK_Users_Roles", "Roles").Value = (Roles)value;
            }
        }

        #endregion
    }

    public partial class Roles : IRole
    { 
    
    }

    public partial class AuthenticationDemoEntities : IDBContext
    { 
        
    }
}
