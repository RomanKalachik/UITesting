using Eremex.AvaloniaUI.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MortgageCalculator.ViewModels;

public sealed class MortgageCalculatorViewModel : INotifyPropertyChanged
{
    private decimal _principal = 300000m;
    private decimal _annualInterestRate = 4.5m;
    private int _loanTermYears = 30;
    private decimal _monthlyPayment;
    private List<PaymentDetail> _amortizationSchedule = new();
    private PaymentDetail? _selectedPaymentDetail;

    SortedNumericDataAdapter? principialSeriesDataAdapter, interestSeriesDataAdapter, ballanceSeriesDataAdapter;

    public SortedNumericDataAdapter? PrincipialSeriesDataAdapter
    {
        get => principialSeriesDataAdapter;
        set => SetField(ref principialSeriesDataAdapter, value);
    }

    public SortedNumericDataAdapter? InterestSeriesDataAdapter
    {
        get => interestSeriesDataAdapter;
        set => SetField(ref interestSeriesDataAdapter, value);
    }

    //public SortedNumericDataAdapter? BallanceSeriesDataAdapter
    //{
    //    get => ballanceSeriesDataAdapter;
    //    set => SetField(ref ballanceSeriesDataAdapter, value);
    //}

    public event PropertyChangedEventHandler? PropertyChanged;

    public decimal Principal
    {
        get => _principal;
        set
        {
            if (SetField(ref _principal, value))
            {
                CalculateMortgage();
            }
        }
    }

    public decimal AnnualInterestRate
    {
        get => _annualInterestRate;
        set
        {
            if (SetField(ref _annualInterestRate, value))
            {
                CalculateMortgage();
            }
        }
    }

    public int LoanTermYears
    {
        get => _loanTermYears;
        set
        {
            if (SetField(ref _loanTermYears, value))
            {
                CalculateMortgage();
            }
        }
    }

    public decimal MonthlyPayment
    {
        get => _monthlyPayment;
        private set => SetField(ref _monthlyPayment, value);
    }

    public List<PaymentDetail> AmortizationSchedule
    {
        get => _amortizationSchedule;
        private set => SetField(ref _amortizationSchedule, value);
    }

    public PaymentDetail? SelectedPaymentDetail
    {
        get => _selectedPaymentDetail;
        set => SetField(ref _selectedPaymentDetail, value);
    }

    public MortgageCalculatorViewModel()
    {
        CalculateMortgage();
    }

    private void CalculateMortgage()
    {
        try
        {
            MonthlyPayment = SampleMortgageCalculator.CalculateMonthlyPayment(
                Principal,
                AnnualInterestRate,
                LoanTermYears);

            AmortizationSchedule = SampleMortgageCalculator.GenerateAmortizationSchedule(
                Principal,
                AnnualInterestRate,
                LoanTermYears);
        }
        catch (ArgumentOutOfRangeException)
        {
            // Reset to default values if invalid input
            MonthlyPayment = 0;
            AmortizationSchedule = new List<PaymentDetail>();
        }
        var argumentList = AmortizationSchedule.Select(s => (double)s.MonthNumber).ToList();
        InterestSeriesDataAdapter = new SortedNumericDataAdapter(argumentList, AmortizationSchedule.Select(s => (double)s.InterestAmount).ToList());
        PrincipialSeriesDataAdapter = new SortedNumericDataAdapter(argumentList, AmortizationSchedule.Select(s => (double)s.PrincipalAmount).ToList());
        
        //BallanceSeriesDataAdapter = new SortedNumericDataAdapter(argumentList, AmortizationSchedule.Select(s => (double)s.RemainingBalance/100).ToList());

    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}