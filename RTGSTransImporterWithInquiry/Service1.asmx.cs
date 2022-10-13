using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using RTGSImporter;

namespace RTGSTransImporter
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    /// 

    public class AuthHeader : SoapHeader
    {
        public string Username;
        public string Password;
    }


    [WebService(Namespace = "result.MsgId")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Result
    {
        public string ErrorMessage = "";
        public string MsgId = "";
        public string StatusName = "";
    }


    public class Service1 : System.Web.Services.WebService
    {
        public AuthHeader Authentication;
        public string UserName = ConfigurationManager.AppSettings["UserName"];
        public string Password = ConfigurationManager.AppSettings["Password"];

        [WebMethod]
        public Result SendPacs08Short
            (
                string TransactionType_001_or_031_or_040_or_041, // CtgyPurpPrtry,
                string Currency_Only_BDT,//Ccy,
                decimal Sending_Amount,//IntrBkSttlmAmt
                string Charge_Bearer_Only_SHAR, //ChrgBr
                string Account_Holder_Name, //DbtrNm
                string Sender_Account, //DbtrAcctOthrId
                string Sender_Branch_RoutingNo, //DbtrAgtBranchId 
                string Receiver_Name, //CdtrNm
                string Receiver_Bank_BIC, //CdtrAgtBICFI
                string Receiver_Account,  //CdtrAcctOthrId
                string Receiver_Branch_RoutingNo,   //CdtrAgtBranchId 
                string Unstructured, //Ustrd
                string Payment_Reason, //PmntRsn
                string SendingDept, 
                bool Charge_Waived_True_Or_False //ChargeWaived

            )
        {
            Result result = new Result();
            RTGSTransImporter.pacs008 pacs = new pacs008();
            pacs = setPacs008(
               TransactionType_001_or_031_or_040_or_041, // CtgyPurpPrtry,
                Currency_Only_BDT,//Ccy,
                Sending_Amount,//IntrBkSttlmAmt
                Charge_Bearer_Only_SHAR,
                Account_Holder_Name,
                Sender_Account,
                Sender_Branch_RoutingNo,
                Receiver_Name,
                Receiver_Bank_BIC, 
                Receiver_Account,
                Receiver_Branch_RoutingNo,
                Unstructured,
                Payment_Reason,
                SendingDept,
                Charge_Waived_True_Or_False
                );

            result.ErrorMessage = Validate(pacs);
            if (result.ErrorMessage == "")
            {
                RTGSTransImporter.TeamRedDB db = new TeamRedDB();
                result.MsgId = db.InsertOutward008(pacs);
                WriteLog("Success");
            }

            return result;            
        }

        private pacs008 setPacs008
            (
                string CtgyPurpPrtry,
                string Ccy,
                decimal IntrBkSttlmAmt,
                string ChrgBr,
                string DbtrNm,
                string DbtrAcctOthrId,
                string DbtrAgtBranchId,
                string CdtrNm,
                string CdtrAgtBICFI,
                string CdtrAcctOthrId,
                string CdtrAgtBranchId,
                string Ustrd,
                string PmntRsn,
                string SendingDept,
                bool ChargeWaived
            )
        {
            RTGSTransImporter.pacs008 pacs = new pacs008();
            string Credt = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            pacs.CreDt = Credt + "Z";
            pacs.CreDtTm = Credt;
            pacs.BatchBookingID = "";
            pacs.NbOfTxs = 0;
            pacs.CtgyPurpPrtry = CtgyPurpPrtry;
            pacs.Ccy = Ccy;
            try
            {
                pacs.IntrBkSttlmAmt = IntrBkSttlmAmt;

            }
            catch (Exception)
            {
                pacs.IntrBkSttlmAmt = 0;
            }


            ///pacs.IntrBkSttlmAmt = 0;

            pacs.IntrBkSttlmDt = System.DateTime.Today.ToString("yyyy-MM-dd");
            pacs.ChrgBr = ChrgBr;
            pacs.InstgAgtBICFI = "";
            pacs.InstgAgtNm = "";
            pacs.InstgAgtBranchId = DbtrAgtBranchId;

            pacs.InstdAgtBICFI = CdtrAgtBICFI;
            pacs.InstdAgtNm = CdtrAgtBICFI;
            pacs.InstdAgtBranchId = CdtrAgtBranchId;
            pacs.DbtrNm = DbtrNm;

            pacs.DbtrPstlAdr = "";
            pacs.DbtrStrtNm = "";
            pacs.DbtrTwnNm = "";
            pacs.DbtrAdrLine = "";
            pacs.DbtrCtry = "";
            pacs.DbtrAcctOthrId = DbtrAcctOthrId;

            pacs.DbtrAgtBICFI = "";
            pacs.DbtrAgtNm = "";
            pacs.DbtrAgtBranchId = DbtrAgtBranchId;
            pacs.DbtrAgtAcctOthrId = "";
            pacs.DbtrAgtAcctPrtry = "";
            pacs.CdtrAgtBICFI = CdtrAgtBICFI;
            pacs.CdtrAgtNm = "";
            pacs.CdtrAgtBranchId = CdtrAgtBranchId;
            pacs.CdtrAgtAcctOthrId = "";
            pacs.CdtrAgtAcctPrtry = "";
            pacs.CdtrNm = CdtrNm;

            pacs.CdtrPstlAdr = "";
            pacs.CdtrStrtNm = "";
            pacs.CdtrTwnNm = "";
            pacs.CdtrAdrLine = "";
            pacs.CdtrCtry = "";
            pacs.CdtrAcctOthrId = CdtrAcctOthrId;

            pacs.CdtrAcctPrtry = "";
            pacs.InstrInf = "";
            pacs.Ustrd = Ustrd;
            pacs.PmntRsn = PmntRsn;
            pacs.SendingDept = SendingDept;
            try
            {
                pacs.ChargeWaived = ChargeWaived;
            }
            catch (Exception)
            {
                pacs.ChargeWaived = false;
                WriteLog("ChargeWaived not boolean");
            }

            return pacs;

        }

        private string Validate(pacs008 pacs)
        {
            string errmsg = "";
              if ("001".IndexOf(pacs.CtgyPurpPrtry) < -1)
            //if (pacs.CtgyPurpPrtry == "" || pacs.CtgyPurpPrtry != "001" || pacs.CtgyPurpPrtry != "031")
            {
                errmsg = errmsg + "\n" + "Invalid Category purpose ID ";
            }

            string[] CtgyPurpPrtry = { "001", "031", "040", "041" };
            int ind1 = Array.IndexOf(CtgyPurpPrtry, pacs.CtgyPurpPrtry.ToString());
            if (ind1 == -1)
            {
                errmsg = errmsg + "\n" + "Invalid Category purpose ID ";
            }


            if (pacs.IntrBkSttlmAmt == 0)
            {
                errmsg = errmsg + "\n" + "Invalid Amount ";
            }

            if (pacs.InstgAgtBranchId == "")
            {
                errmsg = errmsg + "\n" + "Invalid BranchID ";
            }
            if ("BDT,USD,GPY".IndexOf(pacs.Ccy) < 0)
                if (pacs.Ccy == "")
                {
                    errmsg = errmsg + "\n" + "Invalid Currency ";
                }

              if (pacs.ChrgBr == "")
                {
                    errmsg = errmsg + "\n" + "Invalid Charge Bearer ";
                }

            if (pacs.InstdAgtBICFI == "")
            {
                errmsg = errmsg + "\n" + "Invalid InstdAgtBICFI ";
            }

            if (pacs.InstdAgtBranchId == "")
            {
                errmsg = errmsg + "\n" + "Invalid InstdAgtBranchId ";
            }

            string[] ccy = {"BDT","USD","GPY"};
            int ind = Array.IndexOf(ccy, pacs.Ccy.ToString());
            if (ind==-1)
            {
                errmsg = errmsg + "\n" + "Invalid Currency ";
            }

            string[] ChrgBr = { "SHAR", "DEBT", "CRED" };
            int ints = Array.IndexOf(ChrgBr, pacs.ChrgBr.ToString());
            if (ints == -1)
            {
                errmsg = errmsg + "\n" + "Invalid Charge Bearer ";
            }



            if (pacs.DbtrNm == "")
            {
                errmsg = errmsg + "\n" + "Debitor Name Need FillUp ";
            }

            if (pacs.DbtrAgtBranchId == "")
            {
                errmsg = errmsg + "\n" + "Debitor BranchID Need To Fillup ";
            }


            if (pacs.DbtrAcctOthrId == "")
            {
                errmsg = errmsg + "\n" + "Debitor Account Must be Fillup ";
            }


            if (pacs.CdtrAgtBranchId == "")
            {
                errmsg = errmsg + "\n" + "Creditor BranchID Need FillUp ";
            }

            if (pacs.CdtrAcctOthrId == "")
            {
              
                errmsg = errmsg + "\n" + "Creditor Account Must be Fillup ";
            }

            if (pacs.CdtrNm == "")
            {
                errmsg = errmsg + "\n" + "Creditor Name Need FillUp ";
            }
        
                 return errmsg;

        }

        private void WriteLog(string log)
        {
            string fileName = HttpContext.Current.Request.MapPath("MyLogs.txt");
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(DateTime.Now.ToString() + " " + log);
            sw.Close();
               
        }
         [WebMethod]
        public DataSet GetRTGSStatus(string MsgID)
        {
            TeamRedDB db = new TeamRedDB();
            DataTable dt = db.GetRTGSStatus(MsgID);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }


    }
}