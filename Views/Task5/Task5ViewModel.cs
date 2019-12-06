using HelixProject.HelperClasses;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace HelixProject.Views
{
    class Task5ViewModel : TaskViewModel
    {
        public new string Name
        {
            get
            {
                return "Task5";
            }
        }

        Dispatcher dispatcher;
        bool direction;
        public NotificationClass<int> minZ { get; set; }
        public NotificationClass<int> maxZ { get; set; }

        public Task5ViewModel(ApplicationViewModel app)
            : base(app)
        {
            dispatcher = App.Current.Dispatcher;
            aTimer = new Timer(500) { Interval = 20 };
            aTimer.Elapsed += new ElapsedEventHandler(MoveAroundZOnTimer);
            minZ = new NotificationClass<int>(-5);
            maxZ = new NotificationClass<int>(5);
            loadAction = notify;
        }

        private ICommand start;

        public ICommand Start
        {
            get
            {
                if (start == null)
                {
                    start = new RelayCommand(
                        param => { aTimer.Enabled = true; },
                        param => { return app.Model != null; }
                    );
                }
                return start;
            }
        }

        private ICommand stop;

        public ICommand Stop
        {
            get
            {
                if (stop == null)
                {
                    stop = new RelayCommand(
                        param => { aTimer.Enabled = false; },
                        param => {return app.Model != null; } 
                    );
                }
                return stop;
            }
        }

        private void MoveAroundZOnTimer(object sender, ElapsedEventArgs e)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    var matrix = moveModel(app.Transform.Value);
                    app.Transform = new MatrixTransform3D(matrix);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.ToString());
                    MessageBox.Show("ошибка приложения");
                }
            }), DispatcherPriority.Render
            );
        }


        private Matrix3D moveModel(Matrix3D m)
        {
            try
            {
                if (m.OffsetZ > maxZ.Notification)
                {
                    direction = false;
                }

                else if (m.OffsetZ <= minZ.Notification)
                {
                    direction = true;

                }
                if (direction)
                {
                    m.OffsetZ++;
                }
                else
                {
                    m.OffsetZ--;
                }
                return m;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString());
                MessageBox.Show("ошибка приложения");
            }
            return new Matrix3D();
        }


        /// <summary>
        /// if needed matrix calculations can be done asynchronously
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void MoveAroundZOnTimerAsync(object sender, ElapsedEventArgs e)
        {
            Matrix3D m = new Matrix3D();
            await dispatcher.BeginInvoke(new Action(() =>
             {
                 try
                 {
                     m = app.Transform.Value;
                 }
                 catch (Exception ex)
                 {
                     Logger.Log.Error(ex.ToString());
                     MessageBox.Show("ошибка приложения");
                 }
             }), DispatcherPriority.Render
            );
            m = await moveModelAsync(m);
            await dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    var matrix = moveModel(app.Transform.Value);
                    app.Transform = new MatrixTransform3D(matrix);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.ToString());
                    MessageBox.Show("ошибка приложения");
                }
            }), DispatcherPriority.Render
            );
        }

        private async Task<Matrix3D> moveModelAsync(Matrix3D m)
        {
            return await Task.Factory.StartNew(() =>
            {
                m = moveModel(m);
                return m;
            });
        }

        private void notify()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
