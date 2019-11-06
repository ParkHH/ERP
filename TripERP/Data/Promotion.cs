using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripERP.Data
{
    class Promotion
    {
        public string PRDT_PRMO_CD { get; set; }
        public string PRMO_NM { get; set; }
        public string PRDT_CNMB { get; set; }
        public string PRDT_NM { get; set; }
        public string ADLT_PRMO_DSCT_RT { get; set; }
        public string CHLD_PRMO_DSCT_RT { get; set; }
        public string INFN_PRMO_DSCT_RT { get; set; }
        public string ADLT_PRMO_DSCT_AMT { get; set; }
        public string CHLD_PRMO_DSCT_AMT { get; set; }
        public string INFN_PRMO_DSCT_AMT { get; set; }
        public string APLY_LNCH_DT { get; set; }
        public string APLY_END_DT { get; set; }
        public string USE_YN { get; set; }
        
    }
}
