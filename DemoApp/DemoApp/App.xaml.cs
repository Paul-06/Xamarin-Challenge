using DemoApp.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp
{
    public partial class App : Application
    {

        public static DatabaseContext Context { get; set; }

        public static MasterDetailPage MasterDet { get; set; }

        public App()
        {
            InitializeComponent();
            InitializeDatabase();
            MainPage = new MainPage();
        }

        private void InitializeDatabase()
        {
            var folderApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = System.IO.Path.Combine(folderApp, "Notes.db3");
            Context = new DatabaseContext(dbPath);
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
