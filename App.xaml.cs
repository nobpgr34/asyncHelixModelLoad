using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HelixProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
            : base()
        {
            Logger.InitLogger();
            AppDomain.CurrentDomain.UnhandledException += Current_DispatcherUnhandledException;
        }

        void Current_DispatcherUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
            {
                MessageBox.Show("Ошибка приложения. Приложение будет закрыто");
                var exp = (Exception)e.ExceptionObject;
                Logger.Log.Fatal(exp.Source);
                Logger.Log.Fatal(exp.Message);
                Logger.Log.Fatal(exp.StackTrace);
            }
            Process.GetCurrentProcess().Kill();
        }

    }

}
