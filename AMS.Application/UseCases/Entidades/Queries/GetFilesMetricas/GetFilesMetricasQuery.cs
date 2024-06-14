using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application;

public class GetFilesMetricasQuery : IRequest<BaseResponse<IEnumerable<S3ObjectDto>>>
{

}
