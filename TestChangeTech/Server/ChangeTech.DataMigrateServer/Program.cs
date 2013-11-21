using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.DataMigrateServer.LocalDB;
using ChangeTech.DataMigrateServer.RemoteDB;

namespace ChangeTech.DataMigrateServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start data copy at: {0}", DateTime.Now.ToString());

                CopyUser();

                Console.WriteLine("Complete data copy at: {0}", DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private static void CopyUser()
        {
            using (LocalChangeTechEntities localChangeTechDBInstance = new LocalChangeTechEntities())
            {
                var users = from User in localChangeTechDBInstance.User select User;
                using (RemoteChangeTechEntities remoteChangeTechDBInstance = new RemoteChangeTechEntities())
                {
                    foreach (ChangeTech.DataMigrateServer.LocalDB.User localUserEntity in users)
                    {
                        ChangeTech.DataMigrateServer.RemoteDB.User remoteUserEntity = new ChangeTech.DataMigrateServer.RemoteDB.User();
                        remoteUserEntity.UserGUID = localUserEntity.UserGUID;
                        remoteUserEntity.Email = localUserEntity.Email;
                        remoteUserEntity.FirstName = localUserEntity.FirstName;
                        remoteUserEntity.Gender = localUserEntity.Gender;
                        remoteUserEntity.IsDeleted = localUserEntity.IsDeleted;
                        remoteUserEntity.LastLogon = localUserEntity.LastLogon;
                        remoteUserEntity.LastName = localUserEntity.LastName;
                        remoteUserEntity.MobilePhone = localUserEntity.MobilePhone;
                        remoteUserEntity.Password = localUserEntity.Password;
                        remoteUserEntity.Security = localUserEntity.Security;
                        remoteUserEntity.UserType = localUserEntity.UserType;

                        Console.WriteLine("Copy User {0} {1}", remoteUserEntity.UserGUID, remoteUserEntity.Email);
                        remoteChangeTechDBInstance.AddToUser(remoteUserEntity);
                        remoteChangeTechDBInstance.SaveChanges();
                    }
                }
            }
        }
    }
}
