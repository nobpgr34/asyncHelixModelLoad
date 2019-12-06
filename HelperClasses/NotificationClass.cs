using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HelixProject.HelperClasses
{
    class NotificationClass<Base> : INotifyPropertyChanged
    {
        public NotificationClass(Base Notification)
        {
            this.notification = Notification;
        }
        Base notification;

        public Base Notification
        {
            get
            {
                return this.notification;
            }
            set
            {
                if ((this.notification.Equals(value) != true))
                {
                    this.notification = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
