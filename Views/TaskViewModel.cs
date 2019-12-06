using HelixProject.HelperClasses;
using HelixToolkit.Wpf;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace HelixProject.Views
{
    class TaskViewModel : ObservableObject, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Task";
            }
        }


        protected static Timer aTimer;
        protected ApplicationViewModel app;
        protected Action loadAction;
        protected Action clearAction;
        protected Model3DGroup MyModel;

        public Action workWithBoundingBox { get; set; }
        public NotificationClass<bool> value { get; set; }

        public TaskViewModel(ApplicationViewModel app)
        {
            this.app = app;
            workWithBoundingBox = clearBoxAndLines;
            value=new NotificationClass<bool>(false);
        }

        private ICommand loadModel;

        public ICommand LoadModel
        {
            get
            {
                if (loadModel == null)
                {
                    loadModel = new RelayCommand(
                        param => Load3DModel(),
                       null
                    );
                }
                return loadModel;
            }
        }

        private ICommand clearModel;

        public ICommand ClearModel
        {
            get
            {
                if (clearModel == null)
                {
                    clearModel = new RelayCommand(
                        param => Clear3DModel(),
                       null
                    );
                }
                return clearModel;
            }
        }

        private void Clear3DModel()
        {
            try
            {
                app.Model = null;
                if (aTimer.Enabled)
                { aTimer.Enabled = false; }
                if (clearAction != null)
                {
                    clearAction();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString());
                MessageBox.Show("ошибка приложения");
            }
        }

        protected void clearBoxAndLines()
        {
            try
            {
                app.Rect = new Rect3D();
                app.LinePoints = new Point3DCollection();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString());
                MessageBox.Show("ошибка приложения");
            }
        }

        private async Task<Model3DGroup> LoadAsync(string model3DPath, bool freeze)
        {
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    var mi = new ModelImporter();
                    return mi.Load(model3DPath, null, true);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.ToString());
                    MessageBox.Show("ошибка приложения");
                }
                return null;
            });
        }

        private async void Load3DModel()
        {
            try
            {
                string filename = null;
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "obj Files|*.obj";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    filename = dlg.FileName;
                }
                if (filename == null)
                {
                    MessageBox.Show("Выберите файл");
                    return;
                }
                MyModel = await LoadAsync(filename, true);
                if (MyModel == null)
                {
                    MessageBox.Show("Попробуйте снова");
                    return;
                }
                app.Model = MyModel;
                app.Transform = new MatrixTransform3D();
                if (loadAction != null)
                {
                    loadAction();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString());
                MessageBox.Show("ошибка приложения");
            }
        }
    }
}
