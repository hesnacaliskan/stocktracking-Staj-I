using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Staj1_MvcProje.Models.Entity;

namespace Staj1_MvcProje.Security
{
    public class PersonelRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        
        public override string[] GetRolesForUser(string username)
        {
            MvcDbStokEntities2 db = new MvcDbStokEntities2();
            var kullanici = db.TBLKULLANICI.FirstOrDefault(m => m.AD == username);
            return new string[] { kullanici.ROL };
        }
               
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}