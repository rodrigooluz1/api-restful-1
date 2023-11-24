using System;
using API.Domain.ViewModels;

namespace API.Services
{
	public class UserService
	{
		public bool ValidateUser(UserViewModel user)
		{
			if (user.UserName == "admin" && user.Password == "1234")
				return true;
			else
				return false;
		}
	}
}

