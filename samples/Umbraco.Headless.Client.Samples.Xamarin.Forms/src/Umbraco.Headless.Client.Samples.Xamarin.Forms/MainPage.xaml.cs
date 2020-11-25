using System;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Xamarin.Forms;

namespace Umbraco.Headless.Client.Samples.Xamarin.Forms
{
    public partial class MainPage : ContentPage
    {
        private readonly ContentDeliveryService _deliveryService;
        private readonly Content _content;

        public MainPage(ContentDeliveryService deliveryService, Content content = null)
        {
            _deliveryService = deliveryService ?? throw new ArgumentNullException(nameof(deliveryService));
            _content = content;

            InitializeComponent();
            ListView.RefreshCommand = new Command(async () => await Refresh());

            Refresh().ConfigureAwait(false);
        }

        private async Task Refresh()
        {
            ListView.IsRefreshing = true;

            await LoadData().ConfigureAwait(false);
        }

        private async Task LoadData()
        {
            if (_content == null)
            {
                var result = await _deliveryService.Content.GetRoot();
                ListView.ItemsSource = result;
            }
            else
            {
                NavigationPage.SetTitleView(this, new Label
                {
                    Text = _content.Name,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                });

                var result = await _deliveryService.Content.GetChildren(_content.Id, pageSize: 50);
                ListView.ItemsSource = result.Content?.Items;
            }

            ListView.IsRefreshing = false;
        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new MainPage(_deliveryService, e.Item as Content));
        }
    }
}
