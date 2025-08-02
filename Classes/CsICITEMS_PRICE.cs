using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    public class CsICITEMS_PRICE
    {
        public int TenentID { get; set; }
        public long MYPRODID { get; set; }
        public int UOM { get; set; }
        public int MyID { get; set; }
        public string UserProdID { get; set; }
        public Nullable<decimal> ORIGINALPRICE { get; set; }
        public Nullable<int> MAXDISCOUNT { get; set; }
        public Nullable<decimal> SPECIALSALE { get; set; }
        public string REFERENCE { get; set; }
        public Nullable<long> CRUP_ID { get; set; }
        public string LOCATIONID { get; set; }
        public Nullable<int> COMPANYID { get; set; }
        public Nullable<decimal> basecost { get; set; }
        public Nullable<decimal> onlinesale1 { get; set; }
        public Nullable<decimal> onlinesale2 { get; set; }
        public Nullable<decimal> msrp { get; set; }
        public Nullable<decimal> price { get; set; }
        public string currency { get; set; }
        public Nullable<decimal> onlinesale3 { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public string Uploadby { get; set; }
        public Nullable<System.DateTime> SyncDate { get; set; }
        public string Syncby { get; set; }
        public Nullable<int> SynID { get; set; }

    }

    public class CsICITEMS_PRICEList
    {
        public IList<CsICITEMS_PRICE> data { get; set; }
    }
    public class GetCsICITEMS_PRICE
    {
        public CsICITEMS_PRICE data { get; set; }
    }
}
