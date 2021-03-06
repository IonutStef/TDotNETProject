USE [master]
GO
/****** Object:  Database [ProiectDotNET]    Script Date: 15-Apr-16 3:45:21 PM ******/
CREATE DATABASE [ProiectDotNET]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProiectDotNET', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ProiectDotNET.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProiectDotNET_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ProiectDotNET_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ProiectDotNET] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProiectDotNET].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProiectDotNET] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProiectDotNET] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProiectDotNET] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProiectDotNET] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProiectDotNET] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProiectDotNET] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProiectDotNET] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProiectDotNET] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProiectDotNET] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProiectDotNET] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProiectDotNET] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProiectDotNET] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProiectDotNET] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProiectDotNET] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProiectDotNET] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProiectDotNET] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProiectDotNET] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProiectDotNET] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProiectDotNET] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProiectDotNET] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProiectDotNET] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProiectDotNET] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProiectDotNET] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProiectDotNET] SET  MULTI_USER 
GO
ALTER DATABASE [ProiectDotNET] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProiectDotNET] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProiectDotNET] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProiectDotNET] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ProiectDotNET] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ProiectDotNET]
GO
/****** Object:  Table [dbo].[Chapter]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chapter](
	[ChapterId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[SubjectId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Chapter] PRIMARY KEY CLUSTERED 
(
	[ChapterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChapterTest]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChapterTest](
	[ChapterId] [uniqueidentifier] NOT NULL,
	[TestId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ChapterTest_1] PRIMARY KEY CLUSTERED 
(
	[ChapterId] ASC,
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Examination]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Examination](
	[ExaminationId] [uniqueidentifier] NOT NULL,
	[TestId] [uniqueidentifier] NOT NULL,
	[Date] [date] NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_Examination_1] PRIMARY KEY CLUSTERED 
(
	[ExaminationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Question]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionId] [uniqueidentifier] NOT NULL,
	[Requirement] [varchar](max) NOT NULL,
	[Justification] [varchar](max) NULL,
	[ChapterId] [uniqueidentifier] NOT NULL,
	[Duplicate] [bit] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Response]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Response](
	[ResponseId] [uniqueidentifier] NOT NULL,
	[Answer] [varchar](max) NOT NULL,
	[Correct] [bit] NOT NULL,
	[QuestionId] [uniqueidentifier] NOT NULL,
	[Duplicate] [bit] NULL,
 CONSTRAINT [PK_Response] PRIMARY KEY CLUSTERED 
(
	[ResponseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SolvedTest]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolvedTest](
	[SolvedTestId] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[TestId] [uniqueidentifier] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Punctaj] [int] NOT NULL,
	[Pending] [bit] NULL,
 CONSTRAINT [PK_SolvedTest] PRIMARY KEY CLUSTERED 
(
	[SolvedTestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentExamination]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentExamination](
	[StudentId] [uniqueidentifier] NOT NULL,
	[ExaminationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_StudentExamination] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[ExaminationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentSubject]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSubject](
	[StudentId] [uniqueidentifier] NOT NULL,
	[SubjectId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_StudentSubject_1] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subject]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [uniqueidentifier] NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Test]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Test](
	[TestId] [uniqueidentifier] NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
	[TestReferencedId] [uniqueidentifier] NULL,
	[TimeToSolve] [int] NOT NULL,
	[NoQuestions] [int] NOT NULL,
	[Punctaj] [int] NOT NULL,
	[Duplicate] [bit] NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TestQuestion]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestQuestion](
	[TestId] [uniqueidentifier] NOT NULL,
	[QuestionId] [uniqueidentifier] NOT NULL,
	[Punctaj] [int] NOT NULL,
	[Duplicate] [bit] NULL,
	[TestQuestionId] [uniqueidentifier] NOT NULL,
	[TestQuestionReferencedId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TestQuestion_1] PRIMARY KEY CLUSTERED 
(
	[TestQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 15-Apr-16 3:45:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[UserType] [int] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Chapter_UniqueTitle]    Script Date: 15-Apr-16 3:45:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Chapter_UniqueTitle] ON [dbo].[Chapter]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Examination]    Script Date: 15-Apr-16 3:45:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Examination] ON [dbo].[Examination]
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Subject_UniqueTitle]    Script Date: 15-Apr-16 3:45:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Subject_UniqueTitle] ON [dbo].[Subject]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TestQuestion]    Script Date: 15-Apr-16 3:45:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TestQuestion] ON [dbo].[TestQuestion]
(
	[TestId] ASC,
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TestQuestion_1]    Script Date: 15-Apr-16 3:45:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_TestQuestion_1] ON [dbo].[TestQuestion]
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_User_UniqueEmail]    Script Date: 15-Apr-16 3:45:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_UniqueEmail] ON [dbo].[User]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_User_UniqueStudent]    Script Date: 15-Apr-16 3:45:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_UniqueStudent] ON [dbo].[User]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chapter]  WITH CHECK ADD  CONSTRAINT [FK_Chapter_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Chapter] CHECK CONSTRAINT [FK_Chapter_Subject]
GO
ALTER TABLE [dbo].[ChapterTest]  WITH CHECK ADD  CONSTRAINT [FK_ChapterTest_Chapter] FOREIGN KEY([ChapterId])
REFERENCES [dbo].[Chapter] ([ChapterId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChapterTest] CHECK CONSTRAINT [FK_ChapterTest_Chapter]
GO
ALTER TABLE [dbo].[ChapterTest]  WITH CHECK ADD  CONSTRAINT [FK_ChapterTest_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([TestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChapterTest] CHECK CONSTRAINT [FK_ChapterTest_Test]
GO
ALTER TABLE [dbo].[Examination]  WITH CHECK ADD  CONSTRAINT [FK_Examination_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([TestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Examination] CHECK CONSTRAINT [FK_Examination_Test]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Chapter] FOREIGN KEY([ChapterId])
REFERENCES [dbo].[Chapter] ([ChapterId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Chapter]
GO
ALTER TABLE [dbo].[Response]  WITH CHECK ADD  CONSTRAINT [FK_Response_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Response] CHECK CONSTRAINT [FK_Response_Question]
GO
ALTER TABLE [dbo].[SolvedTest]  WITH CHECK ADD  CONSTRAINT [FK_SolvedTest_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolvedTest] CHECK CONSTRAINT [FK_SolvedTest_Student]
GO
ALTER TABLE [dbo].[SolvedTest]  WITH CHECK ADD  CONSTRAINT [FK_SolvedTest_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([TestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolvedTest] CHECK CONSTRAINT [FK_SolvedTest_Test]
GO
ALTER TABLE [dbo].[StudentExamination]  WITH CHECK ADD  CONSTRAINT [FK_StudentExamination_Examination] FOREIGN KEY([ExaminationId])
REFERENCES [dbo].[Examination] ([ExaminationId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentExamination] CHECK CONSTRAINT [FK_StudentExamination_Examination]
GO
ALTER TABLE [dbo].[StudentExamination]  WITH CHECK ADD  CONSTRAINT [FK_StudentExamination_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentExamination] CHECK CONSTRAINT [FK_StudentExamination_Student]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubject_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [FK_StudentSubject_Student]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubject_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [FK_StudentSubject_Subject]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_Test] FOREIGN KEY([TestReferencedId])
REFERENCES [dbo].[Test] ([TestId])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_Test]
GO
ALTER TABLE [dbo].[TestQuestion]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestion_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TestQuestion] CHECK CONSTRAINT [FK_TestQuestion_Question]
GO
ALTER TABLE [dbo].[TestQuestion]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestion_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([TestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TestQuestion] CHECK CONSTRAINT [FK_TestQuestion_Test]
GO
ALTER TABLE [dbo].[TestQuestion]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestion_TestQuestion] FOREIGN KEY([TestQuestionReferencedId])
REFERENCES [dbo].[TestQuestion] ([TestQuestionId])
GO
ALTER TABLE [dbo].[TestQuestion] CHECK CONSTRAINT [FK_TestQuestion_TestQuestion]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Student]
GO
USE [master]
GO
ALTER DATABASE [ProiectDotNET] SET  READ_WRITE 
GO
