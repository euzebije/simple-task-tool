using System;

namespace STT.Model.Entity
{
    public class WorkItemType : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public WorkItemType(){}
        public WorkItemType(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("description");

            Name = name;
            Description = description;
        }
    }
}
