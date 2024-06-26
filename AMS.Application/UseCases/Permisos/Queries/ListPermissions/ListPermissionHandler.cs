﻿using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Permissions;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Permisos.Queries.ListPermissions
{
    public class ListPermissionHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ListPermissionQuery, PaginatorResponse<PermissionsListResponseDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PaginatorResponse<PermissionsListResponseDto>> Handle(ListPermissionQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatorResponse<PermissionsListResponseDto>();

            try
            {

                response = await _unitOfWork.GroupRepository.ListPermissionAsync(request);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.QUERY_SUCCESS;

            }
            catch (Exception ex)
            {

                response.Status = (int)ResponseCode.CONFLICT;
                response.Message = ex.Message;

            }

            return response;

        }

    }
}