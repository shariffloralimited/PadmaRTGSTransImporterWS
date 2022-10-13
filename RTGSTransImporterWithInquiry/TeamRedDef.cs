using System;

namespace RTGSImporter
{
        public class Pacs008
        {
            public string FileName = "";
            public string PacsID = "";
            public string DetectTime = "";
            public string FrBICFI = "";
            public string ToBICFI = "";
            public string BizMsgIdr = "";
            public string MsgDefIdr = "";
            public string BizSvc = "";
            public string CreDt = "";
            public string MsgId = "";
            public string CreDtTm = "";
            public string BatchBookingID = "";
            public string BtchBookg = "";
            public int    NbOfTxs = 0;
            public string InstrId = "";
            public string EndToEndId = "";
            public string TxId = "";
            public string ClrChanl = "";
            public int SvcLvlPrtry = 0;
            public string LclInstrmPrtry = "";
            public string CtgyPurpPrtry = "";
            public string Ccy = "";
            public decimal TtlIntrBkSttlmAmt = 0;
            public decimal IntrBkSttlmAmt = 0;
            public string IntrBkSttlmDt = "";
            public string ChrgBr = "";
            public string InstgAgtBICFI = "";
            public string InstgAgtNm = "";
            public string InstgAgtBranchId = "";
            public string InstdAgtBICFI = "";
            public string InstdAgtNm = "";
            public string InstdAgtBranchId = "";
            public string DbtrNm = "";
            public string DbtrPstlAdr = "";
            public string DbtrStrtNm = "";
            public string DbtrTwnNm = "";
            public string DbtrAdrLine = "";
            public string DbtrCtry = "";
            public string DbtrAcctOthrId = "";
            public string DbtrAgtBICFI = "";
            public string DbtrAgtNm = "";
            public string DbtrAgtBranchId = "";
            public string DbtrAgtAcctOthrId = "";
            public string DbtrAgtAcctPrtry = "";
            public string CdtrAgtBICFI = "";
            public string CdtrAgtNm = "";
            public string CdtrAgtBranchId = "";
            public string CdtrAgtAcctOthrId = "";
            public string CdtrAgtAcctPrtry = "";
            public string CdtrNm = "";
            public string CdtrPstlAdr = "";
            public string CdtrStrtNm = "";
            public string CdtrTwnNm = "";
            public string CdtrAdrLine = "";
            public string CdtrCtry = "";
            public string CdtrAcctOthrId = "";
            public string CdtrAcctPrtry = "";
            public string InstrInf = "";
            public string Ustrd = "";
            public string PmntRsn = "";
            public string ReturnReason = "";
            public int DeptId = 0;
            public string Maker = "";
            public string MakeTime = "";
            public string MakerIP = "";
            public string Checker = "";
            public string CheckTime = "";
            public string CheckerIP = "";
            public string Admin = "";
            public string AdminTime = "";
            public string AdminIP = "";
            public string DeletedBy = "";
            public string DeleteTime = "";
            public string CBSResponse = "";
            public string CBSTime = "";
            public int StatusID = 0;
            public string FrBankBIC = "";
            public string FrBank = ""; 
            public string FrBranch = "";
            public string ToBranch = "";
            public string BrnchCD = "";
            public bool ChargeWaived = false;
            public string OutwardID = "";
        }

        public class Pacs002
        {
            public string FileName = "";
            public string Pacs02ID = "";
            public string DetectTime = "";
            public string FrBICFI = "";
            public string ToBICFI = "";
            public string BizMsgIdr = "";
            public string MsgDefIdr = "";
            public string BizSvc = "";
            public string CreDt = "";
            public string MsgId = "";
            public string CreDtTm = "";
            public string OrgnlMsgId = "";
            public string OrgnlMsgNmId = "";
            public string OrgnlCreDtTm = "";
            public string GrpSts = "";
            public string RsnPrtry = "";
            public string AddtlInf = "";
            public string OrgnlInstrId = "";
            public string OrgnlEndToEndId = "";
            public string OrgnlTxId = "";
            public string TxSts = "";
            public string TxInfAndStsPrtry = "";
            public string TxInfAndStsAddtlInf = "";
        }

        public class Camt04
        {
            public string FileName = "";
            public string Camt04ID = "";
            public string DetectTime = "";
            public string FrBICFI = "";
            public string ToBICFI = "";
            public string BizMsgIdr = "";
            public string MsgDefIdr = "";
            public string BizSvc = "";
            public string CreDt = "";
            public string MsgHdrMsgId = "";
            public string CreDtTm = "";
            public string OrgnlBizQryMsgId = "";
            public string AcctIdOthrId = "";
            public string Cd = "";
            public string Ccy = "";
            public string AmtWthtCcy = "";
            public string CdtDbtInd = "";
            public string AnyBIC = "";
            public string XmlData = "";
        }

        public class Camt05
        {
            public string FileName = "";
            public string Camt05ID = "";
            public string DetectTime = "";
            public string MsgId = "";
            public string CreDtTm = "";
            public string ReqTpId = "";
            public string BICFI = "";
            public string TxId = "";
            public string IntrBkSttlmDt = "";
        }
}
