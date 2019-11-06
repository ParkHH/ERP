using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace TripERP.Common
{
    /**
     * Excel Helper by Jungil Lim
     * Version : 1.0.0
     */

    class ExcelHelper
    {
        static public bool gridToExcel(string saveFileName, DataGridView gridView)
        {
            Excel.Application excelApplication = null;
            Excel.Workbook excelWorkBook = null;
            Excel.Worksheet excelWorksheet = null;

            bool isSavedFile = false;
            bool retVal = false;

            try
            {
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                // 그리드를 전체 선택하여 클립보드에 복사 (해당 그리드가 전체 선택 가능하도록 해야 함) 
                // ClipboardCopyMode = EnableAlwaysIncludeHeaderText
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                gridView.MultiSelect = true;
                gridView.SelectAll();
                
                DataObject dataObject = gridView.GetClipboardContent();
                if (dataObject != null)
                {
                    Clipboard.SetDataObject(dataObject);
                }

                //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                // 엑셀에 붙여 넣기
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                object misValue = System.Reflection.Missing.Value;
                excelApplication = new Excel.Application();

                excelApplication.Visible = false;

                excelWorkBook = excelApplication.Workbooks.Add(misValue);
                excelWorksheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);
                Excel.Range range = (Excel.Range)excelWorksheet.Cells[1, 1];

                range.Select();

                excelWorksheet.PasteSpecial(range, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                excelWorkBook.SaveAs(saveFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
                string message = string.Format("Export 중 오류가 발생했습니다. ({0})", ex.Message + "[StackTrace]" + ex.StackTrace);

                MessageBox.Show(message);
                Console.WriteLine(message);
            }
            finally
            {
                excelWorkBook.Close(isSavedFile);
                excelApplication.Quit();

                ReleaseExcelObject(excelWorksheet);
                ReleaseExcelObject(excelWorkBook);
                ReleaseExcelObject(excelApplication);
            }

            return retVal;
        }


        static public bool ExportExcel(string saveFileName, DataGridView gridView)
        {
            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            bool isSavedFile = false;
            bool retVal = false;

            try
            {
                // Excel 파일 저장
                application = new Excel.Application();
                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.get_Item(1) as Excel.Worksheet;
                //application.Visible = false;

                // 전체 기본 폰트
                application.StandardFont = "맑은 고딕";
                application.StandardFontSize = 11;
                application.get_Range("A1").EntireColumn.NumberFormat = "@";


                // Column 출력
                for (int ci = 0; ci < gridView.Columns.Count; ci++)
                {
                    DataGridViewColumn gridCol = gridView.Columns[ci];
                    // CheckBox, Hidden Cell 타입은 출력하지 않는다. 
                    if (gridCol.GetType() == typeof(DataGridViewCheckBoxColumn) || gridCol.Visible == false)
                        continue;

                    worksheet.Cells[1, ci] = gridCol.HeaderText;
                }

                // Row 출력
                for (int ri = 0; ri < gridView.Rows.Count; ri++)
                {
                    DataGridViewRow gridRow = gridView.Rows[ri];

                    int realCol = 0;
                    for (int ci = 0; ci < gridRow.Cells.Count; ci++)
                    {
                        // CheckBox, Hidden Cell 타입은 출력하지 않는다. 
                        if (gridRow.Cells[ci].GetType() == typeof(DataGridViewCheckBoxColumn) || gridRow.Cells[ci].Visible == false || gridRow.Cells[ci].Value.ToString().Equals("True") || gridRow.Cells[ci].Value.ToString().Equals("False"))
                            continue;

                        realCol++;
                        worksheet.Cells[ri + 2, realCol] = gridRow.Cells[ci].Value.ToString();
                    }
                }

                workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //workbook.SaveAs(saveFileName, Excel.XlFileFormat.xlWorkbookNormal);
                //workbook.SaveAs(saveFileName, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                retVal = true;

            }
            catch (Exception ex)
            {
                retVal = false;
                string message = string.Format("Export 중 오류가 발생했습니다. ({0})", ex.Message + "[StackTrace]" + ex.StackTrace);

                MessageBox.Show(message);
                Console.WriteLine(message);
            }
            finally
            {
                workbook.Close(isSavedFile);
                application.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(application);
            }

            return retVal;
        }






        static public bool ExportExcel(string saveFileName, DataSet dataSet, List<string> colList)
        {
            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            bool isSavedFile = false;
            bool retVal = false;

            try
            {
                // Excel 파일 저장
                application = new Excel.Application();
                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.get_Item(1) as Excel.Worksheet;
                //application.Visible = false;

                // 전체 기본 폰트
                application.StandardFont = "맑은 고딕";
                application.StandardFontSize = 11;
                application.get_Range("A1").EntireColumn.NumberFormat = "@";


                // Column 출력
                for (int ci = 0; ci < colList.Count; ci++)
                {
                    string colHeader = colList[ci];
                    worksheet.Cells[1, ci] = colHeader;
                }

                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    // Row 출력
                    for (int ri = 0; ri < dataSet.Tables[0].Rows.Count; ri++)
                    {
                        DataRow dataRow = dataSet.Tables[0].Rows[ri];

                        //for (int ci = 0; ci < dataSet.Tables[0].Columns.Count; ci++)
                        for (int ci = 0; ci < colList.Count; ci++)
                        {
                            worksheet.Cells[ri + 2, ci + 1] = (dataRow[ci] == null) ? "" : dataRow[ci].ToString();
                        }
                    }
                }

                application.get_Range("A1", "Z1").EntireColumn.AutoFit();
                application.get_Range("A1", "Z1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //workbook.SaveAs(saveFileName, Excel.XlFileFormat.xlWorkbookNormal);
                //workbook.SaveAs(saveFileName, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                retVal = true;

            }
            catch (Exception ex)
            {
                retVal = false;
                string message = string.Format("Export 중 오류가 발생했습니다. ({0})", ex.Message);
                MessageBox.Show(message);
                Console.WriteLine(message);
            }
            finally
            {
                workbook.Close(isSavedFile);
                application.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(application);
            }

            return retVal;
        }

        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
