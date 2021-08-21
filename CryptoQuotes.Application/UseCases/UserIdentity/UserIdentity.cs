namespace Application.UseCases.UserIdentity
{
	public class UserIdentityModel
    {
		public UserIdentityModel(string token, string userName)
		{
			Token = token;
			UserName = userName;
		}
		
		public string Token { get; set; }

        public string UserName { get; set; } 
	}
}
