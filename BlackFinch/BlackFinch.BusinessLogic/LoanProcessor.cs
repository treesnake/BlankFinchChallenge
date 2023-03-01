using BlackFinch.IBusinessLogic;
using BlackFinch.Models;

namespace BlackFinch.BusinessLogic;
public class LoanProcessor : ILoanProcessor
{
    public LoanApplicationResult ProcessLoanApplication(LoanApplication loanApplication)
    {
        //Check Boundary Values
        if (loanApplication.LoanAmount < 100000)
        {
            return new LoanApplicationResult(Guid.NewGuid(), false, "Loan Declined: Loan Value too low", loanApplication);
        }
        else if (loanApplication.LoanAmount > 1500000)
        {
            return new LoanApplicationResult(Guid.NewGuid(), false, "Loan Declined: Loan Value too high", loanApplication);
        }

        else if (loanApplication.LoanApplicant.CreditScore < 750)
        {
            return new LoanApplicationResult(Guid.NewGuid(), false, "Loan Declined: Low Credit Score" + loanApplication.Id, loanApplication);
        }

        // Check Values > £1,000,000
        else if (loanApplication.LoanAmount >= 1000000)
        {
            //LTV must be 60% or less AND credit score 950 or more
            if (loanApplication.LTV <= 60 && loanApplication.LoanApplicant.CreditScore >= 950)
            {
                return new LoanApplicationResult(Guid.NewGuid(), true, "Loan Approved" + loanApplication.Id, loanApplication);
            }
            else
            {
                return new LoanApplicationResult(Guid.NewGuid(), false, "Loan Declined" + loanApplication.Id, loanApplication);
            }
        }

        else if (loanApplication.LoanAmount < 1000000)
        {
            // Declined - LTV too high
            if (loanApplication.LTV >= 90)
            {
                return new LoanApplicationResult(Guid.NewGuid(), false, "Loan Declined" + loanApplication.Id, loanApplication);
            }

            //if LTV greater than 80 and credit score 900 or more
            else if (loanApplication.LTV >= 80 && loanApplication.LoanApplicant.CreditScore >= 900)
            {
                return new LoanApplicationResult(Guid.NewGuid(), true, "Loan Approved" + loanApplication.Id, loanApplication);
            }

            //if LTV greater than 70 and credit score 800 or more
            else if (loanApplication.LTV >= 60 && loanApplication.LoanApplicant.CreditScore >= 800)
            {
                return new LoanApplicationResult(Guid.NewGuid(), true, "Loan Approved" + loanApplication.Id, loanApplication);
            }

            //if LTV greater than 0 and credit score 750 or more
            else if (loanApplication.LTV < 60 && loanApplication.LoanApplicant.CreditScore >= 750)
            {
                return new LoanApplicationResult(Guid.NewGuid(), true, "Loan Approved" + loanApplication.Id, loanApplication);
            }
            
            else
            {
                return new LoanApplicationResult(Guid.NewGuid(), false, "Loan Declined " + loanApplication.Id, loanApplication);
            }

        }

        //Catch-All - leave this in because, as the code changes, an error may lead to clause-escape which can result in undesired processing
        //Error message intentionally vague - Revealing specific details is a cyber risk (e.g. Boundary exploits)
        else
        {
            return new LoanApplicationResult(Guid.NewGuid(), false, "Error: Contact Tech Support - Loan Application " + loanApplication.Id, loanApplication);
        }
    }


}

