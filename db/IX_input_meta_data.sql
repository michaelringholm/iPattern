USE [iPattern]
GO

/****** Object:  Index [IX_input_meta_data]    Script Date: 06/14/2011 14:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_input_meta_data] ON [dbo].[input_meta_data] 
(
	[analysis_input_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


