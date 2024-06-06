using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Groups;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Groups.Command.CreateGroup
{
    public class CreateGroupHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<CreateGroupCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<bool>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var grupoDto = _mapper.Map<GroupCreateDto>(request);
                string? idEntidadString = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "IdEntidad")?.Value;
                if (long.TryParse(idEntidadString, out long idEntidad))
                {
                    grupoDto.IdEntidad = idEntidad;
                    await _unitOfWork.GroupRepository.CreateAsync(grupoDto);

                    response.Status = (int)ResponseCode.CREATED;
                    response.Message = ResponseMessage.GROUP_SUCCESS_CREATE;
                }
                else
                {
                    response.Status = (int)ResponseCode.BAD_REQUEST;
                }


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
