using ALFCConnect.Data;
using ALFCConnect.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ALFCConnect.ViewModels
{
    public class BiblesListView : ListView
    {
        private BibleVersions db = new BibleVersions();

        public BiblesListView(string currentValue)
        {
            List<IDataListItem> items = db.GetList();
            this.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.VerticalOptions = LayoutOptions.FillAndExpand;
            this.ItemsSource = items;
            this.ItemTemplate = BaseListItemTemplate.Get(currentValue);
            this.ItemSelected += BiblesListView_ItemSelected;
        }

        void BiblesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
