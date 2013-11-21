using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Data.EntityClient;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace ChangeTech.SQLServerRepository
{
    public class StoreProcedure : RepositoryBase, IStoreProcedure
    {
        #region IStoreProcedure Members
        public string UpdateLastSendEmailTimeAndInsertLog(Guid programGuid,Guid userGuid)
        {
            string message = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.UpdateLastSendEmailTimeAndInsertLog";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGUID", programGuid);
                command.Parameters.AddWithValue("UserGUID", userGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        message += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return message;
        }

        public string GetProgramReportXML(Guid programGuid)
        {
            string programReport = string.Empty;

            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetProgramXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGUID", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        programReport += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return programReport;
        }

        public string GetPreviewPageModelAsXML(Guid pageGuid, Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            string pagePreviewModelXML = string.Empty;

            Session sessionEntity = GetEntities<Session>(s => s.SessionGUID == sessionGuid).FirstOrDefault();
            if (!sessionEntity.ProgramReference.IsLoaded)
            {
                sessionEntity.ProgramReference.Load();
            }
            Guid programGuid = sessionEntity.Program.ProgramGUID;

            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.GetPagePreviewModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("pageGuid", pageGuid);
                command.Parameters.AddWithValue("languageGuid", languageGuid);
                command.Parameters.AddWithValue("sessionGuid", sessionGuid);
                command.Parameters.AddWithValue("pageSequenceGuid", pageSequenceGuid);
                command.Parameters.AddWithValue("userGuid", userGuid);
                command.Parameters.AddWithValue("programGuid", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        pagePreviewModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return pagePreviewModelXML;
        }

        public string GetPreviewSessionModelAsXML(Guid languageGuid, Guid sessionGuid, Guid userGuid)
        {
            string pagePreviewModelXML = string.Empty;
            Session sessionEntity = GetEntities<Session>(s => s.SessionGUID == sessionGuid).FirstOrDefault();
            if (!sessionEntity.ProgramReference.IsLoaded)
            {
                sessionEntity.ProgramReference.Load();
            }
            Guid programGuid = sessionEntity.Program.ProgramGUID;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.CommandTimeout = 20000;
                command.Connection = connection;
                command.CommandText = "Entities.GetSessionPreviewModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("languageGuid", languageGuid);
                command.Parameters.AddWithValue("sessionGuid", sessionGuid);
                command.Parameters.AddWithValue("userGuid", userGuid);
                command.Parameters.AddWithValue("programGuid", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        pagePreviewModelXML += dr.GetString(0);
                    }
                }
                catch
                {

                }
                finally
                {
                    connection.Close();
                }
            }
            return pagePreviewModelXML;
        }

        public string GetPinCodeModelAsXML(Guid programGuid, Guid lanaugeGuid, Guid userGuid)
        {
            string pinCodeXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetProgramPinCodeAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGUID", programGuid);
                command.Parameters.AddWithValue("languageGUID", lanaugeGuid);
                command.Parameters.AddWithValue("userGUID", userGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        pinCodeXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return pinCodeXML;
        }

        public string GetPaymentModelAsXML(Guid programGuid, Guid languageGuid, Guid userGuid)
        {
            string pinCodeXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetPaymentAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGUID", programGuid);
                command.Parameters.AddWithValue("languageGUID", languageGuid);
                command.Parameters.AddWithValue("userGUID", userGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        pinCodeXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return pinCodeXML;
        }
        public string GetPreviewPageSequenceModelAsXMl(Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            string pagePreviewModelXML = string.Empty;
            Session sessionEntity = GetEntities<Session>(s => s.SessionGUID == sessionGuid).FirstOrDefault();
            if (!sessionEntity.ProgramReference.IsLoaded)
            {
                sessionEntity.ProgramReference.Load();
            }
            Guid programGuid = sessionEntity.Program.ProgramGUID;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.GetPageSequencePreviewModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("languageGuid", languageGuid);
                command.Parameters.AddWithValue("sessionGuid", sessionGuid);
                command.Parameters.AddWithValue("pageSequenceGuid", pageSequenceGuid);
                command.Parameters.AddWithValue("userGuid", userGuid);
                command.Parameters.AddWithValue("programGuid", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        pagePreviewModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return pagePreviewModelXML;
        }

        public string GetRelapseModelAsXML(Guid languageGuid, Guid programGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            string relapseModelXML = string.Empty;

            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetRelapseModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("languageGuid", languageGuid);
                command.Parameters.AddWithValue("pageSequenceGuid", pageSequenceGuid);
                command.Parameters.AddWithValue("userGuid", userGuid);
                command.Parameters.AddWithValue("programGuid", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        relapseModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return relapseModelXML;
        }

        public string GetRelapsePreviewModelAsXML(Guid languageGuid, Guid programGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            string relapsePreviewModelXML = string.Empty;

            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetRelapsePreviewModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("languageGuid", languageGuid);
                command.Parameters.AddWithValue("sessionGuid", Guid.Empty);
                command.Parameters.AddWithValue("pageSequenceGuid", pageSequenceGuid.ToString().ToUpper());
                command.Parameters.AddWithValue("userGuid", userGuid);
                command.Parameters.AddWithValue("programGuid", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        relapsePreviewModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return relapsePreviewModelXML;
        }

        public string GetTempPreviewPageSequenceModelAsXMl(Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            string pagePreviewModelXML = string.Empty;
            Session sessionEntity = GetEntities<Session>(s => s.SessionGUID == sessionGuid).FirstOrDefault();
            if (!sessionEntity.ProgramReference.IsLoaded)
            {
                sessionEntity.ProgramReference.Load();
            }
            Guid programGuid = sessionEntity.Program.ProgramGUID;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetTempPageSequencePreviewModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("languageGuid", languageGuid);
                command.Parameters.AddWithValue("sessionGuid", sessionGuid);
                command.Parameters.AddWithValue("pageSequenceGuid", pageSequenceGuid);
                command.Parameters.AddWithValue("userGuid", userGuid);
                command.Parameters.AddWithValue("programGuid", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        pagePreviewModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return pagePreviewModelXML;
        }

        public string GetLiveSessionModelAsXML(Guid userGuid, Guid programGuid, Guid languageGuid, int day)
        {
            string pagePreviewModelXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.GetSessionModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("languageGuid", languageGuid);
                command.Parameters.AddWithValue("programGuid", programGuid);
                command.Parameters.AddWithValue("userGuid", userGuid);
                command.Parameters.AddWithValue("day", day);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        pagePreviewModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return pagePreviewModelXML;
        }

        public string GetCTPPModelAsXML(Guid programGuid)
        {
            string ctppModelXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.GetCTPPModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGuid", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        ctppModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return ctppModelXML;
        }

        public string GetCTPPModelAsXMLByProgramGuidAndUserGuid(Guid programGuid,Guid userGuid)
        {
            string ctppModelXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.GetCTPPModelAsXMLByProgramGuidAndUserGuid";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGuid", programGuid);
                command.Parameters.AddWithValue("userGuid", userGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        ctppModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return ctppModelXML;
        }

        public string GetProgramAccessoryModelAsXML(Guid programGuid, Guid languageGuid)
        {
            string progarmAccessoryModelXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetProgramLoginAndPasswordReminderAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGUID", programGuid);
                command.Parameters.AddWithValue("languageGUID", languageGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        progarmAccessoryModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return progarmAccessoryModelXML;
        }

        //public string GetTipMessagePageAsXML(Guid programGuid, Guid userGuid, Guid LanguageGuid, string message)
        //{
        //    string tipMesagePageXML = string.Empty;
        //    using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
        //    {
        //        connection.Open();
        //        EntityCommand command = new EntityCommand();
        //        command.Connection = connection;
        //        command.CommandText = "Entities.GetTipMessagePageAsXML";
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("programGuid", programGuid);
        //        command.Parameters.AddWithValue("userGuid", userGuid);
        //        command.Parameters.AddWithValue("languageGuid", LanguageGuid);
        //        command.Parameters.AddWithValue("message", message);
        //        try
        //        {
        //            EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
        //            while (dr.Read())
        //            {
        //                tipMesagePageXML += dr.GetString(0);
        //            }
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }

        //    return tipMesagePageXML;
        //}

        public string GetProgramSessionEndingModelAsXML(Guid programGuid, Guid languageGuid, Guid userGuid, int IsRegistered)
        {
            string progarmSessionEndingModelXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetSessionEndingModelAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGUID", programGuid);
                command.Parameters.AddWithValue("languageGUID", languageGuid);
                command.Parameters.AddWithValue("userGUID", userGuid);
                command.Parameters.AddWithValue("Day", IsRegistered);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        progarmSessionEndingModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return progarmSessionEndingModelXML;
        }

        public string GetProgramUserVariableAsXML(Guid programGuid)
        {
            StringBuilder progarmUserVariableXML = new StringBuilder();
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.CommandTimeout = 20000;
                command.Connection = connection;
                command.CommandText = "Entities.GetProgramUserVariable";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProgramGUID", programGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        progarmUserVariableXML.Append(dr.GetString(0));
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return progarmUserVariableXML.ToString();
        }

        public string GetEmptySessionXML(Guid programGuid, Guid userGuid, Guid languageGuid)
        {
            string progarmSessionEndingModelXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetEmptySessionXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("programGuid", programGuid);
                command.Parameters.AddWithValue("userGuid", userGuid);
                command.Parameters.AddWithValue("languageGuid", languageGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        progarmSessionEndingModelXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return progarmSessionEndingModelXML;
        }

        //DTD-1507,worklog.
        //2. Need to modify SP "UpdatePageVariableInPageAndPageQuestion", there might have 2 "Gender" variable in the old programs, 
        //so we need not only use variable name but also use variable type to get the correct variableguid.
        public void UpdatePageVariableAfterCopyProgram(Guid oldProgramGuid, Guid newProgramGuid, string variableName, string variableType)
        {
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.UpdatePageVariableInPageAndPageQuestion";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("oldProgramGUID", oldProgramGuid);
                command.Parameters.AddWithValue("newProgarmGUID", newProgramGuid);
                command.Parameters.AddWithValue("pageVariableName", variableName);
                command.Parameters.AddWithValue("pageVariableType", variableType);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateProgramRoomAfterCopyProgram(Guid oldProgramGuid, Guid newProgramGuid, string roomName)
        {
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.UpdateProgramRoomInSessionContent";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("oldProgramGUID", oldProgramGuid);
                command.Parameters.AddWithValue("newProgarmGUID", newProgramGuid);
                command.Parameters.AddWithValue("programRoomName", roomName);
                command.ExecuteNonQuery();
            }
        }

        public List<PageContent> GetPageContentByProgram(Guid ProgramGuid)
        {
            List<PageContent> list = new List<PageContent>();
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.GetPageContentByProgram";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProgramGuid", ProgramGuid);
                try
                {

                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        list.Add(new PageContent
                        {
                            PageGUID = new Guid(dr["PageGUID"].ToString()),
                            //LanguageGUID = new Guid(dr["LanguageGUID"].ToString()),
                            Heading = dr["Heading"].ToString(),
                            Body = dr["Body"].ToString(),
                            PresenterImagePosition = dr["PresenterImagePosition"].ToString(),
                            //PresenterImageGuid
                            //Resource1 = new Resource
                            //{
                            //    ResourceGUID = new Guid(dr["PresenterImageGUID"].ToString()),
                            //},
                            //Resource = new Resource
                            //{
                            //    ResourceGUID = new Guid(dr["BackgroundImageGUID"].ToString()),
                            //},
                            PrimaryButtonCaption = dr["PrimaryButtonCaption"].ToString(),
                            PrimaryButtonActionParameter = dr["PrimaryButtonActionParameter"].ToString(),
                            AfterShowExpression = dr["AfterShowExpression"].ToString(),
                            BeforeShowExpression = dr["BeforeShowExpression"].ToString(),
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

        public List<PageQuestionContent> GetQuestionByProgram(Guid ProgramGuid)
        {
            List<PageQuestionContent> list = new List<PageQuestionContent>();
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetQuestionByProgram";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProgramGuid", ProgramGuid);
                try
                {

                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        list.Add(new PageQuestionContent
                        {
                            PageQuestionGUID = new Guid(dr["PageQuestionGUID"].ToString()),
                            //LanguageGUID = new Guid(dr["LanguageGUID"].ToString()),
                            Caption = dr["Caption"].ToString(),
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

        public List<PageQuestionItemContent> GetQuestionItemByProgram(Guid ProgramGuid)
        {
            List<PageQuestionItemContent> list = new List<PageQuestionItemContent>();
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetQuestionItemByProgram";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProgramGuid", ProgramGuid);
                try
                {

                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        list.Add(new PageQuestionItemContent
                        {
                            PageQuestionItemGUID = new Guid(dr["PageQuestionItemGUID"].ToString()),
                            //LanguageGUID = new Guid(dr["LanguageGUID"].ToString()),
                            Item = dr["Item"].ToString(),
                            Feedback = dr["Feedback"].ToString(),
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

        public List<Program> GetProgramByPageAndLanguageGUID(Guid PageGuid, Guid LanguageGuid)
        {
            List<Program> list = new List<Program>();
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetProgramByPageAndLanguageGUID";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("PageGUID", PageGuid);
                command.Parameters.AddWithValue("LanguageGUID", LanguageGuid);
                try
                {

                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        list.Add(new Program
                        {
                            Name = dr["Name"].ToString(),
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

        public int SearchActivityLogNumber(string condition)
        {
            int countOfLogs = 0;

            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.SearchActivityLogNumber";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Where", condition);
                try
                {
                    countOfLogs = Convert.ToInt32(command.ExecuteScalar());
                }
                finally
                {
                    connection.Close();
                }
            }
            return countOfLogs;
        }

        public DataTable GetProgramUserPageVariable(Guid programGuid)
        {
            DataTable programUserPageVariable = new DataTable();
            string sqlString = @"
            -- GetProgramUserPageVariable Method in PageVariableService.cs
            --DECLARE @programGuid uniqueidentifier;
            DECLARE @S NVARCHAR(MAX);
            SET @S=''
            --SET @programGuid = '35E19FBB-5078-497D-B608-2F6650DAE7A6'

            SELECT @S = @S + ',' + QUOTENAME(Name) + '=MAX(CASE WHEN Name='+QUOTENAME(Name,'''')+' THEN CONVERT(NVARCHAR(4000),Value) ELSE NULL END)'
            FROM  
	            (SELECT U.UserGUID, 
	            U.Email, 
	            C.Name AS Company,
	            PV.Name, 
	            (CASE WHEN UPV.Value IS NULL 
		            THEN CASE WHEN QAV.UserInput IS NULL  
			            THEN CONVERT(VARCHAR,PQI.Score) 
			            ELSE QAV.UserInput END 
		            ELSE UPV.Value END) AS Value 
	            FROM [User] U
	            JOIN ProgramUser PU ON PU.UserGUID = U.UserGUID
	            LEFT JOIN Company C ON PU.Company = C.CompanyGUID
	            LEFT JOIN PageVariable PV ON U.ProgramGUID = PV.ProgramGUID
	            LEFT JOIN UserPageVariable UPV ON UPV.UserGUID = U.UserGUID AND UPV.PageVariableGUID = PV.PageVariableGUID
	            LEFT JOIN QuestionAnswerValue QAV ON QAV.QuestionAnswerGUID = UPV.QuestionAnswerGUID
	            LEFT JOIN PageQuestionItem PQI ON PQI.PageQuestionItemGUID = QAV.PageQuestionItemGUID
	            WHERE U.ProgramGUID = @programGuid 
	            AND PV.PageVariableType = 'Program' 
	            AND (U.IsDeleted IS NULL OR U.IsDeleted = 0) 
	            AND U.Email NOT LIKE 'ChangeTechTemp%' 
	            ) AS Variable
            GROUP BY Name
            ORDER BY Name

            --SELECT @S

            EXEC('SELECT UserGUID, 
            Email,
            Company' + @S + 
            ' FROM 
            (SELECT U.UserGUID, 
	            U.Email, 
	            C.Name AS Company,
	            PV.Name, 
	            (CASE WHEN UPV.Value IS NULL 
		            THEN CASE WHEN QAV.UserInput IS NULL  
			            THEN CONVERT(VARCHAR,PQI.Score) 
			            ELSE QAV.UserInput END 
		            ELSE UPV.Value END) AS Value 
	            FROM [User] U
	            JOIN ProgramUser PU ON PU.UserGUID = U.UserGUID
	            LEFT JOIN Company C ON PU.Company = C.CompanyGUID
	            LEFT JOIN PageVariable PV ON U.ProgramGUID = PV.ProgramGUID
	            LEFT JOIN UserPageVariable UPV ON UPV.UserGUID = U.UserGUID AND UPV.PageVariableGUID = PV.PageVariableGUID
	            LEFT JOIN QuestionAnswerValue QAV ON QAV.QuestionAnswerGUID = UPV.QuestionAnswerGUID
	            LEFT JOIN PageQuestionItem PQI ON PQI.PageQuestionItemGUID = QAV.PageQuestionItemGUID
	            WHERE U.ProgramGUID = ''' + @programGuid + '''
	            AND PV.PageVariableType = ''Program'' 
	            AND (U.IsDeleted IS NULL OR U.IsDeleted = 0) 
	            AND U.Email NOT LIKE ''ChangeTechTemp%'' 
	            ) AS Variable
            GROUP BY UserGUID,Email,Company 
            ORDER BY Email, UserGUID')           
            ";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.Text;
                command.CommandText = sqlString;
                command.Parameters.AddWithValue("programGUID", programGuid);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(programUserPageVariable);
            }

            return programUserPageVariable;
        }

        public DataTable GetProgramUserPageVariableExtension(Guid programGuid)
        {
            DataTable programUserPageVariable = new DataTable();
            string sqlString = @"
             -- GetProgramUserPageVariableExtension Method in PageVariableService.cs
            --DECLARE @programGuid uniqueidentifier;
            DECLARE @S NVARCHAR(MAX);
            SET @S=''
            --SET @programGuid = '35E19FBB-5078-497D-B608-2F6650DAE7A6'

            SELECT @S = @S + ',' + QUOTENAME(Name) + '=MAX(CASE WHEN Name='+QUOTENAME(Name,'''')+' THEN CONVERT(NVARCHAR(4000),Value) ELSE NULL END)'
            FROM  
	            (SELECT U.UserGUID, 
	            U.Email, 
	            (CASE WHEN HPOL.ProgramUserGUID IS NULL
					THEN C.Name  
					ELSE HPO.CustomerName END ) AS Company, 
	            PU.[Status] AS [Status],
	            PU.[Day] AS CurrentDay,
	            PU.StartDate AS RegisterDate,
	            U.Gender AS UserGender, 
	            PU.LastFinishDate AS LastProgramActivityDate, 
	            PV.Name, 
	            (CASE WHEN UPV.Value IS NULL 
		            THEN CASE WHEN QAV.UserInput IS NULL  
			            THEN dbo.GetSumScoreByMutipleCheckBox(U.UserGUID,PQ.QuestionGUID,PQI.PageQuestionGUID,PQI.Score) 
			            ELSE QAV.UserInput END 
		            ELSE UPV.Value END) AS Value, 
                (CASE WHEN HPOL.ProgramUserGUID IS NULL
					THEN '0'  
					ELSE '1' END ) AS [HP Order]
	            FROM [User] U
	            JOIN ProgramUser PU ON PU.UserGUID = U.UserGUID
	            LEFT JOIN Company C ON PU.Company = C.CompanyGUID
	            LEFT JOIN PageVariable PV ON U.ProgramGUID = PV.ProgramGUID
	            LEFT JOIN UserPageVariable UPV ON UPV.UserGUID = U.UserGUID AND UPV.PageVariableGUID = PV.PageVariableGUID
	            LEFT JOIN QuestionAnswerValue QAV ON QAV.QuestionAnswerGUID = UPV.QuestionAnswerGUID
	            LEFT JOIN PageQuestionItem PQI ON PQI.PageQuestionItemGUID = QAV.PageQuestionItemGUID
                LEFT JOIN PageQuestion PQ ON PQ.PageQuestionGUID=PQI.PageQuestionGUID
                LEFT JOIN HPOrderLicence HPOL ON HPOL.ProgramUserGUID = PU.ProgramUserGUID
                LEFT JOIN HPOrder HPO ON HPO.HPOrderGUID = HPOL.HPOrderGUID
	            WHERE U.ProgramGUID = @programGuid 
	            AND PV.PageVariableType = 'Program' 
	            AND (U.IsDeleted IS NULL OR U.IsDeleted = 0)
                AND (HPOL.IsDeleted IS NULL OR HPOL.IsDeleted = 0)
	            AND (HPO.IsDeleted IS NULL OR HPO.IsDeleted = 0) 
	            --AND U.Email NOT LIKE 'ChangeTechTemp%' 
	            --AND U.EMAIL = 'kenneth.wergeland@akan.no'
	            --ORDER BY U.Email, U.UserGUID, PV.Name
	            ) AS Variable
            GROUP BY Name
            ORDER BY Name

            --SELECT @S

            EXEC('SELECT UserGUID, 
            Email,
            Company,
            [Status],
            CurrentDay,
            RegisterDate,
            UserGender,
            LastProgramActivityDate,
            [HP Order]' + @S + 
            ' FROM 
            (SELECT U.UserGUID, 
	            U.Email, 
	            (CASE WHEN HPOL.ProgramUserGUID IS NULL
					THEN C.Name  
					ELSE HPO.CustomerName END ) AS Company,
	            PU.[Status] AS [Status],
	            PU.[Day] AS CurrentDay,
	            PU.StartDate AS RegisterDate,
	            U.Gender AS UserGender, 
	            PU.LastFinishDate AS LastProgramActivityDate, 
	            PV.Name, 
	            (CASE WHEN UPV.Value IS NULL 
		            THEN CASE WHEN QAV.UserInput IS NULL  
			            THEN dbo.GetSumScoreByMutipleCheckBox(U.UserGUID,PQ.QuestionGUID,PQI.PageQuestionGUID,PQI.Score)
			            ELSE QAV.UserInput END 
		            ELSE UPV.Value END) AS Value,
                (CASE WHEN HPOL.ProgramUserGUID IS NULL
					THEN ''0''  
					ELSE ''1'' END ) AS [HP Order]  
	            FROM [User] U
	            JOIN ProgramUser PU ON PU.UserGUID = U.UserGUID
	            LEFT JOIN Company C ON PU.Company = C.CompanyGUID
	            LEFT JOIN PageVariable PV ON U.ProgramGUID = PV.ProgramGUID
	            LEFT JOIN UserPageVariable UPV ON UPV.UserGUID = U.UserGUID AND UPV.PageVariableGUID = PV.PageVariableGUID
	            LEFT JOIN QuestionAnswerValue QAV ON QAV.QuestionAnswerGUID = UPV.QuestionAnswerGUID
	            LEFT JOIN PageQuestionItem PQI ON PQI.PageQuestionItemGUID = QAV.PageQuestionItemGUID
                LEFT JOIN PageQuestion PQ ON PQ.PageQuestionGUID=PQI.PageQuestionGUID
                LEFT JOIN HPOrderLicence HPOL ON HPOL.ProgramUserGUID = PU.ProgramUserGUID
                LEFT JOIN HPOrder HPO ON HPO.HPOrderGUID = HPOL.HPOrderGUID 
	            WHERE U.ProgramGUID = ''' + @programGuid + '''
	            AND PV.PageVariableType = ''Program'' 
	            AND (U.IsDeleted IS NULL OR U.IsDeleted = 0)
                AND (HPOL.IsDeleted IS NULL OR HPOL.IsDeleted = 0)
	            AND (HPO.IsDeleted IS NULL OR HPO.IsDeleted = 0)  
	            ) AS Variable
            GROUP BY UserGUID,Email,Company,[Status],CurrentDay,RegisterDate,UserGender,LastProgramActivityDate,[HP Order] 
            ORDER BY Email, UserGUID')
            ";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.Text;
                command.CommandText = sqlString;
                command.Parameters.AddWithValue("programGUID", programGuid);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(programUserPageVariable);
            }

            return programUserPageVariable;
        }

        //public DataTable GetUserPageVariableValueAsDataTable(Guid programGuid)//Now the storage procedure has null Email user, it need be resolved.
        //{
        //    DataTable userPagevariableValue = new DataTable();
        //    string sqlString = "select [user].Email, Company.Name as Company, PageVariable.Name, (case when UserPageVariable.Value is null then case when QuestionAnswerValue.UserInput is null then convert(varchar,PageQuestionItem.Score) else QuestionAnswerValue.UserInput end else UserPageVariable.Value end) as value from [User]";
        //    sqlString += " left join programuser on programuser.userguid=[user].userguid";
        //    sqlString += " left join company on programuser.company = company.companyguid";
        //    sqlString += " left join PageVariable on [user].ProgramGUID=PageVariable.ProgramGUID";
        //    sqlString += " left join UserPageVariable on UserPageVariable.UserGUID=[user].UserGUID and UserPageVariable.PageVariableGUID=PageVariable.PageVariableGUID";
        //    sqlString += " left join QuestionAnswerValue on QuestionAnswerValue.QuestionAnswerGUID = UserPageVariable.QuestionAnswerGUID";
        //    sqlString += " left join PageQuestionItem on PageQuestionItem.PageQuestionItemGUID = QuestionAnswerValue.PageQuestionItemGUID";
        //    sqlString += " where [user].ProgramGUID=@programGUID and PageVariable.PageVariableType='Program' and ([user].isdeleted=0 or [user].isdeleted is null) and [user].email not like 'ChangeTechTemp%' order by [user].email, PageVariable.Name";

        //    string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
        //    using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = connection;
        //        command.CommandTimeout = 20000;
        //        command.CommandType = CommandType.Text;
        //        command.CommandText = sqlString;
        //        command.Parameters.AddWithValue("programGUID", programGuid);
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);
        //        adapter.Fill(userPagevariableValue);
        //    }

        //    return userPagevariableValue;
        //}

        //public DataTable GetUserPageVariableValueExtensionAsDataTable(Guid programGuid)//compared with GetUserPageVariableValueAsDataTable, add 5 new columns. This is new.
        //{
        //    DataTable userPagevariableValue = new DataTable();
        //    string sqlString = "select [user].Email, Company.Name as Company,programuser.Status as Status,programuser.Day as CurrentDay,programuser.StartDate as RegisterDate,[user].Gender as UserGender, programuser.LastFinishDate as LastProgramActivityDate, PageVariable.Name, (case when UserPageVariable.Value is null then case when QuestionAnswerValue.UserInput is null then convert(varchar,PageQuestionItem.Score) else QuestionAnswerValue.UserInput end else UserPageVariable.Value end) as value from [User]";
        //    sqlString += " left join programuser on programuser.userguid=[user].userguid";
        //    sqlString += " left join company on programuser.company = company.companyguid";
        //    sqlString += " left join PageVariable on [user].ProgramGUID=PageVariable.ProgramGUID";
        //    sqlString += " left join UserPageVariable on UserPageVariable.UserGUID=[user].UserGUID and UserPageVariable.PageVariableGUID=PageVariable.PageVariableGUID";
        //    sqlString += " left join QuestionAnswerValue on QuestionAnswerValue.QuestionAnswerGUID = UserPageVariable.QuestionAnswerGUID";
        //    sqlString += " left join PageQuestionItem on PageQuestionItem.PageQuestionItemGUID = QuestionAnswerValue.PageQuestionItemGUID";
        //    sqlString += " where [user].ProgramGUID=@programGUID and PageVariable.PageVariableType='Program' and ([user].isdeleted=0 or [user].isdeleted is null) and [user].email not like 'ChangeTechTemp%' order by [user].email, PageVariable.Name";

        //    string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
        //    using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = connection;
        //        command.CommandTimeout = 20000;
        //        command.CommandType = CommandType.Text;
        //        command.CommandText = sqlString;
        //        command.Parameters.AddWithValue("programGUID", programGuid);
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);
        //        adapter.Fill(userPagevariableValue);
        //    }

        //    return userPagevariableValue;
        //}

        public List<ActivityLog> SearchActivityLog(string condition, int startNumber, int endNumber)
        {
            List<ActivityLog> list = new List<ActivityLog>();

            string sqlStr = "select t.* from ";
            sqlStr += "(select activitylog.*, row_number() over (order by ID asc) as LocalIndex from dbo.activitylog LEFT JOIN ProgramUser ON ActivityLog.UserGuid = ProgramUser.UserGuid and ActivityLog.ProgramGuid = ProgramUser.ProgramGuid {0}) as t";
            sqlStr += " where t.localIndex between {1} and {2}";
            sqlStr = string.Format(sqlStr, condition, startNumber, endNumber);

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = sqlStr;
                command.CommandType = CommandType.Text;
                //command.Parameters.AddWithValue("inactiveDays", inactiveDays);
                try
                {
                    SqlDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        list.Add(
                            new ActivityLog
                            {
                                ActivityLogType = Convert.ToInt32(dr["ActivityLogType"]),
                                UserGuid = new Guid(dr["UserGuid"].ToString()),
                                ProgramGuid = new Guid(dr["ProgramGuid"].ToString()),
                                SessionGuid = new Guid(dr["SessionGuid"].ToString()),
                                ActivityDateTime = Convert.ToDateTime(dr["ActivityDateTime"].ToString()),
                                Message = dr["Message"].ToString()
                            }
                        );
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return list;
        }

        //public string GetActivityLogReport(string emaillistStr, string fileds, string days, string programGuid)
        //{
        //    string result = string.Empty;
        //    using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
        //    {
        //        connection.Open();
        //        EntityCommand command = new EntityCommand();
        //        command.Connection = connection;
        //        command.CommandText = "Entities.ActivityReport";
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("Fields", fileds);
        //        command.Parameters.AddWithValue("Emails", emaillistStr);
        //        command.Parameters.AddWithValue("Days", days);
        //        command.Parameters.AddWithValue("ProgramGUID", programGuid);
        //        try
        //        {                    
        //            EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
        //            while (dr.Read())
        //            {
        //                result += dr.GetString(0);
        //            }
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //    return result;
        //}

        public string GetTranslationData(Guid programGuid, int startDay, int endDay, bool includeRelapse, bool includeProgramRoom, bool includeProgramAccessory, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString)
        {
            string translationDataXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.GetTranslationData";

                //@ProgramGUID UNIQUEIDENTIFIER,
                //@StartDay INT,
                //@EndDay INT,
                //@IncludeRelapse bit,
                //@IncludeProgramRoom bit,
                //@IncludeProgramAccessory bit,
                //@IncludeEmailTemplate bit,
                //@IncludeHelpItem bit,
                //@IncludeUserMenu bit,
                //@IncludeTipMessage bit,
                //@IncludeSpecialString bit
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProgramGUID", programGuid);
                command.Parameters.AddWithValue("StartDay", startDay);
                command.Parameters.AddWithValue("EndDay", endDay);
                command.Parameters.AddWithValue("IncludeRelapse", includeRelapse);
                command.Parameters.AddWithValue("IncludeProgramRoom", includeProgramRoom);
                command.Parameters.AddWithValue("IncludeProgramAccessory", includeProgramAccessory);
                command.Parameters.AddWithValue("IncludeEmailTemplate", includeEmailTemplate);
                command.Parameters.AddWithValue("IncludeHelpItem", includeHelpItem);
                command.Parameters.AddWithValue("IncludeUserMenu", includeUserMenu);
                command.Parameters.AddWithValue("IncludeTipMessage", includeTipMessage);
                command.Parameters.AddWithValue("IncludeSpecialString", includeSpecialString);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        translationDataXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return translationDataXML;
        }

        //come from GetTranslationData
        public string GetTranslationDataForTranslate(Guid programGuid, int startDay, int endDay, bool includeRelapse, bool includeProgramRoom, bool includeProgramAccessory, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString, Guid fromLanguageGuid)
        {
            string translationDataXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = "Entities.GetTranslationDataForTranslate";

                //@ProgramGUID UNIQUEIDENTIFIER,
                //@StartDay INT,
                //@EndDay INT,
                //@IncludeRelapse bit,
                //@IncludeProgramRoom bit,
                //@IncludeProgramAccessory bit,
                //@IncludeEmailTemplate bit,
                //@IncludeHelpItem bit,
                //@IncludeUserMenu bit,
                //@IncludeTipMessage bit,
                //@IncludeSpecialString bit,
                //@fromLanguageGUID UNIQUEIDENTIFIER
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProgramGUID", programGuid);
                command.Parameters.AddWithValue("StartDay", startDay);
                command.Parameters.AddWithValue("EndDay", endDay);
                command.Parameters.AddWithValue("IncludeRelapse", includeRelapse);
                command.Parameters.AddWithValue("IncludeProgramRoom", includeProgramRoom);
                command.Parameters.AddWithValue("IncludeProgramAccessory", includeProgramAccessory);
                command.Parameters.AddWithValue("IncludeEmailTemplate", includeEmailTemplate);
                command.Parameters.AddWithValue("IncludeHelpItem", includeHelpItem);
                command.Parameters.AddWithValue("IncludeUserMenu", includeUserMenu);
                command.Parameters.AddWithValue("IncludeTipMessage", includeTipMessage);
                command.Parameters.AddWithValue("IncludeSpecialString", includeSpecialString);
                command.Parameters.AddWithValue("fromLanguageGUID", fromLanguageGuid);
                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        translationDataXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return translationDataXML;
        }


        public DataTable GetTranslationJobs(Guid translatorGuid)
        {
            DataTable translationJobTable = new DataTable();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetTranslationJobs";
                if (translatorGuid != Guid.Empty)
                    command.Parameters.AddWithValue("UserGuid", translatorGuid);
                else
                    command.Parameters.AddWithValue("UserGuid", DBNull.Value);

                SqlDataAdapter sa = new SqlDataAdapter(command);
                try
                {
                    sa.Fill(translationJobTable);
                }
                finally
                {
                    connection.Close();
                }
            }
            return translationJobTable;
        }

        public DataTable GetTranslationJobContents(Guid TranslationJobGUID)
        {
            DataTable translationJobContentTable = new DataTable();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetTranslationJobContents";

                command.Parameters.AddWithValue("TranslationJobGUID", TranslationJobGUID);

                SqlDataAdapter sa = new SqlDataAdapter(command);
                try
                {
                    sa.Fill(translationJobContentTable);
                }
                finally
                {
                    connection.Close();
                }
            }
            return translationJobContentTable;
        }

        public DataTable GetTranslationJobElements(Guid TranslationJobContentGUID)
        {
            DataTable translationJobElementTable = new DataTable();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetTranslationJobElements";

                command.Parameters.AddWithValue("TranslationJobContentGUID", TranslationJobContentGUID);

                SqlDataAdapter sa = new SqlDataAdapter(command);
                try
                {
                    sa.Fill(translationJobElementTable);
                }
                finally
                {
                    connection.Close();
                }
            }
            return translationJobElementTable;
        }

        public DataTable GetProgramUserReport()
        {
            DataTable programUserLog = new DataTable();
            string sqlString = "SELECT P.ProgramGUID, P.Name, R.NameOnServer, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID) AllUserSV, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND PU.[Status] = 'Screening') NotCompleteSCR, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND PU.[Status] <> 'Screening') CompleteSCR, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND PU.[Status] = 'Registered') Registered, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND PU.[Status] <> 'Screening') UsersInProg, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND PU.[Status] = 'Completed') Completed, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND PU.[Status] = 'Terminated') Terminated, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND DATEDIFF(d,PU.StartDate,GETDATE()) <= 1 ) RegisteredLast24Hours, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND DATEDIFF(w,PU.StartDate,GETDATE()) <= 1 ) RegisteredLastWeek, ";
            sqlString += "(SELECT COUNT(*) FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID WHERE U.UserType = 1 AND PU.ProgramGUID= P.ProgramGUID AND DATEDIFF(m,PU.StartDate,GETDATE()) <= 1 ) RegisteredLastMonth ";
            sqlString += "FROM Program P LEFT JOIN [Resource] R ON P.ProgramLogoGUID = R.ResourceGUID ";
            sqlString += "ORDER BY RegisteredLast24Hours DESC, RegisteredLastWeek DESC, RegisteredLastMonth DESC, P.Name";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.Text;
                command.CommandText = sqlString;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try
                {
                    adapter.Fill(programUserLog);
                }
                finally
                {
                    connection.Close();
                }
            }

            return programUserLog;
        }

        public DataTable GetPageBodyResource(Guid sessionGUID)
        {
            DataTable pageBodyResource = new DataTable();
            string sqlString = "SELECT PC.Body ";
            sqlString += "FROM PageContent PC JOIN Page P ON PC.PageGUID = P.PageGUID ";
            sqlString += "JOIN PageSequence PS ON P.PageSequenceGUID = PS.PageSequenceGUID ";
            sqlString += "JOIN SessionContent SC ON P.PageSequenceGUID = SC.PageSequenceGUID ";
            sqlString += "WHERE PC.Body LIKE '%<A%target=%</A>%' ";
            sqlString += "AND (P.IsDeleted IS NULL OR P.IsDeleted = 0) ";
            sqlString += "AND (PS.IsDeleted IS NULL OR PS.IsDeleted = 0) ";
            sqlString += "AND (PC.IsDeleted IS NULL OR PC.IsDeleted = 0) ";
            sqlString += "AND (SC.IsDeleted IS NULL OR SC.IsDeleted = 0) ";
            sqlString += "AND SC.SessionGUID = @sessionGUID ";


            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.Text;
                command.CommandText = sqlString;
                command.Parameters.AddWithValue("sessionGUID", sessionGUID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try
                {
                    adapter.Fill(pageBodyResource);
                }
                finally
                {
                    connection.Close();
                }
            }

            return pageBodyResource;
        }

        public DataTable GetSessionResource(Guid sessionGUID)
        {
            DataTable sessionResource = new DataTable();
            string sqlString = @"
                /*
                -- Test sessionGUID
                DECLARE @sessionGUID nvarchar(100);
                SET @sessionGUID = 'e3942168-04a1-4189-8b8e-9da202fcb26a';
                */
                DECLARE @pc_guid uniqueidentifier, @pc_body nvarchar(max);
                DECLARE @resource_guid uniqueidentifier;
                DECLARE @resource_type nvarchar(100);
                DECLARE @resource_name nvarchar(200);
                DECLARE @resource_nameonserver nvarchar(200);
                DECLARE @resource_originalurl nvarchar(max);
                DECLARE @resource_newurl nvarchar(max);
                DECLARE @link_start_index int;
                DECLARE @link_end_index int;
                DECLARE @containerindex int;
                DECLARE @targetindex int;
                DECLARE @parameter_target_index int;
                DECLARE @parameter_media_index int;
                DECLARE @containername nvarchar(max);

                DECLARE @changetechstorage_blob_url nvarchar(max);
                SET @changetechstorage_blob_url = 'http://changetechstorage.blob.core.windows.net/';
                DECLARE @changetech_requestresource_url nvarchar(max);
                SET @changetech_requestresource_url = 'http://changetech.cloudapp.net/RequestResource.aspx';
                DECLARE @changetech_original_image_directory_url nvarchar(max);
                SET @changetech_original_image_directory_url = 'http://changetech.cloudapp.net/originalimagecontainer/'
                DECLARE @link nvarchar(max);
                SET @link = '%<A%target=%</A>%';
                DECLARE @link_start nvarchar(max);
                SET @link_start = '<A href=''';
                DECLARE @link_end nvarchar(max);
                SET @link_end = '</A>';
                DECLARE @link_target nvarchar(max);
                SET @link_target = 'target=''_blank''';
                DECLARE @link_container nvarchar(max);
                SET @link_container = 'container';
                DECLARE @parameter_target nvarchar(max);
                SET @parameter_target = 'target=';
                DECLARE @parameter_media nvarchar(max);
                SET @parameter_media = 'media=';
                DECLARE @parameter_name nvarchar(max);
                SET @parameter_name = 'name=';

                -- Create #TempResource
                CREATE TABLE #TempResource (ResourceType nvarchar(100), Link nvarchar(max))

                -- Resources in page body
                DECLARE pagecontent_cursor CURSOR FOR
                SELECT PC.PageGUID, PC.Body
                FROM PageContent PC 
                JOIN Page P ON PC.PageGUID = P.PageGUID
                JOIN PageSequence PS ON P.PageSequenceGUID = PS.PageSequenceGUID
                JOIN SessionContent SC ON P.PageSequenceGUID = SC.PageSequenceGUID
                WHERE PC.Body LIKE @link
                AND (P.IsDeleted IS NULL OR P.IsDeleted = 0)
                AND (PS.IsDeleted IS NULL OR PS.IsDeleted = 0)
                AND (PC.IsDeleted IS NULL OR PC.IsDeleted = 0)
                AND (SC.IsDeleted IS NULL OR SC.IsDeleted = 0)
                AND SC.SessionGUID = @sessionGUID


                OPEN pagecontent_cursor;

                FETCH NEXT FROM pagecontent_cursor 
                INTO @pc_guid, @pc_body;

                -- Check @@FETCH_STATUS to see if there are any
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    --PRINT @pc_body;

                    WHILE (CHARINDEX(@link_start + @changetechstorage_blob_url, @pc_body, 0) > 0 
		                OR CHARINDEX(@link_start + @changetech_requestresource_url, @pc_body, 0) > 0)
	                BEGIN
		                IF (CHARINDEX(@link_start + @changetechstorage_blob_url, @pc_body, 0) > 0)
		                BEGIN
			                SET @link_start_index = CHARINDEX(@link_start + @changetechstorage_blob_url, @pc_body,0);
			                SET @containerindex = CHARINDEX(@link_container, @pc_body, @link_start_index);
			                SET @targetindex = CHARINDEX(@link_target, @pc_body, @link_start_index);
			                SET @link_end_index = CHARINDEX(@link_end, @pc_body, @link_start_index);
			                IF (@containerindex > 0 AND @targetindex > 0)
			                BEGIN
				                SET @containername = SUBSTRING(@pc_body, @link_start_index+56, @containerindex+9-@link_start_index-56);
				                --PRINT @containername
				                --PRINT '**********************************'
				                IF @containername = 'documentcontainer'
					                SET @resource_type = 'Document'
				                ELSE IF @containername = 'videocontainer' 
					                SET @resource_type = 'Video'
				                ELSE IF @containername = 'audiocontainer'
					                SET @resource_type = 'Audio'
				                ELSE IF @containername = 'originalimagecontainer'
					                SET @resource_type = 'Image'
				                ELSE
					                BREAK
				                --PRINT @resource_type;

				                SET @resource_nameonserver = SUBSTRING(@pc_body, @containerindex+10, @targetindex-2-@containerindex-10);
				                --PRINT @resource_nameonserver;
				
				                SELECT @resource_guid = ResourceGUID,  @resource_name = Name
				                FROM [Resource] 
				                WHERE NameOnServer = @resource_nameonserver
				
				                SET @resource_originalurl = SUBSTRING(@pc_body, @link_start_index, @link_end_index+4-@link_start_index);
				                --PRINT @resource_originalurl;
				
				                SET @resource_newurl = @link_start + @changetech_requestresource_url 
					                + '?' + @parameter_target + @resource_type 
					                + '&' + @parameter_media + @resource_nameonserver 
					                + '&' + @parameter_name + @resource_name + ''' '
					                + SUBSTRING(@pc_body, @targetindex, @link_end_index+4-@targetindex);
				                --PRINT @resource_newurl;
				
				                SET @pc_body = SUBSTRING(@pc_body, @link_end_index+4, LEN(@pc_body));
				                --PRINT @pc_body;
				                --PRINT '**********************************'
				
				                INSERT INTO #TempResource(ResourceType, Link) VALUES(@resource_type, @resource_newurl);
				                CONTINUE
			                END
			                ELSE
				                BREAK
		                END
		                ELSE IF (CHARINDEX(@link_start + @changetech_requestresource_url, @pc_body, 0) > 0)
		                BEGIN
			                SET @link_start_index = CHARINDEX(@link_start + @changetech_requestresource_url, @pc_body,0);
			                SET @link_end_index = CHARINDEX(@link_end, @pc_body, @link_start_index);
			                SET @parameter_target_index = CHARINDEX(@parameter_target, @pc_body, @link_start_index);
			                SET @parameter_media_index = CHARINDEX(@parameter_media, @pc_body, @link_start_index);
			
			                --PRINT '**********************************'
			
			                SET @resource_originalurl = SUBSTRING(@pc_body, @link_start_index, @link_end_index+4-@link_start_index);
			                --PRINT @resource_originalurl;
			
			                SET @resource_type = SUBSTRING(@pc_body, @parameter_target_index+7, @parameter_media_index-1-@parameter_target_index-7);
			                --PRINT @resource_type
			
			                SET @pc_body = SUBSTRING(@pc_body, @link_end_index+4, LEN(@pc_body));
			
			                --PRINT '**********************************'
			
			                INSERT INTO #TempResource(ResourceType, Link) VALUES(@resource_type, @resource_originalurl);
			                CONTINUE
		                END
		                ELSE
			                BREAK
	                END
                    -- This is executed as long as the previous fetch succeeds.
	                FETCH NEXT FROM pagecontent_cursor  INTO @pc_guid, @pc_body;

                END

                CLOSE pagecontent_cursor;
                DEALLOCATE pagecontent_cursor;


                -- Resources in resource
                DECLARE pageresource_cursor CURSOR FOR
                SELECT R.ResourceGUID, R.Name, R.[Type], R.NameOnServer
                FROM [Resource] R
                JOIN PageMedia PM ON R.ResourceGUID = PM.MediaGUID
                JOIN Page P ON PM.PageGUID = P.PageGUID
                JOIN PageSequence PS ON P.PageSequenceGUID = PS.PageSequenceGUID
                JOIN SessionContent SC ON P.PageSequenceGUID = SC.PageSequenceGUID
                WHERE 
                 (R.IsDeleted IS NULL OR R.IsDeleted = 0)
                AND (P.IsDeleted IS NULL OR P.IsDeleted = 0)
                AND (PM.IsDeleted IS NULL OR PM.IsDeleted = 0)
                AND (PS.IsDeleted IS NULL OR PS.IsDeleted = 0)
                AND (SC.IsDeleted IS NULL OR SC.IsDeleted = 0)
                AND SC.SessionGUID = @sessionGUID

                OPEN pageresource_cursor;

                FETCH NEXT FROM pageresource_cursor 
                INTO @resource_guid, @resource_name, @resource_type, @resource_nameonserver;

                -- Check @@FETCH_STATUS to see if there are any
                WHILE @@FETCH_STATUS = 0
                BEGIN
	                -- So far, not display image
	                IF (@resource_type = 'Image')
	                BEGIN
		                SET @resource_newurl = @link_start + @changetech_original_image_directory_url 
			                + @resource_nameonserver 
			                + ''' ' + @link_target + '>'
			                + @resource_name + @link_end;
		                --PRINT @resource_newurl;
	                END
	                ELSE
	                BEGIN
		                SET @resource_newurl = @link_start + @changetech_requestresource_url 
			                + '?' + @parameter_target + @resource_type 
			                + '&' + @parameter_media + @resource_nameonserver 
			                + '&' + @parameter_name + @resource_name 
			                + ''' ' + @link_target + '>'
			                + @resource_name + @link_end;
		                --PRINT @resource_newurl;
		                INSERT INTO #TempResource(ResourceType, Link) VALUES(@resource_type, @resource_newurl);
	                END		
	
                    -- This is executed as long as the previous fetch succeeds.
	                FETCH NEXT FROM pageresource_cursor  INTO @resource_guid, @resource_name, @resource_type, @resource_nameonserver;

                END

                CLOSE pageresource_cursor;
                DEALLOCATE pageresource_cursor;

                -- Get session resources
                SELECT * FROM #TempResource

                -- Drop #TempResource
                DROP TABLE #TempResource;
";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.Text;
                command.CommandText = sqlString;
                command.Parameters.AddWithValue("sessionGUID", sessionGUID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try
                {
                    adapter.Fill(sessionResource);
                }
                finally
                {
                    connection.Close();
                }
            }

            return sessionResource;
        }

        public int GetInactiveUserNumber(int inactiveDays)
        {
            int countOfInactiveUserNumber = 0;

            string sqlString = "SELECT COUNT(*) ";
            sqlString += "FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID ";
            sqlString += "WHERE U.UserType = 1 AND PU.[Status] <> 'Active' ";
            sqlString += "AND DATEDIFF(d,PU.LastFinishDate,GETDATE()) > @inactiveDays  ";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = sqlString;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("inactiveDays", inactiveDays);
                try
                {
                    countOfInactiveUserNumber = Convert.ToInt32(command.ExecuteScalar());
                }
                finally
                {
                    connection.Close();
                }
            }
            return countOfInactiveUserNumber;
        }

        public List<ProgramUser> GetInactiveUsers(int inactiveDays)
        {
            List<ProgramUser> list = new List<ProgramUser>();

            string sqlString = "SELECT PU.ProgramUserGUID, U.FirstName, U.Gender, U.LastName, U.MobilePhone, U.PinCode, U.Security, U.UserGUID, U.Email, U.UserType  ";
            sqlString += "FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID ";
            sqlString += "WHERE U.UserType = 1 AND PU.[Status] <> 'Active' ";
            sqlString += "AND DATEDIFF(d,PU.LastFinishDate,GETDATE()) > @inactiveDays  ";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = sqlString;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("inactiveDays", inactiveDays);
                try
                {
                    SqlDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        list.Add(new ProgramUser
                        {
                            ProgramUserGUID = new Guid(dr["ProgramUserGUID"].ToString()),
                            User = new User
                            {
                                FirstName = dr["FirstName"].ToString(),
                                Gender = dr["Gender"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                MobilePhone = dr["MobilePhone"].ToString(),
                                PinCode = dr["PinCode"].ToString(),
                                Security = Convert.ToInt32(dr["Security"].ToString()),
                                UserGUID = new Guid(dr["UserGUID"].ToString()),
                                Email = dr["Email"].ToString(),
                                UserType = (!string.IsNullOrEmpty(dr["UserType"].ToString())) ? 1 : Convert.ToInt32(dr["UserType"].ToString())
                            }
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

        public int GetLoginUserNumber(int loginMinutes)
        {
            int countOfLoginUserNumber = 0;

            string sqlString = "SELECT COUNT(*) ";
            sqlString += "FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID ";
            sqlString += "WHERE U.UserType = 1 ";
            sqlString += "AND DATEDIFF(n,U.LastLogon,GETDATE()) <= @loginMinutes  ";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = sqlString;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("loginMinutes", loginMinutes);
                try
                {
                    countOfLoginUserNumber = Convert.ToInt32(command.ExecuteScalar());
                }
                finally
                {
                    connection.Close();
                }
            }
            return countOfLoginUserNumber;
        }

        public int GetRegisteredUserNumber(int registeredDays)
        {
            int countOfInactiveUserNumber = 0;

            string sqlString = "SELECT COUNT(*) ";
            sqlString += "FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID ";
            sqlString += "WHERE U.UserType = 1 ";
            sqlString += "AND DATEDIFF(d,PU.StartDate,GETDATE()) <= @registeredDays  ";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = sqlString;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("registeredDays", registeredDays);
                try
                {
                    countOfInactiveUserNumber = Convert.ToInt32(command.ExecuteScalar());
                }
                finally
                {
                    connection.Close();
                }
            }
            return countOfInactiveUserNumber;
        }

        //TODO: not finished, should return table from the sql(get users who missed today's class).
        public List<ProgramUser> GetMissedClassUsers(int missedDays, int pageNumber, int pageSize, out int totalCount)
        {
            List<ProgramUser> list = new List<ProgramUser>();
            totalCount = 0;
            string sqlString = "SELECT * FROM (  ";
            sqlString += "SELECT PU.ProgramUserGUID, U.FirstName, U.Gender, U.LastName, ";
            sqlString += "U.MobilePhone, U.PinCode, U.Security, U.UserGUID, U.Email, U.UserType,  ";
            sqlString += "(dbo.ShouldDoDay(PU.ProgramUserGUID, GETDATE()) - ISNULL(PU.[Day],0)) MissedDay,  ";
            sqlString += "ROW_NUMBER() OVER (ORDER BY U.Email) AS num   ";
            sqlString += "FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID ";
            sqlString += "WHERE U.UserType = 1 AND (PU.[Status] = 'Active' OR PU.[Status] = 'Paused') ";
            sqlString += "AND dbo.ShouldDoDay(PU.ProgramUserGUID, GETDATE()) - PU.[Day] >= @missedDays  ";
            sqlString += ") AS A WHERE num BETWEEN @pageSize*(@pageNumber-1)+1 AND @pageSize*@pageNumber  ";

            sqlString += "SELECT COUNT(*) TotalCount ";
            sqlString += "FROM ProgramUser PU JOIN [USER] U ON PU.UserGUID = U.UserGUID ";
            sqlString += "WHERE U.UserType = 1 AND (PU.[Status] = 'Active' OR PU.[Status] = 'Paused') ";
            sqlString += "AND dbo.ShouldDoDay(PU.ProgramUserGUID, GETDATE()) - PU.[Day] >= @missedDays  ";

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = sqlString;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("missedDays", missedDays);
                command.Parameters.AddWithValue("pageSize", pageSize);
                command.Parameters.AddWithValue("pageNumber", pageNumber);
                try
                {
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            list.Add(new ProgramUser
                            {
                                ProgramUserGUID = new Guid(dr["ProgramUserGUID"].ToString()),
                                User = new User
                                {
                                    FirstName = dr["FirstName"].ToString(),
                                    Gender = dr["Gender"].ToString(),
                                    LastName = dr["LastName"].ToString(),
                                    MobilePhone = dr["MobilePhone"].ToString(),
                                    PinCode = dr["PinCode"].ToString(),
                                    Security = Convert.ToInt32(dr["Security"].ToString()),
                                    UserGUID = new Guid(dr["UserGUID"].ToString()),
                                    Email = dr["Email"].ToString(),
                                    UserType = (!string.IsNullOrEmpty(dr["UserType"].ToString())) ? 1 : Convert.ToInt32(dr["UserType"].ToString())
                                }
                            });
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            totalCount = Convert.ToInt32(dr["TotalCount"].ToString());
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return list;
        }

        public List<string> GetPageContentBySessionGUID(Guid sessionGuid)
        {
            List<string> list = new List<string>();
            string sqlString = @"
                        SELECT pc.Body FROM PageContent pc
                        JOIN Page pg ON pc.PageGUID=pg.PageGUID 
                        --JOIN PageSequence ps ON pg.PageSequenceGUID=ps.PageSequenceGUID
                        JOIN SessionContent sc ON pg.PageSequenceGUID=sc.PageSequenceGUID 
                        --JOIN [Session] s ON sc.SessionGUID=s.SessionGUID
                        WHERE sc.SessionGUID=@sessionGuid 
                        AND (sc.IsDeleted is null OR sc.IsDeleted=0)
                        AND (pg.IsDeleted is null OR pg.IsDeleted=0)
                        --AND (s.IsDeleted is null OR s.IsDeleted=0)
                        --AND (ps.IsDeleted is null OR ps.IsDeleted=0)
                        AND (pc.IsDeleted is null OR pc.IsDeleted=0)";
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandText = sqlString;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("sessionGuid", sessionGuid);
                try
                {
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            list.Add(dr["Body"].ToString());
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return list;
        }

        public DataTable GetAllInformationForReminderEmailBeforeMailtime(DateTime Now)
        {
            DataTable reminderEmailInfo = new DataTable();

            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllInformationForReminderEmailBeforeMailtime";
                command.Parameters.AddWithValue("MailTime", Now);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(reminderEmailInfo);
            }

            return reminderEmailInfo;
        }

        public DataTable GetProgramDailySMSContentList(Guid ProgramGuid)
        {
            DataTable programDailySMSContentTable = new DataTable();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetProgramDailySMSContentList";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("ProgramGuid", ProgramGuid);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(programDailySMSContentTable);
            }
            return programDailySMSContentTable;
        }

        public void ExistProgramDailySMSListIntoShortMessageQueue(DateTime Now)
        {
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "ExistProgramDailySMSListIntoShortMessageQueue";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("MailTime", Now);

                command.ExecuteNonQuery();
            }
        }

        public void ExistRemindReportSMSListIntoShortMessageQueue(DateTime Now, int timeSpanMinutes)
        {
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "ExistRemindReportSMSListIntoShortMessageQueue";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("MailTime", Now);
                command.Parameters.AddWithValue("TimeSpanMinutes", timeSpanMinutes);

                command.ExecuteNonQuery();
            }
        }

        public bool GetIsNeedReport(Guid programGuid,Guid userGuid,DateTime now)
        {
            bool IsNeedReport = false;
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetIsNeedReport";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("ProgramGuid", programGuid);
                command.Parameters.AddWithValue("UserGuid", userGuid);
                command.Parameters.AddWithValue("MailTime", now);

                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        IsNeedReport = (bool)dr[0];
                        break;
                    }
                }

            }
            return IsNeedReport;
        }

        public bool GetIsNeedHelp(Guid programGuid, Guid userGuid, DateTime now)
        {
            bool IsNeedHelp = false;
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetIsNeedHelp";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("ProgramGuid", programGuid);
                command.Parameters.AddWithValue("UserGuid", userGuid);
                command.Parameters.AddWithValue("MailTime", now);

                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        IsNeedHelp = (bool)dr[0];
                        break;
                    }
                }

            }
            return IsNeedHelp;
        }

        public DataTable GetPageVariableUsedTimesByProgramGuid(Guid programGuid, Guid pageVariableGuid, string oldVariableName)
        {
            DataTable pageVariableUsedTimesTable = new DataTable();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetPageVariableUsedTimesByProgramGuid";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("programGuid", programGuid);
                command.Parameters.AddWithValue("pageVariableGuid", pageVariableGuid);
                command.Parameters.AddWithValue("oldVariableName", oldVariableName);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(pageVariableUsedTimesTable);
            }
            return pageVariableUsedTimesTable;
        }

        public DataTable UpdatePageVariableNameByProgramGuidAndPageVariableGuid(Guid programGuid, Guid pageVariableGuid,string newVariableName , string oldVariableName)
        {
            DataTable messageTable = new DataTable();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "UpdatePageVariableNameByProgramGuidAndPageVariableGuid";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("programGuid", programGuid);
                command.Parameters.AddWithValue("pageVariableGuid", pageVariableGuid);
                command.Parameters.AddWithValue("newVariableName", newVariableName);
                command.Parameters.AddWithValue("oldVariableName", oldVariableName);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(messageTable);
            }
            return messageTable;
        }

        // Get ShouldDoDay from DB function
        public int ShouldDoDay(Guid programUserGuid, DateTime now)
        {
            int shouldDoDay = 0;
            string sqlConnectionString = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection=new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                //select  [dbo].[ShouldDoDay]('C759ED18-FC2A-4449-8CF4-7A33165DD3C3',getdate())
                command.CommandText = "select [dbo].[ShouldDoDay](@programUserGuid,@datetime)";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("programUserGuid", programUserGuid);
                command.Parameters.AddWithValue("datetime",now);
                try
                {
                     shouldDoDay = Convert.ToInt32(command.ExecuteScalar());
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
                return shouldDoDay;
            }
        }

        public string GetPageGraphAsXML(Guid pageGUID)
        {
            string pageGraphXML = string.Empty;
            using (EntityConnection connection = new EntityConnection(DefaultEntitiesConnectionString))
            {
                connection.Open();
                EntityCommand command = new EntityCommand();
                command.Connection = connection;
                command.CommandText = "Entities.GetPageGraphAsXML";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("PageGuid", pageGUID);

                try
                {
                    EntityDataReader dr = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (dr.Read())
                    {
                        pageGraphXML += dr.GetString(0);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return pageGraphXML;
        }

        //the services provider for ctpp
        public List<SessionPageBody> GetAllPageBodyList(Guid ProgramGUID)
        {
            List<SessionPageBody> sessionPageBodyList = new List<SessionPageBody>();
            string sqlConnectionString = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"SELECT s.sessionGUID,pc.Body FROM PageContent pc
                                        JOIN Page pg ON pc.PageGUID=pg.PageGUID                       
                                        JOIN SessionContent sc ON pg.PageSequenceGUID=sc.PageSequenceGUID 
                                        JOIN [Session] s ON sc.SessionGUID=s.SessionGUID
                                        WHERE s.programguid = @ProgramGUID 
                                        AND (s.IsDeleted is null OR s.IsDeleted=0)
                                        AND (sc.IsDeleted is null OR sc.IsDeleted=0)
                                        AND (pg.IsDeleted is null OR pg.IsDeleted=0)                        
                                        AND (pc.IsDeleted is null OR pc.IsDeleted=0)
                                        Order By s.SessionGUID";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@ProgramGUID", ProgramGUID);
                try
                {
                    SqlDataReader sdr = command.ExecuteReader();
                    while (sdr.Read())
                    {
                        SessionPageBody sPageBody = new SessionPageBody();
                        sPageBody.SessionGUID = new Guid(sdr["sessionGUID"].ToString());
                        sPageBody.PageBody = sdr["Body"].ToString();
                        sessionPageBodyList.Add(sPageBody);
                    }
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
                return sessionPageBodyList;
            }
        }
        public List<SessionPageMediaResource> GetAllPageMediaResource(Guid ProgramGUID)
        {
            List<SessionPageMediaResource> sPageMediaResourceList = new List<SessionPageMediaResource>();
            string sqlConnectionString = GetSQLConnectionString("ChangeTechConnectionString");
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"SELECT s.sessionGUID,pm.MediaGUID,r.Name,r.NameOnServer,r.[Type] FROM dbo.PageMedia pm
                                        JOIN Page pg ON pm.PageGUID=pg.PageGUID                        
                                        JOIN SessionContent sc ON pg.PageSequenceGUID=sc.PageSequenceGUID 
                                        JOIN [Session] s ON sc.SessionGUID=s.SessionGUID
                                        left join [Resource] r on r. ResourceGUID = pm.MediaGUID
                                        WHERE s.programguid = @ProgramGUID 
                                        AND (s.IsDeleted is null OR s.IsDeleted=0)
                                        AND (sc.IsDeleted is null OR sc.IsDeleted=0)
                                        AND (pg.IsDeleted is null OR pg.IsDeleted=0)                       
                                        AND (pm.IsDeleted is null OR pm.IsDeleted=0)
                                        Order By s.SessionGUID";
                command.CommandTimeout = 20000;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@ProgramGUID", ProgramGUID);
                try
                {
                    SqlDataReader sdr = command.ExecuteReader();
                    while (sdr.Read())
                    {
                        SessionPageMediaResource sPageMediaResource = new SessionPageMediaResource();
                        sPageMediaResource.SessionGUID = new Guid(sdr["sessionGUID"].ToString());
                        sPageMediaResource.MediaGUID = new Guid(sdr["MediaGUID"].ToString());
                        sPageMediaResource.Name = sdr["Name"].ToString();
                        sPageMediaResource.NameOnServer = sdr["NameOnServer"].ToString();
                        sPageMediaResource.Type = sdr["Type"].ToString();
                        sPageMediaResourceList.Add(sPageMediaResource);
                    }
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
                return sPageMediaResourceList;
            }
        }
        #endregion
    }
}
