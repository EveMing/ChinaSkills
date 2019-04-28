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
using MWRDemoDll;

namespace HighFrequencyReadCard
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
        ResultMessage res;
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //连接读写器
            res =MifareRFEYE.Instance.ConnDevice();
            if (res.Result == Result.Failure) {
                MessageBox.Show("连接失败");
                return;
            }
            this.txt_show.Content = "连接成功";
            this.listbox.Items.Add("连接成功");


            //加载扇形区
            for (int i = 1;i<13;i++) {
                this.comlist.Items.Add("Data"+i);
            }

        }

        private CardDataKind SelecteCard() {
            string value = this.comlist.SelectedItem.ToString();
            //卡的13个扇形区
            switch (value) {
                case "Data1":  return CardDataKind.Data1;
                case "Data2":  return CardDataKind.Data2;
                case "Data3":  return CardDataKind.Data3;
                case "Data4":  return CardDataKind.Data4;
                case "Data5":  return CardDataKind.Data5;
                case "Data6":  return CardDataKind.Data6;
                case "Data7":  return CardDataKind.Data7;
                case "Data8":  return CardDataKind.Data8;
                case "Data9":  return CardDataKind.Data9;
                case "Data10": return CardDataKind.Data10;
                case "Data11": return CardDataKind.Data11;
                case "Data12": return CardDataKind.Data12;
                case "Data13": return CardDataKind.Data13;
            }
            //默认扇形1
            return CardDataKind.Data1;

        }

        private void Btn_yanzhen_Click(object sender, RoutedEventArgs e)
        {
            //验证卡密码
            res = MifareRFEYE.Instance.AuthCardPwd("",SelecteCard());
            if (res.Result == Result.Failure)
            {
                MessageBox.Show("验证卡密码失败");
                return;
            }
            this.listbox.Items.Add("验证卡密码成功");
        }


        //读卡
        private void Btn_read_Click(object sender, RoutedEventArgs e)
        {
            this.listbox.Items.Add(MifareRFEYE.Instance.ReadString(SelecteCard(), 0, null));
        }


        //寻卡
        private void Btn_search_Click(object sender, RoutedEventArgs e)
        {
            
            res = MifareRFEYE.Instance.Search();
            if (res.Result == Result.Failure)
            {
                MessageBox.Show("寻卡失败");
                return;
            }
            this.listbox.Items.Add("寻卡成功");

        }


        //写卡
        private void Btn__Click(object sender, RoutedEventArgs e)
        {
            int i = MifareRFEYE.Instance.WriteString(SelecteCard(), this.txt_input.Text);
            
            if (i>0)
            {
                MessageBox.Show("写入失败");
                return;
            }
            this.listbox.Items.Add("写入成功");
        }


        //关闭
        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            res = MifareRFEYE.Instance.CloseDevice();

            if (res.Result == Result.Failure)
            {
                MessageBox.Show("设备无法断开");
                return;
            }
            this.listbox.Items.Add("设备关闭");
        }
    }
}
