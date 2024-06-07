using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.GroupUsers.Command.CreateGroupUsers
{
    public class CreateGroupUsersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateGroupUsersCommand, BaseResponse<bool> >
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(CreateGroupUsersCommand request, CancellationToken cancellation)
        {
            var response = new BaseResponse<bool>();

            try {

                var groupUsers = _mapper.Map<Domain.Entities.GroupUsers>(request);
                await _unitOfWork.GroupUserRepository.CreateGroupUsers(groupUsers);

                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseMessage.GROUPUSERS_SUCCESS_CREATED;

            
            }catch (Exception ex) 
            { 
                response.Message = ex.Message;
            }

            return response;
        }


    }
}
