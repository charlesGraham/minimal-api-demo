namespace WebApiDemo.Authority
{
    public static class AppRepo
    {
        private static List<Application> _applications = new()
        {
            new Application
            {
                ApplicationId = 1,
                ApplicationName = "MVCWebApp",
                ClientId = "36854d13-991e-48ea-b481-8bb8c6a58391",
                ClientSecret = "d071cb9d-4a98-40b2-b903-72f13821aef7",
                Scopes = "read,write",
            },
        };


        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(app => app.ClientId == clientId);
        }
    }
}
