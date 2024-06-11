namespace AMS.Application.Dtos.Permissions;

public class PermissionsListResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int State { get; set; }
}