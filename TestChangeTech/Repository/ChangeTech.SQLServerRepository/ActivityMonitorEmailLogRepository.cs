using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace ChangeTech.SQLServerRepository
{
    public class ActivityMonitorEmailLogRepository : RepositoryBase, IActivityMonitorEmailLogRepository
    {
        public const string REMINDEREMAILCONTENTOFSUCCESS = "Reminder email is sent successfuly";
        public const string REMINDEREMAILCONTENTOFFAIL = "Fail to send the reminder email";
        public void Insert(ActivityMonitorEmailLog log)
        {
            using (DynamicObjectContext context = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
            {
                try
                {
                    if (IsLogTypeFitPrioritySetting(log.ActivityLogType, context))
                    {
                        InsertEntity(log, context);
                    }
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public List<ActivityMonitorEmailLog> GetAll()
        {
            return GetEntities<ActivityMonitorEmailLog>().ToList<ActivityMonitorEmailLog>();
        }

        public ActivityMonitorEmailLog GetItem(Guid logGuid)
        {
            return GetEntityById<ActivityMonitorEmailLog>(logGuid);
        }

        public ActivityMonitorEmailLog GetLatestItemByLogTypeAndMessage(int ActivityLogType, string Message)
        {
            //("it.Name LIKE @searchTerm", new ObjectParameter("searchTerm", searchTerm))
            return GetEntities<ActivityMonitorEmailLog>().Where(a => a.ActivityLogType == ActivityLogType && a.Message.Contains(Message)).OrderByDescending(a => a.ActivityDateTime).FirstOrDefault();
        }

        public List<ActivityMonitorEmailLog> GetItems(Guid UserGuid, Guid ProgramGuid, Guid SessionGuid, DateTime? Begin, DateTime? End, int ActivityLogType)
        {
            var query = GetEntities<ActivityMonitorEmailLog>();
            if (UserGuid != Guid.Empty)
            {
                query = query.Where(l => l.UserGuid == UserGuid);
            }
            string condition = @"select * from ActivityMonitorEmailLog as item where 1=1";
            if (UserGuid != Guid.Empty)
            {
                condition += " and item.UserGuid = @userGuid";
            }
            if (ProgramGuid != Guid.Empty)
            {
                condition += " and item.ProgramGuid = @programGuid";
            }
            if (SessionGuid != Guid.Empty)
            {
                condition += " and item.SessionGuid = @sessionGuid";
            }
            if (Begin.HasValue && Begin.Value != DateTime.MinValue)
            {
                condition += " and item.ActivityDateTime >= @begin";
            }
            if (End.HasValue && End.Value != DateTime.MinValue)
            {
                condition += " and item.ActivityDateTime <= @end";
            }
            if (ActivityLogType != 0)
            {
                condition += " and item.ActivityLogType = @activityLogType";
            }

            List<ActivityMonitorEmailLog> list = new List<ActivityMonitorEmailLog>();
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = condition;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("userGuid", UserGuid);
                command.Parameters.AddWithValue("programGuid", ProgramGuid);
                command.Parameters.AddWithValue("sessionGuid", SessionGuid);
                command.Parameters.AddWithValue("begin", Begin);
                command.Parameters.AddWithValue("end", End);
                command.Parameters.AddWithValue("activityLogType", ActivityLogType);
                try
                {

                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        list.Add(new ActivityMonitorEmailLog
                        {
                            ActivityLogType = Convert.ToInt32(dr["ActivityLogType"]),
                            UserGuid = new Guid(dr["UserGuid"].ToString()),
                            ProgramGuid = new Guid(dr["ProgramGuid"].ToString()),
                            SessionGuid = new Guid(dr["SessionGuid"].ToString()),
                            ActivityDateTime = Convert.ToDateTime(dr["ActivityDateTime"].ToString()),
                            Message = dr["Message"].ToString()
                        });
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return list;
        }

        public System.Data.DataTable GetActivityMonitorEmailLogReport(string emaillistStr, string fileds, string days, string programGuid)
        {
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            DataTable result = new DataTable();
            string sqlStr = "SELECT [ProgramName],[SessionName]" + fileds + " FROM [ChangeTech].[dbo].[V_ActivityLog]" +
                " where ProgramGuid ='" + programGuid + "' and [Day] in (" + days + ") and Email in (" + emaillistStr + ") order by Email,[Day]";
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("Fields", fileds);
                command.Parameters.AddWithValue("ProgramGUID", programGuid);
                command.Parameters.AddWithValue("Days", days);
                command.Parameters.AddWithValue("Emails", emaillistStr);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(result);
            }

            return result;
        }

        public ActivityMonitorEmailLog GetLastActivityMonitorEmailLog(Guid userGuid, int logType)
        {
            return GetEntities<ActivityMonitorEmailLog>().Where(a => a.UserGuid == userGuid && a.ActivityLogType == logType).OrderByDescending(a => a.ActivityDateTime).FirstOrDefault();
        }

        public IQueryable<ActivityMonitorEmailLog> GetAllReminderEmailsLogOfToday()
        {
            return GetEntities<ActivityMonitorEmailLog>().Where(a => (a.ActivityDateTime.HasValue ? (DateTime)a.ActivityDateTime >= DateTime.Today : false) && (a.Message.Contains(REMINDEREMAILCONTENTOFSUCCESS) || a.Message.Contains(REMINDEREMAILCONTENTOFFAIL)));
        }
    }
}
