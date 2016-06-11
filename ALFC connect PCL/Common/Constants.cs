

using Xamarin.Forms;

namespace ALFCConnect.Common
{
    public class Constants
    {
        public const string BaseUrl = "http://www.alfc.us/";
        public const string EventsPath = "events/feed/?post_type=slide";
        public const string FeaturedPath = "feed/?post_type=slide&type=featured";
        public const string SlidesPath = "feed/?post_type=slide";
        public const string SermonsPath = "sermons/";

        public const string SearchURLbase = "http://www.biblegateway.com/";
        public const string BibleGatewayURL = "http://www.biblegateway.com/";
        public const string DonateUrl = "https://alfc.ccbchurch.com/w_give_online.php";
        public const string MinistriesUrl = "ministry";
        public const string NewsUrl = "http://alfc.us/";
        public const string PrayersUrl = "ministry/prayer#request";
        public const string GroupsUrl = "connect/finder";
        //Settings //
        public const string UserEmail = "";
        public const string UserFirstName = "firstname";
        public const string UserLastName = "lastname";
        public const string UserPhone = "phone";
        public const string BibleVersion = "NKJV";
        public const string BibleVersionName = "New King James";

        public static Color FontColor = Color.Black;
        public static Color BackgroundColor = Color.White;
        // Settings Keys //
        public  const string BibleVersionKey = "bibleVersion";
        public  const string BibleVersionNameKey = "bibleName";
        public  const string UserNameKey = "alfc_user";
        public  const string UserIntKey = "alfc_int";
    }
}
