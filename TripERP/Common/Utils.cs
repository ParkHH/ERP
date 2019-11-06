using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripERP.Common
{
    class Utils
    {

        /**
      * ComboBox 컨트롤에서 선택된 값을 반환
      */
        static public string GetSelectedComboBoxItemValue(ComboBox comboBox, string type = "s")
        {
            string value = "";
            if (comboBox.SelectedItem == null)
            {
                value = type == "s" ? "" : "0";
            }
            else
            {
                value = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();
            }

            return value;
        }

        /**
     * ComboBox 컨트롤에서 선택된 텍스트를 반환
     */
        static public string GetSelectedComboBoxItemText(ComboBox comboBox, string type = "s")
        {
            string text = "";
            if (comboBox.SelectedItem == null)
            {
                text = type == "s" ? "" : "0";
            }
            else
            {
                text = (comboBox.SelectedItem as ComboBoxItem).Text;
            }

            return text;
        }

        /**
        * 실수만 허용 
        */
        static public void AcceptOnlyRealNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        /**
         * 0~9까지의 숫자만 허용
         */
        static public void AcceptOnlyDigit(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        static public DateTime GetDateTimeFormatFromString(string date, string format = "yyyy-MM-dd")
        {
            if (format == "yyyy-MM-dd")
            {
                if(date.Length > 10)
                    date = date.Substring(0, 10);

                string[] dateArray = date.Split('-');
                if (dateArray.Length >= 3)
                {
                    return new DateTime(
                        Int32.Parse(dateArray[0]),
                        Int32.Parse(dateArray[1]),
                        Int32.Parse(dateArray[2]));
                }
                else
                {
                    return DateTime.Now;
                }
            }
            else
            {
                return DateTime.Now;
            }
        }

        static public DateTime GetDateTimeFormatFromObject(object date, string format = "yyyy-MM-dd")
        {
            if (format == "yyyy-MM-dd")
            {
                if (date == null)
                    return DateTime.Now;

                return GetDateTimeFormatFromString(date.ToString());
            }
            else
            {
                return DateTime.Now;
            }
        }

        static public void SelectComboBoxItemByValue(ComboBox comboBox, string value)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if ((comboBox.Items[i] as ComboBoxItem).Value.ToString() == value)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }
        static public void SelectComboBoxItemByText(ComboBox comboBox, string value)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if ((comboBox.Items[i] as ComboBoxItem).Text == value)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        /**
       * 세자리마다 콤마 삽입
       * TextBox의 객체를 파리미터로 받아서 TextBox의 값 자체를 수정한다. 
       * 콤마 추가, 삭제에 따른 SelectionStart의 값을 조율했다. 
       */
        static public void SetComma(object sender)
        {
            System.Windows.Forms.TextBox textBox = (sender as TextBox);
            if (textBox.Text.Trim() != "")
            {
                int textLength = textBox.TextLength;
                int selectionStart = textBox.SelectionStart;
                int selectionLength = textBox.SelectionLength;

                string digit = GetDigitString(textBox.Text);
                Int64 i = 0;
                if (Int64.TryParse(digit, out i) == false)
                    return;

                textBox.Text = string.Format("{0:N0}", Convert.ToInt64(digit));
                if (selectionStart != 0)
                {
                    if (textLength < textBox.TextLength)
                        selectionStart++;
                    else if (textLength > textBox.TextLength)
                        selectionStart--;
                }
                textBox.SelectionStart = selectionStart;
                textBox.SelectionLength = 0;
            }
        }



        // 값 가져와 계산해야할 경우 ',' 없애기      --> 박현호
        //==================================================================================================================
        public static string removeComma(string value)
        {
            string[] valueStrArr = value.Split(',');
            string newValue = "";

            for (int i = 0; i < valueStrArr.Length; i++)
            {
                newValue += valueStrArr[i];
            }

            return newValue;
        }
        //==================================================================================================================




        /**
         * 세자리마다 콤마 삽입
         */
        static public string SetComma(string value)
        {
            if (value.Trim() == "")
                return value;

            string digit = GetDigitString(value);
            Int64 i = 0;
            if (Int64.TryParse(digit, out i) == false)
                return value;

            return string.Format("{0:N0}", Convert.ToInt64(digit));
        }

        /**
        * 세자리마다 콤마 삽입
        */
        static public string SetComma(long longValue)
        {
            string value = longValue.ToString();
            if (value.Trim() == "")
                return value;

            //string digit = value.Replace(",", "");
            string digit = GetDigitString(value);
            Int64 i = 0;
            if (Int64.TryParse(digit, out i) == false)
                return value;

            return string.Format("{0:N0}", Convert.ToInt64(digit));
        }

        /**
       * 세자리마다 콤마 삽입
       */
        static public string SetComma(double doubleValue, int precision = 2)
        {
            string value = doubleValue.ToString();
            if (value.Trim() == "")
                return value;

            //string digit = value.Replace(",", "");
            string digit = GetDoubleString(value);
            Double i = 0.0d;
            if (Double.TryParse(digit, out i) == false)
                return value;

            return string.Format("{0:N" + precision.ToString() + "}", Convert.ToDouble(digit));
        }

        /**
         * 주어진 문자열에서 0-9 까지의 숫자와 소수점만 가져온다.
         * 음수에 대한 보완이 필요하다. 일단 무식한 방법으로다. 
         * 1개 이상의 소수에 대한 보완이 필요하다. 
         */
        static public string GetDoubleString(string data)
        {
            if (data == null || data.Trim() == "")
                return "";

            string minus = "";
            if (data.Trim().Substring(0, 1) == "-")
                minus = "-";

            return (minus + Regex.Replace(data, "[^0-9.]", "", RegexOptions.Singleline));
            //return Regex.Replace(data, "[^0-9.]", "", RegexOptions.Singleline);
        }

        /**
         * 주어진 문자열에서 0-9 까지의 숫자만 가져온다.
         */
        static public string GetIntegerString(string data)
        {
            if (data == null || data.Trim() == "")
                return "";

            string minus = "";
            if (data.Trim().Substring(0, 1) == "-")
                minus = "-";

            return (minus + Regex.Replace(data, "[^0-9]", "", RegexOptions.Singleline));
        }

        /**
         * 주어진 문자열에서 0-9 까지의 숫자만 가져온다. 
         * 음수에 대한 보완이 필요하다. 일단 무식한 방법으로다. 
         */
        static public string GetDigitString(string data)
        {
            if (data == null || data.Trim() == "")
                return "";

            string minus = "";
            if (data.Trim().Substring(0, 1) == "-")
                minus = "-";

            return (minus + Regex.Replace(data, "[^0-9.]", "", RegexOptions.Singleline));
            //return Regex.Replace(data, "[^0-9]", "", RegexOptions.Singleline);
        }

        /**
        * 단순 스트링 반환, null 값에 대한 예외처리
        */
        static public string GetString(object value)
        {
            if (value == null)
                return "";
            else
                return value.ToString();
        }

        /**
        * DBMS에서 허용되지 않는 문자를 변경한다. 
        * MySQL, SQLite에서 테스트 됐다. 
        * 점진적으로 개선 필요
        */
        static public string ReplaceSpecialChar(string data)
        {
            return data.Replace("'", "''");
        }

        /**
         * 입력된 값으로부터 double 값을 가져온다.
         */
        static public double GetDoubleValue(string data)
        {
            string valueStr = GetDoubleString(data);
            double value = 0.0d;
            if (valueStr != "")
                value = Double.Parse(valueStr);

            return value;
        }
        static public double GetDoubleValue(object data)
        {
            if (data == null)
                return 0.0d;

            return GetDoubleValue(data.ToString());
        }

        /**
         * 입력된 값으로부터 int32 값을 가져온다.
         */
        static public int GetInteger32Value(string data)
        {
            string valueStr = GetIntegerString(data);
            int value = 0;
            if (valueStr != "")
                value = Int32.Parse(valueStr);
            

            return value;
        }
        static public int GetInteger32Value(object data)
        {
            if (data == null)
                return 0;

            return GetInteger32Value(data.ToString());
        }

        // 전체 그리드행 초기화
        internal static void ClearDataGridView(DataGridView dgv)
        {
            // Line(행) 삭제
            foreach (DataGridViewRow dgr in dgv.SelectedRows)
            {
                dgv.Rows.Remove(dgr);
            }
        }

        // 날짜문자열이 YYYYMMDD 형식으로 입력되었는지 체크
        static public bool isYYYYMMDD(string yyyymmdd)
        {
            return Regex.IsMatch(yyyymmdd, @"^(19|20)\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[0-1])$");
        }

        // 지정된 폼이 이미 실행되고 있으면 폼을 리턴
        static public Form isFormActivated(string formName)
        {
            foreach (Form form in Application.OpenForms)
                if (form.Name.Equals(formName)) 
                    return form;

            return null;
        }




        //--------------------------------------------------------------------------------------------
        // MACAddress 가져오기
        //--------------------------------------------------------------------------------------------
        public static string getMACAddress()
        {
            string MACAddress = NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();
            string formatMACAddress = "";

            //--------------------------------------------------------
            // MACAddress 편집 ('-'  추가)
            //--------------------------------------------------------
            for (int i=0; i<MACAddress.Length; i++)
            {
                formatMACAddress += MACAddress[i];
                if (i != MACAddress.Length - 1)
                {
                    if ((i+1)%2 == 0)
                    {
                        formatMACAddress = formatMACAddress+"-";
                    }
                }
              
            }
            return formatMACAddress;
        }

        //--------------------------------------------------------------------------------------------
        // 이메일 유효성 검증
        //--------------------------------------------------------------------------------------------
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
