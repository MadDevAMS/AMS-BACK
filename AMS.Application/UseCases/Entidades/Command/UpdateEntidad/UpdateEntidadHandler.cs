using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Application.Interfaces.Services;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Entidades.Command.UpdateEntidad
{
    public class UpdateEntidadHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext, IS3Files s3Files) : IRequestHandler<UpdateEntidadCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;
        private readonly IS3Files _s3Files = s3Files;

        public async Task<BaseResponse<bool>> Handle(UpdateEntidadCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);
                var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD);

                if (!userId.HasValue || !idEntidad.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var entidad = _mapper.Map<Entidad>(request);
                entidad.Id = idEntidad.Value;

                if (request.File is not null)
                {
                    var prefix = $"Entidad-{idEntidad}";

                    var result = await HandleFileUpload(request.File, prefix);

                    if (!result.Item1)
                    {
                        response.Status = (int)ResponseCode.BAD_REQUEST;
                        response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                        return response;
                    }
                    entidad.Image = result.Item2;
                }

                await _unitOfWork.EntidadRepository.UpdateAsync(entidad, userId.Value);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.ENTIDAD_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        private async Task<(bool, string)> HandleFileUpload(IFormFile file, string prefix)
        {
            bool saveFile = await _s3Files.UploadFileAsync(BucketNames.Entidades, prefix, file);
            if (!saveFile)
            {
                return (false, null);
            }

            string fileUrl = await _s3Files.GetFileAsync(BucketNames.Entidades, prefix, file.FileName);
            return (true, fileUrl);
        }
    }
}
