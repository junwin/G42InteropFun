using System;
using System.Collections.Generic;
using System.Linq;
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
using SignalData;
using Tick42;
using Tick42.StartingContext;

using Tick42.Contexts;
using Tick42.Windows;
using Newtonsoft.Json;
using System.Reflection;
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
using Newtonsoft.Json;
using System.Reflection;


namespace SignalExecutor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISignalExecute
    {
        public IGlueWindow GlueWindow { get; set; }
        public MainWindow()
        {
            /*
            var gwOptions = App.Glue.GlueWindows?.GetStartupOptions() ?? new GlueWindowOptions();
            wOptions.WithType(GlueWindowType.Tab);
            gwOptions.WithTitle("SignalExecutor");

            // register the window and save the result
            App.Glue.GlueWindows?.RegisterWindow(this, gwOptions)?.ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    GlueWindow = t.Result;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

    */
            InitializeComponent();

            SynchronizationContext synchronizationContext = SynchronizationContext.Current;

            Task.Run(() =>
            {
                try
                {
                    InitializeGlue(synchronizationContext);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Failed initializing Glue");
                    Console.WriteLine(exception.ToString());
                }
            });
        }

        public Glue42 Glue { get; private set; }
        private ISignalExecute service_;
        private void InitializeGlue(SynchronizationContext synchronizationContext)
        {
            // initialize Tick42 Interop (AGM) and Metrics components

            //Log("Initializing Glue42");

            string glueUser = Environment.UserName;
            //Log($"User is {glueUser}");

            // these envvars are expanded in some configuration files
            //Environment.SetEnvironmentVariable("PROCESSID", Process.GetCurrentProcess().Id + "");
            // Environment.SetEnvironmentVariable("GW_USERNAME", glueUser);
            //Environment.SetEnvironmentVariable("DEMO_MODE", Mode + "");

            // The Glue42 facade provides a simplified programming
            // interface over the core Glue42 components.
            Glue = new Glue42();

            Glue.Interop.ConnectionStatusChanged += (sender, args) => { Console.WriteLine($"Glue connection is now {args.Status}"); };
            Glue.Interop.TargetStatusChanged += (sender, args) => Console.WriteLine($"{args.Status.State} target {args.Target}");

            var advancedOptions = new Glue42.AdvancedOptions { SynchronizationContext = synchronizationContext };

            Glue.Initialize(
                Assembly.GetEntryAssembly().GetName().Name, // application name - required
                useAgm: true,
                useAppManager: true,
                useMetrics: true,
                useContexts: false,
                useGlueWindows: false,
                credentials: Tuple.Create(glueUser, ""),
                advancedOptions: advancedOptions);

            service_ = Glue.Interop.CreateServiceProxy<ISignalExecute>();
            // let's track the status of the service target - if anybody has implemented it.

            Glue.Interop.CreateServiceSubscription(service_,
                (_, status) => Console.WriteLine($"{nameof(ISignalExecute)} is now {(status ? string.Empty : "in")}active"));

            Console.WriteLine("Initialized Glue Interop");
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            btnRegister.Tag = btnRegister.Tag != null ? (object)null : 5;
            // register the service, and explicitly specify the interface we're registering since
            // the Form has a lot of parent types
            if (btnRegister.Tag != null)
            {
                Glue.Interop.RegisterService<ISignalExecute>(this);
                btnRegister.Content = "Unregister";
            }
            else
            {
                Glue.Interop.UnregisterService<ISignalExecute>(this);
                btnRegister.Content = "Register";
            }
        }

        void ISignalExecute.ExecuteSignal(TradeSignal input)
        {
            //throw new NotImplementedException();
        }

        bool ISignalExecute.ExecuteSignal2(TradeSignal input, out TradeSignal updated)
        {
            updated = input;
            return true;
        }

        void IDisposable.Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
