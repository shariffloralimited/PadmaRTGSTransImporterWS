USE [RTGS]
GO
/****** Object:  StoredProcedure [dbo].[WS_InsertOutwardPacs08shortpage]    Script Date: 11/21/2017 12:07:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[WS_InsertOutwardPacs08shortpage] 
(
	@CtgyPurpPrtry		varchar(35),
	@Ccy				varchar(3),
	@IntrBkSttlmAmt		money,
	@ChrgBr				varchar(4),

	@CreDt				varchar(50),
	@CreDtTm			varchar(50),
	@IntrBkSttlmDt		varchar(10),


	@DbtrNm				varchar(140),
	@DbtrAcctOthrId		varchar(34),
	@DbtrAgtBranchId	varchar(20),

	@CdtrAgtBICFI		varchar(8),
	@CdtrAgtBranchId	varchar(20),

	@CdtrNm				varchar(140),
	@CdtrAcctOthrId		varchar(34),
	
	@Ustrd				varchar(140),
	@PmntRsn			varchar(140),
	@ChargeWaived		bit,

	@SendingDept        varchar(50),

	@MsgID				varchar(50) OUTPUT	
)
AS
BEGIN
	DECLARE @OutwardID uniqueidentifier, @DbtrAgtAcctOthrId varchar(34), @CdtrAgtAcctOthrId varchar(34), @BranchCD varchar(4)
	SET @OutwardID = NEWID()
	
	SELECT @DbtrAgtAcctOthrId = AcctNo
	FROM RTGS_CBAccount
	WHERE CCY = @Ccy
	AND AcctType = 'SA'
	AND BICFI = 'NRBLBDDH'
	
	SELECT @CdtrAgtAcctOthrId = AcctNo
	FROM RTGS_CBAccount
	WHERE CCY = @Ccy
	AND AcctType = 'SA'
	AND BICFI = @CdtrAgtBICFI
	
	BEGIN TRANSACTION
	INSERT INTO Outward08
	(
		OutwardID,
		DetectTime,
		FrBICFI,
		ToBICFI,
		MsgDefIdr,
		BizSvc,
		CreDt,
		CreDtTm,
		BtchBookg,
		NbOfTxs,
		ClrChanl,
		SvcLvlPrtry,
		LclInstrmPrtry,
		CtgyPurpPrtry,
		Ccy,
		IntrBkSttlmAmt,
		IntrBkSttlmDt,
		TtlIntrBkSttlmAmt,
		ChrgBr,
		InstgAgtBICFI,
		InstgAgtNm,
		InstgAgtBranchId,
		InstdAgtBICFI,
		InstdAgtNm,
		InstdAgtBranchId,
		DbtrNm,
		DbtrAcctOthrId,
		DbtrAgtBICFI,
		DbtrAgtNm,
		DbtrAgtBranchId,
		DbtrAgtAcctOthrId,
		DbtrAgtAcctPrtry,
		CdtrAgtBICFI,
		CdtrAgtNm,
		CdtrAgtBranchId,
		CdtrAgtAcctOthrId,
		CdtrAgtAcctPrtry,
		CdtrNm,
		CdtrAcctOthrId,
		CdtrAcctPrtry,
		InstrInf,
		Ustrd,
		PmntRsn,
		Maker,
		MakeTime,
		StatusID,
		ChargeWaived
	)
	VALUES
	(
		@OutwardID,	
		GETDATE(),	
		'NRBLBDDH',	
		'BBHOBDDHRTG', --@ToBICFI,	
		'pacs.008.001.04',	
		'RTGS_SSCT',	
		@CreDt,	
		@CreDtTm,	
		'false',	
		1,	
		'RTGS',	
		'75',	
		'RTGS_SSCT',	
		@CtgyPurpPrtry,	
		@Ccy,	
		@IntrBkSttlmAmt,	
		@IntrBkSttlmDt,
		@IntrBkSttlmAmt,	
		@ChrgBr,	
		'NRBLBDDH',	
		'NRBLBDDH',	
		@DbtrAgtBranchId,	
		@CdtrAgtBICFI,	
		@CdtrAgtBICFI,	
		@CdtrAgtBranchId,	
		@DbtrNm,	
		@DbtrAcctOthrId,	
		'NRBLBDDH',	
		'NRBLBDDH',	
		@DbtrAgtBranchId,	
		'30001708112',	
		'',						--@DbtrAgtAcctPrtry,	-- find from xml sample
		@CdtrAgtBICFI,	
		@CdtrAgtBICFI,
		@CdtrAgtBranchId,	
		@CdtrAgtAcctOthrId,	
		'',						--@CdtrAgtAcctPrtry,	-- find from xml sample
		@CdtrNm,	
		@CdtrAcctOthrId,	
		'',						--@CdtrAcctPrtry,		-- find from xml sample
		@PmntRsn,	
		@Ustrd,	
		@PmntRsn,	
		@SendingDept,	
		GETDATE(),	
		2,	
		@ChargeWaived	

	)
	SELECT @BranchCD = BranchCD FROM ACHUser.dbo.ACH_Branch WHERE RoutingNo =  @DbtrAgtBranchId

	SELECT @MsgID = 'NRB' + CONVERT(varchar(6),GetDate(),12) + '8'+ RIGHT('00000'+ CAST(SLNo as varchar),5)
	FROM Outward08 
	WHERE OutwardID =  @OutwardID

	UPDATE Outward08   
	SET MsgID = @MsgID, BizMsgIdr = @MsgId, InstrId = @MsgId, EndToEndId = @MsgId, TxId = @MsgId, BrnchCD = @BranchCD
	WHERE OutwardID = @OutwardID

	if @CtgyPurpPrtry in (031, 040,041) 
	BEGIN 
	update Outward08
	set ChargeWaived=1
	END 

  
	COMMIT TRANSACTION
END













