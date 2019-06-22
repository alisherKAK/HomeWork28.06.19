using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace HomeWork28_06_19
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string GetHostNameByIp(string ip)
        {
            IPAddress ipAddress;

            if(IPAddress.TryParse(ip, out ipAddress))
            {
                try
                {
                    return Dns.GetHostEntry(ipAddress).HostName;
                }
                catch (ArgumentNullException)
                {
                    return string.Empty;
                }
                catch (ArgumentException)
                {
                    return string.Empty;
                }
                catch (SocketException)
                {
                    return string.Empty;
                }
            }

            return "Incorrect IP or this ip hasn't host name";
        }

        private List<IPAddress> GetIpAddressByHostName(string hostName)
        {
            try
            {
                return Dns.GetHostEntry(hostName).AddressList.ToList();
            }
            catch(SocketException)
            {
                return new List<IPAddress>();
            }
            catch(ArgumentOutOfRangeException)
            {
                return new List<IPAddress>();
            }
        }

        private void GetHostNameButtonClick(object sender, RoutedEventArgs e)
        {
            hostNameTextBlock.Text = GetHostNameByIp(ipAddressTextBox.Text);
        }

        private void GetIpButtonClick(object sender, RoutedEventArgs e)
        {
            ipAddressTextBlock.Text = string.Empty;

            foreach(IPAddress ipAddress in GetIpAddressByHostName(urlAddressTextBox.Text))
            {
                ipAddressTextBlock.Text += ipAddress.ToString() + "\n";
            }
        }
    }
}
