GO
/****** Object:  User [changetech]    Script Date: 06/08/2010 16:06:18 ******/
CREATE USER [changetech] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[HasSubItem] [bit] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[UserTypeID] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[EmailTemplateType]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplateType](
	[EmailTemplateTypeGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_EmailTemplateType] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateTypeGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[FailEmail]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FailEmail](
	[EmailGuid] [uniqueidentifier] NOT NULL,
	[EmailTo] [nvarchar](100) NULL,
	[EmailSubject] [nvarchar](100) NULL,
	[EmailContext] [nvarchar](1000) NULL,
	[ExceptionContext] [nvarchar](1000) NULL,
 CONSTRAINT [PK_FailEmail] PRIMARY KEY CLUSTERED 
(
	[EmailGuid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[ActivityLog]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLog](
	[ActivityLogGuid] [uniqueidentifier] NOT NULL,
	[ActivityLogType] [int] NOT NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
	[ProgramGuid] [uniqueidentifier] NOT NULL,
	[SessionGuid] [uniqueidentifier] NULL,
	[PageSequenceGuid] [uniqueidentifier] NULL,
	[PageGuid] [uniqueidentifier] NULL,
	[ActivityDateTime] [datetime] NULL,
	[Message] [ntext] NULL,
	[Browser] [nvarchar](1000) NULL,
	[IP] [nvarchar](50) NULL,
	[From] [nvarchar](500) NULL,
 CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED 
(
	[ActivityLogGuid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[Language]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[LanguageGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanguageGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UQ_Language] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[Setting]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[SettingID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Value] [int] NULL,
 CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED 
(
	[SettingID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[UserMenuTemplate]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMenuTemplate](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Order] [int] NULL,
 CONSTRAINT [PK_UserMenuTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[ProgramStatus]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramStatus](
	[ProgramStatusGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_ProgramStatus] PRIMARY KEY CLUSTERED 
(
	[ProgramStatusGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UK_ProgramStatus] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[TipMessageType]    Script Date: 06/08/2010 16:06:19 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipMessageType](
	[TipMessageTypeGUID] [uniqueidentifier] NOT NULL,
	[TipMessageTypeName] [nvarchar](500) NOT NULL,
	[Explanation] [ntext] NULL,
 CONSTRAINT [PK_TipMessageType] PRIMARY KEY CLUSTERED 
(
	[TipMessageTypeGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO

CREATE TABLE [dbo].[SystemSetting](
	[Name] [nvarchar](250) NOT NULL,
	[Value] [ntext] NULL,
 CONSTRAINT [PK_SystemSetting] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  StoredProcedure [dbo].[SearchActivityLog]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchActivityLog]
	@Where [varchar](500),
	@StartNumber [varchar](100),
	@EndNumber [varchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
	Declare @SelectEnd varchar(1000)
	Declare @SelectStart varchar(1000)
	Declare @Select varchar(1000)
	Set @Select = ''
	Set @SelectStart = ''
	Set @SelectEnd = 'SELECT TOP '+@EndNumber+ ' * From [ActivityLog] '
	Set @SelectStart = 'SELECT TOP '+@StartNumber+ ' ActivityLogGuid From [ActivityLog] '
	If(@Where <> '')
		BEGIN
		Set @SelectEnd = @SelectEnd + @Where + ' And ActivityLogGuid NOT IN' 
		Set @SelectStart = @SelectStart + @Where + ' ORDER BY ActivityDateTime'
		END
	Set @Select = @SelectEnd + '('+@SelectStart +')'+' ORDER BY ActivityDateTime'
	Print(@Select)
	Exec(@Select)
	
END
GO
/****** Object:  StoredProcedure [dbo].[SearchActivityLogNumber]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchActivityLogNumber]
	@Where [varchar](500)
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
	Declare @Select varchar(1000)
	Set @Select = 'SELECT Count(*) From [ActivityLog]'
	If(@Where <> '')
		Set @Select = @Select + @Where 
	Print(@Select)
	Exec(@Select)
	
END
GO
/****** Object:  Table [dbo].[ChannelType]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChannelType](
	[ChannelTypeGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](1024) NULL,
 CONSTRAINT [PK_ChannelType] PRIMARY KEY CLUSTERED 
(
	[ChannelTypeGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UK_ChannelType] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserGUID] [uniqueidentifier] NOT NULL,
	[UserType] [int] NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[MobilePhone] [char](255) NOT NULL,
	[Gender] [nchar](10) NOT NULL,
	[LastLogon] [datetime] NOT NULL,
	[Security] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UQ_User] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PredictorCategory]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PredictorCategory](
	[PredictorCategoryGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[PrimaryThemeColor] [nvarchar](50) NULL,
	[SecondaryThemeColor] [nvarchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PredictorType] PRIMARY KEY CLUSTERED 
(
	[PredictorCategoryGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[ResourceCategory]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceCategory](
	[ResourceCategoryGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[LastAccessed] [datetime] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ResourceCategory] PRIMARY KEY CLUSTERED 
(
	[ResourceCategoryGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UQ_ResourceCategory] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageQuestion]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageQuestion](
	[PageQuestionGUID] [uniqueidentifier] NOT NULL,
	[PageGUID] [uniqueidentifier] NOT NULL,
	[QuestionGUID] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[IsRequired] [bit] NOT NULL,
	[AddtionalInformation] [nvarchar](50) NULL,
	[PageVariableGUID] [uniqueidentifier] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PageQuestion] PRIMARY KEY CLUSTERED 
(
	[PageQuestionGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[EmailTemplateGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NULL,
	[EmailTemplateTypeGUID] [uniqueidentifier] NULL,
	[Name] [nvarchar](50) NULL,
	[Subject] [nvarchar](300) NULL,
	[Body] [text] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[EmailTemplateTypeContent]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplateTypeContent](
	[EmailTemplateTypeContentGUID] [uniqueidentifier] NOT NULL,
	[EmailTemplateTypeGUID] [uniqueidentifier] NULL,
	[HtmlContent] [text] NULL,
 CONSTRAINT [PK_EmailTemplateTypeContent] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateTypeContentGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[DeleteApplication]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeleteApplication](
	[DeleteApplicationGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Applicant] [uniqueidentifier] NOT NULL,
	[Assignee] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_DeleteApplication] PRIMARY KEY CLUSTERED 
(
	[DeleteApplicationGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[QuestionAnswer]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionAnswer](
	[QuestionAnswerGUID] [uniqueidentifier] NOT NULL,
	[UserGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[SessionContentGUID] [uniqueidentifier] NOT NULL,
	[PageQuestionGUID] [uniqueidentifier] NULL,
	[PageGUID] [uniqueidentifier] NULL,
	[LastUpdated] [datetime] NULL,
 CONSTRAINT [PK_QuestionAnswer] PRIMARY KEY CLUSTERED 
(
	[QuestionAnswerGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[UserPageVariable]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPageVariable](
	[UserPageVariableGUID] [uniqueidentifier] NOT NULL,
	[UserGUID] [uniqueidentifier] NOT NULL,
	[PageVariableGUID] [uniqueidentifier] NOT NULL,
	[QuestionAnswerGUID] [uniqueidentifier] NULL,
	[Value] [nvarchar](500) NULL,
	[LastUpdated] [datetime] NULL,
 CONSTRAINT [PK_UserPageVariable] PRIMARY KEY CLUSTERED 
(
	[UserPageVariableGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[UserPageVariablePerDay]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPageVariablePerDay](
	[UserPageVariablePerDayGUID] [uniqueidentifier] NOT NULL,
	[UserGUID] [uniqueidentifier] NOT NULL,
	[PageVariableGUID] [uniqueidentifier] NOT NULL,
	[SessionGUID] [uniqueidentifier] NOT NULL,
	[QuestionAnswerGUID] [uniqueidentifier] NULL,
	[Value] [nvarchar](500) NULL,
	[LastUpdated] [datetime] NULL,
 CONSTRAINT [PK_UserPageVariablePerDay_1] PRIMARY KEY CLUSTERED 
(
	[UserPageVariablePerDayGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[Program]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program](
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[StatusGUID] [uniqueidentifier] NOT NULL,
	[DefaultLanguageGUID] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[ProgramLogoGUID] [uniqueidentifier] NULL,
	[ParentProgramGUID] [uniqueidentifier] NULL,
	[ProjectManager] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
	[GeneralColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED 
(
	[ProgramGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[ProgramUser]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramUser](
	[ProgramUserGUID] [uniqueidentifier] NOT NULL,
	[UserGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Security] [int] NOT NULL,
	[StartDate] [smalldatetime] NULL,
	[MailTime] [int] NULL,
	[LastFinishDate] [smalldatetime] NULL,
	[Day] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[RegisteredIP] [nvarchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastSendEmailTime] [datetime] NULL,
 CONSTRAINT [PK_ProgramUser] PRIMARY KEY CLUSTERED 
(
	[ProgramUserGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[SpecialString]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialString](
	[Name] [nvarchar](250) NOT NULL,
	[LanguageGUID] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](50) NULL,
 CONSTRAINT [PK_SpecialString_1] PRIMARY KEY CLUSTERED 
(
	[Name] ASC,
	[LanguageGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[TipMessage]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipMessage](
	[TipMessageGUID] [uniqueidentifier] NOT NULL,
	[LanguageGUID] [uniqueidentifier] NOT NULL,
	[TipMessageTypeGUID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Message] [ntext] NULL,
	[BackButtonName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TipMessage] PRIMARY KEY CLUSTERED 
(
	[TipMessageGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[ProgramLanguage]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramLanguage](
	[ProgramLanguageGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[LanguageGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProgramLanguage] PRIMARY KEY CLUSTERED 
(
	[ProgramLanguageGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageTemplate]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageTemplate](
	[PageTemplateGUID] [uniqueidentifier] NOT NULL,
	[ChannelTypeGUID] [uniqueidentifier] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PageTemplate] PRIMARY KEY CLUSTERED 
(
	[PageTemplateGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UQ_PageTemplate] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[InterventCategory]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterventCategory](
	[InterventCategoryGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[PredictorGUID] [uniqueidentifier] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_InterventCategory] PRIMARY KEY CLUSTERED 
(
	[InterventCategoryGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PredictorMaterial]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PredictorMaterial](
	[PredictorReferenceGUID] [uniqueidentifier] NOT NULL,
	[PredictorGUID] [uniqueidentifier] NOT NULL,
	[URL] [nvarchar](1000) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PredictorReference] PRIMARY KEY CLUSTERED 
(
	[PredictorReferenceGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageMedia]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageMedia](
	[PageGUID] [uniqueidentifier] NOT NULL,
	[MediaGUID] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PageMedia_1] PRIMARY KEY CLUSTERED 
(
	[PageGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[QuestionAnswerValue]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionAnswerValue](
	[QuestionAnswerValueGuid] [uniqueidentifier] NOT NULL,
	[QuestionAnswerGUID] [uniqueidentifier] NOT NULL,
	[PageQuestionItemGUID] [uniqueidentifier] NULL,
	[ResourceGUID] [uniqueidentifier] NULL,
	[UserInput] [ntext] NULL,
 CONSTRAINT [PK_QuestionAnswerValue] PRIMARY KEY CLUSTERED 
(
	[QuestionAnswerValueGuid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[Preferences]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preferences](
	[PreferencesGUID] [uniqueidentifier] NOT NULL,
	[PageGUID] [uniqueidentifier] NOT NULL,
	[ImageGUID] [uniqueidentifier] NOT NULL,
	[VariableGUID] [uniqueidentifier] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[AnswerText] [nvarchar](200) NULL,
	[ButtonName] [nvarchar](200) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Preferences_1] PRIMARY KEY CLUSTERED 
(
	[PreferencesGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageContent]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageContent](
	[PageGUID] [uniqueidentifier] NOT NULL,
	[Heading] [ntext] NOT NULL,
	[Body] [ntext] NULL,
	[FooterText] [ntext] NULL,
	[PresenterImageGUID] [uniqueidentifier] NULL,
	[PresenterImagePosition] [nvarchar](50) NULL,
	[BackgroundImageGUID] [uniqueidentifier] NULL,
	[PrimaryButtonCaption] [nvarchar](80) NULL,
	[PrimaryButtonActionParameter] [nvarchar](50) NULL,
	[AfterShowExpression] [ntext] NULL,
	[BeforeShowExpression] [ntext] NULL,
	[BeforeShowExpressionGUID] [uniqueidentifier] NULL,
	[AfterShowExpressionGUID] [uniqueidentifier] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
	[PresenterMode] [nvarchar](50) NULL,
 CONSTRAINT [PK_PageContent_1] PRIMARY KEY CLUSTERED 
(
	[PageGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[PageQuestionItem]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageQuestionItem](
	[PageQuestionItemGUID] [uniqueidentifier] NOT NULL,
	[PageQuestionGUID] [uniqueidentifier] NOT NULL,
	[Order] [int] NULL,
	[Score] [int] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PageQuestionItem] PRIMARY KEY CLUSTERED 
(
	[PageQuestionItemGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [IX_PageQuestionItem] UNIQUE NONCLUSTERED 
(
	[PageQuestionItemGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageQuestionContent]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageQuestionContent](
	[PageQuestionGUID] [uniqueidentifier] NOT NULL,
	[Caption] [nvarchar](1024) NOT NULL,
	[DisableCheckBox] [nvarchar](250) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PageQuestionContent_1] PRIMARY KEY CLUSTERED 
(
	[PageQuestionGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[Graph]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Graph](
	[GraphGUID] [uniqueidentifier] NOT NULL,
	[PageGUID] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Caption] [nvarchar](50) NULL,
	[ScoreRange] [nvarchar](50) NOT NULL,
	[BadScoreRange] [nvarchar](50) NULL,
	[MediumRange] [nvarchar](50) NULL,
	[GoodScoreRange] [nvarchar](50) NULL,
	[TimeRange] [nvarchar](50) NOT NULL,
	[TimeUnit] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Graph] PRIMARY KEY CLUSTERED 
(
	[GraphGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[ProgramSchedule]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramSchedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProgramGUID] [uniqueidentifier] NULL,
	[Week] [int] NULL,
	[WeekDay] [int] NULL,
 CONSTRAINT [PK_ProgramSchedule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageVariable]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageVariable](
	[PageVariableGUID] [uniqueidentifier] NOT NULL,
	[PageVariableGroupGUID] [uniqueidentifier] NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[PageVariableType] [nvarchar](50) NOT NULL,
	[ValueType] [nvarchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PageVariable] PRIMARY KEY CLUSTERED 
(
	[PageVariableGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UK_PageVariable] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[ProgramGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[ProgramRoom]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramRoom](
	[ProgramRoomGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[PrimaryThemeColor] [nvarchar](50) NULL,
	[SecondaryThemeColor] [nvarchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProgramRoom] PRIMARY KEY CLUSTERED 
(
	[ProgramRoomGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UK_ProgramRoom] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[ProgramGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageVariableGroup]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageVariableGroup](
	[PageVariableGroupGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatdBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PageVariableGroup] PRIMARY KEY CLUSTERED 
(
	[PageVariableGroupGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UK_PageVariableGroup] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[ProgramGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[AccessoryTemplate]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessoryTemplate](
	[AccessoryTemplateGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Heading] [nvarchar](500) NULL,
	[Text] [nvarchar](max) NULL,
	[Order] [int] NULL,
	[UserNameText] [nvarchar](100) NULL,
	[PasswordText] [nvarchar](100) NULL,
	[PrimaryButtonText] [nvarchar](200) NULL,
	[SecondaryButtonText] [nvarchar](200) NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AccessoryTemplate] PRIMARY KEY CLUSTERED 
(
	[AccessoryTemplateGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[HelpItem]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HelpItem](
	[HelpItemGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Order] [int] NULL,
	[Question] [ntext] NOT NULL,
	[Answer] [ntext] NOT NULL,
 CONSTRAINT [PK_HelpItem] PRIMARY KEY CLUSTERED 
(
	[HelpItemGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[UserMenu]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMenu](
	[MenuItemGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[MenuText] [nvarchar](250) NULL,
	[MenuFormTitle] [nvarchar](1024) NOT NULL,
	[MenuFormText] [ntext] NULL,
	[MenuFormBackButtonName] [nvarchar](250) NULL,
	[MenuFormSubmitButtonName] [nvarchar](250) NULL,
	[Order] [int] NULL,
 CONSTRAINT [PK_UserMenu] PRIMARY KEY CLUSTERED 
(
	[MenuItemGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[Session]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Day] [int] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[ExpressionGroup]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpressionGroup](
	[ExpressionGroupGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ExpressionGroup] PRIMARY KEY CLUSTERED 
(
	[ExpressionGroupGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF),
 CONSTRAINT [UK_ExpressionGroup] UNIQUE NONCLUSTERED 
(
	[ExpressionGroupGUID] ASC,
	[ProgramGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[LayoutSetting]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LayoutSetting](
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[SettingXML] [nvarchar](max) NULL,
 CONSTRAINT [PK_LayoutSetting] PRIMARY KEY CLUSTERED 
(
	[ProgramGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[Page]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[PageGUID] [uniqueidentifier] NOT NULL,
	[PageSequenceGUID] [uniqueidentifier] NOT NULL,
	[PageOrderNo] [int] NOT NULL,
	[PageTemplateGUID] [uniqueidentifier] NOT NULL,
	[Wait] [nvarchar](50) NULL,
	[PageVariableGUID] [uniqueidentifier] NULL,
	[MaxPreferences] [int] NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[PageGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageQuestionItemContent]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageQuestionItemContent](
	[PageQuestionItemGUID] [uniqueidentifier] NOT NULL,
	[Item] [nvarchar](1024) NOT NULL,
	[Feedback] [nvarchar](1024) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PageQuestionItemContent_1] PRIMARY KEY CLUSTERED 
(
	[PageQuestionItemGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[PageSequence]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageSequence](
	[PageSequenceGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[InterventGUID] [uniqueidentifier] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Sequence] PRIMARY KEY CLUSTERED 
(
	[PageSequenceGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[Intervent]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Intervent](
	[InterventGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[InterventCategoryGUID] [uniqueidentifier] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Intervent] PRIMARY KEY CLUSTERED 
(
	[InterventGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[GraphContent]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GraphContent](
	[GraphGUID] [uniqueidentifier] NOT NULL,
	[Caption] [nvarchar](200) NULL,
	[TimeUnit] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_GraphContent_1] PRIMARY KEY CLUSTERED 
(
	[GraphGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[GraphItem]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GraphItem](
	[GraphItemGUID] [uniqueidentifier] NOT NULL,
	[GraphGUID] [uniqueidentifier] NOT NULL,
	[DataItemExpression] [ntext] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Color] [nvarchar](50) NOT NULL,
	[PointType] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_GraphItem] PRIMARY KEY CLUSTERED 
(
	[GraphItemGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[GraphItemContent]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GraphItemContent](
	[GraphItemGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_GraphItemContent_1] PRIMARY KEY CLUSTERED 
(
	[GraphItemGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[SessionContent]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionContent](
	[SessionContentGUID] [uniqueidentifier] NOT NULL,
	[SessionGUID] [uniqueidentifier] NOT NULL,
	[PageSequenceGUID] [uniqueidentifier] NULL,
	[ProgramRoomGUID] [uniqueidentifier] NULL,
	[PageSequenceOrderNo] [int] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_SessionContent] PRIMARY KEY CLUSTERED 
(
	[SessionContentGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[Expression]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expression](
	[ExpressionGUID] [uniqueidentifier] NOT NULL,
	[ExpressionGroupGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ExpressionText] [ntext] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Expression] PRIMARY KEY CLUSTERED 
(
	[ExpressionGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)  
GO
/****** Object:  Table [dbo].[Predictor]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Predictor](
	[PredictorGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[PredictorCategoryGUID] [uniqueidentifier] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Predictor] PRIMARY KEY CLUSTERED 
(
	[PredictorGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[ResourceGUID] [uniqueidentifier] NOT NULL,
	[ResourceCategoryGUID] [uniqueidentifier] NULL,
	[Name] [nvarchar](1024) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[FileExtension] [nvarchar](50) NULL,
	[NameOnServer] [nvarchar](255) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 
GO

/****** Object:  Table [dbo].[UserGroup]    Script Date: 09/03/2010 12:38:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserGroup](
	[GroupGUID] [uniqueidentifier] NOT NULL,
	[ProgramGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[GroupGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 

GO

ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO

ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_Program]
GO



/****** Object:  StoredProcedure [dbo].[GetTempPageSequencePreviewModelAsXML]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTempPageSequencePreviewModelAsXML]
	@languageGuid [uniqueidentifier],
	@sessionGuid [uniqueidentifier],
	@pageSequenceGuid [uniqueidentifier],
	@userGuid [uniqueidentifier],
	@programGuid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
    'Preview' "@Mode",
    @userGuid "@UserGUID",
    @programGuid "@ProgramGUID",
    'false' "@MissedDay",
	(
		SELECT TOP 1
		       [Session].SessionGUID "@GUID",
			   [Session].Name "@Name",
			   [Session].[Day] "@Day",
			   [Session].[Description] "@Description",
			   [Resource].NameOnServer "@Logo",
		(
			SELECT PageSequence.PageSequenceGUID "@GUID",
			       PredictorCategory.Name "@CategoryName",
				   PredictorCategory.[Description] "@CategoryDescription",
				   '1' "@Order",
				   '0x5cab3c' "@PrimaryThemeColor",
				   '0x5cab3c' "@SecondaryThemeColor",
			(
				SELECT Page.PageGUID "@GUID",
					   Page.PageOrderNo "@Order",
					   PageContent.Heading "@Title",
					   PageContent.Body "@Text",
					   PageTemplate.Name "@Type",
					   PageContent.PrimaryButtonCaption "@ButtonPrimaryName",
--					   PageContent.PrimaryButtonActionParameter "@ButtonPrimaryAction",
					    'Back' "@ButtonSecondaryName",
--					   0 "@ButtonSecondaryAction",
					   PresenterImageResource.NameOnServer "@PresenterImage",
					   PageContent.PresenterImagePosition "@PresenterImagePosition",
					   CASE WHEN PageContent.PresenterImagePosition IS NULL THEN NULL ELSE ISNULL(PageContent.PresenterMode,'Normal') END "@PresenterMode",
					   BackgroundImageResource.NameOnServer "@BackgroundImage",
					   REPLACE(Page.Wait,',','.') "@Interval",	
					   Page.MaxPreferences "@MaxPreferences",	
					   PageContent.BeforeShowExpression "@BeforeExpression",
					   PageContent.AfterShowExpression "@AfterExpression",
					   PageContent.FooterText "@FooterText",
					   PageVariable.Name "@ProgramVariable",
				(	
					SELECT
					(		
						SELECT PageQuestion.PageQuestionGUID "@GUID",
						       Question.Name "@Type",
							   Caption "@Caption",
							   PageVariable.Name "@ProgramVariable",
							   PageQuestion.IsRequired "@IsRequired",
							   PageQuestionContent.DisableCheckBox "@DisableCheckBox",
						(
							SELECT 
							(
								SELECT PageQuestionItem.PageQuestionItemGUID "@GUID",
								       Item "@Item",
									   Feedback "@Feedback",
									   Score "@Score"
								FROM PageQuestionItem LEFT JOIN PageQuestionItemContent
								ON PageQuestionItem.PageQuestionItemGUID = PageQuestionItemContent.PageQuestionItemGUID 
								--AND	PageQuestionItemContent.LanguageGUID = @languageGuid
								WHERE PageQuestion.PageGUID = Page.PageGUID AND
									  PageQuestion.PageQuestionGUID = PageQuestionItem.PageQuestionGUID
								ORDER BY PageQuestionItem.[Order] ASC
								FOR XML PATH('Item'), Type							
							)							
							FOR XML PATH('Items'), Type	
						)	
						FROM Question,
						PageQuestion LEFT JOIN PageQuestionContent
						ON PageQuestion.PageQuestionGUID = PageQuestionContent.PageQuestionGUID 
						--AND PageQuestionContent.LanguageGUID = @languageGuid 
						LEFT JOIN PageVariable ON PageQuestion.PageVariableGUID = PageVariable.PageVariableGUID
						WHERE PageQuestion.PageGUID = Page.PageGUID AND
						Question.QuestionGUID = PageQuestion.QuestionGUID						
						ORDER BY PageQuestion.[Order] ASC		
						FOR XML PATH('Question'), Type
					)
					FOR XML PATH('Questions'), Type
				),
				(
					SELECT PageMedia.[Type] "@Type",
					[Resource].NameOnServer "@Media"
					FROM PageMedia, [Resource]
					WHERE PageMedia.PageGUID = Page.PageGUID AND
					--PageMedia.LanguageGUID = @languageGuid AND
					PageMedia.MediaGUID = [Resource].ResourceGUID AND
					(PageMedia.IsDeleted = 'False' OR PageMedia.IsDeleted IS NULL)					
					FOR XML PATH('Media'), Type
				),
				(
					SELECT
					(
						SELECT [Resource].NameOnServer "@Image",
							   Preferences.Name "@Name",
							   Preferences.[Description] "@Description",
							   Preferences.AnswerText "@Answer",
							   Preferences.ButtonName "@ButtonName",
							   Preferences.PreferencesGUID "@GUID",
							   PageVariable.Name "@ProgramVariable"							       
						FROM Preferences LEFT JOIN [Resource] ON Preferences.ImageGUID = [Resource].ResourceGUID
						LEFT JOIN PageVariable ON Preferences.VariableGUID = PageVariable.PageVariableGUID
						WHERE Preferences.PageGUID = Page.PageGUID AND
							--Preferences.LanguageGUID = @languageGuid AND
							(Preferences.IsDeleted IS NULL OR Preferences.IsDeleted = 'False')
						FOR XML PATH ('Preference'), TYPE
					)
					FOR XML PATH('Preferences'), TYPE
				),
				(
					SELECT
					(
						SELECT Graph.GraphGUID "@GUID",
							   Graph.[Type] "@Type",
							   GraphContent.Caption "@Caption",
							   Graph.ScoreRange "@ScoreRange",
							   Graph.BadScoreRange "@BadScoreRange",
							   Graph.MediumRange "@MediumScoreRange",
							   Graph.GoodScoreRange "@GoodScoreRange",
							   Graph.TimeRange "@TimeRange",
							   Graph.TimeUnit "@TimeUnit",
							   'Startpunkt' "@TimeBaselineUnit",
							  (
								 SELECT 
								 (
									SELECT 
										GraphItemContent.Name "@Name",
										GraphItem.DataItemExpression "@Expression",
										'' "@Values",
										GraphItem.Color "@Color",
										GraphItem.PointType "@PointType"
									FROM GraphItem
									LEFT JOIN GraphItemContent 
									ON GraphItem.GraphItemGUID = GraphItemContent.GraphItemGUID
									--AND GraphItemContent.LanguageGUID = @languageGuid
									WHERE GraphItem.GraphGUID = Graph.GraphGUID AND
									(GraphItem.IsDeleted = 'False' OR GraphItem.IsDeleted IS NULL)
									FOR XML PATH('Item'), TYPE
								 )
								 FOR XML PATH('Items'), TYPE								 
							  )						
						FROM Graph 
						LEFT JOIN GraphContent ON Graph.GraphGUID = GraphContent.GraphGUID 
						--AND GraphContent.LanguageGUID = @languageGuid
						WHERE Graph.PageGUID = Page.PageGUID
						FOR XML PATH('Graph'), TYPE
					)
					FOR XML PATH('Graphs'),TYPE
				)
				FROM Page LEFT JOIN PageVariable On Page.PageVariableGUID = PageVariable.PageVariableGUID,
                 PageTemplate,
				 PageContent				
				 LEFT JOIN [Resource] PresenterImageResource ON PageContent.PresenterImageGUID = PresenterImageResource.ResourceGUID
				 LEFT JOIN [Resource] BackgroundImageResource ON PageContent.BackgroundImageGUID = BackgroundImageResource.ResourceGUID			
				WHERE --PageContent.LanguageGUID = @languageGuid AND
				      Page.PageTemplateGUID = PageTemplate.PageTemplateGUID AND
					  Page.PageGUID = PageContent.PageGUID AND
					  Page.PageSequenceGUID = PageSequence.PageSequenceGUID AND
--					  PageSequence.InterventGUID = Intervent.InterventGUID AND
--					  Intervent.InterventCategoryGUID = InterventCategory.InterventCategoryGUID AND
--					  InterventCategory.PredictorGUID = Predictor.PredictorGUID AND
--					  Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
					  (Page.IsDeleted IS NULL OR Page.IsDeleted = 'False')
					    
				ORDER BY Page.PageOrderNO	  
				FOR XML PATH('Page'), Type
			)
			FROM PageSequence,Intervent,InterventCategory,Predictor,PredictorCategory  CROSS JOIN [Session]
			WHERE PageSequence.PageSequenceGUID = @pageSequenceGuid AND
			    PageSequence.InterventGUID = Intervent.InterventGUID AND
				Intervent.InterventCategoryGUID = InterventCategory.InterventCategoryGUID AND
				InterventCategory.PredictorGUID = Predictor.PredictorGUID AND
				Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
				--SessionContent.PageSequenceGUID = @pageSequenceGuid AND
				[Session].SessionGUID = @sessionGuid AND
				(PageSequence.IsDeleted IS NULL OR PageSequence.IsDeleted = 'False')
				
			FOR XML PATH('PageSequence'), Type
		)
		FROM [Session], Program LEFT JOIN [Resource] ON [Resource].ResourceGUID = Program.ProgramLogoGUID
		WHERE [Session].SessionGUID = @sessionGuid AND
		[Session].ProgramGUID = Program.ProgramGUID
		
		FOR XML PATH('Session'), Type
	),	
	(
		SELECT
		(
			SELECT 'FirstName' "@Name",
				   [User].FirstName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'LastName' "@Name",
				   [User].LastName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'Email' "@Name",
				   [User].Email"@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'MobilePhone' "@Name",
				   [User].MobilePhone "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		)
		FOR XML PATH('GeneralVariables'), TYPE
	),	
	(
		SELECT 		
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				UserPageVariable.Value "@Value"
				FROM  UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID
				WHERE
				UserPageVariable.UserGUID = @userGuid AND
				UserPageVariable.Value IS NOT NULL
				FOR XML PATH('Variable'), TYPE
		),	 	 				
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				SUM(PageQuestionItem.Score) "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestionItem ON QuestionAnswerValue.PageQuestionItemGUID = PageQuestionItem.PageQuestionItemGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND Question.HasSubItem = 'TRUE'
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL	
				GROUP BY PageVariable.Name 
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Numeric' OR Question.Name = 'TimePicker')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Singleline' OR Question.Name = 'Multiline')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				INNER JOIN Page ON QuestionAnswer.PageGUID = Page.PageGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL	
				FOR XML PATH('Variable'), TYPE
		),
		(
			    SELECT PageVariable.Name "@Name",
				'Unknown' "@Type",
				'' "@Value"
				FROM PageVariable								
				WHERE PageVariable.ProgramGUID = @programGuid 
				AND PageVariable.PageVariableGUID NOT IN (
					SELECT DISTINCT(PageVariableGUID) 
					FROM UserPageVariable 
					WHERE UserGUID = @userGuid
				)			
				FOR XML PATH('Variable'), TYPE
		)
		FOR XML PATH('ProgramVariables'), TYPE
	),
	(
		SELECT
		(
			SELECT a.TipMessageTypeName "@Name",
			b.Title "@Title",
			b.Message "@Message",
			b.BackButtonName "@BackButtonName"
			FROM TipMessageType a LEFT JOIN TipMessage b ON a.TipMessageTypeGUID = b.TipMessageTypeGUID
			WHERE b.LanguageGUID = @languageGuid
			FOR XML PATH('Message'), TYPE
		)
		FOR XML PATH('TipMessages'), TYPE
	),
	(
		SELECT
		[Value] "@Title",
		(
			SELECT
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid AND LanguageGUID = @languageGuid
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		)
		FROM SpecialString WHERE LanguageGUID = @languageGuid AND [Name]='SettingMenu'
		FOR XML PATH('SettingMenu'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormBackButtonName "@BackButtonName",
		(
			SELECT
			Question "@Title",
			Answer "@Text"
			FROM HelpItem
			WHERE ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('HelpItem'), TYPE
		)
		FROM UserMenu
		WHERE [Name] = 'HelpFunction' AND 
		ProgramGUID = @programGuid 
		FOR XML PATH('Help'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'TipFriendFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('TipFriend'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'PauseProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ProgramStatus'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName",
		(
			SELECT
			'Email' "@Name",
			Email "@OldValue",
			'email' "@Format"
			FROM [User]
			WHERE UserGUID = @userGuid
			FOR XML PATH('Item'), TYPE
		)--,
--		(
--			SELECT
--			'FirstName' "@Name",
--			FirstName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'LastName' "@Name",
--			LastName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'MobilePhone' "@Name",
--			MobilePhone "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		)		
		FROM UserMenu
		WHERE [Name] = 'ProfileFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Profile'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'ExitProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ExitProgram'), TYPE
	)
	FOR XML PATH('XMLModel')
END
GO
/****** Object:  StoredProcedure [dbo].[GetSessionPreviewModelAsXML]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSessionPreviewModelAsXML]
	@languageGuid [uniqueidentifier],
	@sessionGuid [uniqueidentifier],
	@userGuid [uniqueidentifier],
	@programGuid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
    'Preview' "@Mode",
    @userGuid "@UserGUID",
    @programGuid "@ProgramGUID", 
	(
		SELECT [Session].SessionGUID "@GUID",
			   [Session].Name "@Name",
			   [Session].[Day] "@Day",
			   [Session].[Description] "@Description",
			   [Resource].NameOnServer "@Logo",
		(
			SELECT PageSequence.PageSequenceGUID "@GUID",
				   PredictorCategory.Name "@CategoryName",
				   PredictorCategory.[Description] "@CategoryDescription",
				   SessionContent.PageSequenceOrderNo "@Order",
				   ISNULL(ProgramRoom.PrimaryThemeColor, Program.GeneralColor) "@PrimaryThemeColor",
				   ProgramRoom.SecondaryThemeColor "@SecondaryThemeColor",
				   ProgramRoom.Name "@ProgramRoomName",
				   ProgramRoom.[Description] "@ProgramRoomDescription", 
			(
				SELECT Page.PageGUID "@GUID",
					   Page.PageOrderNo "@Order",
					   PageContent.Heading "@Title",
					   PageContent.Body "@Text",
					   PageTemplate.Name "@Type",
					   PageContent.PrimaryButtonCaption "@ButtonPrimaryName",
--					   PageContent.PrimaryButtonActionParameter "@ButtonPrimaryAction",
					   'Back' "@ButtonSecondaryName",
--					   0 "@ButtonSecondaryAction",
					   PresenterImageResource.NameOnServer "@PresenterImage",
					   PageContent.PresenterImagePosition "@PresenterImagePosition",
					   CASE WHEN PageContent.PresenterImagePosition IS NULL THEN NULL ELSE ISNULL(PageContent.PresenterMode,'Normal') END "@PresenterMode",
					   BackgroundImageResource.NameOnServer "@BackgroundImage",
					   REPLACE(Page.Wait,',','.') "@Interval",   	
					   Page.MaxPreferences "@MaxPreferences",
					   PageContent.BeforeShowExpression "@BeforeExpression",
					   PageContent.AfterShowExpression "@AfterExpression",	
					   PageContent.FooterText "@FooterText",   
					   PageVariable.Name "@ProgramVariable",
				(		
					SELECT
					(	
					    SELECT PageQuestion.PageQuestionGUID "@GUID",
						       Question.Name "@Type",
							   Caption "@Caption",
							   PageVariable.Name "@ProgramVariable",
							   PageQuestion.IsRequired "@IsRequired",
							   PageQuestionContent.DisableCheckBox "@DisableCheckBox",
						(
							SELECT 
							(
								SELECT PageQuestionItem.PageQuestionItemGUID "@GUID",
								       Item "@Item",
									   Feedback "@Feedback",
									   Score "@Score"
								FROM PageQuestionItem LEFT JOIN PageQuestionItemContent
								ON PageQuestionItem.PageQuestionItemGUID = PageQuestionItemContent.PageQuestionItemGUID
								WHERE PageQuestion.PageGUID = Page.PageGUID AND
									  PageQuestion.PageQuestionGUID = PageQuestionItem.PageQuestionGUID AND
									  (PageQuestionItem.IsDeleted IS NULL OR PageQuestionItem.IsDeleted = 'False')
								ORDER BY PageQuestionItem.[Order] ASC
								FOR XML PATH('Item'), Type							
							)							
							FOR XML PATH('Items'), Type	
						)	
						FROM Question,
						PageQuestion LEFT JOIN PageQuestionContent
						ON PageQuestion.PageQuestionGUID = PageQuestionContent.PageQuestionGUID
						LEFT JOIN PageVariable ON PageQuestion.PageVariableGUID = PageVariable.PageVariableGUID
						WHERE PageQuestion.PageGUID = Page.PageGUID AND
						Question.QuestionGUID = PageQuestion.QuestionGUID AND
						(PageQuestion.IsDeleted IS NULL OR PageQuestion.IsDeleted = 'False')
						ORDER BY PageQuestion.[Order] ASC		
						FOR XML PATH('Question'), Type
					)
					FOR XML PATH('Questions'), Type
				),
				(
					SELECT PageMedia.[Type] "@Type",
					[Resource].NameOnServer "@Media"
					FROM PageMedia, [Resource]
					WHERE PageMedia.MediaGUID = [Resource].ResourceGUID AND
					PageMedia.PageGUID = Page.PageGUID AND
					(PageMedia.IsDeleted = 'False' OR PageMedia.IsDeleted IS NULL)					
					FOR XML PATH('Media'), Type
				),
				(
					SELECT
					(
						SELECT [Resource].NameOnServer "@Image",
							   Preferences.Name "@Name",
							   Preferences.[Description] "@Description",
							   Preferences.AnswerText "@Answer",
							   Preferences.ButtonName "@ButtonName",
							   Preferences.PreferencesGUID "@GUID",
							   PageVariable.Name "@ProgramVariable"    
						FROM Preferences LEFT JOIN [Resource] ON Preferences.ImageGUID = [Resource].ResourceGUID
						LEFT JOIN PageVariable ON Preferences.VariableGUID = PageVariable.PageVariableGUID
						WHERE Preferences.PageGUID = Page.PageGUID AND
							(Preferences.IsDeleted IS NULL OR Preferences.IsDeleted = 'False')
						FOR XML PATH ('Preference'), TYPE
					)
					FOR XML PATH('Preferences'), TYPE
				),
				(
					SELECT
					(
						SELECT Graph.GraphGUID "@GUID",
							   Graph.[Type] "@Type",
							   GraphContent.Caption "@Caption",
							   Graph.ScoreRange "@ScoreRange",
							   Graph.BadScoreRange "@BadScoreRange",
							   Graph.MediumRange "@MediumScoreRange",
							   Graph.GoodScoreRange "@GoodScoreRange",
							   Graph.TimeRange "@TimeRange",
							   Graph.TimeUnit "@TimeUnit",
							   'Startpunkt' "@TimeBaselineUnit",
							  (
								 SELECT 
								 (
									SELECT 
										GraphItemContent.Name "@Name",
										GraphItem.DataItemExpression "@Expression",
										'' "@Values",
										GraphItem.Color "@Color",
										GraphItem.PointType "@PointType"
									FROM GraphItem
									LEFT JOIN GraphItemContent 
									ON GraphItem.GraphItemGUID = GraphItemContent.GraphItemGUID									
									WHERE GraphItem.GraphGUID = Graph.GraphGUID AND
									(GraphItem.IsDeleted = 'False' OR GraphItem.IsDeleted IS NULL)
									FOR XML PATH('Item'), TYPE
								 )
								 FOR XML PATH('Items'), TYPE								 
							  )						
						FROM Graph 
						LEFT JOIN GraphContent ON Graph.GraphGUID = GraphContent.GraphGUID 					
						WHERE Graph.PageGUID = Page.PageGUID
						FOR XML PATH('Graph'), TYPE
					)
					FOR XML PATH('Graphs'),TYPE
				)
				FROM Page LEFT JOIN PageVariable On Page.PageVariableGUID = PageVariable.PageVariableGUID,
                 PageTemplate,
				 PageContent				
				 LEFT JOIN [Resource] PresenterImageResource ON PageContent.PresenterImageGUID = PresenterImageResource.ResourceGUID
				 LEFT JOIN [Resource] BackgroundImageResource ON PageContent.BackgroundImageGUID = BackgroundImageResource.ResourceGUID			
				WHERE Page.PageGUID = PageContent.PageGUID AND
					  Page.PageSequenceGUID = PageSequence.PageSequenceGUID AND
--					  PageSequence.InterventGUID = Intervent.InterventGUID AND
--					  Intervent.InterventCategoryGUID = InterventCategory.InterventCategoryGUID AND
--					  InterventCategory.PredictorGUID = Predictor.PredictorGUID AND
--					  Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
					  Page.PageTemplateGUID = PageTemplate.PageTemplateGUID AND
					  (Page.IsDeleted IS NULL OR Page.IsDeleted = 'False')  
				ORDER BY Page.PageOrderNO	  
				FOR XML PATH('Page'), Type
			)
			FROM PageSequence, Intervent,InterventCategory,Predictor,PredictorCategory, 
			SessionContent LEFT JOIN ProgramRoom ON SessionContent.ProgramRoomGUID = ProgramRoom.ProgramRoomGUID,
			Program
			WHERE SessionContent.SessionGUID = @sessionGuid AND
				Program.ProgramGUID = @programGuid AND
			    SessionContent.PageSequenceGUID = PageSequence.PageSequenceGUID AND
				PageSequence.InterventGUID = Intervent.InterventGUID AND
				Intervent.InterventCategoryGUID = InterventCategory.InterventCategoryGUID AND
				InterventCategory.PredictorGUID = Predictor.PredictorGUID AND
				Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
				(PageSequence.IsDeleted IS NULL OR PageSequence.IsDeleted = 'False') AND
				(SessionContent.IsDeleted IS NULL OR SessionContent.IsDeleted = 'False')
			ORDER BY SessionContent.PageSequenceOrderNo	
			FOR XML PATH('PageSequence'), Type
		)
		FROM [Session], Program LEFT JOIN [Resource] ON Program.ProgramLogoGUID = [Resource].ResourceGUID
		WHERE [Session].SessionGUID = @sessionGuid AND
		[Session].ProgramGUID = Program.ProgramGUID
		FOR XML PATH('Session'), Type
	),	
	(
		SELECT
		(
			SELECT 'FirstName' "@Name",
				   [User].FirstName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'LastName' "@Name",
				   [User].LastName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'Email' "@Name",
				   [User].Email"@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'MobilePhone' "@Name",
				   [User].MobilePhone "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		)
		FOR XML PATH('GeneralVariables'), TYPE
	),	
	(
		SELECT 		
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				UserPageVariable.Value "@Value"
				FROM  UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID
				WHERE
				UserPageVariable.UserGUID = @userGuid AND
				UserPageVariable.Value IS NOT NULL
				FOR XML PATH('Variable'), TYPE
		),	 				
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				SUM(PageQuestionItem.Score) "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestionItem ON QuestionAnswerValue.PageQuestionItemGUID = PageQuestionItem.PageQuestionItemGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND Question.HasSubItem = 'TRUE'
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				GROUP BY PageVariable.Name 				
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Numeric' OR Question.Name = 'TimePicker')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Singleline' OR Question.Name = 'Multiline')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				INNER JOIN Page ON QuestionAnswer.PageGUID = Page.PageGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
			    SELECT PageVariable.Name "@Name",
				'Unknown' "@Type",
				'' "@Value"
				FROM PageVariable								
				WHERE PageVariable.ProgramGUID = @programGuid 
				AND PageVariable.PageVariableGUID NOT IN (
					SELECT DISTINCT(PageVariableGUID) 
					FROM UserPageVariable 
					WHERE UserGUID = @userGuid
				)			
				FOR XML PATH('Variable'), TYPE
		)
		FOR XML PATH('ProgramVariables'), TYPE
	),
	(
		SELECT
		(
			SELECT a.TipMessageTypeName "@Name",
			b.Title "@Title",
			b.Message "@Message",
			b.BackButtonName "@BackButtonName"
			FROM TipMessageType a LEFT JOIN TipMessage b ON a.TipMessageTypeGUID = b.TipMessageTypeGUID
			WHERE b.LanguageGUID = @languageGuid
			FOR XML PATH('Message'), TYPE
		)
		FOR XML PATH('TipMessages'), TYPE
	),
	(
		SELECT
		[Value] "@Title",
		(
			SELECT
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		)
		FROM SpecialString WHERE LanguageGUID = @languageGuid AND [Name]='SettingMenu'
		FOR XML PATH('SettingMenu'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormBackButtonName "@BackButtonName",
		(
			SELECT
			Question "@Title",
			Answer "@Text"
			FROM HelpItem
			WHERE ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('HelpItem'), TYPE
		)
		FROM UserMenu
		WHERE [Name] = 'HelpFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Help'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'TipFriendFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('TipFriend'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'PauseProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ProgramStatus'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName",
		(
			SELECT
			'Email' "@Name",
			Email "@OldValue",
			'email' "@Format"
			FROM [User]
			WHERE UserGUID = @userGuid
			FOR XML PATH('Item'), TYPE
		)--,
--		(
--			SELECT
--			'FirstName' "@Name",
--			FirstName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'LastName' "@Name",
--			LastName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'MobilePhone' "@Name",
--			MobilePhone "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		)		
		FROM UserMenu
		WHERE [Name] = 'ProfileFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Profile'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'ExitProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ExitProgram'), TYPE
	)
	FOR XML PATH('XMLModel')
END
GO
/****** Object:  StoredProcedure [dbo].[GetPagePreviewModelAsXML]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPagePreviewModelAsXML]
	@pageGuid [uniqueidentifier],
	@languageGuid [uniqueidentifier],
	@sessionGuid [uniqueidentifier],
	@pageSequenceGuid [uniqueidentifier],
	@userGuid [uniqueidentifier],
	@programGuid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
	 'Preview' "@Mode",
	 @userGuid "@UserGUID",
	 @programGuid "@ProgramGUID",
	 'false' "@MissedDay",
	(
		SELECT TOP 1
		       [Session].SessionGUID "@GUID",
			   [Session].Name "@Name",
			   [Session].[Day] "@Day",
			   [Session].[Description] "@Description",	
			   [Resource].NameOnServer "@Logo",		  
		(
			SELECT PageSequence.PageSequenceGUID "@GUID",
			       PredictorCategory.Name "@CategoryName",
				   PredictorCategory.[Description] "@CategoryDescription",
				   1 "@Order",
				   PredictorCategory.PrimaryThemeColor "@PrimaryThemeColor",
				   PredictorCategory.SecondaryThemeColor "@SecondaryThemeColor",
			(
				SELECT Page.PageGUID "@GUID",
					   Page.PageOrderNo "@Order",
					   PageContent.Heading "@Title",
					   PageContent.Body "@Text",
					   PageTemplate.Name "@Type",
					   PageContent.PrimaryButtonCaption "@ButtonPrimaryName",
--					   PageContent.PrimaryButtonActionParameter "@ButtonPrimaryAction",
					   'Back' "@ButtonSecondaryName",
--					   0 "@ButtonSecondaryAction",
					   PresenterImageResource.NameOnServer "@PresenterImage",
					   PageContent.PresenterImagePosition "@PresenterImagePosition",
					   CASE WHEN PageContent.PresenterImagePosition IS NULL THEN NULL ELSE ISNULL(PageContent.PresenterMode,'Normal') END "@PresenterMode",
					   BackgroundImageResource.NameOnServer "@BackgroundImage",
					   REPLACE(Page.Wait,',','.') "@Interval",			 
					   Page.MaxPreferences "@MaxPreferences",	
					   PageContent.BeforeShowExpression "@BeforeExpression",
					   PageContent.AfterShowExpression "@AfterExpression",  
					   PageContent.FooterText "@FooterText",	
					   PageVariable.Name "@ProgramVariable",	   				   
				(	
						SELECT
						(		
							SELECT PageQuestion.PageQuestionGUID "@GUID",
							       Question.Name "@Type",
								   PageQuestionContent.Caption "@Caption",
								   PageQuestion.IsRequired "@IsRequired",
								   PageQuestionContent.DisableCheckBox "@DisableCheckBox",
								   PageVariable.Name "@ProgramVariable",
							(
								SELECT 
								(
									SELECT PageQuestionItem.PageQuestionItemGUID "@GUID",
									       Item "@Item",
										   Feedback "@Feedback",
										   Score "@Score"
									FROM PageQuestionItem LEFT JOIN PageQuestionItemContent 
									ON PageQuestionItem.PageQuestionItemGUID = PageQuestionItemContent.PageQuestionItemGUID 
									--AND PageQuestionItemContent.LanguageGUID = @languageGuid
									WHERE 
									PageQuestion.PageQuestionGUID = PageQuestionItem.PageQuestionGUID AND
									(PageQuestionItem.IsDeleted = 'False'OR PageQuestionItem.IsDeleted IS NULL)
									ORDER BY PageQuestionItem.[Order] ASC
									FOR XML PATH('Item'), Type							
								)							
								FOR XML PATH('Items'), Type	
							)	
							FROM Question, 
							PageQuestion LEFT JOIN PageQuestionContent ON PageQuestion.PageQuestionGUID = PageQuestionContent.PageQuestionGUID
							--AND PageQuestionContent.LanguageGUID = @languageGuid
							LEFT JOIN PageVariable ON PageQuestion.PageVariableGUID = PageVariable.PageVariableGUID
							WHERE PageQuestion.PageGUID = Page.PageGUID AND
							PageQuestion.QuestionGUID = Question.QuestionGUID AND
							(PageQuestion.IsDeleted = 'False'OR PageQuestion.IsDeleted IS NULL)	
							ORDER BY PageQuestion.[Order]ASC					
							FOR XML PATH('Question'), Type
						)
						FOR XML PATH('Questions'), Type
				),
				(
					SELECT PageMedia.[Type] "@Type",
					[Resource].NameOnServer "@Media"
					FROM PageMedia, [Resource]
					WHERE PageMedia.PageGUID = @pageGuid AND
					--PageMedia.LanguageGUID = @languageGuid AND
					PageMedia.MediaGUID = [Resource].ResourceGUID AND
					(PageMedia.IsDeleted = 'False' OR PageMedia.IsDeleted IS NULL)				
					FOR XML PATH('Media'), Type
				),
				(
					SELECT
					(
						SELECT [Resource].NameOnServer "@Image",
							   Preferences.Name "@Name",
							   Preferences.[Description] "@Description",
							   Preferences.AnswerText "@Answer",
							   Preferences.ButtonName "@ButtonName",
							   Preferences.PreferencesGUID "@GUID",
							   PageVariable.Name "@ProgramVariable"					       
						FROM Preferences LEFT JOIN [Resource] ON Preferences.ImageGUID = [Resource].ResourceGUID
						LEFT JOIN PageVariable ON Preferences.VariableGUID = PageVariable.PageVariableGUID
						WHERE Preferences.PageGUID = Page.PageGUID AND
							--Preferences.LanguageGUID = @languageGuid AND
							(Preferences.IsDeleted = 'False'OR Preferences.IsDeleted IS NULL)
						FOR XML PATH ('Preference'), TYPE
					)
					FOR XML PATH('Preferences'), TYPE
				),
				(
					SELECT
					(
						SELECT Graph.GraphGUID "@GUID",
							   Graph.[Type] "@Type",
							   GraphContent.Caption "@Caption",
							   Graph.ScoreRange "@ScoreRange",
							   Graph.BadScoreRange "@BadScoreRange",
							   Graph.MediumRange "@MediumScoreRange",
							   Graph.GoodScoreRange "@GoodScoreRange",
							   Graph.TimeRange "@TimeRange",
							   Graph.TimeUnit "@TimeUnit",
							   'Startpunkt' "@TimeBaselineUnit",
							  (
								 SELECT 
								 (
									SELECT 
										GraphItemContent.Name "@Name",
										GraphItem.DataItemExpression "@Expression",
										'' "@Values",
										GraphItem.Color "@Color",
										GraphItem.PointType "@PointType"
									FROM GraphItem
									LEFT JOIN GraphItemContent 
									ON GraphItem.GraphItemGUID = GraphItemContent.GraphItemGUID
									--AND GraphItemContent.LanguageGUID = @languageGuid
									WHERE GraphItem.GraphGUID = Graph.GraphGUID AND
									(GraphItem.IsDeleted = 'False' OR GraphItem.IsDeleted IS NULL)
									FOR XML PATH('Item'), TYPE
								 )
								 FOR XML PATH('Items'), TYPE								 
							  )						
						FROM Graph 
						LEFT JOIN GraphContent ON Graph.GraphGUID = GraphContent.GraphGUID 
						--AND GraphContent.LanguageGUID = @languageGuid
						WHERE Graph.PageGUID = Page.PageGUID
						FOR XML PATH('Graph'), TYPE
					)
					FOR XML PATH('Graphs'),TYPE
				)
				FROM Page LEFT JOIN PageVariable ON Page.PageVariableGUID = PageVariable.PageVariableGUID,
                 PageTemplate,
				 PageContent
				 LEFT JOIN [Resource] PresenterImageResource ON PageContent.PresenterImageGUID = PresenterImageResource.ResourceGUID
				 LEFT JOIN [Resource] BackgroundImageResource ON PageContent.BackgroundImageGUID = BackgroundImageResource.ResourceGUID			
				WHERE Page.PageGUID = @pageGuid AND
					  --PageContent.LanguageGUID = @languageGuid AND
					  Page.PageGUID = PageContent.PageGUID AND
					  Page.PageSequenceGUID = PageSequence.PageSequenceGUID AND
					  PageSequence.InterventGUID = Intervent.InterventGUID AND
--					  Intervent.InterventCategoryGUID = InterventCategory.InterventCategoryGUID AND
--					  InterventCategory.PredictorGUID = Predictor.PredictorGUID AND
--					  Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
					  Page.PageTemplateGUID = PageTemplate.PageTemplateGUID  
					  
				FOR XML PATH('Page'), Type
			)
			FROM PageSequence,Intervent,InterventCategory,Predictor,PredictorCategory
			WHERE PageSequence.PageSequenceGUID = @pageSequenceGuid AND
			PageSequence.InterventGUID = Intervent.InterventGUID AND
				Intervent.InterventCategoryGUID = InterventCategory.InterventCategoryGUID AND
				InterventCategory.PredictorGUID = Predictor.PredictorGUID AND
				Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID
			FOR XML PATH('PageSequence'), Type
		)
		FROM [Session], Program LEFT JOIN [Resource] ON Program.ProgramLogoGUID = [Resource].ResourceGUID
		WHERE [Session].SessionGuid = @sessionGuid AND
		[Session].ProgramGUID = Program.ProgramGUID
		
		FOR XML PATH('Session'), Type
	),
	(
		SELECT
		(
			SELECT 'FirstName' "@Name",
				   [User].FirstName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'LastName' "@Name",
				   [User].LastName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'Email' "@Name",
				   [User].Email"@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'MobilePhone' "@Name",
				   [User].MobilePhone "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		)
		FOR XML PATH('GeneralVariables'), TYPE
	),	
	(
		SELECT 		
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				UserPageVariable.Value "@Value"
				FROM  UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid AND
				UserPageVariable.Value IS NOT NULL
				FOR XML PATH('Variable'), TYPE
		),		
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				SUM(PageQuestionItem.Score) "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestionItem ON QuestionAnswerValue.PageQuestionItemGUID = PageQuestionItem.PageQuestionItemGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND Question.HasSubItem = 'TRUE'
				AND PageVariable.ProgramGUID = @programGuid 
				AND UserPageVariable.Value IS NULL				
				GROUP BY PageVariable.Name 
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Numeric' OR Question.Name = 'TimePicker')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Singleline' OR Question.Name = 'Multiline')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				INNER JOIN Page ON QuestionAnswer.PageGUID = Page.PageGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
			    SELECT PageVariable.Name "@Name",
				'Unknown' "@Type",
				'' "@Value"
				FROM PageVariable								
				WHERE PageVariable.ProgramGUID = @programGuid 
				AND PageVariable.PageVariableGUID NOT IN (
					SELECT DISTINCT(PageVariableGUID) 
					FROM UserPageVariable 
					WHERE UserGUID = @userGuid
				)			
				FOR XML PATH('Variable'), TYPE
		)
		FOR XML PATH('ProgramVariables'), TYPE
	),
	(
		SELECT
		(
			SELECT a.TipMessageTypeName "@Name",
			b.Title "@Title",
			b.Message "@Message",
			b.BackButtonName "@BackButtonName"
			FROM TipMessageType a LEFT JOIN TipMessage b ON a.TipMessageTypeGUID = b.TipMessageTypeGUID
			WHERE b.LanguageGUID = @languageGuid
			FOR XML PATH('Message'), TYPE
		)
		FOR XML PATH('TipMessages'), TYPE
	),
	(
		SELECT
		[Value] "@Title",
		(
			SELECT
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		)
		FROM SpecialString WHERE LanguageGUID = @languageGuid AND [Name]='SettingMenu'
		FOR XML PATH('SettingMenu'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormBackButtonName "@BackButtonName",
		(
			SELECT
			Question "@Title",
			Answer "@Text"
			FROM HelpItem, Program
			WHERE HelpItem.ProgramGUID = @programGuid AND
			Program.ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('HelpItem'), TYPE
		)
		FROM UserMenu, Program
		WHERE UserMenu.[Name] = 'HelpFunction' AND 
		UserMenu.ProgramGUID = @programGuid AND 
		Program.ProgramGUID = @programGuid 
		FOR XML PATH('Help'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'TipFriendFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('TipFriend'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu, Program
		WHERE UserMenu.[Name] = 'PauseProgramFunction' AND 
		UserMenu.ProgramGUID = @programGuid AND
		Program.ProgramGUID = @programGuid
		
		FOR XML PATH('ProgramStatus'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName",
		(
			SELECT
			'Email' "@Name",
			Email "@OldValue",
			'email' "@Format"
			FROM [User]
			WHERE UserGUID = @userGuid
			FOR XML PATH('Item'), TYPE
		)--,
--		(
--			SELECT
--			'FirstName' "@Name",
--			FirstName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'LastName' "@Name",
--			LastName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'MobilePhone' "@Name",
--			MobilePhone "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		)		
		FROM UserMenu, Program
		WHERE UserMenu.[Name] = 'ProfileFunction' AND 
		UserMenu.ProgramGUID = @programGuid AND 
		Program.ProgramGUID = @programGuid 
		FOR XML PATH('Profile'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu, Program
		WHERE UserMenu.[Name] = 'ExitProgramFunction' AND 
		UserMenu.ProgramGUID = @programGuid AND
		Program.ProgramGUID = @programGuid 
		FOR XML PATH('ExitProgram'), TYPE
	)
	FOR XML PATH('XMLModel')
END
GO
/****** Object:  StoredProcedure [dbo].[GetSessionModelAsXML]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSessionModelAsXML]
	@userGuid [uniqueidentifier],
	@programGuid [uniqueidentifier],
	@languageGuid [uniqueidentifier],
	@day [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	 SELECT
    'Live' "@Mode",
    @userGuid "@UserGUID",
    @programGuid "@ProgramGUID",
    'false' "@MissedDay",
	(
		SELECT [Session].SessionGUID "@GUID",
			   [Session].Name "@Name",
			   [Session].[Day] "@Day",
			   [Session].[Description] "@Description",
			   [Resource].NameOnServer "@Logo",
		(
			SELECT PageSequence.PageSequenceGUID "@GUID",
				   PredictorCategory.Name "@CategoryName",
				   PredictorCategory.[Description] "@CategoryDescription",
				   SessionContent.PageSequenceOrderNo "@Order",
				   ISNULL(ProgramRoom.PrimaryThemeColor, Program.GeneralColor) "@PrimaryThemeColor",
				   ProgramRoom.SecondaryThemeColor "@SecondaryThemeColor",
				   ProgramRoom.Name "@ProgramRoomName",
				   ProgramRoom.[Description] "@ProgramRoomDescription", 
			(
				SELECT Page.PageGUID "@GUID",
					   Page.PageOrderNo "@Order",
					   PageContent.Heading "@Title",
					   PageContent.Body "@Text",
					   PageTemplate.Name "@Type",
					   PageContent.PrimaryButtonCaption "@ButtonPrimaryName",
--					   PageContent.PrimaryButtonActionParameter "@ButtonPrimaryAction",
					    'Back' "@ButtonSecondaryName",
--					   0 "@ButtonSecondaryAction",
					   PresenterImageResource.NameOnServer "@PresenterImage",
					   PageContent.PresenterImagePosition "@PresenterImagePosition",
					   CASE WHEN PageContent.PresenterImagePosition IS NULL THEN NULL ELSE ISNULL(PageContent.PresenterMode,'Normal') END "@PresenterMode",
					   BackgroundImageResource.NameOnServer "@BackgroundImage",
					   REPLACE(Page.Wait,',','.') "@Interval",   	
					   Page.MaxPreferences "@MaxPreferences",
					   PageContent.BeforeShowExpression "@BeforeExpression",
					   PageContent.AfterShowExpression "@AfterExpression",	
					   PageContent.FooterText "@FooterText",
					   PageVariable.Name "@ProgramVariable",
				(		
					SELECT
					(	
					    SELECT PageQuestion.PageQuestionGUID "@GUID",
						       Question.Name "@Type",
							   Caption "@Caption",
							   PageVariable.Name "@ProgramVariable",
							   PageQuestion.IsRequired "@IsRequired",
							   PageQuestionContent.DisableCheckBox "@DisableCheckBox",
						(
							SELECT 
							(
								SELECT PageQuestionItem.PageQuestionItemGUID "@GUID",
								       Item "@Item",
									   Feedback "@Feedback",
									   Score "@Score"
								FROM PageQuestionItem LEFT JOIN PageQuestionItemContent
								ON PageQuestionItem.PageQuestionItemGUID = PageQuestionItemContent.PageQuestionItemGUID 
								--AND	PageQuestionItemContent.LanguageGUID = @languageGuid
								WHERE PageQuestion.PageGUID = Page.PageGUID AND
									  PageQuestion.PageQuestionGUID = PageQuestionItem.PageQuestionGUID AND
									  (PageQuestionItem.IsDeleted IS NULL OR PageQuestionItem.IsDeleted = 'False')
								ORDER BY PageQuestionItem.[Order] ASC
								FOR XML PATH('Item'), Type							
							)							
							FOR XML PATH('Items'), Type	
						)	
						FROM Question,
						PageQuestion LEFT JOIN PageQuestionContent
						ON PageQuestion.PageQuestionGUID = PageQuestionContent.PageQuestionGUID 
						--AND	PageQuestionContent.LanguageGUID = @languageGuid 
						LEFT JOIN PageVariable ON PageQuestion.PageVariableGUID = PageVariable.PageVariableGUID
						WHERE PageQuestion.PageGUID = Page.PageGUID AND
						Question.QuestionGUID = PageQuestion.QuestionGUID AND
						(PageQuestion.IsDeleted IS NULL OR PageQuestion.IsDeleted = 'False')
						ORDER BY PageQuestion.[Order] ASC		
						FOR XML PATH('Question'), Type
					)
					FOR XML PATH('Questions'), Type
				),
				(
					SELECT PageMedia.[Type] "@Type",
					[Resource].NameOnServer "@Media"
					FROM PageMedia, [Resource]
					WHERE --PageMedia.LanguageGUID = @languageGuid AND
					PageMedia.MediaGUID = [Resource].ResourceGUID AND
					PageMedia.PageGUID = Page.PageGUID AND
					(PageMedia.IsDeleted = 'False' OR PageMedia.IsDeleted IS NULL)					
					FOR XML PATH('Media'), Type
				),
				(
					SELECT
					(
						SELECT [Resource].NameOnServer "@Image",
							   Preferences.Name "@Name",
							   Preferences.[Description] "@Description",
							   Preferences.AnswerText "@Answer",
							   Preferences.ButtonName "@ButtonName",
							   Preferences.PreferencesGUID "@GUID",
							   PageVariable.Name "@ProgramVariable"							       
						FROM Preferences LEFT JOIN [Resource] ON Preferences.ImageGUID = [Resource].ResourceGUID
						LEFT JOIN PageVariable ON Preferences.VariableGUID = PageVariable.PageVariableGUID
						WHERE Preferences.PageGUID = Page.PageGUID AND
							--Preferences.LanguageGUID = @languageGuid AND
							(Preferences.IsDeleted IS NULL OR Preferences.IsDeleted = 'False')
						FOR XML PATH ('Preference'), TYPE
					)
					FOR XML PATH('Preferences'), TYPE
				),
				(
					SELECT
					(
						SELECT Graph.GraphGUID "@GUID",
							   Graph.[Type] "@Type",
							   GraphContent.Caption "@Caption",
							   Graph.ScoreRange "@ScoreRange",
							   Graph.BadScoreRange "@BadScoreRange",
							   Graph.MediumRange "@MediumScoreRange",
							   Graph.GoodScoreRange "@GoodScoreRange",
							   Graph.TimeRange "@TimeRange",
							   Graph.TimeUnit "@TimeUnit",
							   'Startpunkt' "@TimeBaselineUnit",
							  (
								 SELECT 
								 (
									SELECT 
										GraphItemContent.Name "@Name",
										GraphItem.DataItemExpression "@Expression",
										'' "@Values",
										GraphItem.Color "@Color",
										GraphItem.PointType "@PointType"
									FROM GraphItem
									LEFT JOIN GraphItemContent 
									ON GraphItem.GraphItemGUID = GraphItemContent.GraphItemGUID
									--AND GraphItemContent.LanguageGUID = @languageGuid
									WHERE GraphItem.GraphGUID = Graph.GraphGUID AND
									(GraphItem.IsDeleted = 'False' OR GraphItem.IsDeleted IS NULL)
									FOR XML PATH('Item'), TYPE
								 )
								 FOR XML PATH('Items'), TYPE								 
							  )						
						FROM Graph 
						LEFT JOIN GraphContent ON Graph.GraphGUID = GraphContent.GraphGUID 
						--AND GraphContent.LanguageGUID = @languageGuid
						WHERE Graph.PageGUID = Page.PageGUID
						FOR XML PATH('Graph'), TYPE
					)
					FOR XML PATH('Graphs'),TYPE
				)
				FROM Page LEFT JOIN PageVariable On Page.PageVariableGUID = PageVariable.PageVariableGUID,
                 PageTemplate,
				 PageContent				
				 LEFT JOIN [Resource] PresenterImageResource ON PageContent.PresenterImageGUID = PresenterImageResource.ResourceGUID
				 LEFT JOIN [Resource] BackgroundImageResource ON PageContent.BackgroundImageGUID = BackgroundImageResource.ResourceGUID			
				WHERE --PageContent.LanguageGUID = @languageGuid AND
					  Page.PageGUID = PageContent.PageGUID AND
					  Page.PageSequenceGUID = PageSequence.PageSequenceGUID AND					  
					  Page.PageTemplateGUID = PageTemplate.PageTemplateGUID AND
					  (Page.IsDeleted IS NULL OR Page.IsDeleted = 'False')   
				ORDER BY Page.PageOrderNO	  
				FOR XML PATH('Page'), Type
			)
			FROM PageSequence, Intervent,InterventCategory,Predictor,PredictorCategory,
			SessionContent LEFT JOIN ProgramRoom ON SessionContent.ProgramRoomGUID = ProgramRoom.ProgramRoomGUID,
			Program
			WHERE SessionContent.SessionGUID = [Session].SessionGUID AND
				Program.ProgramGUID = @programGuid AND
			    SessionContent.PageSequenceGUID = PageSequence.PageSequenceGUID AND
				PageSequence.InterventGUID = Intervent.InterventGUID AND
				Intervent.InterventCategoryGUID = InterventCategory.InterventCategoryGUID AND
				InterventCategory.PredictorGUID = Predictor.PredictorGUID AND
				Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
				(PageSequence.IsDeleted IS NULL OR PageSequence.IsDeleted = 'False') AND
				(SessionContent.IsDeleted IS NULL OR SessionContent.IsDeleted = 'False')
			ORDER BY SessionContent.PageSequenceOrderNo	
			FOR XML PATH('PageSequence'), Type
		)
		FROM [Session], Program LEFT JOIN [Resource] ON Program.ProgramLogoGUID = [Resource].ResourceGUID
		WHERE [Day] = @day AND Program.ProgramGUID = @programGuid AND [Session].ProgramGUID = Program.ProgramGUID				
		FOR XML PATH('Session'), Type
	),
	(
		SELECT
		(
			SELECT 'FirstName' "@Name",
				   [User].FirstName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'LastName' "@Name",
				   [User].LastName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'Email' "@Name",
				   [User].Email"@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'MobilePhone' "@Name",
				   [User].MobilePhone "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		)
		FOR XML PATH('GeneralVariables'), TYPE
	),
	(
		SELECT 		
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				UserPageVariable.Value "@Value"
				FROM  UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID
				WHERE
				UserPageVariable.UserGUID = @userGuid AND
				UserPageVariable.Value IS NOT NULL
				FOR XML PATH('Variable'), TYPE
		),	
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				SUM(PageQuestionItem.Score) "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestionItem ON QuestionAnswerValue.PageQuestionItemGUID = PageQuestionItem.PageQuestionItemGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND Question.HasSubItem = 'TRUE'
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL	
				GROUP BY PageVariable.Name 
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Numeric' OR Question.Name = 'TimePicker')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Singleline' OR Question.Name = 'Multiline')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				INNER JOIN Page ON QuestionAnswer.PageGUID = Page.PageGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL	
				FOR XML PATH('Variable'), TYPE
		),
		(
			    SELECT PageVariable.Name "@Name",
				'Unknown' "@Type",
				'' "@Value"
				FROM PageVariable								
				WHERE PageVariable.ProgramGUID = @programGuid 
				AND PageVariable.PageVariableGUID NOT IN (
					SELECT DISTINCT(PageVariableGUID) 
					FROM UserPageVariable 
					WHERE UserGUID = @userGuid
				)			
				FOR XML PATH('Variable'), TYPE
		)
		FOR XML PATH('ProgramVariables'), TYPE
	),
	(
		SELECT
		(
			SELECT a.TipMessageTypeName "@Name",
			b.Title "@Title",
			b.Message "@Message",
			b.BackButtonName "@BackButtonName"
			FROM TipMessageType a LEFT JOIN TipMessage b ON a.TipMessageTypeGUID = b.TipMessageTypeGUID
			WHERE b.LanguageGUID = @languageGuid
			FOR XML PATH('Message'), TYPE
		)
		FOR XML PATH('TipMessages'), TYPE
	),
	(
		SELECT
		[Value] "@Title",
		(
			SELECT 
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid AND LanguageGUID = @languageGuid AND @day <> 0
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		),
		(
			SELECT TOP 1
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid AND LanguageGUID = @languageGuid AND @day = 0
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		)
		FROM SpecialString WHERE LanguageGUID = @languageGuid AND [Name]='SettingMenu'
		FOR XML PATH('SettingMenu'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormBackButtonName "@BackButtonName",
		(
			SELECT
			Question "@Title",
			Answer "@Text"
			FROM HelpItem
			WHERE ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('HelpItem'), TYPE
		)
		FROM UserMenu
		WHERE [Name] = 'HelpFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Help'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'TipFriendFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('TipFriend'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'PauseProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ProgramStatus'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName",
		(
			SELECT
			'Email' "@Name",
			Email "@OldValue",
			'email' "@Format"
			FROM [User]
			WHERE UserGUID = @userGuid
			FOR XML PATH('Item'), TYPE
		)--,
--		(
--			SELECT
--			'FirstName' "@Name",
--			FirstName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'LastName' "@Name",
--			LastName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'MobilePhone' "@Name",
--			MobilePhone "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		)		
		FROM UserMenu
		WHERE [Name] = 'ProfileFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Profile'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'ExitProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ExitProgram'), TYPE
	)
	FOR XML PATH('XMLModel')
END
GO
/****** Object:  StoredProcedure [dbo].[GetPageSequencePreviewModelAsXML]    Script Date: 06/08/2010 16:06:23 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPageSequencePreviewModelAsXML]
	@languageGuid [uniqueidentifier],
	@sessionGuid [uniqueidentifier],
	@pageSequenceGuid [uniqueidentifier],
	@userGuid [uniqueidentifier],
	@programGuid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT
    'Preview' "@Mode",
    @userGuid "@UserGUID",
    @programGuid "@ProgramGUID",
    'false' "@MissedDay",
	(
		SELECT TOP 1
		       [Session].SessionGUID "@GUID",
			   [Session].Name "@Name",
			   [Session].[Day] "@Day",
			   [Session].[Description] "@Description",
			   [Resource].NameOnServer "@Logo",
		(
			SELECT TOP 1
				   PageSequence.PageSequenceGUID "@GUID",
			       PredictorCategory.Name "@CategoryName",
				   PredictorCategory.[Description] "@CategoryDescription",
				   SessionContent.PageSequenceOrderNo "@Order",
				   ISNULL(ProgramRoom.PrimaryThemeColor, Program.GeneralColor) "@PrimaryThemeColor",
				   ProgramRoom.SecondaryThemeColor "@SecondaryThemeColor",
				   ProgramRoom.Name "@ProgramRoomName",
				   ProgramRoom.[Description] "@ProgramRoomDescription", 
			(
				SELECT Page.PageGUID "@GUID",
					   Page.PageOrderNo "@Order",
					   PageContent.Heading "@Title",
					   PageContent.Body "@Text",
					   PageTemplate.Name "@Type",
					   PageContent.PrimaryButtonCaption "@ButtonPrimaryName",
--					   PageContent.PrimaryButtonActionParameter "@ButtonPrimaryAction",
					    'Back' "@ButtonSecondaryName",
--					   0 "@ButtonSecondaryAction",
					   PresenterImageResource.NameOnServer "@PresenterImage",
					   PageContent.PresenterImagePosition "@PresenterImagePosition",
					   CASE WHEN PageContent.PresenterImagePosition IS NULL THEN NULL ELSE ISNULL(PageContent.PresenterMode,'Normal') END "@PresenterMode",
					   BackgroundImageResource.NameOnServer "@BackgroundImage",
					   REPLACE(Page.Wait,',','.') "@Interval",	
					   Page.MaxPreferences "@MaxPreferences",	
					   PageContent.BeforeShowExpression "@BeforeExpression",
					   PageContent.AfterShowExpression "@AfterExpression",	
					   PageContent.FooterText "@FooterText",
					   PageVariable.Name "@ProgramVariable",
				(	
					SELECT
					(		
						SELECT PageQuestion.PageQuestionGUID "@GUID",
						       Question.Name "@Type",
							   Caption "@Caption",
							   PageVariable.Name "@ProgramVariable",
							   PageQuestion.IsRequired "@IsRequired",
							   PageQuestionContent.DisableCheckBox "@DisableCheckBox",							   
						(
							SELECT 
							(
								SELECT PageQuestionItem.PageQuestionItemGUID "@GUID",
								       Item "@Item",
									   Feedback "@Feedback",
									   Score "@Score"
								FROM PageQuestionItem LEFT JOIN PageQuestionItemContent
								ON PageQuestionItem.PageQuestionItemGUID = PageQuestionItemContent.PageQuestionItemGUID 
								--AND	PageQuestionItemContent.LanguageGUID = @languageGuid
								WHERE PageQuestion.PageGUID = Page.PageGUID AND
									  PageQuestion.PageQuestionGUID = PageQuestionItem.PageQuestionGUID AND
									  (PageQuestionItem.IsDeleted IS NULL OR PageQuestionItem.IsDeleted = 'False')
								ORDER BY PageQuestionItem.[Order] ASC
								FOR XML PATH('Item'), Type							
							)							
							FOR XML PATH('Items'), Type	
						)	
						FROM Question,
						PageQuestion LEFT JOIN PageQuestionContent
						ON PageQuestion.PageQuestionGUID = PageQuestionContent.PageQuestionGUID 
						--AND	PageQuestionContent.LanguageGUID = @languageGuid 
						LEFT JOIN PageVariable ON PageQuestion.PageVariableGUID = PageVariable.PageVariableGUID
						WHERE PageQuestion.PageGUID = Page.PageGUID AND
						Question.QuestionGUID = PageQuestion.QuestionGUID AND
						(PageQuestion.IsDeleted IS NULL OR PageQuestion.IsDeleted = 'False')						
						ORDER BY PageQuestion.[Order] ASC		
						FOR XML PATH('Question'), Type
					)
					FOR XML PATH('Questions'), Type
				),
				(
					SELECT PageMedia.[Type] "@Type",
					[Resource].NameOnServer "@Media"
					FROM PageMedia, [Resource]
					WHERE PageMedia.PageGUID = Page.PageGUID AND
					--PageMedia.LanguageGUID = @languageGuid AND
					PageMedia.MediaGUID = [Resource].ResourceGUID AND
					(PageMedia.IsDeleted = 'False' OR PageMedia.IsDeleted IS NULL)										
					FOR XML PATH('Media'), Type
				),
				(
					SELECT
					(
						SELECT [Resource].NameOnServer "@Image",
							   Preferences.Name "@Name",
							   Preferences.[Description] "@Description",
							   Preferences.AnswerText "@Answer",
							   Preferences.ButtonName "@ButtonName",
							   Preferences.PreferencesGUID "@GUID",
							   PageVariable.Name "@ProgramVariable"					       
						FROM Preferences LEFT JOIN [Resource] ON Preferences.ImageGUID = [Resource].ResourceGUID
						LEFT JOIN PageVariable ON Preferences.VariableGUID = PageVariable.PageVariableGUID
						WHERE Preferences.PageGUID = Page.PageGUID AND
							--Preferences.LanguageGUID = @languageGuid AND
							(Preferences.IsDeleted IS NULL OR Preferences.IsDeleted = 'False')
						FOR XML PATH ('Preference'), TYPE
					)
					FOR XML PATH('Preferences'), TYPE
				),
				(
					SELECT
					(
						SELECT Graph.GraphGUID "@GUID",
							   Graph.[Type] "@Type",
							   GraphContent.Caption "@Caption",
							   Graph.ScoreRange "@ScoreRange",
							   Graph.BadScoreRange "@BadScoreRange",
							   Graph.MediumRange "@MediumScoreRange",
							   Graph.GoodScoreRange "@GoodScoreRange",
							   Graph.TimeRange "@TimeRange",
							   Graph.TimeUnit "@TimeUnit",
							   'Startpunkt' "@TimeBaselineUnit",
							  (
								 SELECT 
								 (
									SELECT 
										GraphItemContent.Name "@Name",
										GraphItem.DataItemExpression "@Expression",
										'' "@Values",
										GraphItem.Color "@Color",
										GraphItem.PointType "@PointType"
									FROM GraphItem
									LEFT JOIN GraphItemContent 
									ON GraphItem.GraphItemGUID = GraphItemContent.GraphItemGUID
									--AND GraphItemContent.LanguageGUID = @languageGuid
									WHERE GraphItem.GraphGUID = Graph.GraphGUID AND
									(GraphItem.IsDeleted = 'False' OR GraphItem.IsDeleted IS NULL)
									FOR XML PATH('Item'), TYPE
								 )
								 FOR XML PATH('Items'), TYPE								 
							  )						
						FROM Graph 
						LEFT JOIN GraphContent ON Graph.GraphGUID = GraphContent.GraphGUID 
						--AND GraphContent.LanguageGUID = @languageGuid
						WHERE Graph.PageGUID = Page.PageGUID
						FOR XML PATH('Graph'), TYPE
					)
					FOR XML PATH('Graphs'),TYPE
				)
				FROM Page LEFT JOIN PageVariable On Page.PageVariableGUID = PageVariable.PageVariableGUID,
                 PageTemplate,
				 PageContent				
				 LEFT JOIN [Resource] PresenterImageResource ON PageContent.PresenterImageGUID = PresenterImageResource.ResourceGUID
				 LEFT JOIN [Resource] BackgroundImageResource ON PageContent.BackgroundImageGUID = BackgroundImageResource.ResourceGUID			
				WHERE --PageContent.LanguageGUID = @languageGuid AND
				      Page.PageTemplateGUID = PageTemplate.PageTemplateGUID AND
					  Page.PageGUID = PageContent.PageGUID AND
					  Page.PageSequenceGUID = PageSequence.PageSequenceGUID AND
					  (Page.IsDeleted IS NULL OR Page.IsDeleted = 'False')
					    
				ORDER BY Page.PageOrderNO	  
				FOR XML PATH('Page'), Type
			)
			FROM PageSequence,Intervent,InterventCategory,Predictor,PredictorCategory, 
			SessionContent LEFT JOIN ProgramRoom ON SessionContent.ProgramRoomGUID = ProgramRoom.ProgramRoomGUID,
		    Program
			WHERE PageSequence.PageSequenceGUID = @pageSequenceGuid AND
			    Program.ProgramGUID = @programGuid AND
			    PageSequence.InterventGUID = Intervent.InterventGUID AND
				Intervent.InterventCategoryGUID = InterventCategory.InterventCategoryGUID AND
				InterventCategory.PredictorGUID = Predictor.PredictorGUID AND
				Predictor.PredictorCategoryGUID = PredictorCategory.PredictorCategoryGUID AND
				SessionContent.PageSequenceGUID = @pageSequenceGuid AND
				SessionContent.SessionGUID = @sessionGuid AND
				(PageSequence.IsDeleted IS NULL OR PageSequence.IsDeleted = 'False') AND
				(SessionContent.IsDeleted IS NULL OR SessionContent.IsDeleted = 'False')
				
			FOR XML PATH('PageSequence'), Type
		)
		FROM [Session], Program LEFT JOIN [Resource] ON [Resource].ResourceGUID = Program.ProgramLogoGUID
		WHERE [Session].SessionGUID = @sessionGuid AND
		[Session].ProgramGUID = Program.ProgramGUID
		
		FOR XML PATH('Session'), Type
	),
	(
		SELECT
		(
			SELECT 'FirstName' "@Name",
				   [User].FirstName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'LastName' "@Name",
				   [User].LastName "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'Email' "@Name",
				   [User].Email"@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		),		
		(
			SELECT 'MobilePhone' "@Name",
				   [User].MobilePhone "@Value",
				   'String' "@Type"
			FROM [User]
			WHERE [User].UserGUID = @userGuid
			FOR XML PATH('Variable'), Type
		)
		FOR XML PATH('GeneralVariables'), TYPE
	),	
	(
		SELECT 		
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				UserPageVariable.Value "@Value"
				FROM  UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID
				WHERE
				UserPageVariable.UserGUID = @userGuid AND
				UserPageVariable.Value IS NOT NULL
				FOR XML PATH('Variable'), TYPE
		),	 				
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				SUM(PageQuestionItem.Score) "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestionItem ON QuestionAnswerValue.PageQuestionItemGUID = PageQuestionItem.PageQuestionItemGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND Question.HasSubItem = 'TRUE'
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL	
				GROUP BY PageVariable.Name 
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'Numeric' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Numeric' OR Question.Name = 'TimePicker')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				LEFT JOIN PageQuestion ON QuestionAnswer.PageQuestionGUID = PageQuestion.PageQuestionGUID
				LEFT JOIN Question ON PageQuestion.QuestionGUID = Question.QuestionGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND (Question.Name = 'Singleline' OR Question.Name = 'Multiline')
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL
				FOR XML PATH('Variable'), TYPE
		),
		(
				SELECT PageVariable.Name "@Name",
				'String' "@Type",
				QuestionAnswerValue.UserInput "@Value" 
				FROM UserPageVariable LEFT JOIN PageVariable ON UserPageVariable.PageVariableGUID = PageVariable.PageVariableGUID				 
				LEFT JOIN QuestionAnswer ON UserPageVariable.QuestionAnswerGUID = QuestionAnswer.QuestionAnswerGUID
				LEFT JOIN QuestionAnswerValue ON QuestionAnswer.QuestionAnswerGUID = QuestionAnswerValue.QuestionAnswerGUID
				INNER JOIN Page ON QuestionAnswer.PageGUID = Page.PageGUID
				WHERE 
				UserPageVariable.UserGUID = @userGuid 
				AND QuestionAnswer.UserGUID = @userGuid
				AND PageVariable.ProgramGUID = @programGuid
				AND UserPageVariable.Value IS NULL	
				FOR XML PATH('Variable'), TYPE
		),
		(
			    SELECT PageVariable.Name "@Name",
				'Unknown' "@Type",
				'' "@Value"
				FROM PageVariable								
				WHERE PageVariable.ProgramGUID = @programGuid 
				AND PageVariable.PageVariableGUID NOT IN (
					SELECT DISTINCT(PageVariableGUID) 
					FROM UserPageVariable 
					WHERE UserGUID = @userGuid
				)			
				FOR XML PATH('Variable'), TYPE
		)
		FOR XML PATH('ProgramVariables'), TYPE
	),
	(
		SELECT
		(
			SELECT a.TipMessageTypeName "@Name",
			b.Title "@Title",
			b.Message "@Message",
			b.BackButtonName "@BackButtonName"
			FROM TipMessageType a LEFT JOIN TipMessage b ON a.TipMessageTypeGUID = b.TipMessageTypeGUID
			WHERE b.LanguageGUID = @languageGuid
			FOR XML PATH('Message'), TYPE
		)
		FOR XML PATH('TipMessages'), TYPE
	),
	(
		SELECT
		[Value] "@Title",
		(
			SELECT
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		)
		FROM SpecialString WHERE LanguageGUID = @languageGuid AND [Name]='SettingMenu'
		FOR XML PATH('SettingMenu'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormBackButtonName "@BackButtonName",
		(
			SELECT
			Question "@Title",
			Answer "@Text"
			FROM HelpItem
			WHERE ProgramGUID = @programGuid
			FOR XML PATH('HelpItem'), TYPE
		)
		FROM UserMenu
		WHERE [Name] = 'HelpFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Help'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'TipFriendFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('TipFriend'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'PauseProgramFunction' AND 
		ProgramGUID = @programGuid 
		FOR XML PATH('ProgramStatus'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName",
		(
			SELECT
			'Email' "@Name",
			Email "@OldValue",
			'email' "@Format"
			FROM [User]
			WHERE UserGUID = @userGuid
			FOR XML PATH('Item'), TYPE
		)--,
--		(
--			SELECT
--			'FirstName' "@Name",
--			FirstName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'LastName' "@Name",
--			LastName "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		),
--		(
--			SELECT
--			'MobilePhone' "@Name",
--			MobilePhone "@OldValue",
--			'string' "@Format"
--			FROM [User]
--			WHERE UserGUID = @userGuid
--			FOR XML PATH('Item'), TYPE
--		)		
		FROM UserMenu
		WHERE [Name] = 'ProfileFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Profile'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'ExitProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ExitProgram'), TYPE
	)
	FOR XML PATH('XMLModel')
END
GO
/****** Object:  View [dbo].[V_ActivityLogUser]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_ActivityLogUser]
AS
SELECT DISTINCT ActivityLog.UserGuid, ActivityLog.ProgramGuid, ActivityLog.SessionGuid, [User].Email, ProgramUser.Status, UserType.Name
FROM         ProgramUser INNER JOIN
                      ActivityLog ON ProgramUser.UserGUID = ActivityLog.UserGuid AND ProgramUser.ProgramGUID = ActivityLog.ProgramGuid INNER JOIN
                      [User] ON ProgramUser.UserGUID = [User].UserGUID INNER JOIN
                      UserType ON [User].UserType = UserType.UserTypeID
GO
/****** Object:  StoredProcedure [dbo].[ClearAccount]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClearAccount]
	@email [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
		SET NOCOUNT ON;    

	/*Delete User Page Variable*/
	DELETE 
	FROM UserPageVariable
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = @email
	)

	/*Delete User Page Variable Per Day*/
	DELETE 
	FROM UserPageVariablePerDay
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = @email
	)

	/*DELET Question Answer Value*/
	DELETE 
	FROM QuestionAnswerValue 
	WHERE QuestionAnswerGUID IN 
	(
		SELECT QuestionAnswerGUID
		FROM QuestionAnswer
		WHERE UserGUID IN
		(
			SELECT UserGUID FROM [User] WHERE Email = @email
		)
	)

	/*Delete Question Answer*/
	DELETE 
	FROM QuestionAnswer
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = @email
	)

	/*DELETE Program User*/
	DELETE  FROM ProgramUser
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = @email
	)

	DELETE FROM [User]
	WHERE Email = @email
END
GO
/****** Object:  StoredProcedure [dbo].[ClearAnswerOfAccount]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClearAnswerOfAccount]
	@email [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
		SET NOCOUNT ON;    

	/*Delete User Page Variable*/
	DELETE 
	FROM UserPageVariable
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = @email
	)

	/*Delete User Page Variable Per Day*/
	DELETE 
	FROM UserPageVariablePerDay
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = @email
	)

	/*DELET Question Answer Value*/
	DELETE 
	FROM QuestionAnswerValue 
	WHERE QuestionAnswerGUID IN 
	(
		SELECT QuestionAnswerGUID
		FROM QuestionAnswer
		WHERE UserGUID IN
		(
			SELECT UserGUID FROM [User] WHERE Email = @email
		)
	)

	/*Delete Question Answer*/
	DELETE 
	FROM QuestionAnswer
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = @email
	)
END
GO
/****** Object:  StoredProcedure [dbo].[ClearTempAccount]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClearTempAccount]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;    

	/*Delete User Page Variable*/
	DELETE 
	FROM UserPageVariable
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = 'ChangeTechTemp' + CAST(UserGUID AS NVARCHAR(50))
	)

	/*Delete User Page Variable Per Day*/
	DELETE 
	FROM UserPageVariablePerDay
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = 'ChangeTechTemp' + CAST(UserGUID AS NVARCHAR(50))
	)

	/*DELET Question Answer Value*/
	DELETE 
	FROM QuestionAnswerValue 
	WHERE QuestionAnswerGUID IN 
	(
		SELECT QuestionAnswerGUID
		FROM QuestionAnswer
		WHERE UserGUID IN
		(
			SELECT UserGUID FROM [User] WHERE Email = 'ChangeTechTemp' + CAST(UserGUID AS NVARCHAR(50))
		)
	)

	/*Delete Question Answer*/
	DELETE 
	FROM QuestionAnswer
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = 'ChangeTechTemp' + CAST(UserGUID AS NVARCHAR(50))
	)

	/*DELETE Program User*/
	DELETE  FROM ProgramUser
	WHERE UserGUID IN
	(
		SELECT UserGUID FROM [User] WHERE Email = 'ChangeTechTemp' + CAST(UserGUID AS NVARCHAR(50))
	)

	/*Delete User*/
	DELETE  FROM [User] WHERE Email = 'ChangeTechTemp' + CAST(UserGUID AS NVARCHAR(50))
END
GO
/****** Object:  StoredProcedure [dbo].[GetSessionEndingModelAsXML]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSessionEndingModelAsXML]
	@programGUID [uniqueidentifier],
	@languageGUID [uniqueidentifier],
	@userGUID [uniqueidentifier],
	@day [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	  SELECT 
		@userGUID "@UserGUID",
		@programGUID "@ProgramGUID",
	(
		SELECT 
		'' "@GUID",
		'SessionEnding' "@Name",
		'the sesseion ending page' "@Description",
        ISNULL(ProgramUser.[Day],'0') "@Day",		
		[Resource].NameOnServer "@Logo",
		(
			SELECT Top 1 
				   '' "@CategoryName",
				   '' "@CategoryDescription",
				   '1' "@Order",
				   Program.GeneralColor "@PrimaryThemeColor",
				   Program.GeneralColor "@SecondaryThemeColor",
				   '' "@ProgramRoomName",
				   '' "@ProgramRoomDescription",
				   'SessionEnding' "@Name",
				(
					SELECT 
						 AccessoryTemplateGUID "@GUID",
						 [Type] "@Type",
						 [Order] "@Order",
						 Heading "@Title",
						 [Text] "@Text",
						 PrimaryButtonText "@ButtonPrimaryName"
					FROM AccessoryTemplate
					WHERE ProgramGUID = @programGUID and [Type]='Session ending'
					Order by [Order] asc
					FOR XML PATH('Page'), TYPE				
				)
			FROM AccessoryTemplate, Program
			WHERE Program.ProgramGUID = @programGUID
			FOR XML PATH('PageSequence'), TYPE
		)
		FROM Program 
		LEFT JOIN [Resource] ON Program.ProgramLogoGUID = [Resource].ResourceGUID
		LEFT JOIN ProgramUser ON ProgramUser.ProgramGUID = Program.ProgramGUID AND ProgramUser.UserGUID = @userGUID
		WHERE Program.ProgramGUID = @programGUID
		FOR XML PATH('Session'), TYPE
	),
	(
		SELECT
		(
			SELECT a.TipMessageTypeName "@Name",
			b.Title "@Title",
			b.Message "@Message",
			b.BackButtonName "@BackButtonName"
			FROM TipMessageType a LEFT JOIN TipMessage b ON a.TipMessageTypeGUID = b.TipMessageTypeGUID
			WHERE b.LanguageGUID = @languageGuid
			FOR XML PATH('Message'), TYPE
		)
		FOR XML PATH('TipMessages'), TYPE
	),
	(
		SELECT
		[Value] "@Title",
		(
			SELECT 
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid AND LanguageGUID = @languageGuid AND @day <> 0
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		),
		(
			SELECT TOP 1
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid AND LanguageGUID = @languageGuid AND @day = 0
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		)
		FROM SpecialString WHERE LanguageGUID = @languageGuid AND [Name]='SettingMenu'
		FOR XML PATH('SettingMenu'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormBackButtonName "@BackButtonName",
		(
			SELECT
			Question "@Title",
			Answer "@Text"
			FROM HelpItem
			WHERE ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('HelpItem'), TYPE
		)
		FROM UserMenu
		WHERE [Name] = 'HelpFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Help'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'TipFriendFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('TipFriend'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'PauseProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ProgramStatus'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName",
		(
			SELECT
			'Email' "@Name",
			Email "@OldValue",
			'email' "@Format"
			FROM [User]
			WHERE UserGUID = @userGuid
			FOR XML PATH('Item'), TYPE
		)
		FROM UserMenu
		WHERE [Name] = 'ProfileFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Profile'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormText "@Text",
		MenuFormBackButtonName "@BackButtonName",
		MenuFormSubmitButtonName "@SubmitButtonName"		
		FROM UserMenu
		WHERE [Name] = 'ExitProgramFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('ExitProgram'), TYPE
	)
	From Program where Program.ProgramGUID = @programGUID
	FOR XML PATH('XMLModel')
END
GO
/****** Object:  View [dbo].[V_ActivityLogStartTime]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_ActivityLogStartTime]
AS
SELECT DISTINCT ActivityLogType, UserGuid, ProgramGuid, SessionGuid, MAX(ActivityDateTime) AS ActivityDateTime
FROM         dbo.ActivityLog
WHERE     (ActivityLogType = 5)
GROUP BY ActivityLogType, UserGuid, ProgramGuid, SessionGuid
GO
/****** Object:  View [dbo].[V_ActivityLogEndTime]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_ActivityLogEndTime]
AS
SELECT     ActivityLogType, UserGuid, ProgramGuid, SessionGuid, MAX(ActivityDateTime) AS ActivityDateTime
FROM         dbo.ActivityLog
WHERE     (ActivityLogType = 3)
GROUP BY ActivityLogType, UserGuid, ProgramGuid, SessionGuid
GO
/****** Object:  View [dbo].[V_ActivityLogLoginTime]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_ActivityLogLoginTime]
AS
SELECT     ActivityLogType, UserGuid, ProgramGuid, SessionGuid, MAX(ActivityDateTime) AS ActivityDateTime
FROM         dbo.ActivityLog
WHERE     (ActivityLogType = 2)
GROUP BY ActivityLogType, UserGuid, ProgramGuid, SessionGuid
GO
/****** Object:  StoredProcedure [dbo].[GetProgramXML]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProgramXML]
	@programGUID [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT 
	Program.Name "@Name",
	[Language].Name "@Language",
	Program.[Description] "@Description",
		(
		SELECT 
		Name "@Name",
		[Day] "@Order",
		[Description] "@Description",	
			(
			SELECT
			PageSequence.Name "@Name",
			[SessionContent].PageSequenceOrderNo "@Order",
			PageSequence.[Description] "@Description",
				(
				SELECT
				PageTemplate.Name "@Type",
				[Page].PageOrderNo "@Order",
				PageContent.Heading "@Title",
				PageContent.Body "@Text",
				PageContent.FooterText "@FooterText",
				PageContent.BeforeShowExpression "@BeforeShowExpression",
				PageContent.AfterShowExpression "@AfterShowExpression"
				FROM [Page]
				LEFT JOIN PageTemplate ON [Page].PageTemplateGUID = PageTemplate.PageTemplateGUID
				LEFT JOIN PageContent ON PageContent.PageGUID = [Page].PageGUID
 				WHERE [Page].PageSequenceGUID = PageSequence.PageSequenceGUID AND ([Page].IsDeleted IS NULL OR [Page].IsDeleted = 'False')
				ORDER BY [Page].PageOrderNo
				FOR XML PATH('Page'),Type
				)
			FROM [SessionContent] LEFT JOIN PageSequence 			
			ON [SessionContent].PageSequenceGUID = PageSequence.PageSequenceGUID
			WHERE [SessionContent].SessionGUID = [Session].SessionGUID AND ([SessionContent].IsDeleted IS NULL OR [SessionContent].IsDeleted = 'False')
			ORDER BY [SessionContent].PageSequenceOrderNo
			FOR XML PATH('PageSequence'),Type
			)
		FROM [Session]
		WHERE [Session].ProgramGUID = Program.ProgramGUID AND ([Session].IsDeleted IS NULL OR [Session].IsDeleted = 'False')
	    ORDER BY [Session].[Day]
		FOR XML PATH('Session'),Type		
		)	
	FROM Program LEFT JOIN [Language]
	ON Program.DefaultLanguageGUID = [Language].LanguageGUID
	WHERE Program.ProgramGUID = @programGUID
	FOR XML PATH('Program'), Type
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetProgramLoginAndPasswordReminderAsXML]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProgramLoginAndPasswordReminderAsXML]
	@programGUID [uniqueidentifier],
	@languageGUID [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		'' "@UserGUID",
		@programGUID "@ProgramGUID",
		'false' "@MissedDay",
	(
		SELECT 
		'' "@GUID",
		'Login' "@Name",
		'the login session' "@Description",
        '0' "@Day",		
		[Resource].NameOnServer "@Logo",
		(
			SELECT Top 1 
				   '' "@CategoryName",
				   '' "@CategoryDescription",
				   '1' "@Order",
				   Program.GeneralColor "@PrimaryThemeColor",
				   Program.GeneralColor "@SecondaryThemeColor",
				   '' "@ProgramRoomName",
				   '' "@ProgramRoomDescription",
				   'Accessory' "@Name",
				(
					SELECT 
						 AccessoryTemplateGUID "@GUID",
						 [Type] "@Type",
						 [Order] "@Order",
						 Heading "@Title",
						 [Text] +';'+UserNameText+';'+ ISNULL(PasswordText,'') "@Text",
						 SecondaryButtonText "@FooterText",
						 PrimaryButtonText "@ButtonPrimaryName",
						 '0' "@ButtonPrimaryAction",
						 '0' "@ButtonSecondaryAction"
					FROM AccessoryTemplate
					WHERE ProgramGUID = @programGUID 
					--and LanguageGUID = @languageGUID 
					and ([Type]='Login' or [Type]='Password reminder')
					Order by [Order] asc
					FOR XML PATH('Page'), TYPE				
				)
			FROM AccessoryTemplate,Program
			WHERE Program.ProgramGUID = @programGuid
			FOR XML PATH('PageSequence'), TYPE
		)
		FROM Program LEFT JOIN [Resource] ON Program.ProgramLogoGUID = [Resource].ResourceGUID
		WHERE Program.ProgramGUID = @programGUID
		FOR XML PATH('Session'), TYPE
	),
	(
		SELECT
		(
			SELECT a.TipMessageTypeName "@Name",
			b.Title "@Title",
			b.Message "@Message",
			b.BackButtonName "@BackButtonName"
			FROM TipMessageType a LEFT JOIN TipMessage b ON a.TipMessageTypeGUID = b.TipMessageTypeGUID
			WHERE b.LanguageGUID = @languageGuid
			FOR XML PATH('Message'), TYPE
		)
		FOR XML PATH('TipMessages'), TYPE
	),
	(
		SELECT
		[Value] "@Title",
		(
			SELECT TOP 1
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid AND LanguageGUID = @languageGuid
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		)
		FROM SpecialString WHERE LanguageGUID = @languageGuid AND [Name]='SettingMenu'
		FOR XML PATH('SettingMenu'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormBackButtonName "@BackButtonName",
		(
			SELECT
			Question "@Title",
			Answer "@Text"
			FROM HelpItem
			WHERE ProgramGUID = @programGuid ORDER BY [Order]
			FOR XML PATH('HelpItem'), TYPE
		)
		FROM UserMenu
		WHERE [Name] = 'HelpFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Help'), TYPE
	)
	From Program where Program.ProgramGUID = @programGUID
	FOR XML PATH('XMLModel')
END
GO
/****** Object:  StoredProcedure [dbo].[GetEmptySessionXML]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEmptySessionXML]
	@programGuid [uniqueidentifier],
	@userGuid [uniqueidentifier],
	@languageGuid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     SELECT 
	 @userGuid "@UserGUID",
	 @programGUID "@ProgramGUID",
	 'false' "@MissedDay",
	(
		SELECT 
		NEWID() "@GUID",
		'SessionEnding' "@Name",
		'Server message' "@Description",
        '0' "@Day",		
		[Resource].NameOnServer "@Logo",
		(
			SELECT Top 1 
				   '' "@CategoryName",
				   '' "@CategoryDescription",
				   '1' "@Order",
				   Program.GeneralColor "@PrimaryThemeColor",
				   Program.GeneralColor "@SecondaryThemeColor",
				   '' "@ProgramRoomName",
				   '' "@ProgramRoomDescription",
				   'SessionEnding' "@Name",
				(
					SELECT 
						 NEWID() "@GUID",
						 'Session ending' "@Type",
						 '0' "@Order",
						 '' "@Title",
						 '{0}' "@Text",
						 'OK' "@ButtonPrimaryName"
					FOR XML PATH('Page'), TYPE				
				)
			FROM AccessoryTemplate LEFT JOIN Program ON AccessoryTemplate.ProgramGUID = Program.ProgramGUID
			FOR XML PATH('PageSequence'), TYPE
		)
		FROM Program LEFT JOIN [Resource] ON Program.ProgramLogoGUID = [Resource].ResourceGUID
		WHERE Program.ProgramGUID = @programGUID
		FOR XML PATH('Session'), TYPE
	),
	(
		SELECT
		(
			SELECT a.TipMessageTypeName "@Name",
			b.Title "@Title",
			b.Message "@Message",
			b.BackButtonName "@BackButtonName"
			FROM TipMessageType a LEFT JOIN TipMessage b ON a.TipMessageTypeGUID = b.TipMessageTypeGUID
			WHERE b.LanguageGUID = @languageGuid
			FOR XML PATH('Message'), TYPE
		)
		FOR XML PATH('TipMessages'), TYPE
	),
	(
		SELECT
		[Value] "@Title",
		(
			SELECT TOP 1
			[Name] "@FunctionName",
			MenuText "@Name"
			FROM UserMenu
			WHERE ProgramGUID = @programGuid
			ORDER BY [Order] ASC
			FOR XML PATH('MenuItem'), TYPE
		)
		FROM SpecialString WHERE LanguageGUID = @languageGuid AND [Name]='SettingMenu'
		FOR XML PATH('SettingMenu'), TYPE
	),
	(
		SELECT
		MenuFormTitle "@Title",
		MenuFormBackButtonName "@BackButtonName",
		(
			SELECT
			Question "@Title",
			Answer "@Text"
			FROM HelpItem
			WHERE ProgramGUID = @programGuid ORDER BY [Order]
			FOR XML PATH('HelpItem'), TYPE
		)
		FROM UserMenu
		WHERE [Name] = 'HelpFunction' AND 
		ProgramGUID = @programGuid
		FOR XML PATH('Help'), TYPE
	)
	From Program where Program.ProgramGUID = @programGUID
	FOR XML PATH('XMLModel')
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePageVariableInPageAndPageQuestion]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePageVariableInPageAndPageQuestion]
	@oldProgramGUID [uniqueidentifier],
	@newProgarmGUID [uniqueidentifier],
	@pageVariableName [nvarchar](500)
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [Page]
	SET [PageVariableGUID] = (SELECT [PageVariableGUID] FROM PageVariable Where [ProgramGUID]=@newProgarmGUID and Name = @pageVariableName )
	Where [PageVariableGUID] = (SELECT [PageVariableGUID] FROM PageVariable Where [ProgramGUID]=@oldProgramGUID and Name = @pageVariableName )
	AND [PageGUID] in (SELECT [PageGUID] 
    FROM [Page] 
	LEFT JOIN [SessionContent] on [Page].PageSequenceGUID = [SessionContent].PageSequenceGUID
	LEFT JOIN [Session] on [Session].SessionGUID = [SessionContent].SessionGUID
	WHERE [Session].ProgramGUID = @newProgarmGUID
	 ) 
	
	UPDATE [PageQuestion]
	SET [PageVariableGUID] =(SELECT [PageVariableGUID] FROM PageVariable Where [ProgramGUID]=@newProgarmGUID and Name = @pageVariableName )
	Where [PageVariableGUID] = (SELECT [PageVariableGUID] FROM PageVariable Where [ProgramGUID]=@oldProgramGUID and Name = @pageVariableName )
	AND [PageQuestion].PageGUID in (SELECT [PageGUID] 
    FROM [Page] 
	LEFT JOIN [SessionContent] on [Page].PageSequenceGUID = [SessionContent].PageSequenceGUID
	LEFT JOIN [Session] on [Session].SessionGUID = [SessionContent].SessionGUID
	WHERE [Session].ProgramGUID = @newProgarmGUID
	 ) 
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuestionItemByProgram]    Script Date: 06/08/2010 16:06:24 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuestionItemByProgram]
	@ProgramGuid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT     PageQuestionItemContent.PageQuestionItemGUID, PageQuestionItemContent.Item, PageQuestionItemContent.Feedback, 
                      PageQuestionItemContent.LastUpdated, PageQuestionItemContent.LastUpdatedBy, PageQuestionItemContent.IsDeleted
	FROM         Program INNER JOIN
                      Session ON Program.ProgramGUID = Session.ProgramGUID INNER JOIN
                      SessionContent ON Session.SessionGUID = SessionContent.SessionGUID INNER JOIN
                      PageSequence ON SessionContent.PageSequenceGUID = PageSequence.PageSequenceGUID INNER JOIN
                      Page ON PageSequence.PageSequenceGUID = Page.PageSequenceGUID INNER JOIN
                      PageQuestion ON Page.PageGUID = PageQuestion.PageGUID INNER JOIN
                      PageQuestionItem ON PageQuestion.PageQuestionGUID = PageQuestionItem.PageQuestionGUID INNER JOIN
                      PageQuestionItemContent ON PageQuestionItem.PageQuestionItemGUID = PageQuestionItemContent.PageQuestionItemGUID
	Where	Program.ProgramGUID = @ProgramGuid AND
	([Session].IsDeleted IS NULL OR [Session].IsDeleted = 'False') AND
	(SessionContent.IsDeleted IS NULL OR SessionContent.IsDeleted = 'False') AND
	(PageSequence.IsDeleted IS NULL OR PageSequence.IsDeleted = 'False') AND
	(Page.IsDeleted IS NULL OR Page.IsDeleted = 'False') AND
	(PageQuestion.IsDeleted IS NULL OR PageQuestion.IsDeleted = 'False') AND
	(PageQuestionItem.IsDeleted IS NULL OR PageQuestionItem.IsDeleted = 'False') AND
	(PageQuestionItemContent.IsDeleted IS NULL OR PageQuestionItemContent.IsDeleted = 'False')
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuestionByProgram]    Script Date: 06/08/2010 16:06:25 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuestionByProgram]
	@ProgramGuid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT     PageQuestionContent.PageQuestionGUID, PageQuestionContent.Caption, PageQuestionContent.LastUpdated, 
                      PageQuestionContent.LastUpdatedBy, PageQuestionContent.IsDeleted, PageQuestionContent.DisableCheckBox
	FROM         Program INNER JOIN
                      Session ON Program.ProgramGUID = Session.ProgramGUID INNER JOIN
                      SessionContent ON Session.SessionGUID = SessionContent.SessionGUID INNER JOIN
                      PageSequence ON SessionContent.PageSequenceGUID = PageSequence.PageSequenceGUID INNER JOIN
                      Page ON PageSequence.PageSequenceGUID = Page.PageSequenceGUID INNER JOIN
                      PageQuestion ON Page.PageGUID = PageQuestion.PageGUID INNER JOIN
                      PageQuestionContent ON PageQuestion.PageQuestionGUID = PageQuestionContent.PageQuestionGUID
	Where	Program.ProgramGUID = @ProgramGuid AND
	([Session].IsDeleted IS NULL OR [Session].IsDeleted = 'False') AND
	(SessionContent.IsDeleted IS NULL OR SessionContent.IsDeleted = 'False') AND
	(PageSequence.IsDeleted IS NULL OR PageSequence.IsDeleted = 'False') AND
	(Page.IsDeleted IS NULL OR Page.IsDeleted = 'False') AND
	(PageQuestion.IsDeleted IS NULL OR PageQuestion.IsDeleted = 'False') AND
	(PageQuestionContent.IsDeleted IS NULL OR PageQuestionContent.IsDeleted = 'False')
END
GO
/****** Object:  StoredProcedure [dbo].[GetPageContentByProgram]    Script Date: 06/08/2010 16:06:25 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPageContentByProgram]
	@ProgramGuid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT     PageContent.PageGUID, PageContent.Heading, PageContent.Body, PageContent.FooterText, PageContent.PresenterImageGUID, 
                      PageContent.PresenterImagePosition, PageContent.BackgroundImageGUID, PageContent.PrimaryButtonCaption, PageContent.PrimaryButtonActionParameter, 
                      PageContent.AfterShowExpression, PageContent.BeforeShowExpression, PageContent.BeforeShowExpressionGUID, PageContent.AfterShowExpressionGUID, 
                      PageContent.LastUpdated, PageContent.LastUpdatedBy, PageContent.IsDeleted
	FROM         Program INNER JOIN
                      [Session] ON Program.ProgramGUID = [Session].ProgramGUID INNER JOIN
                      SessionContent ON Session.SessionGUID = SessionContent.SessionGUID INNER JOIN
                      PageSequence ON SessionContent.PageSequenceGUID = PageSequence.PageSequenceGUID INNER JOIN
                      Page ON PageSequence.PageSequenceGUID = Page.PageSequenceGUID INNER JOIN
                      PageContent ON Page.PageGUID = PageContent.PageGUID
	Where	Program.ProgramGUID = @ProgramGuid AND
	([Session].IsDeleted IS NULL OR [Session].IsDeleted = 'False') AND
	(SessionContent.IsDeleted IS NULL OR SessionContent.IsDeleted = 'False') AND
	(PageSequence.IsDeleted IS NULL OR PageSequence.IsDeleted = 'False') AND
	(Page.IsDeleted IS NULL OR Page.IsDeleted = 'False') AND
	(PageContent.IsDeleted IS NULL OR PageContent.IsDeleted = 'False')
END
GO
/****** Object:  StoredProcedure [dbo].[GetProgramByPageAndLanguageGUID]    Script Date: 06/08/2010 16:06:25 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProgramByPageAndLanguageGUID]
	@PageGUID [uniqueidentifier],
	@LanguageGUID [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT DISTINCT Program.*
	FROM Page INNER JOIN
          PageSequence ON Page.PageSequenceGUID = PageSequence.PageSequenceGUID INNER JOIN
          SessionContent ON PageSequence.PageSequenceGUID = SessionContent.PageSequenceGUID INNER JOIN
          Session ON SessionContent.SessionGUID = Session.SessionGUID INNER JOIN
          Program ON Session.ProgramGUID = Program.ProgramGUID INNER JOIN
          PageContent ON Page.PageGUID = PageContent.PageGUID
	Where	PageContent.PageGUID = @PageGUID
	AND (Program.IsDeleted IS NULL OR Program.IsDeleted = 'False')
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProgramRoomInSessionContent]    Script Date: 06/08/2010 16:06:25 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateProgramRoomInSessionContent]
	@oldProgramGUID [uniqueidentifier],
	@newProgarmGUID [uniqueidentifier],
	@programRoomName [nvarchar](500)
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [SessionContent]
    SET [ProgramRoomGUID] = (SELECT [ProgramRoomGUID] FROM ProgramRoom Where [ProgramGUID]=@newProgarmGUID and Name = @programRoomName )
    WHERE [ProgramRoomGUID] = (SELECT [ProgramRoomGUID] FROM ProgramRoom Where [ProgramGUID]=@oldProgramGUID and Name = @programRoomName )
    AND [SessionGUID] in (SELECT [SessionGUID] FROM [Session] WHERE ProgramGUID = @newProgarmGUID)
END
GO
/****** Object:  View [dbo].[V_ActivityLog]    Script Date: 06/08/2010 16:06:25 ******/

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_ActivityLog]
AS
SELECT     V_ActivityLogUser.UserGuid, V_ActivityLogUser.ProgramGuid, V_ActivityLogUser.SessionGuid, V_ActivityLogUser.Email, 
                      V_ActivityLogLoginTime.ActivityDateTime AS LoginTime, V_ActivityLogStartTime.ActivityDateTime AS StartTime, 
                      V_ActivityLogEndTime.ActivityDateTime AS EndTime, V_ActivityLogUser.Status, V_ActivityLogUser.Name AS UserType, 
                      Session.Name AS SessionName, Program.Name AS ProgramName, Session.Day
FROM         V_ActivityLogUser INNER JOIN
                      Session ON V_ActivityLogUser.SessionGuid = Session.SessionGUID INNER JOIN
                      Program ON V_ActivityLogUser.ProgramGuid = Program.ProgramGUID LEFT OUTER JOIN
                      V_ActivityLogStartTime ON V_ActivityLogUser.UserGuid = V_ActivityLogStartTime.UserGuid AND 
                      V_ActivityLogUser.ProgramGuid = V_ActivityLogStartTime.ProgramGuid AND 
                      V_ActivityLogUser.SessionGuid = V_ActivityLogStartTime.SessionGuid LEFT OUTER JOIN
                      V_ActivityLogEndTime ON V_ActivityLogUser.UserGuid = V_ActivityLogEndTime.UserGuid AND 
                      V_ActivityLogUser.ProgramGuid = V_ActivityLogEndTime.ProgramGuid AND 
                      V_ActivityLogUser.SessionGuid = V_ActivityLogEndTime.SessionGuid LEFT OUTER JOIN
                      V_ActivityLogLoginTime ON V_ActivityLogUser.UserGuid = V_ActivityLogLoginTime.UserGuid AND 
                      V_ActivityLogUser.ProgramGuid = V_ActivityLogLoginTime.ProgramGuid AND 
                      V_ActivityLogUser.SessionGuid = V_ActivityLogLoginTime.SessionGuid
WHERE     (V_ActivityLogUser.Email NOT IN
                          (SELECT     Email
                            FROM          [User]
                            WHERE      (Email LIKE 'ChangeTechTemp%')))
GO
/****** Object:  Default [DF_User_LastLogon]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_LastLogon]  DEFAULT (getdate()) FOR [LastLogon]
GO
/****** Object:  Default [DF_User_Security]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Security]  DEFAULT ((0)) FOR [Security]
GO
/****** Object:  Default [DF_PredictorType_PredictorTypePK]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PredictorCategory] ADD  CONSTRAINT [DF_PredictorType_PredictorTypePK]  DEFAULT (newid()) FOR [PredictorCategoryGUID]
GO
/****** Object:  Default [DF_PageQuestion_Order]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageQuestion] ADD  CONSTRAINT [DF_PageQuestion_Order]  DEFAULT ((0)) FOR [Order]
GO
/****** Object:  Default [DF_Program_ProgramGUID]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Program] ADD  CONSTRAINT [DF_Program_ProgramGUID]  DEFAULT (newid()) FOR [ProgramGUID]
GO
/****** Object:  Default [DF_ProgramUser_Security]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[ProgramUser] ADD  CONSTRAINT [DF_ProgramUser_Security]  DEFAULT ((20)) FOR [Security]
GO
/****** Object:  Default [DF_Intervent_InterventGUID]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[InterventCategory] ADD  CONSTRAINT [DF_Intervent_InterventGUID]  DEFAULT (newid()) FOR [InterventCategoryGUID]
GO
/****** Object:  Default [DF_PredictorReference_PredictorReferencePK]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PredictorMaterial] ADD  CONSTRAINT [DF_PredictorReference_PredictorReferencePK]  DEFAULT (newid()) FOR [PredictorReferenceGUID]
GO
/****** Object:  Default [DF_PageContent_PrimaryButtonActionParameter]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageContent] ADD  CONSTRAINT [DF_PageContent_PrimaryButtonActionParameter]  DEFAULT ((0)) FOR [PrimaryButtonActionParameter]
GO
/****** Object:  Default [DF_PageQuestionContent_DisableCheckBox]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageQuestionContent] ADD  CONSTRAINT [DF_PageQuestionContent_DisableCheckBox]  DEFAULT (N'Don''t know') FOR [DisableCheckBox]
GO
/****** Object:  Default [DF_PageVariable_ValueType]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageVariable] ADD  CONSTRAINT [DF_PageVariable_ValueType]  DEFAULT (N'String') FOR [ValueType]
GO
/****** Object:  Default [DF_Session_SessionGUID]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_SessionGUID]  DEFAULT (newid()) FOR [SessionGUID]
GO
/****** Object:  Default [DF_Page_PageGUID]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Page_PageGUID]  DEFAULT (newid()) FOR [PageGUID]
GO
/****** Object:  Default [DF_Page_IsDeleted]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Page_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Sequence_SequenceGUID]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageSequence] ADD  CONSTRAINT [DF_Sequence_SequenceGUID]  DEFAULT (newid()) FOR [PageSequenceGUID]
GO
/****** Object:  Default [DF_SpecificIntervent_SpecificInterventGUID]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Intervent] ADD  CONSTRAINT [DF_SpecificIntervent_SpecificInterventGUID]  DEFAULT (newid()) FOR [InterventGUID]
GO
/****** Object:  Default [DF_SessionContent_SessionContentGUID]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[SessionContent] ADD  CONSTRAINT [DF_SessionContent_SessionContentGUID]  DEFAULT (newid()) FOR [SessionContentGUID]
GO
/****** Object:  Default [DF_Predictor_PredictorGUID]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Predictor] ADD  CONSTRAINT [DF_Predictor_PredictorGUID]  DEFAULT (newid()) FOR [PredictorGUID]
GO
/****** Object:  Default [DF_Resource_StoreName]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Resource] ADD  CONSTRAINT [DF_Resource_StoreName]  DEFAULT (N'ResourceGUID + FileExtenseion') FOR [NameOnServer]
GO
/****** Object:  ForeignKey [FK_PageQuestion_Page]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageQuestion]  WITH NOCHECK ADD  CONSTRAINT [FK_PageQuestion_Page] FOREIGN KEY([PageGUID])
REFERENCES [dbo].[Page] ([PageGUID])
GO
ALTER TABLE [dbo].[PageQuestion] CHECK CONSTRAINT [FK_PageQuestion_Page]
GO
/****** Object:  ForeignKey [FK_PageQuestion_PageVariable]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageQuestion]  WITH NOCHECK ADD  CONSTRAINT [FK_PageQuestion_PageVariable] FOREIGN KEY([PageVariableGUID])
REFERENCES [dbo].[PageVariable] ([PageVariableGUID])
GO
ALTER TABLE [dbo].[PageQuestion] CHECK CONSTRAINT [FK_PageQuestion_PageVariable]
GO
/****** Object:  ForeignKey [FK_PageQuestion_Question]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageQuestion]  WITH NOCHECK ADD  CONSTRAINT [FK_PageQuestion_Question] FOREIGN KEY([QuestionGUID])
REFERENCES [dbo].[Question] ([QuestionGUID])
GO
ALTER TABLE [dbo].[PageQuestion] CHECK CONSTRAINT [FK_PageQuestion_Question]
GO
/****** Object:  ForeignKey [FK_EmailTemplate_EmailTemplateType]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[EmailTemplate]  WITH NOCHECK ADD  CONSTRAINT [FK_EmailTemplate_EmailTemplateType] FOREIGN KEY([EmailTemplateTypeGUID])
REFERENCES [dbo].[EmailTemplateType] ([EmailTemplateTypeGUID])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK_EmailTemplate_EmailTemplateType]
GO
/****** Object:  ForeignKey [FK_EmailTemplate_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[EmailTemplate]  WITH NOCHECK ADD  CONSTRAINT [FK_EmailTemplate_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK_EmailTemplate_Program]
GO
/****** Object:  ForeignKey [FK_EmailTemplateTypeContent_EmailTemplateType]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[EmailTemplateTypeContent]  WITH NOCHECK ADD  CONSTRAINT [FK_EmailTemplateTypeContent_EmailTemplateType] FOREIGN KEY([EmailTemplateTypeGUID])
REFERENCES [dbo].[EmailTemplateType] ([EmailTemplateTypeGUID])
GO
ALTER TABLE [dbo].[EmailTemplateTypeContent] CHECK CONSTRAINT [FK_EmailTemplateTypeContent_EmailTemplateType]
GO
/****** Object:  ForeignKey [FK_DeleteApplication_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[DeleteApplication]  WITH CHECK ADD  CONSTRAINT [FK_DeleteApplication_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[DeleteApplication] CHECK CONSTRAINT [FK_DeleteApplication_Program]
GO
/****** Object:  ForeignKey [FK_DeleteApplication_User]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[DeleteApplication]  WITH CHECK ADD  CONSTRAINT [FK_DeleteApplication_User] FOREIGN KEY([Applicant])
REFERENCES [dbo].[User] ([UserGUID])
GO
ALTER TABLE [dbo].[DeleteApplication] CHECK CONSTRAINT [FK_DeleteApplication_User]
GO
/****** Object:  ForeignKey [FK_DeleteApplication_User1]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[DeleteApplication]  WITH CHECK ADD  CONSTRAINT [FK_DeleteApplication_User1] FOREIGN KEY([Assignee])
REFERENCES [dbo].[User] ([UserGUID])
GO
ALTER TABLE [dbo].[DeleteApplication] CHECK CONSTRAINT [FK_DeleteApplication_User1]
GO
/****** Object:  ForeignKey [FK_QuestionAnswer_Page]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[QuestionAnswer]  WITH NOCHECK ADD  CONSTRAINT [FK_QuestionAnswer_Page] FOREIGN KEY([PageGUID])
REFERENCES [dbo].[Page] ([PageGUID])
GO
ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_Page]
GO
/****** Object:  ForeignKey [FK_QuestionAnswer_PageQuestion]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[QuestionAnswer]  WITH NOCHECK ADD  CONSTRAINT [FK_QuestionAnswer_PageQuestion] FOREIGN KEY([PageQuestionGUID])
REFERENCES [dbo].[PageQuestion] ([PageQuestionGUID])
GO
ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_PageQuestion]
GO
/****** Object:  ForeignKey [FK_QuestionAnswer_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[QuestionAnswer]  WITH NOCHECK ADD  CONSTRAINT [FK_QuestionAnswer_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_Program]
GO
/****** Object:  ForeignKey [FK_QuestionAnswer_SessionContent]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[QuestionAnswer]  WITH NOCHECK ADD  CONSTRAINT [FK_QuestionAnswer_SessionContent] FOREIGN KEY([SessionContentGUID])
REFERENCES [dbo].[SessionContent] ([SessionContentGUID])
GO
ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_SessionContent]
GO
/****** Object:  ForeignKey [FK_QuestionAnswer_User]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[QuestionAnswer]  WITH NOCHECK ADD  CONSTRAINT [FK_QuestionAnswer_User] FOREIGN KEY([UserGUID])
REFERENCES [dbo].[User] ([UserGUID])
GO
ALTER TABLE [dbo].[QuestionAnswer] CHECK CONSTRAINT [FK_QuestionAnswer_User]
GO
/****** Object:  ForeignKey [FK_UserPageVariable_PageVariable]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[UserPageVariable]  WITH NOCHECK ADD  CONSTRAINT [FK_UserPageVariable_PageVariable] FOREIGN KEY([PageVariableGUID])
REFERENCES [dbo].[PageVariable] ([PageVariableGUID])
GO
ALTER TABLE [dbo].[UserPageVariable] CHECK CONSTRAINT [FK_UserPageVariable_PageVariable]
GO
/****** Object:  ForeignKey [FK_UserPageVariable_QuestionAnswer]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[UserPageVariable]  WITH NOCHECK ADD  CONSTRAINT [FK_UserPageVariable_QuestionAnswer] FOREIGN KEY([QuestionAnswerGUID])
REFERENCES [dbo].[QuestionAnswer] ([QuestionAnswerGUID])
GO
ALTER TABLE [dbo].[UserPageVariable] CHECK CONSTRAINT [FK_UserPageVariable_QuestionAnswer]
GO
/****** Object:  ForeignKey [FK_UserPageVariable_User]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[UserPageVariable]  WITH NOCHECK ADD  CONSTRAINT [FK_UserPageVariable_User] FOREIGN KEY([UserGUID])
REFERENCES [dbo].[User] ([UserGUID])
GO
ALTER TABLE [dbo].[UserPageVariable] CHECK CONSTRAINT [FK_UserPageVariable_User]
GO
/****** Object:  ForeignKey [FK_UserPageVariablePerDay_PageVariable]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[UserPageVariablePerDay]  WITH NOCHECK ADD  CONSTRAINT [FK_UserPageVariablePerDay_PageVariable] FOREIGN KEY([PageVariableGUID])
REFERENCES [dbo].[PageVariable] ([PageVariableGUID])
GO
ALTER TABLE [dbo].[UserPageVariablePerDay] CHECK CONSTRAINT [FK_UserPageVariablePerDay_PageVariable]
GO
/****** Object:  ForeignKey [FK_UserPageVariablePerDay_QuestionAnswer]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[UserPageVariablePerDay]  WITH NOCHECK ADD  CONSTRAINT [FK_UserPageVariablePerDay_QuestionAnswer] FOREIGN KEY([QuestionAnswerGUID])
REFERENCES [dbo].[QuestionAnswer] ([QuestionAnswerGUID])
GO
ALTER TABLE [dbo].[UserPageVariablePerDay] CHECK CONSTRAINT [FK_UserPageVariablePerDay_QuestionAnswer]
GO
/****** Object:  ForeignKey [FK_UserPageVariablePerDay_User]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[UserPageVariablePerDay]  WITH NOCHECK ADD  CONSTRAINT [FK_UserPageVariablePerDay_User] FOREIGN KEY([UserGUID])
REFERENCES [dbo].[User] ([UserGUID])
GO
ALTER TABLE [dbo].[UserPageVariablePerDay] CHECK CONSTRAINT [FK_UserPageVariablePerDay_User]
GO
/****** Object:  ForeignKey [FK_Program_Language]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Program]  WITH NOCHECK ADD  CONSTRAINT [FK_Program_Language] FOREIGN KEY([DefaultLanguageGUID])
REFERENCES [dbo].[Language] ([LanguageGUID])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_Language]
GO
/****** Object:  ForeignKey [FK_Program_ProgramStatus]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Program]  WITH NOCHECK ADD  CONSTRAINT [FK_Program_ProgramStatus] FOREIGN KEY([StatusGUID])
REFERENCES [dbo].[ProgramStatus] ([ProgramStatusGUID])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_ProgramStatus]
GO
/****** Object:  ForeignKey [FK_Program_Resource]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Program]  WITH NOCHECK ADD  CONSTRAINT [FK_Program_Resource] FOREIGN KEY([ProgramLogoGUID])
REFERENCES [dbo].[Resource] ([ResourceGUID])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_Resource]
GO
/****** Object:  ForeignKey [FK_Program_User]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Program]  WITH NOCHECK ADD  CONSTRAINT [FK_Program_User] FOREIGN KEY([ProjectManager])
REFERENCES [dbo].[User] ([UserGUID])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_User]
GO
/****** Object:  ForeignKey [FK_ProgramUser_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[ProgramUser]  WITH NOCHECK ADD  CONSTRAINT [FK_ProgramUser_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[ProgramUser] CHECK CONSTRAINT [FK_ProgramUser_Program]
GO
/****** Object:  ForeignKey [FK_ProgramUser_User]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[ProgramUser]  WITH NOCHECK ADD  CONSTRAINT [FK_ProgramUser_User] FOREIGN KEY([UserGUID])
REFERENCES [dbo].[User] ([UserGUID])
GO
ALTER TABLE [dbo].[ProgramUser] CHECK CONSTRAINT [FK_ProgramUser_User]
GO
/****** Object:  ForeignKey [FK_SpecialString_Language]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[SpecialString]  WITH NOCHECK ADD  CONSTRAINT [FK_SpecialString_Language] FOREIGN KEY([LanguageGUID])
REFERENCES [dbo].[Language] ([LanguageGUID])
GO
ALTER TABLE [dbo].[SpecialString] CHECK CONSTRAINT [FK_SpecialString_Language]
GO
/****** Object:  ForeignKey [FK_TipMessage_Language]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[TipMessage]  WITH NOCHECK ADD  CONSTRAINT [FK_TipMessage_Language] FOREIGN KEY([LanguageGUID])
REFERENCES [dbo].[Language] ([LanguageGUID])
GO
ALTER TABLE [dbo].[TipMessage] CHECK CONSTRAINT [FK_TipMessage_Language]
GO
/****** Object:  ForeignKey [FK_TipMessage_TipMessageType]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[TipMessage]  WITH NOCHECK ADD  CONSTRAINT [FK_TipMessage_TipMessageType] FOREIGN KEY([TipMessageTypeGUID])
REFERENCES [dbo].[TipMessageType] ([TipMessageTypeGUID])
GO
ALTER TABLE [dbo].[TipMessage] CHECK CONSTRAINT [FK_TipMessage_TipMessageType]
GO
/****** Object:  ForeignKey [FK_ProgramLanguage_Language]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[ProgramLanguage]  WITH NOCHECK ADD  CONSTRAINT [FK_ProgramLanguage_Language] FOREIGN KEY([LanguageGUID])
REFERENCES [dbo].[Language] ([LanguageGUID])
GO
ALTER TABLE [dbo].[ProgramLanguage] CHECK CONSTRAINT [FK_ProgramLanguage_Language]
GO
/****** Object:  ForeignKey [FK_ProgramLanguage_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[ProgramLanguage]  WITH NOCHECK ADD  CONSTRAINT [FK_ProgramLanguage_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[ProgramLanguage] CHECK CONSTRAINT [FK_ProgramLanguage_Program]
GO
/****** Object:  ForeignKey [FK_PageTemplate_ChannelType]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageTemplate]  WITH NOCHECK ADD  CONSTRAINT [FK_PageTemplate_ChannelType] FOREIGN KEY([ChannelTypeGUID])
REFERENCES [dbo].[ChannelType] ([ChannelTypeGUID])
GO
ALTER TABLE [dbo].[PageTemplate] CHECK CONSTRAINT [FK_PageTemplate_ChannelType]
GO
/****** Object:  ForeignKey [FK_InterventCategory_Predictor]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[InterventCategory]  WITH NOCHECK ADD  CONSTRAINT [FK_InterventCategory_Predictor] FOREIGN KEY([PredictorGUID])
REFERENCES [dbo].[Predictor] ([PredictorGUID])
GO
ALTER TABLE [dbo].[InterventCategory] CHECK CONSTRAINT [FK_InterventCategory_Predictor]
GO
/****** Object:  ForeignKey [FK_PredictorReference_Predictor]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PredictorMaterial]  WITH CHECK ADD  CONSTRAINT [FK_PredictorReference_Predictor] FOREIGN KEY([PredictorGUID])
REFERENCES [dbo].[Predictor] ([PredictorGUID])
GO
ALTER TABLE [dbo].[PredictorMaterial] CHECK CONSTRAINT [FK_PredictorReference_Predictor]
GO
/****** Object:  ForeignKey [FK_PageMedia_Page]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageMedia]  WITH NOCHECK ADD  CONSTRAINT [FK_PageMedia_Page] FOREIGN KEY([PageGUID])
REFERENCES [dbo].[Page] ([PageGUID])
GO
ALTER TABLE [dbo].[PageMedia] CHECK CONSTRAINT [FK_PageMedia_Page]
GO
/****** Object:  ForeignKey [FK_PageMedia_Resource]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageMedia]  WITH NOCHECK ADD  CONSTRAINT [FK_PageMedia_Resource] FOREIGN KEY([MediaGUID])
REFERENCES [dbo].[Resource] ([ResourceGUID])
GO
ALTER TABLE [dbo].[PageMedia] CHECK CONSTRAINT [FK_PageMedia_Resource]
GO
/****** Object:  ForeignKey [FK_QuestionAnswerValue_PageQuestionItem]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[QuestionAnswerValue]  WITH NOCHECK ADD  CONSTRAINT [FK_QuestionAnswerValue_PageQuestionItem] FOREIGN KEY([PageQuestionItemGUID])
REFERENCES [dbo].[PageQuestionItem] ([PageQuestionItemGUID])
GO
ALTER TABLE [dbo].[QuestionAnswerValue] CHECK CONSTRAINT [FK_QuestionAnswerValue_PageQuestionItem]
GO
/****** Object:  ForeignKey [FK_QuestionAnswerValue_QuestionAnswer]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[QuestionAnswerValue]  WITH NOCHECK ADD  CONSTRAINT [FK_QuestionAnswerValue_QuestionAnswer] FOREIGN KEY([QuestionAnswerGUID])
REFERENCES [dbo].[QuestionAnswer] ([QuestionAnswerGUID])
GO
ALTER TABLE [dbo].[QuestionAnswerValue] CHECK CONSTRAINT [FK_QuestionAnswerValue_QuestionAnswer]
GO
/****** Object:  ForeignKey [FK_QuestionAnswerValue_Resource]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[QuestionAnswerValue]  WITH NOCHECK ADD  CONSTRAINT [FK_QuestionAnswerValue_Resource] FOREIGN KEY([ResourceGUID])
REFERENCES [dbo].[Resource] ([ResourceGUID])
GO
ALTER TABLE [dbo].[QuestionAnswerValue] CHECK CONSTRAINT [FK_QuestionAnswerValue_Resource]
GO
/****** Object:  ForeignKey [FK_Preferences_Page]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Preferences]  WITH NOCHECK ADD  CONSTRAINT [FK_Preferences_Page] FOREIGN KEY([PageGUID])
REFERENCES [dbo].[Page] ([PageGUID])
GO
ALTER TABLE [dbo].[Preferences] CHECK CONSTRAINT [FK_Preferences_Page]
GO
/****** Object:  ForeignKey [FK_Preferences_PageVariable]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Preferences]  WITH NOCHECK ADD  CONSTRAINT [FK_Preferences_PageVariable] FOREIGN KEY([VariableGUID])
REFERENCES [dbo].[PageVariable] ([PageVariableGUID])
GO
ALTER TABLE [dbo].[Preferences] CHECK CONSTRAINT [FK_Preferences_PageVariable]
GO
/****** Object:  ForeignKey [FK_Preferences_Resource]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Preferences]  WITH NOCHECK ADD  CONSTRAINT [FK_Preferences_Resource] FOREIGN KEY([ImageGUID])
REFERENCES [dbo].[Resource] ([ResourceGUID])
GO
ALTER TABLE [dbo].[Preferences] CHECK CONSTRAINT [FK_Preferences_Resource]
GO
/****** Object:  ForeignKey [FK_PageContent_Expression]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageContent]  WITH NOCHECK ADD  CONSTRAINT [FK_PageContent_Expression] FOREIGN KEY([BeforeShowExpressionGUID])
REFERENCES [dbo].[Expression] ([ExpressionGUID])
GO
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Expression]
GO
/****** Object:  ForeignKey [FK_PageContent_Expression1]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageContent]  WITH NOCHECK ADD  CONSTRAINT [FK_PageContent_Expression1] FOREIGN KEY([AfterShowExpressionGUID])
REFERENCES [dbo].[Expression] ([ExpressionGUID])
GO
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Expression1]
GO
/****** Object:  ForeignKey [FK_PageContent_Page]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageContent]  WITH NOCHECK ADD  CONSTRAINT [FK_PageContent_Page] FOREIGN KEY([PageGUID])
REFERENCES [dbo].[Page] ([PageGUID])
GO
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Page]
GO
/****** Object:  ForeignKey [FK_PageContent_Resource_BackgroundImage]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageContent]  WITH NOCHECK ADD  CONSTRAINT [FK_PageContent_Resource_BackgroundImage] FOREIGN KEY([BackgroundImageGUID])
REFERENCES [dbo].[Resource] ([ResourceGUID])
GO
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Resource_BackgroundImage]
GO
/****** Object:  ForeignKey [FK_PageContent_Resource_PresenterImage]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageContent]  WITH NOCHECK ADD  CONSTRAINT [FK_PageContent_Resource_PresenterImage] FOREIGN KEY([PresenterImageGUID])
REFERENCES [dbo].[Resource] ([ResourceGUID])
GO
ALTER TABLE [dbo].[PageContent] CHECK CONSTRAINT [FK_PageContent_Resource_PresenterImage]
GO
/****** Object:  ForeignKey [FK_PageQuestionItem_PageQuestion]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageQuestionItem]  WITH NOCHECK ADD  CONSTRAINT [FK_PageQuestionItem_PageQuestion] FOREIGN KEY([PageQuestionGUID])
REFERENCES [dbo].[PageQuestion] ([PageQuestionGUID])
GO
ALTER TABLE [dbo].[PageQuestionItem] CHECK CONSTRAINT [FK_PageQuestionItem_PageQuestion]
GO
/****** Object:  ForeignKey [FK_PageQuestionContent_PageQuestion]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageQuestionContent]  WITH NOCHECK ADD  CONSTRAINT [FK_PageQuestionContent_PageQuestion] FOREIGN KEY([PageQuestionGUID])
REFERENCES [dbo].[PageQuestion] ([PageQuestionGUID])
GO
ALTER TABLE [dbo].[PageQuestionContent] CHECK CONSTRAINT [FK_PageQuestionContent_PageQuestion]
GO
/****** Object:  ForeignKey [FK_Graph_Page]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Graph]  WITH NOCHECK ADD  CONSTRAINT [FK_Graph_Page] FOREIGN KEY([PageGUID])
REFERENCES [dbo].[Page] ([PageGUID])
GO
ALTER TABLE [dbo].[Graph] CHECK CONSTRAINT [FK_Graph_Page]
GO
/****** Object:  ForeignKey [FK_ProgramSchedule_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[ProgramSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ProgramSchedule_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[ProgramSchedule] CHECK CONSTRAINT [FK_ProgramSchedule_Program]
GO
/****** Object:  ForeignKey [FK_PageVariable_PageVariableGroup]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageVariable]  WITH NOCHECK ADD  CONSTRAINT [FK_PageVariable_PageVariableGroup] FOREIGN KEY([PageVariableGroupGUID])
REFERENCES [dbo].[PageVariableGroup] ([PageVariableGroupGUID])
GO
ALTER TABLE [dbo].[PageVariable] CHECK CONSTRAINT [FK_PageVariable_PageVariableGroup]
GO
/****** Object:  ForeignKey [FK_PageVariable_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageVariable]  WITH NOCHECK ADD  CONSTRAINT [FK_PageVariable_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[PageVariable] CHECK CONSTRAINT [FK_PageVariable_Program]
GO
/****** Object:  ForeignKey [FK_ProgramRoom_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[ProgramRoom]  WITH NOCHECK ADD  CONSTRAINT [FK_ProgramRoom_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[ProgramRoom] CHECK CONSTRAINT [FK_ProgramRoom_Program]
GO
/****** Object:  ForeignKey [FK_PageVariableGroup_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageVariableGroup]  WITH NOCHECK ADD  CONSTRAINT [FK_PageVariableGroup_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[PageVariableGroup] CHECK CONSTRAINT [FK_PageVariableGroup_Program]
GO
/****** Object:  ForeignKey [FK_AccessoryTemplate_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[AccessoryTemplate]  WITH NOCHECK ADD  CONSTRAINT [FK_AccessoryTemplate_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[AccessoryTemplate] CHECK CONSTRAINT [FK_AccessoryTemplate_Program]
GO
/****** Object:  ForeignKey [FK_HelpItem_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[HelpItem]  WITH NOCHECK ADD  CONSTRAINT [FK_HelpItem_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[HelpItem] CHECK CONSTRAINT [FK_HelpItem_Program]
GO
/****** Object:  ForeignKey [FK_UserMenu_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[UserMenu]  WITH NOCHECK ADD  CONSTRAINT [FK_UserMenu_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[UserMenu] CHECK CONSTRAINT [FK_UserMenu_Program]
GO
/****** Object:  ForeignKey [FK_Session_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Session]  WITH NOCHECK ADD  CONSTRAINT [FK_Session_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Program]
GO
/****** Object:  ForeignKey [FK_ExpressionGroup_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[ExpressionGroup]  WITH NOCHECK ADD  CONSTRAINT [FK_ExpressionGroup_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[ExpressionGroup] CHECK CONSTRAINT [FK_ExpressionGroup_Program]
GO
/****** Object:  ForeignKey [FK_LayoutSetting_Program]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[LayoutSetting]  WITH NOCHECK ADD  CONSTRAINT [FK_LayoutSetting_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO
ALTER TABLE [dbo].[LayoutSetting] CHECK CONSTRAINT [FK_LayoutSetting_Program]
GO
/****** Object:  ForeignKey [FK_Page_PageSequence]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Page]  WITH NOCHECK ADD  CONSTRAINT [FK_Page_PageSequence] FOREIGN KEY([PageSequenceGUID])
REFERENCES [dbo].[PageSequence] ([PageSequenceGUID])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_PageSequence]
GO
/****** Object:  ForeignKey [FK_Page_PageTemplate]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Page]  WITH NOCHECK ADD  CONSTRAINT [FK_Page_PageTemplate] FOREIGN KEY([PageTemplateGUID])
REFERENCES [dbo].[PageTemplate] ([PageTemplateGUID])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_PageTemplate]
GO
/****** Object:  ForeignKey [FK_Page_PageVariable]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Page]  WITH NOCHECK ADD  CONSTRAINT [FK_Page_PageVariable] FOREIGN KEY([PageVariableGUID])
REFERENCES [dbo].[PageVariable] ([PageVariableGUID])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_PageVariable]
GO
/****** Object:  ForeignKey [FK_PageQuestionItemContent_PageQuestionItem]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageQuestionItemContent]  WITH NOCHECK ADD  CONSTRAINT [FK_PageQuestionItemContent_PageQuestionItem] FOREIGN KEY([PageQuestionItemGUID])
REFERENCES [dbo].[PageQuestionItem] ([PageQuestionItemGUID])
GO
ALTER TABLE [dbo].[PageQuestionItemContent] CHECK CONSTRAINT [FK_PageQuestionItemContent_PageQuestionItem]
GO
/****** Object:  ForeignKey [FK_Sequence_Intervent]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[PageSequence]  WITH NOCHECK ADD  CONSTRAINT [FK_Sequence_Intervent] FOREIGN KEY([InterventGUID])
REFERENCES [dbo].[Intervent] ([InterventGUID])
GO
ALTER TABLE [dbo].[PageSequence] CHECK CONSTRAINT [FK_Sequence_Intervent]
GO
/****** Object:  ForeignKey [FK_Intervent_InterventCategory]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Intervent]  WITH NOCHECK ADD  CONSTRAINT [FK_Intervent_InterventCategory] FOREIGN KEY([InterventCategoryGUID])
REFERENCES [dbo].[InterventCategory] ([InterventCategoryGUID])
GO
ALTER TABLE [dbo].[Intervent] CHECK CONSTRAINT [FK_Intervent_InterventCategory]
GO
/****** Object:  ForeignKey [FK_GraphContent_Graph]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[GraphContent]  WITH NOCHECK ADD  CONSTRAINT [FK_GraphContent_Graph] FOREIGN KEY([GraphGUID])
REFERENCES [dbo].[Graph] ([GraphGUID])
GO
ALTER TABLE [dbo].[GraphContent] CHECK CONSTRAINT [FK_GraphContent_Graph]
GO
/****** Object:  ForeignKey [FK_GraphItem_Graph]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[GraphItem]  WITH NOCHECK ADD  CONSTRAINT [FK_GraphItem_Graph] FOREIGN KEY([GraphGUID])
REFERENCES [dbo].[Graph] ([GraphGUID])
GO
ALTER TABLE [dbo].[GraphItem] CHECK CONSTRAINT [FK_GraphItem_Graph]
GO
/****** Object:  ForeignKey [FK_GraphItemContent_GraphItem]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[GraphItemContent]  WITH NOCHECK ADD  CONSTRAINT [FK_GraphItemContent_GraphItem] FOREIGN KEY([GraphItemGUID])
REFERENCES [dbo].[GraphItem] ([GraphItemGUID])
GO
ALTER TABLE [dbo].[GraphItemContent] CHECK CONSTRAINT [FK_GraphItemContent_GraphItem]
GO
/****** Object:  ForeignKey [FK_SessionContent_PageSequence]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[SessionContent]  WITH NOCHECK ADD  CONSTRAINT [FK_SessionContent_PageSequence] FOREIGN KEY([PageSequenceGUID])
REFERENCES [dbo].[PageSequence] ([PageSequenceGUID])
GO
ALTER TABLE [dbo].[SessionContent] CHECK CONSTRAINT [FK_SessionContent_PageSequence]
GO
/****** Object:  ForeignKey [FK_SessionContent_ProgramRoom]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[SessionContent]  WITH NOCHECK ADD  CONSTRAINT [FK_SessionContent_ProgramRoom] FOREIGN KEY([ProgramRoomGUID])
REFERENCES [dbo].[ProgramRoom] ([ProgramRoomGUID])
GO
ALTER TABLE [dbo].[SessionContent] CHECK CONSTRAINT [FK_SessionContent_ProgramRoom]
GO
/****** Object:  ForeignKey [FK_SessionContent_Session]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[SessionContent]  WITH NOCHECK ADD  CONSTRAINT [FK_SessionContent_Session] FOREIGN KEY([SessionGUID])
REFERENCES [dbo].[Session] ([SessionGUID])
GO
ALTER TABLE [dbo].[SessionContent] CHECK CONSTRAINT [FK_SessionContent_Session]
GO
/****** Object:  ForeignKey [FK_Expression_ExpressionGroup]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Expression]  WITH NOCHECK ADD  CONSTRAINT [FK_Expression_ExpressionGroup] FOREIGN KEY([ExpressionGroupGUID])
REFERENCES [dbo].[ExpressionGroup] ([ExpressionGroupGUID])
GO
ALTER TABLE [dbo].[Expression] CHECK CONSTRAINT [FK_Expression_ExpressionGroup]
GO
/****** Object:  ForeignKey [FK_Predictor_PredictorType]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Predictor]  WITH NOCHECK ADD  CONSTRAINT [FK_Predictor_PredictorType] FOREIGN KEY([PredictorCategoryGUID])
REFERENCES [dbo].[PredictorCategory] ([PredictorCategoryGUID])
GO
ALTER TABLE [dbo].[Predictor] CHECK CONSTRAINT [FK_Predictor_PredictorType]
GO
/****** Object:  ForeignKey [FK_Resource_ResourceCategory]    Script Date: 06/08/2010 16:06:23 ******/
ALTER TABLE [dbo].[Resource]  WITH NOCHECK ADD  CONSTRAINT [FK_Resource_ResourceCategory] FOREIGN KEY([ResourceCategoryGUID])
REFERENCES [dbo].[ResourceCategory] ([ResourceCategoryGUID])
GO
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_ResourceCategory]
GO

CREATE TABLE [dbo].[Company](
	[CompanyGUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

CREATE TABLE [dbo].[CompanyRight](
	[CompanyRightGUID] [uniqueidentifier] NOT NULL,
	[CompanyGUID] [uniqueidentifier] NULL,
	[ProgramGUID] [uniqueidentifier] NULL,
	[OverdueTime] [datetime] NULL,
 CONSTRAINT [PK_CompanyRight] PRIMARY KEY CLUSTERED 
(
	[CompanyRightGUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
) 

GO

ALTER TABLE [dbo].[CompanyRight]  WITH CHECK ADD  CONSTRAINT [FK_CompanyRight_Company] FOREIGN KEY([CompanyGUID])
REFERENCES [dbo].[Company] ([CompanyGUID])
GO

ALTER TABLE [dbo].[CompanyRight] CHECK CONSTRAINT [FK_CompanyRight_Company]
GO

ALTER TABLE [dbo].[CompanyRight]  WITH CHECK ADD  CONSTRAINT [FK_CompanyRight_Program] FOREIGN KEY([ProgramGUID])
REFERENCES [dbo].[Program] ([ProgramGUID])
GO

ALTER TABLE [dbo].[CompanyRight] CHECK CONSTRAINT [FK_CompanyRight_Program]
GO


ALTER TABLE ProgramUser ADD [Company] 
[uniqueidentifier] NULL

ALTER TABLE [dbo].[ProgramUser]  WITH CHECK ADD  CONSTRAINT [FK_ProgramUser_Company] FOREIGN KEY([Company])
REFERENCES [dbo].[Company] ([CompanyGUID])
GO
