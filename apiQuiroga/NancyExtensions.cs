using apiQuiroga.Models.Usuario;
using Nancy;
using System;

namespace apiQuiroga
{
   public static class NancyExtensions
   {
      public static UsuarioModel BindToken(this NancyModule nancy)
      {
         var usuario = new UsuarioModel()
         {
            IdUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirst("idUsuario").Value),
            Usuario = nancy.Context.CurrentUser.FindFirst("usuario").Value
         };

         return usuario;
      }
   }
}