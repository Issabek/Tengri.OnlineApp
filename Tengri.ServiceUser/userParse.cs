using System;
using RestSharp;
using log4net;
using log4net.Config;
namespace Tengri.ServiceUser
{
    public class userParse 
    {
        private static ILog log = LogManager.GetLogger("LOGGER");

        public static User GetUserData(string iin)
        {
            XmlConfigurator.Configure();
            try
            {
                var request = new RestRequest("/api-form/load-info-by-iin/?iin=" + iin + "&params[city_check]=true", Method.GET);
                IRestResponse response = GetResponse(GetClient(), request);
                var error = response.ErrorException;
                User data = new User(response.Content);
                return data;
            }
            catch (Exception ex)
            {
                log.Debug("Here is a debug log.");
                log.Info("... and an Info log.");
                log.Warn("... and a warning.");
                log.Error("... and an error.");
                log.Fatal("... and a fatal error.");
                Console.WriteLine(ex.Message);
                return null;

            }
        }
        private static RestClient GetClient()
        {
            RestClient client = new RestClient("https://meteor.almaty.e-orda.kz/ru");

            return client;
        }

        private static IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            try
            {
                IRestResponse response = client.Get(request);
                return response;
            }
            catch (System.NullReferenceException nex)
            {

            }
            catch (Exception ex)
            {

            }

            return null;
        }

    }
}
