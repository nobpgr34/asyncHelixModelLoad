using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace HelixProject.Views
{
    class Task4ViewModel : TaskViewModel
    {
        public new string Name
        {
            get
            {
                return "Task4";
            }
        }

        public Task4ViewModel(ApplicationViewModel app) : base(app)
        {
            loadAction = setBoundingBox;
            clearAction = clearBoxAndLines;
            workWithBoundingBox += setBoundingBox;
        }

        void setBoundingBox()
        {
            try
            {
                aTimer.Enabled = false;
                app.Transform = new MatrixTransform3D();
                app.Rect = new Rect3D();
                app.LinePoints = new Point3DCollection();
                if (app.Model != null)
                {
                    var bounds = app.Model.Bounds;
                    app.Rect = bounds;
                    var center = new Point3D((bounds.X + bounds.SizeX / 2), (bounds.Y + bounds.SizeY / 2), (bounds.Z + bounds.SizeZ / 2));
                    app.LinePoints.Add(new Point3D(-50, center.Y, center.Z));
                    app.LinePoints.Add(new Point3D(50, center.Y, center.Z));
                    app.LinePoints.Add(new Point3D(center.X, -50, center.Z));
                    app.LinePoints.Add(new Point3D(center.X, 50, center.Z));
                    app.LinePoints.Add(new Point3D(center.X, center.Y, -50));
                    app.LinePoints.Add(new Point3D(center.X, center.Y, 50));
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
