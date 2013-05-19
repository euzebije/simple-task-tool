namespace STT.UI.Desktop.ViewModel.Test.Mock
{
    public class MockViewModel : ViewModelBase
    {
        private string _implicitNotificationProperty;
        public string ImplicitNotificationProperty
        {
            get { return _implicitNotificationProperty; }
            set
            {
                if (value != _implicitNotificationProperty)
                {
                    _implicitNotificationProperty = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _explicitNotificationProperty;
        public string ExplicitNotificationProperty
        {
            get { return _explicitNotificationProperty; }
            set
            {
                if (value != _explicitNotificationProperty)
                {
                    _explicitNotificationProperty = value;
                    OnPropertyChanged("ExplicitNotificationProperty");
                }
            }
        }
    }
}
