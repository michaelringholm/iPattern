using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BL;
using System.IO;
using System.Text;
using System.Xml;
using System.Configuration;

[WebService(Namespace = "http://ihedge.dk/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {
    }

    public Int32 AreaID
    {
        get
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["areaID"]);
        }
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void SendToiPattern(String iFlowMessage)
    {
        try
        {
            Analyzer.AnalyzeText(iFlowMessage);
        }
        catch (Exception ex)
        {
            //ErrorDAC.LogError("iPatternWebService", "Service", "GetInformationTypeForiFlow", ex);
            File.WriteAllText(@"c:\temp\services_error.txt", ex.Message + ex.StackTrace);
            throw new Exception(ex.Message + ex.StackTrace);
        }
    }

    [WebMethod]
    public string GetInformationTypeForiFlow(String iFlowMessage)
    {
        try
        {
            Analyzer.AnalyzeText(iFlowMessage);
        }
        catch (Exception ex)
        {
            //ErrorDAC.LogError("iPatternWebService", "Service", "GetInformationTypeForiFlow", ex);
            File.WriteAllText(@"c:\temp\services_error.txt", ex.Message +  ex.StackTrace);
            throw;
        }
        /*List<AnalysisResultDTO> results = Analyzer.AnalyzeText(content, AreaID);
		XmlDocument xDoc = new XmlDocument();
		xDoc.LoadXml(iFlowMessage);

		String base64Content = xDoc.SelectSingleNode("IFlowMessageDTO/Content").InnerText;
		byte[] contentBytes = Convert.FromBase64String(base64Content);
		String content = Encoding.UTF8.GetString(contentBytes);

        xDoc.SelectSingleNode("IFlowMessageDTO/InquiryReply").InnerXml = resType;

        return xDoc.OuterXml;*/
		return "Søfragt";
    }
}
