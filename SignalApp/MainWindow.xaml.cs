using Newtonsoft.Json;
using SignalData;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using DOT.AGM;
using DOT.Core.Extensions;
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
            App.Glue.ContinueWith(g =>
            {
                var glue = g.Result;

                var state = glue.GetRestoreState<SignalState>();
                
                var gwOptions = glue.GlueWindows?.GetStartupOptions() ?? new GlueWindowOptions();
                gwOptions.WithType(GlueWindowType.Tab);
                gwOptions.WithTitle("SignalApp");

                // register the window and save the result
                glue.GlueWindows?.RegisterWindow(this, gwOptions)?.ContinueWith(t =>
                {
                    if (t.IsCompleted)
                    {
                        GlueWindow = t.Result;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
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