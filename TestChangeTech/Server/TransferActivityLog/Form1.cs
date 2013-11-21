using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransferActivityLog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 1;
            DateTime startDateTime = DateTime.Now;
            while (true)
            {
                label2.Text = i.ToString();
                if (InsertIntoNewLog(i) < 100000)
                {
                    label2.Text = (DateTime.Now - startDateTime).Seconds.ToString();
                    break;
                }
                i++;

                //if (i > 10)
                //{
                //    break;
                //}
            }
        }

        public int InsertIntoNewLog(int pageNumber)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection();
            using (conn)
            {
                conn.ConnectionString = "Server=tcp:hvmgf7a4jf.database.windows.net;Database=ChangeTech;User ID=ChangeTech;Password=Huez_3daler;Trusted_Connection=False;Encrypt=True;";
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandTimeout = 360000;
                //comm.CommandText = 
                string sqlStr = "INSERT INTO [dbo].[ActivityLogBAK] ([ActivityLogGuid],[ActivityLogType],[UserGuid],[ProgramGuid],[SessionGuid],[PageSequenceGuid],[PageGuid],[ActivityDateTime],[Message],[Browser],[IP],[From]) ";
                sqlStr += "select [ActivityLogGuid],[ActivityLogType],[UserGuid],[ProgramGuid],[SessionGuid],[PageSequenceGuid],[PageGuid],[ActivityDateTime],[Message],[Browser],[IP],[From] from";
                sqlStr += " ( select p.*, row_number() over (order by activitydatetime asc) as LocalIndex from dbo.activitylog p ) as t where t.localIndex between " + ((pageNumber - 1) * 100000 + 1) + " and " + (pageNumber) * 100000;


                //string sqlStr = "INSERT INTO [dbo].[ActivityLogBAK] ([ActivityLogGuid],[ActivityLogType],[UserGuid],[ProgramGuid],[SessionGuid],[PageSequenceGuid],[PageGuid],[ActivityDateTime],[Message],[Browser],[IP],[From]) ";
                //sqlStr += "select [ActivityLogGuid],[ActivityLogType],[UserGuid],[ProgramGuid],[SessionGuid],[PageSequenceGuid],[PageGuid],[ActivityDateTime],[Message],[Browser],[IP],[From] from activitylog order by ActivityDateTime";

                comm.CommandText = sqlStr;
                comm.CommandType = CommandType.Text;
                result = comm.ExecuteNonQuery();
            }

            return result;
        }
    }
}
