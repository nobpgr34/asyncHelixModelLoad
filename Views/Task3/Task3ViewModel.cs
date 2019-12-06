using System;
using System.Windows;
using System.Windows.Media.Media3D;
namespace HelixProject.Views
{
    class Task3ViewModel : TaskViewModel
    {
        public new string Name
        {
            get
            {
                return "Task3";
            }
        }

        public Task3ViewModel(ApplicationViewModel app) : base(app)
        {
            workWithBoundingBox += stopMovement;
        }

        private void stopMovement()
        {
            try
            {
                if (aTimer.Enabled)
                {
                    aTimer.Enabled = false;
                }
                app.Transform = new MatrixTransform3D();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString());
                MessageBox.Show("ошибка приложения");
            }
        }
    }
}
