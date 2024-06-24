namespace AMS.Application.Commons.Utils
{
    internal static class Constans
    {
        public static string[] RazonesSociales =
        [
            "SAC",
            "SA",
            "EIRL",
            "SAA",
            "SRL"
        ];
    }

    internal static class Claims
    {
        public const string ENTIDAD = "IdEntidad";
        public const string USERID = "IdUser";
    }

    internal static class MeasurementType
    {
        public const int ACCELERATION = 1;
        public const int VELOCITY = 2;
        public const int TEMPERATURE = 3;
    }
    internal static class BucketNames
    {
        public const string Entidades = "dev-ams-api-bucket ";//ams-bucket-api
    }
}
