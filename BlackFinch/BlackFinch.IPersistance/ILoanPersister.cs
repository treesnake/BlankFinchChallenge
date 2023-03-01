using BlackFinch.Models;

namespace BlackFinch.IPersistance;
public interface ILoanPersister
{
    bool PersistLoanApplication(LoanApplicationResult applicationResult);

    int GetTotalNumberOfApplicants();

    int GetTotalNumberOfSuccessfulApplicants();

    int GetTotalNumberOfUnsuccessfulApplicants();

    decimal GetTotalValueOfLoansWritten();

    decimal GetMeanAverageOfLoanToValue();
}

