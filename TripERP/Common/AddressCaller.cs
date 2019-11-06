using System;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace TripERP.Common
{
    class AddressCaller
    {
        public static string CallingAddressInfo(string search_keyword, string search_page, string count_Per_Page)
        {
            string keyword = search_keyword;
            string currentPage = search_page;
            string countPerPage = count_Per_Page;
            
            string url_info = string.Format("http://www.juso.go.kr/addrlink/addrLinkApiJsonp.do?confmKey={0}&keyword={1}&resultType=json&currentPage={2}&countPerPage={3}", Global.confmKey, keyword, currentPage, countPerPage);

            WebRequest request = WebRequest.Create(url_info); // 호출할 url
            request.Method = "POST";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            //Console.WriteLine(responseFromServer); // response 출력
            //Console.WriteLine(responseFromServer.GetType()); // System.String type                      

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}