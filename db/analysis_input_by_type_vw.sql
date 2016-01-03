USE [iPattern]
GO

/****** Object:  View [dbo].[analysis_input_by_type_vw]    Script Date: 06/10/2011 13:36:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
ALTER VIEW [dbo].[analysis_input_by_type_vw]
AS
SELECT     ai.id, ai.text_input, ai.status, ai.event_time, ar.information_type_id, ai.area_id, ar.is_read, ar.id AS analysis_result_id, imd.meta_value AS email, 
                      a.filter_on_unknown
FROM         dbo.analysis_input AS ai INNER JOIN
                      dbo.analysis_result AS ar ON ar.analysis_input_id = ai.id INNER JOIN
                      dbo.area AS a ON a.id = ai.area_id LEFT OUTER JOIN
                      dbo.input_meta_data AS imd ON imd.analysis_input_id = ai.id
WHERE     (imd.title = 'mailbox_user')

GO

