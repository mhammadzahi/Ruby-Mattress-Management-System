using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Text.RegularExpressions;
//using Microsoft.Office.Interop.Excel;

namespace Ruby_Mattress_Management_System {
    public partial class Form2 : Form{
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection("server=localhost; database=productionmaster; uid=root");

        public Form2(){
            InitializeComponent();
        }
        int id_Selitem;

        
        private void RetrievePDF(int id_item){//retrieve drawing & save
            //retrieve drawing from db
            byte[] pdfData = null;
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }
            var cmd1 = new MySqlCommand("select drawing from item where id_item = @id", con);
            cmd1.Parameters.AddWithValue("@id", id_item);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read()){
                pdfData = (byte[])dr1["drawing"];
            }
            
            dr1.Close();
            con.Close();
            // Save the Excel workbook and close the Excel application
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "pdf files (*.pdf)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.InitialDirectory = "C:\\";
            saveFileDialog.FileName = "Drawing.pdf";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK){
                string fileName = saveFileDialog.FileName;
                // Save the file using the specified file name and location
                //pdfData.SaveAs(fileName);
                File.WriteAllBytes(fileName, pdfData);
                //*************
                DialogResult resultM = MessageBox.Show("Your drawing is saved in " + fileName + ", would you like to Open it? ", "Open it?", MessageBoxButtons.YesNo);
                if (resultM == DialogResult.Yes){//open pdf
                    Process.Start(fileName);
                }
            }
            else{
                Interaction.MsgBox("You didn't select any folder!");
            }
        }//end Retrieve function

        private void newButt_Click(object sender, EventArgs e){//Add New Button
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
        }
        private void fillItemDGV(int id){//fill items DGV function
            itemsDGV.Visible = true;
            label2.Visible = true;
            try{ //open Connection
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            MySqlCommand cmd = new MySqlCommand("select id_item, name_item, category_item, description_item, s_width, s_length, s_height, quantity_item, remark_item from item where id_job_card = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            itemsDGV.DataSource = dt;
            dr.Close();
            con.Close();

            itemsDGV.Columns[0].HeaderText = "id item";
            itemsDGV.Columns[1].HeaderText = "Name";
            itemsDGV.Columns[2].HeaderText = "Category";
            itemsDGV.Columns[3].HeaderText = "Description";
            itemsDGV.Columns[4].HeaderText = "Width";
            itemsDGV.Columns[5].HeaderText = "Length";
            itemsDGV.Columns[6].HeaderText = "Height";
            itemsDGV.Columns[7].HeaderText = "Quantity";
            itemsDGV.Columns[8].HeaderText = "Remark";

            for (int i = 0; i < 9; i++){
                itemsDGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            itemsDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            itemsDGV.EnableHeadersVisualStyles = false;
            //hide Id_item column 
            itemsDGV.Columns["id_item"].Visible = false;
        }// end fill items DGV function
        private void FilljobCardDJV() {//fill Job Cards DGV
            try { //open Connection
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show(ex.Message);
            }

            MySqlCommand cmd = new MySqlCommand("select * from job_card where id_job > 0", con);
            //MySqlCommand cmd = new MySqlCommand("select order_date, delive_date, location, area, type, saleman, name_item, customer, lift_size_len, lift_size_width FROM job_card JOIN item i on job_card.id_job = i.id_job_card", con);

            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            jobCardDJV.DataSource = dt;
            dr.Close();
            con.Close();

            jobCardDJV.Columns[0].HeaderText = "ID Job Card";
            jobCardDJV.Columns[1].HeaderText = "Order Date";
            jobCardDJV.Columns[2].HeaderText = "Delivery Date";
            jobCardDJV.Columns[3].HeaderText = "Location";
            jobCardDJV.Columns[4].HeaderText = "Area";
            jobCardDJV.Columns[5].HeaderText = "Type";
            jobCardDJV.Columns[6].HeaderText = "Saleman";
            jobCardDJV.Columns[7].HeaderText = "Customer";
            jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
            jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
            for(int i = 0; i < 9; i++){
                //jobCardDJV.Columns[i].Width = 50;
                jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            jobCardDJV.EnableHeadersVisualStyles = false;

        }//end fill Job Cards DGV

        private void Form2_Load(object sender, EventArgs e){ //form load
            button1.Enabled = false;//show drawing off
            FilljobCardDJV();
            //fill emirates
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Abu Dhabi");
            comboBox1.Items.Add("Dubai");
            comboBox1.Items.Add("Sharjah");
            comboBox1.Items.Add("Ajman");
            comboBox1.Items.Add("Umm Al Quwain");
            comboBox1.Items.Add("Ras Al Khaimah");
            comboBox1.Items.Add("Fujairah");
            //
            textBox2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e){//show item drawing button
            RetrievePDF(id_Selitem);
        }

        private void jobCardDJV_CellClick(object sender, DataGridViewCellEventArgs e){//select row to generate jobCard Id and call fillItemDGV function
            button1.Enabled = false;//(off drawing) because no item is selected yet
            if (e.RowIndex >= 0){// Check if the click was on a row header (to avoid processing clicks on column headers)
                //select a row when select one cell in this row
                // Deselect all other rows
                jobCardDJV.ClearSelection();
                // Select the clicked row
                jobCardDJV.Rows[e.RowIndex].Selected = true;
                //
                DataGridViewRow row = jobCardDJV.Rows[e.RowIndex];
                // Access the value of a cell in the row by specifying the column index
                int id = (int)row.Cells[0].Value;
                //Interaction.MsgBox(value);
                fillItemDGV(id);
            }
        }

        private void itemsDGV_CellClick(object sender, DataGridViewCellEventArgs e){//select a row when select one cell in this row
            // Deselect all other rows
            itemsDGV.ClearSelection();
            // Select the clicked row
            itemsDGV.Rows[e.RowIndex].Selected = true;
            DataGridViewRow row = itemsDGV.Rows[e.RowIndex];
            // Access the value of a cell in the row by specifying the column index
            id_Selitem = (int)row.Cells[0].Value;
            button1.Enabled = true;
        }

        private void excelButt_Click(object sender, EventArgs e){//jobCardDJV to excel
            //
            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

            // Set the header row in Excel
            for (int i = 1; i <= jobCardDJV.Columns.Count; i++){
                worksheet.Cells[1, i] = jobCardDJV.Columns[i - 1].HeaderText;
            }

            // Set the values in Excel
            for (int i = 0; i < jobCardDJV.Rows.Count; i++){
                for (int j = 0; j < jobCardDJV.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = jobCardDJV.Rows[i].Cells[j].Value.ToString();
                }
            }

            // Save the Excel workbook and close the Excel application
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.InitialDirectory = "C:\\";
            saveFileDialog.FileName = "AllJobCards.xlsx";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK){
                string fileName = saveFileDialog.FileName;
                // Save the file using the specified file name and location
                workbook.SaveAs(fileName);
            }
            
            excel.Quit();
        }//end save to excel button

        private void clearButt_Click(object sender, EventArgs e){//clear all filters
            //
            itemsDGV.Visible = false;
            label2.Visible = false;
            //
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;

            radioButton1.Checked = false;
            radioButton2.Checked = false;

            comboBox1.Text = string.Empty;

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;

            this.OnLoad(e);// call form load
            itemsDGV.DataSource = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e){//enable/desable txtbox2 
            //*****
            textBox5.Text = string.Empty;
            textBox4.Text = string.Empty;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox3.Text = string.Empty;
            comboBox1.Text = string.Empty;
            //*****
            if (textBox1.Text != string.Empty){
                textBox2.Enabled = true;
            }

            else{
                textBox2.Text = string.Empty;
                textBox2.Enabled = false;
                this.OnLoad(e);// call form load
            }
                
        }

        private void textBox2_TextChanged(object sender, EventArgs e){// filter 1
            itemsDGV.Visible = false;
            label2.Visible = false;

            if (textBox2.Text != string.Empty){
                try
                { //open Connection
                    con.Open();
                    
                }
                catch (MySql.Data.MySqlClient.MySqlException ex){
                    MessageBox.Show(ex.Message);
                }
                //----------------
                MySqlCommand cmd = new MySqlCommand("select * from job_card where id_job between @txt1 AND @txt2", con);
                cmd.Parameters.AddWithValue("@txt1", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@txt2", int.Parse(textBox2.Text));
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                jobCardDJV.DataSource = dt;
                dr.Close();
                //---------------
                jobCardDJV.Columns[0].HeaderText = "ID Job Card";
                jobCardDJV.Columns[1].HeaderText = "Order Date";
                jobCardDJV.Columns[2].HeaderText = "Delivery Date";
                jobCardDJV.Columns[3].HeaderText = "Location";
                jobCardDJV.Columns[4].HeaderText = "Area";
                jobCardDJV.Columns[5].HeaderText = "Type";
                jobCardDJV.Columns[6].HeaderText = "Saleman";
                jobCardDJV.Columns[7].HeaderText = "Customer";
                jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
                jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
                for (int i = 0; i < 9; i++){
                    //jobCardDJV.Columns[i].Width = 50;
                    jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //for header color
                jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
                jobCardDJV.EnableHeadersVisualStyles = false;

                con.Close();
            }//end if
            else{
                this.OnLoad(e);// call form load
            }
        }//end filter

        private void jobCardDJV_CellDoubleClick(object sender, DataGridViewCellEventArgs e){//double click to edit jobcard
            if(e.RowIndex >= 0){// Check if the click was on a row header (to avoid processing clicks on column headers)
                //select a row when select one cell in this row
                // Deselect all other rows
                jobCardDJV.ClearSelection();
                // Select the clicked row
                jobCardDJV.Rows[e.RowIndex].Selected = true;
                DataGridViewRow row = jobCardDJV.Rows[e.RowIndex];
                // Access the value of a cell in the row by specifying the column index
                int id = (int)row.Cells[0].Value;

                //show other window
                this.Hide();
                Form1 frm = new Form1(id, true);//edit mode
                frm.Show();
            }
        }//end double click to edit jobcard event


        private void button2_Click(object sender, EventArgs e){//save job card as pdf Button (printable template)
            /*//get document folder path
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //retrieve template
            byte[] pdfData = null;
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            var cmd1 = new MySqlCommand("select pdf_template from files", con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read()){
                pdfData = (byte[])dr1["pdf_template"];
            }
            dr1.Close();
            con.Close();
            //save to document folder
            File.WriteAllBytes(filePath + "\\in.pdf", pdfData);
            //******************************OK**********************************/
            
        }//end save job card as pdf (printable template)

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){//combobox emirates filter
            itemsDGV.Visible = false;
            label2.Visible = false;
            if (comboBox1.Text != String.Empty){
                try{ //open Connection
                    con.Open();

                }
                catch (MySql.Data.MySqlClient.MySqlException ex){
                    MessageBox.Show(ex.Message);
                }
                //----------------------------------------------------------------
                MySqlCommand cmd = new MySqlCommand("select * from job_card where location = @loc", con);
                cmd.Parameters.AddWithValue("@loc", comboBox1.Text);
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                jobCardDJV.DataSource = dt;
                dr.Close();
                //---------------------------------------------------------------
                jobCardDJV.Columns[0].HeaderText = "ID Job Card";
                jobCardDJV.Columns[1].HeaderText = "Order Date";
                jobCardDJV.Columns[2].HeaderText = "Delivery Date";
                jobCardDJV.Columns[3].HeaderText = "Location";
                jobCardDJV.Columns[4].HeaderText = "Area";
                jobCardDJV.Columns[5].HeaderText = "Type";
                jobCardDJV.Columns[6].HeaderText = "Saleman";
                jobCardDJV.Columns[7].HeaderText = "Customer";
                jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
                jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
                for (int i = 0; i < 9; i++)
                {
                    //jobCardDJV.Columns[i].Width = 50;
                    jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //for header color
                jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
                jobCardDJV.EnableHeadersVisualStyles = false;

                con.Close();
            }//end if
            else
            {
                this.OnLoad(e);// call form load
            }
        }//end combobox emirates filter

        private void radioButton2_CheckedChanged(object sender, EventArgs e){ //Type filter 1
            //*******
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox3.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            //*******
            itemsDGV.Visible = false;
            label2.Visible = false;
            try
            { //open Connection
                con.Open();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //----------------------------------------------------------------
            MySqlCommand cmd = new MySqlCommand("select * from job_card where type = 'villa'", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            jobCardDJV.DataSource = dt;
            dr.Close();
            //---------------------------------------------------------------
            jobCardDJV.Columns[0].HeaderText = "ID Job Card";
            jobCardDJV.Columns[1].HeaderText = "Order Date";
            jobCardDJV.Columns[2].HeaderText = "Delivery Date";
            jobCardDJV.Columns[3].HeaderText = "Location";
            jobCardDJV.Columns[4].HeaderText = "Area";
            jobCardDJV.Columns[5].HeaderText = "Type";
            jobCardDJV.Columns[6].HeaderText = "Saleman";
            jobCardDJV.Columns[7].HeaderText = "Customer";
            jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
            jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
            for (int i = 0; i < 9; i++)
            {
                //jobCardDJV.Columns[i].Width = 50;
                jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            jobCardDJV.EnableHeadersVisualStyles = false;

            con.Close();
        }//end 1

        private void radioButton1_CheckedChanged(object sender, EventArgs e){ //Type filter 2
            //*******
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox3.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            //*******
            itemsDGV.Visible = false;
            label2.Visible = false;
            try
            { //open Connection
                con.Open();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //----------------------------------------------------------------
            MySqlCommand cmd = new MySqlCommand("select * from job_card where type = 'BDG'", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            jobCardDJV.DataSource = dt;
            dr.Close();
            //---------------------------------------------------------------
            jobCardDJV.Columns[0].HeaderText = "ID Job Card";
            jobCardDJV.Columns[1].HeaderText = "Order Date";
            jobCardDJV.Columns[2].HeaderText = "Delivery Date";
            jobCardDJV.Columns[3].HeaderText = "Location";
            jobCardDJV.Columns[4].HeaderText = "Area";
            jobCardDJV.Columns[5].HeaderText = "Type";
            jobCardDJV.Columns[6].HeaderText = "Saleman";
            jobCardDJV.Columns[7].HeaderText = "Customer";
            jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
            jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
            for (int i = 0; i < 9; i++)
            {
                //jobCardDJV.Columns[i].Width = 50;
                jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            jobCardDJV.EnableHeadersVisualStyles = false;

            con.Close();
        }//end 2

        private void textBox4_TextChanged(object sender, EventArgs e){//saleman filer
            
            itemsDGV.Visible = false;
            label2.Visible = false;
            if (textBox4.Text != string.Empty){
                try
                { //open Connection
                    con.Open();

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //----------------SELECT * FROM employees WHERE last_name LIKE '%Smith%';
                MySqlCommand cmd = new MySqlCommand("select * from job_card where saleman LIKE @sm", con);
                cmd.Parameters.AddWithValue("@sm", "%" + textBox4.Text + "%");
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                jobCardDJV.DataSource = dt;
                dr.Close();
                //---------------
                jobCardDJV.Columns[0].HeaderText = "ID Job Card";
                jobCardDJV.Columns[1].HeaderText = "Order Date";
                jobCardDJV.Columns[2].HeaderText = "Delivery Date";
                jobCardDJV.Columns[3].HeaderText = "Location";
                jobCardDJV.Columns[4].HeaderText = "Area";
                jobCardDJV.Columns[5].HeaderText = "Type";
                jobCardDJV.Columns[6].HeaderText = "Saleman";
                jobCardDJV.Columns[7].HeaderText = "Customer";
                jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
                jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
                for (int i = 0; i < 9; i++)
                {
                    //jobCardDJV.Columns[i].Width = 50;
                    jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //for header color
                jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
                jobCardDJV.EnableHeadersVisualStyles = false;

                con.Close();
            }//end if
            else
            {
                this.OnLoad(e);// call form load
            }
        }//end saleman filer

        private void textBox5_TextChanged(object sender, EventArgs e){//customer filter
            
            itemsDGV.Visible = false;
            label2.Visible = false;
            if (textBox5.Text != string.Empty)
            {
                try
                { //open Connection
                    con.Open();

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //----------------
                MySqlCommand cmd = new MySqlCommand("select * from job_card where customer like @cu", con);
                cmd.Parameters.AddWithValue("@cu", "%" + textBox5.Text + "%");
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                jobCardDJV.DataSource = dt;
                dr.Close();
                //---------------
                jobCardDJV.Columns[0].HeaderText = "ID Job Card";
                jobCardDJV.Columns[1].HeaderText = "Order Date";
                jobCardDJV.Columns[2].HeaderText = "Delivery Date";
                jobCardDJV.Columns[3].HeaderText = "Location";
                jobCardDJV.Columns[4].HeaderText = "Area";
                jobCardDJV.Columns[5].HeaderText = "Type";
                jobCardDJV.Columns[6].HeaderText = "Saleman";
                jobCardDJV.Columns[7].HeaderText = "Customer";
                jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
                jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
                for (int i = 0; i < 9; i++)
                {
                    //jobCardDJV.Columns[i].Width = 50;
                    jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //for header color
                jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
                jobCardDJV.EnableHeadersVisualStyles = false;

                con.Close();
            }//end if
            else
            {
                this.OnLoad(e);// call form load
            }
        }//end customer filter

        private void textBox3_TextChanged(object sender, EventArgs e){//area filter
            
            itemsDGV.Visible = false;
            label2.Visible = false;
            if (textBox3.Text != string.Empty)
            {
                try
                { //open Connection
                    con.Open();

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //----------------SELECT * FROM employees WHERE last_name LIKE '%Smith%';
                MySqlCommand cmd = new MySqlCommand("select * from job_card where area LIKE @ar", con);
                cmd.Parameters.AddWithValue("@ar", "%" + textBox3.Text + "%");
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                jobCardDJV.DataSource = dt;
                dr.Close();
                //---------------
                jobCardDJV.Columns[0].HeaderText = "ID Job Card";
                jobCardDJV.Columns[1].HeaderText = "Order Date";
                jobCardDJV.Columns[2].HeaderText = "Delivery Date";
                jobCardDJV.Columns[3].HeaderText = "Location";
                jobCardDJV.Columns[4].HeaderText = "Area";
                jobCardDJV.Columns[5].HeaderText = "Type";
                jobCardDJV.Columns[6].HeaderText = "Saleman";
                jobCardDJV.Columns[7].HeaderText = "Customer";
                jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
                jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
                for (int i = 0; i < 9; i++)
                {
                    //jobCardDJV.Columns[i].Width = 50;
                    jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //for header color
                jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
                jobCardDJV.EnableHeadersVisualStyles = false;

                con.Close();
            }//end if
            else
            {
                this.OnLoad(e);// call form load
            }
        }//end area filter

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e){//order date filter 101
            itemsDGV.Visible = false;
            label2.Visible = false;
            try
            { //open Connection
                con.Open();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //----------------
            MySqlCommand cmd = new MySqlCommand("select * from job_card where order_date between @date1 AND @date2", con);
            cmd.Parameters.AddWithValue("@date1", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@date2", dateTimePicker2.Value);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            jobCardDJV.DataSource = dt;
            dr.Close();
            //---------------
            jobCardDJV.Columns[0].HeaderText = "ID Job Card";
            jobCardDJV.Columns[1].HeaderText = "Order Date";
            jobCardDJV.Columns[2].HeaderText = "Delivery Date";
            jobCardDJV.Columns[3].HeaderText = "Location";
            jobCardDJV.Columns[4].HeaderText = "Area";
            jobCardDJV.Columns[5].HeaderText = "Type";
            jobCardDJV.Columns[6].HeaderText = "Saleman";
            jobCardDJV.Columns[7].HeaderText = "Customer";
            jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
            jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
            for (int i = 0; i < 9; i++)
            {
                //jobCardDJV.Columns[i].Width = 50;
                jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            jobCardDJV.EnableHeadersVisualStyles = false;

            con.Close();
        }//end order date filter 101

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e){// oreder date filter 102
            itemsDGV.Visible = false;
            label2.Visible = false;
            try
            { //open Connection
                con.Open();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //----------------
            MySqlCommand cmd = new MySqlCommand("select * from job_card where order_date between @date1 AND @date2", con);
            cmd.Parameters.AddWithValue("@date1", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@date2", dateTimePicker2.Value);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            jobCardDJV.DataSource = dt;
            dr.Close();
            //---------------
            jobCardDJV.Columns[0].HeaderText = "ID Job Card";
            jobCardDJV.Columns[1].HeaderText = "Order Date";
            jobCardDJV.Columns[2].HeaderText = "Delivery Date";
            jobCardDJV.Columns[3].HeaderText = "Location";
            jobCardDJV.Columns[4].HeaderText = "Area";
            jobCardDJV.Columns[5].HeaderText = "Type";
            jobCardDJV.Columns[6].HeaderText = "Saleman";
            jobCardDJV.Columns[7].HeaderText = "Customer";
            jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
            jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
            for (int i = 0; i < 9; i++){
                //jobCardDJV.Columns[i].Width = 50;
                jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            jobCardDJV.EnableHeadersVisualStyles = false;

            con.Close();
        }// end oreder date filter 102

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e){// delivery date filter 102
            itemsDGV.Visible = false;
            label2.Visible = false;
            try
            { //open Connection
                con.Open();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //----------------
            MySqlCommand cmd = new MySqlCommand("select * from job_card where delive_date between @date1 AND @date2", con);
            cmd.Parameters.AddWithValue("@date1", dateTimePicker4.Value);
            cmd.Parameters.AddWithValue("@date2", dateTimePicker3.Value);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            jobCardDJV.DataSource = dt;
            dr.Close();
            //---------------
            jobCardDJV.Columns[0].HeaderText = "ID Job Card";
            jobCardDJV.Columns[1].HeaderText = "Order Date";
            jobCardDJV.Columns[2].HeaderText = "Delivery Date";
            jobCardDJV.Columns[3].HeaderText = "Location";
            jobCardDJV.Columns[4].HeaderText = "Area";
            jobCardDJV.Columns[5].HeaderText = "Type";
            jobCardDJV.Columns[6].HeaderText = "Saleman";
            jobCardDJV.Columns[7].HeaderText = "Customer";
            jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
            jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
            for (int i = 0; i < 9; i++){
                //jobCardDJV.Columns[i].Width = 50;
                jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            jobCardDJV.EnableHeadersVisualStyles = false;

            con.Close();
        }//end delivery date filter 102

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e){//deliver date filter 101
            itemsDGV.Visible = false;
            label2.Visible = false;
            try
            { //open Connection
                con.Open();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //----------------
            MySqlCommand cmd = new MySqlCommand("select * from job_card where delive_date between @date1 AND @date2", con);
            cmd.Parameters.AddWithValue("@date1", dateTimePicker4.Value);
            cmd.Parameters.AddWithValue("@date2", dateTimePicker3.Value);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            jobCardDJV.DataSource = dt;
            dr.Close();
            //---------------
            jobCardDJV.Columns[0].HeaderText = "ID Job Card";
            jobCardDJV.Columns[1].HeaderText = "Order Date";
            jobCardDJV.Columns[2].HeaderText = "Delivery Date";
            jobCardDJV.Columns[3].HeaderText = "Location";
            jobCardDJV.Columns[4].HeaderText = "Area";
            jobCardDJV.Columns[5].HeaderText = "Type";
            jobCardDJV.Columns[6].HeaderText = "Saleman";
            jobCardDJV.Columns[7].HeaderText = "Customer";
            jobCardDJV.Columns[8].HeaderText = "Lift Size Length";
            jobCardDJV.Columns[9].HeaderText = "Lift Size Width";
            for (int i = 0; i < 9; i++){
                //jobCardDJV.Columns[i].Width = 50;
                jobCardDJV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //for header color
            jobCardDJV.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            jobCardDJV.EnableHeadersVisualStyles = false;

            con.Close();
        }//end deliver date filter 101
    }//end Form2 class
}//end namespace
