using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileCatalog.Data;
using System.IO;

namespace MobileCatalog
{
    public partial class App : Application
    {
        static CatalogDb catalogDb;

        public static CatalogDb CatalogDb
        {
            get
            {
                if(catalogDb == null)
                {
                    catalogDb = new CatalogDb(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CatalogDb.db"));
                }
                return catalogDb;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
