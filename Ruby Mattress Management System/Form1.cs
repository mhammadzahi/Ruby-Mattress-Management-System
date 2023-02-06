using System;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using MySqlX.XDevAPI.Relational;
using System.Data.SqlClient;
using System.Xml;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace Ruby_Mattress_Management_System
{
    public partial class Form1 : Form {
        string fileName;
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection("server=localhost; database=productionmaster; uid=root");
        public Form1(){
            InitializeComponent();
        }
        public string uploadDraw(){
            using (OpenFileDialog openFileDialog = new OpenFileDialog()){
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if(openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileName;
                else
                    return "error";
            }
            
        }//end uploadDraw function
        private void verIemContr(){
            if(comboBox3.Text == string.Empty || comboBox4.Text == string.Empty || textBox4.Text == string.Empty || i_width.Text == string.Empty || i_length.Text == string.Empty || i_height.Text == string.Empty || textBox6.Text == string.Empty || textBox7.Text == string.Empty)
                button4.Enabled = false;
            else
                button4.Enabled = true;
        }
        private void fillData(){
            try{ //open Connection
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            MySqlCommand cmd = new MySqlCommand("select name_item, category_item, description_item, s_width, s_length, s_height, quantity_item, remark_item from item", con);
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

        }

        private void refresh_(){
            button2.Enabled = false;
            button8.Enabled = false;
            button3.Enabled = false;

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
            // Fill Cat Comb
            comboBox4.Items.Add("A");
            comboBox4.Items.Add("B");
            comboBox4.Items.Add("C");
            comboBox4.Items.Add("D");

            try { //open Connection
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
            var cmd3 = new MySqlCommand("select name_item from item", con);
            MySqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read()){
                comboBox3.Items.Add(dr3.GetString(0));
            }
            dr3.Close();
            con.Close();

            fillData();

            verIemContr();
        }//end refresh func
        private void Form1_Load(object sender, EventArgs e){
            //Fill Emirates Combo
            refresh_();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e){
            textBox8.Enabled = false;
            textBox1.Enabled = false;
            label12.Enabled = false;
            label2.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e){
            textBox8.Enabled = true;
            textBox1.Enabled = true;
            label12.Enabled = true;
            label2.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e){//upload drawing
            fileName = uploadDraw();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e){
            
            if(checkBox1.Checked == true){
                villaRadio.Enabled = false;
                bdgRadio.Enabled = false;
                //
                textBox8.Enabled = false;
                textBox1.Enabled = false;
                label2.Enabled = false;
                label12.Enabled = false;
            }
            else {
                villaRadio.Enabled = true;
                bdgRadio.Enabled = true;
                //
                textBox8.Enabled = true;
                textBox1.Enabled = true;
                label2.Enabled = true;
                label12.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e){
          
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e){ //add new job card to db
            string itemType;
            if(villaRadio.Checked == true){
                itemType = "villa";
            }
            else{
                itemType = "BDG";
            }
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }
            //insert to database
            
            MySqlCommand cmd;
            if (fileName != null && fileName != "error"){
                byte[] pdfData = System.IO.File.ReadAllBytes(fileName);
                cmd = new MySqlCommand("insert into job_card(order_date, delive_date, location, area, type, saleman, customer, item, drawing) values(@od, @dd, @loc, @a, @t, @sm, @cust, @i, @file_)", con);
                cmd.Parameters.AddWithValue("@od", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@dd", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@loc", emiratesComb.Text);
                cmd.Parameters.AddWithValue("@a", textBox9.Text);
                cmd.Parameters.AddWithValue("@t", itemType);
                cmd.Parameters.AddWithValue("@sm", comboBox2.Text);
                cmd.Parameters.AddWithValue("@cust", comboBox1.Text);
                cmd.Parameters.AddWithValue("@i", comboBox3.Text);
                cmd.Parameters.AddWithValue("@file_", pdfData);
                cmd.ExecuteNonQuery();
            }
            else{
                cmd = new MySqlCommand("insert into job_card(order_date, delive_date, location, area, type, saleman, customer, item) values(@od, @dd, @loc, @a, @t, @sm, @cust, @i)", con);
                cmd.Parameters.AddWithValue("@od", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@dd", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@loc", emiratesComb.Text);
                cmd.Parameters.AddWithValue("@a", textBox9.Text);
                cmd.Parameters.AddWithValue("@t", itemType);
                cmd.Parameters.AddWithValue("@sm", comboBox2.Text);
                cmd.Parameters.AddWithValue("@cust", comboBox1.Text);
                cmd.Parameters.AddWithValue("@i", comboBox3.Text);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            refresh_();
        }

        private void button1_Click(object sender, EventArgs e){
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
            MySqlCommand cmd = new MySqlCommand("insert into customer(name_customer, number_customer) values(@name, @number)", con);
            cmd.Parameters.AddWithValue("@name", customerName);
            cmd.Parameters.AddWithValue("@number", customerNumber);
            cmd.ExecuteNonQuery();
            con.Close();
            refresh_();
        }

        private void button7_Click(object sender, EventArgs e){
            comboBox2.SelectedIndex = -1;
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
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
            MySqlCommand cmd = new MySqlCommand("insert into saleman(name_saleman, phone_number_saleman) values(@name, @number)", con);
            cmd.Parameters.AddWithValue("@name", saleManeNamae);
            cmd.Parameters.AddWithValue("@number", saleManeNumber);
            cmd.ExecuteNonQuery();
            con.Close();
            refresh_();
        }

        private void button4_Click(object sender, EventArgs e){
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //insert to database
            MySqlCommand cmd = new MySqlCommand("insert into item(name_item, category_item, description_item, s_length, s_width, s_height, quantity_item, remark_item) values(@na, @ca, @de, @len, @wid, @hei, @qu, @re)", con);
            cmd.Parameters.AddWithValue("@na", comboBox3.Text);
            cmd.Parameters.AddWithValue("@ca", comboBox4.Text);
            cmd.Parameters.AddWithValue("@de", textBox4.Text);
            cmd.Parameters.AddWithValue("@len", int.Parse(i_length.Text));//int
            cmd.Parameters.AddWithValue("@wid", int.Parse(i_width.Text));//int
            cmd.Parameters.AddWithValue("@hei", int.Parse(i_height.Text));//int
            cmd.Parameters.AddWithValue("@qu", int.Parse(textBox6.Text));//int
            cmd.Parameters.AddWithValue("@re", textBox7.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            refresh_();
        }

        private void textBox7_TextChanged(object sender, EventArgs e){
            verIemContr();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            verIemContr();
        }

        private void i_height_TextChanged(object sender, EventArgs e)
        {
            verIemContr();
        }

        private void i_length_TextChanged(object sender, EventArgs e)
        {
            verIemContr();
        }

        private void i_width_TextChanged(object sender, EventArgs e)
        {

            verIemContr();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            verIemContr();
        }

        private void i_width_KeyPress(object sender, KeyPressEventArgs e){
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void i_length_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void i_height_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button8.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
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

        private void button5_Click(object sender, EventArgs e){//cancel button
            
        }
    }
}
