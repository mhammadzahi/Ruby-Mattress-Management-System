using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ruby_Mattress_Management_System
{
    public partial class Form2 : Form{
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection("server=localhost; database=productionmaster; uid=root");

        public Form2()
        {
            InitializeComponent();
        }

        private void newButt_Click(object sender, EventArgs e){
            this.Hide();
            jobCardForm frm = new jobCardForm();
            frm.Show();
        }
        private void FilljobCardDJV() {
            try { //open Connection
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show(ex.Message);
            }

            MySqlCommand cmd = new MySqlCommand("select * from job_card", con);
            //MySqlCommand cmd = new MySqlCommand("select order_date, delive_date, location, area, type, saleman, name_item, customer, lift_size_len, lift_size_width FROM job_card JOIN item i on job_card.id_job = i.id_job_card", con);

            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            jobCardDJV.DataSource = dt;
            dr.Close();
            con.Close();

            jobCardDJV.Columns[0].HeaderText = "Order Date";
            jobCardDJV.Columns[1].HeaderText = "Delivery Date";
            jobCardDJV.Columns[2].HeaderText = "Location";
            jobCardDJV.Columns[3].HeaderText = "Area";
            jobCardDJV.Columns[4].HeaderText = "Type";
            jobCardDJV.Columns[5].HeaderText = "Saleman";
            jobCardDJV.Columns[6].HeaderText = "Customer";
            //jobCardDJV.Columns[7].HeaderText = "drawing";
            jobCardDJV.Columns[7].HeaderText = "Lift Size Length";
            jobCardDJV.Columns[8].HeaderText = "Lift Size Width";
            for(int i = 0; i < 9; i++){
                //jobCardDJV.Columns[i].Width = 50;
                jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            jobCardDJV.EnableHeadersVisualStyles = false;

        }//end

        private void Form2_Load(object sender, EventArgs e){
            FilljobCardDJV();
        }

        private void button1_Click(object sender, EventArgs e){//show drawing button

        }

        private void jobCardDJV_SelectionChanged(object sender, EventArgs e){//selection changed
            foreach (DataGridViewRow row in jobCardDJV.SelectedRows){
                // 
                Interaction.MsgBox(row.Cells[0].Value.ToString());
            }
        }
    }
}
