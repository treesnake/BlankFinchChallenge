using System.Diagnostics.CodeAnalysis;

namespace BlackFinch.Models
{
    public class LoanApplication
    {
        // Using the required keyword to force the values to be set upon instantiation 
        public required Guid Id { get; init; } //Id for persistance
        public required decimal LoanAmount; //using decimal for high precision right of point
        public required decimal AssetValue; //using decimal for high precision right of point
        public required decimal LTV { get; init; }
        public required LoanApplicant LoanApplicant { get; init; }

        public LoanApplication() { }

        [SetsRequiredMembers]
        public LoanApplication(Guid id, decimal loanAmount, decimal assetValue , LoanApplicant loanApplicant)
        {
            Id = id;
            if (!SetLoanAmount(loanAmount))
            {
                throw new Exception("Invalid Loan Amount: " + loanAmount);
            }

            if (!SetAssetValue(assetValue))
            {
                throw new Exception("Invalid Asset Value: " + assetValue);
            }

            //null check
            if (loanApplicant != null)
            {
                LoanApplicant = loanApplicant;
            }
            else
            {
                throw new Exception("Invalid Applicant: Null value");
            }

            //Set the LTV (loan divided by asset multiplied by 100)
            // i.e. £100,000 asset with £50,000 loan = LTV of 50%
            LTV = (loanAmount /assetValue) * 100; 
            
        }

        public bool SetLoanAmount(decimal loanAmount)
        {
            if (loanAmount > 0)
            {
                LoanAmount = loanAmount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetAssetValue(decimal assetValue)
        {
            // Assumption - Asset value must be > 0 but values less than the baseline LTV produce failure rather than error
            if (assetValue > 0)
            {
                AssetValue = assetValue;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


