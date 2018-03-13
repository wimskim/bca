using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CryptoTrader
{
    public partial class btcusdt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.DollarCostAvg.CalculateDollarCostAvg();
        }
    }
}