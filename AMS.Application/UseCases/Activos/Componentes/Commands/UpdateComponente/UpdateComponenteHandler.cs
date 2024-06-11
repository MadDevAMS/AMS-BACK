﻿using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.UpdateComponente
{
    public class UpdateComponenteHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<UpdateComponenteCommand, BaseResponse<ComponenteDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<ComponenteDto>> Handle(UpdateComponenteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ComponenteDto>();

            try
            {
                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);

                if (!userId.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var componente = _mapper.Map<ComponenteDto>(request);
                var data = await _unitOfWork.ActivosRepository.UpdateComponenteAsync(componente, userId.Value);

                response.Data = data;
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.COMPONENTE_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
