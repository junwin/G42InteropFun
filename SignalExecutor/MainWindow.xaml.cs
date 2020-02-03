using System.Threading.Tasks;
using System.Windows;
using Tick42.Windows;

namespace SignalExecutor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IGlueWindow GlueWindow { get; set; }

        public MainWindow()
        {
            var gwOptions = App.Glue.GlueWindows?.GetStartupOptions() ?? new GlueWindowOptions();
            gwOptions.WithType(GlueWindowType.Tab);
            gwOptions.WithTitle("SignalExecutor");

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
    }
}