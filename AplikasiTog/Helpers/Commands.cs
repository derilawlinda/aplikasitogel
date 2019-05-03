﻿using System.IO;
using System.Windows.Media;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System.Windows;
using Microsoft.Win32;
using Apel.Helpers;

namespace Apel
{
    public static class Commands
    {
        static Commands()
        {
            CommandManager.RegisterClassCommandBinding(typeof(SfDataGrid), new CommandBinding(ExportToExcel, OnExecuteExportToExcel, OnCanExecuteExportToExcel));
        }

        #region ExportToExcel Command

        public static RoutedCommand ExportToExcel = new RoutedCommand("ExportToExcel", typeof(SfDataGrid));

        private static void OnExecuteExportToExcel(object sender, ExecutedRoutedEventArgs args)
        {
            var dataGrid = args.Source as SfDataGrid;
            EccelOptionsConverter ExcelOption =new EccelOptionsConverter();
            if (dataGrid == null) return;
            try
            {
                var options = args.Parameter as ExcelExportingOptions;
                options.ExcelVersion = ExcelVersion.Excel2010;
                options.ExportingEventHandler = ExportingHandler;
                if (!ExcelOption.isCustomized)
                    options.CellsExportingEventHandler = CellExportingHandler;
                else
                    options.CellsExportingEventHandler = CustomizeCellExportingHandler;

                var excelEngine = dataGrid.ExportToExcel(dataGrid.View, options);

                var workBook = excelEngine.Excel.Workbooks[0];
                
                SaveFileDialog sfd = new SaveFileDialog
                {
                    FilterIndex = 2,
                    Filter = "Excel 97 to 2003 Files(*.xls)|*.xls|Excel 2007 to 2010 Files(*.xlsx)|*.xlsx",
                    FileName = "Book1"
                };

                if (sfd.ShowDialog() == true)
                {
                    using (Stream stream = sfd.OpenFile())
                    {
                        if (sfd.FilterIndex == 1)
                            workBook.Version = ExcelVersion.Excel97to2003;
                        else
                            workBook.Version = ExcelVersion.Excel2010;
                        workBook.SaveAs(stream);                        
                    }

                    //Message box confirmation to view the created spreadsheet.
                    if (MessageBox.Show("Do you want to view the workbook?", "Workbook has been created",
                                        MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                        System.Diagnostics.Process.Start(sfd.FileName);
                    } 
                }                                              
            }
            catch (Exception)
            {

            }
        }

        private static void CustomizeCellExportingHandler(object sender, GridCellExcelExportingEventArgs e)
        {
            if (e.ColumnName == "ProductName")
            {
                e.Range.CellStyle.ColorIndex = ExcelKnownColors.Violet;
            }
        }

        private static void CellExportingHandler(object sender, GridCellExcelExportingEventArgs e)
        {
            e.Range.CellStyle.Font.Size = 12;
            e.Range.CellStyle.Font.FontName = "Segoe UI";
            
           if (e.ColumnName == "UnitPrice" || e.ColumnName == "UnitsInStock")
            {
               double value=0;
               if (double.TryParse(e.CellValue.ToString(), out value))
               {
                   e.Range.Number = value;
               }
                e.Handled = true;
            }
        }

        private static void ExportingHandler(object sender, GridExcelExportingEventArgs e)
        {
         
            if (e.CellType == ExportCellType.HeaderCell)
            {
                e.CellStyle.BackGroundBrush = new SolidColorBrush(Colors.LightSteelBlue);
                e.CellStyle.ForeGroundBrush = new SolidColorBrush(Colors.DarkRed);
                e.CellStyle.FontInfo.Bold = true;
            }
            else if (e.CellType == ExportCellType.GroupCaptionCell)
            {
                e.CellStyle.BackGroundBrush = new SolidColorBrush(Colors.LightSlateGray);
                e.CellStyle.ForeGroundBrush = new SolidColorBrush(Colors.LightYellow);
            }
            else if (e.CellType == ExportCellType.GroupSummaryCell)
            {
                e.CellStyle.BackGroundBrush = new SolidColorBrush(Colors.LightGray);
            }
            e.CellStyle.FontInfo.Size = 12;
            e.CellStyle.FontInfo.FontName = "Segoe UI";
            e.Handled = true;
        }

        private static void OnCanExecuteExportToExcel(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

        #endregion

    }
}
