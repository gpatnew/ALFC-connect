
using Amazon;
using System.Net;
using Xamarin.Forms;

namespace ALConnect.Common
{
    public class Constants
    {
        public const string BaseUrl = "https://www.alfc.us/";
        public const string DevBaseUrl = "http://www.alfc.us/";
        public const string EventsPath = "events/feed/?post_type=slide";
        public const string SlidesPath = "feed/?post_type=slide";
        public const string SermonsPath = "sermons/feed";

        public const string SearchURLbase = "http://www.biblegateway.com/";
        public const string BibleGatewayURL = "http://www.biblegateway.com/";
        public const string DonateUrl = "https://alfc.ccbchurch.com/w_give_online.php";
        public const string MinistriesUrl = "ministry/missions/";
        public const string NewsUrl = "http://alfc.us/";
        public const string PrayerIOS = "ministry/prayer/";
        public const string PrayersUrl = "https://alfc.ccbchurch.com/form_response.php?id=90";
        public const string GroupsUrl = "connect/finder";
        //Settings //
        //MobileAppUser
        //AKIAI5LZYLJNAGONYSJQ
        //FcYW945njnfmEpUkYownTx51HRLky1NK6lUrbD5F
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

        public const string AWSAccountId = "";
        public const string Bucket = "mobileapp.alfc.us";
        public const string S3Path = "https://s3-us-west-2.amazonaws.com/";
        public const string Featured = "feature";
        public const string FeaturePath = "featureitem";
        public const string Message = "weekly";
        public const string MessagePath = "weeklymessage";
        public const string CognitoPoolId = "us-west-2:a9473368-6eb5-4366-903a-5279d3a8fe51";
        public const string SQSSermonQueue = "https://sqs.us-west-1.amazonaws.com/650481127744/MyQueue";
        public static RegionEndpoint REGION = RegionEndpoint.USWest2;

        public const HttpStatusCode NO_SUCH_BUCKET_STATUS_CODE = HttpStatusCode.NotFound;
        public const HttpStatusCode BUCKET_ACCESS_FORBIDDEN_STATUS_CODE = HttpStatusCode.Forbidden;
        public const HttpStatusCode BUCKET_REDIRECT_STATUS_CODE = HttpStatusCode.Redirect;
    }
}
