using CustomProject.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CustomProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;

        }

        protected override void OnExit(ExitEventArgs e)
        {
            SimpleCompiler.CloseAllProcesses();
            base.OnExit(e);
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogProvider.Instance().AddError("Unhandled exception occurred: \n" + e.Exception.Message);
            LogProvider.Instance().SaveLogs("Critical Error.txt");
        }


    }
}
