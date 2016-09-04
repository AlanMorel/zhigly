using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Web.Services;
using Zhigly.Code;

namespace Zhigly
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public void report(int listing, int reason)
        {
            Debug.WriteLine("REPORTING ID: " + listing + " FOR REASON: " + reason);

            bool result = Database.Report(listing, reason);

            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(result));
        }
    }
}
