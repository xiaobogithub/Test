using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data.SqlClient;
using System.Data;

namespace ChangeTech.SQLServerRepository
{
    public class PageVaribleRepository : RepositoryBase, IPageVaribleRepository
    {
        public IQueryable<PageVariable> GetPageVariableByProgram(Guid programGUID)
        {
            return GetEntities<PageVariable>(p => p.Program.ProgramGUID == programGUID);
        }

        //public IQueryable<PageVariable> GetPageVariableByProgram(Guid programGUID, string pageVariableType)
        //{
        //    if (pageVariableType == "Program")
        //        return GetEntities<PageVariable>(p => p.Program.ProgramGUID == programGUID && p.PageVariableType == pageVariableType).OrderBy(p => p.Name);
        //    else
        //        return GetEntities<PageVariable>(p => p.PageVariableType == pageVariableType).OrderBy(p => p.Name);
        //}

        public PageVariable GetItem(Guid pageVariableGUID)
        {
            return GetEntities<PageVariable>(v => v.PageVariableGUID == pageVariableGUID).FirstOrDefault();
        }

        public PageVariable GetVariableByProgramGuidAndVariableName(Guid programGuid, string variableName)
        {
            return GetEntities<PageVariable>(v => v.Name == variableName && v.Program.ProgramGUID == programGuid).FirstOrDefault();
        }

        public void Delete(Guid pageVariableGUID)
        {
            DeleteEntity<PageVariable>(p => p.PageVariableGUID == pageVariableGUID, Guid.Empty);
        }

        public void Edit(PageVariable pageVariable)
        {
            UpdateEntity(pageVariable);
        }

        public void Add(PageVariable pageVariable)
        {
            InsertEntity(pageVariable);
        }

        //public IQueryable<PageVariable> GetVariableByProgramGuidAndGroupGuid(Guid programGuid, Guid groupGuid)
        //{
        //    return GetEntities<PageVariable>(p => p.Program.ProgramGUID == programGuid && p.PageVariableGroup.PageVariableGroupGUID == groupGuid && p.PageVariableType == "Program");
        //}

        public PageVariable GetPageVariableByProgramGuidAndParentPageVariableGuid(Guid programGuid, Guid parentPageVariableGuid)
        {
            return GetEntities<PageVariable>(p => p.Program.ProgramGUID == programGuid).FirstOrDefault();
        }

        public void DeleteVariableOfProgram(Guid programGuid)
        {
            DeleteEntities<PageVariable>(p => p.Program.ProgramGUID == programGuid, Guid.Empty);
        }

        public List<string> GetAllPageVariableNameListByProgramGUID(Guid programGUID)
        {
            List<string> variableList = new List<string>();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            string sqlStr = "SELECT Name FROM PageVariable WHERE ProgramGUID = @programGUID";
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("programGUID", programGUID);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                while (reader.Read())
                {
                    variableList.Add(reader["Name"].ToString());
                }
            }

            return variableList;
        }      

        public List<string> GetPageVariableBindByQuestionOfProgram(Guid programGUID)
        {
            List<string> variableList = new List<string>();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            string sqlStr = "Select PageVariable.Name From Page INNER JOIN";
            sqlStr += " dbo.PageSequence ON dbo.Page.PageSequenceGUID = dbo.PageSequence.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.SessionContent ON dbo.PageSequence.PageSequenceGUID = dbo.SessionContent.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.Session ON dbo.SessionContent.SessionGUID = dbo.Session.SessionGUID INNER JOIN";
            sqlStr += " dbo.Program ON dbo.Session.ProgramGUID = Program.programGUID INNER JOIN";
            sqlStr += " PageQuestion ON PageQuestion.PageGUID = Page.PageGUID INNER JOIN";
            sqlStr += " PageVariable ON PageVariable.PageVariableGUID = PageQuestion.PageVariableGUID";
            sqlStr += " where (dbo.Program.ProgramGUID = @programGUID)";
            sqlStr += " AND ([SessionContent].IsDeleted is null OR [SessionContent].IsDeleted=0)";
            sqlStr += " AND ([Page].IsDeleted is null OR [Page].IsDeleted=0)";
            sqlStr += " AND ([Session].IsDeleted is null OR [Session].IsDeleted=0)";
            sqlStr += " AND (PageQuestion.IsDeleted is null OR PageQuestion.IsDeleted=0)";
            sqlStr += " AND PageQuestion.PagevariableGUID is not null";
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("programGUID", programGUID);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                while (reader.Read())
                {
                    variableList.Add(reader["Name"].ToString());
                }
            }

            return variableList;
        }

        public List<string> GetPageVariableBindByPageOfProgramGUID(Guid programGUID)
        {
            List<string> variableList = new List<string>();
            string sqlConnectionString = GetSQLConnectionString("ChangeTechConnectionString");
            string sqlStr = "Select PageVariable.Name From Page INNER JOIN";
            sqlStr += " dbo.PageSequence ON dbo.Page.PageSequenceGUID = dbo.PageSequence.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.SessionContent ON dbo.PageSequence.PageSequenceGUID = dbo.SessionContent.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.Session ON dbo.SessionContent.SessionGUID = dbo.Session.SessionGUID INNER JOIN";
            sqlStr += " dbo.Program ON dbo.Session.ProgramGUID = Program.programGUID INNER JOIN";
            sqlStr += " PageVariable ON PageVariable.PageVariableGUID = Page.PageVariableGUID";
            sqlStr += " where (dbo.Program.ProgramGUID = @programGUID)";
            sqlStr += " AND ([SessionContent].IsDeleted is null OR [SessionContent].IsDeleted=0)";
            sqlStr += " AND ([Page].IsDeleted is null OR [Page].IsDeleted=0)";
            sqlStr += " AND ([Session].IsDeleted is null OR [Session].IsDeleted=0)";
            sqlStr += " AND Page.PagevariableGUID is not null";
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("programGUID", programGUID);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                while (reader.Read())
                {
                    variableList.Add(reader["Name"].ToString());
                }
            }

            return variableList;
        }

        //public DataTable GetPageContentsByProgramGuid(Guid programGuid)
        //{
        //    DataTable dt = null;
        //    string sqlConnectionString = GetSQLConnectionString("ChangeTechConnectionString");
        //    string sqlStr = "Select PageContent.* from PageContent INNER JOIN ";
        //    sqlStr += " dbo.Page on dbo.Page.PageGUID =dbo.PageContent.PageGUID  INNER JOIN ";
        //    sqlStr += " dbo.PageSequence ON dbo.Page.PageSequenceGUID = dbo.PageSequence.PageSequenceGUID INNER JOIN ";
        //    sqlStr += " dbo.SessionContent ON dbo.PageSequence.PageSequenceGUID = dbo.SessionContent.PageSequenceGUID INNER JOIN ";
        //    sqlStr += " dbo.[Session] ON dbo.SessionContent.SessionGUID = dbo.[Session].SessionGUID INNER JOIN ";
        //    sqlStr += " dbo.Program ON dbo.Session.ProgramGUID = Program.programGUID  ";
        //    sqlStr += " where (dbo.Program.ProgramGUID = @programGUID)";
        //    sqlStr += " AND (Page.IsDeleted is null OR Page.IsDeleted=0)";
        //    sqlStr += " AND (PageContent.IsDeleted is null OR PageContent.IsDeleted=0)";
        //    sqlStr += " AND ([SessionContent].IsDeleted is null OR [SessionContent].IsDeleted=0)";
        //    sqlStr += " AND ([Session].IsDeleted is null OR [Session].IsDeleted=0)";
        //    sqlStr += " AND (Program.IsDeleted is null OR Program.IsDeleted=0)";
        //    using (SqlConnection connection = new SqlConnection(sqlConnectionString))
        //    {
        //        using (SqlDataAdapter adapter =new SqlDataAdapter(sqlStr,connection))
        //        {
        //            SqlCommand command = adapter.SelectCommand;
        //            command.CommandType = CommandType.Text;
        //            command.Parameters.AddWithValue("@programGUID", programGuid);
        //            dt = new DataTable();
        //            adapter.Fill(dt);
        //        }
        //    }

        //    return dt;
        //}

        public int GetPageVariableCountByProgramGuidAndPageVaribleGuid(Guid programGuid, Guid pageVaribleGuid)
        {
            int pageVariableCount = 0;
            string sqlConnectionString = GetSQLConnectionString("ChangeTechConnectionString");
            string sqlStr = "Select Count(*) From Page INNER JOIN";
            sqlStr += " dbo.PageSequence ON dbo.Page.PageSequenceGUID = dbo.PageSequence.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.SessionContent ON dbo.PageSequence.PageSequenceGUID = dbo.SessionContent.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.Session ON dbo.SessionContent.SessionGUID = dbo.Session.SessionGUID INNER JOIN";
            sqlStr += " dbo.Program ON dbo.Session.ProgramGUID = Program.programGUID INNER JOIN";
            sqlStr += " PageQuestion ON PageQuestion.PageGUID = Page.PageGUID INNER JOIN";
            sqlStr += " PageVariable ON PageVariable.PageVariableGUID = PageQuestion.PageVariableGUID";
            sqlStr += " where (dbo.Program.ProgramGUID = @programGUID)";
            sqlStr += " AND (dbo.PageVariable.PageVariableGUID =@pageVariableGUID)";
            sqlStr += " AND ([SessionContent].IsDeleted is null OR [SessionContent].IsDeleted=0)";
            sqlStr += " AND ([Page].IsDeleted is null OR [Page].IsDeleted=0)";
            sqlStr += " AND ([Session].IsDeleted is null OR [Session].IsDeleted=0)";
            sqlStr += " AND (PageQuestion.IsDeleted is null OR PageQuestion.IsDeleted=0)";
            sqlStr += " AND PageQuestion.PagevariableGUID is not null";

            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("programGUID", programGuid);
                command.Parameters.AddWithValue("pageVariableGUID", pageVaribleGuid);
                pageVariableCount = Convert.ToInt32(command.ExecuteScalar());
            }
            return pageVariableCount;
        }

    }
}
