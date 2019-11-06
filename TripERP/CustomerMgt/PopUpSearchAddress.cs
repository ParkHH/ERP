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
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripERP.CustomerMgt
{
    public partial class PopUpSearchAddress : Form
    {
        public enum eAddressListDataGridView
        {
            ZIPCD,                 // 우편번호
            ROADNM,                // 도로명 주소   
            ROADNM2,               // 도로명 참조주소           
            JIBUNADDR              // 지번주소            
        }

        private string ZIP_CD;             // 우편번호
        private string ROAD_NM;            // 도로명 주소
        private string ROAD_NM2;           //도로명 주소2 
        private string JIBUNADDR;          // 지번주소            

        private string count_per_page = "10";   // 한페이지 출력 개수
        private string search_page = "1";       // 검색대상 페이지
        private string total_count;             // 전체 검색건수
        private string total_page;              // 전체 페이지수
        private string currentPage;             // 현재 페이지
        private string SUCCESS_MESSAGE;         // 검색 Response 여부       
        
        public string get_zipcode()
        {
            return this.ZIP_CD;
        }

        public string get_roadname()
        {
            return this.ROAD_NM;
        }

        public string get_roadname2()
        {
            return this.ROAD_NM2;
        }

        public PopUpSearchAddress()
        {
            InitializeComponent();
        }

        // 폼 로딩 초기화
        private void PopupSearchAddress_Load(object sender, EventArgs e)
        {
            InitControls();
            ResetInputFormField();
        }

        // 초기화
        private void InitControls()
        {
            roadNameTextBox.Text = "";                     
            InitDataGridView();         // 그리드 스타일 초기화
        }

        private void InitDataGridView() {
        //{
        //    DataGridView dataGridView = addressListDataGridView;
        //    dataGridView.RowHeadersVisible = false;
        //    dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    dataGridView.MultiSelect = false;
        //    dataGridView.ColumnHeadersVisible = true;
        //    dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        // 그리드 더블클릭
        private void addressListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 행의 주소정보를 호출창으로 전달하고 폼을 닫는다.
            get_zipcode();
            get_roadname();
        }        

        private void ResetInputFormField()
        {
            SearchPagetextBox.Text = "";              // 페이지 표시창            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 도로명주소 검색
        private void searchAddressListButton_Click(object sender, EventArgs e)
        {
            search_page = "1";
            SearchPagetextBox.Text = search_page;
            searchAddressList(search_page);
        }

        private void searchAddressList(string search_page = "1")
        {            
            addressListDataGridView.Rows.Clear();               // 그리드뷰 클리어
            
            string Search_Keyword = "";                         // 검색어
            Search_Keyword = roadNameTextBox.Text.Trim();       // 검색어 저장            
            
            string responseFromServer = AddressCaller.CallingAddressInfo(Search_Keyword, search_page, count_per_page);   // 검색어 전달            

            string json = responseFromServer.Trim().Trim('(', ')');
            var jo = JObject.Parse(json);

            total_count = jo["results"]["common"]["totalCount"].ToString();         // 전체개수            
            string SUCCESS_MESSAGE = jo["results"]["common"]["errorMessage"].ToString();   // 정상여부
            currentPage = jo["results"]["common"]["currentPage"].ToString();        // 현재 페이지 

            int total_page_temp;
            total_page_temp = (int)Math.Ceiling((double)Convert.ToInt64(total_count) / (double)Convert.ToInt64(count_per_page));      // 전체 페이지 개수= 전체 검색 결과 / 한페이지당 출력건수                        

            total_page = total_page_temp.ToString();

            FinalPagetextBox.Text = total_page;           // 임시

            if (SUCCESS_MESSAGE == "정상" && Convert.ToInt64(total_count) > 0)
            {
                // 수신한 개수만큼 출력
                if(Convert.ToInt64(currentPage) != Convert.ToInt64(total_page))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ZIP_CD = jo["results"]["juso"][i]["zipNo"].ToString();
                        ROAD_NM = jo["results"]["juso"][i]["roadAddrPart1"].ToString();
                        ROAD_NM2 = jo["results"]["juso"][i]["roadAddrPart2"].ToString();
                        JIBUNADDR = jo["results"]["juso"][i]["jibunAddr"].ToString();
                        // grid에 출력
                        addressListDataGridView.Rows.Add
                        (
                            ZIP_CD,             // 우편번호
                            ROAD_NM,            // 도로명주소      
                            ROAD_NM2,           // 도로명주소2
                            JIBUNADDR           // 지번주소
                        );
                    }
                }
                // 마지막 페이지일 경우 개수만큼 출력
                else
                {
                    long remainder = Convert.ToInt64(total_count) % Convert.ToInt64(count_per_page);
                    for (int i = 0; i < remainder; i++)
                    {
                        ZIP_CD = jo["results"]["juso"][i]["zipNo"].ToString();
                        ROAD_NM = jo["results"]["juso"][i]["roadAddrPart1"].ToString();
                        ROAD_NM2 = jo["results"]["juso"][i]["roadAddrPart2"].ToString();
                        JIBUNADDR = jo["results"]["juso"][i]["jibunAddr"].ToString();

                        // grid에 출력
                        addressListDataGridView.Rows.Add
                        (
                            ZIP_CD,             // 우편번호
                            ROAD_NM,            // 도로명주소                        
                            ROAD_NM2,           // 도로명주소2
                            JIBUNADDR           // 지번주소
                        );
                    }
                }
                
            }
            else if (SUCCESS_MESSAGE == "정상" && Convert.ToInt64(total_count) == 0)
            {
                MessageBox.Show("검색결과가 없습니다.");
            }   
            else
            {
                MessageBox.Show("도로명 주소 사이트 연결상태가 좋지 않거나 검색데이터가 너무 많습니다.");
            }
        }

        // 다음 페이지 버튼클릭
        private void NextPage_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt64(search_page)>0 && Convert.ToInt64(search_page) < Convert.ToInt64(total_page))
            { 
                addressListDataGridView.Rows.Clear();               // 그리드뷰 클리어
                long search_page_temp;
                search_page_temp = Convert.ToInt64(search_page) + 1;
                search_page = search_page_temp.ToString();
                SearchPagetextBox.Text = search_page;
                searchAddressList(search_page);
            }
            else
            {
                MessageBox.Show("마지막 페이지 입니다.");
            }
        }
        
        // 마지막 페이지 버튼클릭
        private void LastPageButton_Click(object sender, EventArgs e)
        {
            addressListDataGridView.Rows.Clear();               // 그리드뷰 클리어
            long search_page_temp;
            search_page_temp = Convert.ToInt64(total_page);     // 마지막 페이지
            search_page = search_page_temp.ToString();
            SearchPagetextBox.Text = search_page;
            searchAddressList(search_page);
        }
        
        // 이전 페이지 버튼클릭
        private void PreviousPage_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(search_page) > 1)
            {
                addressListDataGridView.Rows.Clear();               // 그리드뷰 클리어
                long search_page_temp;
                search_page_temp = Convert.ToInt64(search_page) - 1;
                search_page = search_page_temp.ToString();
                SearchPagetextBox.Text = search_page;
                searchAddressList(search_page);
            }
            else
            {
                MessageBox.Show("첫 페이지 입니다.");
            }
        }

        // 첫 페이지 버튼클릭
        private void FirstPage_Click(object sender, EventArgs e)
        {
            addressListDataGridView.Rows.Clear();               // 그리드뷰 클리어
            long search_page_temp;
            search_page_temp = 1;                               // 첫 페이지
            search_page = search_page_temp.ToString();
            SearchPagetextBox.Text = search_page;
            searchAddressList(search_page);
        }

        private void Address_GridView_Cell_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (addressListDataGridView.SelectedRows.Count == 0)
                return;

            ZIP_CD = addressListDataGridView.SelectedRows[0].Cells[(int)eAddressListDataGridView.ZIPCD].Value.ToString();                  // 우편번호
            ROAD_NM = addressListDataGridView.SelectedRows[0].Cells[(int)eAddressListDataGridView.ROADNM].Value.ToString();                 // 도로명주소
            ROAD_NM2 = addressListDataGridView.SelectedRows[0].Cells[(int)eAddressListDataGridView.ROADNM2].Value.ToString();                 // 도로명주소
        }

        private void choiceCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Address_GridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = addressListDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && addressListDataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (addressListDataGridView.SelectedRows.Count == 0)
                return;

            ZIP_CD = addressListDataGridView.Rows[rowIndex].Cells[(int)eAddressListDataGridView.ZIPCD].Value.ToString();                  // 우편번호
            ROAD_NM = addressListDataGridView.Rows[rowIndex].Cells[(int)eAddressListDataGridView.ROADNM].Value.ToString();                // 도로명주소
            ROAD_NM2 = addressListDataGridView.Rows[rowIndex].Cells[(int)eAddressListDataGridView.ROADNM2].Value.ToString();             // 도로명참조주소
        }
    }
}