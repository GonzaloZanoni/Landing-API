USE [master]
GO
/****** Object:  Database [Landing2DB]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  Table [dbo].[Contactos]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  Table [dbo].[Header]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  StoredProcedure [dbo].[Contactos_Agregar]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  StoredProcedure [dbo].[Contactos_Modificar]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  StoredProcedure [dbo].[Contactos_ObtenerContactoPorId]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  StoredProcedure [dbo].[Contactos_ObtenerContactosPorNombre]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  StoredProcedure [dbo].[Header_Agregar]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  StoredProcedure [dbo].[Header_Modificar]    Script Date: 17/8/2023 19:36:40 ******/
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
/****** Object:  StoredProcedure [dbo].[Header_ObtenerHeader]    Script Date: 17/8/2023 19:36:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Header_ObtenerHeader]

AS

Select *

From  Header
GO
/****** Object:  StoredProcedure [dbo].[Header_ObtenerHeaderPorId]    Script Date: 17/8/2023 19:36:40 ******/
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
USE [master]
GO
ALTER DATABASE [Landing2DB] SET  READ_WRITE 
GO
