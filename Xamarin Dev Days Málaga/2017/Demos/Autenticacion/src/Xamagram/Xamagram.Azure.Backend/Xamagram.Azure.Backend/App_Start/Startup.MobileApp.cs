using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using System.Web.Hosting;
using System.IO;
using Newtonsoft.Json;
using Xamagram.Azure.Backend;
using Xamagram.DataObjects;

namespace Xamagram.Azure.Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            string dataPath = HostingEnvironment.MapPath(@"~/App_Data/items.json");
            string rawData = File.ReadAllText(dataPath);
            var xamagramItems = JsonConvert.DeserializeObject<IEnumerable<XamagramItem>>(rawData);

            Uri currentRequest = System.Web.HttpContext.Current.Request.Url;

            foreach (var item in xamagramItems)
            {
                // Update default image urls using current domain name
                item.Image = $"https://{currentRequest.Authority}/Images/{item.Id}.png";
            }

            context.Set<XamagramItem>().AddRange(xamagramItems);

            base.Seed(context);
        }
    }
}


