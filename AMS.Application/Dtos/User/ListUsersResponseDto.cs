﻿using AMS.Application.Dtos.Groups;

namespace AMS.Application.Dtos.User
{
    public class ListUsersResponseDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int State { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public List<GroupListDto> Group { get; set; } = null!;
    }
}
