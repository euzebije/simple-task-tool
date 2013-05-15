using System;
using System.Collections.Generic;

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
        public Project Project { get; set; }
        public WorkItem Parent { get; set; }
        public ICollection<WorkItem> Children { get; set; }
        public bool IsFinished { get; set; }

        public WorkItem()
        {
            Children = new List<WorkItem>();
        }
        public WorkItem(string title, string description, UserAccount createdBy, Priority priority, WorkItemType type, Project project)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");
            if (createdBy == null)
                throw new ArgumentNullException("createdBy");
            if (type == null)
                throw new ArgumentNullException("type");
            if (project == null)
                throw new ArgumentNullException("project");

            Title = title;
            Description = description;
            CreatedBy = createdBy;

            var now = DateTime.Now;
            CreatedOn = now;
            LastUpdate = now;

            Priority = priority;
            Type = type;
            Project = project;
            Children = new List<WorkItem>();
        }
        public WorkItem(string title, string description, UserAccount createdBy, Priority priority, WorkItemType type, Project project, WorkItem parent)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");
            if (createdBy == null)
                throw new ArgumentNullException("createdBy");
            if (type == null)
                throw new ArgumentNullException("type");
            if (project == null)
                throw new ArgumentNullException("project");
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
            Project = project;
            Parent = parent;
            Children = new List<WorkItem>();
        }
    }
}
