using SignalData;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DOT.Core.Extensions;
using Tick42;
using Tick42.StartingContext;

namespace SignalExecutor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Task<Glue42> Glue;
        public static ISignalExecute Service;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Glue = new Glue42();
            //Glue.Initialize("SignalExecutor");

            SynchronizationContext synchronizationContext = SynchronizationContext.Current;
            InitializeGlue(synchronizationContext);
        }

        private void InitializeGlue(SynchronizationContext synchronizationContext)
        {
            // initialize Tick42 Interop (AGM) and Metrics components

            Console.WriteLine("Initializing Glue42");

            string glueUser = Environment.UserName;
            Console.WriteLine($"User is {glueUser}");

            // these envvars are expanded in some configuration files
            Environment.SetEnvironmentVariable("PROCESSID", Process.GetCurrentProcess().Id + "");
            Environment.SetEnvironmentVariable("GW_USERNAME", glueUser);
            //Environment.SetEnvironmentVariable("DEMO_MODE", Mode + "");

            // The Glue42 facade provides a simplified programming
            // interface over the core Glue42 components.

            var initializeOptions = new InitializeOptions();
            initializeOptions.SetSaveRestoreStateEndpoint(value =>
            {
                var tcs = new TaskCompletionSource<ExecutorSignalState>();
                Dispatcher.BeginInvoke((Action) (() => tcs.TrySetResult((MainWindow as MainWindow).GetState())));
                return tcs.Task;
            });

            Glue = Glue42.InitializeGlue(initializeOptions).ContinueWith(glue =>
            {
                // The Glue42 facade provides a simplified programming
                // interface over the core Glue42 components.
                var g = glue.Result;
                g.Interop.ConnectionStatusChanged += (sender, args) =>
                {
                    Console.WriteLine($"Glue connection is now {args.Status}");
                };
                g.Interop.TargetStatusChanged += (sender, args) =>
                    Console.WriteLine($"{args.Status.State} target {args.Target}");

                var advancedOptions = new Glue42.AdvancedOptions { SynchronizationContext = synchronizationContext };

                Service = g.Interop.CreateServiceProxy<ISignalExecute>();
                // let's track the status of the service target - if anybody has implemented it.

                g.Interop.CreateServiceSubscription(Service,
                    (_, status) => Console.WriteLine($"{nameof(ISignalExecute)} is now {(status ? string.Empty : "in")}active"));

                Console.WriteLine("Initialized Glue Interop");

                return g;
            });
        }
    }
}