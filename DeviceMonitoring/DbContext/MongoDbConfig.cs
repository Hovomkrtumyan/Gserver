namespace DeviceMonitoring.DbContext
{
    public static class MongoDbConfig
    {
        public static string Database { get; set; }
        public static string Host { get; set; }
        public static int Port { get; set; }
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    return $@"mongodb://{Host}:{Port}";
                return $@"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}