USE [master]
GO
/****** Object:  Database [Landing2DB]    Script Date: 23/8/2023 09:02:13 ******/
CREATE DATABASE [Landing2DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Landing2DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Landing2DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Landing2DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Landing2DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Landing2DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Landing2DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Landing2DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Landing2DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Landing2DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Landing2DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Landing2DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Landing2DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Landing2DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Landing2DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Landing2DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Landing2DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Landing2DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Landing2DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Landing2DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Landing2DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Landing2DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Landing2DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Landing2DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Landing2DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Landing2DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Landing2DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Landing2DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Landing2DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Landing2DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Landing2DB] SET  MULTI_USER 
GO
ALTER DATABASE [Landing2DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Landing2DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Landing2DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Landing2DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Landing2DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Landing2DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Landing2DB', N'ON'
GO
ALTER DATABASE [Landing2DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [Landing2DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Landing2DB]
GO
/****** Object:  Table [dbo].[Contactos]    Script Date: 23/8/2023 09:02:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contactos](
	[IdContacto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Mensaje] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Contactos] PRIMARY KEY CLUSTERED 
(
	[IdContacto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[IdEmpresa] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Direccion] [varchar](100) NULL,
	[Telefono] [varchar](50) NULL,
	[Localidad] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Descripcion] [varchar](500) NULL,
	[RazónSocial] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Footer]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Footer](
	[IdFooter] [int] IDENTITY(1,1) NOT NULL,
	[DescripcionDireccion] [varchar](100) NULL,
	[RutaDireccion] [varchar](max) NULL,
	[Telefono] [varchar](50) NULL,
	[RutaTelefono] [varchar](max) NULL,
	[DescripcionEmail] [varchar](50) NULL,
	[RutaEmail] [varchar](max) NULL,
	[Localidad] [varchar](50) NULL,
 CONSTRAINT [PK_Footer] PRIMARY KEY CLUSTERED 
(
	[IdFooter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Galeria]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Galeria](
	[IdGaleria] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NULL,
	[ColorFuente] [varchar](100) NULL,
	[ColorFondo] [varchar](100) NULL,
 CONSTRAINT [PK_Galeria] PRIMARY KEY CLUSTERED 
(
	[IdGaleria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GaleriaImagenes]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GaleriaImagenes](
	[IdGaleriaImagen] [int] IDENTITY(1,1) NOT NULL,
	[IdGaleria] [int] NOT NULL,
	[RutaImagen] [varchar](max) NULL,
	[Descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_GaleriaImagenes] PRIMARY KEY CLUSTERED 
(
	[IdGaleriaImagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Header]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Header](
	[IdHeader] [int] IDENTITY(1,1) NOT NULL,
	[RutaLogo] [varchar](max) NULL,
	[Nombre] [varchar](50) NULL,
	[ColorFuente] [varchar](50) NULL,
	[ColorFondo] [varchar](50) NULL,
 CONSTRAINT [PK_Header] PRIMARY KEY CLUSTERED 
(
	[IdHeader] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Landings]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Landings](
	[IdLanding] [int] IDENTITY(1,1) NOT NULL,
	[IdHeader] [int] NOT NULL,
	[IdPortada] [int] NOT NULL,
	[IdSeccionServicio] [int] NOT NULL,
	[IdGaleria] [int] NOT NULL,
	[IdTestimonio] [int] NOT NULL,
	[IdContacto] [int] NOT NULL,
	[IdFooter] [int] NOT NULL,
	[IdEmpresa] [int] NOT NULL,
 CONSTRAINT [PK_Landings] PRIMARY KEY CLUSTERED 
(
	[IdLanding] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PortadaImagen]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortadaImagen](
	[IdPortada] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](100) NULL,
	[RutaImagen] [varchar](max) NULL,
	[ColorFuente] [varchar](50) NULL,
 CONSTRAINT [PK_PortadaImagen] PRIMARY KEY CLUSTERED 
(
	[IdPortada] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RedesSociales]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RedesSociales](
	[IdRedSocial] [int] NOT NULL,
	[IdFooter] [int] NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Ruta] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeccionServicios]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeccionServicios](
	[IdSeccionServicio] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](100) NULL,
	[RutaPDF] [varchar](max) NULL,
 CONSTRAINT [PK_SeccionServicios] PRIMARY KEY CLUSTERED 
(
	[IdSeccionServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicioImagenes]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicioImagenes](
	[IdServicioImagen] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[RutaImagen] [varchar](max) NULL,
	[IdSeccionServicio] [int] NOT NULL,
 CONSTRAINT [PK_ServicioImagenes] PRIMARY KEY CLUSTERED 
(
	[IdServicioImagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Testimonios]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Testimonios](
	[IdTestimonio] [int] IDENTITY(1,1) NOT NULL,
	[NombreCliente] [varchar](100) NOT NULL,
	[RutaImagen] [varchar](max) NULL,
	[Parrafo] [varchar](500) NOT NULL,
	[Titulo] [varchar](50) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Testimonios] PRIMARY KEY CLUSTERED 
(
	[IdTestimonio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Contactos] ON 

INSERT [dbo].[Contactos] ([IdContacto], [Nombre], [Telefono], [Email], [Mensaje]) VALUES (1, N'string', N'string', N'string', N'string')
SET IDENTITY_INSERT [dbo].[Contactos] OFF
GO
SET IDENTITY_INSERT [dbo].[Header] ON 

INSERT [dbo].[Header] ([IdHeader], [RutaLogo], [Nombre], [ColorFuente], [ColorFondo]) VALUES (1, N'qweqwe', N'qweqwe', N'qwew', N'ferefr')
SET IDENTITY_INSERT [dbo].[Header] OFF
GO
SET IDENTITY_INSERT [dbo].[PortadaImagen] ON 

INSERT [dbo].[PortadaImagen] ([IdPortada], [Titulo], [RutaImagen], [ColorFuente]) VALUES (1, N'nuevo titulo', N'image', N'32iffrokwl')
SET IDENTITY_INSERT [dbo].[PortadaImagen] OFF
GO
SET IDENTITY_INSERT [dbo].[SeccionServicios] ON 

INSERT [dbo].[SeccionServicios] ([IdSeccionServicio], [Titulo], [RutaPDF]) VALUES (1, N'Nueva sección modificada', N'nuevo PDF')
SET IDENTITY_INSERT [dbo].[SeccionServicios] OFF
GO
SET IDENTITY_INSERT [dbo].[ServicioImagenes] ON 

INSERT [dbo].[ServicioImagenes] ([IdServicioImagen], [Descripcion], [RutaImagen], [IdSeccionServicio]) VALUES (1, N'primera imagen', N'image.png', 1)
INSERT [dbo].[ServicioImagenes] ([IdServicioImagen], [Descripcion], [RutaImagen], [IdSeccionServicio]) VALUES (2, N'imagen 2', N'image 2', 1)
SET IDENTITY_INSERT [dbo].[ServicioImagenes] OFF
GO
SET IDENTITY_INSERT [dbo].[Testimonios] ON 

INSERT [dbo].[Testimonios] ([IdTestimonio], [NombreCliente], [RutaImagen], [Parrafo], [Titulo], [Activo]) VALUES (1, N'gonzalo', N'wer', N'wer', N'wer', 1)
SET IDENTITY_INSERT [dbo].[Testimonios] OFF
GO
/****** Object:  StoredProcedure [dbo].[Contactos_Agregar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Contactos_Agregar]

@Nombre VARCHAR(50),
@Telefono varchar(50),
@Email varchar(50),
@Mensaje varchar(50)

AS

INSERT INTO Contactos
VALUES(@Nombre, @Telefono,@Email,@Mensaje)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[Contactos_Modificar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Contactos_Modificar]

@IdContacto INT,
@Nombre VARCHAR(50),
@Telefono VARCHAR(50),
@Email VARCHAR(50),
@Mensaje VARCHAR(50)

AS

UPDATE Contactos
SET Nombre = @Nombre, @Telefono = Telefono, @Email = Email, @Mensaje = Mensaje
WHERE IdContacto = @IdContacto

SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[Contactos_ObtenerContactoPorId]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Contactos_ObtenerContactoPorId]

@IdContacto INT

AS

SELECT *
FROM Contactos
WHERE IdContacto = @IdContacto
GO
/****** Object:  StoredProcedure [dbo].[Contactos_ObtenerContactos]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Contactos_ObtenerContactos]
as
select*
from Contactos
GO
/****** Object:  StoredProcedure [dbo].[Contactos_ObtenerContactosPorNombre]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Contactos_ObtenerContactosPorNombre]

@Nombre VARCHAR(50)

AS

SELECT *
FROM Contactos
WHERE Nombre = @Nombre
GO
/****** Object:  StoredProcedure [dbo].[Header_Agregar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Header_Agregar]
@RutaLogo varchar(MAX),
@Nombre varchar(50),
@ColorFuente varchar(50),
@ColorFondo varchar(50)
AS
insert into Header
values (@RutaLogo,@Nombre,@ColorFuente,@ColorFondo)
SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[Header_Modificar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Header_Modificar]
@IdHeader int,
@RutaLogo varchar(MAX),
@Nombre varchar(50),
@ColorFuente varchar(50),
@ColorFondo varchar(50)

as

update Header
set RutaLogo =@RutaLogo,Nombre=@Nombre,ColorFuente=@ColorFuente,ColorFondo=@ColorFondo
where IdHeader=@IdHeader
SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[Header_ObtenerHeader]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Header_ObtenerHeader]

AS

Select *

From  Header
GO
/****** Object:  StoredProcedure [dbo].[Header_ObtenerHeaderPorId]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Header_ObtenerHeaderPorId]

@IdHeader INT

AS
select *
FROM Header
WHERE IdHeader = @IdHeader
GO
/****** Object:  StoredProcedure [dbo].[PortadaImagen_Modificar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[PortadaImagen_Modificar]
@IdPortada int,
@Titulo varchar(100),
@RutaImagen varchar(MAX),
@ColorFuente varchar(50)
as
update PortadaImagen
set Titulo = @Titulo, RutaImagen = @RutaImagen, ColorFuente = @ColorFuente
where IdPortada = @IdPortada
SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[PortadaImagen_Obtener]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[PortadaImagen_Obtener]
as
select *
from PortadaImagen
GO
/****** Object:  StoredProcedure [dbo].[PortadaImagen_ObtenerPorId]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[PortadaImagen_ObtenerPorId]
@IdPortada int
as
Select *
From PortadaImagen
where IdPortada = @IdPortada
GO
/****** Object:  StoredProcedure [dbo].[SeccionServicio_Modificar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SeccionServicio_Modificar]
@IdSeccionServicio int,
@Titulo varchar (100),
@RutaPDF varchar(MAX)
as
update SeccionServicios
set Titulo = @Titulo, RutaPDF = @RutaPDF
where IdSeccionServicio = @IdSeccionServicio
SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[SeccionServicio_ObtenerPorId]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SeccionServicio_ObtenerPorId]
@IdSeccionServicio int
as
select *
From SeccionServicios
where IdSeccionServicio = @IdSeccionServicio
GO
/****** Object:  StoredProcedure [dbo].[SeccionServicios_Obtener]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SeccionServicios_Obtener]
as
Select*
from SeccionServicios
GO
/****** Object:  StoredProcedure [dbo].[ServicioImagen_Agregar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ServicioImagen_Agregar]

@Descripcion varchar(100),
@RutaImagen varchar(MAX),
@IdSeccionServicio int
as 
insert into ServicioImagenes
values(@Descripcion,@RutaImagen,@IdSeccionServicio)
SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[ServicioImagen_Obtener]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ServicioImagen_Obtener]
as
select I.IdServicioImagen, I.Descripcion, I.RutaImagen, S.IdSeccionServicio, S.Titulo
from ServicioImagenes I JOIN SeccionServicios S on I.IdSeccionServicio = S.IdSeccionServicio
GO
/****** Object:  StoredProcedure [dbo].[ServicioImagen_ObtenerPorId]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ServicioImagen_ObtenerPorId]
@IdServicioImagen int
as
select I.IdServicioImagen, I.Descripcion, I.RutaImagen, S.IdSeccionServicio, S.Titulo
from ServicioImagenes I JOIN SeccionServicios S on I.IdSeccionServicio = S.IdSeccionServicio
where I.IdServicioImagen = @IdServicioImagen
GO
/****** Object:  StoredProcedure [dbo].[ServicioImagen_ObtenerPorNombre]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ServicioImagen_ObtenerPorNombre]
@Descripcion varchar(100)
as
select I.IdServicioImagen, I.Descripcion, I.RutaImagen, S.IdSeccionServicio, S.Titulo
from ServicioImagenes I JOIN SeccionServicios S on I.IdSeccionServicio = S.IdSeccionServicio
where Descripcion = @Descripcion
GO
/****** Object:  StoredProcedure [dbo].[ServicioImagene_Modificar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ServicioImagene_Modificar]
@IdServicioImagen int,
@Descripcion varchar(100),
@RutaImagen varchar(MAX),
@IdSeccionServicio int
as 
update ServicioImagenes
set Descripcion = @Descripcion, RutaImagen = @RutaImagen, IdSeccionServicio = @IdSeccionServicio
where IdServicioImagen = @IdServicioImagen
SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[Testimonio_Agregar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Testimonio_Agregar]
@NombreCliente varchar(100),
@RutaImagen varchar(max),
@Parrafo varchar(500),
@Titulo varchar(50),
@Activo bit
AS
insert into Testimonios
values (@NombreCliente,@RutaImagen,@Parrafo,@Titulo,@Activo)
SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[Testimonio_Modificar]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Testimonio_Modificar]
@IdTestimonio int,
@NombreCliente varchar(100),
@RutaImagen varchar(max),
@Parrafo varchar(500),
@Titulo varchar(50),
@Activo bit

as
update Testimonios
set NombreCliente = @NombreCliente, RutaImagen = @RutaImagen, Parrafo = @Parrafo, Titulo=@Titulo, Activo = @Activo
where IdTestimonio = @IdTestimonio
SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[Testimonio_Obtener]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Testimonio_Obtener]
as
Select *
From Testimonios
GO
/****** Object:  StoredProcedure [dbo].[Testimonio_ObtenerPorId]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Testimonio_ObtenerPorId]
@IdTestimonio int
as
select *
from Testimonios
where IdTestimonio = @IdTestimonio
GO
/****** Object:  StoredProcedure [dbo].[Testimonio_ObtenerPorNombre]    Script Date: 23/8/2023 09:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Testimonio_ObtenerPorNombre]
@NombreCliente varchar(100)
as
select *
from Testimonios
where NombreCliente = @NombreCliente
GO
USE [master]
GO
ALTER DATABASE [Landing2DB] SET  READ_WRITE 
GO
