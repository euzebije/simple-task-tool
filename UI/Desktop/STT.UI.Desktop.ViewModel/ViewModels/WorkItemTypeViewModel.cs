using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class WorkItemTypeViewModel : ViewModelBase<WorkItemType>
    {
        public WorkItemTypeViewModel(WorkItemType model)
            : base(model)
        {
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }
        public string Description
        {
            get { return Model.Description; }
            set
            {
                if (Model.Description != value)
                {
                    Model.Description = value;
                    RaisePropertyChanged(() => Description);
                }
            }
        }
    }
}
