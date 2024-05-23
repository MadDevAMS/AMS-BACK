using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Entidad;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace AMS.Application.UseCases.Entidades.Command.CreateEntidad
{
    public class CreateEntidadHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateEntidadCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(CreateEntidadCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var entidadDto = _mapper.Map<EntidadRegistroDto>(request);
                entidadDto.Password = BC.HashPassword(entidadDto.Password);
                await _unitOfWork.EntidadRepository.CreateAsync(entidadDto);

                response.IsSuccess = true;
                response.Message = ResponseMessage.ENTIDAD_SUCCESS_CREATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
