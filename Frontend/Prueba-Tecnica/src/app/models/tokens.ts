
export interface AccessToken{
  accessToken: string;
}
export interface AuthTokens extends AccessToken  {
  refreshToken: string;
}


