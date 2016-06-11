using ALFCConnect.Common;
using ALFCConnect.Data;
using ALFCConnect.ViewModels;
using System;
using System.Collections;
using Xamarin.Forms;
namespace ALFCConnect.Models
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
