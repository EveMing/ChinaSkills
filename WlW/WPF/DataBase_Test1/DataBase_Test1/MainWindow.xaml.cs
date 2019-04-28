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
        public Dictionary<int, string> dicDB;


        //查询 
        private void button_Click(object sender, RoutedEventArgs e)
        {
            dbConnect(0, "SELECT name from users");
            //foreach(var data in)
        }


        public Dictionary<int, string> dbConnect(int Type, string str)
        {

            string strConn = "data source=127.0.0.1;initial catalog=Test;user id=sa;password=123456;";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    

                    switch (Type) {

                        //查询
                        case 0 :

                            SqlCommand cmd = new SqlCommand(str, conn);
                            //SqlDataAdapter sqldata = new SqlDataAdapter(cmd);

                            //object sele = cmd.ExecuteScalar();

                            //conn.Close();

                            dicDB = new Dictionary<int, string>();

                            SqlDataReader re = cmd.ExecuteReader();

                            while (re.Read()) {

                                this.listBox.Items.Add(re["name"]);

                            }

                            conn.Close();

                            return dicDB;

 
                        //添加     
                        case 1:
                            using (SqlCommand comm=conn.CreateCommand()) {
                                string sqlStr = "insert into users(name) values(@name)";
                                comm.CommandText = sqlStr;
                                comm.Parameters.AddWithValue("@name",this.txt_insert.Text);
                                if (comm.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show("插入成功");

                                }
                                else {
                                    MessageBox.Show("插入失败");

                                };

                            }
                                break;



                        //删除
                        case 2:

                            break;


                    }
                }
                catch (SqlException s)
                {

                    Console.Write(s);

                }
                return dicDB;

            }
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            dbConnect(1, "");
        }
    }

}
