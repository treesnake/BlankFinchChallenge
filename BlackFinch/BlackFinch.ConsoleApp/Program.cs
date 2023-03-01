using BlackFinch.BusinessLogic;
using BlackFinch.IBusinessLogic;
using BlackFinch.IPersistance;
using BlackFinch.Models;
using BlackFinch.Persistance;

namespace BlackFinch.ConsoleApp;
class Program
{
    static void Main(string[] args)
    {
        ILoanProcessor processor = new LoanProcessor();
        ILoanPersister persister = new LoanPersister();

        //menu
        char keyPressed = '1';
        bool exit = false;

        
        while (exit == false)
        {
            Console.Clear();
            Console.WriteLine("--- BlackFinch Challenge ---");
            Console.WriteLine(string.Format(GetMenu(), persister.GetTotalNumberOfApplicants(), persister.GetTotalNumberOfSuccessfulApplicants(), persister.GetTotalNumberOfUnsuccessfulApplicants(),
                persister.GetTotalValueOfLoansWritten(), persister.GetMeanAverageOfLoanToValue()));
            keyPressed = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (keyPressed == '1')
            {
                GetLoanInfo(processor, persister);
            }
            else if (keyPressed == '2')
            {
                exit = true;
            }
        }
    }

    private static void GetLoanInfo(ILoanProcessor processor, ILoanPersister persister)
    {
        try
        {
            ConsoleValues values = new ConsoleValues();
            Console.WriteLine("""
            Enter Credit Score
            >
            """);
            values.CreditScore = Console.ReadLine();

            Console.WriteLine("""
            Loan Amount
            >
            """);
            values.LoanAmount = Console.ReadLine();

            Console.WriteLine("""
            Asset Value
            >
            """);
            values.AssetValue = Console.ReadLine();

            LoanApplicant applicant = new LoanApplicant(Guid.NewGuid(), int.Parse(values.CreditScore));
            Console.WriteLine(string.Format(
                """
            ApplicantId: {0}
            Applicant Credit Score: {1}
            """, applicant.Id, applicant.CreditScore));

            LoanApplication application = new LoanApplication(Guid.NewGuid(), decimal.Parse(values.LoanAmount), decimal.Parse(values.AssetValue),
                applicant);

            Console.WriteLine(string.Format("""
            Loan Amount: £{0}
            Asset Value: £{1}
            Loan To Value: {2}%
            """, application.LoanAmount, application.AssetValue, application.LTV));

            LoanApplicationResult result = processor.ProcessLoanApplication(application);
            persister.PersistLoanApplication(result);

            Console.WriteLine(string.Format("""
                The Application was {0}
                The Response was {1}
                """, (result.Successful ? "Successful" : "Unsuccessful"), result.ResultReason));

            Console.WriteLine("Press anything to return to main menu");
            Console.ReadKey();
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Caught exception: " + e.Message);
            Console.WriteLine("Press anything to return to main menu");
            Console.ReadKey();
            Console.WriteLine();
        }
        

    }

    private static string GetMenu()
    {
        //c# 7 new string notation
        return """
            There are currently {0} applications, {1} of these are successul and {2} of these are unsuccessful
            The total value of loans written to date is : £{3}
            The average Loan to Value is: {4}%

            Please select one of the following options:
            1) Enter new loan application
            2) Exit (or ctrl + c)

            >
            """;
    }

    
}

//new file scoping in c#7
file class ConsoleValues
{
    public string CreditScore="";
    public string LoanAmount="";
    public string AssetValue="";
}

