using apiQuiroga.Models.Usuario;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using WarmPack.Utilities;

namespace apiQuiroga.Models
{
    public class Globales
    {
        private static AppConfigurationX _Configuracion;

        //Clave de seguridad del token
        public static string Key => "Ch4ng0s!!";

        //Emisor del token si es necesario
        public static string ValidIssuer = "http://kragsoftware.com/";

        //Audiencias del token
        public static List<String> ValidAudiences = new List<String> { "miportalmx", "triplesoft" };

        //Cadena de conexion
        //public static string ConexionPrincipal => Globales.Configuracion.ConnectionString("admision", false, true, "data source = negrito.uasnet.mx; initial catalog = SIIA_CE; user id = alejandro; password = triplea");
        //public static string ApiKey => Globales.Configuracion.Parameter("ApiKey", true, "x-api-key").ToString();

        //valor true para que desencripte la cadena de conexino
        public static string ConexionPrincipal => Globales.Configuracion.ConnectionString("Conexion", true);
        public static string ApiKey => Globales.Configuracion.Parameter("api-key").ToString();
        public static string RutaApp;

        //Manejo de los token
        public static byte[] KeyByteArray = Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.ASCII.GetBytes(Globales.Key)));
        public static SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(KeyByteArray);

        public static List<UsuarioTokenRefreshModel> TokenRefreshUsuario = new List<UsuarioTokenRefreshModel>();

        //Opciones de configuracion        
        public static AppConfigurationX Configuracion
        {
            get
            {
                if (_Configuracion == null)
                {
                    var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                    var uri = new Uri(codeBase);
                    String path = Path.GetDirectoryName(uri.LocalPath) + "\\";

                    Globales.RutaApp = path;

                    // moficar la semilla con precaución, solo al inicio o no se va a poder leer la configuración al menos que se modifique
                    //_Configuracion = new AppConfigurationX(@"C:\Config\api_config.xml", new Encrypter("Ch4ng0s!!"));
                    //_Configuracion = new AppConfigurationX(Globales.RutaApp + "dll\\api_config.xml", new Encrypter("Ch4ng0s!!"));
                    _Configuracion = new AppConfigurationX(Globales.RutaApp + "Config.xml", new Encrypter("Ch4ng0s!!"));
                }

                return _Configuracion;
            }
            set
            {
                _Configuracion = value;
            }
        }

        public static TokenResponseModel GetJwtUsuario(UsuarioModel usuario)
        {
            var now = DateTime.Now;
            const int expireMinutes = 30;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Usuario),
                new Claim("idUsuario", usuario.IdUsuario.ToString()),
                new Claim("usuario", usuario.Usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: Globales.ValidIssuer,
                audience: Globales.ValidAudiences?.FirstOrDefault(),
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(expireMinutes)),
                signingCredentials: new SigningCredentials(Globales.SigningKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResponseModel
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)TimeSpan.FromMinutes(expireMinutes).TotalSeconds,
                RefreshToken = Guid.NewGuid().ToString(),
            };

            Globales.TokenRefreshUsuario.RemoveAll(p => p.Usuario?.IdUsuario == usuario.IdUsuario);
            Globales.TokenRefreshUsuario.Add(new UsuarioTokenRefreshModel() { Usuario = usuario, Uid = response.RefreshToken });

            return response;
        }
    }
}
