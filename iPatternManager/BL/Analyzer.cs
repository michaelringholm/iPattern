using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Xml.XPath;
using DTL;
using DAL;

namespace BL
{

    public class Analyzer
    {
        public static List<AnalysisResultDTO> AnalyzeText(String xml)
        {
            try
            {
                //EventLog.WriteEntry("iHedge Services", "AnalyzeText");                
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);

                try
                {
                    String type = xDoc.SelectSingleNode("incomingMail/content/").Attributes["type"].Value;

                    /*if(!String.IsNullOrEmpty(type))
                    {
                        type = type.ToLower();
                        type = type.Substring(type.IndexOf("charset")+7);
                        if(type.IndexOf(";"))

                    }*/
                }
                catch (Exception ex)
                {

                }

                String base64Content = xDoc.SelectSingleNode("incomingMail/content").InnerText;
                byte[] contentBytes = Convert.FromBase64String(base64Content);
                String content = Encoding.GetEncoding("ISO-8859-1").GetString(contentBytes);

                Dictionary<String, String> metaValues = ExtractInputMetaData(xDoc);//Emailfolder
                //throw new Exception("Step 2");
                //TEMPORARY 
                if (content.Contains("PULL = "))
                {
                    string text = content.Substring(content.IndexOf("PULL = ")+7, content.IndexOf("PULLEND") - content.IndexOf("PULL = ") - 8);
                    metaValues.Add("owner", text);
                }
                //TEMP-end
                Int32 areaID = GetDefaultAreaID(metaValues);
                Int32 analysisInputID = InputDAC.StoreTextInput(content, areaID);

                foreach (KeyValuePair<String, String> metaItem in metaValues)
                {
                    InputDAC.StoreMetaData(new InputMetaDataDTO
                    {
                        InputID = analysisInputID,
                        Title = metaItem.Key,
                        MetaValue = metaItem.Value
                    });
                }

                //throw new Exception("Step 3");
                FindAndStoreAttachments(xDoc, analysisInputID);

                AnalysisDAC.StoreWordHeaders(content, analysisInputID);
                //throw new Exception("Step 4");
                return GetAnalysisResults(analysisInputID, areaID, content);
            }
            catch (Exception ex)
            {
                throw new Exception("XML = [" + xml + "]\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

		private static void FindAndStoreAttachments(XmlDocument xDoc, Int32 analysisInputID)
        {
            //XmlNodeList attachmentNodeList = xDoc.SelectNodes("IFlowMessageDTO/Attachments/Attachment");
            XmlNodeList attachmentNodeList = xDoc.SelectNodes("incomingMail/attachments/attachment");
			foreach (XmlNode attachmentNode in attachmentNodeList)
			{
                    String contentAsBase64 = attachmentNode.InnerText;
                    String path = attachmentNode.Attributes["filename"].InnerText;
                    String ext = Path.GetExtension(path);
                    String title = Path.GetFileName(path);
                //String title = attachmentNode.SelectSingleNode("title").InnerText;
                //String ext = attachmentNode.SelectSingleNode("extension").InnerText;
                //String contentAsBase64 = attachmentNode.SelectSingleNode("content").InnerText;

				InputDAC.StoreAttachment(new AttachmentDTO
				{
					AnalysisInputID = analysisInputID,
                    Title = title,
                    Filename = title + "." + ext,
					BinaryData = Convert.FromBase64String(contentAsBase64)
				});
			}
		}

		public static List<AnalysisResultDTO> RerunAnalysis(int analysisInputID, Int32 areaID)
		{
			String text = InputDAC.GetInputMessage(analysisInputID).TextInput;
			InputDAC.DeleteWordHeaders(analysisInputID);
			AnalysisDAC.StoreWordHeaders(text, analysisInputID); // This section could be taken out in time
			return GetAnalysisResults(analysisInputID, areaID, text);
		}

		private static List<AnalysisResultDTO> GetAnalysisResults(Int32 analysisInputID, Int32 areaID, String text)
		{
			ResultDAC.GenerateResult(analysisInputID, areaID);

			List<AnalysisResultDTO> results = ResultDAC.GetResults(analysisInputID, areaID);

			foreach (AnalysisResultDTO result in results)
			{				
				IdentifyMetaDataValues(text, result.InformationTypeID, result.ID.Value);
				IQProxy.IQProxy iqProxy = new IQProxy.IQProxy(ConfigurationManager.ConnectionStrings["iqdb"]);
				String iPatternMessage = "<ipattern><informationTypeTitle>" + result.InformationTypeTitle + "</informationTypeTitle>";
				iPatternMessage += "<messageID>" + result.ID.Value +"</messageID>";
				iPatternMessage += "<text>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(text)) +"</text>";
				iPatternMessage += "</ipattern>";
				iqProxy.PutMessage(ConfigurationManager.AppSettings["WSSSyncIngoingQueueName"], iPatternMessage, null);
			}

			return results;
		}

        private static Dictionary<String, String> ExtractInputMetaData(XmlDocument xDoc)
        {
            Dictionary<String, String> metaValues = new Dictionary<String, String>();
            //foreach (XmlNode metaDataNode in xDoc.SelectNodes("IFlowMessageDTO/Metadata/MetadataItem"))
            //    metaValues.Add(metaDataNode.SelectSingleNode("key").InnerText, metaDataNode.SelectSingleNode("value").InnerText);
            //return metaValues; 

            //foreach (XmlNode metaDataNode in xDoc.SelectNodes("iflowinput/metadata/metadataitem"))
            //    metaValues.Add(metaDataNode.SelectSingleNode("key").InnerText, metaDataNode.SelectSingleNode("value").InnerText);
            //return metaValues;

            //XPathNavigator nav = xDoc.CreateNavigator();
            //XPathExpression expr;
            //expr = nav.Compile("incomingMail/from/address");
            //metaValues["from"] = nav.Select(expr).ToString();
            //expr = nav.Compile("incomingMail/mailboxUser");
            //metaValues["mailbox_user"] = nav.Select(expr).ToString();
            //expr = nav.Compile("incomingMail/mailboxFolder");
            //metaValues["mailbox_folder"] = nav.Select(expr).ToString();
            //expr = nav.Compile("incomingMail/sent");
            //metaValues["sent"] = nav.Select(expr).ToString();
            //expr = nav.Compile("incomingMail/subject");
            //metaValues["subject"] = nav.Select(expr).ToString();

            metaValues["from"] = xDoc.SelectSingleNode("incomingMail/from/address").InnerText;
            metaValues["mailbox_user"] = xDoc.SelectSingleNode("incomingMail/mailboxUser").InnerText;
            metaValues["mailbox_folder"] = xDoc.SelectSingleNode("incomingMail/mailboxFolder").InnerText;
            metaValues["sent"] = xDoc.SelectSingleNode("incomingMail/sent").InnerText;
            metaValues["subject"] = xDoc.SelectSingleNode("incomingMail/subject").InnerText;

            return metaValues;
        }

        private static Int32 GetDefaultAreaID(Dictionary<String, String> metaValues)
        {
            Nullable<Int32> defaultAreaID = AreaDAC.GetAreaIDByAddress(metaValues["mailbox_user"], metaValues["from"]); 
            if (defaultAreaID.HasValue)
                return defaultAreaID.Value;
             
            //MembershipUserCollection users = Membership.FindUsersByEmail(metaValues["from"]);
            //IEnumerator e = users.GetEnumerator();
            //if (e.MoveNext())
            //{
            //    Nullable<Int32> defaultAreaID = UserDAC.GetDefaultAreaId(((MembershipUser)e.Current).ProviderUserKey);
            //    if (defaultAreaID.HasValue)
            //        return defaultAreaID.Value;
            //}
            else 
                throw new Exception("No default area found");
        }      

        public static void IdentifyMetaDataValues(String text, Int32 informationTypeID, Int32 analysisResultID)
        {
            MetaDAC.DeleteValues(analysisResultID);
            List<MetaDataKeyDTO> metaDataKeys = MetaDAC.GetMetaDataKeys(informationTypeID);

            foreach (MetaDataKeyDTO metaDataKey in metaDataKeys)
            {
                //MatchCollection matches = new Regex(@"[0-9]{4}\s[0-9]{4}|[0-9]{8}|[0-9]{2}\s[0-9]{2}\s[0-9]{2}\s[0-9]{2}", RegexOptions.IgnoreCase).Matches(text);
                MatchCollection matches = new Regex(metaDataKey.RegEx).Matches(text);

                List<String> values = new List<string>();
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        values.Add(match.Value);
                        MetaDAC.StoreValue(new MetaDataValueDTO
                        {
                            AnalysisResultID = analysisResultID,
                            MetaDataKeyID = metaDataKey.ID.Value,
                            MetaValue = match.Value
                        });
                    }
                }
            }

        }


    }

}
