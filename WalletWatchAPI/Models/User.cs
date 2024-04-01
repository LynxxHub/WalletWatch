using Microsoft.AspNetCore.Identity;

namespace WalletWatchAPI.Models
{
    public class User : IdentityUser
    {
		private string _firstName;

		public string FirstName
		{
			get { return _firstName; }
			private set { _firstName = value; }
		}

		private string _lastName;

		public string LastName
		{
			get { return _lastName; }
            private set { _lastName = value; }
		}

		private string _profilePictureURL;

		public string ProfilePictureURL
		{
			get { return _profilePictureURL; }
            private set { _profilePictureURL = value; }
		}

        public User(string firstName, string lastName, string profilePictureURL)
        {
			FirstName = firstName;
			LastName = lastName;
			ProfilePictureURL = profilePictureURL;
        }

        public User()
        {
            
        }
    }
}
