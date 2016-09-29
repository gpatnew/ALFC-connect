using ALConnect.Interfaces;
using ALConnect.Models;
using System.Collections.Generic;

namespace ALConnect.Data
{
    class BibleVersions : IDataInfo
    {
        string bible = "21st Century King James Version, KJ21;American Standard Version, ASV;Amplified Bible, AMP;BRG Bible, BRG;Common English Bible, CEB;Complete Jewish Bible, CJB;Contemporary English Version, CEV;Darby Translation, DARBY;Disciples’ Literal New Testament, DLNT;Douay-Rheims 1899 American Edition, DRA;Easy-to-Read Version, ERV;English Standard Version, ESV;English Standard Version Anglicised, ESVUK;Expanded Bible, EXB;1599 Geneva Bible, GNV;GOD’S WORD Translation, GW;Good News Translation, GNT;Holman Christian Standard Bible, HCSB;International Children’s Bible, ICB;International Standard Version, ISV;J.B. Phillips New Testament, PHILLIPS;Jubilee Bible 2000, JUB;King James Version, KJV;Authorized King James Version, AKJV;Lexham English Bible, LEB;Living Bible, TLB;The Message, MSG;Modern English Version, MEV;Mounce Reverse-Interlinear New Testament, MOUNCE;Names of God Bible, NOG;New American Bible  Revised Edition, NABRE;New American Standard Bible, NASB;New Century Version, NCV;New English Translation, NET;New International Readers Version, NIRV;New International Version, NIV;New International Version - UK, NIVUK;New King James Version, NKJV;New Life Version, NLV;New Living Translation, NLT;New Revised Standard Version, NRSV;New Revised Standard Version, Anglicised, NRSVA;New Revised Standard Version, Anglicised Catholic Edition, NRSVACE;New Revised Standard Version Catholic Edition, NRSVCE;Orthodox Jewish Bible, OJB;Revised Standard Version, RSV;Revised Standard Version Catholic Edition, RSVCE;The Voice, VOICE;World English Bible, WEB;Worldwide English, New Testament;, WE;Wycliffe Bible, WYC;Youngs Literal Translation, YLT";
        string[] versions;
        List<IDataListItem> booklist;

        public BibleVersions()
        {
            versions = bible.Split(';');
            booklist = new List<IDataListItem>();
            BuildVersionList();
        }

        private void BuildVersionList()
        {
            for (int i = 0; i < versions.Length; i++)
            {
                string[] bookinfo = versions[i].Split(',');
                booklist.Add(new Bible { Id = i, Name = bookinfo[0], Value = bookinfo[1] });
            }
        }

        public List<IDataListItem> GetList()
        {
            return booklist;
        }
    }
}
