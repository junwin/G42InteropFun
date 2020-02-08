using Newtonsoft.Json;
using SignalData;
using System.Collections.Generic;
using System.Windows;
using Tick42;
using Tick42.Windows;

namespace SignalApp
{
    public partial class MainWindow : Window
    {
        public IGlueWindow GlueWindow { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            App.Glue.RegisterWindow(this, "SignalApp", GlueWindowType.Tab)
                .ContinueWith(r => { GlueWindow = r.Result; });
        }

        private void btnSendSignal_Click(object sender, RoutedEventArgs e)
        {
            // Get the sinals from test data
            string text = System.IO.File.ReadAllText(@"TestSignals.json");

            // Deserialize JSON
            var signals = JsonConvert.DeserializeObject<List<TradeSignal>>(text);
            App.Service?.ExecuteSignal2(signals[0], out TradeSignal ts);
        }

        //  We need some sort of interoperation  to execute the signal
        
        public SignalState GetState()
        {
            return new SignalState();
        }
    }

    public class SignalState
    {
    }
}