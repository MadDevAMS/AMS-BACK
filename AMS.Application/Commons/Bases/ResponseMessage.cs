namespace AMS.Application.Commons.Bases
{
    internal static class ResponseMessage
    {
        public const string USER_SUCCESS_REGISTER = "El usuario se registrado exitosamente";
        public const string USER_SUCCESS_UPDATE= "El usuario se actualizo exitosamente";
    }

    internal static class MessageValidator
    {
        public const string BAD_EMAIL = "El correo no tiene el formato correcto.";
        public const string NOT_NULL = "El Nombre no puede ser nulo.";
        public const string NOT_EMPTY = "El campo no puede estar vacio.";
        public const string PASSWORD_LEGHT = "La contraseña debe tener al menos 8 caracteres.";
        public const string PASSWORD_SPECIAL_CHARACTER = "La contraseña debe contener al menos un carácter especial.";
    }

    internal static class ExceptionMessage
    {
        public const string USER_EXISTS = "Ya existe una cuenta con este correo";
    }
}
