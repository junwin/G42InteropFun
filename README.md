# G42InteropFun
Projects to try out Glue42 interop/linking and UI Integration.


Assume that we have two existing WPF .net applications, one that checks out the markets, adds some magic and then raises a signal to make some trade.

The second is an app that can execute trades/orders in the different stock markets.

The problem we want to solve is that to date these are separate applications that take up space on the users desktop and worse the user needs some manual process to get the signal from the first application to the second.

You can download the code here: https://github.com/junwin/G42InteropFun


1 Pre Reqs

Before you start you need to install Glue42, you can get a trial version from their website here: https://glue42.com/free-trial/

1.1 Glue42 NuGet
You need to add the Glue42 NuGet package to the project. A simple way to do this from the NuGet package manager off the Tools menu in VS.

Alternatively, you can add a reference to the .net lib from here:
%localappdata%\tick42\gluesdk\Glue42Net\


2) Create and initialize an instance of Glue42 in your application.

I am working with WPF apps hence to do this add the following code into App class in  App.xaml.cs file:


namespace SignalApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Glue42 Glue;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Glue = new Glue42();
            Glue.Initialize("SignalExecutor");
        }
    }
}

This will initialize Glue42 with the default features; you will see a wider range of options later on


3) UI Integration
To add basic UI integration to some window in your app is easy, in WPF you can add the following code to your window's constructor.



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

You can now run up the app and dock it to the samples provided by tick 42.

Add similar code to the SignalExecutor project to enable them both to work together.



4) Interop

Our SignalApp uses a data object called TradeSignal and what we need to do is send a request passing the data to the executor
