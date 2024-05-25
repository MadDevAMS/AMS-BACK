namespace AMS.Application.Commons.Bases
{
    internal static class ResponseMessage
    {
        public const string QUERY_SUCCESS = "Consulta Exitosa";
        public const string USER_SUCCESS_REGISTER = "El usuario se registrado exitosamente";
        public const string ENTIDAD_SUCCESS_UPDATE = "La Entidad se actualizo exitosamente";
        public const string ENTIDAD_SUCCESS_CREATE = "La Entidad se creo exitosamente, ingrese con su nuevo usuario";
        public const string TOKEN_SUCCESS = "Token generado correctamente";
    }

    internal static class MessageValidator
    {
        public const string BAD_EMAIL = "El correo no tiene el formato correcto.";
        public const string NOT_NULL = "El Nombre no puede ser nulo.";
        public const string NOT_EMPTY = "El campo no puede estar vacio.";
        public const string PASSWORD_LEGHT = "La contraseña debe tener al menos 8 caracteres.";
        public const string PASSWORD_SPECIAL_CHARACTER = "La contraseña debe contener al menos un carácter especial.";
        public const string RUC_LEGHT = "El RUC debe tener 11 digitos";
        public const string TELEFONO_LEGHT_MAX = "El telefono debe tener como maximo 10 digitos";
        public const string TELEFONO_LEGHT_MIN = "El telefono debe tener como minimo 9 digitos";
        public const string CONFIRM_PASSWORD = "Las contraseñas no coinciden";
        public const string RAZON_SOCIAL_INVALID = "La Razon social no es valida";
    }

    internal static class ExceptionMessage
    {
        public const string USER_EXISTS = "Ya existe una cuenta con este correo";
        public const string INVALID_CREDENTIALS = "El usuario y/o contrasena es incorrecto.";
        public const string CONFIRM_PASSWORD = "Las contraseñas no coinciden";
    }
}
