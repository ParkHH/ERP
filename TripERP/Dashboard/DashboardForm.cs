using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.ReservationMgt;
using TripERP.Common;

namespace TripERP.Dashboard
{
    public partial class DashboardForm : Form
    {
        private LinkLabel link;
        private List<FlowLayoutPanel> fl_list = new List<FlowLayoutPanel>();
        private List<FlowLayoutPanel> fl_income_list = new List<FlowLayoutPanel>();
        private DateTime currentDate = DateTime.Today;
        private FlowLayoutPanel fl;

        // 대시보드 여행 스케쥴
        private string PRDT_NM = "";    // 상품명
        private string CMPN_NM = "";    // 모객업체
        private string CUST_NM = "";    // 고객명
        private string CUST_CNT = "";    // 고객수
        private string EMPL_NM = "";    // 담당자
        private string STTS_CT = "";    // 예약상태
        private string DPTR_DT = "";    // 출발일자
        private string RSVT_DT = "";    // 예약일자
        private string RSVT_NO = "";    // 예약번호

        // 입금내역
        private string DPWH_CNMB = "";          // 일련번호
        private string FRST_RGST_DTM = "";     // 입금일자
        private string TRAN_CUST_NM = "";    // 거래고객명
        private string REMK_CNTS = "";          // 비고내용
        private string DPWH_WON_AMT = ""; // 입출금원화내역
        private bool is_update = false;

        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            flDays.SuspendLayout();
            displayScheduleChart();
            flDays.ResumeLayout();
        }

        private void displayScheduleChart()
        {
            // 일정 패널 생성(가로행 최대 6라인(7 x 6)
            GenerateDayPanel(42);

            // 요일 데이터 표기
            DisplayCurrentDate();
            // AddScheduleToFlDay(1);
        }


        private void DashboardForm_Enter(object sender, EventArgs e)
        {
            AddIncomeToFlIncome();
            DisplayCurrentDate();
        }


        // 입금내역 (링크)라벨 추가
        private void AddIncomeToFlIncome()
        {
            for(int i = 0; i < 10; i++)
            {
                fl_income_list[i].Controls.Clear();
            }

            string query = string.Format("CALL SelectDashboardIncomeList ()");
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("입금내역을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                Label link = new Label();
                int index = dataSet.Tables[0].Rows.IndexOf(datarow);

                DPWH_CNMB = datarow["DPWH_CNMB"].ToString();
                FRST_RGST_DTM = datarow["FRST_RGST_DTM"].ToString() + "\t";        // 상품명
                TRAN_CUST_NM = datarow["TRAN_CUST_NM"].ToString() + "\t";        // 거래고객명
                REMK_CNTS = datarow["REMK_CNTS"].ToString() + "\t";
                DPWH_WON_AMT = datarow["DPWH_WON_AMT"].ToString();

                DateTime dt = new DateTime();
                DateTime.TryParse(FRST_RGST_DTM, null, System.Globalization.DateTimeStyles.AssumeLocal, out dt);
                string strDt = dt.ToString("MM/dd HH:mm ");
                string link_text = "";

                link.Name = $"Label{DPWH_CNMB}";

                // Form Activate시 입금내역 Reload
                //if (in_is_update)
                //    link.Text = "";

                link_text = strDt + "    " + TRAN_CUST_NM + "\n" + REMK_CNTS + "    " + Utils.SetComma(DPWH_WON_AMT) + "원";            
                
                link.Text = link_text;
                link.Size = new Size(330, 48);
                link.Font = new Font("Malgun Gothic", 12, FontStyle.Bold);

                fl_income_list[index].Controls.Add(link);

                
            }
        }

        // 예약일정 링크라벨 추가
        private void AddScheduleToFlDay(int startDayAtFlNum)
        {
            DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            string query_start_dt = startDate.ToString("yyyy-MM-dd 00:00:00");
            string query_end_dt = endDate.ToString("yyyy-MM-dd 23:59:59");

            string query = string.Format("CALL SelectDashboardScheduleList ('{0}', '{1}')", query_start_dt, query_end_dt);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("대시보드 스케쥴을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                link = new LinkLabel();

                PRDT_NM = datarow["PRDT_NM"].ToString();        // 상품명
                CMPN_NM = datarow["CMPN_NM"].ToString();        // 모객업체
                CUST_NM = datarow["CUST_NM"].ToString();        // 고객명
                CUST_CNT = datarow["CUST_CNT"].ToString();      // 고객수
                EMPL_NM = datarow["EMPL_NM"].ToString();           // 담당직원
                STTS_CT = datarow["RSVT_STTS_CD"].ToString();   // 예약상태코드
                DPTR_DT = datarow["DPTR_DT"].ToString();        // 출발일자
                RSVT_DT = datarow["RSVT_DT"].ToString();        // 예약일자
                //TM_CNT = datarow["TM_CNT"].ToString();          // 팀 인원수
                RSVT_NO = datarow["RSVT_NO"].ToString();        // 예약번호

                /*
                //-----------------------------------------------------------------------------------------
                // 암호화 내역 복호화           --> 191024 박현호
                //-----------------------------------------------------------------------------------------
                CUST_NM = EncryptMgt.Decrypt(CUST_NM, EncryptMgt.aesEncryptKey);
                */

                DateTime dt_DPTR_DT = DateTime.Parse(DPTR_DT);

                link.LinkClicked += new LinkLabelLinkClickedEventHandler(link_LinkClicked);

                link.Name = $"link{PRDT_NM}";
                link.Text = CUST_NM + "(" + CUST_CNT + ")";
                link.Size = new Size(170, 25);
                link.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                link.Links[0].LinkData = PRDT_NM + "^" + CMPN_NM + "^" + CUST_NM + "^" + EMPL_NM + "^" + STTS_CT + "^" + DPTR_DT + "^" + RSVT_DT + "^" + CUST_CNT + "^" + RSVT_NO;

                fl_list[(dt_DPTR_DT.Day - 1) + (startDayAtFlNum - 1)].Controls.Add(link);
            }
        }

        private static string TabbedString(int count)
        {
            return count > 0 ? new string('\t', count) : string.Empty;
        }

        // 예약목록 링크라벨 클릭시
        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] split_LinkData = e.Link.LinkData.ToString().Split('^');
            OpenReservationListForm(split_LinkData);
        }

        // 링크라벨 클릭시 예약목록조회 폼 이동
        private void OpenReservationListForm(string[] IN_SPLIT_LINK_DATA)
        {
            if (IN_SPLIT_LINK_DATA.Equals(null))
            {
                MessageBox.Show("예약 목록을 선택해 주십시오.");
                return;
            }

            string IN_CMPN_NM = IN_SPLIT_LINK_DATA[1];
            string IN_CUST_NM = IN_SPLIT_LINK_DATA[2];
            string IN_EMPL_NM = IN_SPLIT_LINK_DATA[3];
            string IN_STTS_CD = IN_SPLIT_LINK_DATA[4];
            string IN_DPTR_DT = IN_SPLIT_LINK_DATA[5];
            string IN_RSVT_DT = IN_SPLIT_LINK_DATA[6];
            string IN_CUST_CNT = IN_SPLIT_LINK_DATA[7];
            string IN_RSVT_NO = IN_SPLIT_LINK_DATA[8];

            ReservationDetailInfoMgt RSVT_DETL_FORM = new ReservationDetailInfoMgt();
            RSVT_DETL_FORM.MdiParent = Global.mainForm;
            RSVT_DETL_FORM.Text = "예약상세관리";
            RSVT_DETL_FORM.FormBorderStyle = FormBorderStyle.None;
            RSVT_DETL_FORM.Dock = DockStyle.Fill;
            RSVT_DETL_FORM.SetSaveMode(Global.SAVEMODE_UPDATE);
            RSVT_DETL_FORM.SetReservationNumber(IN_RSVT_NO);

            if (RSVT_DETL_FORM.IsHandleCreated == true)
                RSVT_DETL_FORM.BringToFront();
            else
                RSVT_DETL_FORM.Show();
        }

        // 요일 라벨 추가
        private void AddLabelDayToFlDay(int startDayAtFlNumber, int totalDaysInMonth)
        {
            foreach (FlowLayoutPanel fl in fl_list)
                fl.Controls.Clear();

            for (int i = 1; i <= totalDaysInMonth; i++)
            {
                string day = getDay(new DateTime(currentDate.Year, currentDate.Month, i));

                Label lbl = new Label();
                lbl.Name = $"lblDay{i}";
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.Padding = new Padding(0, 0, 10, 0);
                lbl.Size = new Size(192, 33);
                lbl.Text = i.ToString();
                lbl.Font = new Font("Malgun Gothic", 15, FontStyle.Bold);

                DateTime dt = new DateTime(Convert.ToInt16(currentDate.ToString("yyyy")), Convert.ToInt16(currentDate.ToString("MM")), i);

                // 오늘 이전의 날짜는 배경 회색처리
                if (dt < DateTime.Today)
                    fl_list[(i - 1) + (startDayAtFlNumber - 1)].BackColor = Color.LightGray;
                else
                    fl_list[(i - 1) + (startDayAtFlNumber - 1)].BackColor = Color.WhiteSmoke;

                // 주말 색처리
                if (day.Equals("토"))
                    lbl.ForeColor = Color.Blue;
                if (day.Equals("일"))
                    lbl.ForeColor = Color.Red;

                fl_list[(i - 1) + (startDayAtFlNumber - 1)].Controls.Add(lbl);

                fl_list[(i - 1) + (startDayAtFlNumber - 1 )].BorderStyle = BorderStyle.FixedSingle;
            }
        }

        // 일정 패널 생성
        private void GenerateDayPanel(int totalDays)
        {
            flDays.Controls.Clear();
            fl_list.Clear();

            int today = Convert.ToInt16(currentDate.ToString("dd"));

            int SetMont = DateTime.DaysInMonth(Convert.ToInt16(currentDate.ToString("yyyy")), Convert.ToInt16(currentDate.ToString("MM")));
            DateTime dt = new DateTime(Convert.ToInt16(currentDate.ToString("yyyy")), Convert.ToInt16(currentDate.ToString("MM")), SetMont);

            // 요일 패널
            for (int i = 0; i < totalDays; i++)
            {
                fl = new FlowLayoutPanel();
                fl.Name = $"flDay{i}";
                fl.Size = new Size(200, 127);

                fl.HorizontalScroll.Maximum = 0;
                fl.AutoScroll = false;
                fl.VerticalScroll.Visible = false;
                fl.AutoScroll = true;

                flDays.Controls.Add(fl);
                fl_list.Add(fl);
            }

            // 입금내역
            for (int i = 0; i < 10; i++)
            {
                fl = new FlowLayoutPanel();
                fl.Size = new Size(342, 48);
                fl.BorderStyle = BorderStyle.None;
                fl.AutoScroll = true;
                fl.BackColor = Color.White;
                flIncome.Controls.Add(fl);
                fl_income_list.Add(fl);
            }
        }

        // 현재 년, 월 표시
        private void DisplayCurrentDate()
        {
            lblYearAndMonth.Text = currentDate.ToString("yyyy년, MMMM");
            int firstDayAtFlNum = GetFirstDayofWeekofCurrentDate();
            int totalDay = GetTotalDaysOfcurrentDate();
            AddLabelDayToFlDay(firstDayAtFlNum, totalDay);
            AddScheduleToFlDay(firstDayAtFlNum);
        }

        // 저번달
        private void PrevMonth()
        {
            currentDate = currentDate.AddMonths(-1);
            GenerateDayPanel(42);
            DisplayCurrentDate();
        }

        // 다음달
        private void NextMonth()
        {
            currentDate = currentDate.AddMonths(1);
            GenerateDayPanel(42);
            DisplayCurrentDate();
        }

        // 투데이
        private void Today()
        {
            currentDate = DateTime.Today;
            GenerateDayPanel(42);
            DisplayCurrentDate();
        }

        private int GetFirstDayofWeekofCurrentDate()
        {
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            return Convert.ToInt16(firstDayOfMonth.DayOfWeek + 1);
        }

        private int GetTotalDaysOfcurrentDate()
        {
            DateTime firstDayOfCurrentDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            return firstDayOfCurrentDate.AddMonths(1).AddDays(-1).Day;
        }

        private string getDay(DateTime dt)
        {
            string strDay = "";

            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    strDay = "월";
                    break;
                case DayOfWeek.Tuesday:
                    strDay = "화";
                    break;
                case DayOfWeek.Wednesday:
                    strDay = "수";
                    break;
                case DayOfWeek.Thursday:
                    strDay = "목";
                    break;
                case DayOfWeek.Friday:
                    strDay = "금";
                    break;
                case DayOfWeek.Saturday:
                    strDay = "토";
                    break;
                case DayOfWeek.Sunday:
                    strDay = "일";
                    break;
            }
            return strDay;
        }

        private void prevMonth_Click(object sender, EventArgs e)
        {
            PrevMonth();
        }

        private void nextMonth_Click(object sender, EventArgs e)
        {
            NextMonth();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Today();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddIncomeToFlIncome();
        }

        private void DashboardForm_KeyDown(object sender, KeyEventArgs e)
        {
            // F5를 누르면 화면을 Refresh
            if (e.KeyCode == Keys.F5)
            {
                displayScheduleChart();
            }
        }

        private void flDays_MouseMove(object sender, MouseEventArgs e)
        {
            TripERPmain obj = new TripERPmain();
            obj.resetTimer();
            //MessageBox.Show("마우스 움직임");
        }


    }
}
