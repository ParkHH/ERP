using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripERP.Login;

namespace TripERP.Common
{
    class Global
    {
        // Login Info
        static public LoginInfo loginInfo = null;

        // juso.go.kr Key값
        public static string confmKey = "U01TX0FVVEgyMDE5MDgxMzE0MjIzNjEwODk0NjQ=";

        // Common Code Map
        //static public Dictionary<string, Dictionary<string, CommonCodeItem>> allCommonCodeMap = null; 
        static public Dictionary<string, List<CommonCodeItem>> allCommonCodeMap = null;        

        // 예약상태
        public enum eReservationStatus {  notYetDecided = 1,  canceled = 9 };
        // 진행상태
        public enum eCutOffStatus {  confirmed = 5 };
        // 발송 미디어 코드
        public enum eSendMediaCode {  sms = 1, email, fax, post, manual = 6 }; 
        // 입출금 코드 (입금:1, 출금: 2)
        public enum eDepositCode { deposit = 1, withdraw = 2}
        // 입금 유형 (선금, 예약금, 중도금, 잔금)
        public enum eDepositType {  downPayment = 10, reservationPrice, partPrice, balanceAmount } 


        static public TripERPmain mainForm = null;

        static public string SAVEMODE_UPDATE = "UPDATE";
        static public string SAVEMODE_ADD = "ADD";

        static public bool LoadCommonCodeInfo()
        {
            if(allCommonCodeMap != null)
            {
                allCommonCodeMap.Clear();
            }
            else
            {
                allCommonCodeMap = new Dictionary<string, List<CommonCodeItem>>();
            }

            string query = string.Format("CALL SelectAllCommonCodeList ()");

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                return false; 
            }

            List<CommonCodeItem> commonCodeList = null;
            string currentName = ""; 
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string DOMN_ENG_NM = dataRow["DOMN_ENG_NM"].ToString();
                string CD_VLID_VAL = dataRow["CD_VLID_VAL"].ToString();
                string SCRN_SORT_ORD = dataRow["SCRN_SORT_ORD"].ToString();
                string CD_VLID_VAL_DESC = dataRow["CD_VLID_VAL_DESC"].ToString();

                if(currentName != DOMN_ENG_NM)
                {
                    if(currentName != "")
                    {
                        allCommonCodeMap.Add(currentName, commonCodeList);
                    }

                    commonCodeList = new List<CommonCodeItem>();
                    currentName = DOMN_ENG_NM;
                }

                CommonCodeItem item = new CommonCodeItem(DOMN_ENG_NM, CD_VLID_VAL, Int32.Parse(SCRN_SORT_ORD), CD_VLID_VAL_DESC);
                commonCodeList.Add(item);
            }

            return true; 
        }

        static public string GetCommonCodeDesc(string name, string value)
        {
            if (name == "" || value == "")
                return "";

            if (allCommonCodeMap.ContainsKey(name) == false)
                return "";

            List<CommonCodeItem> commonCodList = allCommonCodeMap[name];
            if (commonCodList != null)
            {
                for (int i = 0; i < commonCodList.Count; i++)
                {
                    if (commonCodList[i].Value.ToString() == value)
                        return commonCodList[i].Desc;
                }
            }

            return ""; 
        }

        static public List<CommonCodeItem> GetCommonCodeList(string name)
        {
            if (allCommonCodeMap.ContainsKey(name) == true)
                return allCommonCodeMap[name];
            else
                return new List<CommonCodeItem>();
        }


       
    }
}
