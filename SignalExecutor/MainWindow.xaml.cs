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
using SignalData;
using Tick42;
using Tick42.StartingContext;

using Tick42.Contexts;
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
