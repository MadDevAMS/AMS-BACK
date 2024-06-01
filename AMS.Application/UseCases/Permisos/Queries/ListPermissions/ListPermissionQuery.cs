using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Roles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.Permisos.Queries.ListPermissions
{
    public class ListPermissionQuery : BasePagination, IRequest<PaginatorResponse<PermissionsListResponseDto>>
    {
        public long IdPermission { get; set; }
        public string NamePermission { get; set; } = string.Empty;
        public int StatePermission { get; set; }
    }
}
