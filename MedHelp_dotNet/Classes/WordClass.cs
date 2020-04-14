using System;
using System.Data;
using _Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace MedHelp_dotNet.Classes
{
    public class WordClass
    {
        public static void ExportWordData(DataTable wordData, DateTime PeriodDateFrom, DateTime PeriodDateTo)
        {
            _Word._Application WordApp = new _Word.Application();
            _Word._Document WordDoc = new _Word.Document();

            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc";

            WordDoc = WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            _Word.Paragraph oPara1;
            oPara1 = WordDoc.Content.Paragraphs.Add(ref oMissing);
            
            if (PeriodDateFrom.ToString("MMMMyyyy") == PeriodDateTo.ToString("MMMMyyyy")) oPara1.Range.Text = $"Информация о детях, обратившихся за медицинской помощью в медицинские организации Краснодарского края из оздоровительных организаций всех форм собственности Краснодарского края за {PeriodDateTo:MMMM yyyy}";
            else if (PeriodDateFrom.ToString("yyyy") == PeriodDateTo.ToString("yyyy")) oPara1.Range.Text = $"Информация о детях, обратившихся за медицинской помощью в медицинские организации Краснодарского края из оздоровительных организаций всех форм собственности Краснодарского края за {PeriodDateFrom:MMMM}-{PeriodDateTo:MMMM yyyy}";
            else oPara1.Range.Text = $"Информация о детях, обратившихся за медицинской помощью в медицинские организации Краснодарского края из оздоровительных организаций всех форм собственности Краснодарского края за {PeriodDateFrom:MMMM yyyy}-{PeriodDateTo:MMMM yyyy}";
            
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
            oPara1.Range.Font.Name = "Times New Roman";
            oPara1.Range.Font.Size = 14; //Размер шрифта абзаца
            oPara1.Format.SpaceAfter = 14;
            oPara1.Range.InsertParagraphAfter();

            _Word.Table oTable;
            _Word.Range wrdRng = WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = WordDoc.Tables.Add(wrdRng, 2 + wordData.Rows.Count, 10, ref oMissing, ref oMissing); // Размерность таблицы Nх10 (N - кол-во строк зависит от кол-ва строк в принимаемом аргументе, 10 столбцов)
            oTable.AutoFitBehavior(_Word.WdAutoFitBehavior.wdAutoFitFixed); // wdAutoFitFixed - фиксированный размер столбцов
            oTable.Rows.SetLeftIndent(-75, _Word.WdRulerStyle.wdAdjustNone); //Смещение таблицы влево на 75 единиц
            oTable.Range.ParagraphFormat.SpaceBefore = 6;
            
            oTable.Range.Bold = 0; //Выделение шрифта жирным
            oTable.Range.Font.Size = 10; //Размер шрифта в таблице
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphCenter; //Горизонтальное выравнивание текста по центру ячейки
            oTable.Range.Cells.VerticalAlignment = _Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; //Вертикальное выравнивание текста в ячейке
            oTable.Borders.InsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle; //Отрисовка сплошных линий внутри таблицы
            oTable.Borders.OutsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;//Отрисовка сплошных линий снаружи таблицы

            //Размеры столбцов в единицах
            oTable.Columns[1].Width = 102; 
            oTable.Columns[2].Width = 67;
            oTable.Columns[3].Width = 40;
            oTable.Columns[4].Width = 40;
            oTable.Columns[5].Width = 40;
            oTable.Columns[6].Width = 40;
            oTable.Columns[7].Width = 60.03f;
            oTable.Columns[8].Width = 62.64f;
            oTable.Columns[9].Width = 63.51f;
            oTable.Columns[10].Width = 66.41f;

            oTable.Rows[2].Height = 85; //Высота 2 строки в таблице

            oTable.Rows[1].Cells[1].Range.Text = "Территория";
            oTable.Rows[1].Cells[2].Range.Text = "Количество случаев";

            oTable.Rows[1].Cells[3].Range.Text = "Ребёнок находится на организованном отдыхе";
            oTable.Rows[2].Cells[3].Range.Text = "Самостоятельно";
            oTable.Rows[2].Cells[4].Range.Text = "По путевке Мать и дитя";

            oTable.Rows[1].Cells[5].Range.Text = "Ребёнок находится на неорганизованном отдыхе";
            oTable.Rows[2].Cells[5].Range.Text = "Самостоятельно";
            oTable.Rows[2].Cells[6].Range.Text = "С законным представителем";

            oTable.Rows[1].Cells[7].Range.Text = "Первичная медико-санитарная помощь (ПМСП)";
            oTable.Rows[1].Cells[8].Range.Text = "Первичная специализированная медико-санитарная помощь (ПСМСП)";

            oTable.Rows[1].Cells[9].Range.Text = "Специализированная медицинская помощь (СМП)";
            oTable.Rows[1].Cells[10].Range.Text = "Направлен (переведён) в реанимацию";

            oTable.Cell(1, 1).Merge(oTable.Cell(2, 1)); //Объединение 1 ячейки 1 строки с 1 ячейкой 2 строки
            oTable.Cell(1, 2).Merge(oTable.Cell(2, 2)); //Объединение 2 ячейки 1 строки со 2 ячейкой 2 строки
            oTable.Cell(1, 3).Merge(oTable.Cell(1, 4)); //Объединение 3 ячейки 1 строки с 4 ячейкой 1 строки
            oTable.Cell(1, 4).Merge(oTable.Cell(1, 5)); //Объединение 4 ячейки 1 строки с 5 ячейкой 1 строки
            oTable.Cell(1, 5).Merge(oTable.Cell(2, 7)); //Объединение 5 ячейки 1 строки с 7 ячейкой 2 строки
            oTable.Cell(1, 6).Merge(oTable.Cell(2, 8)); //Объединение 6 ячейки 1 строки с 8 ячейкой 2 строки
            oTable.Cell(1, 7).Merge(oTable.Cell(2, 9)); //Объединение 7 ячейки 1 строки с 9 ячейкой 2 строки
            oTable.Cell(1, 8).Merge(oTable.Cell(2, 10)); //Объединение 8 ячейки 1 строки с 10 ячейкой 2 строки

            oTable.Cell(2, 3).Range.Orientation = _Word.WdTextOrientation.wdTextOrientationUpward; //Направление текста вверх на 90 градусов
            oTable.Cell(2, 4).Range.Orientation = _Word.WdTextOrientation.wdTextOrientationUpward; //Направление текста вверх на 90 градусов

            oTable.Cell(2, 5).Range.Orientation = _Word.WdTextOrientation.wdTextOrientationUpward; //Направление текста вверх на 90 градусов
            oTable.Cell(2, 6).Range.Orientation = _Word.WdTextOrientation.wdTextOrientationUpward; //Направление текста вверх на 90 градусов

            for (int i = 0; i < wordData.Rows.Count; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    oTable.Cell(i + 3, j + 1).Range.Text = wordData.Rows[i][j].ToString();
                }
            }

            object oRng = WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            _Word.Paragraph oPara4;
            oRng = WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara4 = WordDoc.Content.Paragraphs.Add(ref oRng);
            oPara4.Range.InsertParagraphBefore();
            oPara4.Range.Text = "And here's another table:";
            oPara4.Format.SpaceAfter = 24;
            oPara4.Range.InsertParagraphAfter();


            WordApp.Visible = true;
        }
    }
}
