using CurrencyConverter.Model;
using CurrencyConverter.Services;

namespace CurrencyConverter.View
{
    public partial class CurrencyConverterView : Form
    {
        private CurrencyService _currencyService;
        private ConverterPresenter _presenter;

        public CurrencyConverterView(CurrencyService currencyService)
        {
            InitializeComponent();

            _currencyService = currencyService;
            _presenter = new ConverterPresenter(_currencyService, sourceComboBox, targetComboBox, amountTextBox, convertButton, resultLabel);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Result<ICurrencyCollection> result = await _currencyService.LoadAsync();
            if (!result.IsSuccess)
            {
                _presenter.ShowError(result.Error);
                return;
            }

            _presenter.Refresh(result.Value);
        }
    }
}
