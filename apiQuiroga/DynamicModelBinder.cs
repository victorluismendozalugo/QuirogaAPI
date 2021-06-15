using Nancy;
using Newtonsoft.Json;
using System.IO;

namespace apiQuiroga
{
   public static class DynamicModelBinder
   {
      public static dynamic BindModel(this NancyModule context)
      {
         var serializer = new JsonSerializer();

         using (var sr = new StreamReader(context.Request.Body))
         {
            using (var jsonTextReader = new JsonTextReader(sr))
            {
               return serializer.Deserialize(jsonTextReader);
            }
         }
      }
   }
}