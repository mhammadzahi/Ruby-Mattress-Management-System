using System;
using Microsoft.VisualBasic;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using MySqlX.XDevAPI.Relational;
//using System.Data.SqlClient;
//using System.Xml;
//using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Diagnostics;
//using System.Drawing;
using System.Globalization;
//using static System.Net.WebRequestMethods;

namespace Ruby_Mattress_Management_System{
    public partial class Form1 : Form {
        string fileName;
        int jobCrdId;
        bool newJobCard;
        int id_edit;
        bool is_edit = false;
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection("server=localhost; database=productionmaster; uid=root");
        public Form1(int id_edit, bool is_edit){//const 1
            InitializeComponent();
            this.id_edit = id_edit;
            this.is_edit = is_edit;
        }
        public Form1(){//const 2
            InitializeComponent();
        }
        public string uploadDraw(){//return file path --for item--
            using (OpenFileDialog openFileDialog = new OpenFileDialog()){
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if(openFileDialog.ShowDialog() == DialogResult.OK){
                    return openFileDialog.FileName;
                }
                else{
                    return "File not selected!";
                }
            }
            
        }//end uploadDraw for item
        //****************************************************************
        private void verJobContr(){ //against empty jobcard controls
            if (newJobCard){
                button6.Enabled = false;
            }
            else{
                if (comboBox1.Text == string.Empty || comboBox2.Text == string.Empty || (villaRadio.Checked == false && bdgRadio.Checked == false) || (bdgRadio.Checked == true && (textBox8.Text == string.Empty || textBox1.Text == string.Empty)) || emiratesComb.Text == string.Empty)
                {
                    if (checkBox1.Checked == true && bdgRadio.Checked == true)
                    {
                        button6.Enabled = true;
                    }
                    else
                    {
                        button6.Enabled = false;
                    }
                }
                else
                {
                    button6.Enabled = true;
                }
            }
        }
        private void verIemContr(){ //against empty items  controls
            if(comboBox3.Text == string.Empty || comboBox4.Text == string.Empty || textBox4.Text == string.Empty || i_width.Text == string.Empty || i_length.Text == string.Empty || i_height.Text == string.Empty || textBox6.Text == string.Empty || textBox7.Text == string.Empty || button9.Text == "Upload Drawings")
                button4.Enabled = false;
            else
                button4.Enabled = true;
        }
        private void fillData_ed(){//fill items DGV edit mode
            try{ //open Connection
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            MySqlCommand cmd = new MySqlCommand("select name_item, category_item, description_item, s_width, s_length, s_height, quantity_item, remark_item, drawing_name from item where id_job_card = @idJobCard", con);
            cmd.Parameters.AddWithValue("@idJobCard", id_edit);
            MySqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridItem.DataSource = dt;
            dr.Close();
            con.Close();

            dataGridItem.Columns[0].HeaderText = "Name";
            dataGridItem.Columns[1].HeaderText = "Category";
            dataGridItem.Columns[2].HeaderText = "Description";
            dataGridItem.Columns[3].HeaderText = "Width";
            dataGridItem.Columns[4].HeaderText = "Length";
            dataGridItem.Columns[5].HeaderText = "Height";
            dataGridItem.Columns[6].HeaderText = "Quantity";
            dataGridItem.Columns[7].HeaderText = "Remarks";
            dataGridItem.Columns[8].HeaderText = "Drawing";

            dataGridItem.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            dataGridItem.EnableHeadersVisualStyles = false;
            //description column width
            dataGridItem.Columns[2].Width = 432;
        }//end fill items DGV edit mode
        private void fillData(){//fill items DGV creation mode
            try{ //open Connection
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            MySqlCommand cmd = new MySqlCommand("select name_item, category_item, description_item, s_width, s_length, s_height, quantity_item, remark_item, drawing_name from item where id_job_card = @idJobCard", con);
            cmd.Parameters.AddWithValue("@idJobCard", jobCrdId);
            MySqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridItem.DataSource = dt;
            dr.Close();
            con.Close();
            
            dataGridItem.Columns[0].HeaderText = "Name";
            dataGridItem.Columns[1].HeaderText = "Category";
            dataGridItem.Columns[2].HeaderText = "Description";
            dataGridItem.Columns[3].HeaderText = "Width";
            dataGridItem.Columns[4].HeaderText = "Length";
            dataGridItem.Columns[5].HeaderText = "Height";
            dataGridItem.Columns[6].HeaderText = "Quantity";
            dataGridItem.Columns[7].HeaderText = "Remarks";
            dataGridItem.Columns[8].HeaderText = "Drawing";

            dataGridItem.ColumnHeadersDefaultCellStyle.BackColor = Color.Coral;
            dataGridItem.EnableHeadersVisualStyles = false;
            //description column width
            dataGridItem.Columns[2].Width = 432;
        }//end fill items DGV creation mode
        private void refresh_Ed_(){//refresh job card in edit mode
            //villa vs BDG checkbox problem
            villaRadio.Enabled = true;
            bdgRadio.Enabled = true;
            textBox8.Enabled = true;
            textBox1.Enabled = true;
            //label2.Enabled = true;
            label12.Enabled = true;
            checkBox1.Checked = false;
            villaRadio.Checked = false;
            bdgRadio.Checked = false;
            //
            fileName = null;
            //
            button2.Enabled = false;
            button8.Enabled = false;
            button3.Enabled = false;
            button9.Text = "Upload Drawings";

            //empty controls
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            emiratesComb.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            i_height.Text = string.Empty;
            i_width.Text = string.Empty;
            i_length.Text = string.Empty;

            //fill emirates
            emiratesComb.Items.Clear();
            emiratesComb.Items.Add("Abu Dhabi");
            emiratesComb.Items.Add("Dubai");
            emiratesComb.Items.Add("Sharjah");
            emiratesComb.Items.Add("Ajman");
            emiratesComb.Items.Add("Umm Al Quwain");
            emiratesComb.Items.Add("Ras Al Khaimah");
            emiratesComb.Items.Add("Fujairah");
            // Fill Categories
            comboBox4.Items.Add("A");
            comboBox4.Items.Add("B");
            comboBox4.Items.Add("C");
            comboBox4.Items.Add("D");

            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Fill Saleman Combo
            var cmd1 = new MySqlCommand("select name_saleman from saleman", con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1.GetString(0));
            }
            dr1.Close();

            //Fill customer Combo
            var cmd2 = new MySqlCommand("select name_customer from customer", con);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox1.Items.Add(dr2.GetString(0));
            }
            dr2.Close();

            //Fill item Combo
            var cmd3 = new MySqlCommand("select name_ from items_combo", con);
            MySqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                comboBox3.Items.Add(dr3.GetString(0));
            }
            dr3.Close();
            //
            con.Close();

            fillData_ed();//DGVItems
            verIemContr();
            verJobContr();
        }//end refresh in edit mode function

        private void refresh_(){//refresh in job card creation mode
            //villa vs BDG checkbox problem
            villaRadio.Enabled = true;
            bdgRadio.Enabled = true;
            textBox8.Enabled = true;
            textBox1.Enabled = true;
            //label2.Enabled = true;
            label12.Enabled = true;
            checkBox1.Checked = false;
            villaRadio.Checked = false;
            bdgRadio.Checked = false;
            //
            fileName = null;
            //
            button2.Enabled = false;
            button8.Enabled = false;
            button3.Enabled = false;
            button9.Text = "Upload Drawings";

            //empty controls
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            emiratesComb.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            i_height.Text = string.Empty;
            i_width.Text = string.Empty;
            i_length.Text = string.Empty;
            
            //fill emirates
            emiratesComb.Items.Clear();
            emiratesComb.Items.Add("Abu Dhabi");
            emiratesComb.Items.Add("Dubai");
            emiratesComb.Items.Add("Sharjah");
            emiratesComb.Items.Add("Ajman");
            emiratesComb.Items.Add("Umm Al Quwain");
            emiratesComb.Items.Add("Ras Al Khaimah");
            emiratesComb.Items.Add("Fujairah");
            // Fill Categories
            comboBox4.Items.Add("A");
            comboBox4.Items.Add("B");
            comboBox4.Items.Add("C");
            comboBox4.Items.Add("D");

            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            //Fill Saleman Combo
            var cmd1 = new MySqlCommand("select name_saleman from saleman", con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read()){
                comboBox2.Items.Add(dr1.GetString(0));
            }
            dr1.Close();

            //Fill customer Combo
            var cmd2 = new MySqlCommand("select name_customer from customer", con);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read()){
                comboBox1.Items.Add(dr2.GetString(0));
            }
            dr2.Close();

            //Fill item Combo
            var cmd3 = new MySqlCommand("select name_ from items_combo", con);
            MySqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read()){
                comboBox3.Items.Add(dr3.GetString(0));
            }
            dr3.Close();
            con.Close();

            fillData();//DGV
            verIemContr();
            verJobContr();
        }//end refresh func in creation mode
        private void Form1_Load(object sender, EventArgs e){//form1.Load
            if(is_edit){
                //show label2
                label2.Text = "Edit Job Card N° " + id_edit;
                label2.Visible = true;
                //Interaction.MsgBox(is_edit + " " + id_edit);
                refresh_Ed_();
            }
            else{
                label2.Text = "Create a new Job Card";
                label2.Visible = true;
                jobCrdId = generInt();
                newJobCard = true;
                refresh_();
            }
        }//end form load

        private void radioButton1_CheckedChanged(object sender, EventArgs e){
            textBox8.Text = string.Empty;
            textBox1.Text = string.Empty;

            textBox8.Enabled = false;
            textBox1.Enabled = false;

            label12.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e){
            checkBox1.Checked = false;
            textBox8.Enabled = true;
            textBox1.Enabled = true;
            label12.Enabled = true;
            //label2.Enabled = true;
        }
        private void button9_Click(object sender, EventArgs e){//upload drawing
            fileName = uploadDraw();
            if (fileName != "File not selected!")
                button9.Text = Path.GetFileName(fileName);
            else
                button9.Text = "Upload Drawings";

            verIemContr();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e){//handle checkbox event
            
            textBox1.Text = string.Empty;
            textBox8.Text = string.Empty;
            if(checkBox1.Checked == true){
                textBox8.Text = string.Empty;
                textBox1.Text = string.Empty;
                //
                textBox8.Enabled = false;
                textBox1.Enabled = false;
          
                label12.Enabled = false;
            }
            else {
                villaRadio.Enabled = true;
                bdgRadio.Enabled = true;
                //
                textBox8.Enabled = true;
                textBox1.Enabled = true;
                //label2.Enabled = true;
                label12.Enabled = true;
            }
            verJobContr();
        }//end checkbox event

        private void button10_Click(object sender, EventArgs e){
          
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e){//save job card in db
            if (is_edit){

            }
            else{
                string itemType;
                if (villaRadio.Checked == true)
                {
                    itemType = "villa";
                }
                else if (bdgRadio.Checked == true)
                {
                    itemType = "BDG";
                }
                else
                {
                    itemType = null;
                }
                try
                {
                    con.Open();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MySqlCommand cmd;
                if (textBox1.Text != string.Empty || textBox8.Text != string.Empty){
                    //byte[] pdfData = File.ReadAllBytes(fileNameJob);
                    cmd = new MySqlCommand("update job_card set order_date = DATE_FORMAT(@od, '%Y-%m-%d %H:%i:%s'), delive_date = DATE_FORMAT(@dd, '%Y-%m-%d %H:%i:%s'), location = @loc, area = @a, type = @t, saleman = @sm, customer = @cust, lift_size_len = @len, lift_size_width = @wid where id_job = @id", con);
                    cmd.Parameters.AddWithValue("@id", jobCrdId);
                    cmd.Parameters.AddWithValue("@od", dateTimePicker2.Value);
                    cmd.Parameters.AddWithValue("@dd", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@loc", emiratesComb.Text);
                    cmd.Parameters.AddWithValue("@a", textBox9.Text);
                    cmd.Parameters.AddWithValue("@t", itemType);
                    cmd.Parameters.AddWithValue("@sm", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@cust", comboBox1.Text);
                    //cmd.Parameters.AddWithValue("@i", comboBox3.Text);
                    //cmd.Parameters.AddWithValue("@file_", pdfData);
                    cmd.Parameters.AddWithValue("@len", int.Parse(textBox8.Text));
                    cmd.Parameters.AddWithValue("@wid", int.Parse(textBox1.Text));
                    cmd.ExecuteNonQuery();
                }
                else{
                    //byte[] pdfData = File.ReadAllBytes(fileNameJob);
                    cmd = new MySqlCommand("update job_card set order_date = @od, delive_date = @dd, location = @loc, area = @a, type = @t, saleman = @sm, customer = @cust where id_job = @id", con);
                    cmd.Parameters.AddWithValue("@id", jobCrdId);
                    cmd.Parameters.AddWithValue("@od", dateTimePicker2);
                    cmd.Parameters.AddWithValue("@dd", dateTimePicker1);
                    cmd.Parameters.AddWithValue("@loc", emiratesComb.Text);
                    cmd.Parameters.AddWithValue("@a", textBox9.Text);
                    cmd.Parameters.AddWithValue("@t", itemType);
                    cmd.Parameters.AddWithValue("@sm", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@cust", comboBox1.Text);
                    //cmd.Parameters.AddWithValue("@i", comboBox3.Text);
                    //cmd.Parameters.AddWithValue("@file_", pdfData);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                newJobCard = true;
                jobCrdId = generInt();
                refresh_();
            }
        }//end save job card in db

        private void button1_Click(object sender, EventArgs e){//add new customer
            comboBox1.SelectedIndex = -1;
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            string customerName, customerNumber, msg1, title1, msg2, title2;

            msg1 = "Enter a customer name: ";
            msg2 = "Enter a customer number: ";

            title1 = "Customer Name";
            title2 = "Customer number";

            customerName = Interaction.InputBox(msg1, title1);
            customerNumber = Interaction.InputBox(msg2, title2);
            //insert to database
            if(customerName != string.Empty && customerNumber != string.Empty){
                MySqlCommand cmd = new MySqlCommand("insert into customer(name_customer, number_customer) values(@name, @number)", con);
                cmd.Parameters.AddWithValue("@name", customerName);
                cmd.Parameters.AddWithValue("@number", customerNumber);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            refresh_();
        }

        private void button7_Click(object sender, EventArgs e){//edit saleman
            comboBox2.SelectedIndex = -1;
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            string saleManeNamae, saleManeNumber, msg1, title1, msg2, title2;

            msg1 = "Enter a saleman name: ";
            msg2 = "Enter a saleman number: ";

            title1 = "saleMan Name";
            title2 = "saleMan number";

            saleManeNamae = Interaction.InputBox(msg1, title1);
            saleManeNumber = Interaction.InputBox(msg2, title2);
            //insert to database
            if(saleManeNamae != string.Empty && saleManeNumber != string.Empty) {
                MySqlCommand cmd = new MySqlCommand("insert into saleman(name_saleman, phone_number_saleman) values(@name, @number)", con);
                cmd.Parameters.AddWithValue("@name", saleManeNamae);
                cmd.Parameters.AddWithValue("@number", saleManeNumber);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            refresh_();
        }

        private void button4_Click(object sender, EventArgs e){//new item in the db
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            if(newJobCard){//create the first table (job_card) if its a new job card
                MySqlCommand cmd0 = new MySqlCommand("insert into job_card(id_job) values(@iddd)", con);
                cmd0.Parameters.AddWithValue("@iddd", jobCrdId);
                cmd0.ExecuteNonQuery();
                newJobCard = false;
            }

            //insert to database
            byte[] pdfData = File.ReadAllBytes(fileName);
            MySqlCommand cmd = new MySqlCommand("insert into item(name_item, category_item, description_item, s_length, s_width, s_height, quantity_item, remark_item, drawing_name, drawing, id_job_card) values(@na, @ca, @de, @len, @wid, @hei, @qu, @re, @drn, @dr, @idJob)", con);
            cmd.Parameters.AddWithValue("@na", comboBox3.Text);
            cmd.Parameters.AddWithValue("@ca", comboBox4.Text);
            cmd.Parameters.AddWithValue("@de", textBox4.Text);
            cmd.Parameters.AddWithValue("@len", int.Parse(i_length.Text));//int
            cmd.Parameters.AddWithValue("@wid", int.Parse(i_width.Text));//int
            cmd.Parameters.AddWithValue("@hei", int.Parse(i_height.Text));//int
            cmd.Parameters.AddWithValue("@qu", int.Parse(textBox6.Text));//int
            cmd.Parameters.AddWithValue("@re", textBox7.Text);
            cmd.Parameters.AddWithValue("@drn", button9.Text);
            cmd.Parameters.AddWithValue("@dr", pdfData);
            cmd.Parameters.AddWithValue("@idJob", jobCrdId);
            cmd.ExecuteNonQuery();

            con.Close();
            refresh_();
        }//end new item in db

        private void textBox7_TextChanged(object sender, EventArgs e){
            verIemContr();
        }
        private void textBox6_TextChanged(object sender, EventArgs e){
            verIemContr();
        }
        private void i_height_TextChanged(object sender, EventArgs e){
            verIemContr();
        }

        private void i_length_TextChanged(object sender, EventArgs e){
            verIemContr();
        }
        private void i_width_TextChanged(object sender, EventArgs e){
            verIemContr();
        }

        private void textBox4_TextChanged(object sender, EventArgs e){
            verIemContr();
        }

        private void i_width_KeyPress(object sender, KeyPressEventArgs e){
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void i_length_KeyPress(object sender, KeyPressEventArgs e){
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void i_height_KeyPress(object sender, KeyPressEventArgs e){
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e){
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e){//edit customer
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            string Namee, number, msg1, title1, msg2, title2;

            msg1 = "Enter the new customer name: ";
            msg2 = "Enter the new customer number: ";

            title1 = "Edit Customer Name";
            title2 = "Edit Customer Number";

            Namee = Interaction.InputBox(msg1, title1);
            number = Interaction.InputBox(msg2, title2);
            //update and save to database
            MySqlCommand cmd = new MySqlCommand("update customer set name_customer = @name, number_customer = @number where name_customer = @sn", con);
            cmd.Parameters.AddWithValue("@name", Namee);
            cmd.Parameters.AddWithValue("@number", number);
            cmd.Parameters.AddWithValue("@sn", comboBox1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            refresh_();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
            button2.Enabled = true;
            verJobContr();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e){
            button8.Enabled = true;
            verJobContr();
        }

        private void button11_Click(object sender, EventArgs e){
            refresh_();
        }

        private void button8_Click(object sender, EventArgs e){// update saleman
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            string Namee, number, msg1, title1, msg2, title2;

            msg1 = "Enter the new Saleman name: ";
            msg2 = "Enter the new Saleman number: ";

            title1 = "Edit Saleman Name";
            title2 = "Edit Saleman Number";

            Namee = Interaction.InputBox(msg1, title1);
            number = Interaction.InputBox(msg2, title2);
            //update and save to database
            MySqlCommand cmd = new MySqlCommand("update saleman set name_saleman = @name, phone_number_saleman = @number where name_saleman = @sn", con);
            cmd.Parameters.AddWithValue("@name", Namee);
            cmd.Parameters.AddWithValue("@number", number);
            cmd.Parameters.AddWithValue("@sn", comboBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            refresh_();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e){
            verIemContr();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e){
            verIemContr();
            //verJobContr();
        }

        private void button3_Click(object sender, EventArgs e){//for item edition, first select from DGV and then dit
            //nothing yet
        }

        private void button5_Click(object sender, EventArgs e){//add new item
            comboBox3.SelectedIndex = -1;
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            string itemNamae, msg1, title1;

            msg1 = "Enter an item name: ";

            title1 = "saleMan Name";

            itemNamae = Interaction.InputBox(msg1, title1);
            //insert to database
            if(itemNamae != string.Empty){
                MySqlCommand cmd = new MySqlCommand("insert into items_combo(name_) values(@item)", con);
                cmd.Parameters.AddWithValue("@item", itemNamae);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            refresh_();
        }
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e){
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e){
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        
        private void emiratesComb_SelectedIndexChanged(object sender, EventArgs e){
            verJobContr();
        }

        private void bdgRadio_Click(object sender, EventArgs e)
        {
            verJobContr();
        }

        private void villaRadio_Click(object sender, EventArgs e)
        {
            verJobContr();
        }

        private void textBox2_TextChanged(object sender, EventArgs e){//search
            int index = comboBox3.FindString(textBox2.Text);

            if (index != -1){
                comboBox3.SelectedIndex = index;
            }
            else
            {
                comboBox3.SelectedIndex = -1;
            }
            if (textBox2.Text == string.Empty){
                comboBox3.SelectedItem = null;
            }
        }//end search

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int index = comboBox2.FindString(textBox3.Text);

            if (index != -1)
            {
                comboBox2.SelectedIndex = index;
            }
            else
            {
                comboBox2.SelectedIndex = -1;
            }
            if (textBox3.Text == string.Empty)
            {
                comboBox2.SelectedItem = null;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e){//search
            int index = comboBox1.FindString(textBox5.Text);

            if (index != -1)
            {
                comboBox1.SelectedIndex = index;
            }
            else
            {
                comboBox1.SelectedIndex = -1;
            }
            if (textBox5.Text == string.Empty)
            {
                comboBox1.SelectedItem = null;
            }
        }

        private void button12_Click(object sender, EventArgs e){//switch to all job cards form
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }
        public int generInt(){//select max id and + 1 to it*************
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }
            var cmd1 = new MySqlCommand("select MAX(id_job) from job_card", con);
            int res = (int)cmd1.ExecuteScalar();
            con.Close();
            //DateTime date = DateTime.Now;
            //int numericalValue = (int)((date.ToUniversalTime().Ticks - new DateTime(0001, 01, 01, 0, 0, 0, DateTimeKind.Utc).Ticks) / 900000);
            //return Math.Abs(numericalValue);
            return res + 1;
        }

        private void textBox8_TextChanged(object sender, EventArgs e){//lift size
            verJobContr();
        }

        private void textBox1_TextChanged(object sender, EventArgs e){//lift size
            verJobContr();
        }

        private void textBox9_TextChanged(object sender, EventArgs e){
            verJobContr();
        }
    }//end form
}//end namespace