using Newtonsoft.Json;
using SignalData;
using System.Collections.Generic;
using System.Windows;

namespace SignalApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSendSignal_Click(object sender, RoutedEventArgs e)
        {
            // Get the sinals from test data
            string text = System.IO.File.ReadAllText(@"TestSignals.json");

            // Deserialize JSON
            var signals = JsonConvert.DeserializeObject<List<TradeSignal>>(text);
        }

        //  We need some sort of interoperation  to execute the signal
    }
}