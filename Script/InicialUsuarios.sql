USE [Padaria]
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([Id], [Nome], [Email], [DataNascimento], [PerfilUsuario], [Senha]) VALUES (1, N'Administrador', N'administrador@email', CAST(N'1993-08-02T00:00:00.0000000' AS DateTime2), 0, N'1234')
GO
INSERT [dbo].[Usuario] ([Id], [Nome], [Email], [DataNascimento], [PerfilUsuario], [Senha]) VALUES (2, N'Padeiro', N'padeiro@email', CAST(N'2020-03-21T00:00:00.0000000' AS DateTime2), 1, N'1234')
GO
INSERT [dbo].[Usuario] ([Id], [Nome], [Email], [DataNascimento], [PerfilUsuario], [Senha]) VALUES (3, N'Estoquista', N'estoquista@email', CAST(N'2020-03-21T00:00:00.0000000' AS DateTime2), 2, N'1234')
GO
INSERT [dbo].[Usuario] ([Id], [Nome], [Email], [DataNascimento], [PerfilUsuario], [Senha]) VALUES (4, N'Vendedor', N'vendedor@email', CAST(N'2020-03-21T00:00:00.0000000' AS DateTime2), 3, N'1234')
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
