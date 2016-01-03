USE [iPattern]
GO

/****** Object:  Index [IX_information_type_3]    Script Date: 06/14/2011 15:49:07 ******/
CREATE NONCLUSTERED INDEX [IX_information_type_3] ON [dbo].[information_type] 
(
	[title] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


