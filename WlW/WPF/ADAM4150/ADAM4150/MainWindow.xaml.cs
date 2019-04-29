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
using System.IO.Ports;

namespace ADAM4150
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getPort();


        }

        public void getPort() {
            //读取串口
            foreach (var item in SerialPort.GetPortNames())
            {

                this.Comlist.Items.Add(item);
            }

            if (this.Comlist.SelectedIndex>0) {

                MessageBox.Show("请选择串口");

            }

        }

    }
}
