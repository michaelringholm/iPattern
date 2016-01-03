USE [iPattern]
GO

/****** Object:  Index [IX_analysis_result_1]    Script Date: 06/14/2011 15:44:50 ******/
CREATE NONCLUSTERED INDEX [IX_analysis_result_1] ON [dbo].[analysis_result] 
(
	[analysis_input_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


