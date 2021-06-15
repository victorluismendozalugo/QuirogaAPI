using Nancy;

namespace apiQuiroga.Modules
{
   public class RootModule : NancyModule
   {
      public RootModule()
      {
         Get("/", _ => GetRoot());
      }

      private object GetRoot()
      {
         return Response.AsJson("La api Quiroga se encuentra funcionando ... :)");
      }
   }
}