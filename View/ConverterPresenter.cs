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

    public void Exchange(object? sender, EventArgs e)
    {
        ICurrency? sourceCurrency = _sourceCurrencyC.SelectedItem as ICurrency;
        ICurrency? targetCurrency = _targetCurrencyC.SelectedItem as ICurrency;

        Result<decimal> result = _currencyService.Convert(sourceCurrency, targetCurrency, _amountCurrencyC.Text);
        if (!result.IsSuccess)
        {
            _resultLabel.Text = result.Error;
            return;
        }

        _resultLabel.Text = $"{_amountCurrencyC.Text.Trim()} {sourceCurrency!.Code} = {result.Value:N5} {targetCurrency!.Code}";
    }

    public ConverterPresenter(CurrencyService currencyService, ComboBox sourceCurrency, ComboBox targetCurrency,
                       TextBox amountCurrency, Button submitButton, Label resultLabel)
    {
        this._sourceCurrencyC = sourceCurrency;
        this._targetCurrencyC = targetCurrency;
        this._amountCurrencyC = amountCurrency;
        this._submitButtonC = submitButton;
        this._resultLabel = resultLabel;
        this._currencyService = currencyService;

        this._submitButtonC.Click += Exchange;


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

        _resultLabel.Text = "Wpisz kwote i kliknij w przycisk!";
    }
}
