
using uitesting;

namespace MortgageCalculatorTests;

public class MortgageCalculatorTests
{
    [Theory]
    [InlineData(100000, 5, 30, 536.82)]
    [InlineData(200000, 3.5, 30, 898.09)]
    [InlineData(350000, 4.25, 15, 2632.97)]
    [InlineData(500000, 6, 30, 2997.75)]
    public void CalculateMonthlyPayment_ValidInputs_ReturnsCorrectPayment(
        decimal principal,
        decimal annualInterestRate,
        int loanTermYears,
        decimal expectedPayment)
    {
        var payment = MortgageCalculator.CalculateMonthlyPayment(principal, annualInterestRate, loanTermYears);
        Assert.Equal(expectedPayment, payment);
    }

    [Fact]
    public void CalculateMonthlyPayment_ZeroInterest_ReturnsSimplePayment()
    {
        var payment = MortgageCalculator.CalculateMonthlyPayment(120000, 0, 10);
        Assert.Equal(1000, payment);
    }

    [Theory]
    [InlineData(0, 5, 30, "Principal must be positive")]
    [InlineData(-100000, 5, 30, "Principal must be positive")]
    [InlineData(100000, -5, 30, "Interest rate cannot be negative")]
    [InlineData(100000, 5, 0, "Loan term must be positive")]
    public void CalculateMonthlyPayment_InvalidInputs_ThrowsException(
        decimal principal,
        decimal annualInterestRate,
        int loanTermYears,
        string expectedMessage)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            MortgageCalculator.CalculateMonthlyPayment(principal, annualInterestRate, loanTermYears));

        Assert.Contains(expectedMessage, ex.Message);
    }

    [Fact]
    public void GenerateAmortizationSchedule_ValidInputs_ReturnsCorrectSchedule()
    {
        // Test a simple 2-year loan of $1000 at 10% interest
        var schedule = MortgageCalculator.GenerateAmortizationSchedule(1000, 10, 2);

        // Should have 24 payments
        Assert.Equal(24, schedule.Count);

        // Check first payment
        var firstPayment = schedule[0];
        Assert.Equal(1, firstPayment.MonthNumber);
        Assert.Equal(46.14M, firstPayment.PaymentAmount);
        Assert.Equal(37.81M, firstPayment.PrincipalAmount,1);
        Assert.Equal(8.33M, firstPayment.InterestAmount,2);
        Assert.Equal(962.19M, firstPayment.RemainingBalance,2);

        // Check last payment
        var lastPayment = schedule[23];
        Assert.Equal(24, lastPayment.MonthNumber);
        Assert.Equal(46.14M, lastPayment.PaymentAmount,2);
        Assert.Equal(45.89M, lastPayment.PrincipalAmount,2);
        Assert.Equal(0.25M, lastPayment.InterestAmount,2);
        Assert.Equal(0, lastPayment.RemainingBalance,3);
    }

    [Fact]
    public void GenerateAmortizationSchedule_PrincipalShouldBeZeroAtEnd()
    {
        var schedule = MortgageCalculator.GenerateAmortizationSchedule(200000, (decimal)3.5, 30);
        var lastPayment = schedule.Last();

        Assert.Equal(0, lastPayment.RemainingBalance);
        Assert.True(lastPayment.PrincipalAmount > 0);
    }

    [Fact]
    public void GenerateAmortizationSchedule_TotalPaymentsShouldMatch()
    {
        var principal = 300000M;
        var rate = 4M;
        var term = 20;

        var schedule = MortgageCalculator.GenerateAmortizationSchedule(principal, rate, term);
        var monthlyPayment = MortgageCalculator.CalculateMonthlyPayment(principal, rate, term);

        // Sum of all principal payments should equal original principal
        var totalPrincipalPaid = schedule.Sum(p => p.PrincipalAmount);
        Assert.Equal(principal, totalPrincipalPaid,1);

        // Sum of all payments should equal monthly payment * number of months
        var totalPaid = schedule.Sum(p => p.PaymentAmount);
        Assert.Equal(monthlyPayment * term * 12, totalPaid);
    }
}