using AMS.Application.Commons.Bases;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.GroupPermissions.Command.AssignPermissiontoGroup
{
    public class AssignPermissiontoGroupHandler(IUnitOfWork unitOfWork,IMapper mapper) :  IRequestHandler<AssignPermissiontoGroupCommand ,BaseResponse<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(AssignPermissiontoGroupCommand request, CancellationToken cancellation)
        {
            var response = new BaseResponse<bool>();
            try { 
            
            } catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        
    }
}
