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

namespace Ruby_Mattress_Management_System
{
    public partial class Form1 : Form {
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection("server=localhost; database=productionmaster; uid=root");
        public Form1()
        {
            InitializeComponent();
        }
        private void emptContr(){
            textBox1.Text = string.Empty;
            textBox4.Text = string.Empty;
            i_length.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            //
            emiratesComb.SelectedItem = null;
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            //
            emiratesComb.Text = string.Empty;
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;

        }
        private void Form1_Load(object sender, EventArgs e){
            //Fill Emirates Combo
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
            //open Connection
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

            //Fill Customer Combo
            var cmd2 = new MySqlCommand("select name_customer from customer", con);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            while(dr2.Read()){
                comboBox1.Items.Add(dr2.GetString(0));
            }
            dr2.Close();

            //Fill Item Combo
            var cmd3 = new MySqlCommand("select name_item from item", con);
            MySqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                comboBox3.Items.Add(dr3.GetString(0));
            }
            dr3.Close();
            con.Close();
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

        private void button9_Click(object sender, EventArgs e){
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK){
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }

       

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e){
            
            if(checkBox1.Checked == true){
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                //
                textBox8.Enabled = false;
                textBox1.Enabled = false;
                label2.Enabled = false;
                label12.Enabled = false;
            }
            else {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                //
                textBox8.Enabled = true;
                textBox1.Enabled = true;
                label2.Enabled = true;
                label12.Enabled = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            emptContr();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            emptContr();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e){
            comboBox1.SelectedIndex = -1;
            try{
                con.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

            string customerNamae, customerNumber, msg1, title1, msg2, title2;

            msg1 = "Enter a customer name: ";
            msg2 = "Enter a customer number: ";

            title1 = "Customer Name";
            title2 = "Customer number";

            customerNamae = Interaction.InputBox(msg1, title1);
            customerNumber = Interaction.InputBox(msg2, title2);
            //insert to database
            MySqlCommand cmd = new MySqlCommand("insert into customer(name_customer, number_customer) values(@name, @number)", con);
            cmd.Parameters.AddWithValue("@name", customerNamae);
            cmd.Parameters.AddWithValue("@number", customerNamae);
            cmd.ExecuteNonQuery();
            //refresh combobox
            var cmd1 = new MySqlCommand("select name_saleman from saleman", con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1.GetString(0));
            }
            dr1.Close();
            
            //empty and refresh
            comboBox1.Items.Clear();
            var cmd2 = new MySqlCommand("select name_customer from customer", con);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox1.Items.Add(dr2.GetString(0));
            }
            dr2.Close();
            con.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            try
            {
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
 
            //empty and refresh
            comboBox2.Items.Clear();
            var cmd2 = new MySqlCommand("select name_saleman from saleman", con);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox2.Items.Add(dr2.GetString(0));
            }
            dr2.Close();
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e){
            try
            {
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

            //empty and refresh
            comboBox3.Items.Clear();
            MySqlCommand cmd2 = new MySqlCommand("select name_item from item", con);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox3.Items.Add(dr2.GetString(0));
            }
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            textBox4.Text = "";
            i_length.Text = "";
            i_width.Text = "";
            i_height.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            //fill others
            MySqlCommand cmd3 = new MySqlCommand("insert into", con);
            con.Close();
            //Form1_Load();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
