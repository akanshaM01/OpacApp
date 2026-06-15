using System;
using System.Collections.Generic;
using System.Text;

namespace OpacMauiApp.Models
{
    public class LoginCmd
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class LoginResp
    {

        public int? ID { get; set; }  //???? it is identity in DB
        public string memberid { get; set; }
        public string Userid { get; set; }
        public string ValidUpTo { get; set; }
        public DateTime? LoginTime { get; set; }
        public string CurrentSession { get; set; }
        public string UniqueId { get; set; } //session id
        public List<LibrarySetupLimitMod> LibIdAll { get; set; } //libraries assigned - further check allowed on front before show
        public byte[] memberPic { get; set; }
        public int? LibId { get; set; } //only if single lib is assigned/available
        public AuthTokenResponse AuthTokenResponse { get; set; }

    }
    public class AuthTokenResponse
    {
        public string AccessToken { get; set; }

        public int AccessTokenExpiresIn { get; set; }

        public string SessionId { get; set; }
    }
    public class LoginSetLibDecrCmd
    {
        public string? login { get; set; }
        public string? password { get; set; }
        public string? libId { get; set; }
        public string? fingerprint { get; set; }
    }
    public class LoggedDetMod
    {
        public int? ID { get; set; }  //???? it is identity in DB
        public string? User_id { get; set; }
        public string? Name { get; set; }
        public string? MemCode { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public int? LibId { get; set; }
        public string LibName { get; set; }
        //public string Token { get; set; }
        //public int AccessTokenExpiresIn { get; set; }
        //public string SessonId { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public AuthTokenResponse AuthTokenResponse { get; set; }
        public int? DatabaseId { get; set; }
    }
    public class RefreshToken
    {
        public string? Token { get; set; } = "";
        public string? oldToken { get; set; } = "";
        public string? fingerprint { get; set; } = "";
        public string? Uniqueid { get; set; } = ""; //session id
        public string? UserId { get; set; } = "";
        public int? LibId { get; set; }
        public DateTime Expiry { get; set; }
        public bool Revoked { get; set; }
    }
}
