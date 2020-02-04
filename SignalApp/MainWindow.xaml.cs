using Newtonsoft.Json;
using SignalData;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Tick42.Windows;

namespace SignalApp
{
    public partial class MainWindow : Window
    {
        public IGlueWindow GlueWindow { get; set; }

        public MainWindow()
        {
            var gwOptions = App.Glue.GlueWindows?.GetStartupOptions() ?? new GlueWindowOptions();
            gwOptions.WithType(GlueWindowType.Tab);
            gwOptions.WithTitle("SignalApp");

            // register the window and save the result
            App.Glue.GlueWindows?.RegisterWindow(this, gwOptions)?.ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    GlueWindow = t.Result;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            InitializeComponent();
        }

        private void btnSendSignal_Click(object sender, RoutedEventArgs e)
        {
            // Get the sinals from test data
            string text = System.IO.File.ReadAllText(@"TestSignals.json");

            // Deserialize JSON
            var signals = JsonConvert.DeserializeObject<List<TradeSignal>>(text);
            TradeSignal ts;
            service_.ExecuteSignal2(signals[0], out ts);
        }

        //  We need some sort of interoperation  to execute the signal
    }
}