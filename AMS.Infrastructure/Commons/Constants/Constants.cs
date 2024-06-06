namespace AMS.Infrastructure.Commons.Commons
{
    internal static class FolderS3
    {
        public const string AmsUser = "AMS/users_pictures";
    }

    internal static class CustomClaims
    {
        public const string Permissions = "Permissions";
        public const string Groups = "Groups";
        public const string Entidad = "IdEntidad";
    }

    internal static class Utils
    {
        public const int ESTADO_ACTIVO = 1;
        public const long GROUP_ADMIN_ID = 1;
        public const string EMPTY_STRING = "";

    }

    internal static class ExcelHeaders
    {
        public const string TIMESTAMP = "timestamp";
        public const string VALUE = "value";
        public const string MEASUREMENT_TYPE = "measurement_type";
        public const string AXIS_LEVEL = "axis_label";
        public const string SPOT_DYID = "spot_dyid";
        public const string SPOT_NAME = "spot_name";
        public const string SPOT_TYPE = "spot_type";
        public const string SPOT_RPM = "spot_rpm";
        public const string SPOT_POWER = "spot_power";
        public const string SPOT_MODEL = "spot_model";
        public const string MACHINE_ID = "machine_id";
        public const string MACHINE_NAME = "machine_name";
        public const string BATERY_LEVEL = "batery_level";
        public const string TELEMETRY = "telemetry";
        public const string INTERVAL_UNIT = "interval_unit";
        public const string DYNAMIC_RANGE_IN_G = "dynamic_range_in_g";
        public const string DATA_SOURCE = "data_source";
        public const string SPOT_PATH = "spot_path";
    }
}
