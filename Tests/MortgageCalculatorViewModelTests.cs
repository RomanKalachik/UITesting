// MortgageCalculatorViewModelTests.cs
using Xunit;
using System.ComponentModel;
using MortgageCalculator.ViewModels;

namespace MortgageCalculatorTests;

public class MortgageCalculatorViewModelTests
{
    [Fact]
    public void Constructor_InitializesWithDefaultValues()
    {
        var vm = new MortgageCalculatorViewModel();

        Assert.Equal(300000m, vm.Principal);
        Assert.Equal(4.5m, vm.AnnualInterestRate);
        Assert.Equal(30, vm.LoanTermYears);
        Assert.True(vm.MonthlyPayment > 0);
        Assert.NotEmpty(vm.AmortizationSchedule);
    }

    [Fact]
    public void PropertyChanged_RaisedWhenPrincipalChanges()
    {
        var vm = new MortgageCalculatorViewModel();
        var raised = false;
        vm.PropertyChanged += (s, e) => raised |= e.PropertyName == nameof(vm.Principal);

        vm.Principal = 400000m;

        Assert.True(raised);
    }

    [Fact]
    public void MonthlyPayment_RecalculatedWhenPrincipalChanges()
    {
        var vm = new MortgageCalculatorViewModel();
        var originalPayment = vm.MonthlyPayment;

        vm.Principal = 400000m;

        Assert.NotEqual(originalPayment, vm.MonthlyPayment);
        Assert.True(vm.MonthlyPayment > originalPayment);
    }

    [Fact]
    public void AmortizationSchedule_RecalculatedWhenInterestRateChanges()
    {
        var vm = new MortgageCalculatorViewModel();
        var originalSchedule = vm.AmortizationSchedule;

        vm.AnnualInterestRate = 5.0m;

        Assert.NotSame(originalSchedule, vm.AmortizationSchedule);
        Assert.Equal(vm.LoanTermYears * 12, vm.AmortizationSchedule.Count);
    }

    [Fact]
    public void InvalidInput_ResetsCalculatedValues()
    {
        var vm = new MortgageCalculatorViewModel();
        var originalPayment = vm.MonthlyPayment;

        vm.Principal = -1000m; // Invalid value

        Assert.Equal(0, vm.MonthlyPayment);
        Assert.Empty(vm.AmortizationSchedule);
    }

    [Fact]
    public void SelectedPaymentDetail_RaisesPropertyChanged()
    {
        var vm = new MortgageCalculatorViewModel();
        var raised = false;
        vm.PropertyChanged += (s, e) => raised = e.PropertyName == nameof(vm.SelectedPaymentDetail);

        vm.SelectedPaymentDetail = vm.AmortizationSchedule[0];

        Assert.True(raised);
        Assert.NotNull(vm.SelectedPaymentDetail);
    }
}