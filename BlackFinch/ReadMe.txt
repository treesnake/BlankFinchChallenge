Hello!

Made notes about my solution to the challenge in here, this includes assumptions and limitations as well as a general explanation of the solution. I've also added feedback at the bottom, if you would like any. 

-- -- --

General Overview

I've decoupled the code as much as possible to allow for testing and reuse. For example BlackFinch.Models could be moved out to a NugetPackage and the models could then be shared through different projects.

I've fake persisted the previous application loan applications, in reality the persistance could be carried out by any number of means using the relevant interface (IBlackFinchPersist) which would then use the persistance method of choice (Entity Framework, MongoDB, etc)

I went outside the timebox roughly when I started the persistance part, I kept going to deliver a console app that would run. My next steps would have been to start writing the tests, particularly on the Business Logic as there's a lot of boundaries that need to be enforced.

-- -- --

Assumptions

+ Decimal values are rounded down (ie £56.7783201 becomes £56.77) - this was the rule on a financial project I worked on previously.

+ Credit Score is a whole number (i.e. Not a decimal like 1.99 or 998.302)

+ Credit Score boundary values are inclusive (between 1 and 999 includes 1 and 999)

+ Loan Boundary Values are inclusive (between £100,000 and £1,500,000)

+ Asset Value is stored per application, as customer may choose different assets per application

+ Asset Value must be > 0 but values that produce an LTV below the minumum threshold produce unsuccessful applications rather than exceptions

+ Credit Scores below 750 never get approved

-- -- --

Limitations

+ No custom exceptions

+ No graceful error handling

+ No error logging

+ Business

+ Asset specifics not stored 

-- -- --

Developer Notes

+ Separated Interfaces for Business Logic and Persistance as these often get split when moved into NuGet Packages

+ I prefer to use braces on one line if statements

    I.E.
    if(1 == 1)
    {
        do something
    }

    rather than

    if(1 == 1)
        do something

-- -- --

Feedback

I really like this tech test - it feels topical and not too time consuming.

It might be useful to add a definition of Credit Score (to establish higher is better and lower is worse)
