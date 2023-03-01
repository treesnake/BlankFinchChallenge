using BlackFinch.IPersistance;
using BlackFinch.Models;

namespace BlackFinch.Persistance;

public class LoanPersister : ILoanPersister
{
    private List<LoanApplicationResult> loanApplicationResults = new List<LoanApplicationResult>();

    public decimal GetMeanAverageOfLoanToValue()
    {
        if (loanApplicationResults.Count < 1)
        {
            return 0;
        }
        else
        {
            return (from val in loanApplicationResults select val.LoanApplication.LTV).Average();
        }
    }

    public int GetTotalNumberOfApplicants()
    {
        return loanApplicationResults.Count;
    }

    public int GetTotalNumberOfSuccessfulApplicants()
    {
        return loanApplicationResults.Where(x => x.Successful).Count();
    }

    public int GetTotalNumberOfUnsuccessfulApplicants()
    {
        return loanApplicationResults.Where(x => !x.Successful).Count();
    }

    public decimal GetTotalValueOfLoansWritten()
    {
        return loanApplicationResults.Where(x => x.Successful).Sum(x => x.LoanApplication.LoanAmount);
    }

    public bool PersistLoanApplication(LoanApplicationResult applicationResult)
    {
        if (applicationResult != null)
        {
            loanApplicationResults.Add(applicationResult);
            return true;
        }
        else
        {
            return false;
        }
    }
}

