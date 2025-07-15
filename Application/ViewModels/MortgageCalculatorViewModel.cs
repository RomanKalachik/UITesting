// MortgageCalculatorViewModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MortgageCalculator;

public sealed class MortgageCalculatorViewModel : INotifyPropertyChanged
{
    private decimal _principal = 300000m;
    private decimal _annualInterestRate = 4.5m;
    private int _loanTermYears = 30;
    private decimal _monthlyPayment;
    private List<PaymentDetail> _amortizationSchedule = new();
    private PaymentDetail? _selectedPaymentDetail;

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