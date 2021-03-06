USE [master]
GO
/****** Object:  Database [DBBlogify]    Script Date: 14/01/2021 7:13:25 p. m. ******/
CREATE DATABASE [DBBlogify] COLLATE Modern_Spanish_CI_AS
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBBlogify].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBBlogify] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBBlogify] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBBlogify] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBBlogify] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBBlogify] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBBlogify] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBBlogify] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBBlogify] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBBlogify] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBBlogify] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBBlogify] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBBlogify] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBBlogify] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBBlogify] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBBlogify] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBBlogify] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBBlogify] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBBlogify] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBBlogify] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBBlogify] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBBlogify] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBBlogify] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBBlogify] SET RECOVERY FULL 
GO
ALTER DATABASE [DBBlogify] SET  MULTI_USER 
GO
ALTER DATABASE [DBBlogify] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBBlogify] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBBlogify] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBBlogify] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBBlogify] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBBlogify', N'ON'
GO
ALTER DATABASE [DBBlogify] SET QUERY_STORE = OFF
GO
USE [DBBlogify]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 14/01/2021 7:13:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [int] NOT NULL,
	[Title] [char](120) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Author] [char](20) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Data] [varchar](max) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Editor] [char](20) COLLATE Modern_Spanish_CI_AS NULL,
	[Edited] [smalldatetime] NULL,
	[Revision] [varchar](max) COLLATE Modern_Spanish_CI_AS NULL,
	[Status] [char](1) COLLATE Modern_Spanish_CI_AS NOT NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 14/01/2021 7:13:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [char](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 14/01/2021 7:13:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BlogId] [int] NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Data] [varchar](200) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Author] [char](20) COLLATE Modern_Spanish_CI_AS NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14/01/2021 7:13:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Username] [char](20) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Password] [varchar](512) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Name] [char](150) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Role] [char](20) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 

INSERT [dbo].[Blog] ([Id], [Category], [Title], [Created], [Author], [Data], [Editor], [Edited], [Revision], [Status]) VALUES (3, 2, N'1 MINUTE AMAZING RECIPES                                                                                                ', CAST(N'2021-01-13T08:03:00' AS SmallDateTime), N'blogger1            ', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras tristique, massa ultricies sagittis suscipit, nulla velit pharetra elit, non faucibus est odio ut dui. Quisque euismod libero sit amet turpis aliquam, a imperdiet augue aliquam. Quisque et urna tempor, pharetra elit at, eleifend leo. Vestibulum sed est arcu. Ut mollis felis felis. Integer sagittis, ipsum ac cursus sodales, lectus ex fringilla ex, non fermentum urna neque ut sapien. Curabitur a orci sit amet turpis tincidunt laoreet. Aliquam finibus commodo quam, vehicula efficitur mauris pharetra eget. Fusce ac varius sapien.

Curabitur ut ex vitae ex fringilla auctor a non quam. Nullam laoreet leo id libero fermentum hendrerit. Aenean rutrum, massa vestibulum mattis tempus, dolor risus interdum leo, vitae dapibus odio sem at augue. Morbi quis odio tristique, interdum magna ut, gravida mauris. Praesent ullamcorper condimentum tristique. Nulla varius, eros vel pretium pharetra, magna nisi ullamcorper nunc, vel sodales neque metus sed lectus. Nam interdum libero non tristique consectetur. Fusce tortor dui, tincidunt eget urna eu, bibendum scelerisque elit. Duis dignissim lectus et quam fringilla, aliquam lacinia ante eleifend.

Duis ut eros in dui elementum rhoncus. Sed eu ipsum eget urna tincidunt viverra. Curabitur fermentum non lectus eget venenatis. Quisque congue facilisis nisl blandit gravida. Nullam ullamcorper diam libero, ac aliquam erat imperdiet et. Nunc ac arcu at libero mollis tempus eu in turpis. Phasellus volutpat diam vitae est tristique consectetur. Sed vestibulum velit nec quam porta hendrerit. Vivamus pharetra, urna a sollicitudin rhoncus, erat justo pellentesque elit, et pellentesque magna nunc eu nibh. Nullam eget ex lectus. Fusce velit nunc, imperdiet nec erat quis, ultricies sagittis felis. Duis accumsan pulvinar facilisis. ', N'klacatt             ', CAST(N'2021-01-13T08:03:00' AS SmallDateTime), NULL, N'A')
INSERT [dbo].[Blog] ([Id], [Category], [Title], [Created], [Author], [Data], [Editor], [Edited], [Revision], [Status]) VALUES (5, 1, N'HOW TO BE A GOOD PARENT WITHOUT NOTICING                                                                                ', CAST(N'2021-01-13T08:10:00' AS SmallDateTime), N'blogger2            ', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras tristique, massa ultricies sagittis suscipit, nulla velit pharetra elit, non faucibus est odio ut dui. Quisque euismod libero sit amet turpis aliquam, a imperdiet augue aliquam. Quisque et urna tempor, pharetra elit at, eleifend leo. Vestibulum sed est arcu. Ut mollis felis felis. Integer sagittis, ipsum ac cursus sodales, lectus ex fringilla ex, non fermentum urna neque ut sapien. Curabitur a orci sit amet turpis tincidunt laoreet. Aliquam finibus commodo quam, vehicula efficitur mauris pharetra eget. Fusce ac varius sapien.

Curabitur ut ex vitae ex fringilla auctor a non quam. Nullam laoreet leo id libero fermentum hendrerit. Aenean rutrum, massa vestibulum mattis tempus, dolor risus interdum leo, vitae dapibus odio sem at augue. Morbi quis odio tristique, interdum magna ut, gravida mauris. Praesent ullamcorper condimentum tristique. Nulla varius, eros vel pretium pharetra, magna nisi ullamcorper nunc, vel sodales neque metus sed lectus. Nam interdum libero non tristique consectetur. Fusce tortor dui, tincidunt eget urna eu, bibendum scelerisque elit. Duis dignissim lectus et quam fringilla, aliquam lacinia ante eleifend.

Duis ut eros in dui elementum rhoncus. Sed eu ipsum eget urna tincidunt viverra. Curabitur fermentum non lectus eget venenatis. Quisque congue facilisis nisl blandit gravida. Nullam ullamcorper diam libero, ac aliquam erat imperdiet et. Nunc ac arcu at libero mollis tempus eu in turpis. Phasellus volutpat diam vitae est tristique consectetur. Sed vestibulum velit nec quam porta hendrerit. Vivamus pharetra, urna a sollicitudin rhoncus, erat justo pellentesque elit, et pellentesque magna nunc eu nibh. Nullam eget ex lectus. Fusce velit nunc, imperdiet nec erat quis, ultricies sagittis felis. Duis accumsan pulvinar facilisis. ', N'klacatt             ', CAST(N'2021-01-13T08:10:00' AS SmallDateTime), NULL, N'A')
INSERT [dbo].[Blog] ([Id], [Category], [Title], [Created], [Author], [Data], [Editor], [Edited], [Revision], [Status]) VALUES (6, 2, N'EASY!!! BBQ Pork Ribs (3 Hours)                                                                                         ', CAST(N'2021-01-14T10:08:00' AS SmallDateTime), N'blogger1            ', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras tristique, massa ultricies sagittis suscipit, nulla velit pharetra elit, non faucibus est odio ut dui. Quisque euismod libero sit amet turpis aliquam, a imperdiet augue aliquam. Quisque et urna tempor, pharetra elit at, eleifend leo. Vestibulum sed est arcu. Ut mollis felis felis. Integer sagittis, ipsum ac cursus sodales, lectus ex fringilla ex, non fermentum urna neque ut sapien. Curabitur a orci sit amet turpis tincidunt laoreet. Aliquam finibus commodo quam, vehicula efficitur mauris pharetra eget. Fusce ac varius sapien.

Curabitur ut ex vitae ex fringilla auctor a non quam. Nullam laoreet leo id libero fermentum hendrerit. Aenean rutrum, massa vestibulum mattis tempus, dolor risus interdum leo, vitae dapibus odio sem at augue. Morbi quis odio tristique, interdum magna ut, gravida mauris. Praesent ullamcorper condimentum tristique. Nulla varius, eros vel pretium pharetra, magna nisi ullamcorper nunc, vel sodales neque metus sed lectus. Nam interdum libero non tristique consectetur. Fusce tortor dui, tincidunt eget urna eu, bibendum scelerisque elit. Duis dignissim lectus et quam fringilla, aliquam lacinia ante eleifend.

Duis ut eros in dui elementum rhoncus. Sed eu ipsum eget urna tincidunt viverra. Curabitur fermentum non lectus eget venenatis. Quisque congue facilisis nisl blandit gravida. Nullam ullamcorper diam libero, ac aliquam erat imperdiet et. Nunc ac arcu at libero mollis tempus eu in turpis. Phasellus volutpat diam vitae est tristique consectetur. Sed vestibulum velit nec quam porta hendrerit. Vivamus pharetra, urna a sollicitudin rhoncus, erat justo pellentesque elit, et pellentesque magna nunc eu nibh. Nullam eget ex lectus. Fusce velit nunc, imperdiet nec erat quis, ultricies sagittis felis. Duis accumsan pulvinar facilisis.

Nunc finibus lectus sit amet urna lacinia lobortis. Aenean bibendum vulputate enim nec finibus. Pellentesque at nisi bibendum, imperdiet nibh eget, cursus dolor. Nam nec mattis libero. Suspendisse maximus eleifend viverra. Proin ac ipsum id urna consequat ullamcorper eget in nisi. Integer nec condimentum justo.

Suspendisse potenti. Praesent a mauris consectetur, gravida risus ac, consectetur ligula. Maecenas varius quam quis iaculis vehicula. Proin dictum tortor nunc, a fringilla leo tincidunt at. Cras sed nunc turpis. Sed aliquam ipsum non aliquam pretium. Vestibulum elementum velit ex, eu tristique eros pharetra eget. Aenean a purus sit amet nibh sodales tincidunt ut quis sem. Praesent aliquam consectetur risus non condimentum. Pellentesque rutrum, libero in pharetra facilisis, tortor ligula pretium felis, vel pellentesque nisi orci non est. 

Enjoy!', N'klacatt             ', CAST(N'2021-01-14T12:16:00' AS SmallDateTime), NULL, N'A')
INSERT [dbo].[Blog] ([Id], [Category], [Title], [Created], [Author], [Data], [Editor], [Edited], [Revision], [Status]) VALUES (8, 4, N'Why we need Family?                                                                                                     ', CAST(N'2021-01-14T12:29:00' AS SmallDateTime), N'blogger1            ', N'Curabitur ut ex vitae ex fringilla auctor a non quam. Nullam laoreet leo id libero fermentum hendrerit. Aenean rutrum, massa vestibulum mattis tempus, dolor risus interdum leo, vitae dapibus odio sem at augue. Morbi quis odio tristique, interdum magna ut, gravida mauris. Praesent ullamcorper condimentum tristique. Nulla varius, eros vel pretium pharetra, magna nisi ullamcorper nunc, vel sodales neque metus sed lectus. Nam interdum libero non tristique consectetur. Fusce tortor dui, tincidunt eget urna eu, bibendum scelerisque elit. Duis dignissim lectus et quam fringilla, aliquam lacinia ante eleifend.

Duis ut eros in dui elementum rhoncus. Sed eu ipsum eget urna tincidunt viverra. Curabitur fermentum non lectus eget venenatis. Quisque congue facilisis nisl blandit gravida. Nullam ullamcorper diam libero, ac aliquam erat imperdiet et. Nunc ac arcu at libero mollis tempus eu in turpis. Phasellus volutpat diam vitae est tristique consectetur. Sed vestibulum velit nec quam porta hendrerit. Vivamus pharetra, urna a sollicitudin rhoncus, erat justo pellentesque elit, et pellentesque magna nunc eu nibh. Nullam eget ex lectus. Fusce velit nunc, imperdiet nec erat quis, ultricies sagittis felis. Duis accumsan pulvinar facilisis.

Nunc finibus lectus sit amet urna lacinia lobortis. Aenean bibendum vulputate enim nec finibus. Pellentesque at nisi bibendum, imperdiet nibh eget, cursus dolor. Nam nec mattis libero. Suspendisse maximus eleifend viverra. Proin ac ipsum id urna consequat ullamcorper eget in nisi. Integer nec condimentum justo. ', N'klacatt             ', CAST(N'2021-01-14T12:30:00' AS SmallDateTime), NULL, N'A')
INSERT [dbo].[Blog] ([Id], [Category], [Title], [Created], [Author], [Data], [Editor], [Edited], [Revision], [Status]) VALUES (15, 4, N'Tips for getting out of friendzone                                                                                      ', CAST(N'2021-01-14T16:33:00' AS SmallDateTime), N'blogger2            ', N'Curabitur ut ex vitae ex fringilla auctor a non quam. Nullam laoreet leo id libero fermentum hendrerit. Aenean rutrum, massa vestibulum mattis tempus, dolor risus interdum leo, vitae dapibus odio sem at augue. Morbi quis odio tristique, interdum magna ut, gravida mauris. Praesent ullamcorper condimentum tristique. Nulla varius, eros vel pretium pharetra, magna nisi ullamcorper nunc, vel sodales neque metus sed lectus. Nam interdum libero non tristique consectetur. Fusce tortor dui, tincidunt eget urna eu, bibendum scelerisque elit. Duis dignissim lectus et quam fringilla, aliquam lacinia ante eleifend.

Duis ut eros in dui elementum rhoncus. Sed eu ipsum eget urna tincidunt viverra. Curabitur fermentum non lectus eget venenatis. Quisque congue facilisis nisl blandit gravida. Nullam ullamcorper diam libero, ac aliquam erat imperdiet et. Nunc ac arcu at libero mollis tempus eu in turpis. Phasellus volutpat diam vitae est tristique consectetur. Sed vestibulum velit nec quam porta hendrerit. Vivamus pharetra, urna a sollicitudin rhoncus, erat justo pellentesque elit, et pellentesque magna nunc eu nibh. Nullam eget ex lectus. Fusce velit nunc, imperdiet nec erat quis, ultricies sagittis felis. Duis accumsan pulvinar facilisis.

Nunc finibus lectus sit amet urna lacinia lobortis. Aenean bibendum vulputate enim nec finibus. Pellentesque at nisi bibendum, imperdiet nibh eget, cursus dolor. Nam nec mattis libero. Suspendisse maximus eleifend viverra. Proin ac ipsum id urna consequat ullamcorper eget in nisi. Integer nec condimentum justo. ', N'klacatt             ', CAST(N'2021-01-14T17:32:00' AS SmallDateTime), NULL, N'A')
INSERT [dbo].[Blog] ([Id], [Category], [Title], [Created], [Author], [Data], [Editor], [Edited], [Revision], [Status]) VALUES (19, 2, N'EASY!! Pork Belly                                                                                                       ', CAST(N'2021-01-14T17:54:00' AS SmallDateTime), N'blogger1            ', N'<div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras tristique, massa ultricies sagittis suscipit, nulla velit pharetra elit, non faucibus est odio ut dui. Quisque euismod libero sit amet turpis aliquam, a imperdiet augue aliquam. Quisque et urna tempor, pharetra elit at, eleifend leo. Vestibulum sed est arcu. Ut mollis felis felis. Integer sagittis, ipsum ac cursus sodales, lectus ex fringilla ex, non fermentum urna neque ut sapien. <br></div><div><br></div><div>Curabitur a orci sit amet turpis tincidunt laoreet. Aliquam finibus commodo quam, vehicula efficitur mauris pharetra eget. Fusce ac varius sapien.

Curabitur ut ex vitae ex fringilla auctor a non quam. Nullam laoreet leo id libero fermentum hendrerit. Aenean rutrum, massa vestibulum mattis tempus, dolor risus interdum leo, vitae dapibus odio sem at augue. Morbi quis odio tristique, interdum magna ut, gravida mauris. Praesent ullamcorper condimentum tristique. Nulla varius, eros vel pretium pharetra, magna nisi ullamcorper nunc, vel sodales neque metus sed lectus. Nam interdum libero non tristique consectetur. Fusce tortor dui, tincidunt eget urna eu, bibendum scelerisque elit. <br></div><div><br></div><div>Duis dignissim lectus et quam fringilla, aliquam lacinia ante eleifend.

Duis ut eros in dui elementum rhoncus. Sed eu ipsum eget urna tincidunt viverra. Curabitur fermentum non lectus eget venenatis. Quisque congue facilisis nisl blandit gravida. Nullam ullamcorper diam libero, ac aliquam erat imperdiet et. Nunc ac arcu at libero mollis tempus eu in turpis. Phasellus volutpat diam vitae est tristique consectetur. Sed vestibulum velit nec quam porta hendrerit. Vivamus pharetra, urna a sollicitudin rhoncus, erat justo pellentesque elit, et pellentesque magna nunc eu nibh. Nullam eget ex lectus. Fusce velit nunc, imperdiet nec erat quis, ultricies sagittis felis. <br></div><div><br></div><div><b>Duis accumsan pulvinar facilisis.</b>

Nunc finibus lectus sit amet urna lacinia lobortis. Aenean bibendum vulputate enim nec finibus. Pellentesque at nisi bibendum, imperdiet nibh eget, cursus dolor. Nam nec mattis libero. Suspendisse maximus eleifend viverra. Proin ac ipsum id urna consequat ullamcorper eget in nisi. Integer nec condimentum justo.

Suspendisse potenti. Praesent a mauris consectetur, gravida risus ac, consectetur ligula. Maecenas varius quam quis iaculis vehicula. Proin dictum tortor nunc, a fringilla leo tincidunt at. Cras sed nunc turpis. Sed aliquam ipsum non aliquam pretium. Vestibulum elementum velit ex, eu tristique eros pharetra eget. Aenean a purus sit amet nibh sodales tincidunt ut quis sem. Praesent aliquam consectetur risus non condimentum. Pellentesque rutrum, libero in pharetra facilisis, tortor ligula pretium felis, vel pellentesque nisi orci non est. 

Enjoy!</div>', N'klacatt             ', CAST(N'2021-01-14T18:07:00' AS SmallDateTime), NULL, N'P')
SET IDENTITY_INSERT [dbo].[Blog] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Parenthood                                        ')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Cooking                                           ')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Landscaping                                       ')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'Family                                            ')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (5, N'Relationships                                     ')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (2, 3, CAST(N'2021-01-13T13:05:00' AS SmallDateTime), N'Great!', N'Anonymous           ')
INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (3, 3, CAST(N'2021-01-13T13:05:00' AS SmallDateTime), N'Awsome article', N'Anonymous           ')
INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (4, 3, CAST(N'2021-01-13T13:05:00' AS SmallDateTime), N'Nice, just what I needed', N'Anonymous           ')
INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (5, 3, CAST(N'2021-01-13T15:18:00' AS SmallDateTime), N'New comment, just for testing', N'Anonymous           ')
INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (6, 3, CAST(N'2021-01-13T15:38:00' AS SmallDateTime), N'New comment just for testing', N'Anonymous           ')
INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (7, 3, CAST(N'2021-01-13T15:39:00' AS SmallDateTime), N'Another test just to se if there is a problem.', N'Anonymous           ')
INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (8, 5, CAST(N'2021-01-13T18:31:00' AS SmallDateTime), N'Nice the first comment for me.', N'klacatt             ')
INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (9, 6, CAST(N'2021-01-14T12:17:00' AS SmallDateTime), N'oooh ray!!! comments and more comments', N'klacatt             ')
INSERT [dbo].[Comment] ([Id], [BlogId], [Created], [Data], [Author]) VALUES (10, 8, CAST(N'2021-01-14T12:30:00' AS SmallDateTime), N'Family is important', N'Anonymous           ')
SET IDENTITY_INSERT [dbo].[Comment] OFF
INSERT [dbo].[User] ([Username], [Password], [Name], [Role], [IsActive]) VALUES (N'blogger1            ', N'Back4g00d', N'Blogger 1                                                                                                                                             ', N'Writer              ', 1)
INSERT [dbo].[User] ([Username], [Password], [Name], [Role], [IsActive]) VALUES (N'blogger2            ', N'Back4g00d', N'Blogger 2                                                                                                                                             ', N'Writer              ', 1)
INSERT [dbo].[User] ([Username], [Password], [Name], [Role], [IsActive]) VALUES (N'klacatt             ', N'Back4g00d', N'Kade Lacatt                                                                                                                                           ', N'Editor              ', 1)
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Author] FOREIGN KEY([Author])
REFERENCES [dbo].[User] ([Username])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Author]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Category] FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Category]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Editor] FOREIGN KEY([Editor])
REFERENCES [dbo].[User] ([Username])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Editor]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Blog] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blog] ([Id])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Blog]
GO
USE [master]
GO
ALTER DATABASE [DBBlogify] SET  READ_WRITE 
GO
