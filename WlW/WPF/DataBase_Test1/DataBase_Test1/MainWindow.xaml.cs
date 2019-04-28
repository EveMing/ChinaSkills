using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace DataBase_Test1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        //查询 
        private void button_Click(object sender, RoutedEventArgs e)
        {

            this.listBox.Items.Clear();
            dbConnect(0, "SELECT name from users");
            //foreach(var data in)
        }


        

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            dbConnect(1, "");
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            
            dbConnect(2, "delete from users where name=@name");
            this.listBox.Items.Clear();
            dbConnect(0, "SELECT name from users");
        }


        public void dbConnect(int Type, string str)
        {

            string strConn = "data source=192.168.100.40;initial catalog=test;user id=sa;password=qwer1234.;";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();


                    switch (Type)
                    {

                        //查询
                        case 0:

                            SqlCommand cmd = new SqlCommand(str, conn);

                            //执行返回第一行第一列
                            //object sele = cmd.ExecuteScalar();

                            SqlDataReader re = cmd.ExecuteReader();

                            while (re.Read())
                            {

                                this.listBox.Items.Add(re["name"]);

                            }

                            conn.Close();

                            return;


                        //添加     
                        case 1:
                            using (SqlCommand comm = conn.CreateCommand())
                            {
                                string sqlStr = "insert into users(name) values(@name)";
                                comm.CommandText = sqlStr;
                                comm.Parameters.AddWithValue("@name", this.txt_insert.Text);
                                if (comm.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show("插入成功");
                                    this.txt_insert.Text = null;
                                }
                                else
                                {
                                    MessageBox.Show("插入失败");

                                };

                            }
                            conn.Close();
                            break;



                        //删除
                        case 2:
                            using (SqlCommand com = conn.CreateCommand())
                            {
                                //string sql = "delete from users where name=@name";
                                com.CommandText = str;
                                com.Parameters.AddWithValue("@name", this.listBox.SelectedItem.ToString());
                                if (com.ExecuteNonQuery() > 0)
                                {

                                    MessageBox.Show("删除成功");
                                }

                            }
                            break;


                    }
                }
                catch (SqlException s)
                {

                    Console.Write(s);

                }
                return;

            }
        }
    }

}
