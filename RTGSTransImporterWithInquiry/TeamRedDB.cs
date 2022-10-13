using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RTGS;


namespace RTGSTransImporter
{
    internal class TeamRedDB
    {
        public string InsertOutward008(pacs008 pacs)
        {
            SqlConnection myConnection = new SqlConnection(RTGS.AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("WS_InsertOutwardPacs08shortpage", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterCreDt = new SqlParameter("@CreDt", SqlDbType.VarChar, 50);
            parameterCreDt.Value = pacs.CreDt;
            myCommand.Parameters.Add(parameterCreDt);

            SqlParameter parameterCreDtTm = new SqlParameter("@CreDtTm", SqlDbType.VarChar, 50);
            parameterCreDtTm.Value = pacs.CreDtTm;
            myCommand.Parameters.Add(parameterCreDtTm);

            SqlParameter parameterCtgyPurpPrtry = new SqlParameter("@CtgyPurpPrtry", SqlDbType.VarChar, 35);
            parameterCtgyPurpPrtry.Value = pacs.CtgyPurpPrtry;
            myCommand.Parameters.Add(parameterCtgyPurpPrtry);

            SqlParameter parameterCcy = new SqlParameter("@Ccy", SqlDbType.VarChar, 3);
            parameterCcy.Value = pacs.Ccy;
            myCommand.Parameters.Add(parameterCcy);

            SqlParameter parameterIntrBkSttlmAmt = new SqlParameter("@IntrBkSttlmAmt", SqlDbType.Money);
            parameterIntrBkSttlmAmt.Value = pacs.IntrBkSttlmAmt;
            myCommand.Parameters.Add(parameterIntrBkSttlmAmt);

            SqlParameter parameterIntrBkSttlmDt = new SqlParameter("@IntrBkSttlmDt", SqlDbType.VarChar, 10);
            parameterIntrBkSttlmDt.Value = pacs.IntrBkSttlmDt;
            myCommand.Parameters.Add(parameterIntrBkSttlmDt);

            SqlParameter parameterChrgBr = new SqlParameter("@ChrgBr", SqlDbType.VarChar, 4);
            parameterChrgBr.Value = pacs.ChrgBr;
            myCommand.Parameters.Add(parameterChrgBr);

            SqlParameter parameterDbtrNm = new SqlParameter("@DbtrNm", SqlDbType.VarChar, 140);
            parameterDbtrNm.Value = pacs.DbtrNm;
            myCommand.Parameters.Add(parameterDbtrNm);

            SqlParameter parameterDbtrAcctOthrId = new SqlParameter("@DbtrAcctOthrId", SqlDbType.VarChar, 34);
            parameterDbtrAcctOthrId.Value = pacs.DbtrAcctOthrId;
            myCommand.Parameters.Add(parameterDbtrAcctOthrId);

            SqlParameter parameterDbtrAgtBranchId = new SqlParameter("@DbtrAgtBranchId", SqlDbType.VarChar, 20);
            parameterDbtrAgtBranchId.Value = pacs.DbtrAgtBranchId;
            myCommand.Parameters.Add(parameterDbtrAgtBranchId);

            SqlParameter parameterCdtrAgtBICFI = new SqlParameter("@CdtrAgtBICFI", SqlDbType.VarChar, 8);
            parameterCdtrAgtBICFI.Value = pacs.CdtrAgtBICFI;
            myCommand.Parameters.Add(parameterCdtrAgtBICFI);

            SqlParameter parameterCdtrAgtBranchId = new SqlParameter("@CdtrAgtBranchId", SqlDbType.VarChar, 20);
            parameterCdtrAgtBranchId.Value = pacs.CdtrAgtBranchId;
            myCommand.Parameters.Add(parameterCdtrAgtBranchId);

            SqlParameter parameterCdtrNm = new SqlParameter("@CdtrNm", SqlDbType.VarChar, 140);
            parameterCdtrNm.Value = pacs.CdtrNm;
            myCommand.Parameters.Add(parameterCdtrNm);

            SqlParameter parameterCdtrAcctOthrId = new SqlParameter("@CdtrAcctOthrId", SqlDbType.VarChar, 34);
            parameterCdtrAcctOthrId.Value = pacs.CdtrAcctOthrId;
            myCommand.Parameters.Add(parameterCdtrAcctOthrId);

            SqlParameter parameterUstrd = new SqlParameter("@Ustrd", SqlDbType.VarChar, 140);
            parameterUstrd.Value = pacs.Ustrd;
            myCommand.Parameters.Add(parameterUstrd);

            SqlParameter parameterPmntRsn = new SqlParameter("@PmntRsn", SqlDbType.VarChar, 140);
            parameterPmntRsn.Value = pacs.PmntRsn;
            myCommand.Parameters.Add(parameterPmntRsn);

            SqlParameter parameterSendingDept = new SqlParameter("@SendingDept", SqlDbType.VarChar, 50);
            parameterSendingDept.Value = pacs.SendingDept;
            myCommand.Parameters.Add(parameterSendingDept);

            SqlParameter parameterChargeWaived = new SqlParameter("@ChargeWaived", SqlDbType.Bit);
            parameterChargeWaived.Value = pacs.ChargeWaived;
            myCommand.Parameters.Add(parameterChargeWaived);

            SqlParameter parameterMsgId = new SqlParameter("@MsgId", SqlDbType.VarChar, 50);
            parameterMsgId.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMsgId);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            string MsgId = parameterMsgId.Value.ToString();

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return MsgId;


        }

        public DataTable GetRTGSStatus(string MsgID)
           
        {


            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlDataAdapter myCommand = new SqlDataAdapter("WS_RTGSSearchPacs008StatusName", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterMsgID = new SqlParameter("@MsgID", SqlDbType.VarChar, 50);
            parameterMsgID.Value = MsgID;
            myCommand.SelectCommand.Parameters.Add(parameterMsgID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;

        }
    }
}
