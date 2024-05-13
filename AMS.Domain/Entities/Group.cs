﻿namespace AMS.Domain.Entities
{
    public sealed class Group : BaseEntity
    {
        public Group()
        {
            GroupPermission = new HashSet<GroupPermission>();
            GroupUsers = new HashSet<GroupUsers>();
        }

        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public ICollection<GroupPermission> GroupPermission { get; set; }
        public ICollection<GroupUsers> GroupUsers { get; set; }

    }
}