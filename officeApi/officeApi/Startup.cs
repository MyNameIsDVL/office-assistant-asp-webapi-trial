using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

[assembly: OwinStartup(typeof(officeApi.Startup))]

namespace officeApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Aby uzyskać więcej informacji o sposobie konfigurowania aplikacji, odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCors(CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                // token do autoryzacji logowania
                TokenEndpointPath = new PathString("/token"),
                // jak chcemy zwalidować użytkownika na podstawie podanego loginu i hasła
                Provider = new OAuthProvider(),
                // Określany jest dostęp do wygenerowanego tokena dla logowania, przez określony czas
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(100),
                // zezwolenie niezabezpieczone połączenie http
                AllowInsecureHttp = true
            };
            // aby aplikacja wiedziala że ma uzywać tych opcji
            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
