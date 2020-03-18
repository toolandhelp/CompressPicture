using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompressPicture
{
    public partial class ProPicUplToOSS : Form
    {
        private Comm _comm;
        public ProPicUplToOSS()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Base64.encodeBase64URLSafe(data.getBytes(ENCODING))

            //byte[] bytes = Base64.encodeBase64URLSafe(data.getBytes(ENCODING));

            var bytes = Encoding.UTF8.GetBytes("@上海泛墨数码科技有限公司");

            char[] padding = { '=' };
            string aa = Convert.ToBase64String(bytes, Base64FormattingOptions.None).TrimEnd(padding).Replace('+', '-').Replace('/', '_');

    //        string incoming = returnValue
    //.Replace('_', '/').Replace('-', '+');
    //        switch (returnValue.Length % 4)
    //        {
    //            case 2: incoming += "=="; break;
    //            case 3: incoming += "="; break;
    //        }
    //        byte[] bytes = Convert.FromBase64String(incoming);
    //        string originalText = Encoding.ASCII.GetString(bytes);
        }



    }
}
