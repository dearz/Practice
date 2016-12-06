using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using grproLib;
using MyProgram.GlobalTypes;


namespace MyProgram.Reports
{
    public class ReportPrint
    {
        private GridppReport report = new GridppReport();
        private object source;
        private DataSourceType sourceType;
        private GridppReport subReport;
        private object subSource;
        private DataSourceType subSourceType;

        private string locationReportFiles = Application.StartupPath + "\\ReportsFile";

        public bool SetReportFile(string reportName, string reportTitle)
        {
            string fileName = string.Format("{0}\\{1}.grf", locationReportFiles, reportName);
            try
            {
                if (File.Exists(fileName))
                {
                    report.LoadFromFile(fileName);
                    report.Title = reportTitle;
                    return true;
                }
                else
                {
                    report = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  未能成功加载报表样式文件！");
                report = null;
                return false;
            }
        }

        public void SetDataSource(object source,DataSourceType sourceType)
        {
            if (report != null)
            {
                this.source = source;
                this.sourceType = sourceType;
                report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(report_FetchRecord);
                report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(report_FetchRecord);
            }
        }
        /// <summary>
        /// 必须有这事件函数，才能report.DetailGrid.Recordset.Append()
        /// </summary>
        private void report_FetchRecord()
        {
            if (this.source != null)
            {
                if (sourceType == DataSourceType.DataTable)
                {
                    DataTable dt = source as DataTable;
                    FillRecordToReport(report, dt);
                }
                if (sourceType == DataSourceType.DataReader)
                {
                    IDataReader dr = source as IDataReader;
                    FillRecordToReport(report, dr);
                }
            }
        }

        /// <summary>
        /// 填充DataTable类型的数据源的数据到报表的明细网格的记录集的对应字段中
        /// </summary>
        /// <param name="rpt"></param>
        /// <param name="dt"></param>
        private void FillRecordToReport(GridppReport rpt, DataTable dt)
        {
            MatchFieldPairType[] matchFieldPair = new MatchFieldPairType[System.Math.Min(report.DetailGrid.Recordset.Fields.Count, dt.Columns.Count)];
            int matchFieldPairIndex = 0;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                foreach (IGRField fld in report.DetailGrid.Recordset.Fields)
                {
                    if (string.Compare(fld.Name, dt.Columns[i].ColumnName, true) == 0)
                    {
                        matchFieldPair[matchFieldPairIndex].field = fld;
                        matchFieldPair[matchFieldPairIndex].MatchColumnIndex = i;
                        matchFieldPairIndex++;
                        break;
                    }
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
                report.DetailGrid.Recordset.Append();
                foreach (MatchFieldPairType fieldPair in matchFieldPair)
                {
                    if(!dr.IsNull(fieldPair.MatchColumnIndex))
                    fieldPair.field.Value = dr[fieldPair.MatchColumnIndex];
                }
                report.DetailGrid.Recordset.Post();
            }
        }
        /// <summary>
        /// 填充DataReader类型的数据源的数据到报表的明细网格的记录集的对应字段中
        /// </summary>
        /// <param name="rpt"></param>
        /// <param name="dr"></param>
        private void FillRecordToReport(GridppReport rpt, IDataReader dr)
        {
            MatchFieldPairType[] matchFieldPair = new MatchFieldPairType[System.Math.Min(report.DetailGrid.Recordset.Fields.Count, dr.FieldCount)];
            int matchFieldPairIndex = 0;
            for (int i = 0; i < dr.FieldCount; i++)
            {
                foreach (IGRField fld in report.DetailGrid.Recordset.Fields)
                {
                    if (string.Compare(fld.Name, dr.GetName(i)) == 0)
                    {
                        matchFieldPair[matchFieldPairIndex].field = fld;
                        matchFieldPair[matchFieldPairIndex].MatchColumnIndex = i;
                        matchFieldPairIndex++;
                        break;
                    }
                }
            }

            while (dr.Read())
            {
                report.DetailGrid.Recordset.Append();
                foreach (MatchFieldPairType fieldPair in matchFieldPair)
                {
                    if (!dr.IsDBNull(fieldPair.MatchColumnIndex))
                    {
                        fieldPair.field.Value = dr.GetValue(fieldPair.MatchColumnIndex);
                    }
                }
                report.DetailGrid.Recordset.Post();
            }
        }

        /// <summary>
        /// 设置报表样式的中的参数值
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        public void SetParamter(string paramName, string paramValue)
        {
            if (report != null)
            {
                try
                {
                    report.ParameterByName(paramName).Value = paramValue;
                }
                catch(Exception)
                {
                    MessageBox.Show("未能找到指定的参数，请检查报表样式！");
                }
            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        public void PreviewReport()
        {
            if (this.report != null)
            {
                PreviewForm previewForm = new PreviewForm(report);
                previewForm.MaximizeBox = false;
                previewForm.Width = Screen.PrimaryScreen.WorkingArea.Width;
                previewForm.Height = Screen.PrimaryScreen.WorkingArea.Height;
                previewForm.StartPosition = FormStartPosition.CenterParent;
                previewForm.ShowDialog();
            }
        }
    }
}
