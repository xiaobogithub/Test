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
    public class ExpressionRepository : RepositoryBase, IExpressionRepository
    {
        public IQueryable<Expression> GetExpressionOfGroup(Guid expressionGroupGUID)
        {
            return GetEntities<Expression>().Where(e => e.ExpressionGroup.ExpressionGroupGUID == expressionGroupGUID);
        }

        public IQueryable<Expression> GetExpressionOfGroup(string expressionGroupName)
        {
            return GetEntities<Expression>().Where(e => e.ExpressionGroup.Name.Equals(expressionGroupName));
        }

        public IQueryable<Expression> GetExpressionOfProgram(Guid programGuid)
        {
            return GetEntities<Expression>().Where(e => e.ExpressionGroup.Program.ProgramGUID == programGuid);
        }

        public Expression GetExpression(Guid expressionGUID)
        {
            return GetEntities<Expression>().Where(e => e.ExpressionGUID == expressionGUID).FirstOrDefault();
        }

        public void AddExpression(Expression expression)
        {
            InsertEntity(expression);
        }

        public void UpdateExpression(Expression expression)
        {
            UpdateEntity(expression);
        }

        public void DeleteExpression(Guid expressionGUID)
        {
            DeleteEntity<Expression>(e => e.ExpressionGUID == expressionGUID, Guid.Empty);
        }

        public List<string> GetExpressionPageVariableUsedInExpressionByProgram(Guid programGUID)
        {
            List<string> expressionList = new List<string>();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");

            string sqlStr = "SELECT dbo.PageContent.Heading,dbo.PageContent.Body, dbo.PageContent.FooterText, dbo.PageContent.AfterShowExpression, dbo.PageContent.BeforeShowExpression";
            sqlStr += " FROM dbo.Page INNER JOIN";
            sqlStr += " dbo.PageContent ON dbo.Page.PageGUID = dbo.PageContent.PageGUID INNER JOIN";
            sqlStr += " dbo.PageSequence ON dbo.Page.PageSequenceGUID = dbo.PageSequence.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.SessionContent ON dbo.PageSequence.PageSequenceGUID = dbo.SessionContent.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.Session ON dbo.SessionContent.SessionGUID = dbo.Session.SessionGUID INNER JOIN";
            sqlStr += " dbo.Program ON dbo.Session.ProgramGUID = Program.programGUID";
            sqlStr += " WHERE (dbo.Program.ProgramGUID = @program) AND ((dbo.PageContent.AfterShowExpression LIKE '%{V:%') OR";
            sqlStr += " (dbo.PageContent.BeforeShowExpression LIKE '%{V:%') or dbo.PageContent.Heading LIKE '%{V:%' or dbo.PageContent.Body LIKE '%{V:%' or dbo.PageContent.FooterText LIKE '%{V:%')";
            sqlStr += " and ([SessionContent].IsDeleted is null OR [SessionContent].IsDeleted=0)";
            sqlStr += " and ([Page].IsDeleted is null OR [Page].IsDeleted=0) and ([Session].IsDeleted is null OR [Session].IsDeleted=0)";

            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("program", programGUID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string heading = reader["Heading"].ToString();
                    string body = reader["Body"].ToString();
                    string footerText = reader["FooterText"].ToString();
                    string AfterShowExpression = reader["AfterShowExpression"].ToString();
                    string BeforeShowExpression = reader["BeforeShowExpression"].ToString();
                    if (heading.Contains("{V:"))
                    {
                        expressionList.Add(heading);
                    }
                    if (body.Contains("{V:"))
                    {
                        expressionList.Add(body);
                    }
                    if (footerText.Contains("{V:"))
                    {
                        expressionList.Add(footerText);
                    }
                    if (AfterShowExpression.Contains("{V:"))
                    {
                        expressionList.Add(AfterShowExpression);
                    }
                    if (BeforeShowExpression.Contains("{V:"))
                    {
                        expressionList.Add(BeforeShowExpression);
                    }
                }
            }

            return expressionList;
        }

        public List<string> GetExpressionForSession(Guid sessionGuid)
        {
            List<string> expressionList = new List<string>();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            string sqlStr = "SELECT dbo.SessionContent.PageSequenceOrderNo, dbo.Page.PageOrderNo, dbo.PageContent.AfterShowExpression, dbo.PageContent.BeforeShowExpression";
            sqlStr += " FROM dbo.Page INNER JOIN";
            sqlStr += " dbo.PageContent ON dbo.Page.PageGUID = dbo.PageContent.PageGUID INNER JOIN";
            sqlStr += " dbo.PageSequence ON dbo.Page.PageSequenceGUID = dbo.PageSequence.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.SessionContent ON dbo.PageSequence.PageSequenceGUID = dbo.SessionContent.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.Session ON dbo.SessionContent.SessionGUID = dbo.Session.SessionGUID";
            sqlStr += " WHERE (dbo.Session.SessionGUID = @session) AND ((dbo.PageContent.AfterShowExpression LIKE '%GOTO%') OR";
            sqlStr += " (dbo.PageContent.BeforeShowExpression LIKE '%GOTO%'))";
            sqlStr += " and ([SessionContent].IsDeleted is null OR [SessionContent].IsDeleted=0)";
            sqlStr += " and ([Page].IsDeleted is null OR [Page].IsDeleted=0)";
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("session", sessionGuid);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                while (reader.Read())
                {
                    string pagesequenceAndPage = reader["PageSequenceOrderNo"] + ";" + reader["PageOrderNo"] + ";";
                    string afterShowExpression = reader["AfterShowExpression"].ToString();
                    string befroeShowExpression = reader["BeforeShowExpression"].ToString();

                    if (!string.IsNullOrEmpty(afterShowExpression) && afterShowExpression.Contains("GOTO"))
                    {
                        expressionList.Add(pagesequenceAndPage + afterShowExpression);
                    }
                    if (!string.IsNullOrEmpty(befroeShowExpression) && befroeShowExpression.Contains("GOTO"))
                    {
                        expressionList.Add(pagesequenceAndPage + befroeShowExpression);
                    }
                }
            }

            return expressionList;
        }

        //get all page with expression
        public List<string> GetPagesequenceForSessionWithExpression(Guid sessionGuid)
        {
            List<string> pageList = new List<string>();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            string sqlStr = "SELECT dbo.SessionContent.PageSequenceOrderNo, dbo.Page.PageOrderNo, dbo.PageContent.AfterShowExpression, dbo.PageContent.BeforeShowExpression";
            sqlStr += " FROM dbo.Page INNER JOIN";
            sqlStr += " dbo.PageContent ON dbo.Page.PageGUID = dbo.PageContent.PageGUID INNER JOIN";
            sqlStr += " dbo.PageSequence ON dbo.Page.PageSequenceGUID = dbo.PageSequence.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.SessionContent ON dbo.PageSequence.PageSequenceGUID = dbo.SessionContent.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.Session ON dbo.SessionContent.SessionGUID = dbo.Session.SessionGUID";
            sqlStr += " WHERE (dbo.Session.SessionGUID = @session) ";
            sqlStr += " and ([SessionContent].IsDeleted is null OR [SessionContent].IsDeleted=0)";
            sqlStr += " and ([Page].IsDeleted is null OR [Page].IsDeleted=0)";
            sqlStr += " order by dbo.SessionContent.PageSequenceOrderNo, dbo.Page.PageOrderNo";
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("session", sessionGuid);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                while (reader.Read())
                {
                    string pagesequenceAndPage = reader["PageSequenceOrderNo"] + ";" + reader["PageOrderNo"];

                    string afterShowExpression = reader["AfterShowExpression"].ToString();

                    string beforeShowExpression = reader["BeforeShowExpression"].ToString();


                    if (!string.IsNullOrEmpty(beforeShowExpression) && beforeShowExpression.Contains("GOTO"))
                    {
                        pagesequenceAndPage += ";Before " + beforeShowExpression;

                    }
                    if (!string.IsNullOrEmpty(afterShowExpression) && afterShowExpression.Contains("GOTO"))
                    {
                        pagesequenceAndPage += ";After " + afterShowExpression;
                    }

                    pageList.Add(pagesequenceAndPage);

                }
            }

            return pageList;
        }

        //get all expression for relapse from a session
        public List<string> GetExpressionForRelapseFromSession(Guid sessionGuid)
        {
            List<string> expressionList = new List<string>();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            string sqlStr = "SELECT dbo.SessionContent.PageSequenceOrderNo, dbo.Page.PageOrderNo, dbo.PageContent.AfterShowExpression, dbo.PageContent.BeforeShowExpression";
            sqlStr += " FROM dbo.Page INNER JOIN";
            sqlStr += " dbo.PageContent ON dbo.Page.PageGUID = dbo.PageContent.PageGUID INNER JOIN";
            sqlStr += " dbo.PageSequence ON dbo.Page.PageSequenceGUID = dbo.PageSequence.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.SessionContent ON dbo.PageSequence.PageSequenceGUID = dbo.SessionContent.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.Session ON dbo.SessionContent.SessionGUID = dbo.Session.SessionGUID";
            sqlStr += " WHERE (dbo.Session.SessionGUID = @session) AND ((dbo.PageContent.AfterShowExpression LIKE '%GOSUB%') OR";
            sqlStr += " (dbo.PageContent.BeforeShowExpression LIKE '%GOSUB%'))";
            sqlStr += " and ([SessionContent].IsDeleted is null OR [SessionContent].IsDeleted=0)";
            sqlStr += " and ([Page].IsDeleted is null OR [Page].IsDeleted=0)";
            sqlStr += " order by dbo.SessionContent.PageSequenceOrderNo, dbo.Page.PageOrderNo";
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("session", sessionGuid);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                string pagesequenceAndPage = string.Empty;
                string afterShowExpression = string.Empty;
                string beforeShowExpression = string.Empty;
                string expression = string.Empty;
                while (reader.Read())
                {
                    pagesequenceAndPage = reader["PageSequenceOrderNo"] + ";" + reader["PageOrderNo"];
                    afterShowExpression = reader["AfterShowExpression"].ToString();
                    beforeShowExpression = reader["BeforeShowExpression"].ToString();

                    if (!string.IsNullOrEmpty(beforeShowExpression) && beforeShowExpression.Contains("GOSUB"))
                    {
                        expression = pagesequenceAndPage + ";" + beforeShowExpression;
                        expressionList.Add(expression);
                    }
                    if (!string.IsNullOrEmpty(afterShowExpression) && afterShowExpression.Contains("GOSUB"))
                    {
                        expression = pagesequenceAndPage + ";" + afterShowExpression;
                        expressionList.Add(expression);
                    }


                }
            }

            return expressionList;
        }

        //get all relapse pages  from a program
        public List<string> GetRelapsePagesequenceFromProgram(Guid programGuid)
        {
            List<string> expressionList = new List<string>();
            string sqlconnectionstring = GetSQLConnectionString("ChangeTechConnectionString");
            string sqlStr = "SELECT ps.PageSequenceGUID,pg.PageOrderNo,ps.[Name] as PagesequenceName,pc.AfterShowExpression,pc.BeforeShowExpression ";
            sqlStr += " FROM dbo.PageSequence ps INNER JOIN";
            sqlStr += " (SELECT PageSequenceGUID FROM dbo.Relapse WHERE ProgramGUID = @ProgramGuid) rl ON";
            sqlStr += " ps.PageSequenceGUID = rl.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.Page pg ON ps.PageSequenceGUID = pg.PageSequenceGUID INNER JOIN";
            sqlStr += " dbo.PageContent pc ON pg.PageGUID = pc.PageGUID";
            sqlStr += " WHERE (ps.IsDeleted IS NULL OR ps.IsDeleted=0)";
            sqlStr += " AND (pg.IsDeleted IS NULL OR pg.IsDeleted=0)";
            sqlStr += " AND (pc.IsDeleted IS NULL OR pc.IsDeleted=0)";
            sqlStr += " ORDER BY ps.PageSequenceGUID,pg.PageOrderNo";
            using (SqlConnection connection = new SqlConnection(sqlconnectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlStr, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("ProgramGuid", programGuid);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                string pagesequenceAndPage = string.Empty;
                string afterShowExpression = string.Empty;
                string beforeShowExpression = string.Empty;
                while (reader.Read())
                {
                    pagesequenceAndPage = reader["PageSequenceGUID"] + ";" + reader["PageOrderNo"] + ";" + reader["PagesequenceName"];
                    afterShowExpression = reader["AfterShowExpression"].ToString();
                    beforeShowExpression = reader["BeforeShowExpression"].ToString();

                    if (!string.IsNullOrEmpty(beforeShowExpression))
                    {
                        pagesequenceAndPage += ";Before " + beforeShowExpression;
                    }
                    if (!string.IsNullOrEmpty(afterShowExpression))
                    {
                        pagesequenceAndPage += ";After " + afterShowExpression;
                    }
                    expressionList.Add(pagesequenceAndPage);
                }
            }

            return expressionList;
        }
       
    }
}