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
using DigitalLibrary;
using System.Windows.Threading;

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
        public void getValue(object sender, EventArgs e)
        {
            //Console.Write("log:"+e.ToString());
            if (this.Comlist.SelectedIndex < 0)
            {
                MessageBox.Show("请选择串口");
                return;
            }

            comset = new ComSettingModel();

            comset.DigitalQuantityCom = Comlist.SelectedValue.ToString();

            adam = new DigitalLibrary.ADAM4150(comset);

            adam.SetData();

            lab_fire.Content = adam.DI1 ? "有火灾" : "一切正常";
            lab_humanbody.Content = adam.DI0 ? "有人" : "一切正常";
            lab_infrared.Content = adam.DI4 ? "有人入侵" : "一切正常";
            lab_smoke.Content = adam.DI2 ? "有烟雾" : "一切正常";

        }

        private void Btn_start_Click(object sender, RoutedEventArgs e)
        {

            DispatcherTimer dis = new DispatcherTimer();
            dis.Tick += new EventHandler(getValue);
            dis.Interval = TimeSpan.FromMilliseconds(1000);
            dis.Start();


        }



        public bool ExecuteOnOff(ADAM4150FuncID ID)
        {
            if (this.Comlist.SelectedIndex < 0)
            {
                MessageBox.Show("请选择串口");
                return false;
            }
            comset = new ComSettingModel();

            comset.DigitalQuantityCom = Comlist.SelectedValue.ToString();

            adam = new DigitalLibrary.ADAM4150(comset);

            return adam.OnOff(ID);

        }

        //灯泡
        private void Btn_lamp_Click(object sender, RoutedEventArgs e)
        {

            if (this.btn_lamp.Content.ToString() == "打开" && ExecuteOnOff(ADAM4150FuncID.OnDO1))
            {

                this.btn_lamp.Content = "关闭";
                return;
            }
            else if (this.btn_lamp.Content.ToString() != "打开")
            {
                ExecuteOnOff(ADAM4150FuncID.OffDO1);
                this.btn_lamp.Content = "打开";
                return;
            }
            MessageBox.Show("打开失败");
        }

        //报警灯
        private void Btn_lamp_police_Click(object sender, RoutedEventArgs e)
        {
            if (this.btn_lamp_police.Content.ToString() == "打开" && ExecuteOnOff(ADAM4150FuncID.OnDO0))
            {


                this.btn_lamp_police.Content = "关闭";
                return;
            }
            else if (this.btn_lamp_police.Content.ToString() != "打开")
            {
                ExecuteOnOff(ADAM4150FuncID.OffDO0);
                this.btn_lamp_police.Content = "打开";
                return;
            }
            MessageBox.Show("打开失败");

        }

        //风扇
        private void Btn_wind_Click(object sender, RoutedEventArgs e)
        {
            if (this.btn_wind.Content.ToString() == "打开" && ExecuteOnOff(ADAM4150FuncID.OnDO2))
            {

                this.btn_wind.Content = "关闭";
                return;
            }
            if (this.btn_wind.Content.ToString() != "打开")
            {
                ExecuteOnOff(ADAM4150FuncID.OffDO2);
                this.btn_wind.Content = "打开";
                return;
            }
            MessageBox.Show("打开失败");

        }
    }
}
