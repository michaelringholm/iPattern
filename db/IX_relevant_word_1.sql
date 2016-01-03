USE [iPattern]
GO

/****** Object:  Index [IX_relevant_word_1]    Script Date: 06/14/2011 15:51:50 ******/
CREATE NONCLUSTERED INDEX [IX_relevant_word_1] ON [dbo].[relevant_word] 
(
	[information_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


