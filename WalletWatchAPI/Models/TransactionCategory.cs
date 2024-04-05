using WalletWatchAPI.DTOs;
using WalletWatchAPI.Models.Enums;

namespace WalletWatchAPI.Models
{
    public class TransactionCategory
    {
		private string _id;

		public string Id
		{
			get { return _id; }
			private set { _id = value; }
		}

		private string _icon;

		public string Icon
		{
			get { return _icon; }
            private set { _icon = value; }
		}

		private string _name;

		public string Name
		{
			get { return _name; }
            private set { _name = value; }
		}

		private TransactionType _type;

		public TransactionType Type
		{
			get { return _type; }
			private set { _type = value; }
		}


		public TransactionCategory()
        {
            
        }

		public void Update(TransactionCategoryDTO transactionCategoryDto)
		{
			Name = transactionCategoryDto.Name;
			Icon = transactionCategoryDto.Icon;
			Type = (TransactionType)Enum.Parse(typeof(TransactionType), transactionCategoryDto.Type);
		}
    }
}
