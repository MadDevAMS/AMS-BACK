namespace AMS.Application.Commons.Utils
{
    internal static class ResponseMessage
    {
        public const string QUERY_SUCCESS = "Consulta Exitosa";

        public const string LOGIN_SUCCESS = "Inicio de sesion exitoso";

        public const string USER_SUCCESS_REGISTER = "El usuario se registrado exitosamente";
        public const string DELETE_USER_SUCCESS = "El usuario se elimino correctamente";

        public const string ENTIDAD_SUCCESS_CREATE = "La Entidad se creo exitosamente, ingrese con su nuevo usuario";
        public const string ENTIDAD_SUCCESS_UPDATE = "La Entidad se actualizo exitosamente";

        public const string TOKEN_SUCCESS = "Token generado correctamente";

        public const string GROUP_UPDATE_CREATE = "El grupo se creo correctamente";
        public const string GROUP_SUCCESS_UPDATE = "El grupo se actualizo correctamente";
        public const string GROUP_SUCCESS_DELETE = "El grupo se elimino correctamente";
    }

    internal static class ResponseActivosMessage
    {
        public const string METRICA_SUCCESS_REGISTER = "La Metrica se registro correctamente";
        public const string METRICA_SUCCESS_UPDATE = "La Metrica se actualizo correctamente";
        public const string METRICA_SUCCESS_DELETE = "La Metrica se elimino correctamente";

        public const string PUNTO_SUCCESS_REGISTER = "El Punto de Monitoreo se registro correctamente";
        public const string PUNTO_SUCCESS_UPDATE = "El Punto de Monitoreo se actualizo correctamente";
        public const string PUNTO_SUCCESS_DELETE = "El Punto de Monitoreo se elimino correctamente";

        public const string AREA_SUCCESS_REGISTER = "El Area se registro correctamente";
        public const string AREA_SUCCESS_UPDATE = "El Area se actualizo correctamente";
        public const string AREA_SUCCESS_DELETE = "El Area se elimino correctamente";

        public const string COMPONENTE_SUCCESS_REGISTER = "El Componente se registro correctamente";
        public const string COMPONENTE_SUCCESS_UPDATE = "El Componente se actualizo correctamente";
        public const string COMPONENTE_SUCCESS_DELETE = "El Componente se elimino correctamente";

        public const string MAQUINA_SUCCESS_REGISTER = "La Maquina se registro correctamente";
        public const string MAQUINA_SUCCESS_UPDATE = "La Maquina se actualizo correctamente";
        public const string MAQUINA_SUCCESS_DELETE = "La Maquina se elimino correctamente";

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
    };

    public static class MiddlewareMessage
    {
        public const string NOT_AUTHORIZATION = "No cuenta con los permisos necesarios comunicate con el administrador.";
        public const string ERRORS_REQUEST = "Errores de validacion";
    }
}
