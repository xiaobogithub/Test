USE [ChangeTech]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Question](
	[QuestionGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[HasSubItem] [bit] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PredictorCategory]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PredictorCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PredictorCategory](
	[PredictorCategoryGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_PredictorType] PRIMARY KEY NONCLUSTERED 
(
	[PredictorCategoryGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ButtonActionType]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ButtonActionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ButtonActionType](
	[ButtonActionTypeGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [tinyint] NOT NULL,
 CONSTRAINT [PK_ButtonActionType] PRIMARY KEY CLUSTERED 
(
	[ButtonActionTypeGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_ButtonActionType] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ButtonActionType', N'COLUMN',N'Type'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 indicate this type can be used by both primary button and secondary button. 1 indicates this type can be used by primary button only. 2 indicates this type can be used by secondary button only.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ButtonActionType', @level2type=N'COLUMN',@level2name=N'Type'
GO
/****** Object:  Table [dbo].[User]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[UserGUID] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[MobilePhone] [char](255) NOT NULL,
	[Gender] [nchar](10) NOT NULL,
	[LastLogon] [datetime] NOT NULL,
	[Security] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_User] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[GetPageDesignModelAsXML]    Script Date: 08/24/2009 15:54:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPageDesignModelAsXML]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Chen Pu>
-- Create date: <2009-08-03>
-- Description:	<Get page design model>
-- =============================================
CREATE PROCEDURE [dbo].[GetPageDesignModelAsXML] 
@pageGUID as uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

SELECT 
(
	SELECT 
	(
		SELECT [Control].Name "@Name",
			   [Control].Description "@Description",
		(
			SELECT
			(
				SELECT	ControlProperty.Name "@Name",
						ValueType "@ValueType",
						ControlProperty.Description "@Description",
						ControlProperty.IsRequired "@IsRequired"
				FROM ControlProperty
				WHERE [Control].ControlGUID = ControlProperty.ControlGUID
				ORDER BY ControlProperty.Name
				FOR XML PATH(''Property''), Type
			)
			FOR XML PATH(''Properties''), Type
		),
		(
			SELECT
			(
				SELECT ControlEvent.Name "@Name"
				FROM ControlEvent
				WHERE [Control].ControlGUID = ControlEvent.ControlGUID
				FOR XML PATH(''Event''), Type
			)
			FOR XML PATH(''Events''), Type
		)
		FROM [Control]
		FOR XML PATH(''Control''), Type
	)
	FOR XML PATH(''SystemControls''), Type),
		
	(
		SELECT	Page.Name "@Name", 
				Page.PageGUID "@PageGUID",
		(
			SELECT
			(
				SELECT	[Control].Name "@Name", 
						PageControl.PageControlGUID "@PageControlGUID",
				(
					SELECT
					(
						SELECT	ControlProperty.Name "@Name",
								PageControlProperty.Value "@Value"
						FROM PageControlProperty, ControlProperty
						WHERE	PageControlProperty.PropertyGUID = ControlProperty.ControlPropertyGUID AND
								PageControlProperty.PageControlGUID = PageControl.PageControlGUID
						FOR XML PATH(''Property''), Type
					)
					FOR XML PATH(''Properties''), Type
			   ),
			   (
					SELECT 
					(
						SELECT	ControlEvent.Name "@Name",
								PageControlEvent.ActionExpression "@ActionExpression",
								PageControlEvent.ConditionExpression "@ConditionExpression"
						FROM PageControlEvent, ControlEvent
						WHERE	PageControlEvent.EventGUID = ControlEvent.ControlEventGUID AND
								PageControlEvent.PageControlGUID = PageControl.PageControlGUID
						FOR XML PATH(''Event''), Type
					)
					FOR XML PATH(''Events''), Type
			 )
			 FROM PageControl, [Control]
			 WHERE Page.PageGUID = PageControl.PageGUID AND
				PageControl.ControlGUID = [Control].ControlGUID
			 FOR XML PATH(''Control''), Type
		 )
		 FOR XML PATH(''PageControls''), Type
	)
	FROM Page
	WHERE Page.PageGUID = @pageGuid
	FOR XML PATH(''Page''), Type
)
FOR XML PATH(''PageDesignModel''), Type
END
' 
END
GO
/****** Object:  Table [dbo].[PageTemplate]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageTemplate](
	[PageTemplateGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_PageTemplate] PRIMARY KEY CLUSTERED 
(
	[PageTemplateGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_PageTemplate] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Language]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Language]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Language](
	[LanguageGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanguageGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Language] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProgramStatus]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProgramStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProgramStatus](
	[ProgramStatusGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_ProgramStatus] PRIMARY KEY CLUSTERED 
(
	[ProgramStatusGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_ProgramStatus] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageContent]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageContent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageContent](
	[PageGUID] [uniqueidentifier] NOT NULL,
	[LanguageGUID] [uniqueidentifier] NOT NULL,
	[PageTemplateGUID] [uniqueidentifier] NOT NULL,
	[Heading] [nvarchar](50) NOT NULL,
	[Body] [ntext] NULL,
	[ImageGUID] [uniqueidentifier] NULL,
	[ImageLocation] [nvarchar](50) NULL,
	[VideoGUID] [uniqueidentifier] NULL,
	[PrimaryButtonCaption] [nvarchar](50) NOT NULL,
	[PrimaryButtonActionTypeGUID] [uniqueidentifier] NOT NULL,
	[PrimaryButtonActionParameter] [int] NOT NULL,
	[SecondaryButtonActionTypeGUID] [uniqueidentifier] NOT NULL,
	[SecondaryButtonActionParameter] [int] NOT NULL,
 CONSTRAINT [PK_PageContent] PRIMARY KEY CLUSTERED 
(
	[PageGUID] ASC,
	[LanguageGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageQuestion]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageQuestion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageQuestion](
	[PageQuestionGUID] [uniqueidentifier] NOT NULL,
	[PageGUID] [uniqueidentifier] NOT NULL,
	[LanguageGUID] [uniqueidentifier] NOT NULL,
	[QuestionGUID] [uniqueidentifier] NOT NULL,
	[Caption] [nvarchar](1024) NULL,
	[IsRequired] [bit] NOT NULL,
 CONSTRAINT [PK_PageQuestion] PRIMARY KEY CLUSTERED 
(
	[PageQuestionGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Predictor]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Predictor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Predictor](
	[PredictorGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[PredictorCategoryGUID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Predictor] PRIMARY KEY NONCLUSTERED 
(
	[PredictorGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserPerformance]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPerformance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPerformance](
	[UserPerformanceGUID] [uniqueidentifier] NOT NULL,
	[UserPlanningGUID] [uniqueidentifier] NOT NULL,
	[SessionGUID] [uniqueidentifier] NOT NULL,
	[Performance] [int] NOT NULL,
 CONSTRAINT [PK_UserPerformance_1] PRIMARY KEY CLUSTERED 
(
	[UserPerformanceGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserPlanning]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPlanning]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPlanning](
	[UserPlanningGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[UserGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[StartDay] [int] NOT NULL,
	[EndDay] [int] NOT NULL,
	[Target] [int] NOT NULL,
	[Unit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserPlanning] PRIMARY KEY CLUSTERED 
(
	[UserPlanningGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProgramUser]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProgramUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProgramUser](
	[UserGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Security] [int] NOT NULL,
 CONSTRAINT [PK_ProgramSecurity] PRIMARY KEY CLUSTERED 
(
	[UserGUID] ASC,
	[ProgramGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuestionAnswer]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuestionAnswer](
	[QuestionAnswerGUID] [uniqueidentifier] NOT NULL,
	[UserGUID] [uniqueidentifier] NOT NULL,
	[PageQuestionGUID] [uniqueidentifier] NOT NULL,
	[PageQuestionItemGUID] [uniqueidentifier] NULL,
	[UserInput] [ntext] NULL,
 CONSTRAINT [PK_QuestionAnswer] PRIMARY KEY CLUSTERED 
(
	[QuestionAnswerGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Video]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Video]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Video](
	[VideoGUID] [uniqueidentifier] NOT NULL,
	[VideoCategoryGUID] [uniqueidentifier] NULL,
	[Name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Video] PRIMARY KEY CLUSTERED 
(
	[VideoGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Image]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Image]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Image](
	[ImageGUID] [uniqueidentifier] NOT NULL,
	[ImageCategoryGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ImageGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SpecificIntervent]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpecificIntervent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SpecificIntervent](
	[SpecificInterventGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[InterventGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SpecificIntervent] PRIMARY KEY NONCLUSTERED 
(
	[SpecificInterventGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Intervent]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Intervent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Intervent](
	[InterventGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[PredictorGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Intervent] PRIMARY KEY NONCLUSTERED 
(
	[InterventGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PredictorMaterial]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PredictorMaterial]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PredictorMaterial](
	[PredictorReferenceGUID] [uniqueidentifier] NOT NULL,
	[PredictorGUID] [uniqueidentifier] NOT NULL,
	[URL] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_PredictorReference] PRIMARY KEY NONCLUSTERED 
(
	[PredictorReferenceGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'PredictorMaterial', N'COLUMN',N'PredictorGUID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK to Preditor table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PredictorMaterial', @level2type=N'COLUMN',@level2name=N'PredictorGUID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'PredictorMaterial', N'COLUMN',N'URL'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Reference to document or link to information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PredictorMaterial', @level2type=N'COLUMN',@level2name=N'URL'
GO
/****** Object:  Table [dbo].[PageQuestionItem]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageQuestionItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageQuestionItem](
	[PageQuestionItemGUID] [uniqueidentifier] NOT NULL,
	[LanguageGUID] [uniqueidentifier] NOT NULL,
	[PageQuestionGUID] [uniqueidentifier] NOT NULL,
	[Item] [nvarchar](1024) NOT NULL,
	[Feedback] [nvarchar](1024) NULL,
	[Score] [int] NULL,
 CONSTRAINT [PK_PageQuestionItem] PRIMARY KEY CLUSTERED 
(
	[PageQuestionItemGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_PageQuestionItem] UNIQUE NONCLUSTERED 
(
	[PageQuestionItemGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Program]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Program]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Program](
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[StatusGUID] [uniqueidentifier] NOT NULL,
	[DefaultLanguageGUID] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY NONCLUSTERED 
(
	[ProgramGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Session]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Session]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Session](
	[SessionGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Day] [int] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY NONCLUSTERED 
(
	[SessionGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[VideoCategory]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VideoCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VideoCategory](
	[VideoCategoryGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_VideoCategory] PRIMARY KEY CLUSTERED 
(
	[VideoCategoryGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ImageCategory]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ImageCategory](
	[ImageCategoryGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_ImageCategory] PRIMARY KEY CLUSTERED 
(
	[ImageCategoryGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SessionContent]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionContent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SessionContent](
	[SessionContentGUID] [uniqueidentifier] NOT NULL,
	[SessionGUID] [uniqueidentifier] NOT NULL,
	[PageSequenceGUID] [uniqueidentifier] NULL,
	[PageSequenceOrderNo] [int] NOT NULL,
 CONSTRAINT [PK_SessionContent] PRIMARY KEY NONCLUSTERED 
(
	[SessionContentGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageSequence]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageSequence]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageSequence](
	[PageSequenceGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[SpecificInterventGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Sequence] PRIMARY KEY NONCLUSTERED 
(
	[PageSequenceGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Page]    Script Date: 08/24/2009 15:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Page]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Page](
	[PageGUID] [uniqueidentifier] NOT NULL,
	[SequenceGUID] [uniqueidentifier] NOT NULL,
	[PageOrderNo] [int] NOT NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[PageGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[GetPagePreviewModelAsXML]    Script Date: 08/24/2009 15:54:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPagePreviewModelAsXML]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Chen Pu>
-- Create date: <2009-08-21>
-- Description:	<>
-- =============================================
CREATE PROCEDURE [dbo].[GetPagePreviewModelAsXML]
@pageGuid as uniqueidentifier,
@languageGuid as uniqueidentifier,
@programGuid as uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
	(
		SELECT [Session].SessionGUID "@GUID",
			   [Session].Name "@Name",
			   [Session].[Day] "@Day",
			   [Session].[Description] "@Description",
		(
			SELECT PredictorCategory.Name "@CategoryName",
				   PredictorCategory.[Description] "@CategoryDescription",
				   SessionContent.PageSequenceOrderNo "@Order",
			(
				SELECT Page.PageGUID "@PageGUID",
					   Page.PageOrderNo "@Order",
					   PageContent.Heading "@Heading",
					   PageContent.Body "@Body",
					   PageTemplate.Name "@Theme",
					   PageContent.PrimaryButtonCaption "@PrimaryButtonCaption",
					   PageContent.PrimaryButtonActionParameter "@PrimaryButtonActionParameter",
					   PageContent.SecondaryButtonActionParameter "@SecondaryButtonActionParameter",
					   [Image].Name "@Image",
					   PageContent.ImageLocation "@ImageLocation",
					   Video.Name "@Video",					   				   
				(	
					SELECT
					(		
						SELECT Question.Name "@Name",
						(
							SELECT 
							(
								SELECT Item "@Item",
									   Feedback "@Feedback",
									   Score "@Score"
								FROM PageQuestion, PageQuestionItem
								WHERE PageQuestion.PageQuestionGUID = PageQuestionItem.PageQuestionGUID AND
									  PageQuestionItem.LanguageGUID = @languageGuid
								FOR XML PATH(''Item''), Type							
							)
							FROM Question, PageQuestion
								WHERE Question.QuestionGUID = PageQuestion.QuestionGUID AND
									PageQuestion.PageGUID = @pageGuid AND
									PageQuestion.LanguageGUID = @languageGuid
							FOR XML PATH(''Items''), Type	
						)	
						FROM Question, PageQuestion
						WHERE PageQuestion.PageGUID = @pageGuid AND
						PageQuestion.LanguageGUID = @languageGuid AND
						PageQuestion.QuestionGUID = Question.QuestionGUID						
						FOR XML PATH(''Question''), Type
					)
					FOR XML PATH(''Questions''), Type
				)
				FROM Page,PageTemplate,
				 PageContent LEFT JOIN ButtonActionType PrimaryButtonActionType ON PageContent.PrimaryButtonActionTypeGUID = PrimaryButtonActionType.ButtonActionTypeGUID
				 LEFT JOIN ButtonActionType SecondaryButtonActionType ON PageContent.SecondaryButtonActionTypeGUID =  SecondaryButtonActionType.ButtonActionTypeGUID 
				 LEFT JOIN [Image] ON PageContent.ImageGUID = [Image].ImageGUID
				 LEFT JOIN Video ON PageContent.VideoGUID = Video.VideoGUID
				WHERE Page.PageGUID = @pageGuid AND
					  PageContent.LanguageGUID = @languageGuid AND
					  Page.PageGUID = PageContent.PageGUID AND
					  Page.SequenceGUID = PageSequence.PageSequenceGUID AND
					  PageSequence.SpecificInterventGUID = SpecificIntervent.SpecificInterventGUID AND
					  SpecificIntervent.InterventGUID = Intervent.InterventGUID AND
					  Intervent.PredictorGUID = Predictor.PredictorGUID AND
					  Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
					  PageContent.PageTemplateGUID = PageTemplate.PageTemplateGUID  
					  
				FOR XML PATH(''Page''), Type
			)
			FROM Page, PageSequence,SpecificIntervent,Intervent,Predictor,PredictorCategory, SessionContent
			WHERE Page.PageGUID = @pageGuid AND
				Page.SequenceGUID = PageSequence.PageSequenceGUID AND
				PageSequence.SpecificInterventGUID = SpecificIntervent.SpecificInterventGUID AND
				SpecificIntervent.InterventGUID = Intervent.InterventGUID AND
				Intervent.PredictorGUID = Predictor.PredictorGUID AND
				Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
				PageSequence.PageSequenceGUID = SessionContent.PageSequenceGUID
			FOR XML PATH(''PageSequence''), Type
		)
		FROM [Session], Page, PageSequence, SessionContent
		WHERE Page.PageGUID = @pageGuid AND
			Page.SequenceGUID = PageSequence.PageSequenceGUID AND
			SessionContent.PageSequenceGUID = PageSequence.PageSequenceGUID AND
			SessionContent.SessionGUID = [Session].SessionGUID
		
		FOR XML PATH(''Session''), Type
	),
	(
		SELECT
		(
			SELECT UserPlanning.Name "@Name",
				   UserPlanning.StartDay "@StartDate",
				   UserPlanning.EndDay "@EndDate",
				   UserPlanning.[Target] "@Target",
				   UserPlanning.Unit "@Unit"
			FROM UserPlanning
			WHERE UserPlanning.ProgramGUID = @programGuid
			FOR XML Path(''PlanningItem''), Type
		)
		FOR XML Path(''Planning''), Type
	)
	FOR XML PATH(''PagePreviewModel'')
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSessionAsXML]    Script Date: 08/24/2009 15:54:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSessionAsXML]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE  procedure [dbo].[GetSessionAsXML]
@sGUID as uniqueidentifier
as
--- Sample usage: GetSessionAsXML ''{76A742C5-956C-4A0C-B90C-CCFEF1F398C2}''
set nocount on


select SessionGUID, Name, [Description], [Day],
(
	SELECT PageSequenceOrderNo,
	(
	   SELECT PageSequence.Name,
	   (
			SELECT PageGUID,PageOrderNo, Name
			FROM PAGE			
			WHERE Page.SequenceGUID = PageSequence.PageSequenceGUID
			ORDER By PageOrderNo
			FOR XML AUTO, TYPE
	   )
	   FROM PageSequence
	   WHERE PageSequence.PageSequenceGUID = SessionContent.PageSequenceGUID
	FOR XML AUTO, TYPE
	)

	FROM SessionContent
	WHERE SessionContent.SessionGUID = [Session].SessionGUID
	Order By PageSequenceOrderNo 
	FOR XML AUTO, TYPE
)

from [Session] 
where Session.SessionGUID = @sGUID
FOR XML AUTO, TYPE


' 
END
GO
/****** Object:  Default [DF_Intervent_InterventGUID]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Intervent_InterventGUID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Intervent]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Intervent_InterventGUID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Intervent] ADD  CONSTRAINT [DF_Intervent_InterventGUID]  DEFAULT (newid()) FOR [InterventGUID]
END


End
GO
/****** Object:  Default [DF_Page_PageGUID]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Page_PageGUID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Page]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Page_PageGUID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Page_PageGUID]  DEFAULT (newid()) FOR [PageGUID]
END


End
GO
/****** Object:  Default [DF_Page_IsDeleted]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Page_IsDeleted]') AND parent_object_id = OBJECT_ID(N'[dbo].[Page]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Page_IsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Page_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
END


End
GO
/****** Object:  Default [DF_Sequence_SequenceGUID]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Sequence_SequenceGUID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageSequence]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sequence_SequenceGUID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PageSequence] ADD  CONSTRAINT [DF_Sequence_SequenceGUID]  DEFAULT (newid()) FOR [PageSequenceGUID]
END


End
GO
/****** Object:  Default [DF_Predictor_PredictorGUID]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Predictor_PredictorGUID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Predictor]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Predictor_PredictorGUID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Predictor] ADD  CONSTRAINT [DF_Predictor_PredictorGUID]  DEFAULT (newid()) FOR [PredictorGUID]
END


End
GO
/****** Object:  Default [DF_PredictorType_PredictorTypePK]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_PredictorType_PredictorTypePK]') AND parent_object_id = OBJECT_ID(N'[dbo].[PredictorCategory]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PredictorType_PredictorTypePK]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PredictorCategory] ADD  CONSTRAINT [DF_PredictorType_PredictorTypePK]  DEFAULT (newid()) FOR [PredictorCategoryGUID]
END


End
GO
/****** Object:  Default [DF_PredictorReference_PredictorReferencePK]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_PredictorReference_PredictorReferencePK]') AND parent_object_id = OBJECT_ID(N'[dbo].[PredictorMaterial]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PredictorReference_PredictorReferencePK]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PredictorMaterial] ADD  CONSTRAINT [DF_PredictorReference_PredictorReferencePK]  DEFAULT (newid()) FOR [PredictorReferenceGUID]
END


End
GO
/****** Object:  Default [DF_Program_ProgramGUID]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Program_ProgramGUID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Program]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Program_ProgramGUID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Program] ADD  CONSTRAINT [DF_Program_ProgramGUID]  DEFAULT (newid()) FOR [ProgramGUID]
END


End
GO
/****** Object:  Default [DF_ProgramSecurity_Security]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ProgramSecurity_Security]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProgramUser]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ProgramSecurity_Security]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ProgramUser] ADD  CONSTRAINT [DF_ProgramSecurity_Security]  DEFAULT ((20)) FOR [Security]
END


End
GO
/****** Object:  Default [DF_Session_SessionGUID]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Session_SessionGUID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Session_SessionGUID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_SessionGUID]  DEFAULT (newid()) FOR [SessionGUID]
END


End
GO
/****** Object:  Default [DF_SessionContent_SessionContentGUID]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_SessionContent_SessionContentGUID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SessionContent]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SessionContent_SessionContentGUID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SessionContent] ADD  CONSTRAINT [DF_SessionContent_SessionContentGUID]  DEFAULT (newid()) FOR [SessionContentGUID]
END


End
GO
/****** Object:  Default [DF_SpecificIntervent_SpecificInterventGUID]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_SpecificIntervent_SpecificInterventGUID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SpecificIntervent]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SpecificIntervent_SpecificInterventGUID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SpecificIntervent] ADD  CONSTRAINT [DF_SpecificIntervent_SpecificInterventGUID]  DEFAULT (newid()) FOR [SpecificInterventGUID]
END


End
GO
/****** Object:  Default [DF_User_LastLogon]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_LastLogon]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_LastLogon]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_LastLogon]  DEFAULT (getdate()) FOR [LastLogon]
END


End
GO
/****** Object:  Default [DF_User_Security]    Script Date: 08/24/2009 15:54:42 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_Security]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_Security]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Security]  DEFAULT ((0)) FOR [Security]
END


End
GO
/****** Object:  ForeignKey [FK_Image_ImageCategory]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Image_ImageCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Image]'))
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_ImageCategory] FOREIGN KEY([ImageCategoryGUID])
REFERENCES [dbo].[ImageCategory] ([ImageCategoryGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Image_ImageCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Image]'))
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_ImageCategory]
GO
/****** Object:  ForeignKey [FK_ImageCategory_Program]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageCategory_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageCategory]'))
ALTER TABLE [dbo].[ImageCategory]  WITH CHECK ADD  CONSTRAINT [FK_ImageCategory_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageCategory_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageCategory]'))
ALTER TABLE [dbo].[ImageCategory] CHECK CONSTRAINT [FK_ImageCategory_Program]
GO
/****** Object:  ForeignKey [FK_Intervent_Predictor]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Intervent_Predictor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Intervent]'))
ALTER TABLE [dbo].[Intervent]  WITH CHECK ADD  CONSTRAINT [FK_Intervent_Predictor] FOREIGN KEY([PredictorGUID])
REFERENCES [dbo].[Predictor] ([PredictorGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Intervent_Predictor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Intervent]'))
ALTER TABLE [dbo].[Intervent] CHECK CONSTRAINT [FK_Intervent_Predictor]
GO
/****** Object:  ForeignKey [FK_Page_Sequence]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_Sequence]') AND parent_object_id = OBJECT_ID(N'[dbo].[Page]'))
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Sequence] FOREIGN KEY([SequenceGUID])
REFERENCES [dbo].[PageSequence] ([PageSequenceGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_Sequence]') AND parent_object_id = OBJECT_ID(N'[dbo].[Page]'))
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Sequence]
GO
/****** Object:  ForeignKey [FK_PageContent_Image]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent]  WITH CHECK ADD  CONSTRAINT [FK_PageContent_Image] FOREIGN KEY([ImageGUID])
REFERENCES [dbo].[Image] ([ImageGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Image]
GO
/****** Object:  ForeignKey [FK_PageContent_Language]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent]  WITH CHECK ADD  CONSTRAINT [FK_PageContent_Language] FOREIGN KEY([LanguageGUID])
REFERENCES [dbo].[Language] ([LanguageGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Language]
GO
/****** Object:  ForeignKey [FK_PageContent_Page]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent]  WITH CHECK ADD  CONSTRAINT [FK_PageContent_Page] FOREIGN KEY([PageGUID])
REFERENCES [dbo].[Page] ([PageGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Page]
GO
/****** Object:  ForeignKey [FK_PageContent_PageTemplate]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_PageTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent]  WITH CHECK ADD  CONSTRAINT [FK_PageContent_PageTemplate] FOREIGN KEY([PageTemplateGUID])
REFERENCES [dbo].[PageTemplate] ([PageTemplateGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_PageTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_PageTemplate]
GO
/****** Object:  ForeignKey [FK_PageContent_PrimaryButtonActionType]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_PrimaryButtonActionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent]  WITH CHECK ADD  CONSTRAINT [FK_PageContent_PrimaryButtonActionType] FOREIGN KEY([PrimaryButtonActionTypeGUID])
REFERENCES [dbo].[ButtonActionType] ([ButtonActionTypeGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_PrimaryButtonActionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_PrimaryButtonActionType]
GO
/****** Object:  ForeignKey [FK_PageContent_SecondaryButtonActionType]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_SecondaryButtonActionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent]  WITH CHECK ADD  CONSTRAINT [FK_PageContent_SecondaryButtonActionType] FOREIGN KEY([SecondaryButtonActionTypeGUID])
REFERENCES [dbo].[ButtonActionType] ([ButtonActionTypeGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_SecondaryButtonActionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_SecondaryButtonActionType]
GO
/****** Object:  ForeignKey [FK_PageContent_Video]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_Video]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent]  WITH CHECK ADD  CONSTRAINT [FK_PageContent_Video] FOREIGN KEY([VideoGUID])
REFERENCES [dbo].[Video] ([VideoGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageContent_Video]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageContent]'))
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Video]
GO
/****** Object:  ForeignKey [FK_PageQuestion_Language]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestion_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestion]'))
ALTER TABLE [dbo].[PageQuestion]  WITH CHECK ADD  CONSTRAINT [FK_PageQuestion_Language] FOREIGN KEY([LanguageGUID])
REFERENCES [dbo].[Language] ([LanguageGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestion_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestion]'))
ALTER TABLE [dbo].[PageQuestion] CHECK CONSTRAINT [FK_PageQuestion_Language]
GO
/****** Object:  ForeignKey [FK_PageQuestion_Page]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestion_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestion]'))
ALTER TABLE [dbo].[PageQuestion]  WITH CHECK ADD  CONSTRAINT [FK_PageQuestion_Page] FOREIGN KEY([PageGUID])
REFERENCES [dbo].[Page] ([PageGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestion_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestion]'))
ALTER TABLE [dbo].[PageQuestion] CHECK CONSTRAINT [FK_PageQuestion_Page]
GO
/****** Object:  ForeignKey [FK_PageQuestion_Question]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestion_Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestion]'))
ALTER TABLE [dbo].[PageQuestion]  WITH CHECK ADD  CONSTRAINT [FK_PageQuestion_Question] FOREIGN KEY([QuestionGUID])
REFERENCES [dbo].[Question] ([QuestionGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestion_Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestion]'))
ALTER TABLE [dbo].[PageQuestion] CHECK CONSTRAINT [FK_PageQuestion_Question]
GO
/****** Object:  ForeignKey [FK_PageQuestionItem_Language]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestionItem_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestionItem]'))
ALTER TABLE [dbo].[PageQuestionItem]  WITH CHECK ADD  CONSTRAINT [FK_PageQuestionItem_Language] FOREIGN KEY([LanguageGUID])
REFERENCES [dbo].[Language] ([LanguageGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestionItem_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestionItem]'))
ALTER TABLE [dbo].[PageQuestionItem] CHECK CONSTRAINT [FK_PageQuestionItem_Language]
GO
/****** Object:  ForeignKey [FK_PageQuestionItem_PageQuestion]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestionItem_PageQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestionItem]'))
ALTER TABLE [dbo].[PageQuestionItem]  WITH CHECK ADD  CONSTRAINT [FK_PageQuestionItem_PageQuestion] FOREIGN KEY([PageQuestionGUID])
REFERENCES [dbo].[PageQuestion] ([PageQuestionGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageQuestionItem_PageQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageQuestionItem]'))
ALTER TABLE [dbo].[PageQuestionItem] CHECK CONSTRAINT [FK_PageQuestionItem_PageQuestion]
GO
/****** Object:  ForeignKey [FK_Sequence_SpecificIntervent]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Sequence_SpecificIntervent]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageSequence]'))
ALTER TABLE [dbo].[PageSequence]  WITH CHECK ADD  CONSTRAINT [FK_Sequence_SpecificIntervent] FOREIGN KEY([SpecificInterventGUID])
REFERENCES [dbo].[SpecificIntervent] ([SpecificInterventGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Sequence_SpecificIntervent]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageSequence]'))
ALTER TABLE [dbo].[PageSequence] CHECK CONSTRAINT [FK_Sequence_SpecificIntervent]
GO
/****** Object:  ForeignKey [FK_Predictor_PredictorType]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Predictor_PredictorType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Predictor]'))
ALTER TABLE [dbo].[Predictor]  WITH CHECK ADD  CONSTRAINT [FK_Predictor_PredictorType] FOREIGN KEY([PredictorCategoryGUID])
REFERENCES [dbo].[PredictorCategory] ([PredictorCategoryGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Predictor_PredictorType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Predictor]'))
ALTER TABLE [dbo].[Predictor] CHECK CONSTRAINT [FK_Predictor_PredictorType]
GO
/****** Object:  ForeignKey [FK_PredictorReference_Predictor]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PredictorReference_Predictor]') AND parent_object_id = OBJECT_ID(N'[dbo].[PredictorMaterial]'))
ALTER TABLE [dbo].[PredictorMaterial]  WITH CHECK ADD  CONSTRAINT [FK_PredictorReference_Predictor] FOREIGN KEY([PredictorGUID])
REFERENCES [dbo].[Predictor] ([PredictorGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PredictorReference_Predictor]') AND parent_object_id = OBJECT_ID(N'[dbo].[PredictorMaterial]'))
ALTER TABLE [dbo].[PredictorMaterial] CHECK CONSTRAINT [FK_PredictorReference_Predictor]
GO
/****** Object:  ForeignKey [FK_Program_Language]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Program_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[Program]'))
ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Language] FOREIGN KEY([DefaultLanguageGUID])
REFERENCES [dbo].[Language] ([LanguageGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Program_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[Program]'))
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_Language]
GO
/****** Object:  ForeignKey [FK_Program_ProgramStatus]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Program_ProgramStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Program]'))
ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_ProgramStatus] FOREIGN KEY([StatusGUID])
REFERENCES [dbo].[ProgramStatus] ([ProgramStatusGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Program_ProgramStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Program]'))
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_ProgramStatus]
GO
/****** Object:  ForeignKey [FK_ProgramSecurity_Program]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProgramSecurity_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProgramUser]'))
ALTER TABLE [dbo].[ProgramUser]  WITH CHECK ADD  CONSTRAINT [FK_ProgramSecurity_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProgramSecurity_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProgramUser]'))
ALTER TABLE [dbo].[ProgramUser] CHECK CONSTRAINT [FK_ProgramSecurity_Program]
GO
/****** Object:  ForeignKey [FK_ProgramSecurity_User]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProgramSecurity_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProgramUser]'))
ALTER TABLE [dbo].[ProgramUser]  WITH CHECK ADD  CONSTRAINT [FK_ProgramSecurity_User] FOREIGN KEY([UserGUID])
REFERENCES [dbo].[User] ([UserGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProgramSecurity_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProgramUser]'))
ALTER TABLE [dbo].[ProgramUser] CHECK CONSTRAINT [FK_ProgramSecurity_User]
GO
/****** Object:  ForeignKey [FK_QuestionAnswer_PageQuestion]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_PageQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuestionAnswer_PageQuestion] FOREIGN KEY([PageQuestionGUID])
REFERENCES [dbo].[PageQuestion] ([PageQuestionGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_PageQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_PageQuestion]
GO
/****** Object:  ForeignKey [FK_QuestionAnswer_PageQuestionItem]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_PageQuestionItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuestionAnswer_PageQuestionItem] FOREIGN KEY([PageQuestionItemGUID])
REFERENCES [dbo].[PageQuestionItem] ([PageQuestionItemGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_PageQuestionItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_PageQuestionItem]
GO
/****** Object:  ForeignKey [FK_QuestionAnswer_User]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuestionAnswer_User] FOREIGN KEY([UserGUID])
REFERENCES [dbo].[User] ([UserGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuestionAnswer_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuestionAnswer]'))
ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_User]
GO
/****** Object:  ForeignKey [FK_Session_Program]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Session_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Session_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Program]
GO
/****** Object:  ForeignKey [FK_SessionContent_PageSequence]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SessionContent_PageSequence]') AND parent_object_id = OBJECT_ID(N'[dbo].[SessionContent]'))
ALTER TABLE [dbo].[SessionContent]  WITH CHECK ADD  CONSTRAINT [FK_SessionContent_PageSequence] FOREIGN KEY([PageSequenceGUID])
REFERENCES [dbo].[PageSequence] ([PageSequenceGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SessionContent_PageSequence]') AND parent_object_id = OBJECT_ID(N'[dbo].[SessionContent]'))
ALTER TABLE [dbo].[SessionContent] CHECK CONSTRAINT [FK_SessionContent_PageSequence]
GO
/****** Object:  ForeignKey [FK_SessionContent_Session]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SessionContent_Session]') AND parent_object_id = OBJECT_ID(N'[dbo].[SessionContent]'))
ALTER TABLE [dbo].[SessionContent]  WITH CHECK ADD  CONSTRAINT [FK_SessionContent_Session] FOREIGN KEY([SessionGUID])
REFERENCES [dbo].[Session] ([SessionGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SessionContent_Session]') AND parent_object_id = OBJECT_ID(N'[dbo].[SessionContent]'))
ALTER TABLE [dbo].[SessionContent] CHECK CONSTRAINT [FK_SessionContent_Session]
GO
/****** Object:  ForeignKey [FK_SpecificIntervent_Intervent]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SpecificIntervent_Intervent]') AND parent_object_id = OBJECT_ID(N'[dbo].[SpecificIntervent]'))
ALTER TABLE [dbo].[SpecificIntervent]  WITH CHECK ADD  CONSTRAINT [FK_SpecificIntervent_Intervent] FOREIGN KEY([InterventGUID])
REFERENCES [dbo].[Intervent] ([InterventGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SpecificIntervent_Intervent]') AND parent_object_id = OBJECT_ID(N'[dbo].[SpecificIntervent]'))
ALTER TABLE [dbo].[SpecificIntervent] CHECK CONSTRAINT [FK_SpecificIntervent_Intervent]
GO
/****** Object:  ForeignKey [FK_UserPerformance_Session]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPerformance_Session]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPerformance]'))
ALTER TABLE [dbo].[UserPerformance]  WITH CHECK ADD  CONSTRAINT [FK_UserPerformance_Session] FOREIGN KEY([SessionGUID])
REFERENCES [dbo].[Session] ([SessionGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPerformance_Session]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPerformance]'))
ALTER TABLE [dbo].[UserPerformance] CHECK CONSTRAINT [FK_UserPerformance_Session]
GO
/****** Object:  ForeignKey [FK_UserPerformance_UserPlanning]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPerformance_UserPlanning]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPerformance]'))
ALTER TABLE [dbo].[UserPerformance]  WITH CHECK ADD  CONSTRAINT [FK_UserPerformance_UserPlanning] FOREIGN KEY([UserPlanningGUID])
REFERENCES [dbo].[UserPlanning] ([UserPlanningGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPerformance_UserPlanning]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPerformance]'))
ALTER TABLE [dbo].[UserPerformance] CHECK CONSTRAINT [FK_UserPerformance_UserPlanning]
GO
/****** Object:  ForeignKey [FK_UserPlanning_Program]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPlanning_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanning]'))
ALTER TABLE [dbo].[UserPlanning]  WITH CHECK ADD  CONSTRAINT [FK_UserPlanning_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPlanning_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanning]'))
ALTER TABLE [dbo].[UserPlanning] CHECK CONSTRAINT [FK_UserPlanning_Program]
GO
/****** Object:  ForeignKey [FK_UserPlanning_User]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPlanning_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanning]'))
ALTER TABLE [dbo].[UserPlanning]  WITH CHECK ADD  CONSTRAINT [FK_UserPlanning_User] FOREIGN KEY([UserGUID])
REFERENCES [dbo].[User] ([UserGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPlanning_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanning]'))
ALTER TABLE [dbo].[UserPlanning] CHECK CONSTRAINT [FK_UserPlanning_User]
GO
/****** Object:  ForeignKey [FK_Video_VideoCategory]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Video_VideoCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Video]'))
ALTER TABLE [dbo].[Video]  WITH CHECK ADD  CONSTRAINT [FK_Video_VideoCategory] FOREIGN KEY([VideoCategoryGUID])
REFERENCES [dbo].[VideoCategory] ([VideoCategoryGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Video_VideoCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Video]'))
ALTER TABLE [dbo].[Video] CHECK CONSTRAINT [FK_Video_VideoCategory]
GO
/****** Object:  ForeignKey [FK_VideoCategory_Program]    Script Date: 08/24/2009 15:54:42 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VideoCategory_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[VideoCategory]'))
ALTER TABLE [dbo].[VideoCategory]  WITH CHECK ADD  CONSTRAINT [FK_VideoCategory_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VideoCategory_Program]') AND parent_object_id = OBJECT_ID(N'[dbo].[VideoCategory]'))
ALTER TABLE [dbo].[VideoCategory] CHECK CONSTRAINT [FK_VideoCategory_Program]
GO
