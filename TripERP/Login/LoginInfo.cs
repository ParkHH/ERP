using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripERP.Login
{
    class LoginInfo
    {
        //public string TABL_CNMB { get; set; }
        public string CMPN_NO { get; set; }
        public string CMPN_NM { get; set; }
        public string EMPL_NO { get; set; }
        public string EMPL_NM { get; set; }
        public string DEPT_NM { get; set; }
        public string PSTN_NM { get; set; }
        public string ACNT_ID { get; set; }
        public string ACNT_PW { get; set; }
        public string CNNC_PRMI_YN { get; set; }
        public string HLOF_DVSN_CD { get; set; }
        public string SCRN_SORT_ORD { get; set; }
        public string MAC { get; set; }
        //public string FRST_RGST_DTM { get; set; }
        //public string FRST_RGTR_ID { get; set; }
        //public string FINL_MDFC_DTM { get; set; }
        //public string FINL_MDFR_ID { get; set; }

        public DateTime loginTime { get; set; }
    }
}
