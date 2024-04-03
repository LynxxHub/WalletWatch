using WalletWatchAPI.Models.Enums;

namespace WalletWatchAPI.Models
{
    public class Transaction
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

        public User User { get; set; }

        private decimal _amount;

		public decimal Amount
		{
			get { return _amount; }
			private set { _amount = value; }
		}

		private DateTime _transactionDate;

		public DateTime TransactionDate
		{
			get { return _transactionDate; }
			private set { _transactionDate = value; }
		}

		private TransactionType _transactionType;

		public TransactionType TransactionType
		{
			get { return _transactionType; }
			private set { _transactionType = value; }
		}

		private RecurrencePeriod _recurrencePeriod;

		public RecurrencePeriod RecurrencePeriod
		{
			get { return _recurrencePeriod; }
			private set { _recurrencePeriod = value; }
		}

		private bool _isRecurring;

		public bool IsRecurring
		{
			get { return _isRecurring; }
			private set { _isRecurring = value; }	
		}

		private TransactionCategory _category;

		public TransactionCategory Category
		{
			get { return _category; }
			private set { _category = value; }
		}

		private Loan? _loan;

		public Loan? Loan
		{
			get { return _loan; }
			private set { _loan = value; }
		}



		public Transaction()
        {
            
        }

        public DateTime CalculateNextOccurance()
		{
			if (_isRecurring)
			{
				if (_recurrencePeriod == RecurrencePeriod.Daily)
				{
					return _transactionDate.AddDays(1);
				}

				if(_recurrencePeriod == RecurrencePeriod.Weekly)
				{
					return _transactionDate.AddDays(7);
				}

				if(_recurrencePeriod == RecurrencePeriod.Monthly)
				{
					return _transactionDate.AddMonths(1);
				}

				if(_recurrencePeriod == RecurrencePeriod.Annual)
				{
					return _transactionDate.AddYears(1);
				}
			}

			throw new InvalidOperationException($"The transaction with ID {_id} is not setup as a recurring transaction.");
		}

    }
}
