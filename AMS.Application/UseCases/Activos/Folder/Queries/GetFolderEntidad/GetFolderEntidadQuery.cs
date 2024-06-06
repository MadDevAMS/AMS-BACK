using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Folder.Queries.GetFolderEntidad
{
    public class GetFolderEntidadQuery : IRequest<BaseResponse<FolderResponseDto>>
    {
        public long IdEntidad { get; set; }
    }
}
