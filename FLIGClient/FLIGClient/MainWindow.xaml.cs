using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using FLIGServer;
using FLIGServer.Interfaces;
using System.ServiceModel.Description;
using System.Xml;



namespace FLIGClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var client = new FligClient(this.GetBinding(), new EndpointAddress("http://localhost:4554/ClientListener.svc"));
            var clientId = client.AddClient(new FLIGServer.Implementations.ClientData() { Name = "Matt", LastContact = new DateTime(), FailedConnectionAttempts = 0 });
            client.RemoveClient(clientId);
        }

        /// <summary>
        /// Gets the binding.
        /// </summary>
        /// <returns>The binding.</returns>
        private BasicHttpBinding GetBinding()
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Message)
            {
                Name = "basicHttpBindingIRISClient1",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                ReaderQuotas = { MaxStringContentLength = 2147483647, MaxDepth = 32, MaxBytesPerRead = 2147483647 }
            };
            binding.ReaderQuotas.MaxBytesPerRead = 4096;
            binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            binding.Security.Mode = BasicHttpSecurityMode.None;
            return binding;
        }
    }
}
