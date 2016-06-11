using System;
using System.Collections;
using Xamarin.Forms;
using ALFC_SOAP.Data;
using ALFC_SOAP.Model;
using ALFC_SOAP.Common;
namespace ALFC_SOAP
{
    public class BookListView : ListView
    {

        BibleDataInfo db;
        public BookListView()
        {
             db = new BibleDataInfo();
            this.ItemsSource = db.GetList();
            this.ItemTemplate = BaseListItemTemplate.GetColorLabel(AppColors.BGBlue, AppColors.BGPurple);
        }

     
    }
}
