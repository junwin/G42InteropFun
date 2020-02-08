using System;
using System.Windows;
using SignalData;
using Tick42;
using Tick42.Windows;

namespace SignalExecutor
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISignalExecute
    {
        private Glue42 glue_;
        public MainWindow()
        {
            InitializeComponent();

            App.Glue.RegisterWindow(this, "SignalExecutor", GlueWindowType.Tab)
                .ContinueWith(r =>
                {
                    glue_ = App.Glue.Result;
                    GlueWindow = r.Result;
                });
        }

        public IGlueWindow GlueWindow { get; set; }

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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (glue_ == null)
            {
                MessageBox.Show("Glue not yet registered");
                return;
            }

            btnRegister.Tag = btnRegister.Tag != null ? (object) null : 5;
            // register the service, and explicitly specify the interface we're registering since
            // the Form has a lot of parent types
            if (btnRegister.Tag != null)
            {
                glue_.Interop.RegisterService<ISignalExecute>(this);
                btnRegister.Content = "Unregister";
            }
            else
            {
                glue_.Interop.UnregisterService<ISignalExecute>(this);
                btnRegister.Content = "Register";
            }
        }

        public ExecutorSignalState GetState()
        {
            return new ExecutorSignalState();
        }
    }

    public class ExecutorSignalState
    {
    }
}