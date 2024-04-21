using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Saobracaj.Dokumenta.TrainListItem
{
    internal class TrainListItemDAO
    {
        private string connectionString = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        internal void ReadFromExcel(int id_sup)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string file = "";   //variable for the Excel File Location
            DataTable dt = new DataTable();   //container for our excel data
            DataRow row;
            DialogResult result = openFileDialog1.ShowDialog();  // Show the dialog.
            if (result == DialogResult.OK)   // Check if Result == "OK".
            {
                file = openFileDialog1.FileName; //get the filename with the location of the file
                                                 try

                {
                //Create Object for Microsoft.Office.Interop.Excel that will be use to read excel file

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(file);

                Microsoft.Office.Interop.Excel._Worksheet excelWorksheet = excelWorkbook.Sheets[1];

                Microsoft.Office.Interop.Excel.Range excelRange = excelWorksheet.UsedRange;


                int rowCount = excelRange.Rows.Count;  //get row count of excel data

                int colCount = excelRange.Columns.Count; // get column count of excel data

                //Set column names                
                //dt.Columns.Add(excelRange.Cells[1, j].Value2.ToString());
                dt.Columns.Add("TrainListID", typeof(int)); //int
                dt.Columns.Add("RedniBroj", typeof(int)); //int
                dt.Columns.Add("OznakaKola", typeof(string));
                dt.Columns.Add("SerijaKola", typeof(string));
                dt.Columns.Add("TaraKola", typeof(decimal)); //decimal
                dt.Columns.Add("GKocnaMasa", typeof(decimal)); //decimal
                dt.Columns.Add("PKocnaMasa", typeof(decimal)); //decimal
                dt.Columns.Add("DuzinaKola", typeof(decimal)); //decimal
                dt.Columns.Add("BrojOsovina", typeof(int)); //int
                dt.Columns.Add("KontBroj", typeof(string));
                dt.Columns.Add("KontTip", typeof(string));
                dt.Columns.Add("KontTara", typeof(decimal)); //decimal
                dt.Columns.Add("Neto", typeof(decimal)); //decimal
                dt.Columns.Add("NHM", typeof(string)); // izbacuje se
                dt.Columns.Add("UN", typeof(string)); // izbacuje se
                dt.Columns.Add("RIDRobaMasa", typeof(decimal)); //decimal
                dt.Columns.Add("BrojKomada", typeof(int)); //int
                dt.Columns.Add("CIM", typeof(int)); //int
                dt.Columns.Add("OtpStanicaTerminal", typeof(string));
                dt.Columns.Add("PolStanicaTerminal", typeof(string));
                dt.Columns.Add("Posiljac", typeof(string));
                dt.Columns.Add("Primalac", typeof(string));
                dt.Columns.Add("Proizvod", typeof(string));
                dt.Columns.Add("T1", typeof(string));
                dt.Columns.Add("MRN", typeof(string)); // izbacuje se
                dt.Columns.Add("Klient", typeof(string));
                dt.Columns.Add("Plombe", typeof(string)); // izbacuje se
                dt.Columns.Add("Buking", typeof(string));


                //Get Row Data of Excel              
                for (int i = 2; i <= rowCount; i++) //Loop for available row of excel data
                {
                    bool replica = false;
                    row = dt.NewRow();  //assign new row to DataTable

                    row[0] = id_sup;
                    for (int j = 1; j <= colCount; j++) //Loop for available column of excel data
                    {

                        if (j == 1 && excelRange.Cells[i, j].Value.ToString() == excelRange.Cells[i - 1, j].Value.ToString())
                        {
                            replica = true;
                        }
                        string cell = "";

                        if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j].Value2 != null)
                        {
                            cell = excelRange.Cells[i, j].Value.ToString();
                        }

                        if (j == 1 || j == 8 || j == 16 || j == 17)
                        {
                            if (cell == "")
                            {
                                row[j] = 0;
                            }
                            else
                            {

                                row[j] = (int)Convert.ToInt64(cell);
                            }
                        }
                        else if (j == 4 || j == 5 || j == 6 || j == 7 || j == 11 || j == 12 || j == 15)
                        {
                            if (cell == "")
                            {
                                row[j] = 0.00;
                            }
                            else
                            {
                                row[j] = (decimal)Convert.ToDecimal(cell);
                            }
                        }
                        else
                        {
                            row[j] = cell;
                        }


                    }
                    if (replica)
                    {
                        for (int j = 3; j < 9; j++)
                        {
                            row[j] = dt.Rows[i - 3].ItemArray[j];
                        }
                    }


                    dt.Rows.Add(row); //add row to DataTable

                    // obrada podataka
                    DataTable tab = dt.Clone();
                    tab.ImportRow(dt.Rows[i - 2]);


                    char[] separators = new char[] { '-', ',' };
                    string[] subsNHM = tab.Rows[0].Field<string>("NHM").Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    tab.Columns.Remove("NHM");
                    string[] subsMRN = tab.Rows[0].Field<string>("MRN").Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    tab.Columns.Remove("MRN");
                    string[] subsSeals = tab.Rows[0].Field<string>("Plombe").Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    tab.Columns.Remove("Plombe");
                    string[] subUN = tab.Rows[0].Field<string>("UN").Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    tab.Columns.Remove("UN");

                    int resoult = Insert(tab);
                    //MessageBox.Show(resoult + " row(s) is added");

                    int id_last = GetLastRowID();

                    DataTable tabNHM = new DataTable();
                    tabNHM.Columns.Add("NHM", typeof(int));
                    tabNHM.Columns.Add("TrainListStavkeID", typeof(int)).SetOrdinal(0);
                    foreach (string item in subsNHM)
                    {
                        DataRow rowNHM = tabNHM.NewRow();
                        rowNHM[0] = id_last;
                        rowNHM[1] = (int)Convert.ToInt64(item);
                        tabNHM.Rows.Add(rowNHM);
                    }
                    resoult = InsertNHM(tabNHM);
                    //MessageBox.Show(resoult + " row(s) is added");

                    DataTable tabMRN = new DataTable();
                    tabMRN.Columns.Add("MRN", typeof(string));
                    tabMRN.Columns.Add("TrainListStavkeID", typeof(int)).SetOrdinal(0);
                    foreach (string item in subsMRN)
                    {
                        DataRow rowMRN = tabMRN.NewRow();
                        rowMRN[0] = id_last;
                        rowMRN[1] = (string)item;
                        tabMRN.Rows.Add(rowMRN);
                    }
                    resoult = InsertMRN(tabMRN);
                    //MessageBox.Show(resoult + " row(s) is added");

                    DataTable tabSeals = new DataTable();
                    tabSeals.Columns.Add("Plomba", typeof(string));
                    tabSeals.Columns.Add("TrainListStavkeID", typeof(int)).SetOrdinal(0);
                    foreach (string item in subsSeals)
                    {
                        DataRow rowSeals = tabSeals.NewRow();
                        rowSeals[0] = id_last;
                        rowSeals[1] = (string)item;
                        tabSeals.Rows.Add(rowSeals);
                    }
                    resoult = InsertSeals(tabSeals);
                    // MessageBox.Show(resoult + " row(s) is added");

                    DataTable tabUN = new DataTable();
                    tabUN.Columns.Add("UNBroj", typeof(string));
                    tabUN.Columns.Add("TrainListStavkeID", typeof(int)).SetOrdinal(0);
                    foreach (string item in subUN)
                    {
                        DataRow rowUN = tabUN.NewRow();
                        rowUN[0] = id_last;
                        rowUN[1] = (string)item;
                        tabUN.Rows.Add(rowUN);
                    }
                    resoult = InsertUN(tabUN);
                    //MessageBox.Show(resoult + " row(s) is added");

                }

                //Close and Clean excel process
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Marshal.ReleaseComObject(excelRange);
                Marshal.ReleaseComObject(excelWorksheet);
                excelWorkbook.Close();
                Marshal.ReleaseComObject(excelWorkbook);

                //quit 
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        internal List<TrainListItemModel> GetAllBySuperiorId(int id)
        {
            List<TrainListItemModel> returnThese = new List<TrainListItemModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainListStavke_GetAllBySuperiorId", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new TrainListItemModel
                            {
                                Id = (int)reader[0],
                                TrainListId = (int)reader[1],
                                RedniBroj = (int)reader[2],
                                OznakaKola = (string)reader[3],
                                SerijaKola = (string)reader[4],
                                TaraKola = (decimal)reader[5],
                                GKocnaMasa = (decimal)reader[6],
                                PKocnaMasa = (decimal)reader[7],
                                DuzinaKola = (decimal)reader[8],
                                BrojOsovina = (int)reader[9],
                                KontBroj = (string)reader[10],
                                KontTip = (string)reader[11],
                                KontTara = (decimal)reader[12],
                                Neto = (decimal)reader[13],
                                RIDRobaMasa = (decimal)reader[14],
                                BrojKomada = (int)reader[15],
                                CIM = (int)reader[16],
                                OtpStanicaTerminal = (string)reader[17],
                                PolStanicaTerminal = (string)reader[18],
                                Posiljac = (string)reader[19],
                                Primalac = (string)reader[20],
                                Proizvod = (string)reader[21],
                                T1 = (string)reader[22],
                                Klient = (string)reader[23],
                                Buking = (string)reader[24]
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("Can not get TrainListItems");
                }
            }
            return returnThese;
        }

        internal List<int> GetAllNHMBySuperiorId(int id)
        {
            List<int> returnThese = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("NHM_GetAllBySuperiorId", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add((int)reader[0]);

                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("Can not get NHM");
                }
            }
            return returnThese;
        }

        internal List<string> GetAllUNBySuperiorId(int id)
        {
            List<string> returnThese = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("UN_GetAllBySuperiorId", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add((string)reader[0]);

                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("Can not get UN");
                }
            }
            return returnThese;
        }

        internal List<string> GetAllMRNBySuperiorId(int id)
        {
            List<string> returnThese = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("MRN_GetAllBySuperiorId", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add((string)reader[0]);

                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("Can not get MRN");
                }
            }
            return returnThese;
        }

        internal List<string> GetAllSealsBySuperiorId(int id)
        {
            List<string> returnThese = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("Plombe_GetAllBySuperiorId", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add((string)reader[0]);

                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("Can not get Seals");
                }
            }
            return returnThese;
        }

        private int Insert(DataTable data)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainListStavke_InsertP", con);
                command.CommandType = CommandType.StoredProcedure;
                //List<JObject> proba = Table2Object(data);
                string nesto = data.Rows[0].ItemArray[0].ToString();
                string nesto2 = data.Rows[0].ItemArray[1].ToString();

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@TrainListID";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = Convert.ToInt32(data.Rows[0].ItemArray[0].ToString()); ;
                command.Parameters.Add(parameter);

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@RedniBroj";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = Convert.ToInt32(data.Rows[0].ItemArray[1].ToString()); ;
                command.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@OznakaKola";
                parameter2.SqlDbType = SqlDbType.NVarChar;
                parameter2.Size = 30;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = data.Rows[0].ItemArray[2].ToString(); ;
                command.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@SerijaKola";
                parameter3.SqlDbType = SqlDbType.NVarChar;
                parameter3.Size = 30;
                parameter3.Direction = ParameterDirection.Input;
                parameter3.Value = data.Rows[0].ItemArray[3].ToString(); ;
                command.Parameters.Add(parameter3);
                
                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@TaraKola";
                parameter4.SqlDbType = SqlDbType.Decimal;
                parameter4.Direction = ParameterDirection.Input;
                parameter4.Value = Convert.ToDecimal(data.Rows[0].ItemArray[4].ToString()); ;
                command.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@GKocnaMasa";
                parameter5.SqlDbType = SqlDbType.Decimal;
                parameter5.Direction = ParameterDirection.Input;
                parameter5.Value = Convert.ToDecimal(data.Rows[0].ItemArray[5].ToString()); ;
                command.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@PKocnaMasa";
                parameter6.SqlDbType = SqlDbType.Decimal;
                parameter6.Direction = ParameterDirection.Input;
                parameter6.Value = Convert.ToDecimal(data.Rows[0].ItemArray[6].ToString()); ;
                command.Parameters.Add(parameter6);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@DuzinaKola";
                parameter7.SqlDbType = SqlDbType.Decimal;
                parameter7.Direction = ParameterDirection.Input;
                parameter7.Value = Convert.ToDecimal(data.Rows[0].ItemArray[7].ToString()); ;
                command.Parameters.Add(parameter7);

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@BrojOsovina";
                parameter8.SqlDbType = SqlDbType.Int;
                parameter8.Direction = ParameterDirection.Input;
                parameter8.Value = Convert.ToInt32(data.Rows[0].ItemArray[8].ToString()); ;
                command.Parameters.Add(parameter8);

                SqlParameter parameter9 = new SqlParameter();
                parameter9.ParameterName = "@KontBroj";
                parameter9.SqlDbType = SqlDbType.NVarChar;
                parameter9.Size = 30;
                parameter9.Direction = ParameterDirection.Input;
                parameter9.Value = data.Rows[0].ItemArray[9].ToString(); ;
                command.Parameters.Add(parameter9);

                SqlParameter parameter10 = new SqlParameter();
                parameter10.ParameterName = "@KontTip";
                parameter10.SqlDbType = SqlDbType.NVarChar;
                parameter10.Size = 30;
                parameter10.Direction = ParameterDirection.Input;
                parameter10.Value = data.Rows[0].ItemArray[10].ToString(); ;
                command.Parameters.Add(parameter10);

                SqlParameter parameter11 = new SqlParameter();
                parameter11.ParameterName = "@KontTara";
                parameter11.SqlDbType = SqlDbType.Decimal;
                parameter11.Direction = ParameterDirection.Input;
                parameter11.Value = Convert.ToDecimal(data.Rows[0].ItemArray[11].ToString()); ;
                command.Parameters.Add(parameter11);

                SqlParameter parameter12 = new SqlParameter();
                parameter12.ParameterName = "@Neto";
                parameter12.SqlDbType = SqlDbType.Decimal;
                parameter12.Direction = ParameterDirection.Input;
                parameter12.Value = Convert.ToDecimal(data.Rows[0].ItemArray[12].ToString()); ;
                command.Parameters.Add(parameter12);

                SqlParameter parameter13 = new SqlParameter();
                parameter13.ParameterName = "@RIDRobaMasa";
                parameter13.SqlDbType = SqlDbType.Decimal;
                parameter13.Direction = ParameterDirection.Input;
                parameter13.Value = Convert.ToDecimal(data.Rows[0].ItemArray[13].ToString()); ;
                command.Parameters.Add(parameter13);

                SqlParameter parameter14 = new SqlParameter();
                parameter14.ParameterName = "@BrojKomada";
                parameter14.SqlDbType = SqlDbType.Int;
                parameter14.Direction = ParameterDirection.Input;
                parameter14.Value = Convert.ToInt32(data.Rows[0].ItemArray[14].ToString()); ;
                command.Parameters.Add(parameter14);

                SqlParameter parameter15 = new SqlParameter();
                parameter15.ParameterName = "@CIM";
                parameter15.SqlDbType = SqlDbType.Int;
                parameter15.Direction = ParameterDirection.Input;
                parameter15.Value = Convert.ToInt32(data.Rows[0].ItemArray[15].ToString()); ;
                command.Parameters.Add(parameter15);

                SqlParameter parameter16 = new SqlParameter();
                parameter16.ParameterName = "@OtpStanicaTerminal";
                parameter16.SqlDbType = SqlDbType.NVarChar;
                parameter16.Size = 50;
                parameter16.Direction = ParameterDirection.Input;
                parameter16.Value = data.Rows[0].ItemArray[16].ToString(); ;
                command.Parameters.Add(parameter16);

                SqlParameter parameter17 = new SqlParameter();
                parameter17.ParameterName = "@PolStanicaTerminal";
                parameter17.SqlDbType = SqlDbType.NVarChar;
                parameter17.Size = 50;
                parameter17.Direction = ParameterDirection.Input;
                parameter17.Value = data.Rows[0].ItemArray[17].ToString(); ;
                command.Parameters.Add(parameter17);

                SqlParameter parameter18 = new SqlParameter();
                parameter18.ParameterName = "@Posiljac";
                parameter18.SqlDbType = SqlDbType.NVarChar;
                parameter18.Size = 50;
                parameter18.Direction = ParameterDirection.Input;
                parameter18.Value = data.Rows[0].ItemArray[18].ToString(); ;
                command.Parameters.Add(parameter18);

                SqlParameter parameter19 = new SqlParameter();
                parameter19.ParameterName = "@Primalac";
                parameter19.SqlDbType = SqlDbType.NVarChar;
                parameter19.Size = 50;
                parameter19.Direction = ParameterDirection.Input;
                parameter19.Value = data.Rows[0].ItemArray[19].ToString(); ;
                command.Parameters.Add(parameter19);

                SqlParameter parameter20 = new SqlParameter();
                parameter20.ParameterName = "@Proizvod";
                parameter20.SqlDbType = SqlDbType.NVarChar;
                parameter20.Size = 255;
                parameter20.Direction = ParameterDirection.Input;
                parameter20.Value = data.Rows[0].ItemArray[20].ToString(); ;
                command.Parameters.Add(parameter20);

                SqlParameter parameter21 = new SqlParameter();
                parameter21.ParameterName = "@T1";
                parameter21.SqlDbType = SqlDbType.NVarChar;
                parameter21.Size = 50;
                parameter21.Direction = ParameterDirection.Input;
                parameter21.Value = data.Rows[0].ItemArray[21].ToString(); ;
                command.Parameters.Add(parameter21);

                SqlParameter parameter22 = new SqlParameter();
                parameter22.ParameterName = "@Klient";
                parameter22.SqlDbType = SqlDbType.NVarChar;
                parameter22.Size = 50;
                parameter22.Direction = ParameterDirection.Input;
                parameter22.Value = data.Rows[0].ItemArray[22].ToString(); ;
                command.Parameters.Add(parameter22);

                SqlParameter parameter23 = new SqlParameter();
                parameter23.ParameterName = "@Buking";
                parameter23.SqlDbType = SqlDbType.NVarChar;
                parameter23.Size = 50;
                parameter23.Direction = ParameterDirection.Input;
                parameter23.Value = data.Rows[0].ItemArray[23].ToString(); ;
                command.Parameters.Add(parameter23);



                //@Buking nvarchar(50)

                /*
                SqlParameter Parameter = new SqlParameter();
                Parameter.ParameterName = "@items";
                Parameter.SqlDbType = SqlDbType.Structured;
                Parameter.Direction = ParameterDirection.Input;
                Parameter.Value = data;
                command.Parameters.Add(Parameter);
                */
                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("TrainListItem NOT inserted!");
                }
            }
            return success;
        }

        private int GetLastRowID()
        {
            int returnThese = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainListStavke_GetLastRow", con);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese = (int)reader[0];
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("Can not get TrainListItems");
                }
            }
            return returnThese;
        }

        private int InsertNHM(DataTable data)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("NHM_Insert", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter Parameter = new SqlParameter();
                Parameter.ParameterName = "@items";
                Parameter.SqlDbType = SqlDbType.Structured;
                Parameter.Direction = ParameterDirection.Input;
                Parameter.Value = data;
                command.Parameters.Add(Parameter);

                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("NHM NOT inserted!");
                }
            }
            return success;
        }

        private int InsertUN(DataTable data)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("UN_Insert", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter Parameter = new SqlParameter();
                Parameter.ParameterName = "@items";
                Parameter.SqlDbType = SqlDbType.Structured;
                Parameter.Direction = ParameterDirection.Input;
                Parameter.Value = data;
                command.Parameters.Add(Parameter);

                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("UN NOT inserted!");
                }
            }
            return success;
        }

        private int InsertMRN(DataTable data)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("MRN_Insert", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter Parameter = new SqlParameter();
                Parameter.ParameterName = "@items";
                Parameter.SqlDbType = SqlDbType.Structured;
                Parameter.Direction = ParameterDirection.Input;
                Parameter.Value = data;
                command.Parameters.Add(Parameter);

                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("MRN NOT inserted!");
                }
            }
            return success;
        }

        private int InsertSeals(DataTable data)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("Plombe_Insert", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter Parameter = new SqlParameter();
                Parameter.ParameterName = "@items";
                Parameter.SqlDbType = SqlDbType.Structured;
                Parameter.Direction = ParameterDirection.Input;
                Parameter.Value = data;
                command.Parameters.Add(Parameter);

                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw new Exception("Seals NOT inserted!");
                }
            }
            return success;
        }
    }
}
