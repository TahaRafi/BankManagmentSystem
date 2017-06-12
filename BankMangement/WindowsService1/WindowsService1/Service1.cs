using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timeDelay;
        public Service1()
        {
            ///8640000000
            InitializeComponent();
            timeDelay = new System.Timers.Timer(220000);
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);

        }

        private void WorkProcess(object sender, ElapsedEventArgs e)
        {
            LogService("");
            
        }




        protected override void OnStart(string[] args)
        {
            LogService("Service is Started");
            timeDelay.Enabled = true;
        }

        protected override void OnStop()
        {
            LogService("Service Stoped");
            timeDelay.Enabled = false;
        }
        static int counter = 1;
        private void LogService(string content)
        {
            FileStream fs = new FileStream(@"E:\TestServiceLog"+ counter+".txt", FileMode.OpenOrCreate, FileAccess.Write);
            counter++;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=TAHA;Initial Catalog=BankDb;User ID=sa;Password=TAHA;Integrated Security=True";


            con.Open();
            SqlCommand cmd2 = new SqlCommand(@"SELECT Date,Type,Amount FROM Transaction1 WHERE Date = @R", con);

            cmd2.Parameters.AddWithValue("@R", DateTime.Now.ToString("M/d/yyyy"));
            SqlDataReader dr = cmd2.ExecuteReader();

            // dataGridView1.DataBind();
            content = "";
            while (dr.Read())
            {
                string Date = dr.GetString(dr.GetOrdinal("Date"));
                string Type = dr.GetString(dr.GetOrdinal("Type"));
                string Amount = dr.GetString(dr.GetOrdinal("Amount"));
                content = Date + " " + Type + " " + Amount; 


            }


            dr.Close();
            con.Close();


            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }




        void Write()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=TAHA;Initial Catalog=BankDb;User ID=sa;Password=TAHA;Integrated Security=True";



            
        }
    }
}
