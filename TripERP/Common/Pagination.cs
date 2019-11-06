using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Data;

namespace TripERP.Common
{
    /*
     *  본 Class 는 Paging 처리를 위해 필요한 Methdo 를 구현해놓은 Class
     *  
     *  ▶ 전체기능
     *  ---------------------------------------------------------------
     *  1. GridView 주입 및 설정 (Paging 처리가 적용될 Component 설정)
     *  2. Paging 처리 변수 초기화
     *  3. Pagination 동적 출력 (Page 갯수만큼 출력)
     *  4. Page 별 보여질 Data 설정 (Cusor 설정)
     *  5. Column Header Design
     *  ---------------------------------------------------------------
     */
    class Pagination
    { 

        // ◈Paging 처리 변수 초기화
        //=================================================================
        public int CurrentPage { get; set; } = 1;                   // 현재 Page
        public int PagesCount { get; set; } = 1;                    // 총 Page 수
        public int PageRows { get; set; } = 20;                     // PageSize
        public BindingList<Promotion> Baselist { get; set; } = null;     // GridView DataSource (전체 게시물)
        public BindingList<Promotion> Templist { get; set; } = null;     // Page 별 보여질 게시물
        public DataGridView dataGridView { get; set; } = null;              // 게시물들이 출력될 Table
        //=================================================================


        // ◈GridView 주입(설정)
        // 생성자를 통해 설정
        //=================================================================
        public Pagination(DataGridView dataGridView_in)
        {
            this.dataGridView = dataGridView_in;
        }
        //=================================================================



        // ◈Pagination 숫자갱신 (=blocksize)
        //=====================================================================================================================================
        // C# 에서는 ToolScript 를 통하여 Paging 처리에 필요한 Component 를 생성한다.
        // 본 Method 는 Pagination 을 갱신 또는 노출, 숨김 효과를 주기 위해 작성되었으므로 Component 들을 매개변수로 받는다.
        public void RefreshPagination(ToolStripButton btnFirst, ToolStripButton btnBackward, ToolStripButton btnForward, ToolStripButton btnLast, ToolStripButton toolStripButton1, ToolStripButton toolStripButton2, ToolStripButton toolStripButton3, ToolStripButton toolStripButton4, ToolStripButton toolStripButton5)
        {
            ToolStripButton[] items = new ToolStripButton[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 };

            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;                                                                                             // Pagination 의 시작점을 의미
            int pageEndIndex = pageStartIndex + 5;                                                                   // Pagination 의 끝점을 의미
            PagesCount = int.Parse(Math.Ceiling(Baselist.Count / (double)PageRows).ToString()); // 전체 Data 를 PageSize 로 나누었을때 나오는 총 Page 수를 의미
            
            if (PagesCount > 5 && CurrentPage > 2)
                pageStartIndex = CurrentPage - 2;

            if (PagesCount > 5 && CurrentPage > PagesCount - 2)
                pageStartIndex = PagesCount - 4;
            
            for (int i = pageStartIndex; i < pageEndIndex; i++)                 // Pagination 의 동적 출력
            {
                if (i > PagesCount)                                                             // 전체 Page 개수를 넘어서는 Pagination 은 Hidden 처리
                {
                    items[i - pageStartIndex].Visible = false;
                }
                else
                {                    
                    //Changing the page numbers
                    items[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);      // Pagination 에 표기되는 수 설정
                    items[i - pageStartIndex].Visible = true;                                                       // i 가 전체 Page 개수를 넘어서지 않을 경우에는 모든 Pagination 을 출력

                    //Setting the Appearance of the page number buttons
                    // 현재 Page 에 대한 Graphic 효과 설정
                    if (i == CurrentPage)
                    {   // 전체 Pagination 에서 현재 Page 에 해당하는 Pagination 의 배경은 검은색, 글자는 흰색으로 설정
                        items[i - pageStartIndex].BackColor = Color.Black;
                        items[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else
                    {   // 현재 페이지와 같은 번째가 아닌 Pagination 은 배경은 흰색, 글자는 검은색으로 설정
                        items[i - pageStartIndex].BackColor = Color.White;
                        items[i - pageStartIndex].ForeColor = Color.Black;
                    }
                }
            }

            
            // 처음페이지, 이전페이지, 다음페이지, 끝페이지 이동 Button 활성화에 관한 설정
            if (CurrentPage == 1)
                btnBackward.Enabled = btnFirst.Enabled = false;
            else
                btnBackward.Enabled = btnFirst.Enabled = true;

            if (CurrentPage == PagesCount)
                btnForward.Enabled = btnLast.Enabled = false;

            else
                btnForward.Enabled = btnLast.Enabled = true;
        }
        //=====================================================================================================================================




        // ◈Page 에 따른 출력 Data 시작점 설정 (=Cusor)
        //=====================================================================================================================================
        public void RebindGridForPageChange()
        {
            //Rebinding the Datagridview with data
            int datasourcestartIndex = (CurrentPage - 1) * PageRows;
            Templist = new BindingList<Promotion>();
            for (int i = datasourcestartIndex; i < datasourcestartIndex + PageRows; i++)
            {
                if (i >= Baselist.Count)
                    break;

                Templist.Add(Baselist[i]);
            }

            dataGridView.DataSource = Templist;
            dataGridView.ClearSelection();                  // Data 그려질때 자동 선택 해제
            dataGridView.Refresh();            
        }
        //=====================================================================================================================================




        // ◈Table Header 설정
        //=====================================================================================================================================
        public void DataGridViewHeaderSetting()
        {
            dataGridView.AutoGenerateColumns = false;
                
            DataGridViewTextBoxColumn PRDT_PRMO_CD = new DataGridViewTextBoxColumn();
            PRDT_PRMO_CD.DataPropertyName = "PRDT_PRMO_CD";
            PRDT_PRMO_CD.HeaderText = "프로모션코드";
            PRDT_PRMO_CD.Visible = false;
            

            DataGridViewTextBoxColumn PRMO_NM = new DataGridViewTextBoxColumn();
            PRMO_NM.DataPropertyName = "PRMO_NM";
            PRMO_NM.HeaderText = "프로모션명";
            PRMO_NM.Width = 160;

            DataGridViewTextBoxColumn PRDT_CNMB = new DataGridViewTextBoxColumn();
            PRDT_CNMB.DataPropertyName = "PRDT_CNMB";
            PRDT_CNMB.HeaderText = "상품\n일련번호";
            PRDT_CNMB.Width = 80;
            PRDT_CNMB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            DataGridViewTextBoxColumn PRDT_NM = new DataGridViewTextBoxColumn();
            PRDT_NM.DataPropertyName = "PRDT_NM";
            PRDT_NM.HeaderText = "상품명";
            PRDT_NM.Width = 160;

            DataGridViewTextBoxColumn ADLT_PRMO_DSCT_RT = new DataGridViewTextBoxColumn();
            ADLT_PRMO_DSCT_RT.DataPropertyName = "ADLT_PRMO_DSCT_RT";
            ADLT_PRMO_DSCT_RT.HeaderText = "성인할인율";
            ADLT_PRMO_DSCT_RT.Width = 120;
            ADLT_PRMO_DSCT_RT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            DataGridViewTextBoxColumn CHLD_PRMO_DSCT_RT = new DataGridViewTextBoxColumn();
            CHLD_PRMO_DSCT_RT.DataPropertyName = "CHLD_PRMO_DSCT_RT";
            CHLD_PRMO_DSCT_RT.HeaderText = "소아할인율";
            CHLD_PRMO_DSCT_RT.Width = 120;
            CHLD_PRMO_DSCT_RT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn INFN_PRMO_DSCT_RT = new DataGridViewTextBoxColumn();
            INFN_PRMO_DSCT_RT.DataPropertyName = "INFN_PRMO_DSCT_RT";
            INFN_PRMO_DSCT_RT.HeaderText = "유아할인율";
            INFN_PRMO_DSCT_RT.Width = 120;
            INFN_PRMO_DSCT_RT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn ADLT_PRMO_DSCT_AMT = new DataGridViewTextBoxColumn();
            ADLT_PRMO_DSCT_AMT.DataPropertyName = "ADLT_PRMO_DSCT_AMT";
            ADLT_PRMO_DSCT_AMT.HeaderText = "성인할인금액";
            ADLT_PRMO_DSCT_AMT.Width = 135;
            ADLT_PRMO_DSCT_AMT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn CHLD_PRMO_DSCT_AMT = new DataGridViewTextBoxColumn();
            CHLD_PRMO_DSCT_AMT.DataPropertyName = "CHLD_PRMO_DSCT_AMT";
            CHLD_PRMO_DSCT_AMT.HeaderText = "소아할인금액";
            CHLD_PRMO_DSCT_AMT.Width = 135;
            CHLD_PRMO_DSCT_AMT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn INFN_PRMO_DSCT_AMT = new DataGridViewTextBoxColumn();
            INFN_PRMO_DSCT_AMT.DataPropertyName = "INFN_PRMO_DSCT_AMT";
            INFN_PRMO_DSCT_AMT.HeaderText = "유아할인금액";
            INFN_PRMO_DSCT_AMT.Width = 135;
            INFN_PRMO_DSCT_AMT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn APLY_LNCH_DT = new DataGridViewTextBoxColumn();
            APLY_LNCH_DT.DataPropertyName = "APLY_LNCH_DT";
            APLY_LNCH_DT.HeaderText = "적용개시일";
            APLY_LNCH_DT.Width = 130;


            DataGridViewTextBoxColumn APLY_END_DT = new DataGridViewTextBoxColumn();
            APLY_END_DT.DataPropertyName = "APLY_END_DT";
            APLY_END_DT.HeaderText = "적용종료일";
            APLY_END_DT.Width = 130;


            DataGridViewTextBoxColumn USE_YN = new DataGridViewTextBoxColumn();
            USE_YN.DataPropertyName = "USE_YN";
            USE_YN.HeaderText = "사용여부";
            USE_YN.Width = 90;

            dataGridView.Columns.Add(PRDT_PRMO_CD);
            dataGridView.Columns.Add(PRMO_NM);
            dataGridView.Columns.Add(PRDT_CNMB);
            dataGridView.Columns.Add(PRDT_NM);
            dataGridView.Columns.Add(ADLT_PRMO_DSCT_RT);
            dataGridView.Columns.Add(CHLD_PRMO_DSCT_RT);
            dataGridView.Columns.Add(INFN_PRMO_DSCT_RT);
            dataGridView.Columns.Add(ADLT_PRMO_DSCT_AMT);
            dataGridView.Columns.Add(CHLD_PRMO_DSCT_AMT);
            dataGridView.Columns.Add(INFN_PRMO_DSCT_AMT);
            dataGridView.Columns.Add(APLY_LNCH_DT);
            dataGridView.Columns.Add(APLY_END_DT);
            dataGridView.Columns.Add(USE_YN);
            
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("맑은고딕", 12);
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }
        //=====================================================================================================================================
    }
}
