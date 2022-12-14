USE [RTGS]
GO
/****** Object:  StoredProcedure [dbo].[TRANS_GetResult]    Script Date: 11/21/2017 10:26:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [TRANS_GetResult] 'ABBL171029812018'
CREATE PROCEDURE [dbo].[WS_RTGSSearchPacs008StatusName](@MsgID Varchar (50) )
AS
BEGIN
	IF EXISTS (SELECT 1 FROM RTGS15.dbo.Outward08 WHERE MsgID = @MsgID)
	BEGIN
	SELECT
		MsgID,StatusName 
		FROM RTGS15.dbo.Outward08 
		,RTGS.dbo.RTGS_StatusOut
		WHERE MsgID = @MsgID
		AND RTGS15.dbo.Outward08.StatusID = RTGS.dbo.RTGS_StatusOut.StatusID
		--DECLARE @StatusName Varchar (50)
		--IF @StatusName !='Completed'
		--BEGIN
		--SELECT StatusName = 'Transaction is Uder Processing'
		--END
		--ELSE
		--BEGIN
		--select StatusName= @StatusName
		--END
	END
	END