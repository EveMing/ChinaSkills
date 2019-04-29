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
using Camera.Net;
using IPCamera.Properties;





namespace IPCamera
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
        CameraCapture camera;
        private void PanleShow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Comlist.Items.Add("rtsp");
            this.Comlist.Items.Add("http");

        }
        private void Btn_Open_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.Comlist.SelectedItem.ToString() == "rtsp")
            {
                //string connStr = this.Comlist.SelectedItem.ToString() + "://" + this.txt_user.Text + ":" + this.txt_Passw.Text + "@" + this.txt_IP.Text + "/11";
                camera.Open(string.Format("rtsp://{0}:{1}@{2}/{3}", this.txt_user.Text, this.txt_Passw, this.txt_IP.Text, "11"));

                return;
            }
            else
            {
                MessageBox.Show("http");
                //string connStr = this.Comlist.SelectedItem.ToString() + "://" + this.txt_IP.Text + ":80/videostream.cgi?loginuse=" + this.txt_user.Text + "&loginpas=" + this.txt_Passw.Text;
                camera.Open(string.Format("htttp://{0}:{1}/videostream.cgi?loginuse={2}&loginpas={3}", this.txt_IP.Text, "80",this.txt_user.Text ,this.txt_Passw.Text));
                
       

            }
}
    }
}
