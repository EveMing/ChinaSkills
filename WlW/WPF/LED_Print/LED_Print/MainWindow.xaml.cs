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
//
using System.IO.Ports;
using LEDLibrary;


namespace LED_Print
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

        private LEDPlayer player;

        private void ComList1_Loaded(object sender, RoutedEventArgs e)
        {
            //获取串口号
            foreach (var item in SerialPort.GetPortNames())
            {

                this.ComList1.Items.Add(item);
            }
        }

        private void Btn_start1_Click(object sender, RoutedEventArgs e)
        {
            if (ComList1.SelectedValue == null)
            {

                MessageBox.Show("请选择串口");
                return;
            }
            //发送LED字符串
            player = new LEDPlayer(this.ComList1.SelectedItem.ToString());
            MessageBox.Show(player.DisplayText(this.txt_input1.Text));

        }
    }
}
