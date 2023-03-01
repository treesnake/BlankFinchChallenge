using BlackFinch.Models;

namespace BlackFinch.IBusinessLogic;
public interface ILoanProcessor
{
    LoanApplicationResult ProcessLoanApplication(LoanApplication loanApplication);
}

