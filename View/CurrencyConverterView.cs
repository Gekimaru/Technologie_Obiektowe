using CurrencyConverter.Model;
using CurrencyConverter.Services;

namespace CurrencyConverter.View
{
    public partial class CurrencyConverterView : Form
    {
        private CurrencyService _currencyService;
        private ConverterPresenter _defaultView;

        public CurrencyConverterView(CurrencyService currencyService)
        {
            InitializeComponent();

            _currencyService = currencyService;
            _defaultView = new ConverterPresenter(_currencyService ,sourceComboBox, targetComboBox, amountTextBox, convertButton, resultLabel);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var result = await _currencyService.LoadAsync();
            if (!result.IsSuccess)
            {
                ShowError(result.Error);
                return;
            }

            ICurrencyCollection currencies = result.Value;

            _defaultView.Refresh(currencies);
        }

        private void ShowError(string message)
        {
            resultLabel.ForeColor = Color.Firebrick;
            resultLabel.Text = message;
        }
    }
}
