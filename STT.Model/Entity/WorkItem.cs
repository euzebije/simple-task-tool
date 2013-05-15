using System;

namespace STT.Model.Entity
{
    public class WorkItem : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserAccount CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserAccount AssignedTo { get; set; }
        public DateTime LastUpdate { get; set; }
        public Priority Priority { get; set; }
        public WorkItemType Type { get; set; }
        public WorkItem Parent { get; set; }
        public bool IsFinished { get; set; }

        public WorkItem(){}
        public WorkItem(string title, string description, UserAccount createdBy, Priority priority, WorkItemType type)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");
            if (createdBy == null)
                throw new ArgumentNullException("createdBy");
            if (type == null)
                throw new ArgumentNullException("type");

            Title = title;
            Description = description;
            CreatedBy = createdBy;

            var now = DateTime.Now;
            CreatedOn = now;
            LastUpdate = now;

            Priority = priority;
            Type = type;
        }
        public WorkItem(string title, string description, UserAccount createdBy, Priority priority, WorkItemType type, WorkItem parent)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");
            if (createdBy == null)
                throw new ArgumentNullException("createdBy");
            if (type == null)
                throw new ArgumentNullException("type");
            if (parent == null)
                throw new ArgumentNullException("parent");

            Title = title;
            Description = description;
            CreatedBy = createdBy;

            var now = DateTime.Now;
            CreatedOn = now;
            LastUpdate = now;

            Priority = priority;
            Type = type;
            Parent = parent;
        }
    }
}
