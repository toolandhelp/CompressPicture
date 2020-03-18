using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompressPicture
{
   public class Comm
    {

        private DataGridView _dataGV;
        private ToolStripStatusLabel _tsslCurrent;
        private ToolStripStatusLabel _tsslTimeCost;

        private readonly string _OperateKey = Guid.NewGuid().ToString().Split('-').FirstOrDefault();

        public Comm(DataGridView dataGV, ToolStripStatusLabel tsslCurrent,ToolStripStatusLabel tsslTimeCost)
        {
            _dataGV = dataGV;
            _tsslCurrent = tsslCurrent;
            _tsslTimeCost = tsslTimeCost;
        }    

        /// <summary>
        /// 数据显示及保存
        /// </summary>
        /// <param name="operate">操作事件</param>
        /// <param name="itemId">项目id</param>
        /// <param name="oldPath">老图路径</param>
        /// <param name="newPath">新图路径</param>
        /// <param name="status">状态</param>
        /// <param name="summary">摘要</param>
        public void WriteMessage(int operate, string itemId, string oldPath, string newPath, int status, string summary)
        {
            SnowflakeIdWorker snowflakeIdWorker = new SnowflakeIdWorker(1, 1);
            Data_TempCopy tempCopyData = new Data_TempCopy();

            tempCopyData.Id = snowflakeIdWorker.NextId().ToString();
            tempCopyData.OperateKey = _OperateKey;
            tempCopyData.Operate = operate;
            tempCopyData.ItemId = itemId;
            tempCopyData.OldPath = oldPath;
            tempCopyData.NewPath = newPath;
            tempCopyData.Status = status;
            tempCopyData.Summary = summary;
            tempCopyData.CreateDate = DateTime.Now;

            if (status == 0)
            {
                int index = this._dataGV.Rows.Add();
                this._dataGV.Rows[index].Cells[0].Value = tempCopyData.Id;
                this._dataGV.Rows[index].Cells[1].Value = tempCopyData.OperateKey;
                this._dataGV.Rows[index].Cells[2].Value = OperatebyEmun((int)tempCopyData.Operate);
                this._dataGV.Rows[index].Cells[3].Value = tempCopyData.ItemId;
                this._dataGV.Rows[index].Cells[4].Value = tempCopyData.OldPath;
                this._dataGV.Rows[index].Cells[5].Value = tempCopyData.NewPath;
                this._dataGV.Rows[index].Cells[6].Value = StatusbyEmun((int)tempCopyData.Status);
                this._dataGV.Rows[index].Cells[7].Value = tempCopyData.Summary;
                this._dataGV.Rows[index].Cells[8].Value = tempCopyData.CreateDate;
            }
            else
            {

                using (var context = new DataModelEntities())
                {
                    context.Data_TempCopy.Add(tempCopyData);

                    int index = this._dataGV.Rows.Add();
                    this._dataGV.Rows[index].Cells[0].Value = tempCopyData.Id;
                    this._dataGV.Rows[index].Cells[1].Value = tempCopyData.OperateKey;
                    this._dataGV.Rows[index].Cells[2].Value = OperatebyEmun((int)tempCopyData.Operate);
                    this._dataGV.Rows[index].Cells[3].Value = tempCopyData.ItemId;
                    this._dataGV.Rows[index].Cells[4].Value = tempCopyData.OldPath;
                    this._dataGV.Rows[index].Cells[5].Value = tempCopyData.NewPath;
                    this._dataGV.Rows[index].Cells[6].Value = StatusbyEmun((int)tempCopyData.Status);
                    this._dataGV.Rows[index].Cells[7].Value = tempCopyData.Summary;
                    this._dataGV.Rows[index].Cells[8].Value = tempCopyData.CreateDate;

                    //context.SaveChangesAsync();
                    context.SaveChanges();
                }
            }

            this._dataGV.FirstDisplayedScrollingRowIndex = this._dataGV.Rows[this._dataGV.Rows.Count - 1].Index;
        }



        public void Current(int count, string itemId = null)
        {
            string showText = $"当前第：{count}";
            if (!string.IsNullOrEmpty(itemId))
            {
                showText = showText + $",【{itemId}】";
            }
            _tsslCurrent.Text = showText;
        }

        public void TimeCost(string times)
        {
            _tsslTimeCost.Text = $"耗时：{times}";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string OperatebyEmun(int status)
        {
            switch (status)
            {
                case (int)EnumOperate.TitleImg:
                    return EnumOperate.TitleImg.GetEnumText();
                case (int)EnumOperate.File:
                    return EnumOperate.File.GetEnumText();
                case (int)EnumOperate.Content:
                    return EnumOperate.Content.GetEnumText();
            }
            return EnumOperate.TitleImg.GetEnumText();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string StatusbyEmun(int status)
        {
            switch (status)
            {
                case (int)EnumStatus.OldFileUnExists:
                    return EnumStatus.OldFileUnExists.GetEnumText();
                case (int)EnumStatus.NewFileExists:
                    return EnumStatus.NewFileExists.GetEnumText();
                case (int)EnumStatus.Ok:
                    return EnumStatus.Ok.GetEnumText();
                case (int)EnumStatus.Unknown:
                    return EnumStatus.Unknown.GetEnumText();
                case (int)EnumStatus.Error:
                    return EnumStatus.Error.GetEnumText();
            }
            return EnumStatus.Unknown.GetEnumText();
        }
    }
}
