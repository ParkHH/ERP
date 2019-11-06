using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TripERP.Common;
using System.IO;

namespace TripERP.AccountMgt {
    public partial class SlipMgmt : Form {
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 진행상태 객체 동적 생성용
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Label[] _LabelArray = null;
        ComboBox[] _ComboBoxArray = null;
        DateTimePicker[] _DateTimePickerArray = null;
        Guna.UI.WinForms.GunaAdvenceButton[] _ButtonArray = null;
        List<string> _cutOffList = new List<string>();

        private FlowLayoutPanel fl;
        private List<FlowLayoutPanel> fl_Debit_list = new List<FlowLayoutPanel>();
        private List<FlowLayoutPanel> fl_Credit_list = new List<FlowLayoutPanel>();


        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 차변 객체 동적 생성용
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        int _DebitArrayCount { get; set; }

        List<string> _DebitCNMBTextBoxArray = new List<String>();                     // 차변일련번호
        List<Label> _DebitAccountTitCodeLabelArray = new List<Label>();               // 계정과목라벨
        List<ComboBox> _DebitAccountTitCodeComboBoxArray = new List<ComboBox>();        // 계정과목콤보박스
        List<Label> _DebitWonAmountLabelArray = new List<Label>();                        // 차변원화금액라벨
        List<TextBox> _DebitWonAmountTextBoxArray = new List<TextBox>();                 // 차변원화금액텍스트박스
        List<Label> _DebitFrcrAmountLabelArray = new List<Label>();                        // 차변외화금액라벨
        List<TextBox> _DebitFrcrAmountTextBoxArray = new List<TextBox>();                 // 차변외화금액텍스트박스

        List<Guna.UI.WinForms.GunaAdvenceButton> _DebitAddButtonArray = new List<Guna.UI.WinForms.GunaAdvenceButton>();                      // +버튼
        List<Guna.UI.WinForms.GunaAdvenceButton> _DebitEraseButtonArray = new List<Guna.UI.WinForms.GunaAdvenceButton>();                    // -버튼

        //int _DebitXPoint1 = 27;     // 계정과목 라벨 x좌표
        //int _DebitXPoint2 = 104;    // 계정과목 텍스트박스 x좌표
        //int _DebitXPoint3 = 423;    // 금액 라벨 x좌표
        //int _DebitXPoint4 = 472;    // 금액 텍스트박스 x좌표
        //int _DebitXPoint5 = 653;    // +버튼 x좌표
        //int _DebitXPoint6 = 688;    // -버튼 x좌표

        int _DebitXPoint1 = 27;     // 계정과목 라벨 x좌표
        int _DebitXPoint2 = 104;    // 계정과목 텍스트박스 x좌표
        int _DebitXPoint3 = 268;    // 금액 라벨 x좌표
        int _DebitXPoint4 = 348;    // 금액 텍스트박스 x좌표
        int _DebitXPoint5 = 481;    // +버튼 x좌표
        int _DebitXPoint6 = 561;    // -버튼 x좌표
        int _DebitXPoint7 = 653;    // -버튼 x좌표
        int _DebitXPoint8 = 688;    // -버튼 x좌표

        int _DebitYPoint = 112;
        int _DebitLastYPoint = 0;

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 대변 객체 동적 생성용
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        int _CreditArrayCount = 0;

        List<string> _CreditCNMBTextBoxArray = new List<String>();                     // 대변일련번호
        List<Label> _CreditAccountTitCodeLabelArray = new List<Label>();               // 계정과목라벨
        List<ComboBox> _CreditAccountTitCodeComboBoxArray = new List<ComboBox>();        // 게정과목콤보박스
        List<Label> _CreditWonAmountLabelArray = new List<Label>();                        // 대변원화금액라벨
        List<TextBox> _CreditWonAmountTextBoxArray = new List<TextBox>();                 // 대변원화금액텍스트박스
        List<Label> _CreditFrcrAmountLabelArray = new List<Label>();                        // 대변외화금액라벨
        List<TextBox> _CreditFrcrAmountTextBoxArray = new List<TextBox>();                 // 대변외화금액텍스트박스
        List<Guna.UI.WinForms.GunaAdvenceButton> _CreditAddButtonArray = new List<Guna.UI.WinForms.GunaAdvenceButton>();                      // +버튼
        List<Guna.UI.WinForms.GunaAdvenceButton> _CreditEraseButtonArray = new List<Guna.UI.WinForms.GunaAdvenceButton>();                    // -버튼

        //int _CreditXPoint1 = 807;     // 계정과목 라벨 x좌표
        //int _CreditXPoint2 = 884;    // 계정과목 텍스트박스 x좌표
        //int _CreditXPoint3 = 1203;    // 금액 라벨 x좌표
        //int _CreditXPoint4 = 1252;    // 금액 텍스트박스 x좌표
        //int _CreditXPoint5 = 1429;    // +버튼 x좌표
        //int _CreditXPoint6 = 1464;    // -버튼 x좌표

        int _CreditXPoint1 = 831;     // 계정과목 라벨 x좌표
        int _CreditXPoint2 = 908;    // 계정과목 텍스트박스 x좌표
        int _CreditXPoint3 = 1072;    // 금액 라벨 x좌표
        int _CreditXPoint4 = 1152;    // 금액 텍스트박스 x좌표
        int _CreditXPoint5 = 1285;    // +버튼 x좌표
        int _CreditXPoint6 = 1365;    // -버튼 x좌표
        int _CreditXPoint7 = 1498;    // -버튼 x좌표
        int _CreditXPoint8 = 1533;    // -버튼 x좌표

        int _CreditYPoint = 112;
        int _CreditLastYPoint = 0;

        private bool IS_FROM_GRIDVIEW = false;

        private enum egrdSlipDataGridView {
            SLIP_DT,
            SLIP_NO,
            SLIP_CNMB,
            REMK_CNTS,
            ACNT_TIT_NM,
            ACNT_TIT_CD,
            DBCR_DVSN_NM,
            DBCR_DVSN_CD,
            SLIP_WON_AMT,
            SLIP_FRCR_AMT,
            TRAN_EXRT,
            ACCT_NO,
            ACCT_NM,
            CASH_TRNS_DVSN_NM,
            CASH_TRNS_DVSN_CD,
            CUR_CD,
            RCKO_DT,
            DPWH_CNMB,
            EMPL_NM,
            RPSB_EMPL_NO,
            RSVT_NO,
            DELETE_BTN
        }

        private string SLIP_DT { get; set; }         // 전표일자
        private int SLIP_NO { get; set; }                // 전표번호
        private string SLIP_CNMB { get; set; }               // 전표일련번호
        private string ACNT_TIT_CD { get; set; }             // 계정과목코드
        private string ACNT_TIT_NM { get; set; }             // 계정과목명
        private string DBCR_DVSN_CD { get; set; }            // 차대구분코드
        private string DBCR_DVSN_NM { get; set; }            // 차대구분명
        private string CASH_TRNS_DVSN_CD { get; set; }       // 현금대체구분코드
        private string CASH_TRNS_DVSN_NM { get; set; }       // 현금대체구분명
        private string DPWH_DVSN_CD { get; set; }            // 입출금구분코드
        private string DPWH_DVSN_NM { get; set; }            // 입출금구분명
        private string CUR_CD { get; set; }                  // 통화코드
        private string SLIP_WON_AMT { get; set; }                // 전표원화금액
        private string SLIP_FRCR_AMT { get; set; }              // 전표외화금액
        private string TRAN_EXRT { get; set; }              // 거래환율
        private string ACCT_NO { get; set; }                 // 계좌번호
        private string ACCT_NM { get; set; }                 // 계좌명
        private string RCKO_DT { get; set; }                 // 기산일자
        private string DPWH_CNMB { get; set; }               // 입출금일련번호
        private string RPSB_EMPL_NO { get; set; }            // 담당직원번호
        private string EMPL_NM { get; set; }                 // 담당직원명
        private string RSVT_NO { get; set; }                 // 예약번호
        private string REMK_CNTS { get; set; }               // 비고내용
        private string RGTR_ID { get; set; }                 // 등록자ID
        private Button DELETE_BTN { get; set; }              // 삭제버튼

        public SlipMgmt() {
            InitializeComponent();

            searchSlipDateTimePicker_Before.Value = DateTime.Now.AddDays(-30);
        }

        private void SlipMgmt_Load(object sender, EventArgs e) {
            // 입력필드 초기화
            resetInputField();

            InitControls();

            // 차변 입력필드 초기화
            setDefaultDebitInfoLine();

            // 대변 입력필드 초기화
            setDefaultCreditInfoLine();

            SearchDailySlipList();
        }

        private void InitControls() {
            LoadAccountTitleComboBoxItems(searchAccountTitleComboBox);
            LoadBankNameComboBox();
            LoadCurrencyCodeComboBoxItems();
        }

        private void resetInputField() {

        }

        private void setDebitFlowLayoutPanel() {
            fl = new FlowLayoutPanel();
            fl.FlowDirection = FlowDirection.LeftToRight;
            fl.Name = $"fl{_DebitArrayCount}";
            fl.Size = new Size(740, 33);
            fl.BorderStyle = BorderStyle.None;
            fl_Debit_list.Add(fl);
        }

        private void setCreditFlowLayoutPanel() {
            fl = new FlowLayoutPanel();
            fl.FlowDirection = FlowDirection.LeftToRight;
            fl.Name = $"fl{_CreditArrayCount}";
            fl.Size = new Size(740, 33);
            fl.BorderStyle = BorderStyle.None;
            fl_Credit_list.Add(fl);
        }

        //=========================================================================================================================================================================
        // 차변 세팅
        //========================================================================================================================================================================= 
        private void setDefaultDebitInfoLine() {
            _DebitArrayCount = 1;
            _DebitLastYPoint = _DebitYPoint;

            DebitFlowLayoutPanel.HorizontalScroll.Maximum = 0;
            DebitFlowLayoutPanel.AutoScroll = false;
            DebitFlowLayoutPanel.WrapContents = false;
            DebitFlowLayoutPanel.VerticalScroll.Visible = false;
            DebitFlowLayoutPanel.AutoScroll = true;
            DebitFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;

            // DebitFlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
            setDebitFlowLayoutPanel();

            // 계정과목 라벨
            createDebitAccountTitleLabel(_DebitArrayCount, _DebitXPoint1, _DebitYPoint + 3);

            // 차변일련번호 list
            _DebitCNMBTextBoxArray.Add("");

            // 계정과목 콤보박스
            createDebitAccountTitleComboBox(_DebitArrayCount, _DebitXPoint2, _DebitYPoint);

            // 원화금액 라벨
            createDebitWonAmountLabel(_DebitArrayCount, _DebitXPoint3, _DebitYPoint + 3);

            // 원화금액 텍스트박스
            createDebitWonAmountTextBox(_DebitArrayCount, _DebitXPoint4, _DebitYPoint);

            // 외화금액 라벨
            createDebitFrcrAmountLabel(_DebitArrayCount, _DebitXPoint5, _DebitYPoint + 3);
        
            // 외화금액 텍스트박스
            createDebitFrcrAmountTextBox(_DebitArrayCount, _DebitXPoint6, _DebitYPoint);

            // +버튼
            createDebitPlusButton(_DebitArrayCount, _DebitXPoint7, _DebitYPoint);

            // -버튼
            createDebitMinusButton(_DebitArrayCount, _DebitXPoint8, _DebitYPoint);

            DebitFlowLayoutPanel.Controls.Add(fl);

            SetDynamicControls();
        }

        //=========================================================================================================================================================================
        // 대변 세팅
        //=========================================================================================================================================================================
        private void setDefaultCreditInfoLine() {
            _CreditArrayCount = 1;
            _CreditLastYPoint = _CreditYPoint;

            CreditFlowLayoutPanel.HorizontalScroll.Maximum = 0;
            CreditFlowLayoutPanel.AutoScroll = false;
            CreditFlowLayoutPanel.WrapContents = false;
            CreditFlowLayoutPanel.VerticalScroll.Visible = false;
            CreditFlowLayoutPanel.AutoScroll = true;
            CreditFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;

            // DebitFlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
            setCreditFlowLayoutPanel();

            // 계정과목 라벨
            createCreditAccountTitleLabel(_CreditArrayCount, _CreditXPoint1, _CreditYPoint + 3);

            // 차변일련번호 list
            _CreditCNMBTextBoxArray.Add("");

            // 계정과목 콤보박스
            createCreditAccountTitleComboBox(_CreditArrayCount, _CreditXPoint2, _CreditYPoint);

            // 원화금액 라벨
            createCreditWonAmountLabel(_CreditArrayCount, _CreditXPoint3, _CreditYPoint + 3);

            // 원화금액 텍스트박스
            createCreditWonAmountTextBox(_CreditArrayCount, _CreditXPoint4, _CreditYPoint);

            // 외화금액 라벨
            createCreditFrcrAmountLabel(_CreditArrayCount, _CreditXPoint5, _CreditYPoint + 3);

            // 외화금액 텍스트박스
            createCreditFrcrAmountTextBox(_CreditArrayCount, _CreditXPoint6, _CreditYPoint);

            // +버튼
            createCreditPlusButton(_CreditArrayCount, _CreditXPoint7, _CreditYPoint);

            // -버튼
            createCreditMinusButton(_CreditArrayCount, _CreditXPoint8, _CreditYPoint);

            CreditFlowLayoutPanel.Controls.Add(fl);

            SetDynamicControls();
        }



        //=========================================================================================================================================================================
        // [차변]계정과목 라벨 생성
        //=========================================================================================================================================================================
        private void createDebitAccountTitleLabel(int arrayRow, int xPoint, int yPoint) {
            Label label = new Label();
            label.Text = "계정과목";
            label.Font = new Font("맑은 고딕", 12);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Size = new Size(74, 21);
            label.Tag = arrayRow;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label.BringToFront();
            _DebitAccountTitCodeLabelArray.Add(label);

            fl.Controls.Add(label);
        }

        //=========================================================================================================================================================================
        // [차변]계정과목 콤보박스 생성
        //=========================================================================================================================================================================
        private void createDebitAccountTitleComboBox(int arrayRow, int xPoint, int yPoint) {
            ComboBox comboBox = new ComboBox();
            comboBox.Font = new Font("맑은 고딕", 12);
            comboBox.Size = new Size(151, 29);
            comboBox.Tag = arrayRow;
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            comboBox.BringToFront();


            LoadAccountTitleComboBoxItems(comboBox);
            _DebitAccountTitCodeComboBoxArray.Add(comboBox);

            fl.Controls.Add(comboBox);
        }

        //=========================================================================================================================================================================
        // [차변]원화금액 라벨 생성
        //=========================================================================================================================================================================
        private void createDebitWonAmountLabel(int arrayRow, int xPoint, int yPoint) {
            Label label = new Label();
            label.Text = "원화금액";
            label.Font = new Font("맑은 고딕", 12);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Size = new Size(74, 21);
            label.Tag = arrayRow;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label.BringToFront();
            _DebitWonAmountLabelArray.Add(label);

            fl.Controls.Add(label);
        }

        //=========================================================================================================================================================================
        // [차변]원화금액 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createDebitWonAmountTextBox(int arrayRow, int xPoint, int yPoint) {
            TextBox textBox = new TextBox();
            textBox.Text = "0";
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.Size = new Size(120, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox.TextChanged += new EventHandler(changedDebitWonAmount_Event);
            textBox.KeyPress += new KeyPressEventHandler(DebitWonAmount_KeyPressEvent);

            if (!Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            {
                textBox.Enabled = false;
            }

            _DebitWonAmountTextBoxArray.Add(textBox);

            fl.Controls.Add(textBox);
        }

        //=========================================================================================================================================================================
        // [차변]원화금액 소계 이벤트
        //=========================================================================================================================================================================
        private void changedDebitWonAmount_Event(object sender, EventArgs e) {
            TextBox tbx_Price = sender as TextBox;

            double DebitTotalPrice = 0;
            foreach (TextBox prices in _DebitWonAmountTextBoxArray) {
                if (prices.Text != "" && prices.Text != null)
                    DebitTotalPrice += double.Parse(prices.Text.Trim());
            }

            DebitAmountTextBox.Text = Utils.SetComma(DebitTotalPrice);
        }

        //=========================================================================================================================================================================
        // [차변]원화금액 숫자만 입력 이벤트
        //=========================================================================================================================================================================
        private void DebitWonAmount_KeyPressEvent(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        //=========================================================================================================================================================================
        // [차변]외화금액 라벨 생성
        //=========================================================================================================================================================================
        private void createDebitFrcrAmountLabel(int arrayRow, int xPoint, int yPoint)
        {
            Label label = new Label();
            label.Text = "외화금액";
            label.Font = new Font("맑은 고딕", 12);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Size = new Size(74, 21);
            label.Tag = arrayRow;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label.BringToFront();
            _DebitFrcrAmountLabelArray.Add(label);

            fl.Controls.Add(label);
        }

        //=========================================================================================================================================================================
        // [차변]외화금액 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createDebitFrcrAmountTextBox(int arrayRow, int xPoint, int yPoint)
        {
            TextBox textBox = new TextBox();
            textBox.Text = "0";
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.Size = new Size(120, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox.BringToFront();
            textBox.KeyPress += new KeyPressEventHandler(DebitFrcrAmount_KeyPressEvent);
            textBox.TextChanged += new EventHandler(changedDebitFrcrAmount_Event);
            if (Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
                textBox.Enabled = false;

            _DebitFrcrAmountTextBoxArray.Add(textBox);

            fl.Controls.Add(textBox);
        }

        //=========================================================================================================================================================================
        // [차변]외화금액 X 환율 = 원화금액 이벤트
        //=========================================================================================================================================================================
        private void changedDebitFrcrAmount_Event(object sender, EventArgs e)
        {
            TextBox tbx_Price = sender as TextBox;
            int tag = (int)tbx_Price.Tag;
            
            if (TRAN_EXRT_TextBox.Text.Equals("") || (TRAN_EXRT_TextBox.Text.Equals("0.00") && !Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW")))
            {
                MessageBox.Show("환율을 입력해주세요.");
                return;
            }

            double TRAN_EXRT = 0;
            double FRCR_AMT = 0;

            if (TRAN_EXRT_TextBox.Text != "")
                TRAN_EXRT = double.Parse(TRAN_EXRT_TextBox.Text.Trim());

            if (tbx_Price.Text != "")
                FRCR_AMT = double.Parse(tbx_Price.Text.Trim());

            if (!TRAN_EXRT.Equals(0))
                _DebitWonAmountTextBoxArray[tag - 1].Text = Utils.SetComma((TRAN_EXRT * FRCR_AMT).ToString());
        }

        //=========================================================================================================================================================================
        // [차변]외화금액 숫자만 입력 이벤트
        //=========================================================================================================================================================================
        private void DebitFrcrAmount_KeyPressEvent(object sender, KeyPressEventArgs e)
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

        //=========================================================================================================================================================================
        // [차변]행추가(+) 버튼 생성
        //=========================================================================================================================================================================
        private void createDebitPlusButton(int arrayRow, int xPoint, int yPoint) {
            Guna.UI.WinForms.GunaAdvenceButton button = new Guna.UI.WinForms.GunaAdvenceButton();
            button.Size = new Size(29, 29);
            button.Tag = arrayRow;
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            button.Click += new EventHandler(addDebitInfoLineButton_Click);
            button.BringToFront();
            button.BaseColor = Color.FromArgb(37, 35, 63);
            button.Image = TripERP.Properties.Resources.plus_white_30px;
            button.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;

            _DebitAddButtonArray.Add(button);

            fl.Controls.Add(button);
        }

        //=========================================================================================================================================================================
        // [차변]행추가(-) 버튼 생성
        //=========================================================================================================================================================================
        private void createDebitMinusButton(int arrayRow, int xPoint, int yPoint) {
            Guna.UI.WinForms.GunaAdvenceButton button = new Guna.UI.WinForms.GunaAdvenceButton();
            button.Size = new Size(29, 29);
            button.Tag = arrayRow;
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            button.Click += new EventHandler(eraseDebitInfoLineButton_Click);
            button.BringToFront();
            button.BaseColor = Color.FromArgb(37, 35, 63);
            button.Image = TripERP.Properties.Resources.minus_white_30px;
            button.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;

            //if (_DebitArrayCount == 1)
            //    button.Enabled = false;
            //else
            //    _DebitEraseButtonArray[0].Enabled = true;

            _DebitEraseButtonArray.Add(button);

            fl.Controls.Add(button);
        }

        //=========================================================================================================================================================================
        // 차변 행추가 버튼 클릭 이벤트
        //=========================================================================================================================================================================
        private void addDebitInfoLineButton_Click(object sender, EventArgs e) {
            DebitFlowLayoutPanel.SuspendLayout();

            int _DebitArrayCount = _DebitAddButtonArray.Count;
            _DebitArrayCount++;

            // 버튼 이벤트 정의
            Guna.UI.WinForms.GunaAdvenceButton button = sender as Guna.UI.WinForms.GunaAdvenceButton;
            int tag = (int)button.Tag;

            _DebitLastYPoint = _DebitYPoint + 22;

            // DebitFlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
            setDebitFlowLayoutPanel();

            // 계정과목 라벨
            createDebitAccountTitleLabel(0, _DebitXPoint1, _DebitYPoint + 3);

            // 차변일련번호 list
            _DebitCNMBTextBoxArray.Add("");

            // 계정과목 콤보박스
            createDebitAccountTitleComboBox(_DebitArrayCount, _DebitXPoint2, _DebitYPoint);

            // 원화금액 라벨
            createDebitWonAmountLabel(_DebitArrayCount, _DebitXPoint3, _DebitYPoint + 3);

            // 원화금액 텍스트박스
            createDebitWonAmountTextBox(_DebitArrayCount, _DebitXPoint4, _DebitYPoint);

            // 외화금액 라벨
            createDebitFrcrAmountLabel(_DebitArrayCount, _DebitXPoint5, _DebitYPoint + 3);

            // 외화금액 텍스트박스
            createDebitFrcrAmountTextBox(_DebitArrayCount, _DebitXPoint6, _DebitYPoint);

            // +버튼
            createDebitPlusButton(_DebitArrayCount, _DebitXPoint7, _DebitYPoint);

            // -버튼
            createDebitMinusButton(_DebitArrayCount, _DebitXPoint8, _DebitYPoint);

            //SetDynamicControls();

            // 차변금액 합계 반영
            //AddDebitAmount();

            fl_Debit_list.Add(fl);

            DebitFlowLayoutPanel.Controls.Add(fl);

            DebitFlowLayoutPanel.ResumeLayout();
        }

        //=========================================================================================================================================================================
        // 차변 행삭제 버튼 클릭 이벤트
        //=========================================================================================================================================================================
        private void eraseDebitInfoLineButton_Click(object sender, EventArgs e) {
            DebitFlowLayoutPanel.SuspendLayout();

            Label label = sender as Label;
            ComboBox comboBox = sender as ComboBox;
            Guna.UI.WinForms.GunaAdvenceButton button = sender as Guna.UI.WinForms.GunaAdvenceButton;

           

            int tag = (int)button.Tag;
            if (tag <= 0) return;
            if (_DebitCNMBTextBoxArray[tag - 1] == null)
                return;

            int _DebitArrayCount = _DebitEraseButtonArray.Count;

            // 이전 행을 지우려고 하면 무시
            if (_DebitArrayCount > tag) {
                _DebitArrayCount = tag;
            }

            // 첫번째 차변 입력행의 삭제버튼을 누르면 값만 지움
            if (tag == 1 && _DebitAddButtonArray.Count == 1) {
                _DebitAccountTitCodeComboBoxArray[0].SelectedIndex = 0;
                _DebitWonAmountTextBoxArray[0].Text = "0";
                _DebitFrcrAmountTextBoxArray[0].Text = "0";

                DebitAmountTextBox.Text = "0";

                DebitFlowLayoutPanel.ResumeLayout();
                return;
            }


            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 차변 객체 제거
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 최종 y좌표 감소
            _DebitLastYPoint = _DebitLastYPoint - 33;

            // 배열크기 감소
            //arrayCount--;
            //_optionArrayCount = arrayCount;
            _DebitArrayCount--;

            TextBox textBox = null;
            FlowLayoutPanel fl = fl_Debit_list[_DebitArrayCount];

            _DebitAccountTitCodeLabelArray.RemoveAt(_DebitArrayCount);
            _DebitAccountTitCodeComboBoxArray.RemoveAt(_DebitArrayCount);
            _DebitWonAmountLabelArray.RemoveAt(_DebitArrayCount);
            _DebitWonAmountTextBoxArray.RemoveAt(_DebitArrayCount);
            _DebitFrcrAmountLabelArray.RemoveAt(_DebitArrayCount);
            _DebitFrcrAmountTextBoxArray.RemoveAt(_DebitArrayCount);
            _DebitAddButtonArray.RemoveAt(_DebitArrayCount);
            _DebitEraseButtonArray.RemoveAt(_DebitArrayCount);

            DebitFlowLayoutPanel.Controls.RemoveAt(_DebitArrayCount);
            //DebitFlowLayoutPanel.Controls.Remove(fl);
            fl_Debit_list.RemoveAt(_DebitArrayCount);

            foreach (Guna.UI.WinForms.GunaAdvenceButton btn in _DebitEraseButtonArray) {
                if ((int)btn.Tag > tag)
                    btn.Tag = (int)btn.Tag - 1;
            }

            AddDebitAmount();
            DebitFlowLayoutPanel.ResumeLayout();
        }

        //=========================================================================================================================================================================
        // [대변]계정과목 라벨 생성
        //=========================================================================================================================================================================
        private void createCreditAccountTitleLabel(int arrayRow, int xPoint, int yPoint) {
            Label label = new Label();
            label.Text = "계정과목";
            label.Font = new Font("맑은 고딕", 12);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Size = new Size(74, 21);
            label.Tag = arrayRow;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label.BringToFront();
            _CreditAccountTitCodeLabelArray.Add(label);

            fl.Controls.Add(label);
        }

        //=========================================================================================================================================================================
        // [대변]계정과목 콤보박스 생성
        //=========================================================================================================================================================================
        private void createCreditAccountTitleComboBox(int arrayRow, int xPoint, int yPoint) {
            ComboBox comboBox = new ComboBox();
            comboBox.Font = new Font("맑은 고딕", 12);
            comboBox.Size = new Size(151, 29);
            comboBox.Tag = arrayRow;
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            comboBox.BringToFront();

            LoadAccountTitleComboBoxItems(comboBox);
            _CreditAccountTitCodeComboBoxArray.Add(comboBox);

            fl.Controls.Add(comboBox);
        }

        //=========================================================================================================================================================================
        // [대변]원화금액 라벨 생성
        //=========================================================================================================================================================================
        private void createCreditWonAmountLabel(int arrayRow, int xPoint, int yPoint) {
            Label label = new Label();
            label.Text = "원화금액";
            label.Font = new Font("맑은 고딕", 12);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Size = new Size(74, 21);
            label.Tag = arrayRow;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label.BringToFront();
            _CreditWonAmountLabelArray.Add(label);
            

            fl.Controls.Add(label);
        }

        //=========================================================================================================================================================================
        // [대변]원화금액 텍스트박스 생성 
        //=========================================================================================================================================================================
        private void createCreditWonAmountTextBox(int arrayRow, int xPoint, int yPoint) {
            TextBox textBox = new TextBox();
            textBox.Text = "0";
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.Size = new Size(120, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox.BringToFront();
            textBox.TextChanged += new EventHandler(changedCreditWonAmount_Event);
            textBox.KeyPress += new KeyPressEventHandler(CreditWonAmount_KeyPressEvent);
            if (!Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            {
                textBox.Enabled = false;
            }

            _CreditWonAmountTextBoxArray.Add(textBox);

            fl.Controls.Add(textBox);
        }

        //=========================================================================================================================================================================
        // [대변]원화금액 숫자만 입력 이벤트
        //=========================================================================================================================================================================
        private void CreditWonAmount_KeyPressEvent(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        //=========================================================================================================================================================================
        // [대변]원화금액 소계 이벤트
        //=========================================================================================================================================================================
        private void changedCreditWonAmount_Event(object sender, EventArgs e)
        {
            TextBox tbx_Price = sender as TextBox;

            double CreditTotalPrice = 0;
            foreach (TextBox prices in _CreditWonAmountTextBoxArray)
            {
                if (prices.Text != null && prices.Text != "")
                    CreditTotalPrice += double.Parse(prices.Text.Trim());
            }

            CreditAmountTextBox.Text = Utils.SetComma(CreditTotalPrice);
        }

        //=========================================================================================================================================================================
        // [대변]외화금액 라벨 생성
        //=========================================================================================================================================================================
        private void createCreditFrcrAmountLabel(int arrayRow, int xPoint, int yPoint)
        {
            Label label = new Label();
            label.Text = "외화금액";
            label.Font = new Font("맑은 고딕", 12);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Size = new Size(74, 21);
            label.Tag = arrayRow;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label.BringToFront();
            _CreditFrcrAmountLabelArray.Add(label);

            fl.Controls.Add(label);
        }

        //=========================================================================================================================================================================
        // [대변]외화금액 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createCreditFrcrAmountTextBox(int arrayRow, int xPoint, int yPoint)
        {
            TextBox textBox = new TextBox();
            textBox.Text = "0";
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.Size = new Size(120, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox.TextChanged += new EventHandler(changedCreditFrcrAmount_Event);
            textBox.KeyPress += new KeyPressEventHandler(CreditFrcrAmount_KeyPressEvent);
            if (Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            {
                textBox.Enabled = false;
            }

            _CreditFrcrAmountTextBoxArray.Add(textBox);

            fl.Controls.Add(textBox);
        }

        //=========================================================================================================================================================================
        // [대변]외화금액 X 환율 = 원화금액 이벤트
        //=========================================================================================================================================================================
        private void changedCreditFrcrAmount_Event(object sender, EventArgs e)
        {
            TextBox tbx_Price = sender as TextBox;
            int tag = (int)tbx_Price.Tag;

            if (TRAN_EXRT_TextBox.Text.Equals(""))
            {
                MessageBox.Show("환율을 입력해주세요.");
                return;
            }

            double TRAN_EXRT = 0;
            double FRCR_AMT = 0;

            if (TRAN_EXRT_TextBox.Text != "")
                TRAN_EXRT = double.Parse(TRAN_EXRT_TextBox.Text.Trim());

            if (tbx_Price.Text != "")
                FRCR_AMT = double.Parse(tbx_Price.Text.Trim());

            if (!TRAN_EXRT.Equals(0))
                _CreditWonAmountTextBoxArray[tag - 1].Text = Utils.SetComma((TRAN_EXRT * FRCR_AMT).ToString());
        }

        //=========================================================================================================================================================================
        // [대변]외화금액 숫자만 입력 이벤트
        //=========================================================================================================================================================================
        private void CreditFrcrAmount_KeyPressEvent(object sender, KeyPressEventArgs e)
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

        //=========================================================================================================================================================================
        // [대변]행추가(+) 버튼 생성
        //=========================================================================================================================================================================
        private void createCreditPlusButton(int arrayRow, int xPoint, int yPoint) {
            Guna.UI.WinForms.GunaAdvenceButton button = new Guna.UI.WinForms.GunaAdvenceButton();
            button.Size = new Size(29, 29);
            button.Tag = arrayRow;
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            button.Click += new EventHandler(addCreditInfoLineButton_Click);
            button.BringToFront();
            button.BaseColor = Color.FromArgb(37, 35, 63);
            button.Image = TripERP.Properties.Resources.plus_white_30px;
            button.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;

            _CreditAddButtonArray.Add(button);

            fl.Controls.Add(button);
        }

        //=========================================================================================================================================================================
        // [대변]행추가(-) 버튼 생성
        //=========================================================================================================================================================================
        private void createCreditMinusButton(int arrayRow, int xPoint, int yPoint) {
            Guna.UI.WinForms.GunaAdvenceButton button = new Guna.UI.WinForms.GunaAdvenceButton();
            button.Size = new Size(29, 29);
            button.Tag = arrayRow;
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            button.Click += new EventHandler(eraseCreditInfoLineButton_Click);
            button.BringToFront();
            button.BaseColor = Color.FromArgb(37, 35, 63);
            button.Image = TripERP.Properties.Resources.minus_white_30px;
            button.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;

            //if (_DebitArrayCount == 1)
            //    button.Enabled = false;
            //else
            //    _DebitEraseButtonArray[0].Enabled = true;

            _CreditEraseButtonArray.Add(button);

            fl.Controls.Add(button);
        }

        //=========================================================================================================================================================================
        // [대변]옵션 행추가 버튼 클릭 이벤트
        //=========================================================================================================================================================================
        private void addCreditInfoLineButton_Click(object sender, EventArgs e) {
            CreditFlowLayoutPanel.SuspendLayout();

            int _CreditArrayCount = _CreditAddButtonArray.Count;
            _CreditArrayCount++;

            // 버튼 이벤트 정의
            Guna.UI.WinForms.GunaAdvenceButton button = sender as Guna.UI.WinForms.GunaAdvenceButton;
            int tag = (int)button.Tag;

            _CreditLastYPoint = _CreditYPoint + 22;

            // CreditFlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
            setCreditFlowLayoutPanel();

            // 계정과목 라벨
            createCreditAccountTitleLabel(0, _CreditXPoint1, _CreditYPoint + 3);

            // 차변일련번호 list
            _CreditCNMBTextBoxArray.Add("");

            // 계정과목 콤보박스
            createCreditAccountTitleComboBox(_CreditArrayCount, _CreditXPoint2, _CreditYPoint);

            // 원화금액 라벨
            createCreditWonAmountLabel(_CreditArrayCount, _CreditXPoint3, _CreditYPoint + 3);

            // 원화금액 텍스트박스
            createCreditWonAmountTextBox(_CreditArrayCount, _CreditXPoint4, _CreditYPoint);

            // 외화금액 라벨
            createCreditFrcrAmountLabel(_CreditArrayCount, _CreditXPoint5, _CreditYPoint + 3);

            // 외화금액 텍스트박스
            createCreditFrcrAmountTextBox(_CreditArrayCount, _CreditXPoint6, _CreditYPoint);

            // +버튼
            createCreditPlusButton(_CreditArrayCount, _CreditXPoint5, _CreditYPoint);

            // -버튼
            createCreditMinusButton(_CreditArrayCount, _CreditXPoint6, _CreditYPoint);

            //SetDynamicControls();

            // 차변금액 합계 반영
            //AddCreditAmount();

            fl_Credit_list.Add(fl);
            CreditFlowLayoutPanel.Controls.Add(fl);

            CreditFlowLayoutPanel.ResumeLayout();
        }

        //=========================================================================================================================================================================
        // [대변]옵션 행삭제 버튼 클릭 이벤트
        //=========================================================================================================================================================================
        private void eraseCreditInfoLineButton_Click(object sender, EventArgs e) {
            CreditFlowLayoutPanel.SuspendLayout();

            Label label = sender as Label;
            ComboBox comboBox = sender as ComboBox;
            Guna.UI.WinForms.GunaAdvenceButton button = sender as Guna.UI.WinForms.GunaAdvenceButton;

            int tag = (int)button.Tag;
            if (tag <= 0) return;
            if (_CreditCNMBTextBoxArray[tag - 1] == null)
                return;

            int _CreditArrayCount = _CreditEraseButtonArray.Count;

            // 이전 행을 지우려고 하면 무시
            if (_CreditArrayCount > tag) {
                _CreditArrayCount = tag;
            }

            // 첫번째 차변 입력행의 삭제버튼을 누르면 값만 지움
            if (tag == 1 && _CreditAddButtonArray.Count == 1) {
                _CreditAccountTitCodeComboBoxArray[0].SelectedIndex = 0;
                _CreditWonAmountTextBoxArray[0].Text = "0";
                _CreditFrcrAmountTextBoxArray[0].Text = "0";

                CreditFlowLayoutPanel.ResumeLayout();
                CreditAmountTextBox.Text = "";
                return;
            }


            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 차변 객체 제거
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 최종 y좌표 감소
            _CreditLastYPoint = _CreditLastYPoint - 33;

            // 배열크기 감소
            //arrayCount--;
            //_optionArrayCount = arrayCount;
            _CreditArrayCount--;

            TextBox textBox = null;
            FlowLayoutPanel fl = fl_Credit_list[_CreditArrayCount];

            _CreditAccountTitCodeLabelArray.RemoveAt(_CreditArrayCount);
            _CreditAccountTitCodeComboBoxArray.RemoveAt(_CreditArrayCount);
            _CreditWonAmountLabelArray.RemoveAt(_CreditArrayCount);
            _CreditWonAmountTextBoxArray.RemoveAt(_CreditArrayCount);
            _CreditFrcrAmountLabelArray.RemoveAt(_CreditArrayCount);
            _CreditFrcrAmountTextBoxArray.RemoveAt(_CreditArrayCount);
            _CreditAddButtonArray.RemoveAt(_CreditArrayCount);
            _CreditEraseButtonArray.RemoveAt(_CreditArrayCount);

            CreditFlowLayoutPanel.Controls.RemoveAt(_CreditArrayCount);
            //CreditFlowLayoutPanel.Controls.Remove(fl);
            fl_Credit_list.RemoveAt(_CreditArrayCount);

            foreach (Guna.UI.WinForms.GunaAdvenceButton btn in _CreditEraseButtonArray) {
                if ((int)btn.Tag > tag)
                    btn.Tag = (int)btn.Tag - 1;
            }

            AddCreditAmount();
            CreditFlowLayoutPanel.ResumeLayout();
        }

        // 통화코드 콤보박스
        private void LoadCurrencyCodeComboBoxItems() {
            CurrencyCodeComboBox.Items.Clear();

            string query = "CALL SelectCurListFromAccount ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0) {
                MessageBox.Show("통화코드 정보를 가져올 수 없습니다.");
                return;
            }

            //currencyCodeComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows) {
                string CUR_CD = dataRow["CUR_CD"].ToString();
                string CUR_NM = dataRow["CUR_NM"].ToString();
                string CUR_SYBL = dataRow["CUR_SYBL"].ToString();

                ComboBoxItem item = new ComboBoxItem(string.Format("{0}({1})", CUR_NM, CUR_SYBL), CUR_CD);

                CurrencyCodeComboBox.Items.Add(item);      // 판매통화코드
            }

            if (CurrencyCodeComboBox.Items.Count > 0)
                CurrencyCodeComboBox.Text = "";
        }

        // 계정과목 콤보박스
        private void LoadAccountTitleComboBoxItems(ComboBox comboBox) {
            comboBox.Items.Clear();

            string query = "CALL SelectAccountTitleList ('')";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0) {
                MessageBox.Show("계정과목 정보를 가져올 수 없습니다.");
                return;
            }

            //currencyCodeComboBox.Items.Add(new ComboBoxItem("전체", ""));
            comboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows) {
                string ACNT_TIT_CD = dataRow["ACNT_TIT_CD"].ToString();
                string ACNT_TIT_NM = dataRow["ACNT_TIT_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(ACNT_TIT_NM, ACNT_TIT_CD);

                comboBox.Items.Add(item);
            }

            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }

        // 은행명 콤보박스 세팅
        private void LoadBankNameComboBox() {
            string query = "CALL SelectAccountNoComboBoxItems ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0) {
                MessageBox.Show("계좌명을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows) {
                string ACCT_NM = dataRow["ACCT_NM"].ToString();
                string ACCT_NO = dataRow["ACCT_NO"].ToString();

                ComboBoxItem ACCT_NM_ITEM = new ComboBoxItem(string.Format("{0}({1})", ACCT_NM, ACCT_NO), ACCT_NO);

                BankNameComboBox.Items.Add(ACCT_NM_ITEM);
            }
            if (BankNameComboBox.Items.Count > 0)
                BankNameComboBox.SelectedIndex = 0;
        }

        // 분개 저장
        private void saveButton_Click(object sender, EventArgs e) {
            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            // 차변 총가격
            double tot_debitAcount = double.Parse(DebitAmountTextBox.Text.Replace(",", ""));
            //int tot_debitAcount = 0;
            //foreach (TextBox box in _DebitPriceTextBoxArray) {
            //    tot_debitAcount += int.Parse(box.Text.Trim());
            //}

            // 대변 총가격
            double tot_creditAcount = double.Parse(CreditAmountTextBox.Text.Replace(",", ""));
            //int tot_creditAcount = 0;
            //foreach (TextBox box in _CreditPriceTextBoxArray) {
            //    tot_creditAcount += int.Parse(box.Text.Trim());
            //}

            // 차변
            List<string> DEBIT_SLIP_CNMB = new List<string>();      // 전표일련번호
            List<string> DEBIT_ACNT_TIT_CD = new List<string>();    // 계정과목코드
            List<string> DEBIT_SLIP_WON_AMT = new List<string>();       // 전표원화금액
            List<string> DEBIT_SLIP_FRCR_AMT = new List<string>();      // 전표외화금액


            // 대변
            List<string> CREDIT_SLIP_CNMB = new List<string>();      // 전표일련번호
            List<string> CREDIT_ACNT_TIT_CD = new List<string>();    // 계정과목코드
            List<string> CREDIT_SLIP_WON_AMT = new List<string>();       // 전표원화금액
            List<string> CREDIT_SLIP_FRCR_AMT = new List<string>();      // 전표외화금액


            // 차변 <-> 대변 금액 불일치
            if (tot_debitAcount != tot_creditAcount)
                MessageBox.Show("차변/대변 금액이 일치하지 않습니다.");
            // 차변 <-> 대변 금액 일치
            else {
                if (_DebitArrayCount < 1) {
                    MessageBox.Show("하나 이상의 차변이 필요합니다.");
                    return;
                }

                if (_CreditArrayCount < 1 ) {
                    MessageBox.Show("하나 이상의 대변이 필요합니다.");
                    return;
                }

                string query = "";
                DataSet dataSet = null;

                SLIP_DT = slipDateTimePicker.Value.ToString("yyyy-MM-dd");
                REMK_CNTS = debitSlipDescriptionTextBox.Text.Trim();
                TRAN_EXRT = TRAN_EXRT_TextBox.Text.Trim();
                string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;
                string ACCT_NO = Utils.GetSelectedComboBoxItemValue(BankNameComboBox);

                // =====================================================================================
                //                  전표번호 텍스트박스에 빈값이면 새로운 전표 등록
                // =====================================================================================
                if (slipNoTextBox.Text.Equals(""))
                {
                    // =====================================================================================
                    //                                      전표 번호 MAX
                    // =====================================================================================
                    query = string.Format("SELECT MAX(SLIP_NO) + 1 as MAX_SLIP_NO " +
                                          "FROM   db_gbridge_trip.TB_DALY_SLIP_D");
                    SLIP_NO = int.Parse(DbHelper.GetValue(query, "MAX_SLIP_NO", "0"));


                    // =====================================================================================
                    //                                      차변 등록
                    // =====================================================================================
                    int i = 0;
                    for (i = 0; i < _DebitAddButtonArray.Count; i++)
                    {

                        // Default 세팅
                        if (i == 0)
                        {
                            DBCR_DVSN_CD = "1";
                            CASH_TRNS_DVSN_CD = "2";
                            RCKO_DT = DateTime.Now.ToString("yyyy-MM-dd");
                            RPSB_EMPL_NO = Global.loginInfo.EMPL_NO;
                            RSVT_NO = "1";

                            if (ACCT_NO.Equals("763-60-1126-58"))
                            {
                                CUR_CD = "KRW";
                            }
                            else
                            {
                                CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);
                            }
                            
                        }

                        DEBIT_SLIP_CNMB.Add((i + 1).ToString());
                        DEBIT_ACNT_TIT_CD.Add(Utils.GetSelectedComboBoxItemValue(_DebitAccountTitCodeComboBoxArray[i]));
                        DEBIT_SLIP_WON_AMT.Add(Utils.removeComma(_DebitWonAmountTextBoxArray[i].Text.Trim()));
                        DEBIT_SLIP_FRCR_AMT.Add(Utils.removeComma(_DebitFrcrAmountTextBoxArray[i].Text.Trim()));

                        // 차변 INSERT
                        query = string.Format("CALL InsertDailySlip('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                            SLIP_DT, SLIP_NO, DEBIT_SLIP_CNMB[i], DEBIT_ACNT_TIT_CD[i], DBCR_DVSN_CD, CASH_TRNS_DVSN_CD, CUR_CD, DEBIT_SLIP_WON_AMT[i], DEBIT_SLIP_FRCR_AMT[i], TRAN_EXRT, null, ACCT_NO, SLIP_DT, null, RPSB_EMPL_NO, RSVT_NO, REMK_CNTS, FRST_RGTR_ID);

                        int retVal = DbHelper.ExecuteNonQuery(query);
                        if (retVal == -1)
                        {
                            MessageBox.Show("분개데이터를 저장할 수 없습니다.");
                            return;
                        }
                    }

                    // =====================================================================================
                    //                                      대변 등록
                    // =====================================================================================
                    // 전표일련번호 --> 차변 일련번호 MAX + 1
                    int LAST_SLIP_CNMB = int.Parse(DEBIT_SLIP_CNMB[i - 1]) + 1;
                    for (int j = 0; j < _CreditAddButtonArray.Count; j++)
                    {
                        // Default 세팅
                        if (j == 0)
                        {
                            DBCR_DVSN_CD = "2";
                            CASH_TRNS_DVSN_CD = "2";
                            RCKO_DT = DateTime.Now.ToString("yyyy-MM-dd");
                            RPSB_EMPL_NO = Global.loginInfo.EMPL_NO;
                            RSVT_NO = "1";

                            if (ACCT_NO.Equals("763-60-1126-58"))
                            {
                                CUR_CD = "KRW";
                            }
                            else
                            {
                                CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);
                            }

                        }

                        CREDIT_SLIP_CNMB.Add((LAST_SLIP_CNMB).ToString());
                        CREDIT_ACNT_TIT_CD.Add(Utils.GetSelectedComboBoxItemValue(_CreditAccountTitCodeComboBoxArray[j]));
                        CREDIT_SLIP_WON_AMT.Add(Utils.removeComma(_CreditWonAmountTextBoxArray[j].Text.Trim()));
                        CREDIT_SLIP_FRCR_AMT.Add(Utils.removeComma(_CreditFrcrAmountTextBoxArray[j].Text.Trim()));

                        // 대변 INSERT
                        query = string.Format("CALL InsertDailySlip('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                            SLIP_DT, SLIP_NO, CREDIT_SLIP_CNMB[j], CREDIT_ACNT_TIT_CD[j], DBCR_DVSN_CD, CASH_TRNS_DVSN_CD, CUR_CD, CREDIT_SLIP_WON_AMT[j], CREDIT_SLIP_FRCR_AMT[j], TRAN_EXRT, null, ACCT_NO, SLIP_DT, null, RPSB_EMPL_NO, RSVT_NO, REMK_CNTS, FRST_RGTR_ID);

                        int retVal = DbHelper.ExecuteNonQuery(query);
                        if (retVal == -1)
                        {
                            MessageBox.Show("분개데이터를 저장할 수 없습니다.");
                            return;
                        }
                        else if (retVal == 1 && (j == _CreditAddButtonArray.Count - 1))
                        {
                            MessageBox.Show("분개데이터를 저장했습니다.");
                            SearchDailySlipList(); // 등록 후 그리드를 최신상태로 ReFresh
                        }

                        LAST_SLIP_CNMB++;
                    }
                }
                // =====================================================================================
                //                  전표번호 텍스트박스에 빈값이 아니므로 기존의 전표 수정
                // =====================================================================================
                else
                {
                    /*
                     * SLIP_DT, RCK_DT
                        SLIP_CNMB
                        ACNT_TIT_CD
                        SLIP_AMT
                        ACCT_NO
                        REMK_CNTS
                        FINL_MDFC_DTM
                        FINL_MDFR_ID
                     * */
                    int MAX_SLIP_CNMB = _DebitAddButtonArray.Count + _CreditAddButtonArray.Count;
                    SLIP_NO = int.Parse(slipNoTextBox.Text.Trim());
                    SLIP_DT = slipDateTimePicker.Value.ToString("yyyy-MM-dd");
                    RCKO_DT = SLIP_DT;
                    ACCT_NO = Utils.GetSelectedComboBoxItemValue(BankNameComboBox);
                    REMK_CNTS = debitSlipDescriptionTextBox.Text.Trim();

                    if (ACCT_NO.Equals("763-60-1126-58"))
                        CUR_CD = "KRW";
                    else
                        CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);
                    RPSB_EMPL_NO = Global.loginInfo.EMPL_NO;

                    

                    string FINL_MDFR_ID = Global.loginInfo.ACNT_ID;                         // 최종변경자ID
                    string RGST_DTM = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");         // 최초+최종 등록일시

                    for (int i = 0; i < _DebitAddButtonArray.Count; i++)
                    {
                        SLIP_CNMB = (i + 1).ToString();
                        ACNT_TIT_CD = Utils.GetSelectedComboBoxItemValue(_DebitAccountTitCodeComboBoxArray[i]);
                        SLIP_WON_AMT = Utils.removeComma(_DebitWonAmountTextBoxArray[i].Text.Trim());
                        SLIP_FRCR_AMT = Utils.removeComma(_DebitFrcrAmountTextBoxArray[i].Text.Trim());
                        DBCR_DVSN_CD = "1";

                        query = string.Format("CALL UpdateDailySlip('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')",
                            SLIP_NO, SLIP_DT, RCKO_DT, ACCT_NO, REMK_CNTS, SLIP_CNMB, ACNT_TIT_CD, SLIP_WON_AMT, SLIP_FRCR_AMT, TRAN_EXRT, FINL_MDFR_ID, RGST_DTM, DBCR_DVSN_CD, MAX_SLIP_CNMB, RPSB_EMPL_NO, CUR_CD);
                        int retVal = DbHelper.ExecuteNonQuery(query);
                        if (retVal == -1)
                        {
                            MessageBox.Show("분개데이터를 수정할 수 없습니다1.");
                            return;
                        }
                    }

                    int CREDIT_DISTINCT_NUM = _DebitAddButtonArray.Count + 1;
                    for (int j = 0; j < _CreditAddButtonArray.Count; j++)
                    {
                        CREDIT_DISTINCT_NUM += j;
                        SLIP_CNMB = CREDIT_DISTINCT_NUM.ToString();
                        ACNT_TIT_CD = Utils.GetSelectedComboBoxItemValue(_CreditAccountTitCodeComboBoxArray[j]);
                        SLIP_WON_AMT = Utils.removeComma(_CreditWonAmountTextBoxArray[j].Text.Trim());
                        SLIP_FRCR_AMT = Utils.removeComma(_CreditFrcrAmountTextBoxArray[j].Text.Trim());
                        DBCR_DVSN_CD = "2";

                        query = string.Format("CALL UpdateDailySlip('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')",
                            SLIP_NO, SLIP_DT, RCKO_DT, ACCT_NO, REMK_CNTS, SLIP_CNMB, ACNT_TIT_CD, SLIP_WON_AMT, SLIP_FRCR_AMT, TRAN_EXRT, FINL_MDFR_ID, RGST_DTM, DBCR_DVSN_CD, MAX_SLIP_CNMB, RPSB_EMPL_NO, CUR_CD);
                        int retVal = DbHelper.ExecuteNonQuery(query);
                        if (retVal == -1)
                        {
                            MessageBox.Show("분개데이터를 수정할 수 없습니다2.");
                            return;
                        }
                        //else if (retVal == 1 && (j == _CreditAddButtonArray.Count - 1))
                        //{
                        //    MessageBox.Show("분개데이터를 수정했습니다.");
                        //    SearchDailySlipList(); // 등록 후 그리드를 최신상태로 ReFresh
                        //}
                    }

                    query = string.Format("CALL DeleteDailySlipRowsAfterUpdate('{0}','{1}')", SLIP_NO, MAX_SLIP_CNMB);
                    int retVar = DbHelper.ExecuteNonQuery(query);
                    if (retVar == -1)
                    {
                        MessageBox.Show("분개데이터를 수정할 수 없습니다3.");
                    }
                    else if (retVar == 1)
                    {
                        MessageBox.Show("분개데이터를 수정했습니다.");
                    }
                    SearchDailySlipList();
                }
                // 차변/대변 동적 컨트롤 초기화
                DynamicControlResetInit();
            }
        }


        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        // 일별전표검색 버튼 클릭
        private void SearchSlipList_Click(object sender, EventArgs e) {
            SearchDailySlipList();
        }

        // 일별전표내역 검색
        private void SearchDailySlipList() {
            grdSlipDataGridView.Rows.Clear();

            ACNT_TIT_CD = Utils.GetSelectedComboBoxItemValue(searchAccountTitleComboBox);
            string BEFORE_SLIP_DT = searchSlipDateTimePicker_Before.Value.ToString("yyyy-MM-dd 00:00:00");
            string AFTER_SLIP_DT = searchSlipDateTimePicker_After.Value.ToString("yyyy-MM-dd 23:59:59");

            string query = string.Format("CALL SelectDailySlipList ('{0}', '{1}', '{2}')", ACNT_TIT_CD, BEFORE_SLIP_DT, AFTER_SLIP_DT);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0) {
                MessageBox.Show("일별전표내역 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows) {
                SLIP_DT = dataRow["SLIP_DT"].ToString().Substring(0, 10);
                SLIP_NO = int.Parse(dataRow["SLIP_NO"].ToString());
                SLIP_CNMB = dataRow["SLIP_CNMB"].ToString();
                ACNT_TIT_CD = dataRow["ACNT_TIT_CD"].ToString();
                ACNT_TIT_NM = dataRow["ACNT_TIT_NM"].ToString();
                DBCR_DVSN_CD = dataRow["DBCR_DVSN_CD"].ToString();
                CASH_TRNS_DVSN_CD = dataRow["CASH_TRNS_DVSN_CD"].ToString();
                CUR_CD = dataRow["CUR_CD"].ToString();
                SLIP_WON_AMT = dataRow["SLIP_WON_AMT"].ToString();
                SLIP_FRCR_AMT = dataRow["SLIP_FRCR_AMT"].ToString();
                TRAN_EXRT = dataRow["TRAN_EXRT"].ToString();
                ACCT_NO = dataRow["ACCT_NO"].ToString();
                ACCT_NM = dataRow["ACCT_NM"].ToString();
                //CUST_NO = dataRow["CUST_NO"].ToString();
                RCKO_DT = dataRow["RCKO_DT"].ToString().Substring(0, 10);
                DPWH_CNMB = dataRow["DPWH_CNMB"].ToString();
                RPSB_EMPL_NO = dataRow["RPSB_EMPL_NO"].ToString();
                EMPL_NM = dataRow["EMPL_NM"].ToString();
                RSVT_NO = dataRow["RSVT_NO"].ToString();
                REMK_CNTS = dataRow["REMK_CNTS"].ToString();

                if (DBCR_DVSN_CD.Equals("1"))
                    DBCR_DVSN_NM = "차변";
                else
                    DBCR_DVSN_NM = "대변";

                if (CASH_TRNS_DVSN_CD.Equals("1"))
                    CASH_TRNS_DVSN_NM = "현금";
                else
                    CASH_TRNS_DVSN_NM = "대체";

                grdSlipDataGridView.Rows.Add(
                    SLIP_DT,
                    SLIP_NO,
                    SLIP_CNMB,
                    REMK_CNTS,
                    ACNT_TIT_NM,
                    ACNT_TIT_CD,
                    DBCR_DVSN_NM,
                    DBCR_DVSN_CD,
                    double.Parse(SLIP_WON_AMT),
                    double.Parse(SLIP_FRCR_AMT),
                    //Utils.SetComma(Double.Parse(SLIP_WON_AMT), 0),
                    //Utils.SetComma(Double.Parse(SLIP_FRCR_AMT), 2),
                    double.Parse(TRAN_EXRT),
                    ACCT_NO,
                    ACCT_NM,
                    CASH_TRNS_DVSN_NM,
                    CASH_TRNS_DVSN_CD,
                    CUR_CD,
                    //CUST_NO,
                    RCKO_DT,
                    DPWH_CNMB,
                    EMPL_NM,
                    RPSB_EMPL_NO,
                    RSVT_NO
                    );

            }
            grdSlipDataGridView.ClearSelection();

        }

        private void grdSlipDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            //IS_FROM_GRIDVIEW = true;
            if (grdSlipDataGridView.SelectedRows.Count == 0)
                return;

            SLIP_NO = int.Parse(grdSlipDataGridView.SelectedRows[0].Cells[(int)egrdSlipDataGridView.SLIP_NO].Value.ToString());

            string query = string.Format("CALL SelectDailySlipBySlipNo ('{0}')", SLIP_NO);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("일일전표 목록을 가져올 수 없습니다.");
                return;
            }
            DynamicControlResetInit();

            DebitFlowLayoutPanel.SuspendLayout();
            CreditFlowLayoutPanel.SuspendLayout();


            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                SLIP_DT = dataRow["SLIP_DT"].ToString().Substring(0, 10);
                SLIP_NO = int.Parse(dataRow["SLIP_NO"].ToString());
                SLIP_CNMB = dataRow["SLIP_CNMB"].ToString();
                ACNT_TIT_CD = dataRow["ACNT_TIT_CD"].ToString();
                ACNT_TIT_NM = dataRow["ACNT_TIT_NM"].ToString();
                DBCR_DVSN_CD = dataRow["DBCR_DVSN_CD"].ToString();
                CASH_TRNS_DVSN_CD = dataRow["CASH_TRNS_DVSN_CD"].ToString();
                CUR_CD = dataRow["CUR_CD"].ToString();
                SLIP_WON_AMT = dataRow["SLIP_WON_AMT"].ToString();
                SLIP_FRCR_AMT = dataRow["SLIP_FRCR_AMT"].ToString();
                TRAN_EXRT = dataRow["TRAN_EXRT"].ToString();
                ACCT_NO = dataRow["ACCT_NO"].ToString();
                ACCT_NM = dataRow["ACCT_NM"].ToString();
                //CUST_NO = dataRow["CUST_NO"].ToString();
                RCKO_DT = dataRow["RCKO_DT"].ToString().Substring(0, 10);
                DPWH_CNMB = dataRow["DPWH_CNMB"].ToString();
                RPSB_EMPL_NO = dataRow["RPSB_EMPL_NO"].ToString();
                EMPL_NM = dataRow["EMPL_NM"].ToString();
                RSVT_NO = dataRow["RSVT_NO"].ToString();
                REMK_CNTS = dataRow["REMK_CNTS"].ToString();

                slipNoTextBox.Text = SLIP_NO.ToString();
                Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CUR_CD);
                Utils.SelectComboBoxItemByValue(BankNameComboBox, ACCT_NO);

                TRAN_EXRT_TextBox.Text = TRAN_EXRT;
                slipDateTimePicker.Text = SLIP_DT;


                //IS_FROM_GRIDVIEW = false;

                // 차변의 개수
                int DBIT_CNT = int.Parse(dataRow["DBIT_CNT"].ToString());
                // 대변의 개수
                int CRDT_CNT = int.Parse(dataRow["CRDT_CNT"].ToString());
                int MAX_SLIP_CNMB = int.Parse(dataRow["MAX_SLIP_CNMB"].ToString());

                // 공통 작업
                // 기존의 동적Control 삭제 먼저 작업, 1개는 기본 설정됨.

                // 차변의 경우 DebitFlowLayoutPanel에 세팅
                if (DBCR_DVSN_CD.Equals("1"))
                {
                    // 차변의 개수만큼 Control 생성후 데이터 입력
                    if (SLIP_CNMB.Equals("1"))
                    {
                        Utils.SelectComboBoxItemByValue(_DebitAccountTitCodeComboBoxArray[0], ACNT_TIT_CD);
                        _DebitWonAmountTextBoxArray[0].Text = Utils.SetComma(SLIP_WON_AMT);
                        _DebitFrcrAmountTextBoxArray[0].Text = Utils.SetComma(SLIP_FRCR_AMT);
                        debitSlipDescriptionTextBox.Text = REMK_CNTS;
                    }
                    else
                    {
                        fl = new FlowLayoutPanel();
                        fl.FlowDirection = FlowDirection.LeftToRight;
                        fl.Name = $"fl{int.Parse(SLIP_CNMB) - 1}";
                        fl.Size = new Size(740, 33);
                        fl.BorderStyle = BorderStyle.None;
                        fl_Debit_list.Add(fl);

                        // 계정과목 라벨
                        createDebitAccountTitleLabel(int.Parse(SLIP_CNMB), _DebitXPoint1, _DebitYPoint + 3);
                        // 차변일련번호 list
                        _DebitCNMBTextBoxArray.Add("");
                        // 계정과목 콤보박스
                        createDebitAccountTitleComboBox(int.Parse(SLIP_CNMB), _DebitXPoint2, _DebitYPoint);
                        // 원화금액 라벨
                        createDebitWonAmountLabel(int.Parse(SLIP_CNMB), _DebitXPoint3, _DebitYPoint + 3);
                        // 원화금액 텍스트박스
                        createDebitWonAmountTextBox(int.Parse(SLIP_CNMB), _DebitXPoint4, _DebitYPoint);
                        // 외화금액 라벨
                        createDebitFrcrAmountLabel(int.Parse(SLIP_CNMB), _DebitXPoint5, _DebitYPoint + 3);
                        // 외화금액 텍스트박스
                        createDebitFrcrAmountTextBox(int.Parse(SLIP_CNMB), _DebitXPoint6, _DebitYPoint);
                        // +버튼
                        createDebitPlusButton(int.Parse(SLIP_CNMB), _DebitXPoint7, _DebitYPoint);
                        // -버튼
                        createDebitMinusButton(int.Parse(SLIP_CNMB), _DebitXPoint8, _DebitYPoint);

                        Utils.SelectComboBoxItemByValue(_DebitAccountTitCodeComboBoxArray[int.Parse(SLIP_CNMB) - 1], ACNT_TIT_CD);
                        _DebitWonAmountTextBoxArray[int.Parse(SLIP_CNMB) - 1].Text = SLIP_WON_AMT;
                        _DebitFrcrAmountTextBoxArray[int.Parse(SLIP_CNMB) - 1].Text = SLIP_FRCR_AMT;

                        //fl_Debit_list.Add(fl);
                        DebitFlowLayoutPanel.Controls.Add(fl);
                    }
                } 
                // 대변의 경우 CreditFlowLayoutPanel에 세팅
                else
                {
                    // 대변의 개수만큼 Control 생성후 데이터 입력
                    // 대변 init수
                    int CREDIT_DISTINCT_NUM = MAX_SLIP_CNMB - CRDT_CNT + 1;

                    // 대변의 첫번째 행
                    if (CREDIT_DISTINCT_NUM.Equals(int.Parse(SLIP_CNMB)))
                    {
                        Utils.SelectComboBoxItemByValue(_CreditAccountTitCodeComboBoxArray[0], ACNT_TIT_CD);
                        _CreditWonAmountTextBoxArray[0].Text = Utils.SetComma(SLIP_WON_AMT);
                        _CreditFrcrAmountTextBoxArray[0].Text = Utils.SetComma(SLIP_FRCR_AMT);
                    }
                    else
                    {
                        fl = new FlowLayoutPanel();
                        fl.FlowDirection = FlowDirection.LeftToRight;
                        fl.Name = $"fl{int.Parse(SLIP_CNMB) - 1}";
                        fl.Size = new Size(740, 33);
                        fl.BorderStyle = BorderStyle.None;
                        fl_Credit_list.Add(fl);

                        // 계정과목 라벨
                        createCreditAccountTitleLabel(int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM + 1, _CreditXPoint1, _CreditYPoint + 3);
                        // 차변일련번호 list
                        _CreditCNMBTextBoxArray.Add("");
                        // 계정과목 콤보박스
                        createCreditAccountTitleComboBox(int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM + 1, _CreditXPoint2, _CreditYPoint);
                        // 원화금액 라벨
                        createCreditWonAmountLabel(int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM + 1, _CreditXPoint3, _CreditYPoint + 3);
                        // 원화금액 텍스트박스
                        createCreditWonAmountTextBox(int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM + 1, _CreditXPoint4, _CreditYPoint);
                        // 외화금액 라벨
                        createCreditFrcrAmountLabel(int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM + 1, _CreditXPoint5, _CreditYPoint + 3);
                        // 외화금액 텍스트박스
                        createCreditFrcrAmountTextBox(int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM + 1, _CreditXPoint6, _CreditYPoint);
                        // +버튼
                        createCreditPlusButton(int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM + 1, _CreditXPoint7, _CreditYPoint);
                        // -버튼
                        createCreditMinusButton(int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM + 1, _CreditXPoint8, _CreditYPoint);

                        Utils.SelectComboBoxItemByValue(_CreditAccountTitCodeComboBoxArray[int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM], ACNT_TIT_CD);
                        _CreditWonAmountTextBoxArray[int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM].Text = SLIP_WON_AMT;
                        _CreditFrcrAmountTextBoxArray[int.Parse(SLIP_CNMB) - CREDIT_DISTINCT_NUM].Text = SLIP_FRCR_AMT;

                        CreditFlowLayoutPanel.Controls.Add(fl);
                    }
                }
            }

            DebitFlowLayoutPanel.ResumeLayout();
            CreditFlowLayoutPanel.ResumeLayout();
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("선택한 전표의 차대변을 모두 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            SLIP_NO = int.Parse(grdSlipDataGridView.SelectedRows[0].Cells[(int)egrdSlipDataGridView.SLIP_NO].Value.ToString());

            string query = string.Format("CALL DeleteDailySlipRows ('{0}')", SLIP_NO);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("전표를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                MessageBox.Show("해당 전표를 삭제했습니다.");
                DynamicControlResetInit();
                SearchDailySlipList();
            }
        }

        // 초기화
        private void resetButton_Click(object sender, EventArgs e) {
            DynamicControlResetInit();
            debitSlipDescriptionTextBox.Text = "";
        }

        private void DynamicControlResetInit() {
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 동적 컨트롤 삭제
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            DebitFlowLayoutPanel.SuspendLayout();
            CreditFlowLayoutPanel.SuspendLayout();
            
            fl_Debit_list.Clear();
            fl_Credit_list.Clear();

            _DebitArrayCount = 1;

            _DebitCNMBTextBoxArray.Clear();                     // 차변일련번호
            _DebitAccountTitCodeLabelArray.Clear();               // 계정과목라벨
            _DebitAccountTitCodeComboBoxArray.Clear();        // 옵션명
            _DebitWonAmountLabelArray.Clear();                        // 계정과목라벨
            _DebitWonAmountTextBoxArray.Clear();                 // 옵션명
            _DebitFrcrAmountLabelArray.Clear();                        // 계정과목라벨
            _DebitFrcrAmountTextBoxArray.Clear();                 // 옵션명
            _DebitAddButtonArray.Clear();                      // +버튼
            _DebitEraseButtonArray.Clear();                    // -버튼

            _CreditArrayCount = 1;

            _CreditCNMBTextBoxArray.Clear();                     // 대변일련번호
            _CreditAccountTitCodeLabelArray.Clear();               // 계정과목라벨
            _CreditAccountTitCodeComboBoxArray.Clear();        // 옵션명
            _CreditWonAmountLabelArray.Clear();                        // 계정과목라벨
            _CreditWonAmountTextBoxArray.Clear();                 // 옵션명
            _CreditFrcrAmountLabelArray.Clear();                        // 계정과목라벨
            _CreditFrcrAmountTextBoxArray.Clear();                 // 옵션명
            _CreditAddButtonArray.Clear();                      // +버튼
            _CreditEraseButtonArray.Clear();                    // -버튼

            CreditFlowLayoutPanel.Controls.Clear();
            DebitFlowLayoutPanel.Controls.Clear();

            setDefaultCreditInfoLine();
            setDefaultDebitInfoLine();

            DebitAmountTextBox.Text = "";
            CreditAmountTextBox.Text = "";
            slipNoTextBox.Text = "";
            TRAN_EXRT_TextBox.Text = "0";

            DebitFlowLayoutPanel.ResumeLayout();
            CreditFlowLayoutPanel.ResumeLayout();
        }

        private void debitSlipDescriptionTextBox_TextChanged(object sender, EventArgs e) {

        }

        private bool CheckRequireItems() {
            if (debitSlipDescriptionTextBox.Text.Trim() == "") {
                MessageBox.Show("적요를 입력해주세요.");
                return false;
            }

            if (BankNameComboBox.Text.Trim() == "") {
                MessageBox.Show("계좌명을 입력해주세요");
                return false;
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 차변 금액 합계
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void AddDebitAmount() {
            double tot_debitAcount = 0.0;
            foreach (TextBox prices in _DebitWonAmountTextBoxArray) {
                if (prices.Text != null && prices.Text != "")
                    tot_debitAcount += double.Parse(prices.Text.Trim());
            }
            DebitAmountTextBox.Text = Utils.SetComma(tot_debitAcount, 2);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 대변 금액 합계
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void AddCreditAmount() {
            double tot_creditAcount = 0.0;
            foreach (TextBox prices in _CreditWonAmountTextBoxArray) {
                if (prices.Text != null && prices.Text != "")
                    tot_creditAcount += double.Parse(prices.Text.Trim());
            }
            CreditAmountTextBox.Text = Utils.SetComma(tot_creditAcount, 2);
        }

        private void DebitAmountTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        private void CreditAmountTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        // 외화통장 선택시, 통화코드 입력창 Visible
        private void BankNameComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            // 원화 통장
            if (!Utils.GetSelectedComboBoxItemValue(BankNameComboBox).Equals("763-60-1117-53"))
            {
                CurrencyCodeLabel.Visible = false;
                CurrencyCodeComboBox.Visible = false;
                TRAN_EXRT_Label.Visible = false;
                TRAN_EXRT_TextBox.Visible = false;

                Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, "KRW");
            }
            // 외화 통장
            else
            {
                CurrencyCodeLabel.Visible = true;
                CurrencyCodeComboBox.Visible = true;
                TRAN_EXRT_Label.Visible = true;
                TRAN_EXRT_TextBox.Visible = true;

                Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, "USD");
            }

            //if (IS_FROM_GRIDVIEW)
            //    SetDynamicControlsNonTextTouch();
            //else
            SetDynamicControls();
        }

        // 통화코드 변경시, 원화외화 컨트롤 구분
        private void CurrencyCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void SetDynamicControls()
        {
            // 한화의 경우
            if (Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            {
                // 외화 0, Enable == false처리
                foreach (TextBox box in _DebitFrcrAmountTextBoxArray)
                {
                    box.Text = "0";
                    box.Enabled = false;
                }

                foreach (TextBox box in _CreditFrcrAmountTextBoxArray)
                {
                    box.Text = "0";
                    box.Enabled = false;
                }

                // 원화금액부분이 Disabled되있는 경우 풀기
                foreach (TextBox box in _DebitWonAmountTextBoxArray)
                {
                    if (box.Enabled == false)
                        box.Enabled = true;
                }

                foreach (TextBox box in _CreditWonAmountTextBoxArray)
                {
                    if (box.Enabled == false)
                        box.Enabled = true;
                }
            }
            // 외화의 경우
            else
            {
                // 원화 Enable == false 처리(환율 X 외화금액은 여기서 처리 안함)
                foreach (TextBox box in _DebitWonAmountTextBoxArray)
                {
                    box.Text = "0";
                    box.Enabled = false;
                }

                foreach (TextBox box in _CreditWonAmountTextBoxArray)
                {
                    box.Text = "0";
                    box.Enabled = false;
                }

                // 외화금액부분이 Disabled되있는 경우 풀기
                foreach (TextBox box in _DebitFrcrAmountTextBoxArray)
                {
                    if (box.Enabled == false)
                        box.Enabled = true;
                }

                foreach (TextBox box in _CreditFrcrAmountTextBoxArray)
                {
                    if (box.Enabled == false)
                        box.Enabled = true;
                }
            }
        }

        private void SetDynamicControlsNonTextTouch()
        {
            // 한화의 경우
            if (Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            {
                // 외화 0, Enable == false처리
                foreach (TextBox box in _DebitFrcrAmountTextBoxArray)
                {
                    box.Enabled = false;
                }

                foreach (TextBox box in _CreditFrcrAmountTextBoxArray)
                {
                    box.Enabled = false;
                }

                // 원화금액부분이 Disabled되있는 경우 풀기
                foreach (TextBox box in _DebitWonAmountTextBoxArray)
                    if (box.Enabled == false)
                        box.Enabled = true;

                foreach (TextBox box in _CreditWonAmountTextBoxArray)
                    if (box.Enabled == false)
                        box.Enabled = true;
            }
            // 외화의 경우
            else
            {
                // 원화 Enable == false 처리(환율 X 외화금액은 여기서 처리 안함)
                foreach (TextBox box in _DebitWonAmountTextBoxArray)
                {
                    box.Enabled = false;
                }

                foreach (TextBox box in _CreditWonAmountTextBoxArray)
                {
                    box.Enabled = false;
                }

                // 외화금액부분이 Disabled되있는 경우 풀기
                foreach (TextBox box in _DebitFrcrAmountTextBoxArray)
                    if (box.Enabled == false)
                        box.Enabled = true;

                foreach (TextBox box in _CreditFrcrAmountTextBoxArray)
                    if (box.Enabled == false)
                        box.Enabled = true;
            }
        }

        private void TRAN_EXRT_TextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void exportExcelButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "파일 내보내기";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Trim() == "")
                return;

            this.Cursor = Cursors.WaitCursor;

            string filePath = saveFileDialog.FileName.Trim();
            string fileDirPath = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar));

            grdSlipDataGridView.SelectAll();

            if (Directory.Exists(fileDirPath))
            {
                //if (true == ExcelHelper.ExportExcel(filePath, reservationDataGridView))
                if (ExcelHelper.gridToExcel(filePath, grdSlipDataGridView) == true)
                    MessageBox.Show(string.Format("{0}\r\n파일을 저장했습니다.", filePath));
                else
                    MessageBox.Show("파일을 저장 할 수 없습니다.");
            }
            else
            {
                MessageBox.Show("잘못된 저장 경로입니다.");
            }

            this.Cursor = Cursors.Default;
        }

        // 환율 변경시 기존의 입력항목에도 원화에 반영
        private void TRAN_EXRT_TextBox_TextChanged(object sender, EventArgs e)
        {

            double CUR_CD = double.Parse(TRAN_EXRT_TextBox.Text.Trim());

            foreach (TextBox box in _DebitWonAmountTextBoxArray)
            {
                int tag = (int)box.Tag;
                double FRCR_PRICE = double.Parse(_DebitFrcrAmountTextBoxArray[tag - 1].Text);
                box.Text = Utils.SetComma(FRCR_PRICE * CUR_CD);
            }

            foreach (TextBox box in _CreditWonAmountTextBoxArray)
            {
                int tag = (int)box.Tag;
                double FRCR_PRICE = double.Parse(_CreditFrcrAmountTextBoxArray[tag - 1].Text);
                box.Text = Utils.SetComma(FRCR_PRICE * CUR_CD);
            }
        }
    }
}
