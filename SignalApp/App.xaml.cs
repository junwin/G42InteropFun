using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tick42;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tick42;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using SignalData;
using Tick42;

namespace SignalApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Glue42 Glue;
        public static ISignalExecute Service;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            //Glue = new Glue42();
            //Glue.Initialize("SignalApp");

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
                useGlueWindows: true,
                credentials: Tuple.Create(glueUser, ""),
                advancedOptions: advancedOptions);

            Service = Glue.Interop.CreateServiceProxy<ISignalExecute>();
            // let's track the status of the service target - if anybody has implemented it.

            Glue.Interop.CreateServiceSubscription(Service,
                (_, status) => Console.WriteLine($"{nameof(ISignalExecute)} is now {(status ? string.Empty : "in")}active"));

            Console.WriteLine("Initialized Glue Interop");
        }
    }
}
