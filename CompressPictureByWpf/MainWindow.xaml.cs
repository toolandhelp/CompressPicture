using CompressPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompressPictureByWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private delegate void DelegateWriteMessage(string message);


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            Task task = Task.Run(() =>
            {
                TitleImgCope();
            });


            //btnStart.IsEnabled = task.IsCompleted;
        }



        private void TitleImgCope()
        {
            for (int i = 0; i < 10; i++)
            {
                WriteMessage($"+=={i}");
                Thread.Sleep(1000);
            }
          //  using (var context = new DataModelEntities())
            //{
            //    string message = "开始获取数据";

            //    if (this.InvokeRequired)
            //    {
            //        this.Invoke(new DelegateWriteMessage(WriteMessage), new object[] { message });
            //    }
            //    else
            //    {
            //        this.WriteMessage(message);
            //    }

            //    var data = context.Web_ItemLibrary
            //        .Where(o => o.IsDelete.ToString().Equals("0"))
            //        .OrderByDescending(o => o.CreateDate)
            //        .ToList();



            //    for (int i = 0; i < data.Count; i++)
            //    {
            //        message = $"路径：{data[i].ItemTitleImg} 状态：< > 待复制";
            //        if (this.InvokeRequired)
            //        {
            //            this.Invoke(new DelegateWriteMessage(WriteMessage), new object[] { message });
            //        }
            //        else
            //        {
            //            this.WriteMessage(message);
            //        }

            //    }


            //    message = $"共计{data.Count}条";


            //    if (this.InvokeRequired)
            //    {
            //        this.Invoke(new DelegateWriteMessage(WriteMessage), new object[] { message });
            //    }
            //    else
            //    {
            //        this.WriteMessage(message);
            //    }





            //}


        }



        private void WriteMessage(string message)
        {
            txtInfos.AppendText(message + Environment.NewLine);
        }
    }
}
