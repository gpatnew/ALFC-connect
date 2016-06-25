using ALFCconnect.Common;
using ALFCconnect.Data;
using ALFCconnect.ViewModels;
using System;
using System.Collections;
using Xamarin.Forms;
namespace ALFCconnect.Models
{
    public class BookListView : ListView
    {

        BibleDataInfo db;
        public BookListView()
        {
             db = new BibleDataInfo();
            this.ItemsSource = db.GetList();
            this.ItemTemplate = BaseListItemTemplate.GetColorLabel(AppColors.ALFCBGBlue, AppColors.ALFCBGPurple);
        }

     
    }
}
