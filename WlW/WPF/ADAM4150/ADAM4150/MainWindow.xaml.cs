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
using DigitalLibrary;
using ComLibrary;

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

        public ComSettingModel comset;
        public DigitalLibrary.ADAM4150 adam;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getPort();




        }

        public void getPort()
        {
            //读取串口
            foreach (var item in SerialPort.GetPortNames())
            {

                this.Comlist.Items.Add(item);
            }



        }

        private void Btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (this.Comlist.SelectedIndex < 0)
            {
                MessageBox.Show("请选择串口");
                return;
            }

            comset = new ComSettingModel();

            comset.DigitalQuantityCom = Comlist.SelectedValue.ToString();

            adam = new DigitalLibrary.ADAM4150(comset);

            adam.SetData();

            lab_fire.Content = adam.DI5;
            lab_humanbody.Content = adam.DI2;
            lab_infrared.Content = adam.DI4;
            lab_smoke.Content = adam.DI3;
            
            

        }

        public bool ExecuteOnOff(ADAM4150FuncID ID)
        {
            

            comset = new ComSettingModel();

            comset.DigitalQuantityCom = Comlist.SelectedValue.ToString();

            adam = new DigitalLibrary.ADAM4150(comset);


            return adam.OnOff(ID);

        }

        private void Btn_lamp_Click(object sender, RoutedEventArgs e)
        {
            if (ExecuteOnOff(ADAM4150FuncID.OnDO1))
            {

                MessageBox.Show("打开成功");
                return;
            }
            MessageBox.Show("打开失败");
        }

        private void Btn_lamp_police_Click(object sender, RoutedEventArgs e)
        {
            if (ExecuteOnOff(ADAM4150FuncID.OnDO0))
            {

                MessageBox.Show("打开成功");
                return;
            }
            MessageBox.Show("打开失败");
        }

        private void Btn_wind_Click(object sender, RoutedEventArgs e)
        {
            if (ExecuteOnOff(ADAM4150FuncID.OnDO2))
            {

                MessageBox.Show("打开成功");
                return;
            }
            MessageBox.Show("打开失败");
        }
    }
}
