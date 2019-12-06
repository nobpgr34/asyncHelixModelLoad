using HelixProject.HelperClasses;
using HelixProject.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace HelixProject
{
    public class ApplicationViewModel : ObservableObject
    {

        public ApplicationViewModel()
        {
            Logger.InitLogger();
            PageViewModels.Add(new Task3ViewModel(this));
            PageViewModels.Add(new Task4ViewModel(this));
            PageViewModels.Add(new Task5ViewModel(this));
            CurrentPageViewModel = PageViewModels[0];
        }

        private ICommand _changePageCommand;

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        private ObservableCollection<IPageViewModel> _pageViewModels;

        public ObservableCollection<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new ObservableCollection<IPageViewModel>();

                return _pageViewModels;
            }
        }

        private IPageViewModel _currentPageViewModel;

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        private Model3D model;

        public Model3D Model
        {
            get { return model; }
            set
            {
                if (value != model)
                {
                    model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        private Rect3D rect;

        public Rect3D Rect
        {
            get { return rect; }
            set
            {
                if (value != rect)
                {
                    rect = value;
                    OnPropertyChanged("Rect");
                }
            }
        }

        private Point3DCollection linePoints = new Point3DCollection();

        public Point3DCollection LinePoints
        {
            get { return linePoints; }
            set
            {
                if (value != linePoints)
                {
                    linePoints = value;
                    OnPropertyChanged("LinePoints");
                }
            }
        }

        private Transform3D transform = new MatrixTransform3D();

        public Transform3D Transform
        {
            get { return transform; }
            set
            {
                if (value != transform)
                {
                    transform = value;
                    OnPropertyChanged("Transform");
                }
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
            CurrentPageViewModel.workWithBoundingBox();
        }
    }
}
