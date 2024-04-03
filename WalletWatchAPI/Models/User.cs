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

		private IList<Loan> _loans;

		public IList<Loan> Loans
		{
			get { return _loans; }
            private set { _loans = value; }
		}

		private IList<Transaction> _transactions;

		public IList<Transaction> Transactions
		{
			get { return _transactions; }
            private set { _transactions = value; }
		}

        public User()
        {
            
        }
    }
}
