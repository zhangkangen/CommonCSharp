


use AuthApp
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SysModule](
    [Id] [varchar](50) NOT NULL, --id
    [Name] [varchar](200) NOT NULL, --模块名称
    [EnglishName] [varchar](200) NULL, --英文名称（防止将来国际化）
    [ParentId] [varchar](50) NULL,--上级ID这是一个tree
    [Url] [varchar](200) NULL,--链接
    [Iconic] [varchar](200) NULL,--图标，用于链接图标，或tab页图标
    [Sort] [int] NULL,--排序
    [Remark] [varchar](4000) NULL,--说明
    [State] [bit] NULL, --状态
    [CreatePerson] [varchar](200) NULL, --创建人
    [CreateTime] [datetime] NULL,--创建事件
    [IsLast] [bit] NOT NULL, --是否是最后一项
    [Version] [timestamp] NULL, --版本
 CONSTRAINT [PK_SysModule] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SysModule]  WITH NOCHECK ADD  CONSTRAINT [FK_SysModule_SysModule] FOREIGN KEY([ParentId])
REFERENCES [dbo].[SysModule] ([Id])
GO

ALTER TABLE [dbo].[SysModule] NOCHECK CONSTRAINT [FK_SysModule_SysModule]
GO