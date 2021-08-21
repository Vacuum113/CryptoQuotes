namespace Application.UseCases.User
{
	public class UserModel
    {
		public UserModel(Domain.Entities.AppUser.User user)
		{
			UserName = user.IdentityUser.UserName;
		}

		public string UserName { get; set; }
	}
}
