using ALConnect.Common;
using ALConnect.Data;
using ALConnect.ViewModels;
using System;
using System.Collections;
using Xamarin.Forms;
namespace ALConnect.Models
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
