using WalletWatchAPI.Models.Enums;

namespace WalletWatchAPI.Models
{
    public class Loan
    {
		private string _id;

		public string Id
		{
			get { return _id; }
			private set { _id = value; }
		}

		private string _userId;

		public string UserId
		{
			get { return _userId; }
            private set { _userId = value; }
		}

		private User _user;

		public User User
		{
			get { return _user; }
			private set { _user = value; }
		}


		private decimal _amount;

		public decimal Amount
		{
			get { return _amount; }
			private set { _amount = value; }
		}

		private decimal _paymentAmount;

		public decimal PaymentAmount
		{
			get { return _paymentAmount; }
			private set { _paymentAmount = value; }
		}

		private RecurrencePeriod _recurrencePeriod;

		public RecurrencePeriod RecurrencePeriod
        {
			get { return _recurrencePeriod; }
			private set { _recurrencePeriod = value; }
		}

        private LoanType _type;

        public LoanType Type
        {
            get { return _type; }
            private set { _type = value; }
        }

        private DateTime _firstPaymentDate;

		public DateTime FirstPaymentDate
		{
			get { return _firstPaymentDate; }
			private set { _firstPaymentDate = value; }
		}

		private int _durationMonths;

		public int DurationMonths
		{
			get { return _durationMonths; }
			private set { _durationMonths = value; }
		}

		private IList<Transaction> _transactions;

		public IList<Transaction> Transactions
		{
			get { return _transactions; }
            private set { _transactions = value; }
		}




		public Loan()
        {
            
        }

		public decimal CalculateRemainingBalance()
		{
			decimal balance = _amount;
			foreach (var transaction in _transactions)
			{
				balance -= transaction.Amount;
			}

			return balance;
		}

    }
}
