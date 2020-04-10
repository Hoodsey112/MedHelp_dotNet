using System;
using System.Data;
using _Excel = Microsoft.Office.Interop.Excel;

namespace MedHelp_dotNet.Classes
{
    public class ExcelClass
    {
        static _Excel.Application ExcelApp = new _Excel.Application();
        
        public static void ExportExcelData(DataView exportData)
        {
            ExcelApp.Workbooks.Add();
            _Excel._Worksheet workSheet = (_Excel.Worksheet)ExcelApp.ActiveSheet;
            workSheet.PageSetup.Orientation = _Excel.XlPageOrientation.xlLandscape;

            #region Шапка таблицы
            workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[4, 1]].Merge();
            workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[4, 1]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[4, 1]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 1] = "№";

            workSheet.Range[workSheet.Cells[1, 2], workSheet.Cells[4, 2]].Merge();
            workSheet.Range[workSheet.Cells[1, 2], workSheet.Cells[4, 2]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 2], workSheet.Cells[4, 2]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 2] = "Район";

            workSheet.Range[workSheet.Cells[1, 3], workSheet.Cells[4, 3]].Merge();
            workSheet.Range[workSheet.Cells[1, 3], workSheet.Cells[4, 3]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 3], workSheet.Cells[4, 3]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 3], workSheet.Cells[4, 3]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 3] = "Медицинская организация";

            workSheet.Range[workSheet.Cells[1, 4], workSheet.Cells[4, 4]].Merge();
            workSheet.Range[workSheet.Cells[1, 4], workSheet.Cells[4, 4]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 4], workSheet.Cells[4, 4]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 4], workSheet.Cells[4, 4]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 4] = "Дата подачи информации";

            workSheet.Range[workSheet.Cells[1, 5], workSheet.Cells[4, 5]].Merge();
            workSheet.Range[workSheet.Cells[1, 5], workSheet.Cells[4, 5]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 5], workSheet.Cells[4, 5]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 5], workSheet.Cells[4, 5]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 5] = "Полное название детской оздоровительной организации";

            workSheet.Range[workSheet.Cells[1, 6], workSheet.Cells[4, 6]].Merge();
            workSheet.Range[workSheet.Cells[1, 6], workSheet.Cells[4, 6]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 6], workSheet.Cells[4, 6]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 6], workSheet.Cells[4, 6]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 6] = "Адрес детской оздоровительной организации";

            workSheet.Range[workSheet.Cells[1, 7], workSheet.Cells[4, 7]].Merge();
            workSheet.Range[workSheet.Cells[1, 7], workSheet.Cells[4, 7]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 7], workSheet.Cells[4, 7]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 7], workSheet.Cells[4, 7]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 7] = "ФИО ребенка";

            workSheet.Range[workSheet.Cells[1, 8], workSheet.Cells[4, 8]].Merge();
            workSheet.Range[workSheet.Cells[1, 8], workSheet.Cells[4, 8]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 8], workSheet.Cells[4, 8]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 8], workSheet.Cells[4, 8]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 8] = "Дата рождения";

            workSheet.Range[workSheet.Cells[1, 9], workSheet.Cells[4, 9]].Merge();
            workSheet.Range[workSheet.Cells[1, 9], workSheet.Cells[4, 9]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 9], workSheet.Cells[4, 9]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 9], workSheet.Cells[4, 9]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 9] = "Адрес постоянного проживания";

            workSheet.Range[workSheet.Cells[1, 10], workSheet.Cells[4, 10]].Merge();
            workSheet.Range[workSheet.Cells[1, 10], workSheet.Cells[4, 10]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 10], workSheet.Cells[4, 10]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 10], workSheet.Cells[4, 10]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 10] = "Тип отдыха";

            workSheet.Range[workSheet.Cells[1, 11], workSheet.Cells[1, 18]].Merge();
            workSheet.Range[workSheet.Cells[1, 11], workSheet.Cells[1, 16]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 11], workSheet.Cells[1, 16]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 11] = "Обращение за медицинской помощью";

            workSheet.Range[workSheet.Cells[2, 11], workSheet.Cells[4, 11]].Merge();
            workSheet.Range[workSheet.Cells[2, 11], workSheet.Cells[4, 11]].WrapText = true;
            workSheet.Range[workSheet.Cells[2, 11], workSheet.Cells[4, 11]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[2, 11], workSheet.Cells[4, 11]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[2, 11] = "Дата обращения";

            workSheet.Range[workSheet.Cells[2, 12], workSheet.Cells[4, 12]].Merge();
            workSheet.Range[workSheet.Cells[2, 12], workSheet.Cells[4, 12]].WrapText = true;
            workSheet.Range[workSheet.Cells[2, 12], workSheet.Cells[4, 12]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[2, 12], workSheet.Cells[4, 12]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[2, 12] = "Вид оказанной помощи";

            workSheet.Range[workSheet.Cells[2, 13], workSheet.Cells[4, 13]].Merge();
            workSheet.Range[workSheet.Cells[2, 13], workSheet.Cells[4, 13]].WrapText = true;
            workSheet.Range[workSheet.Cells[2, 13], workSheet.Cells[4, 13]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[2, 13], workSheet.Cells[4, 13]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[2, 13] = "Диагноз";

            workSheet.Range[workSheet.Cells[2, 14], workSheet.Cells[4, 14]].Merge();
            workSheet.Range[workSheet.Cells[2, 14], workSheet.Cells[4, 14]].WrapText = true;
            workSheet.Range[workSheet.Cells[2, 14], workSheet.Cells[4, 14]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[2, 14], workSheet.Cells[4, 14]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[2, 14] = "МКБ10";

            workSheet.Range[workSheet.Cells[2, 15], workSheet.Cells[4, 15]].Merge();
            workSheet.Range[workSheet.Cells[2, 15], workSheet.Cells[4, 15]].WrapText = true;
            workSheet.Range[workSheet.Cells[2, 15], workSheet.Cells[4, 15]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[2, 15], workSheet.Cells[4, 15]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[2, 15] = "Врач-специалист";

            workSheet.Range[workSheet.Cells[2, 16], workSheet.Cells[4, 16]].Merge();
            workSheet.Range[workSheet.Cells[2, 16], workSheet.Cells[4, 16]].WrapText = true;
            workSheet.Range[workSheet.Cells[2, 16], workSheet.Cells[4, 16]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[2, 16], workSheet.Cells[4, 16]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[2, 16] = "Отделение";

            workSheet.Range[workSheet.Cells[2, 17], workSheet.Cells[3, 18]].Merge();
            workSheet.Range[workSheet.Cells[2, 17], workSheet.Cells[3, 18]].WrapText = true;
            workSheet.Range[workSheet.Cells[2, 17], workSheet.Cells[3, 18]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[2, 17], workSheet.Cells[3, 18]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[2, 17] = "По тяжести состояния направлен (переведен) в реанимацию";

            workSheet.Cells[4, 17].WrapText = true;
            workSheet.Cells[4, 17].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Cells[4, 17].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[4, 17] = "Направлен (переведен)";

            workSheet.Cells[4, 18].WrapText = true;
            workSheet.Cells[4, 18].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Cells[4, 18].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[4, 18] = "Дата перевода";

            workSheet.Range[workSheet.Cells[1, 19], workSheet.Cells[4, 19]].Merge();
            workSheet.Range[workSheet.Cells[1, 19], workSheet.Cells[4, 19]].WrapText = true;
            workSheet.Range[workSheet.Cells[1, 19], workSheet.Cells[4, 19]].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
            workSheet.Range[workSheet.Cells[1, 19], workSheet.Cells[4, 19]].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
            workSheet.Cells[1, 19] = "Состояние ребенка по степени тяжести";

            ((_Excel.Range)workSheet.Columns[1]).AutoFit();
            ((_Excel.Range)workSheet.Columns[2]).AutoFit();
            ((_Excel.Range)workSheet.Columns[3]).AutoFit();
            ((_Excel.Range)workSheet.Columns[4]).AutoFit();
            ((_Excel.Range)workSheet.Columns[5]).AutoFit();
            ((_Excel.Range)workSheet.Columns[6]).AutoFit();
            ((_Excel.Range)workSheet.Columns[7]).AutoFit();
            ((_Excel.Range)workSheet.Columns[8]).AutoFit();
            ((_Excel.Range)workSheet.Columns[9]).AutoFit();
            ((_Excel.Range)workSheet.Columns[10]).AutoFit();
            ((_Excel.Range)workSheet.Columns[11]).AutoFit();
            ((_Excel.Range)workSheet.Columns[12]).AutoFit();
            ((_Excel.Range)workSheet.Columns[13]).AutoFit();
            ((_Excel.Range)workSheet.Columns[14]).AutoFit();
            ((_Excel.Range)workSheet.Columns[15]).AutoFit();
            ((_Excel.Range)workSheet.Columns[16]).AutoFit();
            ((_Excel.Range)workSheet.Columns[17]).AutoFit();
            ((_Excel.Range)workSheet.Columns[18]).AutoFit();
            ((_Excel.Range)workSheet.Columns[19]).AutoFit();
            #endregion

            for (int i = 0; i < exportData.Count; i++)
            {
                int clmn = 1;

                workSheet.Cells[i + 5, clmn].WrapText = true;
                workSheet.Cells[i + 5, clmn].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
                workSheet.Cells[i + 5, clmn].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
                workSheet.Cells[i + 5, clmn] = i+1;

                for (int j = 0; j < exportData.Table.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                        case 8:
                        case 9:
                            break;
                        case 3:
                        case 7:
                        case 12:
                            clmn++;
                            workSheet.Cells[i + 5, clmn].WrapText = true;
                            workSheet.Cells[i + 5, clmn].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
                            workSheet.Cells[i + 5, clmn].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
                            workSheet.Cells[i + 5, clmn].NumberFormat = "ДД.ММ.ГГГГ";
                            workSheet.Cells[i + 5, clmn] = DateTime.Parse(exportData[i][j].ToString()).ToString("dd.MM.yyyy");
                            break;
                        default:
                            clmn++;
                            workSheet.Cells[i + 5, clmn].WrapText = true;
                            workSheet.Cells[i + 5, clmn].VerticalAlignment = _Excel.XlVAlign.xlVAlignCenter;
                            workSheet.Cells[i + 5, clmn].HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
                            workSheet.Cells[i + 5, clmn] = exportData[i][j].ToString();
                            break;
                    }
                }
            }

            workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[exportData.Count + 4, 19]].Borders.LineStyle = _Excel.XlLineStyle.xlContinuous;

            ExcelApp.Visible = true;
        }
        

    }
}
