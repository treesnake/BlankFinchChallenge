using System;
using System.Diagnostics.CodeAnalysis;

namespace BlackFinch.Models
{
	public class LoanApplicationResult
	{
		// Using the required keyword to force the values to be set upon instantiation 
		public required Guid Id { get; init; } //Id for persistance
		public required bool Successful { get; init; }
		public required string ResultReason { get; init; } //For UI to display a "why"
		public required LoanApplication LoanApplication { get; init; }

		public LoanApplicationResult()
		{
		}

        [SetsRequiredMembers]
        public LoanApplicationResult(Guid id, bool successful, string resultReason, LoanApplication loanApplication)
		{
			Id = id;
			Successful = successful;
			ResultReason = resultReason;

			//Null Check
			if (loanApplication != null)
				LoanApplication = loanApplication;
			else
				throw new Exception("Invalid Loan Application Value: Null Value");
		}
	}
}

