using ALConnect.Common;
using ALConnect.Interfaces;
using ALConnect.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ALConnect.Views
{
    public partial class DropdownListPage : ContentPage
    {
        public DropdownListPage()
        {
            
        }

        public DropdownListPage(ExtendedButton currentButton, string listview, string limitValue = "")
        {
            var list = GetListView(listview, limitValue);
            Content = list;
            this.BackgroundColor = AppColors.White;
            list.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                IDataListItem selectItem = (IDataListItem)e.Item;
                currentButton.Text = !string.IsNullOrEmpty(selectItem.Name) ? selectItem.Name : "select";
                // currentButton.CommandParameter = selectItem.Value;
            };
        }

        private ListView GetListView(string listview, string currentValue)
        {
            ListView list = null;

            switch (listview)
            {
                case "BIBLEVERSIONS":
                    list = new BiblesListView(currentValue);
                    break;
                case "READINGPLANS":
                    //list = new ReadingPlanView(true);
                    break;


            }
            return list;
        }
    }
}
