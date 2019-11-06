using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripERP.Common
{
    class ExcelColumnDictionary
    {              
        // ExcelColumnDic Data 초기화
        public static Dictionary<string, List<string>> initializingExcelColumnDic()
        {
            // Excel Import 시 Excel Column 사전
            // Data 구분을 위한 Column 이름 사전
            Dictionary<string, List<string>> excelColumnDic = new Dictionary<string, List<string>>();

            //1. 주문번호 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> orderNumber = new List<string>();
            orderNumber.Add("주문번호");
            orderNumber.Add("주문 번호");
            orderNumber.Add("주문No.");
            orderNumber.Add("주문 No.");
            orderNumber.Add("주문No");
            orderNumber.Add("주문 No");
            orderNumber.Add("주문no.");
            orderNumber.Add("주문 no.");
            orderNumber.Add("주문no");
            orderNumber.Add("주문 no");
            orderNumber.Add("OrderNumber");
            orderNumber.Add("OrderNum");
            orderNumber.Add("orderNumber");
            orderNumber.Add("orderNum");
            orderNumber.Add("Order Number");
            orderNumber.Add("order Number");
            orderNumber.Add("Order Num");
            orderNumber.Add("order Num");
            //---------------------------------------------------------------------------

            //2. 티켓번호 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> ticketNumber = new List<string>();
            ticketNumber.Add("티켓번호");
            ticketNumber.Add("티켓 번호");
            ticketNumber.Add("티켓번호.");
            ticketNumber.Add("티켓 번호.");
            ticketNumber.Add("티켓NO");
            ticketNumber.Add("티켓NO.");
            ticketNumber.Add("티켓 NO");
            ticketNumber.Add("티켓 NO.");
            ticketNumber.Add("티켓No");
            ticketNumber.Add("티켓No.");
            ticketNumber.Add("티켓 No");
            ticketNumber.Add("티켓 No.");
            ticketNumber.Add("티켓no.");
            ticketNumber.Add("티켓no");
            ticketNumber.Add("티켓 no.");
            ticketNumber.Add("티켓 no");
            ticketNumber.Add("Ticket번호");
            ticketNumber.Add("Ticket 번호");
            ticketNumber.Add("Ticket번호.");
            ticketNumber.Add("Ticket 번호.");
            ticketNumber.Add("TicketNO");
            ticketNumber.Add("TicketNO.");
            ticketNumber.Add("Ticket NO");
            ticketNumber.Add("Ticket NO.");
            ticketNumber.Add("TicketNo");
            ticketNumber.Add("TicketNo.");
            ticketNumber.Add("Ticket No");
            ticketNumber.Add("Ticket No.");
            ticketNumber.Add("Ticket Number.");
            ticketNumber.Add("Ticket Number");
            ticketNumber.Add("TicketNumber.");
            ticketNumber.Add("TicketNumber");
            ticketNumber.Add("TicketNum.");
            ticketNumber.Add("TicketNum");
            ticketNumber.Add("Ticket Num.");
            ticketNumber.Add("Ticket Num");
            //---------------------------------------------------------------------------


            //3. 이름 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> name = new List<string>();
            name.Add("성명");
            name.Add("성 명");
            name.Add("성명.");
            name.Add("성 명.");
            name.Add("이름");
            name.Add("이 름");
            name.Add("이름.");
            name.Add("이 름.");
            name.Add("Name");
            name.Add("Name.");
            name.Add("고객성명");
            name.Add("고객 성명");
            name.Add("고객성명.");
            name.Add("고객 성명.");
            name.Add("고객이름");
            name.Add("고객 이름");
            name.Add("고객이름.");
            name.Add("고객 이름.");
            name.Add("CustomerName");
            name.Add("CustomerName.");
            name.Add("Customer Name");
            name.Add("Customer Name.");
            name.Add("customerName");
            name.Add("customer Name");
            name.Add("customerName.");
            name.Add("customer Name.");
            //---------------------------------------------------------------------------


            //4. 연락처 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> contact = new List<string>();
            contact.Add("연락처");
            contact.Add("연락처.");
            contact.Add("contact");
            contact.Add("contact.");
            contact.Add("Contact");
            contact.Add("Contact.");
            contact.Add("HP");
            contact.Add("HP.");
            contact.Add("H P");
            contact.Add("H P.");
            contact.Add("H.P");
            contact.Add("Phone");
            contact.Add("Phone.");
            contact.Add("PhoneNumber");
            contact.Add("PhoneNumber.");
            contact.Add("Phone Number");
            contact.Add("Phone Number.");
            contact.Add("phoneNumber");
            contact.Add("phoneNumber.");
            contact.Add("phone Number");
            contact.Add("phone Number.");
            contact.Add("phoneNum");
            contact.Add("phoneNum.");
            contact.Add("phone Num");
            contact.Add("phone Num.");
            contact.Add("Phonenumber");
            contact.Add("Phonenumber.");
            contact.Add("Phone number");
            contact.Add("Phone number.");
            //---------------------------------------------------------------------------



            //5. 이메일 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> email = new List<string>();
            email.Add("이메일");
            email.Add("이메일.");
            email.Add("E메일");
            email.Add("E메일.");
            email.Add("E 메일");
            email.Add("E 메일.");
            email.Add("E매일");
            email.Add("E매일.");
            email.Add("E 매일");
            email.Add("E 매일.");
            email.Add("EMail");
            email.Add("EMail.");
            email.Add("E Mail");
            email.Add("E Mail.");
            email.Add("E-Mail");
            email.Add("E-Mail.");
            email.Add("e-Mail");
            email.Add("e-Mail.");
            email.Add("e-mail");
            email.Add("e-mail.");
            email.Add("Email");
            email.Add("Email.");
            email.Add("E mail");
            email.Add("E mail.");
            email.Add("email");
            email.Add("email.");
            email.Add("e mail");
            email.Add("e mail.");
            //---------------------------------------------------------------------------



            //6. 옵션명 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> optionName = new List<string>();
            optionName.Add("옵션명");
            optionName.Add("옵션");
            optionName.Add("옵션명.");
            optionName.Add("옵션.");
            optionName.Add("OptionName");
            optionName.Add("OptionName.");
            optionName.Add("Option Name");
            optionName.Add("Option Name.");
            optionName.Add("Optionname");
            optionName.Add("Optionname.");
            optionName.Add("Option name");
            optionName.Add("Option name.");
            optionName.Add("optionName");
            optionName.Add("optionName.");
            optionName.Add("option Name");
            optionName.Add("option Name.");
            optionName.Add("optionname");
            optionName.Add("optionname.");
            optionName.Add("option name");
            optionName.Add("option name.");
            //---------------------------------------------------------------------------



            //7. 구매금액 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> purchaseAmout = new List<string>();
            purchaseAmout.Add("구매금액");
            purchaseAmout.Add("구매금액.");
            purchaseAmout.Add("구매 금액");
            purchaseAmout.Add("구매 금액.");
            purchaseAmout.Add("구매가격");
            purchaseAmout.Add("구매가격.");
            purchaseAmout.Add("구매 가격");
            purchaseAmout.Add("구매 가격.");
            purchaseAmout.Add("구매가");
            purchaseAmout.Add("구매가.");
            //---------------------------------------------------------------------------



            //8. 할인금액 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> discountAmount = new List<string>();
            discountAmount.Add("할인금액");
            discountAmount.Add("할인금액.");
            discountAmount.Add("할인 금액");
            discountAmount.Add("할인 금액.");
            discountAmount.Add("할인가격");
            discountAmount.Add("할인가격.");
            discountAmount.Add("할인 가격");
            discountAmount.Add("할인 가격.");
            discountAmount.Add("할인가");
            discountAmount.Add("할인가.");
            //---------------------------------------------------------------------------



            //9. 구매일시 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> purchaseDate = new List<string>();
            purchaseDate.Add("구매일시");
            purchaseDate.Add("구매 일시");
            purchaseDate.Add("구매일시.");
            purchaseDate.Add("구매 일시.");
            purchaseDate.Add("구매일자");
            purchaseDate.Add("구매일자.");
            purchaseDate.Add("구매 일자");
            purchaseDate.Add("구매 일자.");
            purchaseDate.Add("구매날짜");
            purchaseDate.Add("구매날짜.");
            purchaseDate.Add("구매 날짜");
            purchaseDate.Add("구매 날짜.");
            //---------------------------------------------------------------------------



            //10. 나이 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            List<string> age = new List<string>();
            age.Add("나이");
            age.Add("나 이");
            age.Add("나이.");
            age.Add("나 이.");
            age.Add("연령");
            age.Add("연령.");
            age.Add("연 령");
            age.Add("연 령.");
            age.Add("age");
            age.Add("age.");
            age.Add("Age");
            age.Add("Age.");
            //---------------------------------------------------------------------------




            //11. 나이 Column 이름 경우의 수 Setting
            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------


            excelColumnDic.Add("주문번호", orderNumber);
            excelColumnDic.Add("티켓번호", ticketNumber);
            excelColumnDic.Add("이름", name);
            excelColumnDic.Add("연락처", contact);
            excelColumnDic.Add("이메일", email);
            excelColumnDic.Add("옵션명", optionName);
            excelColumnDic.Add("구매금액", purchaseAmout);
            excelColumnDic.Add("할인금액", discountAmount);
            excelColumnDic.Add("구매일시", purchaseDate);
            excelColumnDic.Add("나이", age);


            return excelColumnDic;
        }




        // Key 값만 반환하기
        public static List<string> getKeys()
        {        
            Dictionary<string, List<string>> dictionary = initializingExcelColumnDic();
            List<string> keyList = dictionary.Keys.ToList<string>();

            return keyList;
        }
    }
}
