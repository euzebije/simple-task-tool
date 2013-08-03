using System;
using System.Collections.Generic;

namespace STT.Model.Entity
{
    public class Project : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserAccount Owner { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }

        public Project()
        {
            WorkItems = new List<WorkItem>();
        }
        public Project(string name, string description, UserAccount owner)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("description");
            if (owner == null)
                throw new ArgumentNullException("owner");

            Name = name;
            Description = description;
            CreatedOn = DateTime.Now;
            Owner = owner;
            WorkItems = new List<WorkItem>();
        }
    }
}
