using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.GroupPermission;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.GroupPermission.Command.CreateGroupPermissions
{
    public class CreateGroupPermissionsHandler (IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateGroupPermissionsCommand,BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(CreateGroupPermissionsCommand request, CancellationToken cancellation) 
        {
            var response = new BaseResponse<bool>();

            try {
                // 
                if (request.GroupId == null || request.GroupId.Count == 0 || request.PermissionId == null || request.PermissionId.Count == 0) 
                {
                    response.Status = (int)ResponseCode.BAD_REQUEST;
                    response.Message = ResponseMessage.INVALID_GROUP_OR_PERMISSION;
                    return response;
                
                }

                var validGroupIds = new List<long>();
                // Entonces lo que recomiendas es buscar por nombre del grupo y por el nombre del permiso 
                foreach (var groupId in request.GroupId)
                {
                    var group = await _unitOfWork.GroupRepository.GetByiIdAsync(groupId);
                    if (group != null) { 
                        validGroupIds.Add(group.Id);
                    }
                }

                // Quitar lo redundante
                var validPermissionIds = new List<long>(); 

                foreach (var permissionId in request.PermissionId) 
                { 
                    var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(permissionId);
                    if (permission != null)
                    {
                        validPermissionIds.Add(permission.Id);
                    }
                }

                if (validGroupIds.Count == 0 || validPermissionIds.Count == 0) 
                { 
                    response.Status = (int)ResponseCode.NOT_FOUND;
                    response.Message = "No se encontraron ids de grupos o permisos";
                    return response;
                }


                var groupPermissions = new GroupPermissionRegistroDto
                {
                    GroupId = validGroupIds,
                    PermissionId = validPermissionIds,
                };


                groupPermissions = _mapper.Map<GroupPermissionRegistroDto>(request);
                await _unitOfWork.GroupPermissionRepository.CreateGroupPermissionsAsync(groupPermissions);

                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseMessage.GROUP_PERMISSION_CREATE;
                response.Data = true;
            
            } 
            catch (Exception ex) 
            { 
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
