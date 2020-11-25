using Umbraco.Headless.Client.Net.Delivery;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Umbraco.Headless.Client.Samples.Xamarin.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string projectAlias = "";
            string apiKey = null;

            var deliveryService = new ContentDeliveryService(projectAlias, apiKey);

            MainPage = new NavigationPage(new MainPage(deliveryService));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
