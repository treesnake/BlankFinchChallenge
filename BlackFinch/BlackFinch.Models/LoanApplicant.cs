using System.Diagnostics.CodeAnalysis;

namespace BlackFinch.Models
{
	public class LoanApplicant
	{
		// Using the required keyword to force the values to be set upon instantiation 
		public required Guid Id { get; init; } //Id for persistance
		public required int CreditScore;

		public LoanApplicant()
		{
		}

		[SetsRequiredMembers]
		public LoanApplicant(Guid id, int creditScore)
		{
			Id = id;
			if (!SetCreditScore(creditScore))
			{
				throw new Exception("Invalid Credit Score: "+creditScore);
			}
		}

		//Basic validation of credit score value
		public bool SetCreditScore(int creditScore)
		{
			if (creditScore >= 1 && creditScore <= 999)
			{
				CreditScore = creditScore;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}

