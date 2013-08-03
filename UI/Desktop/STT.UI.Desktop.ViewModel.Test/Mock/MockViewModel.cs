namespace STT.UI.Desktop.ViewModel.Test.Mock
{
    public class MockViewModel : ViewModelBase
    {
        private string _expressionNotificationProperty;
        private string _stringNotificationProperty;

        public string ExpressionNotificationProperty
        {
            get { return _expressionNotificationProperty; }
            set
            {
                if (value == _expressionNotificationProperty) return;
                _expressionNotificationProperty = value;
                RaisePropertyChanged(() => ExpressionNotificationProperty);
            }
        }

        public string StringNotificationProperty
        {
            get { return _stringNotificationProperty; }
            set
            {
                if (value == _stringNotificationProperty) return;
                _stringNotificationProperty = value;
                RaisePropertyChanged("StringNotificationProperty");
            }
        }
    }
}
