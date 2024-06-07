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
                
                var groupPermission = _mapper.Map<Domain.Entities.GroupPermission>(request);
                await _unitOfWork.GroupPermissionRepository.CreateGroupPermissionsAsync(groupPermission);

                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseMessage.GROUPPERSSION_SUCCESS_CREATED;

            } 
            catch (Exception ex) 
            { 
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
