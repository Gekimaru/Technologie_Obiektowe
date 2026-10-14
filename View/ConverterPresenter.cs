using CurrencyConverter.Model;
using CurrencyConverter.Services;

namespace CurrencyConverter.View;

public class ConverterPresenter
{
    // dałem sobie C od control
    private ComboBox _sourceCurrencyC;
    private ComboBox _targetCurrencyC;
    private TextBox _amountCurrencyC;
    private Button _submitButtonC;
    private Label _resultLabel;
    private CurrencyService _currencyService;

    public ConverterPresenter(CurrencyService currencyService, ComboBox sourceCurrency, ComboBox targetCurrency,
                              TextBox amountCurrency, Button submitButton, Label resultLabel)
    {
        _currencyService = currencyService;
        _sourceCurrencyC = sourceCurrency;
        _targetCurrencyC = targetCurrency;
        _amountCurrencyC = amountCurrency;
        _submitButtonC = submitButton;
        _resultLabel = resultLabel;

        _submitButtonC.Click += Exchange;
    }

    public void Exchange(object? sender, EventArgs e)
    {
        ICurrency? sourceCurrency = _sourceCurrencyC.SelectedItem as ICurrency;
        ICurrency? targetCurrency = _targetCurrencyC.SelectedItem as ICurrency;

        Result<decimal> result = _currencyService.Convert(sourceCurrency, targetCurrency, _amountCurrencyC.Text);
        if (!result.IsSuccess)
        {
            ShowError(result.Error);
            return;
        }

        ShowInfo($"{_amountCurrencyC.Text.Trim()} {sourceCurrency!.Code} = {result.Value:N5} {targetCurrency!.Code}");
    }

    public void Refresh(ICurrencyCollection currencyCollection)
    {
        _sourceCurrencyC.Items.Clear();
        _targetCurrencyC.Items.Clear();

        ICurrency[] currencies = currencyCollection.GetAll();

        for (int i = 0; i < currencies.Length; i++)
        {
            _sourceCurrencyC.Items.Add(currencies[i]);
            _targetCurrencyC.Items.Add(currencies[i]);
        }

        _sourceCurrencyC.SelectedItem = currencyCollection.Get("EUR") ?? currencies[0];
        _targetCurrencyC.SelectedItem = currencyCollection.Get("PLN") ?? currencies[0];

        ShowInfo("Wpisz kwotę i kliknij w przycisk!");
    }

    public void ShowError(string message)
    {
        _resultLabel.ForeColor = Color.Firebrick;
        _resultLabel.Text = message;
    }

    private void ShowInfo(string message)
    {
        _resultLabel.ForeColor = SystemColors.ControlText;
        _resultLabel.Text = message;
    }
}
