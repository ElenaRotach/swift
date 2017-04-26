using swiftDemon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace swift
{
    public partial class JournalForm : Form
    {
        public JournalForm()
        {
            InitializeComponent();
        }

        private void JournalForm_Load(object sender, EventArgs e)
        {
            List<swiftMess_str> allMess = new List<swiftMess_str>();
            string sql = @"select * from mess";
            string connectionString = @"Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + Environment.CurrentDirectory + @"\swift.mdb";
            OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
            OleDbCommand myOleDbCommand = myOleDbConnection.CreateCommand();
            myOleDbCommand.CommandText = sql;
            myOleDbConnection.Open();
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            
            while (myOleDbDataReader.Read())
            {
                
                allMess.Add(new swiftMess_str(myOleDbDataReader.GetValue(0).ToString(), myOleDbDataReader.GetValue(1).ToString(), myOleDbDataReader.GetValue(2).ToString(), 
                    myOleDbDataReader.GetValue(3).ToString(),
                    myOleDbDataReader.GetValue(4).ToString(), myOleDbDataReader.GetValue(5).ToString(), myOleDbDataReader.GetValue(6).ToString(), myOleDbDataReader.GetValue(7).ToString(),
                    myOleDbDataReader.GetValue(8).ToString(), myOleDbDataReader.GetValue(9).ToString(), myOleDbDataReader.GetValue(10).ToString(), myOleDbDataReader.GetValue(11).ToString(),
                    myOleDbDataReader.GetValue(12).ToString(), myOleDbDataReader.GetValue(13).ToString(), myOleDbDataReader.GetValue(14).ToString(), myOleDbDataReader.GetValue(15).ToString(),
                    myOleDbDataReader.GetValue(16).ToString(), myOleDbDataReader.GetValue(17).ToString(), myOleDbDataReader.GetValue(19).ToString(), myOleDbDataReader.GetValue(19).ToString(),
                    myOleDbDataReader.GetValue(20).ToString(), myOleDbDataReader.GetValue(21).ToString(), myOleDbDataReader.GetValue(22).ToString(), myOleDbDataReader.GetValue(23).ToString(),
                    myOleDbDataReader.GetValue(24).ToString()));
            }

            if (allMess.Count > 0)
            {
                for (int st = 0; st < 25; st++)
                {
                    tabMess.Columns.Add(st.ToString(), st.ToString());
                }
                tabMess.Rows.Add(allMess.Count);
                int i = 0;
                foreach (swiftMess_str msg in allMess)
                {
                    tabMess.Rows[i].Cells[0].Value = msg.transactionReferenceNumber_20;
                    tabMess.Rows[i].Cells[1].Value = msg.valueDate_30V;
                    tabMess.Rows[i].Cells[2].Value = msg.date_32;
                    tabMess.Rows[i].Cells[3].Value = msg.currency_32;
                    tabMess.Rows[i].Cells[4].Value = msg.amount_32;
                    tabMess.Rows[i].Cells[5].Value = msg.currency_33B;
                    tabMess.Rows[i].Cells[6].Value = msg.amount_33B;
                    tabMess.Rows[i].Cells[7].Value = msg.orderingCustomer_50;
                    tabMess.Rows[i].Cells[8].Value = msg.orderingInstitution_52;
                    tabMess.Rows[i].Cells[9].Value = msg.senderCorrespondent_53;
                    tabMess.Rows[i].Cells[10].Value = msg.receiverCorrespondent_54;
                    tabMess.Rows[i].Cells[11].Value = msg.intermediaryInstitution_56;
                    tabMess.Rows[i].Cells[12].Value = msg.accountWithInstitution_57;
                    tabMess.Rows[i].Cells[13].Value = msg.beneficiaryInstitution_58;
                    tabMess.Rows[i].Cells[14].Value = msg.beneficiaryCustomer_59;
                    tabMess.Rows[i].Cells[15].Value = msg.processingCharacteristic;
                    tabMess.Rows[i].Cells[16].Value = msg.mess_direction;
                    tabMess.Rows[i].Cells[17].Value = msg.comment;
                    tabMess.Rows[i].Cells[18].Value = msg.dateTime_mess;
                    tabMess.Rows[i].Cells[19].Value = msg.referenceMess;
                    tabMess.Rows[i].Cells[20].Value = msg.fin;
                    tabMess.Rows[i].Cells[21].Value = msg.swiftNumberBankKontragent;
                    tabMess.Rows[i].Cells[22].Value = msg.naimBankKontragent;
                    tabMess.Rows[i].Cells[23].Value = msg.thread;
                    tabMess.Rows[i].Cells[24].Value = msg.fileName;

                    i++;
                }
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            dynamic excel = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
            excel.Visible = true;
            dynamic workbook = excel.Workbooks.Add();
            dynamic worksheet = workbook.ActiveSheet; //.Worksheets.Add();
            worksheet.Application.Worksheets.Add(); //добавить лист
            //worksheet.Application.Worksheets.Add(); 
            int totalSheets = worksheet.Application.ActiveWorkbook.Sheets.Count;
            (worksheet.Application.ActiveSheet).Move(worksheet.Application.Worksheets[totalSheets]);
            (worksheet.Application.ActiveWorkbook.Sheets[1]).Activate();
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "export";
            excel.Columns("A:E").ColumnWidth = 15;
            int str = 0;
            while (str < tabMess.RowCount)
            {
                for (int st = 0; st < 25; st++)
                {
                       worksheet.Cells[str+1, st+1] = tabMess.Rows[str].Cells[st].Value;
                }
                str++;
            }
        }
    }
}
