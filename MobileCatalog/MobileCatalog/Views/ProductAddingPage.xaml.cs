using MobileCatalog.Models;
using System;
using Xamarin.Forms;

namespace MobileCatalog.Views
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public partial class ProductAddingPage : ContentPage
    {
        public string ProductId
        {
            set
            {
                LoadProduct(value);
            }
        }

        public ProductAddingPage()
        {
            InitializeComponent();

            BindingContext = new Product();
        }

        private async void LoadProduct(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);
                Product product = await App.CatalogDb.GetProductAsync(id);
                BindingContext = product;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Product product = (Product)BindingContext;
            product.DateChanges = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(product.Model)
                && !string.IsNullOrWhiteSpace(product.Name) && !string.IsNullOrWhiteSpace(product.BarCode)
                && !string.IsNullOrWhiteSpace(product.Sort) && !string.IsNullOrWhiteSpace(product.Size)
                && !string.IsNullOrWhiteSpace(product.Color) && !string.IsNullOrWhiteSpace(product.Wight))
            {
                await App.CatalogDb.SaveProductAsync(product);
            }
            else
            {
                await DisplayAlert("Warning", "Wrong value", "OK");
            }
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            Product product = (Product)BindingContext;

            await App.CatalogDb.DeleteProductAsync(product);

            await Shell.Current.GoToAsync("..");
        }
    }
}