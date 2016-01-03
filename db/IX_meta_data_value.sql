USE [iPattern]
GO

/****** Object:  Index [IX_meta_data_value]    Script Date: 06/14/2011 15:46:20 ******/
CREATE NONCLUSTERED INDEX [IX_meta_data_value] ON [dbo].[meta_data_value] 
(
	[analysis_result_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


