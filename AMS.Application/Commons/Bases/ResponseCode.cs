namespace AMS.Application.Commons.Bases
{
    //No le veo sentido dar un status code si de por si con un boolean puedo decirte si salio bien o mal el resultado
    public enum ResponseCode
    {
        OK = 200,
        CREATED = 201,
        ACCEPTED = 202,
        NOT_CONTENT = 204,
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        FORBIDDEN = 403,
        NOT_FOUND = 404,
    }
}
