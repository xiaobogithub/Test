using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data.EntityClient;
using System.Data;
using System.Data.SqlClient;
using System.Data.Objects;

namespace ChangeTech.SQLServerRepository
{
    public class ActivityLogRepository : RepositoryBase, IActivityLogRepository
    {
        public void InsertIntoTestLogTableForReminderEmailsTest(List<TestLog> testLogs)
        {
            InsertEntities(testLogs, Guid.NewGuid());
        }

        public void Insert(ActivityLog log)
        {
            using (DynamicObjectContext context =  new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
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
            }
            //InsertEntity(log);
        }

        public List<ActivityLog> GetAll()
        {
            return GetEntities<ActivityLog>().ToList<ActivityLog>();
        }

        public ActivityLog GetItem(Guid logGuid)
        {
            return GetEntityById<ActivityLog>(logGuid);
        }

        public ActivityLog GetLatestItemByLogTypeAndMessage(int ActivityLogType, string Message)
        {
            //("it.Name LIKE @searchTerm", new ObjectParameter("searchTerm", searchTerm))
            return GetEntities<ActivityLog>().Where(a => a.ActivityLogType == ActivityLogType && a.Message.Contains(Message)).OrderByDescending(a => a.ActivityDateTime).FirstOrDefault();
        }

        public List<ActivityLog> GetItems(Guid UserGuid, Guid ProgramGuid, Guid SessionGuid, DateTime? Begin, DateTime? End, int ActivityLogType)
        {
            var query = GetEntities<ActivityLog>();
            if (UserGuid != Guid.Empty)
            {
                query = query.Where(l => l.UserGuid == UserGuid);
            }
            //if (ProgramGuid != Guid.Empty)
            //{
            //    query = query.Where(l => l.ProgramGuid == ProgramGuid);
            //}
            //if (SessionGuid != Guid.Empty)
            //{
            //    query = query.Where(l => l.SessionGuid == SessionGuid);
            //}
            //if (Begin.HasValue && Begin.Value != DateTime.MinValue)
            //{
            //    query = query.Where(l => l.ActivityDateTime.HasValue ? (l.ActivityDateTime.Value == Begin.Value) : false);
            //}
            //if (End.HasValue && End.Value != DateTime.MinValue)
            //{
            //    query = query.Where(l => l.ActivityDateTime.HasValue ? (l.ActivityDateTime.Value == End.Value) : false);
            //}
            //if (ActivityLogType != 0)
            //{
            //    query = query.Where(l => l.ActivityLogType == ActivityLogType);
            //}

            //List<ActivityLog> list = query.ToList<ActivityLog>();
            //return query.ToList<ActivityLog>();
            string condition = @"select * from ActivityLog as item where 1=1";
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

            List<ActivityLog> list = new List<ActivityLog>();
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
                        list.Add(new ActivityLog
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

        public DataTable GetActivityLogReport(string emaillistStr, string fileds, string days, string programGuid)
        {
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            DataTable result = new DataTable();
            string sqlStr = "SELECT [ProgramName],[SessionName]"+fileds+" FROM [ChangeTech].[dbo].[V_ActivityLog]"+
                " where ProgramGuid ='"+programGuid+"' and [Day] in ("+days+") and Email in ("+emaillistStr+") order by Email,[Day]";
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                SqlCommand command = new SqlCommand(sqlStr,connection);
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

        public ActivityLog GetLastActivity(Guid userGuid, int logType)
        {
            return GetEntities<ActivityLog>().Where(a => a.UserGuid == userGuid && a.ActivityLogType == logType).OrderByDescending(a => a.ActivityDateTime).FirstOrDefault();
        }

        public IQueryable<ActivityLog> GetAllReminderEmailsLogOfTodayByMessages(string successMessage, string failMessage)
        {
            return GetEntities<ActivityLog>().Where(a => (a.ActivityDateTime.HasValue ? (DateTime)a.ActivityDateTime >= DateTime.Today : false) && (a.Message.Contains(successMessage) || a.Message.Contains(failMessage)));
        }
    }
}
