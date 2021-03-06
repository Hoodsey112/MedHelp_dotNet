﻿using System;
using System.Data;
using _Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using NLog;
using System.Windows.Forms;

namespace MedHelp_dotNet.Classes
{
    public class WordClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void ExportWordData(DataTable wordData, DataTable textData, DataTable classTable, DateTime PeriodDateFrom, DateTime PeriodDateTo, DataTable CHLDReport)
        {
            try
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
                oTable = WordDoc.Tables.Add(wrdRng, 3 + wordData.Rows.Count, 10, ref oMissing, ref oMissing); // Размерность таблицы Nх10 (N - кол-во строк зависит от кол-ва строк в принимаемом аргументе, 10 столбцов)
                oTable.AutoFitBehavior(_Word.WdAutoFitBehavior.wdAutoFitFixed); // wdAutoFitFixed - фиксированный размер столбцов
                oTable.Rows.SetLeftIndent(-65, _Word.WdRulerStyle.wdAdjustNone); //Смещение таблицы влево на 75 единиц
                oTable.Range.ParagraphFormat.SpaceBefore = 6;

                oTable.Range.Bold = 0; //Выделение шрифта жирным
                oTable.Range.Font.Size = 10; //Размер шрифта в таблице
                oTable.Range.ParagraphFormat.SpaceAfter = 14;
                oTable.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphCenter; //Горизонтальное выравнивание текста по центру ячейки
                oTable.Range.Cells.VerticalAlignment = _Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; //Вертикальное выравнивание текста в ячейке
                oTable.Borders.InsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle; //Отрисовка сплошных линий внутри таблицы
                oTable.Borders.OutsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;//Отрисовка сплошных линий снаружи таблицы

                //Размеры столбцов в единицах
                oTable.Columns[1].Width = 90; //102
                oTable.Columns[2].Width = 67;
                oTable.Columns[3].Width = 40;
                oTable.Columns[4].Width = 40;
                oTable.Columns[5].Width = 40;
                oTable.Columns[6].Width = 40;
                oTable.Columns[7].Width = 60.03f;
                oTable.Columns[8].Width = 62.64f;
                oTable.Columns[9].Width = 63.51f;
                oTable.Columns[10].Width = 63;//66.41f;

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

                oTable.Cell(3 + wordData.Rows.Count, 1).Range.Bold = 1;
                oTable.Cell(3 + wordData.Rows.Count, 1).Range.Text = "Итого";

                for (int j = 2; j <= 10; j++)
                {
                    int sumColumn = 0;

                    for (int i = 3; i < oTable.Rows.Count; i++)
                    {
                        string _char = oTable.Cell(i, j).Range.Text.ToString();
                        _char = _char.Remove(_char.Length - 2, 2);
                        sumColumn += int.Parse(_char);
                    }

                    oTable.Cell(3 + wordData.Rows.Count, j).Range.Bold = 1;
                    oTable.Cell(3 + wordData.Rows.Count, j).Range.Text = sumColumn.ToString();
                }

                _Word.Paragraph oPara3;
                oPara3 = WordDoc.Content.Paragraphs.Add(ref oMissing);

                oPara3.Range.Font.Bold = 0;
                oPara3.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                oPara3.Range.Font.Name = "Times New Roman";
                oPara3.Range.Font.Size = 14; //Размер шрифта абзаца
                oPara3.Format.SpaceAfter = 14;
                string itog = oTable.Cell(oTable.Rows.Count, 2).Range.Text.ToString();
                itog = itog.Remove(itog.Length - 2, 2);
                if (PeriodDateFrom.ToString("MMMMyyyy") == PeriodDateTo.ToString("MMMMyyyy")) oPara3.Range.Text = $"За {PeriodDateTo:MMMM yyyy} за медицинской помощью обратились {itog} детей, из них:";
                else if (PeriodDateFrom.ToString("yyyy") == PeriodDateTo.ToString("yyyy")) oPara3.Range.Text = $"За {PeriodDateFrom:MMMM}-{PeriodDateTo:MMMM yyyy} за медицинской помощью обратились {itog} детей, из них:";
                else oPara3.Range.Text = $"За {PeriodDateFrom:MMMM yyyy}-{PeriodDateTo:MMMM yyyy} за медицинской помощью обратились {itog} детей, из них:";
                oPara3.Range.InsertParagraphAfter();

                string area = "";
                string relax = "";
                string doo = "";
                string help = "";
                string diag = "";

                //Создание многоуровнего списка(в данном случае создается 5 уровней)
                _Word.ListTemplate template = WordApp.ListGalleries[_Word.WdListGalleryType.wdOutlineNumberGallery].ListTemplates.get_Item(1);

                //1-й уровень
                _Word.ListLevel level = template.ListLevels[1];

                level.NumberFormat = "%1.";
                level.TrailingCharacter = _Word.WdTrailingCharacter.wdTrailingTab;
                level.NumberStyle = _Word.WdListNumberStyle.wdListNumberStyleArabic;
                level.NumberPosition = WordApp.CentimetersToPoints(0f);
                level.Alignment = _Word.WdListLevelAlignment.wdListLevelAlignLeft;
                level.TextPosition = WordApp.CentimetersToPoints(0.63f);
                level.TabPosition = (float)_Word.WdConstants.wdUndefined;
                level.ResetOnHigher = 0;
                level.StartAt = 1;
                level.Font.Bold = 0;
                level.Font.Italic = (int)_Word.WdConstants.wdUndefined;
                level.Font.StrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.Font.Subscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Superscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Shadow = (int)_Word.WdConstants.wdUndefined;
                level.Font.Outline = (int)_Word.WdConstants.wdUndefined;
                level.Font.Emboss = (int)_Word.WdConstants.wdUndefined;
                level.Font.Engrave = (int)_Word.WdConstants.wdUndefined;
                level.Font.AllCaps = (int)_Word.WdConstants.wdUndefined;
                level.Font.Hidden = (int)_Word.WdConstants.wdUndefined;
                level.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                level.Font.Color = _Word.WdColor.wdColorAutomatic;
                level.Font.Size = (int)_Word.WdConstants.wdUndefined;
                level.Font.Animation = _Word.WdAnimation.wdAnimationNone;
                level.Font.DoubleStrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.LinkedStyle = "";

                //2-й уровень
                level = template.ListLevels[2];

                level.NumberFormat = "%1.%2.";
                level.TrailingCharacter = _Word.WdTrailingCharacter.wdTrailingTab;
                level.NumberStyle = _Word.WdListNumberStyle.wdListNumberStyleArabic;
                level.NumberPosition = WordApp.CentimetersToPoints(0.63f);
                level.Alignment = _Word.WdListLevelAlignment.wdListLevelAlignLeft;
                level.TextPosition = WordApp.CentimetersToPoints(1.4f);
                level.TabPosition = (float)_Word.WdConstants.wdUndefined;
                level.ResetOnHigher = 1;
                level.StartAt = 1;
                level.Font.Bold = 0;
                level.Font.Italic = (int)_Word.WdConstants.wdUndefined;
                level.Font.StrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.Font.Subscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Superscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Shadow = (int)_Word.WdConstants.wdUndefined;
                level.Font.Outline = (int)_Word.WdConstants.wdUndefined;
                level.Font.Emboss = (int)_Word.WdConstants.wdUndefined;
                level.Font.Engrave = (int)_Word.WdConstants.wdUndefined;
                level.Font.AllCaps = (int)_Word.WdConstants.wdUndefined;
                level.Font.Hidden = (int)_Word.WdConstants.wdUndefined;
                level.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                level.Font.Color = _Word.WdColor.wdColorAutomatic;
                level.Font.Size = (int)_Word.WdConstants.wdUndefined;
                level.Font.Animation = _Word.WdAnimation.wdAnimationNone;
                level.Font.DoubleStrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.LinkedStyle = "";

                //3-й уровень
                level = template.ListLevels[3];

                level.NumberFormat = "%1.%2.%3.";
                level.TrailingCharacter = _Word.WdTrailingCharacter.wdTrailingTab;
                level.NumberStyle = _Word.WdListNumberStyle.wdListNumberStyleArabic;
                level.NumberPosition = WordApp.CentimetersToPoints(1.27f);
                level.Alignment = _Word.WdListLevelAlignment.wdListLevelAlignLeft;
                level.TextPosition = WordApp.CentimetersToPoints(2.16f);
                level.TabPosition = (float)_Word.WdConstants.wdUndefined;
                level.ResetOnHigher = 2;
                level.StartAt = 1;
                level.Font.Bold = 0;
                level.Font.Italic = (int)_Word.WdConstants.wdUndefined;
                level.Font.StrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.Font.Subscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Superscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Shadow = (int)_Word.WdConstants.wdUndefined;
                level.Font.Outline = (int)_Word.WdConstants.wdUndefined;
                level.Font.Emboss = (int)_Word.WdConstants.wdUndefined;
                level.Font.Engrave = (int)_Word.WdConstants.wdUndefined;
                level.Font.AllCaps = (int)_Word.WdConstants.wdUndefined;
                level.Font.Hidden = (int)_Word.WdConstants.wdUndefined;
                level.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                level.Font.Color = _Word.WdColor.wdColorAutomatic;
                level.Font.Size = (int)_Word.WdConstants.wdUndefined;
                level.Font.Animation = _Word.WdAnimation.wdAnimationNone;
                level.Font.DoubleStrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.LinkedStyle = "";

                //4-й уровень
                level = template.ListLevels[4];

                level.NumberFormat = "%1.%2.%3.%4.";
                level.TrailingCharacter = _Word.WdTrailingCharacter.wdTrailingTab;
                level.NumberStyle = _Word.WdListNumberStyle.wdListNumberStyleArabic;
                level.NumberPosition = WordApp.CentimetersToPoints(1.9f);
                level.Alignment = _Word.WdListLevelAlignment.wdListLevelAlignLeft;
                level.TextPosition = WordApp.CentimetersToPoints(3.05f);
                level.TabPosition = (float)_Word.WdConstants.wdUndefined;
                level.ResetOnHigher = 3;
                level.StartAt = 1;
                level.Font.Bold = 0;
                level.Font.Italic = (int)_Word.WdConstants.wdUndefined;
                level.Font.StrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.Font.Subscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Superscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Shadow = (int)_Word.WdConstants.wdUndefined;
                level.Font.Outline = (int)_Word.WdConstants.wdUndefined;
                level.Font.Emboss = (int)_Word.WdConstants.wdUndefined;
                level.Font.Engrave = (int)_Word.WdConstants.wdUndefined;
                level.Font.AllCaps = (int)_Word.WdConstants.wdUndefined;
                level.Font.Hidden = (int)_Word.WdConstants.wdUndefined;
                level.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                level.Font.Color = _Word.WdColor.wdColorAutomatic;
                level.Font.Size = (int)_Word.WdConstants.wdUndefined;
                level.Font.Animation = _Word.WdAnimation.wdAnimationNone;
                level.Font.DoubleStrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.LinkedStyle = "";

                //5-й уровень
                level = template.ListLevels[5];

                level.NumberFormat = "%1.%2.%3.%4.%5.";
                level.TrailingCharacter = _Word.WdTrailingCharacter.wdTrailingTab;
                level.NumberStyle = _Word.WdListNumberStyle.wdListNumberStyleArabic;
                level.NumberPosition = WordApp.CentimetersToPoints(2.54f);
                level.Alignment = _Word.WdListLevelAlignment.wdListLevelAlignLeft;
                level.TextPosition = WordApp.CentimetersToPoints(3.94f);
                level.TabPosition = (float)_Word.WdConstants.wdUndefined;
                level.ResetOnHigher = 4;
                level.StartAt = 1;
                level.Font.Bold = 0;
                level.Font.Italic = (int)_Word.WdConstants.wdUndefined;
                level.Font.StrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.Font.Subscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Superscript = (int)_Word.WdConstants.wdUndefined;
                level.Font.Shadow = (int)_Word.WdConstants.wdUndefined;
                level.Font.Outline = (int)_Word.WdConstants.wdUndefined;
                level.Font.Emboss = (int)_Word.WdConstants.wdUndefined;
                level.Font.Engrave = (int)_Word.WdConstants.wdUndefined;
                level.Font.AllCaps = (int)_Word.WdConstants.wdUndefined;
                level.Font.Hidden = (int)_Word.WdConstants.wdUndefined;
                level.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                level.Font.Color = _Word.WdColor.wdColorAutomatic;
                level.Font.Size = (int)_Word.WdConstants.wdUndefined;
                level.Font.Animation = _Word.WdAnimation.wdAnimationNone;
                level.Font.DoubleStrikeThrough = (int)_Word.WdConstants.wdUndefined;
                level.LinkedStyle = "";

                template.Name = "";

                for (int i = 0; i < textData.Rows.Count; i++)
                {

                    for (int j = 0; j < 10; j++)
                    {
                        switch (j)
                        {
                            case 0:
                                if (area != textData.Rows[i][j].ToString())
                                {
                                    area = textData.Rows[i][j].ToString();
                                    relax = "";

                                    j++;

                                    _Word.Paragraph oPara4;
                                    oPara4 = WordDoc.Content.Paragraphs.Add(ref oMissing);

                                    oPara4.Range.Font.Bold = 0;
                                    oPara4.Range.Font.Underline = _Word.WdUnderline.wdUnderlineSingle;
                                    oPara4.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                                    oPara4.Range.Font.Name = "Times New Roman";
                                    oPara4.Range.Font.Size = 14; //Размер шрифта абзаца
                                    oPara4.Range.Text = $"{area} ({textData.Rows[i][j]}):";

                                    oPara4.Range.ListFormat.ApplyListTemplateWithLevel(template, false, _Word.WdListApplyTo.wdListApplyToWholeList, _Word.WdDefaultListBehavior.wdWord10ListBehavior);
                                    oPara4.Range.SetListLevel(1);
                                    oPara4.Range.InsertParagraphAfter();
                                }
                                break;

                            case 2:
                                if (relax != textData.Rows[i][j].ToString())
                                {
                                    relax = textData.Rows[i][j].ToString();
                                    doo = "";

                                    j++;

                                    _Word.Paragraph oPara4;
                                    oPara4 = WordDoc.Content.Paragraphs.Add(ref oMissing);

                                    oPara4.Range.Font.Bold = 0;
                                    oPara4.Range.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                                    oPara4.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                                    oPara4.Range.Font.Name = "Times New Roman";
                                    oPara4.Range.Font.Size = 14; //Размер шрифта абзаца
                                    oPara4.Range.Text = $"{relax} ({textData.Rows[i][j]}):";
                                    oPara4.Range.ListFormat.ApplyListTemplateWithLevel(template, false, _Word.WdListApplyTo.wdListApplyToWholeList, _Word.WdDefaultListBehavior.wdWord10ListBehavior);
                                    oPara4.Range.SetListLevel(2);
                                    oPara4.Range.InsertParagraphAfter();
                                }
                                break;

                            case 4:
                                if (doo != textData.Rows[i][j].ToString())
                                {
                                    doo = textData.Rows[i][j].ToString();
                                    help = "";

                                    j++;

                                    _Word.Paragraph oPara4;
                                    oPara4 = WordDoc.Content.Paragraphs.Add(ref oMissing);

                                    oPara4.Range.Font.Bold = 0;
                                    oPara4.Range.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                                    oPara4.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                                    oPara4.Range.Font.Name = "Times New Roman";
                                    oPara4.Range.Font.Size = 14; //Размер шрифта абзаца
                                    oPara4.Range.Text = $"{doo} ({textData.Rows[i][j]}):";
                                    oPara4.Range.ListFormat.ApplyListTemplateWithLevel(template, false, _Word.WdListApplyTo.wdListApplyToWholeList, _Word.WdDefaultListBehavior.wdWord10ListBehavior);
                                    oPara4.Range.SetListLevel(3);
                                    oPara4.Range.InsertParagraphAfter();
                                }
                                break;

                            case 6:
                                if (help != textData.Rows[i][j].ToString())
                                {
                                    help = textData.Rows[i][j].ToString();
                                    diag = "";

                                    j++;

                                    _Word.Paragraph oPara4;
                                    oPara4 = WordDoc.Content.Paragraphs.Add(ref oMissing);

                                    oPara4.Range.Font.Bold = 0;
                                    oPara4.Range.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                                    oPara4.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                                    oPara4.Range.Font.Name = "Times New Roman";
                                    oPara4.Range.Font.Size = 14; //Размер шрифта абзаца
                                    oPara4.Range.Text = $"{help} ({textData.Rows[i][j]}):";
                                    oPara4.Range.ListFormat.ApplyListTemplateWithLevel(template, false, _Word.WdListApplyTo.wdListApplyToWholeList, _Word.WdDefaultListBehavior.wdWord10ListBehavior);
                                    oPara4.Range.SetListLevel(4);
                                    oPara4.Range.InsertParagraphAfter();
                                }
                                break;

                            case 8:
                                if (diag != textData.Rows[i][j].ToString())
                                {
                                    diag = textData.Rows[i][j].ToString();

                                    j++;

                                    _Word.Paragraph oPara4;
                                    oPara4 = WordDoc.Content.Paragraphs.Add();

                                    oPara4.Range.Font.Bold = 0;
                                    oPara4.Range.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                                    oPara4.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                                    oPara4.Range.Font.Name = "Times New Roman";
                                    oPara4.Range.Font.Size = 14; //Размер шрифта абзаца
                                    oPara4.Range.Text = $"{diag} - {textData.Rows[i][j]}";
                                    oPara4.Range.ListFormat.ApplyListTemplateWithLevel(template, false, _Word.WdListApplyTo.wdListApplyToWholeList, _Word.WdDefaultListBehavior.wdWord10ListBehavior);
                                    oPara4.Range.SetListLevel(5);
                                    oPara4.Range.InsertParagraphAfter();
                                }
                                break;
                        }
                    }
                }
                //WordDoc.Paragraphs[WordDoc.Paragraphs.Count].Range.Delete(); //Удаление последнего пустого абзаца
                WordDoc.Paragraphs[WordDoc.Paragraphs.Count].Format.Reset();

                _Word.Paragraph oPara5 = WordDoc.Content.Paragraphs.Add();
                /*_Word.Range wrdRng_1 = oPara5.Range;
                _Word.Table oTable_1 = WordDoc.Tables.Add(wrdRng_1, 2, 2, ref oMissing, ref oMissing);

                var table = WordDoc.Tables[WordDoc.Tables.Count];
                table.set_Style("Сетка таблицы");
                table.ApplyStyleHeadingRows = true;
                table.ApplyStyleLastRow = false;
                table.ApplyStyleFirstColumn = true;
                table.ApplyStyleLastColumn = false;
                table.ApplyStyleRowBands = true;
                table.ApplyStyleColumnBands = false;*/

                _Word.Table oTable_1;
                _Word.Range wrdRng_1 = WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oTable_1 = WordDoc.Tables.Add(wrdRng_1, 2 + classTable.Rows.Count, 14, ref oMissing, ref oMissing); // Размерность таблицы Nх10 (N - кол-во строк зависит от кол-ва строк в принимаемом аргументе, 10 столбцов)
                oTable_1.AutoFitBehavior(_Word.WdAutoFitBehavior.wdAutoFitFixed); // wdAutoFitFixed - фиксированный размер столбцов
                oTable_1.Rows.SetLeftIndent(-55, _Word.WdRulerStyle.wdAdjustNone); //Смещение таблицы влево на 75 единиц
                oTable_1.Range.ParagraphFormat.SpaceBefore = 6;

                oTable_1.Range.Bold = 0; //Выделение шрифта жирным
                oTable_1.Range.Font.Size = 10; //Размер шрифта в таблице
                oTable_1.Range.ParagraphFormat.SpaceAfter = 14;
                oTable_1.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphCenter; //Горизонтальное выравнивание текста по центру ячейки
                oTable_1.Range.Cells.VerticalAlignment = _Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; //Вертикальное выравнивание текста в ячейке
                oTable_1.Borders.InsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle; //Отрисовка сплошных линий внутри таблицы
                oTable_1.Borders.OutsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;//Отрисовка сплошных линий снаружи таблицы

                //Размеры столбцов в единицах
                oTable_1.Columns[1].Width = 90; //102
                oTable_1.Columns[2].Width = 35;
                oTable_1.Columns[3].Width = 35;
                oTable_1.Columns[4].Width = 35;
                oTable_1.Columns[5].Width = 35;
                oTable_1.Columns[6].Width = 35;
                oTable_1.Columns[7].Width = 35;
                oTable_1.Columns[8].Width = 35;
                oTable_1.Columns[9].Width = 35;
                oTable_1.Columns[10].Width = 35;
                oTable_1.Columns[11].Width = 35;
                oTable_1.Columns[12].Width = 35;
                oTable_1.Columns[13].Width = 35;
                oTable_1.Columns[14].Width = 35;

                oTable_1.Rows[1].Height = 95; //Высота 1 строки в таблице

                oTable_1.Rows[1].Cells[1].Range.Text = "Территория";
                oTable_1.Rows[1].Cells[2].Range.Text = "Травмы";

                oTable_1.Rows[1].Cells[3].Range.Text = "Утопления";
                oTable_1.Rows[1].Cells[4].Range.Text = "Инфекционные";
                oTable_1.Rows[1].Cells[5].Range.Text = "Хирургические";

                oTable_1.Rows[1].Cells[6].Range.Text = "Соматические";
                oTable_1.Rows[1].Cells[7].Range.Text = "Нейрохирургические";
                oTable_1.Rows[1].Cells[8].Range.Text = "Неврологические";

                oTable_1.Rows[1].Cells[9].Range.Text = "ЛОР";
                oTable_1.Rows[1].Cells[10].Range.Text = "Отравления";

                oTable_1.Rows[1].Cells[11].Range.Text = "Алкогольные отравления";
                oTable_1.Rows[1].Cells[12].Range.Text = "Наркотические отравления";
                oTable_1.Rows[1].Cells[13].Range.Text = "Прочие";
                oTable_1.Rows[1].Cells[14].Range.Bold = 1;
                oTable_1.Rows[1].Cells[14].Range.Text = "Всего";

                /*oTable_1.Cell(1, 1).Merge(oTable.Cell(2, 1)); //Объединение 1 ячейки 1 строки с 1 ячейкой 2 строки
                oTable_1.Cell(1, 2).Merge(oTable.Cell(2, 2)); //Объединение 2 ячейки 1 строки со 2 ячейкой 2 строки
                oTable_1.Cell(1, 3).Merge(oTable.Cell(1, 4)); //Объединение 3 ячейки 1 строки с 4 ячейкой 1 строки
                oTable_1.Cell(1, 4).Merge(oTable.Cell(1, 5)); //Объединение 4 ячейки 1 строки с 5 ячейкой 1 строки
                oTable_1.Cell(1, 5).Merge(oTable.Cell(2, 7)); //Объединение 5 ячейки 1 строки с 7 ячейкой 2 строки
                oTable_1.Cell(1, 6).Merge(oTable.Cell(2, 8)); //Объединение 6 ячейки 1 строки с 8 ячейкой 2 строки
                oTable_1.Cell(1, 7).Merge(oTable.Cell(2, 9)); //Объединение 7 ячейки 1 строки с 9 ячейкой 2 строки
                oTable_1.Cell(1, 8).Merge(oTable.Cell(2, 10)); //Объединение 8 ячейки 1 строки с 10 ячейкой 2 строки*/

                for (int i = 2; i <= 14; i++)
                {
                    oTable_1.Cell(1, i).Range.Orientation = _Word.WdTextOrientation.wdTextOrientationUpward; //Направление текста вверх на 90 градусов
                }

                //oTable_1.Cell(1, 3).Range.Orientation = _Word.WdTextOrientation.wdTextOrientationUpward; //Направление текста вверх на 90 градусов
                //oTable_1.Cell(2, 5).Range.Orientation = _Word.WdTextOrientation.wdTextOrientationUpward; //Направление текста вверх на 90 градусов
                //oTable_1.Cell(2, 6).Range.Orientation = _Word.WdTextOrientation.wdTextOrientationUpward; //Направление текста вверх на 90 градусов

                for (int i = 0; i < classTable.Rows.Count; i++)
                {
                    for (int j = 0; j <= 12; j++)
                    {
                        oTable_1.Cell(i + 2, j + 1).Range.Text = classTable.Rows[i][j].ToString();
                    }
                }

                for (int i = 2; i < oTable_1.Rows.Count; i++)
                {
                    int sumColumn = 0;

                    for (int j = 2; j <= 13; j++)
                    {
                        string _char = oTable_1.Cell(i, j).Range.Text.ToString();
                        _char = _char.Remove(_char.Length - 2, 2);
                        sumColumn += int.Parse(_char);
                    }

                    oTable_1.Cell(i, 14).Range.Bold = 1;
                    oTable_1.Cell(i, 14).Range.Text = sumColumn.ToString();
                }

                oTable_1.Cell(2 + classTable.Rows.Count, 1).Range.Bold = 1;
                oTable_1.Cell(2 + classTable.Rows.Count, 1).Range.Text = "Итого";

                for (int j = 2; j <= 14; j++)
                {
                    int sumColumn = 0;

                    for (int i = 2; i < oTable_1.Rows.Count; i++)
                    {
                        string _char = oTable_1.Cell(i, j).Range.Text.ToString();
                        _char = _char.Remove(_char.Length - 2, 2);
                        sumColumn += int.Parse(_char);
                    }

                    oTable_1.Cell(2 + wordData.Rows.Count, j).Range.Bold = 1;
                    oTable_1.Cell(2 + wordData.Rows.Count, j).Range.Text = sumColumn.ToString();
                }

                _Word.Paragraph oPara6;
                oPara6 = WordDoc.Content.Paragraphs.Add(ref oMissing);
                oPara6.Range.Font.Bold = 0;
                oPara6.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                oPara6.Range.Font.Name = "Times New Roman";
                oPara6.Range.Font.Size = 14; //Размер шрифта абзаца
                oPara6.Format.SpaceAfter = 14;

                string itog_1 = oTable_1.Cell(oTable_1.Rows.Count, 14).Range.Text.ToString();
                itog_1 = itog_1.Remove(itog_1.Length - 2, 2);

                string itog_list = "";

                for (int i = 2; i <= 13; i++)
                {
                    string sum = oTable_1.Cell(oTable_1.Rows.Count, i).Range.Text;
                    sum = sum.Remove(sum.Length - 2, 2);
                    string chld = "";

                    if (int.Parse(sum) > 0)
                    {
                        switch (int.Parse(sum.Substring(sum.Length - 1)))
                        {
                            case 1:
                                chld = "ребенок";
                                break;
                            case 2:
                            case 3:
                            case 4:
                                chld = "ребенка";
                                break;
                            default:
                                chld = "детей";
                                break;
                        }
                    }

                    if (i == 2)
                    {
                        if (int.Parse(sum) > 0)
                        {
                            itog_list = $"- {oTable_1.Cell(1, i).Range.Text.Remove(oTable_1.Cell(1, i).Range.Text.Length - 2, 2)} - {sum} {chld} ({Math.Round((double.Parse(sum) * 100) / double.Parse(itog_1),1)}%); \r\n";
                        }
                    }
                    else
                    {
                        if (int.Parse(sum) > 0)
                        {
                            itog_list += $"- {oTable_1.Cell(1, i).Range.Text.Remove(oTable_1.Cell(1, i).Range.Text.Length - 2, 2)} - {sum} {chld} ({Math.Round((double.Parse(sum) * 100) / double.Parse(itog_1), 1)}%); \r\n";
                        }
                    }
                }

                string all_chld = "";
                if (int.Parse(itog_1) > 0)
                {
                    switch (int.Parse(itog_1.Substring(itog_1.Length - 1)))
                    {
                        case 1:
                            all_chld = "ребенок";
                            break;
                        case 2:
                        case 3:
                        case 4:
                            all_chld = "ребенка";
                            break;
                        default:
                            all_chld = "детей";
                            break;
                    }
                }

                if (PeriodDateFrom.ToString("MMMMyyyy") == PeriodDateTo.ToString("MMMMyyyy"))
                {
                    oPara6.Range.Text = $"За {PeriodDateTo:MMMM yyyy} года включительно из детских оздоровительных организаций всех форм собственности в медицинские организации края обратилось(ись) {itog_1} {all_chld}, в том числе:\r\n{itog_list}";
                }
                else if (PeriodDateFrom.ToString("yyyy") == PeriodDateTo.ToString("yyyy")) oPara6.Range.Text = $"За {PeriodDateFrom:MMMM}-{PeriodDateTo:MMMM yyyy} года включительно из детских оздоровительных организаций всех форм собственности в медицинские организации края обратилось(ись) {itog_1} {all_chld}, в том числе:\r\n{itog_list}";
                else oPara6.Range.Text = $"За {PeriodDateFrom:MMMM yyyy}-{PeriodDateTo:MMMM yyyy} года включительно из детских оздоровительных организаций всех форм собственности в медицинские организации края обратилось(ись) {itog_1} {all_chld}, в том числе:\r\n{itog_list}";
                oPara6.Range.InsertParagraphAfter();


                string report_chld_org = "";
                string report_chld_nonorg = "";

                for (int i = 0; i < CHLDReport.Rows.Count; i++)
                {
                    string str = CHLDReport.Rows[i][1].ToString();
                    switch (CHLDReport.Rows[i][0].ToString())
                    {
                        case "Организованный отдых":
                            switch (int.Parse(str.Substring(str.Length - 1)))
                            {
                                case 1:
                                    report_chld_org = "ребенку";
                                    break;
                                default:
                                    report_chld_org = "детям";
                                    break;
                            }
                            break;

                        case "Неорганизованный отдых":
                            switch (int.Parse(str.Substring(str.Length - 1)))
                            {
                                case 1:
                                    report_chld_nonorg = "ребенку";
                                    break;
                                default:
                                    report_chld_nonorg = "детям";
                                    break;
                            }
                            break;
                    }
                }

                if (CHLDReport.Rows.Count > 1)
                {
                    _Word.Paragraph oPara7;
                    oPara7 = WordDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara7.Range.Font.Bold = 0;
                    oPara7.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    oPara7.Range.Font.Name = "Times New Roman";
                    oPara7.Range.Font.Size = 14; //Размер шрифта абзаца
                    oPara7.Format.SpaceAfter = 14;
                    oPara7.Range.Text = $"{CHLDReport.Rows[0][0]} - специализированная медицинская помощь оказана {CHLDReport.Rows[0][1]} {report_chld_org}.";
                    oPara7.Range.InsertParagraphAfter();

                    _Word.Paragraph oPara8;
                    oPara8 = WordDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara8.Range.Font.Bold = 0;
                    oPara8.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    oPara8.Range.Font.Name = "Times New Roman";
                    oPara8.Range.Font.Size = 14; //Размер шрифта абзаца
                    oPara8.Format.SpaceAfter = 14;
                    oPara8.Range.Text = $"{CHLDReport.Rows[1][0]} - специализированная медицинская помощь оказана {CHLDReport.Rows[1][1]} {report_chld_nonorg}.";
                    oPara8.Range.InsertParagraphAfter();
                }
                else if (CHLDReport.Rows.Count == 1)
                {
                    _Word.Paragraph oPara7;
                    oPara7 = WordDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara7.Range.Font.Bold = 0;
                    oPara7.Range.Paragraphs.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    oPara7.Range.Font.Name = "Times New Roman";
                    oPara7.Range.Font.Size = 14; //Размер шрифта абзаца
                    oPara7.Format.SpaceAfter = 14;
                    oPara7.Range.Text = $"{CHLDReport.Rows[0][0]} - специализированная медицинская помощь оказана {CHLDReport.Rows[0][1]} {report_chld_org}.";
                    oPara7.Range.InsertParagraphAfter();
                }

                // WordDoc.Paragraphs[WordDoc.Paragraphs.Count].Range.Delete(); //Удаление последнего пустого абзаца
                WordApp.Visible = true;
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
