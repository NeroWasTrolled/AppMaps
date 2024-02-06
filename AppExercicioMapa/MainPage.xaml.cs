using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AppExercicioMapa
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void btnLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                var address = $"{txtLocal.Text}, {txtCidade.Text}, {txtUF.Text}, {txtPais.Text}";
                var locations = await Geocoding.GetLocationsAsync(address);

                if (locations != null && locations.Any())
                {
                    foreach (var location in locations)
                    {
                        var locationInfo = new Location(location.Latitude, location.Longitude);
                        var options = new MapLaunchOptions { Name = "Local Pesquisado" };
                        await Map.OpenAsync(locationInfo, options);
                    }
                }
                else
                {
                    await DisplayAlert("Erro", "Local não encontrado", "OK");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Falhou", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Falhou", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Falhou", ex.Message, "OK");
            }
        }
    }
}
