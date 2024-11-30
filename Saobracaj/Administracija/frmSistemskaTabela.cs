using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    public partial class frmSistemskaTabela : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmSistemskaTabela()
        {
            InitializeComponent();
            sfButton6.Paint += sfButton6_Paint;
            PostaviLogo();
            PostaviFirma();
        }
        private void PostaviFirma()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select NazivFirme, Ulica,  line , PIB, MB " +
  " FROM [dbo].[SistemskePostavkeHeader] "
            , con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               this.Text = dr["NazivFirme"].ToString() +  " " + dr["Line"].ToString() + "  - FORMA -  "  + this.Text + "      KORISNIK: " + Sifarnici.frmLogovanje.user;
            }

            con.Close();
        }

        private void VratiPodatke(string Id)
        {
          
        }

        private void PostaviLogo()
        {
            
           
                if (pictureBox1.Image != null)//check whether picture box contain image or not
                    pictureBox1.Image.Dispose();//clear the image of the picture box if there is image

                SqlConnection con = new SqlConnection(Sifarnici.frmLogovanje.connectionString);//connection to the your database
                SqlCommand cmd = new SqlCommand("ReadImage", con);//create a SQL command object by passing name of the stored procedure and database connection 
                cmd.CommandType = CommandType.StoredProcedure; //set command object command type to stored procedure type
                cmd.Parameters.Add("@imgId", SqlDbType.Int).Value = Convert.ToInt32(1);//add parameter to the command object and set value to that parameter
            SqlDataAdapter adp = new SqlDataAdapter(cmd);//create SQL data adapter with command object
                DataTable dt = new DataTable();//create a data table to hold result of the command
                try
                {
                    if (con.State == ConnectionState.Closed)//check whether connection to database is close or not
                        con.Open();//if connection is close then only open the connection
                    adp.Fill(dt);//data table fill with data by calling the fill method of data adapter object
                    if (dt.Rows.Count > 0)//check whether data table contain any row or not
                    {
                        MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["ImageData"]);//create memory stream by passing byte array of the image
                        pictureBox1.Image = Image.FromStream(ms);//set image property of the picture box by creating a image from stream 
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;//set size mode property of the picture box to stretch 
                        pictureBox1.Refresh();//refresh picture box
                    }
                }
                catch (Exception ex)//catch if any error occur
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//display error message with exception 
                }
                finally
                {
                    if (con.State == ConnectionState.Open)//check whether connection to database is open or not
                        con.Close();//if connection is open then only close the connection
                }
            
         
        }
        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);
            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void sfButton6_Paint(object sender, PaintEventArgs e)
        {
            int radius = 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(this.sfButton6.ClientRectangle.X + 1,
                                           this.sfButton6.ClientRectangle.Y + 1,
                                           this.sfButton6.ClientRectangle.Width - 2,
                                           this.sfButton6.ClientRectangle.Height - 2);
            sfButton6.Region = new Region(GetRoundedRect(rect, radius));
            rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
            e.Graphics.DrawPath(new Pen(Color.Red), GetRoundedRect(rect, radius));
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Sifarnici.frmLogovanje.connectionString); //connection to the your database
            try
            {
                OpenFileDialog fop = new OpenFileDialog(); //create object of open file dialog
                fop.InitialDirectory = @"C:\"; //set Initial directory
                fop.Filter = "[JPG,JPEG]|*.jpg"; //set filter for select only .jpg files
                if (fop.ShowDialog() == DialogResult.OK) //display open file dialog to user and only user select a image enter to if block
                {
                    FileStream FS = new FileStream(@fop.FileName, FileMode.Open, FileAccess.Read); //create a file stream object associate to user selected file 
                    byte[] img = new byte[FS.Length]; //create a byte array with size of user select file stream length
                    FS.Read(img, 0, Convert.ToInt32(FS.Length));//read user selected file stream in to byte array

                    if (con.State == ConnectionState.Closed)//check whether connection to database is close or not
                        con.Open();//if connection is close then only open the connection
                    SqlCommand cmd = new SqlCommand("SaveImage", con);//create a SQL command object by passing name of the stored procedure and database connection 
                    cmd.CommandType = CommandType.StoredProcedure; //set command object command type to stored procedure type
                    cmd.Parameters.Add("@img", SqlDbType.Image).Value = img;//add parameter to the command object and set value to that parameter
                    cmd.ExecuteNonQuery();//execute command                     
                    loadImageIDs();//call user defined method to load image IDs to combo box
                    MessageBox.Show("Image Save Successfully!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);//display save successful message to user
                }
                else
                {
                    MessageBox.Show("Please Select a Image to save!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);//display message to force select a image 
                }

            }
            catch (Exception ex)//catch if any error occur
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);//display error message with exception 
            }
            finally
            {
                if (con.State == ConnectionState.Open)//check whether connection to database is open or not
                    con.Close();//if connection is open then only close the connection
            }
        }

        private void loadImageIDs()
        {
            #region Load Image Ids
            SqlConnection con = new SqlConnection(Sifarnici.frmLogovanje.connectionString);//connection to the your database
            SqlCommand cmd = new SqlCommand("ReadAllImageIDs", con);//create a SQL command object by passing name of the stored procedure and database connection 
            cmd.CommandType = CommandType.StoredProcedure;//set command object command type to stored procedure type
            SqlDataAdapter adp = new SqlDataAdapter(cmd);//create SQL data adapter with command object
            DataTable dt = new DataTable();//create a data table to hold result of the command
            try
            {
                if (con.State == ConnectionState.Closed)//check whether connection to database is close or not
                    con.Open();//if connection is close then only open the connection
                adp.Fill(dt);//data table fill with data by calling the fill method of data adapter object
                if (dt.Rows.Count > 0)//check whether data table contain any row or not
                {
                    cboImage1.DataSource = dt;//set the data source property of the combo box to result set of the command 
                    cboImage1.ValueMember = "ImageID";//set the value member property of the combo box
                    cboImage1.DisplayMember = "ImageID";//set the display member property of the combo box
                    cboImage1.SelectedIndex = 0;//set the selected index property of the combo box to 0
                }
            }
            catch (Exception ex)//catch if any error occur
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);//display error message with exception 
            }
            finally
            {
                if (con.State == ConnectionState.Open)//check whether connection to database is open or not
                    con.Close();//if connection is open then only close the connection
            }
            #endregion
        }

        private void sfButton5_Click(object sender, EventArgs e)
        {
            #region Retrieve Image from DB
            if (cboImage1.SelectedValue != null)//check whether user select image ID or not 
            {
                if (picImage.Image != null)//check whether picture box contain image or not
                    picImage.Image.Dispose();//clear the image of the picture box if there is image

                SqlConnection con = new SqlConnection(Sifarnici.frmLogovanje.connectionString);//connection to the your database
                SqlCommand cmd = new SqlCommand("ReadImage", con);//create a SQL command object by passing name of the stored procedure and database connection 
                cmd.CommandType = CommandType.StoredProcedure; //set command object command type to stored procedure type
                cmd.Parameters.Add("@imgId", SqlDbType.Int).Value = Convert.ToInt32(cboImage1.SelectedValue.ToString());//add parameter to the command object and set value to that parameter
                SqlDataAdapter adp = new SqlDataAdapter(cmd);//create SQL data adapter with command object
                DataTable dt = new DataTable();//create a data table to hold result of the command
                try
                {
                    if (con.State == ConnectionState.Closed)//check whether connection to database is close or not
                        con.Open();//if connection is close then only open the connection
                    adp.Fill(dt);//data table fill with data by calling the fill method of data adapter object
                    if (dt.Rows.Count > 0)//check whether data table contain any row or not
                    {
                        MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["ImageData"]);//create memory stream by passing byte array of the image
                        picImage.Image = Image.FromStream(ms);//set image property of the picture box by creating a image from stream 
                        picImage.SizeMode = PictureBoxSizeMode.StretchImage;//set size mode property of the picture box to stretch 
                        picImage.Refresh();//refresh picture box
                    }
                }
                catch (Exception ex)//catch if any error occur
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//display error message with exception 
                }
                finally
                {
                    if (con.State == ConnectionState.Open)//check whether connection to database is open or not
                        con.Close();//if connection is open then only close the connection
                }
            }
            else
            {
                MessageBox.Show("Please Select a Image ID to Display!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//display message to force select a image ID
            }
            #endregion
        }
    }
}
