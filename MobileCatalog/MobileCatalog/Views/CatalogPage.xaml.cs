using MobileCatalog.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace MobileCatalog.Views
{
    public partial class CatalogPage : ContentPage
    {
        public CatalogPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.CatalogDb.GetProductsAsync();

            base.OnAppearing();
        }
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ProductAddingPage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Product product = (Product)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(ProductAddingPage)}?{nameof(ProductAddingPage.ProductId)}={product.Id}");
            }


        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (picker.SelectedIndex)
            {
                case 0:
                    collectionView.ItemsSource = await App.CatalogDb.SearchProductByCodeAsync(e.NewTextValue);
                    break;
                case 1:
                    collectionView.ItemsSource = await App.CatalogDb.SearchProductByNameAsync(e.NewTextValue.ToLower());
                    break;
                case 2:
                    collectionView.ItemsSource = await App.CatalogDb.SearchProductByBarCodeAsync(e.NewTextValue.ToLower());
                    break;
                case 3:
                    collectionView.ItemsSource = await App.CatalogDb.SearchProductByPriceAsync(e.NewTextValue);
                    break;
                default:
                    break;

            }
        }
    }
}