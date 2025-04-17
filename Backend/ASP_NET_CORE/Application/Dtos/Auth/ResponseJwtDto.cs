namespace Application.Dtos.Auth
{
    public class ResponseJwtDto{

        public string AccessToken { get; set; }
        public string RefreshToken {  get; set; }

        public ResponseJwtDto(string accessToken, string? refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken!;
        }
    }
}
