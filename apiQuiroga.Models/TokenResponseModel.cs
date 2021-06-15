namespace apiQuiroga.Models
{
   public class TokenResponseModel
   {
      public string AccessToken { get; set; }
      public int ExpiresIn { get; set; }
      public string RefreshToken { get; set; }
   }
}