USE [SiteDemoV5_New]
GO
/****** Object:  UserDefinedTableType [dbo].[IntListType]    Script Date: 02/12/2024 14:45:49 ******/
CREATE TYPE [dbo].[IntListType] AS TABLE(
	[ID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[UserAtms]    Script Date: 02/12/2024 14:45:49 ******/
CREATE TYPE [dbo].[UserAtms] AS TABLE(
	[User_Id] [nvarchar](128) NULL,
	[Atm_Id] [nvarchar](128) NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[BeforSop]    Script Date: 02/12/2024 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery3.sql|7|0|C:\Users\AtmView\AppData\Local\Temp\~vs9247.sql
CREATE FUNCTION [dbo].[BeforSop] (@texte nvarchar(max))
RETURNS bigint
as begin
  Declare @Amount bigint
  Declare @number1 int
  Declare @number2 int
  Declare @Txt nvarchar(max)
  Declare @Txt2 nvarchar(max) 
  Declare @Txt3 nvarchar(max)
  Declare @i int
  SET @Amount=0
  set @Txt2 =(SELECT SUBSTRING(@texte, CHARINDEX('CASH COUNTERS BEFORE SOP',@texte) + LEN('CASH COUNTERS BEFORE SOP'), LEN(@texte)))
  set @Txt3 =(SELECT LEFT(@Txt2, CHARINDEX('RETRACTS', @Txt2)-1))
  DECLARE mycurs3 CURSOR
  FOR select value from STRING_SPLIT (@Txt3,' ')
  open mycurs3
  FETCH NEXT FROM mycurs3 INTO @Txt

While @@FETCH_STATUS = 0
begin
	while ((Try_Parse(@Txt as int) is null ) and  @@FETCH_STATUS = 0)
	begin 
	if(CHARINDEX('*',@Txt)!=0)
	begin
	break
	end
	fetch Next from mycurs3 INTO @Txt
	end 
	set @number1=TRY_PARSE(@Txt as int)
	fetch Next from mycurs3 INTO @Txt
	while ((Try_Parse(@Txt as int) is null ) and  @@FETCH_STATUS = 0)
	begin 
	if(CHARINDEX('*',@Txt)!=0)
	begin
	break
	end
	fetch Next from mycurs3 INTO @Txt
	end 
	if(CHARINDEX('*',@Txt)!=0 )
	begin
    set @Txt=(select REPLACE(@Txt, '*', ''))
	end
	set @number2=(select Try_Parse(@Txt as int))
	set @i=(@number1*@number2)
	if(@i is not null)
	begin
    SET @Amount=sum(@Amount+@i)
	end
	
    fetch Next from mycurs3 INTO @Txt

	
END

Deallocate mycurs3

return @Amount
end
GO
/****** Object:  UserDefinedFunction [dbo].[ExtractAmountForNCR]    Script Date: 02/12/2024 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Batch submitted through debugger: SQLQuery3.sql|7|0|C:\Users\AtmView\AppData\Local\Temp\~vs9247.sql
CREATE FUNCTION [dbo].[ExtractAmountForNCR] (@tags nvarchar(max))
RETURNS bigint
as begin
Declare @Txt nvarchar(max)
Declare @Txt1 nvarchar(max)
Declare @Txt1p nvarchar(max)
Declare @Txt2 nvarchar(max)
Declare @Txt2p nvarchar(max)
Declare @Txt3 nvarchar(max)
Declare @Txt3p nvarchar(max)
Declare @Txt3pp nvarchar(max)
Declare @Txt4 nvarchar(max)
Declare @Txt4p nvarchar(max)
dECLARE @Amount bigint
Declare @i int
Declare @k int
set @i=0
set @k=0
 set @Txt =cast( (select right(@tags, len(@tags) - charindex('CASH ADDED', @tags))) as nvarchar(max)) 
 if(( select  (LEN(@tags)-LEN(REPLACE(@tags,'CASH ADDED','')))/LEN('CASH ADDED'))=1)
 begin 
  set @Txt1 =((SELECT SUBSTRING(@Txt, CHARINDEX('TYPE 1',@Txt) ,14)))
  set @Txt1p=(select right(@Txt1, len(@Txt1) - charindex('=', @Txt1)))
  set @Txt2 =(SELECT SUBSTRING(@Txt, CHARINDEX('TYPE 2',@Txt) ,14))
  set @Txt2p=(select right(@Txt2, len(@Txt2) - charindex('=', @Txt2)))
  set @Txt3 =(SELECT SUBSTRING(@Txt, CHARINDEX('TYPE 3',@Txt) ,14))
  set @Txt3p=(select right(@Txt3, len(@Txt3) - charindex('=', @Txt3)))
  set @Txt4 =(SELECT SUBSTRING(@Txt, CHARINDEX('TYPE 4',@Txt) ,14))
  set @Txt4p=(select right(@Txt4, len(@Txt4) - charindex('=', @Txt4)))
  set @Amount=((Try_Parse(@Txt1p as int)*100)+(Try_Parse(@Txt2p as int)*200)+(Try_Parse(@Txt3p as int)*100)+(Try_Parse(@Txt4p as int)*200))
 end
 else 
begin
  set @i=( select  (LEN(@tags)-LEN(REPLACE(@tags,'CASH ADDED','')))/LEN('CASH ADDED'))
  while (@k<=@i)
  begin
  set @Txt =cast( (select right(@Txt, len(@Txt) - charindex('CASH ADDED', @Txt))) as nvarchar(max)) 
  set @Txt1 =((SELECT SUBSTRING(@Txt, CHARINDEX('TYPE 1',@Txt) ,14)))
  set @Txt1p=(select right(@Txt1, len(@Txt1) - charindex('=', @Txt1)))
  set @Txt2 =(SELECT SUBSTRING(@Txt, CHARINDEX('TYPE 2',@Txt) ,14))
  set @Txt2p=(select right(@Txt2, len(@Txt2) - charindex('=', @Txt2)))
  set @Txt3 =(SELECT SUBSTRING(@Txt, CHARINDEX('TYPE 3',@Txt) ,14))
  set @Txt3p=(select right(@Txt3, len(@Txt3) - charindex('=', @Txt3)))
  set @Txt4 =(SELECT SUBSTRING(@Txt, CHARINDEX('TYPE 4',@Txt) ,14))
  set @Txt4p=(select right(@Txt4, len(@Txt4) - charindex('=', @Txt4)))
  if(@k=@i)
  begin
   set @Amount=((Try_Parse(@Txt1p as int)*100)+(Try_Parse(@Txt2p as int)*200)+(Try_Parse(@Txt3p as int)*100)+(Try_Parse(@Txt4p as int)*200))
  end
  else
  begin
  set @Amount=@Amount+((Try_Parse(@Txt1p as int)*100)+(Try_Parse(@Txt2p as int)*200)+(Try_Parse(@Txt3p as int)*100)+(Try_Parse(@Txt4p as int)*200))
  end
  set @k=@k+1
  end
  end

	return @Amount
end
GO
/****** Object:  UserDefinedFunction [dbo].[ExtractAmountForNCRAfterType1]    Script Date: 02/12/2024 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Batch submitted through debugger: SQLQuery3.sql|7|0|C:\Users\AtmView\AppData\Local\Temp\~vs9247.sql
CREATE FUNCTION [dbo].[ExtractAmountForNCRAfterType1] (@DoneOrderAmountTxt VARCHAR(8000))
RETURNS bigint
as begin
Declare @Txt  VARCHAR(8000)
Declare @Txt1 VARCHAR(8000)
dECLARE @Amount int
Declare @i int
Declare @Type int
set @i=0 ;
set @Amount=0;
set @Type=0;
 set @Txt=(select(cast( (select right(@DoneOrderAmountTxt, len((@DoneOrderAmountTxt)) - charindex('REMAINING',(@DoneOrderAmountTxt)))) as varchar(8000))))
set @Txt =((SELECT LEFT(@Txt,39)))
set @Txt=REPLACE(@Txt,'EMAINING        ','')
  DECLARE mycurs3 CURSOR
  FOR select value from STRING_SPLIT (@Txt,' ')
  open mycurs3
  FETCH NEXT FROM mycurs3 INTO @Txt1

While @@FETCH_STATUS = 0
begin
if(Try_Parse(@Txt1 as int) is not null)
begin
set @i=@i+1
if(@i%2=0)
begin
set @Type=200
end
else 
begin
set @Type=100
end
set @Amount=@Amount+(Try_Parse(@Txt1 as int)*@Type)
end
    fetch Next from mycurs3 INTO @Txt1

	
END

Deallocate mycurs3

return @Amount
end
GO
/****** Object:  UserDefinedFunction [dbo].[ExtractAmountForNCRAfterType2]    Script Date: 02/12/2024 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Batch submitted through debugger: SQLQuery5.sql|7|0|C:\Users\AtmView\AppData\Local\Temp\~vsC7A6.sql
-- Batch submitted through debugger: SQLQuery3.sql|7|0|C:\Users\AtmView\AppData\Local\Temp\~vs9247.sql
CREATE FUNCTION [dbo].[ExtractAmountForNCRAfterType2] (@DoneOrderAmountTxt VARCHAR(8000))
RETURNS bigint
as begin
Declare @Txt  VARCHAR(8000)
Declare @Txt1 VARCHAR(8000)
Declare @Txt2 VARCHAR(8000)
dECLARE @Amount int
Declare @i int
Declare @Type int
set @i=0 ;
set @Amount=0;
set @Type=0;
set @Txt=(select(cast( (select right(@DoneOrderAmountTxt, len((@DoneOrderAmountTxt)) - charindex(' REMAINING',(@DoneOrderAmountTxt)))) as varchar(8000))))
--set @Txt =((SELECT LEFT(@Txt,69)))
set @Txt=REPLACE(@Txt,'REMAINING','')
set @Txt=REPLACE(@Txt,'TYPE 1 =','')
set @Txt=REPLACE(@Txt,'TYPE 2 =','')
set @Txt=REPLACE(@Txt,'TYPE 3 =','')
set @Txt=REPLACE(@Txt,'TYPE 4 =','')
DECLARE mycurs3 CURSOR
  FOR select value from STRING_SPLIT (@Txt,' ')
  open mycurs3
  FETCH NEXT FROM mycurs3 INTO @Txt1

While (@@FETCH_STATUS = 0 and @i<=4)
begin
if(charindex('[',(@Txt1))!=0)
begin 
set @Txt1=Left(@Txt1,CHARINDEX('[',@Txt1)-1)
end
if(Try_Parse(@Txt1 as int) is not null)
begin
set @i=@i+1
if(@i%2=0)
begin
set @Type=200
end
else 
begin
set @Type=100
end
set @Amount=@Amount+(Try_Parse(@Txt1 as int)*@Type)

end
    fetch Next from mycurs3 INTO @Txt1
END
Deallocate mycurs3
return @Amount
end
GO
/****** Object:  UserDefinedFunction [dbo].[ExtractDate]    Script Date: 02/12/2024 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Batch submitted through debugger: SQLQuery3.sql|7|0|C:\Users\AtmView\AppData\Local\Temp\~vs9247.sql
CREATE FUNCTION [dbo].[ExtractDate] (@texte nvarchar(max))
RETURNS bigint
as begin
  Declare @Amount bigint
  Declare @number1 int
  Declare @number2 int
  Declare @Txt nvarchar(max)
  Declare @Txt2 nvarchar(max)
  Declare @Txt3 nvarchar(max)
  Declare @i int
  SET @Amount=0
  set @Txt2 =(SELECT SUBSTRING(@texte, CHARINDEX('CASH COUNTERS AFTER SOP',@texte) + LEN('CASH COUNTERS AFTER SOP'), LEN(@texte)))
  set @Txt3 =(SELECT LEFT(@Txt2, CHARINDEX('RETRACTS', @Txt2)-1))
  DECLARE mycurs3 CURSOR
  FOR select value from STRING_SPLIT (@Txt3,' ')
  open mycurs3
  FETCH NEXT FROM mycurs3 INTO @Txt

While @@FETCH_STATUS = 0
begin
	while ((Try_Parse(@Txt as int) is null ) and  @@FETCH_STATUS = 0)
	begin 
	if(CHARINDEX('*',@Txt)!=0)
	begin
	break
	end
	fetch Next from mycurs3 INTO @Txt
	end 
	set @number1=TRY_PARSE(@Txt as int)
	fetch Next from mycurs3 INTO @Txt
	while ((Try_Parse(@Txt as int) is null ) and  @@FETCH_STATUS = 0)
	begin 
	if(CHARINDEX('*',@Txt)!=0)
	begin
	break
	end
	fetch Next from mycurs3 INTO @Txt
	end 
	if(CHARINDEX('*',@Txt)!=0 )
	begin
    set @Txt=(select REPLACE(@Txt, '*', ''))
	end
	set @number2=(select Try_Parse(@Txt as int))
	set @i=(@number1*@number2)
	if(@i is not null)
	begin
    SET @Amount=sum(@Amount+@i)
	end
	
    fetch Next from mycurs3 INTO @Txt

	
END

Deallocate mycurs3

return @Amount
end
GO
/****** Object:  UserDefinedFunction [dbo].[ExtractTrueDoneOrderDate]    Script Date: 02/12/2024 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery3.sql|7|0|C:\Users\AtmView\AppData\Local\Temp\~vs9247.sql
CREATE FUNCTION [dbo].[ExtractTrueDoneOrderDate] (@Date Datetime,@profile varchar(100),@tags nvarchar(max))
RETURNS Datetime
as begin
Declare @TrueDoneOrderDate Datetime
Declare @DateVal nvarchar(max)
Declare @DateVal2 nvarchar(max)
Declare @i int
Declare @Txt nvarchar(max)
Declare @Hour int
Declare @Minute int
Declare @Second int
Declare @Date3 Datetime
Declare @Test nvarchar(max)
if (@profile='WN')
begin
set @DateVal =(select LEFT(@tags,CHARINDEX('CASH COUNTERS AFTER SOP',@tags)-1))
set @DateVal2 =(select RIGHT(@DateVal,10))
set @i=1
DECLARE mycurs3 CURSOR
FOR select value from STRING_SPLIT (@DateVal2,':')
open mycurs3
FETCH NEXT FROM mycurs3 INTO @Txt
While (@@FETCH_STATUS = 0 and @i<=3)
begin
if(TRY_PARSE(@Txt as int) is not null)
begin 
if(@i=1)
begin
SET @Hour=TRY_PARSE(@Txt as int)
end
if(@i=2)
begin
SET @minute=TRY_PARSE(@Txt as int)
end
if(@i=3)
begin
SET @second=TRY_PARSE(@Txt as int)
end 
SET @i=@i+1
end
fetch Next from mycurs3 INTO @Txt
end
Deallocate mycurs3
set @Date3=(cast( @Date as Date))
set @TrueDoneOrderDate =DATEADD(HOUR,@Hour,@Date3)
set @TrueDoneOrderDate =DATEADD(MINUTE,@minute,@TrueDoneOrderDate)
set @TrueDoneOrderDate =DATEADD(SECOND,@second,@TrueDoneOrderDate)
if(@TrueDoneOrderDate is null)
begin 
set @TrueDoneOrderDate=@Date
end
end
--if (@profile='NCR')
--begin
--if((CHARINDEX('[05pCASH ADDED',@tags))!=0)
--begin
--set @DateVal =(select LEFT(@tags,CHARINDEX('[05pCASH ADDED',@tags)-1))
--end 
--else
--begin 
--set @DateVal =(select LEFT(@tags,CHARINDEX('CASH ADDED',@tags)-1))
--end 
--set @i=10
--set @DateVal2 =(select RIGHT(@DateVal,@i))
--set @Test=(select REPLACE(@DateVal2,'*',' '))
--while(Try_Parse(@Test as datetime) is null)
--begin
--SET @i=@i+1
--set @DateVal2 =(select RIGHT(@DateVal,@i))
--set @Test=(select REPLACE(@DateVal2,'*',' '))
--end
--set @TrueDoneOrderDate=Try_Parse(@Test as datetime)
--set @i=@i+1
--set @DateVal2 =(select RIGHT(@DateVal,@i))
--set @Test=(select REPLACE(@DateVal2,'*',' '))
--if(Try_Parse(@Test as datetime)is not null )
--BEGIN
--set @TrueDoneOrderDate=Try_Parse(@Test as datetime)
--END 



--end

return @TrueDoneOrderDate
end
GO
/****** Object:  Table [dbo].[ActionCorrective]    Script Date: 02/12/2024 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionCorrective](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[User_Id] [nvarchar](max) NULL,
	[Color] [nvarchar](max) NULL,
	[EmailTo] [int] NULL,
	[EmailCc] [int] NULL,
	[PhoneTo] [int] NULL,
	[Emails] [nvarchar](max) NULL,
	[Phones] [nvarchar](max) NULL,
	[CcEmails] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ActionCorrective] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Street] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](10) NULL,
	[City] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlertControls]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlertControls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlertId] [nchar](128) NOT NULL,
	[JobCtrlId] [int] NOT NULL,
	[ExecutionDate] [datetime] NOT NULL,
	[EmailSended] [bit] NOT NULL,
	[SmsSended] [bit] NOT NULL,
	[SendEmailOutput] [nvarchar](max) NULL,
	[SendSmsOutput] [nvarchar](max) NULL,
 CONSTRAINT [PK_AlertControl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Alerts]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alerts](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Condition] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Etat] [bit] NOT NULL,
	[Module] [nvarchar](max) NULL,
	[Template_Id] [int] NOT NULL,
	[RiseInterval] [int] NOT NULL,
	[RenotifyInterval] [int] NOT NULL,
	[SendSms] [bit] NOT NULL,
	[SendEmail] [bit] NOT NULL,
	[JobId] [int] NULL,
	[RiseOnStateChanged] [bit] NOT NULL,
	[Parameters] [nvarchar](max) NULL,
	[EmailTo] [int] NULL,
	[EmailCc] [int] NULL,
	[PhoneTo] [int] NULL,
	[Emails] [nvarchar](max) NULL,
	[Phones] [nvarchar](max) NULL,
	[CcEmails] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Alerts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Titre] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_dbo.Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Arguments]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Arguments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArgName] [nvarchar](max) NULL,
	[ArgValue] [nvarchar](max) NULL,
	[AlertId] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Arguments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArretCassetteStock]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArretCassetteStock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AtmArretJournee_Id] [int] NOT NULL,
	[Currency] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Rejected] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[Total] [int] NOT NULL,
	[IdCassette] [nvarchar](max) NULL,
	[Edition] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ArretCassetteStock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArretJourne]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArretJourne](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateArret] [datetime] NOT NULL,
	[SoldeOuverture] [bigint] NULL,
	[SoldeCloture] [bigint] NULL,
	[livraisons] [bigint] NULL,
	[collecte] [bigint] NULL,
	[Depot] [bigint] NULL,
	[retrait] [bigint] NULL,
	[ArretJournestatut] [int] NULL,
	[Branch_Id] [int] NULL,
	[CashStock_Id] [int] NULL,
 CONSTRAINT [PK_dbo.ArretJourne] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[SousMenuId] [int] NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[FirstLogin] [bit] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[Company] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[PasswordExpiration] [datetime] NULL,
	[LastLoginTime] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Atm]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Atm](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Profile] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[Address_Id] [int] NOT NULL,
	[Organisation] [nvarchar](50) NULL,
	[SoftweareRelease] [nvarchar](50) NULL,
	[InstallationDate] [datetime] NULL,
	[SerialNumber] [int] NULL,
	[System] [nvarchar](50) NULL,
	[HostName] [nvarchar](50) NULL,
	[PortNumber] [int] NULL,
	[EnableTLS] [bit] NOT NULL,
	[Agency_Id] [int] NULL,
	[CashProvider_Id] [int] NULL,
	[Actif] [bit] NOT NULL,
	[Lat] [float] NULL,
	[Long] [float] NULL,
	[JrnId] [nvarchar](100) NULL,
	[Account] [nvarchar](100) NULL,
	[CassetteSetup_Id] [int] NULL,
	[ConfigCassette_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Atm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmArreteJoune]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmArreteJoune](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Closingbalance] [int] NOT NULL,
	[Retract] [int] NOT NULL,
	[Rejected] [int] NOT NULL,
	[ArretJournestatut] [int] NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[State_Id] [int] NULL,
 CONSTRAINT [PK_dbo.AtmArreteJoune] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmCashAlert]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmCashAlert](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[State_Id] [int] NOT NULL,
	[Exaustion] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.AtmCashAlert] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmCashAlertExhaution]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmCashAlertExhaution](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[State_Id] [int] NOT NULL,
 CONSTRAINT [PK_AtmCashAlertExhaution] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmCashAlertWarning]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmCashAlertWarning](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[State_Id] [int] NOT NULL,
 CONSTRAINT [PK_AtmCashAlertWarning] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmCommError]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmCommError](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[LastState_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AtmCommError] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmContact]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmContact](
	[Atm_Id] [nvarchar](128) NOT NULL,
	[Contact_Id] [int] NOT NULL,
	[Ordre] [int] NULL,
 CONSTRAINT [PK_dbo.AtmContact] PRIMARY KEY CLUSTERED 
(
	[Atm_Id] ASC,
	[Contact_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmError]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmError](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[State_Id] [int] NOT NULL,
	[ActionCorrective_Id] [int] NULL,
 CONSTRAINT [PK_dbo.AtmError] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmMaintenanceMode]    Script Date: 02/12/2024 14:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmMaintenanceMode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[LastState_Id] [int] NOT NULL,
	[MailSend] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmProfilCommon]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmProfilCommon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Programslist] [nvarchar](max) NULL,
	[MonitoringServices] [nvarchar](max) NULL,
	[JrnSaveDir] [nvarchar](max) NULL,
	[TxPicturesDir] [nvarchar](max) NULL,
	[CaptureMode] [int] NOT NULL,
	[CaptureMtd] [int] NOT NULL,
	[UseProfilXFSVersion] [bit] NOT NULL,
	[MoneticSerIp] [nvarchar](max) NULL,
	[MoneticSerPort] [int] NOT NULL,
	[BankName] [nvarchar](max) NULL,
	[EnableSniffing] [bit] NOT NULL,
	[LaunchWaitTime] [int] NOT NULL,
	[LogKeepTime] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AtmProfilCommon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmProfile]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AtmCons] [nvarchar](max) NULL,
	[CdmServName] [nvarchar](max) NULL,
	[IdcServName] [nvarchar](max) NULL,
	[JrnServName] [nvarchar](max) NULL,
	[PinServName] [nvarchar](max) NULL,
	[RcpServName] [nvarchar](max) NULL,
	[SiuServName] [nvarchar](max) NULL,
	[TtuServName] [nvarchar](max) NULL,
	[JrnlDir] [nvarchar](max) NULL,
	[RebootCmd] [nvarchar](max) NULL,
	[GoInServCmd] [nvarchar](max) NULL,
	[GooutServCmd] [nvarchar](max) NULL,
	[AttachmentDir] [nvarchar](max) NULL,
	[InServPhrase] [nvarchar](max) NULL,
	[OutServPhrase] [nvarchar](max) NULL,
	[ErrAttachFile] [nvarchar](max) NULL,
	[xfs_version] [int] NOT NULL,
	[Programlist] [nvarchar](max) NULL,
	[MonitoringServices] [nvarchar](max) NULL,
	[UseProfilXFSVersion] [nvarchar](max) NULL,
	[CaptureMode] [int] NOT NULL,
	[CaptureMtd] [int] NOT NULL,
	[JrnSaveDir] [nvarchar](max) NULL,
	[TrxPicturesDir] [nvarchar](max) NULL,
	[MoneticSerIp] [nvarchar](max) NULL,
	[MoneticSerPort] [nvarchar](max) NULL,
	[InactiveChkEnabled] [nvarchar](max) NULL,
	[InactiveHours] [nvarchar](max) NULL,
	[JrnNameFilter] [nvarchar](max) NULL,
	[launchwaittime] [int] NOT NULL,
	[JrnlBackupDir] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AtmProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmRejectStatut]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmRejectStatut](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](250) NULL,
	[Label_fr] [nvarchar](250) NULL,
	[StatutId] [int] NOT NULL,
	[Color] [nvarchar](50) NULL,
	[Highlight] [nvarchar](50) NULL,
	[Failure] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.AtmRejectStatut] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtmRemarque]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmRemarque](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[Remarque] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.AtmRemarque] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AVAtmConfig]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AVAtmConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[AtmIP] [nvarchar](max) NULL,
	[Current_Line] [int] NOT NULL,
	[Current_Jrn_Line] [int] NOT NULL,
	[Current_File] [nvarchar](max) NULL,
	[Journal_Dir] [nvarchar](max) NULL,
	[Agent_Version] [nvarchar](max) NULL,
	[Last_Exchange] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.AVAtmConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AVTransaction]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AVTransaction](
	[AtmID] [nvarchar](128) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[AutorisationNumber] [nvarchar](max) NULL,
	[CardNumber] [nvarchar](max) NULL,
	[Rib] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Amount] [int] NOT NULL,
	[Statut] [int] NOT NULL,
	[isCashPresented] [bit] NOT NULL,
	[isCashTaken] [bit] NOT NULL,
	[isCashRetracted] [bit] NOT NULL,
	[isCashoutError] [bit] NOT NULL,
	[ExistInHost] [bit] NOT NULL,
	[IsRejected] [bit] NOT NULL,
	[TrxPictures] [nvarchar](max) NULL,
	[Duration] [float] NULL,
	[ExtraInfos] [nvarchar](max) NULL,
	[is_FraudP] [bit] NOT NULL,
	[FraudReason] [varchar](max) NULL,
 CONSTRAINT [PK_dbo.AVTransaction] PRIMARY KEY CLUSTERED 
(
	[AtmID] ASC,
	[TransactionDate] ASC,
	[TransactionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AVTransactionAgency]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AVTransactionAgency](
	[Agency_Id] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Withdrawal] [bigint] NOT NULL,
	[AutorisationNumber] [nvarchar](max) NULL,
	[Deposit] [bigint] NULL,
	[SoldeOuverture] [bigint] NULL,
	[SoldeCloture] [bigint] NULL,
	[Statut] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AVTransactionAgency] PRIMARY KEY CLUSTERED 
(
	[Agency_Id] ASC,
	[TransactionDate] ASC,
	[Withdrawal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Bank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BinCategory]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BinCategory](
	[Type] [nvarchar](max) NOT NULL,
	[color] [nvarchar](max) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BinCategory_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BinConfiguration]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BinConfiguration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[key] [nvarchar](max) NULL,
	[BinCategory_Id] [int] NOT NULL,
	[Type] [nvarchar](max) NULL,
	[color] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.BinConfiguration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[Address_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Branch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bug]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bug](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Creator] [nvarchar](max) NULL,
	[AssignedUser] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[AtmError_Id] [int] NULL,
	[ActionCorrective_Id] [int] NULL,
	[BugCategory_Id] [int] NOT NULL,
	[BugPriority_Id] [int] NOT NULL,
	[BugStatut_Id] [int] NOT NULL,
	[ResolutionDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Bug] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BugAtm]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BugAtm](
	[Bug_Id] [int] NOT NULL,
	[Atm_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.BugAtm] PRIMARY KEY CLUSTERED 
(
	[Bug_Id] ASC,
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BugAttachment]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BugAttachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Bug_Id] [int] NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[FileName] [nvarchar](max) NULL,
	[ContentType] [nvarchar](max) NULL,
	[Attachment] [varbinary](max) NULL,
 CONSTRAINT [PK_dbo.BugAttachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BugCategory]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BugCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.BugCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BugComment]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BugComment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Bug_Id] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.BugComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BugComponent]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BugComponent](
	[Bug_Id] [int] NOT NULL,
	[Component_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.BugComponent] PRIMARY KEY CLUSTERED 
(
	[Bug_Id] ASC,
	[Component_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BugHistory]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BugHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Bug_Id] [int] NOT NULL,
	[Changes] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.BugHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BugPriority]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BugPriority](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.BugPriority] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BugStatut]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BugStatut](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.BugStatut] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaisseAgence]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaisseAgence](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_dbo.CaisseAgence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CashPoint]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CashPointName] [nvarchar](max) NULL,
	[Profile_Id] [int] NULL,
	[Location] [nvarchar](max) NULL,
	[Description] [nvarchar](100) NULL,
	[Address_Id] [int] NULL,
	[Organisation] [nvarchar](50) NULL,
	[SoftweareRelease] [nvarchar](50) NULL,
	[InstallationDate] [datetime] NULL,
	[System] [nvarchar](50) NULL,
	[HostName] [nvarchar](50) NULL,
	[PortNumber] [int] NULL,
	[EnableTLS] [bit] NULL,
	[SerialNumber] [int] NULL,
 CONSTRAINT [PK_dbo.CashPoint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CashPointContact]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashPointContact](
	[CashPoint_Id] [int] NOT NULL,
	[Contact_Id] [int] NOT NULL,
	[Ordre] [int] NULL,
 CONSTRAINT [PK_dbo.CashPointContact] PRIMARY KEY CLUSTERED 
(
	[CashPoint_Id] ASC,
	[Contact_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CashPointProfile]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashPointProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CashPointProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CashProvider]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashProvider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CashProvider_Name] [nvarchar](max) NULL,
	[CashProvider_Adress] [nvarchar](max) NULL,
	[CashProvider_Phone] [nvarchar](max) NULL,
	[CashProvider_Fax] [nvarchar](max) NULL,
	[CashProvider_Email] [nvarchar](max) NULL,
	[CashProvider_ICE] [nvarchar](max) NULL,
	[CashProvider_ContactName] [nvarchar](max) NULL,
	[CashProvider_IdFiscal] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CashProvider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CashStock]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashStock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[State_Id] [int] NULL,
	[Retract] [int] NULL,
	[Total] [int] NULL,
	[Treshold] [int] NULL,
 CONSTRAINT [PK_dbo.CashStock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CassetteSetup]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CassetteSetup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CassetteStock]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CassetteStock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CashStock_Id] [int] NOT NULL,
	[Currency] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Start] [int] NOT NULL,
	[Presented] [int] NOT NULL,
	[Rejected] [int] NOT NULL,
	[IdCassette] [nvarchar](max) NULL,
	[StateCassette] [nvarchar](max) NULL,
	[Value] [int] NOT NULL,
	[Edition] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CassetteStock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_dbo.City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Raison_social] [nvarchar](max) NULL,
	[Secteur_Activite] [nvarchar](max) NULL,
	[Adresse_Courrier] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Telephone] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[ContractStatut] [int] NULL,
	[Agency_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Command]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Command](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Script] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[NeedArguments] [bit] NOT NULL,
	[TimeOut] [int] NOT NULL,
	[IsJobCmd] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Command] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommandControl]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommandControl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[Command_Id] [int] NOT NULL,
	[ExecutionDate] [datetime] NULL,
	[ExecutStatus] [int] NOT NULL,
	[Arg1] [nvarchar](max) NULL,
	[Arg2] [nvarchar](max) NULL,
	[Arg3] [nvarchar](max) NULL,
	[Result] [int] NOT NULL,
	[Error] [nvarchar](max) NULL,
	[Output] [nvarchar](max) NULL,
	[User_Id] [nvarchar](max) NULL,
	[NotifState] [bit] NOT NULL,
	[JobControleId] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[CanIgnore] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.CommandControl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Component]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Component](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](max) NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Component] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComponentState]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComponentState](
	[State_Id] [int] NOT NULL,
	[Component_Id] [int] NOT NULL,
	[StateComponent_Id] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[LastDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.ComponentState] PRIMARY KEY CLUSTERED 
(
	[State_Id] ASC,
	[Component_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConfigCassette]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigCassette](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Currency] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Edition] [nvarchar](max) NULL,
	[CassetteSetupId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[FirstName] [nvarchar](100) NULL,
	[Company] [nvarchar](100) NULL,
	[Phone] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Function] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContextualMenu]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContextualMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Libelle] [nvarchar](max) NULL,
	[ActionName] [nvarchar](max) NULL,
	[ControllerName] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[OrdreAffichage] [int] NOT NULL,
	[NodeType_Id] [int] NOT NULL,
	[Libelle_eng] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ContextualMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoneOrder]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoneOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoneOrder_Date] [datetime] NOT NULL,
	[DoneOrder_Amount] [int] NOT NULL,
	[DoneOrderState] [int] NOT NULL,
	[Atm_Id] [nvarchar](max) NULL,
	[CashPoint_Id] [int] NULL,
	[CashProvider_Id] [int] NOT NULL,
	[StateBefore_Id] [int] NOT NULL,
	[StateAfter_Id] [int] NULL,
	[AddedAmount] [int] NOT NULL,
 CONSTRAINT [PK_dbo.DoneOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoneOrderAgency]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoneOrderAgency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoneOrderAgency_Date] [datetime] NOT NULL,
	[DoneOrderAgencyDelivery_Amount] [int] NOT NULL,
	[DoneOrderAgencyCollecte_Amount] [int] NOT NULL,
	[DoneOrderStateAgency] [int] NOT NULL,
	[Agency_Id] [int] NOT NULL,
	[CashProvider_Id] [int] NOT NULL,
	[StateBefore_Id] [int] NOT NULL,
	[StateAfter_Id] [int] NULL,
	[AddedAmountAgency] [int] NOT NULL,
 CONSTRAINT [PK_dbo.DoneOrderAgency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EncaisseMax]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncaisseMax](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Agency_Id] [int] NOT NULL,
	[EncaisseMaximum] [bigint] NULL,
	[DefinedEncaisseMaximum] [bigint] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_dbo.EncaisseMax] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorType]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[errcode] [int] NOT NULL,
	[label] [nvarchar](max) NULL,
	[descr] [nvarchar](max) NULL,
	[actioncorrectivid] [int] NULL,
	[IsFailure] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ErrorType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrTypeId]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrTypeId](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[State_Id] [int] NULL,
	[Bug_Id] [int] NULL,
	[ErrorType_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ErrTypeId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
	[ThemeColor] [nvarchar](max) NULL,
	[IsFullDay] [bit] NOT NULL,
	[IsRecurrent] [bit] NULL,
 CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factor]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SumRetrait] [bigint] NULL,
	[ConsommationHier] [bigint] NULL,
	[MSemaineDernier] [float] NULL,
	[MSemaine7] [float] NULL,
	[ConsoMmJrAnP] [bigint] NULL,
	[ConsoMmJrMP] [bigint] NULL,
	[ConsoMMJrSmDer] [bigint] NULL,
	[MoyenneMoisPrec] [float] NULL,
	[MoyenneMMSAnPrec] [float] NULL,
	[MoyenneMMmAnPrec] [float] NULL,
	[ConsommationMaxMDer] [bigint] NULL,
	[ConsommationMaxSDer] [bigint] NULL,
	[PoidTot] [float] NULL,
	[intweekMonth] [int] NULL,
	[Bais] [int] NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[TransactionDay] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Factor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FactorAgency]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactorAgency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SumRetrait] [bigint] NULL,
	[ConsommationHier] [bigint] NULL,
	[MSemaineDernier] [float] NULL,
	[MSemaine7] [float] NULL,
	[ConsoMmJrAnP] [bigint] NULL,
	[ConsoMmJrMP] [bigint] NULL,
	[ConsoMMJrSmDer] [bigint] NULL,
	[MoyenneMoisPrec] [float] NULL,
	[MoyenneMMSAnPrec] [float] NULL,
	[MoyenneMMmAnPrec] [float] NULL,
	[ConsommationMaxMDer] [bigint] NULL,
	[PoidJr] [int] NOT NULL,
	[PoidName] [int] NOT NULL,
	[PoidTot] [int] NOT NULL,
	[intweekMonth] [int] NOT NULL,
	[Bais] [int] NOT NULL,
	[DayName] [nvarchar](max) NULL,
	[TransactionDay] [datetime] NOT NULL,
	[Agency_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.FactorAgency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FactorDepositAgency]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactorDepositAgency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deposit] [bigint] NULL,
	[DepositHier] [bigint] NULL,
	[MSemaineDernier] [float] NULL,
	[MSemaine7] [float] NULL,
	[ConsoMmJrAnP] [bigint] NULL,
	[ConsoMmJrMP] [bigint] NULL,
	[ConsoMMJrSmDer] [bigint] NULL,
	[MoyenneMoisPrec] [float] NULL,
	[MoyenneMMSAnPrec] [float] NULL,
	[MoyenneMMmAnPrec] [float] NULL,
	[DepositMaxMDer] [bigint] NULL,
	[PoidJr] [int] NOT NULL,
	[PoidName] [int] NOT NULL,
	[PoidTot] [int] NOT NULL,
	[intweekMonth] [int] NOT NULL,
	[Bais] [int] NOT NULL,
	[DayName] [nvarchar](max) NULL,
	[TransactionDay] [datetime] NOT NULL,
	[Agency_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.FactorDepositAgency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FPState]    Script Date: 02/12/2024 14:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FPState](
	[Task] [varchar](max) NULL,
	[IsDone] [bit] NULL,
	[Time] [datetime] NULL,
	[Result] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FrequentCommand]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FrequentCommand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Arg1] [nvarchar](max) NULL,
	[Arg2] [nvarchar](max) NULL,
	[Arg3] [nvarchar](max) NULL,
 CONSTRAINT [PK_FrequentCommand2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Holyday]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holyday](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
	[ThemeColor] [nvarchar](max) NULL,
	[IsFullDay] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Holyday] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Im_Atm_Inventory]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Im_Atm_Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[InstallationDate] [datetime] NULL,
	[Address] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](10) NULL,
	[LocationType] [nvarchar](20) NULL,
	[Communication] [nvarchar](100) NULL,
	[Security] [nvarchar](100) NULL,
	[EPP] [nvarchar](100) NULL,
	[ScreenDisplay] [nvarchar](100) NULL,
	[CashDispenser] [nvarchar](100) NULL,
	[CardReader] [nvarchar](100) NULL,
	[Printers] [nvarchar](100) NULL,
	[PowerSupply] [nvarchar](100) NULL,
	[Dimension] [nvarchar](100) NULL,
	[TouchScreen] [nvarchar](10) NULL,
	[Camera] [int] NULL,
	[EnvironmentalConditions] [nvarchar](100) NULL,
	[SupportedLanguages] [nvarchar](100) NULL,
	[Cost] [nvarchar](40) NULL,
	[CostCurrency] [nvarchar](40) NULL,
	[Notes] [nvarchar](150) NULL,
	[Atm_id] [nvarchar](128) NOT NULL,
	[Computer_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Im_Computer_Inventory]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Im_Computer_Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[SerialNumber] [nvarchar](50) NULL,
	[Marque] [nvarchar](50) NULL,
	[Model] [nvarchar](50) NULL,
	[ComputerType] [nvarchar](50) NULL,
	[OperatingSystem] [nvarchar](50) NULL,
	[CPU] [nvarchar](50) NULL,
	[RAM] [nvarchar](50) NULL,
	[Storage] [nvarchar](50) NULL,
	[StorageType] [nvarchar](20) NULL,
	[PurchaseDate] [date] NULL,
	[PurchaseOrderNumber] [nvarchar](30) NULL,
	[Warranty] [nvarchar](50) NULL,
	[WarrantyLength] [nvarchar](50) NULL,
	[WarrantyExpirationDate] [date] NULL,
	[DeliveryDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Im_Move_Inventory]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Im_Move_Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[PreviousLocation] [nvarchar](50) NULL,
	[NewLocation] [nvarchar](50) NULL,
	[AtmInventory_id] [int] NOT NULL,
	[Description] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incident]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incident](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Numero] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[Owner] [nvarchar](max) NULL,
	[Assainer] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Priority] [int] NOT NULL,
	[State] [int] NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Incident] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceItems]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderTypeID] [int] NULL,
	[InvoiceID] [int] NULL,
	[Quantity] [int] NOT NULL,
	[Taxable] [bit] NOT NULL,
	[Total] [float] NOT NULL,
 CONSTRAINT [PK_dbo.InvoiceItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[SalesTaxPercent] [float] NOT NULL,
	[PaymentAmount] [float] NOT NULL,
	[Status] [nvarchar](max) NULL,
	[TotalSalesTax] [float] NOT NULL,
	[SubTotal] [float] NOT NULL,
	[GrandTotal] [float] NOT NULL,
	[AmountDue] [float] NOT NULL,
	[CashProvider_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Invoices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[RetryInterval] [int] NULL,
	[RetryTimes] [int] NULL,
	[FirstStartDate] [datetime] NULL,
	[Frequence] [int] NULL,
	[IsFinished] [bit] NULL,
	[JobType_Id] [int] NOT NULL,
	[StartHour] [nvarchar](max) NULL,
	[DayOfWeek] [int] NULL,
	[DayOfMonth] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ScheduledDate] [datetime] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobAtm]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobAtm](
	[Job_Id] [int] NOT NULL,
	[Atm_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.JobAtm] PRIMARY KEY CLUSTERED 
(
	[Job_Id] ASC,
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobAtmExecutionResult]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobAtmExecutionResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Job_Id] [int] NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[ExecutionDate] [datetime] NOT NULL,
	[ExecutionHour] [nvarchar](max) NULL,
	[ExecutionNumber] [int] NOT NULL,
	[Result] [nvarchar](max) NULL,
	[Output] [nvarchar](max) NULL,
	[JobControle_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.JobAtmExecutionResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobCommand]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobCommand](
	[Job_Id] [int] NOT NULL,
	[Command_Id] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Order] [int] NOT NULL,
	[Agr1] [nvarchar](max) NULL,
	[Agr2] [nvarchar](max) NULL,
	[Agr3] [nvarchar](max) NULL,
	[CanIgnore] [bit] NULL,
 CONSTRAINT [PK_dbo.JobCommand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobCommandExecutionResult]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobCommandExecutionResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Job_Id] [int] NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[Command_Id] [int] NOT NULL,
	[ExecutionDate] [datetime] NOT NULL,
	[ExecutionHour] [nvarchar](max) NULL,
	[Result] [nvarchar](max) NULL,
	[Output] [nvarchar](max) NULL,
	[JobControle_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.JobCommandExecutionResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobControles]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobControles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExecutionDate] [datetime] NOT NULL,
	[Job_Id] [int] NOT NULL,
	[Atms] [nvarchar](max) NULL,
	[RestToExec] [nvarchar](max) NULL,
	[nbOfAtms] [int] NULL,
	[nbOfFails] [int] NULL,
	[nbOfSuccess] [int] NULL,
	[JobState_Id] [int] NULL,
	[LastExecution] [datetime] NULL,
	[NumberOfReexecution] [int] NOT NULL,
 CONSTRAINT [PK__JobContr__3214EC07E0E263E0] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobType]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.JobType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JournalEntry]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalEntry](
	[Atm_Id] [nvarchar](128) NOT NULL,
	[EntryTime] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
	[Filename] [nvarchar](max) NULL,
	[EntryType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.JournalEntry] PRIMARY KEY CLUSTERED 
(
	[Atm_Id] ASC,
	[EntryTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JournalEntryMontly]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalEntryMontly](
	[Atm_Id] [nvarchar](128) NOT NULL,
	[EntryTime] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
	[Filename] [nvarchar](max) NULL,
	[EntryType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.JournalEntryMontly] PRIMARY KEY CLUSTERED 
(
	[Atm_Id] ASC,
	[EntryTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LastDealyTrx]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LastDealyTrx](
	[AtmID] [nvarchar](128) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[AutorisationNumber] [nvarchar](max) NULL,
	[CardNumber] [nvarchar](max) NULL,
	[Rib] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Amount] [int] NOT NULL,
	[Statut] [int] NOT NULL,
	[isCashPresented] [bit] NOT NULL,
	[isCashTaken] [bit] NOT NULL,
	[isCashRetracted] [bit] NOT NULL,
	[isCashoutError] [bit] NOT NULL,
	[ExistInHost] [bit] NOT NULL,
	[IsRejected] [bit] NOT NULL,
	[TrxPictures] [nvarchar](max) NULL,
	[Duration] [float] NULL,
	[ExtraInfos] [nvarchar](max) NULL,
	[is_FraudP] [bit] NOT NULL,
	[FraudReason] [varchar](max) NULL,
 CONSTRAINT [PK_dbo.LastDealyTrx] PRIMARY KEY CLUSTERED 
(
	[AtmID] ASC,
	[TransactionDate] ASC,
	[TransactionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [nvarchar](max) NULL,
	[ControllerName] [nvarchar](max) NULL,
	[AreaName] [nvarchar](max) NULL,
	[IsHttpPost] [bit] NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[User_Id] [nvarchar](max) NULL,
	[AtmId] [nvarchar](max) NULL,
	[IpAdresse] [nvarchar](max) NULL,
	[Session_Id] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matelas]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matelas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[MatelasAmount] [int] NOT NULL,
	[SoldeFinJourneeAmount] [int] NOT NULL,
	[CashPoint_Id] [int] NOT NULL,
	[CashStock_Id] [int] NOT NULL,
	[CashStock_Id2] [int] NOT NULL,
	[MatelasPredicted] [real] NOT NULL,
 CONSTRAINT [PK_dbo.Matelas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name_Fr] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Icon] [nvarchar](max) NULL,
	[Class] [nvarchar](max) NULL,
	[Lien] [nvarchar](max) NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NodeType]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NodeType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.NodeType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ONLINE_AUTHORIZATION]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ONLINE_AUTHORIZATION](
	[AUT_CODE] [decimal](20, 0) NOT NULL,
	[AUT_EXTE_CODE] [varchar](40) NULL,
	[AUT_SOUR_INTE_CODE] [int] NULL,
	[AUT_PROC_INTE_CODE] [varchar](4) NULL,
	[AUT_DEST_INTE_CODE] [int] NULL,
	[AUT_AUTH_INTE_CODE] [int] NULL,
	[AUT_USER] [varchar](20) NULL,
	[AUT_SYST_CUT_OFF] [date] NULL,
	[AUT_ACQU_CUT_OFF] [date] NULL,
	[AUT_ISSU_CUT_OFF] [date] NULL,
	[AUT_REQU_SYST_TIME] [datetime2](7) NULL,
	[AUT_RESP_SYST_TIME] [datetime2](7) NULL,
	[AUT_AUTH_STAT] [varchar](1) NULL,
	[AUT_REVE_STAT] [varchar](1) NULL,
	[AUT_CONF_STAT] [varchar](1) NULL,
	[AUT_MATC_STAT] [varchar](1) NULL,
	[AUT_TRAN_TYPE_CODE] [bigint] NULL,
	[AUT_TRAN_CODE] [varchar](5) NULL,
	[AUT_SYS_CODE] [bigint] NULL,
	[AUT_BIN_CODE] [bigint] NULL,
	[AUT_BIN_TYPE] [varchar](1) NULL,
	[AUT_PROD_TYPE_CODE] [bigint] NULL,
	[AUT_BIN_IDEN] [bigint] NULL,
	[AUT_ISSU_BANK_CODE] [bigint] NULL,
	[AUT_PROF_MERC_GROU_CODE] [bigint] NULL,
	[AUT_COMM_CODE] [bigint] NULL,
	[AUT_COMM_AMOU] [decimal](15, 3) NULL,
	[AUT_COMM_FIX] [decimal](15, 3) NULL,
	[AUT_COMM_RATE] [decimal](9, 6) NULL,
	[AUT_COMM_MIN] [decimal](15, 3) NULL,
	[AUT_COMM_MAX] [decimal](15, 3) NULL,
	[AUT_CARD_STAT] [bigint] NULL,
	[AUT_ACC_STAT] [bigint] NULL,
	[AUT_CUST_STAT] [bigint] NULL,
	[AUT_MCC_GRP_CODE] [bigint] NULL,
	[AUT_CARD_VIP_LEVE] [int] NULL,
	[AUT_SOLV_LEVE_CODE] [bigint] NULL,
	[AUT_MERC_DOM] [char](1) NULL,
	[AUT_PAYM_CHAN] [char](1) NULL,
	[AUT_TERM_STAT] [bigint] NULL,
	[AUT_ACQ_BANK_CODE] [bigint] NULL,
	[AUT_MERC_STAT] [bigint] NULL,
	[AUT_MERC_SOLV] [bigint] NULL,
	[AUT_ACTI_CTRL] [int] NULL,
	[AUT_AVAI_CTRL] [int] NULL,
	[AUT_BALA_CTRL] [int] NULL,
	[AUT_MERC_ACTI_CTRL] [int] NULL,
	[AUT_MTI_CODE] [varchar](4) NULL,
	[AUT_ATY_CODE] [int] NULL,
	[AUT_PRIM_ACCT_NUMB_F002] [varchar](19) NULL,
	[AUT_PROC_CODE_F003] [varchar](6) NULL,
	[AUT_TRAN_TYPE_F003_01] [varchar](2) NULL,
	[AUT_ACCT_TYPE_FROM_F003_02] [varchar](2) NULL,
	[AUT_ACCT_TYPE_TO_F003_03] [varchar](2) NULL,
	[AUT_TRAN_AMOU_F004] [decimal](15, 3) NULL,
	[AUT_SETT_AMOU_F005] [decimal](15, 3) NULL,
	[AUT_BILL_AMOU_F006] [decimal](15, 3) NULL,
	[AUT_ACQU_SETT_AMOU] [decimal](15, 3) NULL,
	[AUT_ISSU_SETT_AMOU] [decimal](15, 3) NULL,
	[AUT_TRAN_DATE_TIME_F007] [date] NULL,
	[AUT_BILL_AMOU_FEES_F008] [decimal](15, 3) NULL,
	[AUT_SETT_CONV_RATE_F009] [decimal](13, 6) NULL,
	[AUT_ACQU_SETT_CONV_RATE] [decimal](13, 6) NULL,
	[AUT_ISSU_SETT_CONV_RATE] [decimal](13, 6) NULL,
	[AUT_BILL_CONV_RATE_F010] [decimal](13, 6) NULL,
	[AUT_SYST_TRAC_AUDIT_NUMB_F011] [varchar](6) NULL,
	[AUT_DATE_TIME_TRAN_F012] [date] NULL,
	[AUT_STAR_DATE_F013] [varchar](4) NULL,
	[AUT_EXPI_DATE_F014] [varchar](4) NULL,
	[AUT_SETT_DATE_F015] [varchar](4) NULL,
	[AUT_CONV_DATE_F016] [varchar](4) NULL,
	[AUT_CAPT_DATE_F017] [varchar](4) NULL,
	[AUT_MERC_TYPE_F018] [varchar](4) NULL,
	[AUT_ACQR_INST_COUN_CODE_F019] [varchar](3) NULL,
	[AUT_PAN_INST_COUN_CODE_F020] [varchar](3) NULL,
	[AUT_FORW_INST_COUN_F021] [varchar](3) NULL,
	[AUT_POS_ENT_MOD_CODE_F022] [varchar](12) NULL,
	[AUT_CARD_ENTR_CAPA_F22_01] [varchar](1) NULL,
	[AUT_CARD_AUTH_CAPA_F22_02] [varchar](1) NULL,
	[AUT_CARD_CAPT_CAPA_F22_03] [varchar](1) NULL,
	[AUT_REWR_CAPA_F22_04] [varchar](1) NULL,
	[AUT_CARD_PRSN_F22_05] [varchar](1) NULL,
	[AUT_PIN_ENTR_CAPA_F22_06] [varchar](1) NULL,
	[AUT_OPER_ENVR_F22_07] [varchar](1) NULL,
	[AUT_CARD_HLDR_PRSC_F22_08] [varchar](1) NULL,
	[AUT_CARD_DATA_INPT_MODE_F22_09] [varchar](1) NULL,
	[AUT_CARD_AUTH_MTHD_F22_10] [varchar](1) NULL,
	[AUT_CARD_AUTH_ENTY_F22_11] [varchar](1) NULL,
	[AUT_CAT_LEVEL_F22_12] [varchar](1) NULL,
	[AUT_CARD_SEQU_NUMB_F023] [varchar](3) NULL,
	[AUT_FUNC_CODE_F024] [varchar](3) NULL,
	[AUT_MESS_REAS_CODE_F025] [varchar](4) NULL,
	[AUT_PIN_CAPT_CODE_F026] [varchar](1) NULL,
	[AUT_AUTH_LENG_F027] [varchar](1) NULL,
	[AUT_TRAN_AMOU_FEE_F028] [varchar](9) NULL,
	[AUT_TRAN_AMOU_FEE_F028_1] [varchar](1) NULL,
	[AUT_TRAN_AMOU_FEE_F028_2] [decimal](15, 3) NULL,
	[AUT_SETT_AMOU_FEE_F029] [varchar](9) NULL,
	[AUT_SETT_AMOU_FEE_F029_1] [varchar](1) NULL,
	[AUT_SETT_AMOU_FEE_F029_2] [decimal](15, 3) NULL,
	[AUT_ORIG_AMOU_F030] [varchar](24) NULL,
	[AUT_ORIG_AMOU_F030_01] [decimal](15, 3) NULL,
	[AUT_ORIG_AMOU_F030_02] [decimal](15, 3) NULL,
	[AUT_ACQR_INST_ID_CODE_F032] [varchar](11) NULL,
	[AUT_FORW_INST_ID_CODE_F033] [varchar](11) NULL,
	[AUT_RETR_REF_NUMB_F037] [varchar](12) NULL,
	[AUT_AUTH_ID_RESP_F038] [varchar](6) NULL,
	[AUT_RESP_CODE_F039] [varchar](3) NULL,
	[AUT_RESP_CODE_F039_1] [varchar](1) NULL,
	[AUT_RESP_CODE_F039_2] [varchar](2) NULL,
	[AUT_SERV_CODE_F040] [varchar](3) NULL,
	[AUT_CARD_ACCP_TERM_ID_F041] [varchar](8) NULL,
	[AUT_CARD_ACCP_ID_CODE_F042] [varchar](15) NULL,
	[AUT_CARD_ACCP_NAME_LOC_F043] [varchar](40) NULL,
	[AUT_ADD_RESP_DATA_F044] [varchar](50) NULL,
	[AUT_MESS_TEXT_F044_01] [varchar](96) NULL,
	[AUT_ACQU_RESP_F044_02] [varchar](3) NULL,
	[AUT_ISSU_RESP_F044_03] [varchar](3) NULL,
	[AUT_ADDI_RESP_F044_04] [varchar](6) NULL,
	[AUT_HSM_RESP_F044_05] [varchar](4) NULL,
	[AUT_FIEL_NUMB_F044_06] [varchar](3) NULL,
	[AUT_ADDI_DATA_F048] [varchar](256) NULL,
	[AUT_MESS_F048_01] [varchar](30) NULL,
	[AUT_STAN_IN_F048_02] [varchar](1) NULL,
	[AUT_PROD_TYPE_F048_03] [varchar](10) NULL,
	[AUT_PRIM_ROUT_F048_04] [varchar](4) NULL,
	[AUT_SECO_ROUT_F048_04] [varchar](4) NULL,
	[AUT_TRAN_CATE_F048_10] [varchar](1) NULL,
	[AUT_CERT_NUMB_F048_11] [varchar](40) NULL,
	[AUT_ELEC_COMM_CERT_F048_12] [varchar](96) NULL,
	[AUT_ELEC_COMM_SECU_F048_13] [varchar](2) NULL,
	[AUT_VERI_REQU_F048_14] [varchar](2) NULL,
	[AUT_VERI_RESP_F048_15] [varchar](1) NULL,
	[AUT_CVV_PRES_INDI_F048_85] [varchar](1) NULL,
	[AUT_CVV_RESP_TYPE_F048_86] [varchar](1) NULL,
	[AUT_CVV_VALI_F048_16] [varchar](1) NULL,
	[AUT_CVV_CHAN_F048_17] [varchar](1) NULL,
	[AUT_CVV_TRAC_F048_18] [varchar](1) NULL,
	[AUT_CVV2_DATA_F048_19] [varchar](3) NULL,
	[AUT_XID_TRAN_ID_F048_09] [varchar](20) NULL,
	[AUT_TRAN_HASH_VALU_F048_10] [varchar](20) NULL,
	[AUT_TRAN_CURR_F049] [varchar](3) NULL,
	[AUT_SETT_CURR_F050] [varchar](3) NULL,
	[AUT_BILL_CURR_F051] [varchar](3) NULL,
	[AUT_ACQU_SETT_CURR] [varchar](3) NULL,
	[AUT_ISSU_SETT_CURR] [varchar](3) NULL,
	[AUT_ADDI_AMOU_F054] [varchar](40) NULL,
	[AUT_ADDI_AMOU_F054_ACCO_TYPE1] [varchar](2) NULL,
	[AUT_ADDI_AMOU_F054_AMOU_TYPE1] [varchar](2) NULL,
	[AUT_ADDI_AMOU_F054_CURR_CODE1] [varchar](3) NULL,
	[AUT_ADDI_AMOU_F054_AMOU_SIGN1] [varchar](1) NULL,
	[AUT_ADDI_AMOU_F054_AMOU1] [decimal](15, 3) NULL,
	[AUT_ADDI_AMOU_F054_ACCO_TYPE2] [varchar](2) NULL,
	[AUT_ADDI_AMOU_F054_AMOU_TYPE2] [varchar](2) NULL,
	[AUT_ADDI_AMOU_F054_CURR_CODE2] [varchar](3) NULL,
	[AUT_ADDI_AMOU_F054_AMOU_SIGN2] [varchar](1) NULL,
	[AUT_ADDI_AMOU_F054_AMOU2] [decimal](15, 3) NULL,
	[AUT_INTE_CIRC_CARD_F055] [varchar](255) NULL,
	[AUT_ICC_APPL_F055_004F] [varchar](38) NULL,
	[AUT_TRAC_DATA_F055_0057] [varchar](38) NULL,
	[AUT_ISSU_SCRI_TEM1_F055_0071] [varchar](256) NULL,
	[AUT_ISSU_SCRI_TEM2_F055_0072] [varchar](256) NULL,
	[AUT_APPL_INTE_PROF_F055_0082] [varchar](4) NULL,
	[AUT_RESE_F055_0082_01] [varchar](1) NULL,
	[AUT_OFFL_SDA_F055_0082_02] [varchar](1) NULL,
	[AUT_OFFL_DDA_F055_0082_03] [varchar](1) NULL,
	[AUT_CARD_VERI_F055_0082_04] [varchar](1) NULL,
	[AUT_TERM_RISK_F055_0082_05] [varchar](1) NULL,
	[AUT_ISSU_AUTH_F055_0082_06] [varchar](1) NULL,
	[AUT_COMB_DDA_F055_0082_07] [varchar](1) NULL,
	[AUT_RESE_F055_0082_08] [varchar](9) NULL,
	[AUT_DEDI_FILE_NAME_F055_0084] [varchar](32) NULL,
	[AUT_ISSU_SCRI_COMM_F055_0086] [varchar](42) NULL,
	[AUT_RESP_CODE_F055_008A] [varchar](2) NULL,
	[AUT_CVM_LIST_F055_008E] [varchar](256) NULL,
	[AUT_ISSU_AUTH_DATA_F055_0091] [varchar](32) NULL,
	[AUT_ARPC_CRYP_F055_0091_01] [varchar](32) NULL,
	[AUT_ARPC_RESP_F055_0091_02] [varchar](2) NULL,
	[AUT_TERM_VERI_RESU_F055_0095] [varchar](10) NULL,
	[AUT_OFFL_AUTH_F055_0095_01] [varchar](1) NULL,
	[AUT_SDA_PASS_F055_0095_02] [varchar](1) NULL,
	[AUT_CHIP_DATA_F055_0095_03] [varchar](1) NULL,
	[AUT_PAN_TERM_F055_0095_04] [varchar](1) NULL,
	[AUT_RESE_F055_0095_05] [varchar](4) NULL,
	[AUT_CHIP_TERM_F055_0095_06] [varchar](1) NULL,
	[AUT_EXPI_APPL_F055_0095_07] [varchar](1) NULL,
	[AUT_APPL_ACTI_F055_0095_08] [varchar](1) NULL,
	[AUT_SERV_ALLW_F055_0095_09] [varchar](1) NULL,
	[AUT_NEW_CARD_F055_0095_10] [varchar](1) NULL,
	[AUT_RESE_F055_0095_11] [varchar](3) NULL,
	[AUT_CARD_VERI_F055_0095_12] [varchar](1) NULL,
	[AUT_UNRE_CVM_F055_0095_13] [varchar](1) NULL,
	[AUT_PIN_TRY_F055_0095_14] [varchar](1) NULL,
	[AUT_PIN_WORK_F055_0095_15] [varchar](1) NULL,
	[AUT_PIN_PRES_F055_0095_16] [varchar](1) NULL,
	[AUT_ONLI_PIN_F055_0095_17] [varchar](1) NULL,
	[AUT_RESE_F055_0095_18] [varchar](2) NULL,
	[AUT_TRAN_LIMI_F055_0095_19] [varchar](1) NULL,
	[AUT_LOWE_LIMI_F055_0095_20] [varchar](1) NULL,
	[AUT_UPPE_LIMI_F055_0095_21] [varchar](1) NULL,
	[AUT_TRAN_RAND_F055_0095_22] [varchar](1) NULL,
	[AUT_FORC_TRAN_F055_0095_23] [varchar](1) NULL,
	[AUT_RESE_F055_0095_24] [varchar](3) NULL,
	[AUT_ISSU_AUTH_F055_0095_25] [varchar](1) NULL,
	[AUT_SCRI_BEFO_F055_0095_26] [varchar](1) NULL,
	[AUT_SCRI_AFTE_F055_0095_27] [varchar](1) NULL,
	[AUT_RESE_F055_0095_28] [varchar](4) NULL,
	[AUT_TRAN_DATE_F055_009A] [varchar](10) NULL,
	[AUT_TRAN_TYPE_F055_009C] [varchar](2) NULL,
	[AUT_APPL_EXPI_DATE_F055_5F24] [varchar](10) NULL,
	[AUT_APPL_STAR_DATE_F055_5F25] [varchar](10) NULL,
	[AUT_TRAN_CURR_F055_5F2A] [varchar](4) NULL,
	[AUT_TRAN_AMOU_F055_9F02] [varchar](15) NULL,
	[AUT_CASH_BACK_F055_9F03] [varchar](15) NULL,
	[AUT_APPL_IDEN_F055_9F06] [varchar](32) NULL,
	[AUT_APPL_USAG_CTRL_F055_9F07] [varchar](4) NULL,
	[AUT_TERM_APPL_VERS_F055_9F09] [varchar](4) NULL,
	[AUT_ISSU_APPL_DATA_F055_9F10] [varchar](64) NULL,
	[AUT_KEY_INDE_F055_9F10_01] [varchar](2) NULL,
	[AUT_CRYP_VERS_F055_9F10_02] [varchar](2) NULL,
	[AUT_SECO_CRYP_F055_9F10_03] [varchar](2) NULL,
	[AUT_FIRS_CRYP_F055_9F10_04] [varchar](2) NULL,
	[AUT_ISSU_AUTH_F055_9F10_05] [varchar](1) NULL,
	[AUT_PIN_PERF_F055_9F10_06] [varchar](1) NULL,
	[AUT_PIN_FAIL_F055_9F10_07] [varchar](1) NULL,
	[AUT_UNAB_ONLI_F055_9F10_08] [varchar](1) NULL,
	[AUT_LAST_ONLI_F055_9F10_09] [varchar](1) NULL,
	[AUT_PIN_LIMI_F055_9F10_10] [varchar](1) NULL,
	[AUT_VELO_CHEC_F055_9F10_11] [varchar](1) NULL,
	[AUT_NEW_CARD_F055_9F10_12] [varchar](1) NULL,
	[AUT_AUTH_FAIL_F055_9F10_13] [varchar](1) NULL,
	[AUT_AUTH_PERF_F055_9F10_14] [varchar](1) NULL,
	[AUT_PIN_BLOC_F055_9F10_15] [varchar](1) NULL,
	[AUT_SDA_F055_9F10_16] [varchar](1) NULL,
	[AUT_NUMB_SCRI_F055_9F10_17] [varchar](4) NULL,
	[AUT_SCRI_FAIL_F055_9F10_18] [varchar](1) NULL,
	[AUT_RESE_F055_9F10_19] [varchar](3) NULL,
	[AUT_IAC_DEFA_F055_9F0D] [varchar](10) NULL,
	[AUT_IAC_DENI_F055_9F0E] [varchar](10) NULL,
	[AUT_IAC_ONLI_F055_9F0F] [varchar](10) NULL,
	[AUT_TERM_COUN_CODE_F055_9F1A] [varchar](4) NULL,
	[AUT_INTE_DEVI_SERI_F055_9F1E] [varchar](16) NULL,
	[AUT_APPL_CRYP_F055_9F26] [varchar](16) NULL,
	[AUT_CRYP_INFO_DATA_F055_9F27] [varchar](2) NULL,
	[AUT_TERM_CAPA_F055_9F33] [varchar](6) NULL,
	[AUT_MANU_KEY_F055_9F33_01] [varchar](1) NULL,
	[AUT_MAGN_STRI_F055_9F33_02] [varchar](1) NULL,
	[AUT_CHIP_READ_F055_9F33_03] [varchar](1) NULL,
	[AUT_RESE_F055_9F33_04] [varchar](5) NULL,
	[AUT_OFFL_PIN_F055_9F33_05] [varchar](1) NULL,
	[AUT_ONLI_PIN_F055_9F33_06] [varchar](1) NULL,
	[AUT_SIGN_ALLW_F055_9F33_07] [varchar](1) NULL,
	[AUT_RESE_F055_9F33_08] [varchar](5) NULL,
	[AUT_SDA_SUPP_F055_9F33_09] [varchar](1) NULL,
	[AUT_RESE_F055_9F33_10] [varchar](1) NULL,
	[AUT_CARD_CAPT_F055_9F33_11] [varchar](1) NULL,
	[AUT_RESE_F055_9F33_12] [varchar](5) NULL,
	[AUT_CHLD_VERI_RESU_F055_9F34] [varchar](8) NULL,
	[AUT_TERM_TYPE_F055_9F35] [varchar](2) NULL,
	[AUT_APPL_TRAN_COUN_F055_9F36] [varchar](4) NULL,
	[AUT_UNPR_NUMB_F055_9F37] [varchar](8) NULL,
	[AUT_TRAN_SEQU_NUMB_F055_9F41] [varchar](8) NULL,
	[AUT_TRAN_CATE_CODE_F055_9F53] [varchar](2) NULL,
	[AUT_ICC_PROC_F055_DF80] [varchar](4) NULL,
	[AUT_APPL_TYPE_F055_DF81] [varchar](2) NULL,
	[AUT_APPL_VERI_F055_DF70] [varchar](4) NULL,
	[AUT_CALC_DATA_F055_DF71] [varchar](7) NULL,
	[AUT_ACCP_CERT_F055_FF72] [varchar](4) NULL,
	[AUT_APPL_TYPE_F055_FF73] [varchar](1) NULL,
	[AUT_CARD_APPL_CURR_F055_FF74] [varchar](3) NULL,
	[AUT_ISSU_CAI_F055_FF75] [varchar](16) NULL,
	[AUT_ISSU_INTE_DATA_F055_FF76] [varchar](12) NULL,
	[AUT_ACQU_CAI_F055_FF77] [varchar](16) NULL,
	[AUT_AQUI_DATA_F055_FF78] [varchar](36) NULL,
	[AUT_ISSU_SCRI_F055_FF00] [varchar](10) NULL,
	[AUT_ORIG_DATA_F056] [varchar](45) NULL,
	[AUT_MESS_TYPE_F056_01] [varchar](4) NULL,
	[AUT_ORIG_TRAC_AUDI_F056_02] [varchar](6) NULL,
	[AUT_ORIG_DATE_TIME_F056_03] [date] NULL,
	[AUT_ORIG_ACQU_ID_F056_04] [varchar](11) NULL,
	[AUT_ORIG_FORW_ID_F056_05] [varchar](11) NULL,
	[AUT_CPS_DATA_F062] [varchar](20) NULL,
	[AUT_AUTH_CHAR_INDI_F062_01] [varchar](1) NULL,
	[AUT_TRAN_IDEN_F062_02] [varchar](15) NULL,
	[AUT_VALI_CODE_F062_03] [varchar](4) NULL,
	[AUT_NETW_DATA_F063] [varchar](24) NULL,
	[AUT_BANK_NET_DATA_F063_01] [varchar](12) NULL,
	[AUT_NETW_ID_F063_02] [varchar](4) NULL,
	[AUT_MESS_REAS_F063_03] [varchar](4) NULL,
	[AUT_STIP_SWIT_F063_04] [varchar](4) NULL,
	[AUT_RECE_INST_COUN_F068] [varchar](3) NULL,
	[AUT_SETT_INST_COUN_F069] [varchar](3) NULL,
	[AUT_SETT_INST_ID_CODE_F099] [varchar](11) NULL,
	[AUT_RECE_INST_ID_CODE_F100] [varchar](11) NULL,
	[AUT_ACCO_ID1_F102] [varchar](28) NULL,
	[AUT_ACCO_ID2_F103] [varchar](28) NULL,
	[AUT_VALI_STAT] [varchar](4) NULL,
	[AUT_NOT_MATC_PROC_STAT] [varchar](1) NULL,
	[AUT_UPDA_ACCO] [varchar](1) NULL,
	[AUT_UPDA_CARD] [varchar](1) NULL,
	[AUT_VMT_BUSINESS_APP_ID] [varchar](2) NULL,
	[AUT_VMT_SENDER_REF_NUMB] [varchar](16) NULL,
	[AUT_VMT_SENDER_ACC_NUMB] [varchar](34) NULL,
	[AUT_VMT_SENDER_NAME] [varchar](30) NULL,
	[AUT_VMT_SENDER_ADDRESS] [varchar](35) NULL,
	[AUT_VMT_SENDER_CITY] [varchar](25) NULL,
	[AUT_VMT_SENDER_STATE_PROV] [varchar](2) NULL,
	[AUT_VMT_SENDER_COUNTRY] [varchar](3) NULL,
	[AUT_VMT_FUNDS_SOURCE] [varchar](2) NULL,
	[AUT_VMT_ADTNL_SENDER_DATA] [varchar](50) NULL,
	[ExistInClearing] [bit] NULL,
 CONSTRAINT [PK_ONLINE_AUTHORIZATION] PRIMARY KEY CLUSTERED 
(
	[AUT_CODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReplishementExact] [datetime] NULL,
	[AtmRepDate] [datetime] NULL,
	[AtmExhaustionDate] [datetime] NULL,
	[AtmAmount] [float] NULL,
	[SendAlert] [bit] NULL,
	[Orderstatut] [int] NOT NULL,
	[ActualAmount] [float] NULL,
	[Branch_Id] [int] NULL,
	[CashPoint_Id] [int] NULL,
	[CashProvider_Id] [int] NOT NULL,
	[CashStock_Id] [int] NULL,
	[OrderCategory_Id] [int] NULL,
	[OrderType_Id] [int] NULL,
	[Ordernature] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderCategory]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OrderCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderTypes]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[UnitPrice] [float] NOT NULL,
 CONSTRAINT [PK_dbo.OrderTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Package]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ZipName] [nvarchar](max) NULL,
	[ZipPath] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[CreatedById] [nvarchar](max) NULL,
	[CreatedByUserName] [nvarchar](max) NULL,
	[Type] [int] NULL,
	[Tag] [nvarchar](250) NULL,
	[HotFixId] [nvarchar](50) NULL,
	[Dynamic] [bit] NULL,
	[RebootAfter] [bit] NOT NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parameters]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parameters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[label] [nvarchar](max) NULL,
	[value] [nvarchar](max) NULL,
	[section] [nvarchar](max) NULL,
	[type] [nvarchar](max) NULL,
	[CashProvider_Id] [int] NULL,
	[CashPoint_Id] [int] NULL,
	[Atm_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Parameters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatchAtms]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatchAtms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[Patch_Id] [int] NULL,
 CONSTRAINT [PK_dbo.PatchAtms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patches]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Mandatory] [bit] NOT NULL,
	[Date] [nvarchar](50) NULL,
	[AtmProfile] [nvarchar](max) NULL,
	[Upload] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Patches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pr_DailyTransactionEvents]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pr_DailyTransactionEvents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AtmID] [nvarchar](max) NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[EventType_Id] [int] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[duration] [int] NOT NULL,
	[statutTrx] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pr_DailyTransactionEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pr_EventsErrorType]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pr_EventsErrorType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Color] [nvarchar](50) NULL,
	[Label] [nvarchar](250) NULL,
	[Highlight] [nvarchar](50) NULL,
	[Highlight_Fr] [nvarchar](250) NULL,
 CONSTRAINT [PK_dbo.Pr_EventsErrorType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pr_EventsType]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pr_EventsType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Color] [nvarchar](50) NULL,
	[Label] [nvarchar](250) NULL,
	[Highlight] [nvarchar](50) NULL,
	[Highlight_Fr] [nvarchar](250) NULL,
	[Duration] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Pr_EventsType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pr_LastDailyTransactionError]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pr_LastDailyTransactionError](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AtmID] [nvarchar](max) NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[EventType_Id] [int] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[duration] [int] NOT NULL,
	[ErrorType_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pr_LastDailyTransactionError] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pr_TransactionError]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pr_TransactionError](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AtmID] [nvarchar](max) NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[EventType_Id] [int] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[duration] [int] NOT NULL,
	[ErrorType_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pr_TransactionError] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pr_TransactionEvents]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pr_TransactionEvents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AtmID] [nvarchar](max) NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[EventType_Id] [int] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[duration] [int] NOT NULL,
	[statutTrx] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pr_TransactionEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Predictor]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Predictor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SumRetraitPredicted] [float] NULL,
	[ConsommationHier] [float] NULL,
	[MSemaineDernier] [float] NULL,
	[MSemaine7] [float] NULL,
	[ConsoMmJrAnP] [bigint] NULL,
	[ConsoMmJrMP] [bigint] NULL,
	[ConsoMMJrSmDer] [bigint] NULL,
	[MoyenneMoisPrec] [float] NULL,
	[MoyenneMMSAnPrec] [float] NULL,
	[MoyenneMMmAnPrec] [float] NULL,
	[ConsommationMaxMDer] [bigint] NULL,
	[ConsommationMaxSDer] [bigint] NULL,
	[SumRetraitReal] [bigint] NULL,
	[PidTotal] [float] NOT NULL,
	[Bais] [int] NOT NULL,
	[isFree] [bit] NOT NULL,
	[TransactionDay] [datetime] NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Predictor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PredictorAgency]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PredictorAgency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SumRetraitPredicted] [float] NULL,
	[ConsommationHier] [float] NULL,
	[MSemaineDernier] [float] NULL,
	[MSemaine7] [float] NULL,
	[ConsoMmJrAnP] [bigint] NULL,
	[ConsoMmJrMP] [bigint] NULL,
	[ConsoMMJrSmDer] [bigint] NULL,
	[MoyenneMoisPrec] [float] NULL,
	[MoyenneMMSAnPrec] [float] NULL,
	[MoyenneMMmAnPrec] [float] NULL,
	[ConsommationMaxMDer] [bigint] NULL,
	[SumRetraitReal] [bigint] NULL,
	[PidTotal] [int] NOT NULL,
	[Bais] [int] NOT NULL,
	[TransactionDay] [datetime] NOT NULL,
	[Agency_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PredictorAgency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PredictorDepositAgency]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PredictorDepositAgency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepositPredicted] [float] NULL,
	[DepositHier] [float] NULL,
	[MSemaineDernier] [float] NULL,
	[MSemaine7] [float] NULL,
	[ConsoMmJrAnP] [bigint] NULL,
	[ConsoMmJrMP] [bigint] NULL,
	[ConsoMMJrSmDer] [bigint] NULL,
	[MoyenneMoisPrec] [float] NULL,
	[MoyenneMMSAnPrec] [float] NULL,
	[MoyenneMMmAnPrec] [float] NULL,
	[DepositMaxMDer] [bigint] NULL,
	[DepositReal] [bigint] NULL,
	[PidTotal] [int] NOT NULL,
	[Bais] [int] NOT NULL,
	[TransactionDay] [datetime] NOT NULL,
	[Agency_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PredictorDepositAgency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RCTransactions]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RCTransactions](
	[FileID] [int] NOT NULL,
	[FilePosition] [nvarchar](255) NULL,
	[TCRNbr] [int] NULL,
	[TypeID] [int] NULL,
	[AtmID] [nvarchar](255) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[CardNumber] [nvarchar](255) NULL,
	[AutorisationNumber] [nvarchar](max) NULL,
	[Rib] [nvarchar](255) NULL,
	[Amount] [int] NOT NULL,
	[isCashPresented] [bit] NOT NULL,
	[isCashTaken] [bit] NOT NULL,
	[isCashRetracted] [bit] NOT NULL,
	[isCashoutError] [bit] NOT NULL,
	[ExistInHost] [bit] NOT NULL,
	[IsRejected] [bit] NOT NULL,
	[TrxPictures] [nvarchar](max) NULL,
	[isSuspecious] [bit] NOT NULL,
	[Duration] [float] NULL,
	[ExtraInfos] [nvarchar](max) NULL,
	[TerminalNbr] [bigint] NOT NULL,
	[MerchantId] [int] NULL,
	[TerminalID] [int] NULL,
	[RefferenceNumber] [nvarchar](255) NULL,
	[RemittanceNbr] [bigint] NULL,
	[CardID] [int] NULL,
	[AuthorizationCodeSourceID] [int] NULL,
	[AcquirerRefferenceNumber] [nvarchar](255) NULL,
	[VoucherNbr] [bigint] NULL,
	[TransactionCurrency] [bigint] NULL,
	[FacturationAmount] [float] NULL,
	[DestinationCurrencyCode] [nvarchar](255) NULL,
	[DestinationCurrencyExpence] [nvarchar](255) NULL,
	[InterchangeFees] [nvarchar](255) NULL,
	[InterchangeFeeCurrency] [nvarchar](255) NULL,
	[InterchangeeFeeCurrencyExpence] [nvarchar](255) NULL,
	[InterchangeFeeSign] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.RCTransactions] PRIMARY KEY CLUSTERED 
(
	[AtmID] ASC,
	[TransactionDate] ASC,
	[TransactionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecentAtmState]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecentAtmState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[State_Id] [int] NOT NULL,
	[Connected] [bit] NOT NULL,
	[LastStateType] [int] NOT NULL,
	[md5Hash] [nvarchar](max) NULL,
	[LastSeen] [datetime] NOT NULL,
	[LastTransaction] [datetime] NOT NULL,
	[NotifState] [int] NOT NULL,
	[checkflag] [bit] NOT NULL,
	[cashalert] [bit] NOT NULL,
	[LastReboot] [datetime] NOT NULL,
	[LastRebootReason] [nvarchar](max) NULL,
	[cashStock] [float] NULL,
 CONSTRAINT [PK_dbo.RecentAtmState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoCardHolder]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoCardHolder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CardID] [int] NULL,
	[PaymentProdID] [int] NULL,
	[CardNumber] [nvarchar](max) NULL,
	[ExpiryDate] [date] NULL,
	[CardVerificationID] [int] NULL,
	[CardCaptureID] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoFile]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoFile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParsingMethod] [varchar](255) NULL,
	[Nomenclature] [varchar](255) NULL,
	[Extension] [varchar](10) NULL,
	[DetailListOperations] [text] NULL,
 CONSTRAINT [PK__RecoFile__3214EC27AEE97B80] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoFileInfo]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoFileInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[HashCode] [nvarchar](max) NULL,
	[ParsingDate] [datetime] NULL,
	[ParsingMethode] [nvarchar](max) NULL,
	[ParsingVersion] [int] NULL,
 CONSTRAINT [PK__RecoFile__3214EC27C71336B5] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoFraud]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoFraud](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionRouteIndicator] [int] NULL,
	[ForwardingInstitutionID] [nvarchar](100) NULL,
	[ReceivingInstitutionID] [nvarchar](100) NULL,
	[CardNumber] [nvarchar](max) NULL,
	[ARN] [nvarchar](max) NULL,
	[TransactionDate] [datetime] NULL,
	[MerchantID] [int] NULL,
	[FraudAmount] [float] NULL,
	[FraudCurrCode] [nvarchar](max) NULL,
	[NotificationCode] [nvarchar](50) NULL,
	[AccountSequenceNumber] [nvarchar](50) NULL,
	[InsuranceYear] [int] NULL,
	[FraudType] [nvarchar](50) NULL,
	[DebitCreditIndicator] [nvarchar](10) NULL,
	[TransactionGenerationMethod] [nvarchar](50) NULL,
	[ElectronicCommerceIndicator] [nvarchar](50) NULL,
 CONSTRAINT [PK__RecoFrau__55433A4BA3373E59] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoMerchant]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoMerchant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantNbr] [bigint] NULL,
	[MerchantName] [nvarchar](max) NULL,
	[MerchantCity] [nvarchar](max) NULL,
	[MerchantCategoryCode] [bigint] NULL,
	[MerchantType] [nvarchar](max) NULL,
	[MerchantCountryCode] [bigint] NULL,
	[MerchantProvinceCode] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoParams]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoParams](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileType] [varchar](255) NULL,
	[ParsingMethode] [varchar](255) NULL,
	[Features] [text] NULL,
	[DetailTrans] [text] NULL,
	[Active] [bit] NULL,
	[HostPath] [nvarchar](max) NULL,
	[ArchivePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_RecoPara_3214EC2769ADCE20] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoParams_TransationType]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoParams_TransationType](
	[RecoParamsId] [int] NOT NULL,
	[TransationTypeId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_RecoParams_TransationType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoParsingResultFile]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoParsingResultFile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[HashCode] [nvarchar](max) NULL,
	[ParsingDate] [datetime] NULL,
	[ParsingMethode] [nvarchar](max) NULL,
	[ParsingVersion] [int] NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[NbrTransactions] [bigint] NULL,
	[MontantTotal] [bigint] NULL,
	[NbrGAB] [bigint] NULL,
	[FileType] [nvarchar](max) NULL,
	[NbrLignes] [bigint] NULL,
	[FileStatus] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoTransaction]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoTransaction](
	[FileID] [int] NOT NULL,
	[AtmID] [nvarchar](128) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[AutorisationNumber] [nvarchar](max) NULL,
	[CardNumber] [nvarchar](max) NULL,
	[Rib] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Amount] [int] NOT NULL,
	[Statut] [int] NOT NULL,
	[isCashPresented] [bit] NOT NULL,
	[isCashTaken] [bit] NOT NULL,
	[isCashRetracted] [bit] NOT NULL,
	[isCashoutError] [bit] NOT NULL,
	[ExistInHost] [bit] NOT NULL,
	[IsRejected] [bit] NOT NULL,
	[TrxPictures] [nvarchar](max) NULL,
	[Duration] [float] NULL,
	[ExtraInfos] [nvarchar](max) NULL,
	[isSuspecious] [bit] NULL,
 CONSTRAINT [PK_dbo.RecoTransaction] PRIMARY KEY CLUSTERED 
(
	[AtmID] ASC,
	[TransactionDate] ASC,
	[TransactionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoTransactionType]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoTransactionType](
	[Id] [int] NOT NULL,
	[TransactionCode] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Label] [nvarchar](250) NULL,
	[Label_fr] [nvarchar](250) NULL,
	[Color] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[Address_Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[abbreviation] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.Region] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReservationStatut] [int] NOT NULL,
	[ReservationType] [int] NOT NULL,
	[Client_Id] [int] NOT NULL,
	[CashProvider_Id] [int] NOT NULL,
	[Total] [bigint] NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Reservation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Result]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Result](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Agency_Id] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[RetraitPredit] [bigint] NULL,
	[DepotPredit] [bigint] NULL,
	[AlimentationPredite] [bigint] NULL,
	[CollectePredite] [bigint] NULL,
	[SoldeFinJourneePredit] [bigint] NULL,
 CONSTRAINT [PK_dbo.Result] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RklAtmProfile]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RklAtmProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DecryptKey] [nvarchar](max) NULL,
	[SigKey] [nvarchar](max) NULL,
	[PublicKeyEncoding] [nvarchar](max) NULL,
	[HashIdentifier] [nvarchar](max) NULL,
	[PublicKeyAtm] [nvarchar](max) NULL,
	[SigKeyAtm] [nvarchar](max) NULL,
	[AtmProfileId] [int] NOT NULL,
 CONSTRAINT [PK_RklAtmProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RklKeyUse]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RklKeyUse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KeyName] [nvarchar](max) NULL,
	[dwUse] [nvarchar](max) NULL,
	[Use] [nvarchar](max) NULL,
	[RklProfileId] [int] NOT NULL,
 CONSTRAINT [PK_RklKeyUse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleSousMenus]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleSousMenus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRole] [nvarchar](max) NULL,
	[IdSousMenu] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.RoleSousMenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SousMenus]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SousMenus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name_Fr] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Lien] [nvarchar](max) NULL,
	[Class] [nvarchar](max) NULL,
	[Script] [nvarchar](max) NULL,
	[Controller] [nvarchar](max) NULL,
	[Module] [nvarchar](max) NULL,
	[Menu_Id] [int] NOT NULL,
	[AdminOnly] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.SousMenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateType_Id] [int] NOT NULL,
	[StateDate] [datetime] NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
	[LastTransaction] [datetime] NOT NULL,
	[msc_state] [int] NOT NULL,
	[ToSave] [bit] NOT NULL,
	[HasErrors] [nvarchar](190) NULL,
 CONSTRAINT [PK_dbo.State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateFieldInt]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateFieldInt](
	[ComponentState_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [int] NOT NULL,
	[ComponentState_State_Id] [int] NULL,
	[ComponentState_Component_Id] [int] NULL,
 CONSTRAINT [PK_dbo.StateFieldInt] PRIMARY KEY CLUSTERED 
(
	[ComponentState_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateFieldStr]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateFieldStr](
	[ComponentState_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
	[ComponentState_State_Id] [int] NULL,
	[ComponentState_Component_Id] [int] NULL,
 CONSTRAINT [PK_dbo.StateFieldStr] PRIMARY KEY CLUSTERED 
(
	[ComponentState_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateJob]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateJob](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.StateJob] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateType]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CssClass] [nvarchar](max) NULL,
	[Color] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.StateType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempAlim]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempAlim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntryTime] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
	[Filename] [nvarchar](max) NULL,
	[EntryType] [int] NOT NULL,
	[Atm_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.TempAlim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[Path] [nvarchar](max) NULL,
	[Params] [nvarchar](max) NULL,
	[SmsContent] [nvarchar](max) NULL,
	[Content_fr] [nvarchar](max) NULL,
	[Template_pdf] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Templates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainData1]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainData1](
	[AtmID] [nvarchar](255) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[CardNumber] [varchar](255) NULL,
	[isCashPresented_toInt] [float] NULL,
	[isCashTaken_toInt] [float] NULL,
	[isCashRetracted_toInt] [float] NULL,
	[isCashoutError_toInt] [float] NULL,
	[ExistInHost_toInt] [float] NULL,
	[Type_encoded_scaled] [float] NULL,
	[Amount_scaled] [float] NULL,
	[Statut_scaled] [float] NULL,
	[CardNumber_encoded_scaled] [float] NULL,
	[time_between_transactions_scaled] [float] NULL,
	[frequecy_per_hour_scaled] [float] NULL,
	[Average_Amount] [float] NULL,
	[Accumulated_Amount] [float] NULL,
	[min] [float] NULL,
	[max] [float] NULL,
	[Lat] [float] NULL,
	[Long] [float] NULL,
	[Distance_between_transactions] [float] NULL,
	[speed] [float] NULL,
	[Average_Speed] [float] NULL,
	[VelocityByLocation] [float] NULL,
	[StandardDeviation] [float] NULL,
	[LocationName_encoded] [float] NULL,
	[is_HM_Fraud] [bit] NULL,
	[is_IF_Fraud] [bit] NULL,
	[is_RC_Fraud] [bit] NULL,
	[is_TimeDiff_Fraud] [bit] NULL,
	[is_UnusualHour_Fraud] [bit] NULL,
	[is_Location_Fraud] [bit] NULL,
	[PINError] [bit] NULL,
	[isnot_Consistent] [bit] NULL,
	[ExpiredCard] [bit] NULL,
	[NbrDeclinedTransctions] [float] NULL,
	[ManyDeclinedTransactions] [bit] NULL,
	[isMultiplePINEntry] [bit] NULL,
	[IsLateNightOrEarlyMorning] [bit] NULL,
	[FrequencyChange] [bit] NULL,
	[isRepeated] [bit] NULL,
	[is_belongsToOrder] [bit] NULL,
	[is_Fraud] [bit] NULL,
 CONSTRAINT [PK_TrainData1] PRIMARY KEY CLUSTERED 
(
	[AtmID] ASC,
	[TransactionDate] ASC,
	[TransactionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainData2]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainData2](
	[AtmID] [nvarchar](255) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[CardNumber] [varchar](255) NULL,
	[isCashPresented_toInt] [float] NULL,
	[isCashTaken_toInt] [float] NULL,
	[isCashRetracted_toInt] [float] NULL,
	[isCashoutError_toInt] [float] NULL,
	[ExistInHost_toInt] [float] NULL,
	[Type_encoded_scaled] [float] NULL,
	[Amount_scaled] [float] NULL,
	[Statut_scaled] [float] NULL,
	[CardNumber_encoded_scaled] [float] NULL,
	[time_between_transactions_scaled] [float] NULL,
	[frequecy_per_hour_scaled] [float] NULL,
	[Average_Amount] [float] NULL,
	[Accumulated_Amount] [float] NULL,
	[min] [float] NULL,
	[max] [float] NULL,
	[Lat] [float] NULL,
	[Long] [float] NULL,
	[Distance_between_transactions] [float] NULL,
	[speed] [float] NULL,
	[Average_Speed] [float] NULL,
	[VelocityByLocation] [float] NULL,
	[StandardDeviation] [float] NULL,
	[LocationName_encoded] [float] NULL,
	[is_HM_Fraud] [float] NULL,
	[is_IF_Fraud] [float] NULL,
	[is_RC_Fraud] [float] NULL,
	[is_TimeDiff_Fraud] [float] NULL,
	[is_UnusualHour_Fraud] [float] NULL,
	[is_Location_Fraud] [float] NULL,
	[PINError] [float] NULL,
	[isnot_Consistent] [float] NULL,
	[ExpiredCard] [float] NULL,
	[NbrDeclinedTransctions] [float] NULL,
	[ManyDeclinedTransactions] [float] NULL,
	[isMultiplePINEntry] [float] NULL,
	[IsLateNightOrEarlyMorning] [float] NULL,
	[FrequencyChange] [float] NULL,
	[isRepeated] [float] NULL,
	[is_belongsToOrder] [float] NULL,
	[is_Fraud] [bit] NULL,
 CONSTRAINT [PK_TrainData2] PRIMARY KEY CLUSTERED 
(
	[AtmID] ASC,
	[TransactionDate] ASC,
	[TransactionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionEcart]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionEcart](
	[AtmID] [nvarchar](128) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[AutorisationNumber] [nvarchar](max) NULL,
	[CardNumber] [nvarchar](max) NULL,
	[Rib] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Amount] [int] NOT NULL,
	[Statut] [int] NOT NULL,
	[isCashPresented] [bit] NOT NULL,
	[isCashTaken] [bit] NOT NULL,
	[isCashRetracted] [bit] NOT NULL,
	[isCashoutError] [bit] NOT NULL,
	[ExistInHost] [bit] NOT NULL,
	[IsRejected] [bit] NOT NULL,
	[IsSuspected] [bit] NOT NULL,
	[TrxPictures] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.TransactionEcart] PRIMARY KEY CLUSTERED 
(
	[AtmID] ASC,
	[TransactionDate] ASC,
	[TransactionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransationType]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransationType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Color] [nvarchar](50) NULL,
	[Highlight] [nvarchar](50) NULL,
	[Label] [nvarchar](250) NULL,
	[Label_fr] [nvarchar](250) NULL,
 CONSTRAINT [PK_dbo.TransationType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TreeViewDetail]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreeViewDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [nvarchar](max) NULL,
	[FilsId] [nvarchar](max) NULL,
	[ParentType] [int] NOT NULL,
	[FilsType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.TreeViewDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAtm]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAtm](
	[User_Id] [nvarchar](128) NOT NULL,
	[Atm_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserAtm] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC,
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSessionInfo]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSessionInfo](
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Id] [nvarchar](255) NOT NULL,
	[AdressIP] [nvarchar](max) NULL,
	[UserId] [nvarchar](150) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[view_authorization]    Script Date: 02/12/2024 14:45:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[view_authorization](
	[RETR_REF_NUMB] [nvarchar](100) NOT NULL,
	[CARD_ACCP_TERM_ID] [nvarchar](100) NOT NULL,
	[SYST_DATETIME] [datetime] NOT NULL,
	[TRANACTION_CODE] [nvarchar](100) NULL,
	[AUTH_CODE] [nvarchar](100) NULL,
	[CARD_NUMB] [nvarchar](114) NULL,
	[CARD_EXPI_DATE] [nvarchar](100) NULL,
	[TRAN_AMOUNT] [int] NOT NULL,
	[BILL_AMOUT] [int] NOT NULL,
	[TRAN_CURR] [nvarchar](100) NULL,
	[DEST_INTE_IDEN] [nvarchar](100) NULL,
	[CONFIRM_STATUS] [nvarchar](100) NULL,
	[RESP_CODE] [nvarchar](100) NULL,
	[REVE_STAT] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.view_authorization] PRIMARY KEY CLUSTERED 
(
	[RETR_REF_NUMB] ASC,
	[CARD_ACCP_TERM_ID] ASC,
	[SYST_DATETIME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Template_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Template_Id] ON [dbo].[Alerts]
(
	[Template_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AlertId]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_AlertId] ON [dbo].[Arguments]
(
	[AlertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AtmArretJournee_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_AtmArretJournee_Id] ON [dbo].[ArretCassetteStock]
(
	[AtmArretJournee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Branch_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Branch_Id] ON [dbo].[ArretJourne]
(
	[Branch_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashStock_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashStock_Id] ON [dbo].[ArretJourne]
(
	[CashStock_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 02/12/2024 14:45:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 02/12/2024 14:45:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Address_Id] ON [dbo].[Atm]
(
	[Address_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Agency_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Agency_Id] ON [dbo].[Atm]
(
	[Agency_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashProvider_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashProvider_Id] ON [dbo].[Atm]
(
	[CashProvider_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[AtmArreteJoune]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_State_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_State_Id] ON [dbo].[AtmArreteJoune]
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[AtmCashAlert]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_State_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_State_Id] ON [dbo].[AtmCashAlert]
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[AtmCommError]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LastState_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_LastState_Id] ON [dbo].[AtmCommError]
(
	[LastState_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[AtmContact]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contact_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Contact_Id] ON [dbo].[AtmContact]
(
	[Contact_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ActionCorrective_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_ActionCorrective_Id] ON [dbo].[AtmError]
(
	[ActionCorrective_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[AtmError]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_State_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_State_Id] ON [dbo].[AtmError]
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[AtmRemarque]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[AVAtmConfig]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Address_Id] ON [dbo].[Branch]
(
	[Address_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ActionCorrective_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_ActionCorrective_Id] ON [dbo].[Bug]
(
	[ActionCorrective_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AtmError_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_AtmError_Id] ON [dbo].[Bug]
(
	[AtmError_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BugCategory_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_BugCategory_Id] ON [dbo].[Bug]
(
	[BugCategory_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BugPriority_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_BugPriority_Id] ON [dbo].[Bug]
(
	[BugPriority_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BugStatut_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_BugStatut_Id] ON [dbo].[Bug]
(
	[BugStatut_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[BugAtm]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bug_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Bug_Id] ON [dbo].[BugAtm]
(
	[Bug_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bug_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Bug_Id] ON [dbo].[BugAttachment]
(
	[Bug_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bug_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Bug_Id] ON [dbo].[BugComment]
(
	[Bug_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bug_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Bug_Id] ON [dbo].[BugComponent]
(
	[Bug_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Component_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Component_Id] ON [dbo].[BugComponent]
(
	[Component_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bug_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Bug_Id] ON [dbo].[BugHistory]
(
	[Bug_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Address_Id] ON [dbo].[CashPoint]
(
	[Address_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Profile_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Profile_Id] ON [dbo].[CashPoint]
(
	[Profile_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashPoint_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashPoint_Id] ON [dbo].[CashPointContact]
(
	[CashPoint_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contact_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Contact_Id] ON [dbo].[CashPointContact]
(
	[Contact_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_State_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_State_Id] ON [dbo].[CashStock]
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashStock_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashStock_Id] ON [dbo].[CassetteStock]
(
	[CashStock_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Agency_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Agency_Id] ON [dbo].[Clients]
(
	[Agency_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[CommandControl]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Command_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Command_Id] ON [dbo].[CommandControl]
(
	[Command_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Component_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Component_Id] ON [dbo].[ComponentState]
(
	[Component_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_State_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_State_Id] ON [dbo].[ComponentState]
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NodeType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_NodeType_Id] ON [dbo].[ContextualMenu]
(
	[NodeType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashPoint_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashPoint_Id] ON [dbo].[DoneOrder]
(
	[CashPoint_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashProvider_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashProvider_Id] ON [dbo].[DoneOrder]
(
	[CashProvider_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StateAfter_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_StateAfter_Id] ON [dbo].[DoneOrder]
(
	[StateAfter_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StateBefore_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_StateBefore_Id] ON [dbo].[DoneOrder]
(
	[StateBefore_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Agency_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Agency_Id] ON [dbo].[DoneOrderAgency]
(
	[Agency_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashProvider_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashProvider_Id] ON [dbo].[DoneOrderAgency]
(
	[CashProvider_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StateAfter_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_StateAfter_Id] ON [dbo].[DoneOrderAgency]
(
	[StateAfter_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StateBefore_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_StateBefore_Id] ON [dbo].[DoneOrderAgency]
(
	[StateBefore_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Agency_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Agency_Id] ON [dbo].[EncaisseMax]
(
	[Agency_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bug_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Bug_Id] ON [dbo].[ErrTypeId]
(
	[Bug_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_State_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_State_Id] ON [dbo].[ErrTypeId]
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [FactorsIndex]    Script Date: 02/12/2024 14:45:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [FactorsIndex] ON [dbo].[Factor]
(
	[TransactionDay] ASC,
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [FactorsIndex]    Script Date: 02/12/2024 14:45:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [FactorsIndex] ON [dbo].[FactorAgency]
(
	[Agency_Id] ASC,
	[TransactionDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [FactorsIndex]    Script Date: 02/12/2024 14:45:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [FactorsIndex] ON [dbo].[FactorDepositAgency]
(
	[Agency_Id] ASC,
	[TransactionDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[Incident]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_InvoiceID]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_InvoiceID] ON [dbo].[InvoiceItems]
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderTypeID]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_OrderTypeID] ON [dbo].[InvoiceItems]
(
	[OrderTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashProvider_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashProvider_Id] ON [dbo].[Invoices]
(
	[CashProvider_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_JobType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_JobType_Id] ON [dbo].[Job]
(
	[JobType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[JobAtm]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Job_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Job_Id] ON [dbo].[JobAtm]
(
	[Job_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[JobAtmExecutionResult]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Job_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Job_Id] ON [dbo].[JobAtmExecutionResult]
(
	[Job_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Command_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Command_Id] ON [dbo].[JobCommand]
(
	[Command_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Job_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Job_Id] ON [dbo].[JobCommand]
(
	[Job_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[JobCommandExecutionResult]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Command_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Command_Id] ON [dbo].[JobCommandExecutionResult]
(
	[Command_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Job_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Job_Id] ON [dbo].[JobCommandExecutionResult]
(
	[Job_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[JournalEntry]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[JournalEntryMontly]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashPoint_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashPoint_Id] ON [dbo].[Matelas]
(
	[CashPoint_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashStock_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashStock_Id] ON [dbo].[Matelas]
(
	[CashStock_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashStock_Id2]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashStock_Id2] ON [dbo].[Matelas]
(
	[CashStock_Id2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Branch_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Branch_Id] ON [dbo].[Order]
(
	[Branch_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashPoint_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashPoint_Id] ON [dbo].[Order]
(
	[CashPoint_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashProvider_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashProvider_Id] ON [dbo].[Order]
(
	[CashProvider_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashStock_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashStock_Id] ON [dbo].[Order]
(
	[CashStock_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderCategory_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_OrderCategory_Id] ON [dbo].[Order]
(
	[OrderCategory_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_OrderType_Id] ON [dbo].[Order]
(
	[OrderType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[Parameters]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashPoint_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashPoint_Id] ON [dbo].[Parameters]
(
	[CashPoint_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashProvider_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashProvider_Id] ON [dbo].[Parameters]
(
	[CashProvider_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[PatchAtms]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Patch_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Patch_Id] ON [dbo].[PatchAtms]
(
	[Patch_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_EventType_Id] ON [dbo].[Pr_DailyTransactionEvents]
(
	[EventType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ErrorType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_ErrorType_Id] ON [dbo].[Pr_LastDailyTransactionError]
(
	[ErrorType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_EventType_Id] ON [dbo].[Pr_LastDailyTransactionError]
(
	[EventType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ErrorType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_ErrorType_Id] ON [dbo].[Pr_TransactionError]
(
	[ErrorType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_EventType_Id] ON [dbo].[Pr_TransactionError]
(
	[EventType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_EventType_Id] ON [dbo].[Pr_TransactionEvents]
(
	[EventType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PredictorIndex]    Script Date: 02/12/2024 14:45:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [PredictorIndex] ON [dbo].[Predictor]
(
	[Atm_Id] ASC,
	[TransactionDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [FactorsIndex]    Script Date: 02/12/2024 14:45:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [FactorsIndex] ON [dbo].[PredictorAgency]
(
	[Agency_Id] ASC,
	[TransactionDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [FactorsIndex]    Script Date: 02/12/2024 14:45:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [FactorsIndex] ON [dbo].[PredictorDepositAgency]
(
	[Agency_Id] ASC,
	[TransactionDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[RecentAtmState]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_State_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_State_Id] ON [dbo].[RecentAtmState]
(
	[State_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Address_Id] ON [dbo].[Region]
(
	[Address_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CashProvider_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_CashProvider_Id] ON [dbo].[Reservation]
(
	[CashProvider_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Client_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Client_Id] ON [dbo].[Reservation]
(
	[Client_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Agency_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Agency_Id] ON [dbo].[Result]
(
	[Agency_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Menu_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Menu_Id] ON [dbo].[SousMenus]
(
	[Menu_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[State]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StateType_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_StateType_Id] ON [dbo].[State]
(
	[StateType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ComponentState_State_Id_ComponentState_Component_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_ComponentState_State_Id_ComponentState_Component_Id] ON [dbo].[StateFieldInt]
(
	[ComponentState_State_Id] ASC,
	[ComponentState_Component_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ComponentState_State_Id_ComponentState_Component_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_ComponentState_State_Id_ComponentState_Component_Id] ON [dbo].[StateFieldStr]
(
	[ComponentState_State_Id] ASC,
	[ComponentState_Component_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[TempAlim]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Atm_Id]    Script Date: 02/12/2024 14:45:52 ******/
CREATE NONCLUSTERED INDEX [IX_Atm_Id] ON [dbo].[UserAtm]
(
	[Atm_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Atm] ADD  CONSTRAINT [DF_Atm_Actif]  DEFAULT ((1)) FOR [Actif]
GO
ALTER TABLE [dbo].[AVTransaction] ADD  DEFAULT ((0)) FOR [is_FraudP]
GO
ALTER TABLE [dbo].[Command] ADD  CONSTRAINT [DF_Command_TimeOut]  DEFAULT ((60)) FOR [TimeOut]
GO
ALTER TABLE [dbo].[Command] ADD  CONSTRAINT [DF_Command_IsJobCmd]  DEFAULT ((1)) FOR [IsJobCmd]
GO
ALTER TABLE [dbo].[ErrorType] ADD  DEFAULT ((1)) FOR [IsFailure]
GO
ALTER TABLE [dbo].[Job] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[JobAtmExecutionResult] ADD  DEFAULT ((1)) FOR [JobControle_Id]
GO
ALTER TABLE [dbo].[JobCommand] ADD  CONSTRAINT [DF_JobCommand_CanIgnore]  DEFAULT ((0)) FOR [CanIgnore]
GO
ALTER TABLE [dbo].[JobCommandExecutionResult] ADD  DEFAULT ((1)) FOR [JobControle_Id]
GO
ALTER TABLE [dbo].[JobControles] ADD  CONSTRAINT [DF_JobControles_NumberOfReexecution]  DEFAULT ((0)) FOR [NumberOfReexecution]
GO
ALTER TABLE [dbo].[LastDealyTrx] ADD  DEFAULT ((0)) FOR [is_FraudP]
GO
ALTER TABLE [dbo].[ONLINE_AUTHORIZATION] ADD  CONSTRAINT [DF_ONLINE_AUTHORIZATION_ExistInClearing]  DEFAULT ((0)) FOR [ExistInClearing]
GO
ALTER TABLE [dbo].[Package] ADD  DEFAULT ((0)) FOR [RebootAfter]
GO
ALTER TABLE [dbo].[RecentAtmState] ADD  CONSTRAINT [DF_RecentAtmState_LastTransaction]  DEFAULT (((1)/(1))/(1970)) FOR [LastTransaction]
GO
ALTER TABLE [dbo].[RecoParams] ADD  DEFAULT ((0)) FOR [Active]
GO
ALTER TABLE [dbo].[RecoTransaction] ADD  DEFAULT ((0)) FOR [isSuspecious]
GO
ALTER TABLE [dbo].[SousMenus] ADD  DEFAULT ((0)) FOR [AdminOnly]
GO
ALTER TABLE [dbo].[State] ADD  DEFAULT ((0)) FOR [ToSave]
GO
ALTER TABLE [dbo].[Alerts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Alerts_dbo.Templates_Template_Id] FOREIGN KEY([Template_Id])
REFERENCES [dbo].[Templates] ([Id])
GO
ALTER TABLE [dbo].[Alerts] CHECK CONSTRAINT [FK_dbo.Alerts_dbo.Templates_Template_Id]
GO
ALTER TABLE [dbo].[Arguments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Arguments_dbo.Alerts_AlertId] FOREIGN KEY([AlertId])
REFERENCES [dbo].[Alerts] ([Id])
GO
ALTER TABLE [dbo].[Arguments] CHECK CONSTRAINT [FK_dbo.Arguments_dbo.Alerts_AlertId]
GO
ALTER TABLE [dbo].[ArretCassetteStock]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ArretCassetteStock_dbo.AtmArreteJoune_AtmArretJournee_Id] FOREIGN KEY([AtmArretJournee_Id])
REFERENCES [dbo].[AtmArreteJoune] ([Id])
GO
ALTER TABLE [dbo].[ArretCassetteStock] CHECK CONSTRAINT [FK_dbo.ArretCassetteStock_dbo.AtmArreteJoune_AtmArretJournee_Id]
GO
ALTER TABLE [dbo].[ArretJourne]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ArretJourne_dbo.Branch_Branch_Id] FOREIGN KEY([Branch_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[ArretJourne] CHECK CONSTRAINT [FK_dbo.ArretJourne_dbo.Branch_Branch_Id]
GO
ALTER TABLE [dbo].[ArretJourne]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ArretJourne_dbo.CashStock_CashStock_Id] FOREIGN KEY([CashStock_Id])
REFERENCES [dbo].[CashStock] ([Id])
GO
ALTER TABLE [dbo].[ArretJourne] CHECK CONSTRAINT [FK_dbo.ArretJourne_dbo.CashStock_CashStock_Id]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Atm]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Atm_dbo.Address_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Atm] CHECK CONSTRAINT [FK_dbo.Atm_dbo.Address_Address_Id]
GO
ALTER TABLE [dbo].[Atm]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Atm_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Atm] CHECK CONSTRAINT [FK_dbo.Atm_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[Atm]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Atm_dbo.CashProvider_CashProvider_Id] FOREIGN KEY([CashProvider_Id])
REFERENCES [dbo].[CashProvider] ([Id])
GO
ALTER TABLE [dbo].[Atm] CHECK CONSTRAINT [FK_dbo.Atm_dbo.CashProvider_CashProvider_Id]
GO
ALTER TABLE [dbo].[AtmArreteJoune]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmArreteJoune_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[AtmArreteJoune] CHECK CONSTRAINT [FK_dbo.AtmArreteJoune_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[AtmArreteJoune]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmArreteJoune_dbo.State_State_Id] FOREIGN KEY([State_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[AtmArreteJoune] CHECK CONSTRAINT [FK_dbo.AtmArreteJoune_dbo.State_State_Id]
GO
ALTER TABLE [dbo].[AtmCashAlert]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmCashAlert_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[AtmCashAlert] CHECK CONSTRAINT [FK_dbo.AtmCashAlert_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[AtmCashAlert]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmCashAlert_dbo.State_State_Id] FOREIGN KEY([State_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[AtmCashAlert] CHECK CONSTRAINT [FK_dbo.AtmCashAlert_dbo.State_State_Id]
GO
ALTER TABLE [dbo].[AtmCashAlertExhaution]  WITH CHECK ADD FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[AtmCashAlertExhaution]  WITH CHECK ADD FOREIGN KEY([State_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[AtmCommError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmCommError_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[AtmCommError] CHECK CONSTRAINT [FK_dbo.AtmCommError_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[AtmCommError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmCommError_dbo.State_LastState_Id] FOREIGN KEY([LastState_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[AtmCommError] CHECK CONSTRAINT [FK_dbo.AtmCommError_dbo.State_LastState_Id]
GO
ALTER TABLE [dbo].[AtmContact]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmContact_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[AtmContact] CHECK CONSTRAINT [FK_dbo.AtmContact_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[AtmContact]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmContact_dbo.Contact_Contact_Id] FOREIGN KEY([Contact_Id])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[AtmContact] CHECK CONSTRAINT [FK_dbo.AtmContact_dbo.Contact_Contact_Id]
GO
ALTER TABLE [dbo].[AtmError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmError_dbo.ActionCorrective_ActionCorrective_Id] FOREIGN KEY([ActionCorrective_Id])
REFERENCES [dbo].[ActionCorrective] ([Id])
GO
ALTER TABLE [dbo].[AtmError] CHECK CONSTRAINT [FK_dbo.AtmError_dbo.ActionCorrective_ActionCorrective_Id]
GO
ALTER TABLE [dbo].[AtmError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmError_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[AtmError] CHECK CONSTRAINT [FK_dbo.AtmError_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[AtmError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmError_dbo.State_State_Id] FOREIGN KEY([State_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[AtmError] CHECK CONSTRAINT [FK_dbo.AtmError_dbo.State_State_Id]
GO
ALTER TABLE [dbo].[AtmRemarque]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtmRemarque_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[AtmRemarque] CHECK CONSTRAINT [FK_dbo.AtmRemarque_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[AVAtmConfig]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AVAtmConfig_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[AVAtmConfig] CHECK CONSTRAINT [FK_dbo.AVAtmConfig_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[BinConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_BinConfiguration_BinCategory] FOREIGN KEY([BinCategory_Id])
REFERENCES [dbo].[BinCategory] ([Id])
GO
ALTER TABLE [dbo].[BinConfiguration] CHECK CONSTRAINT [FK_BinConfiguration_BinCategory]
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Branch_dbo.Address_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_dbo.Branch_dbo.Address_Address_Id]
GO
ALTER TABLE [dbo].[Bug]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Bug_dbo.ActionCorrective_ActionCorrective_Id] FOREIGN KEY([ActionCorrective_Id])
REFERENCES [dbo].[ActionCorrective] ([Id])
GO
ALTER TABLE [dbo].[Bug] CHECK CONSTRAINT [FK_dbo.Bug_dbo.ActionCorrective_ActionCorrective_Id]
GO
ALTER TABLE [dbo].[Bug]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Bug_dbo.AtmError_AtmError_Id] FOREIGN KEY([AtmError_Id])
REFERENCES [dbo].[AtmError] ([Id])
GO
ALTER TABLE [dbo].[Bug] CHECK CONSTRAINT [FK_dbo.Bug_dbo.AtmError_AtmError_Id]
GO
ALTER TABLE [dbo].[Bug]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Bug_dbo.BugCategory_BugCategory_Id] FOREIGN KEY([BugCategory_Id])
REFERENCES [dbo].[BugCategory] ([Id])
GO
ALTER TABLE [dbo].[Bug] CHECK CONSTRAINT [FK_dbo.Bug_dbo.BugCategory_BugCategory_Id]
GO
ALTER TABLE [dbo].[Bug]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Bug_dbo.BugPriority_BugPriority_Id] FOREIGN KEY([BugPriority_Id])
REFERENCES [dbo].[BugPriority] ([Id])
GO
ALTER TABLE [dbo].[Bug] CHECK CONSTRAINT [FK_dbo.Bug_dbo.BugPriority_BugPriority_Id]
GO
ALTER TABLE [dbo].[Bug]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Bug_dbo.BugStatut_BugStatut_Id] FOREIGN KEY([BugStatut_Id])
REFERENCES [dbo].[BugStatut] ([Id])
GO
ALTER TABLE [dbo].[Bug] CHECK CONSTRAINT [FK_dbo.Bug_dbo.BugStatut_BugStatut_Id]
GO
ALTER TABLE [dbo].[BugAtm]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BugAtm_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[BugAtm] CHECK CONSTRAINT [FK_dbo.BugAtm_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[BugAtm]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BugAtm_dbo.Bug_Bug_Id] FOREIGN KEY([Bug_Id])
REFERENCES [dbo].[Bug] ([Id])
GO
ALTER TABLE [dbo].[BugAtm] CHECK CONSTRAINT [FK_dbo.BugAtm_dbo.Bug_Bug_Id]
GO
ALTER TABLE [dbo].[BugAttachment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BugAttachment_dbo.Bug_Bug_Id] FOREIGN KEY([Bug_Id])
REFERENCES [dbo].[Bug] ([Id])
GO
ALTER TABLE [dbo].[BugAttachment] CHECK CONSTRAINT [FK_dbo.BugAttachment_dbo.Bug_Bug_Id]
GO
ALTER TABLE [dbo].[BugComment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BugComment_dbo.Bug_Bug_Id] FOREIGN KEY([Bug_Id])
REFERENCES [dbo].[Bug] ([Id])
GO
ALTER TABLE [dbo].[BugComment] CHECK CONSTRAINT [FK_dbo.BugComment_dbo.Bug_Bug_Id]
GO
ALTER TABLE [dbo].[BugComponent]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BugComponent_dbo.Bug_Bug_Id] FOREIGN KEY([Bug_Id])
REFERENCES [dbo].[Bug] ([Id])
GO
ALTER TABLE [dbo].[BugComponent] CHECK CONSTRAINT [FK_dbo.BugComponent_dbo.Bug_Bug_Id]
GO
ALTER TABLE [dbo].[BugComponent]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BugComponent_dbo.Component_Component_Id] FOREIGN KEY([Component_Id])
REFERENCES [dbo].[Component] ([Id])
GO
ALTER TABLE [dbo].[BugComponent] CHECK CONSTRAINT [FK_dbo.BugComponent_dbo.Component_Component_Id]
GO
ALTER TABLE [dbo].[BugHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BugHistory_dbo.Bug_Bug_Id] FOREIGN KEY([Bug_Id])
REFERENCES [dbo].[Bug] ([Id])
GO
ALTER TABLE [dbo].[BugHistory] CHECK CONSTRAINT [FK_dbo.BugHistory_dbo.Bug_Bug_Id]
GO
ALTER TABLE [dbo].[CashPoint]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CashPoint_dbo.Address_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[CashPoint] CHECK CONSTRAINT [FK_dbo.CashPoint_dbo.Address_Address_Id]
GO
ALTER TABLE [dbo].[CashPoint]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CashPoint_dbo.CashPointProfile_Profile_Id] FOREIGN KEY([Profile_Id])
REFERENCES [dbo].[CashPointProfile] ([Id])
GO
ALTER TABLE [dbo].[CashPoint] CHECK CONSTRAINT [FK_dbo.CashPoint_dbo.CashPointProfile_Profile_Id]
GO
ALTER TABLE [dbo].[CashPointContact]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CashPointContact_dbo.CashPoint_CashPoint_Id] FOREIGN KEY([CashPoint_Id])
REFERENCES [dbo].[CashPoint] ([Id])
GO
ALTER TABLE [dbo].[CashPointContact] CHECK CONSTRAINT [FK_dbo.CashPointContact_dbo.CashPoint_CashPoint_Id]
GO
ALTER TABLE [dbo].[CashPointContact]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CashPointContact_dbo.Contact_Contact_Id] FOREIGN KEY([Contact_Id])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[CashPointContact] CHECK CONSTRAINT [FK_dbo.CashPointContact_dbo.Contact_Contact_Id]
GO
ALTER TABLE [dbo].[CashStock]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CashStock_dbo.State_State_Id] FOREIGN KEY([State_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[CashStock] CHECK CONSTRAINT [FK_dbo.CashStock_dbo.State_State_Id]
GO
ALTER TABLE [dbo].[CassetteStock]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CassetteStock_dbo.CashStock_CashStock_Id] FOREIGN KEY([CashStock_Id])
REFERENCES [dbo].[CashStock] ([Id])
GO
ALTER TABLE [dbo].[CassetteStock] CHECK CONSTRAINT [FK_dbo.CassetteStock_dbo.CashStock_CashStock_Id]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Clients_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_dbo.Clients_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[ComponentState]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ComponentState_dbo.Component_Component_Id] FOREIGN KEY([Component_Id])
REFERENCES [dbo].[Component] ([Id])
GO
ALTER TABLE [dbo].[ComponentState] CHECK CONSTRAINT [FK_dbo.ComponentState_dbo.Component_Component_Id]
GO
ALTER TABLE [dbo].[ComponentState]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ComponentState_dbo.State_State_Id] FOREIGN KEY([State_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[ComponentState] CHECK CONSTRAINT [FK_dbo.ComponentState_dbo.State_State_Id]
GO
ALTER TABLE [dbo].[ContextualMenu]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ContextualMenu_dbo.NodeType_NodeType_Id] FOREIGN KEY([NodeType_Id])
REFERENCES [dbo].[NodeType] ([Id])
GO
ALTER TABLE [dbo].[ContextualMenu] CHECK CONSTRAINT [FK_dbo.ContextualMenu_dbo.NodeType_NodeType_Id]
GO
ALTER TABLE [dbo].[DoneOrder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DoneOrder_dbo.CashPoint_CashPoint_Id] FOREIGN KEY([CashPoint_Id])
REFERENCES [dbo].[CashPoint] ([Id])
GO
ALTER TABLE [dbo].[DoneOrder] CHECK CONSTRAINT [FK_dbo.DoneOrder_dbo.CashPoint_CashPoint_Id]
GO
ALTER TABLE [dbo].[DoneOrder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DoneOrder_dbo.CashProvider_CashProvider_Id] FOREIGN KEY([CashProvider_Id])
REFERENCES [dbo].[CashProvider] ([Id])
GO
ALTER TABLE [dbo].[DoneOrder] CHECK CONSTRAINT [FK_dbo.DoneOrder_dbo.CashProvider_CashProvider_Id]
GO
ALTER TABLE [dbo].[DoneOrder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DoneOrder_dbo.State_StateAfter_Id] FOREIGN KEY([StateAfter_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[DoneOrder] CHECK CONSTRAINT [FK_dbo.DoneOrder_dbo.State_StateAfter_Id]
GO
ALTER TABLE [dbo].[DoneOrder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DoneOrder_dbo.State_StateBefore_Id] FOREIGN KEY([StateBefore_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[DoneOrder] CHECK CONSTRAINT [FK_dbo.DoneOrder_dbo.State_StateBefore_Id]
GO
ALTER TABLE [dbo].[DoneOrderAgency]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DoneOrderAgency_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[DoneOrderAgency] CHECK CONSTRAINT [FK_dbo.DoneOrderAgency_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[DoneOrderAgency]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DoneOrderAgency_dbo.CashProvider_CashProvider_Id] FOREIGN KEY([CashProvider_Id])
REFERENCES [dbo].[CashProvider] ([Id])
GO
ALTER TABLE [dbo].[DoneOrderAgency] CHECK CONSTRAINT [FK_dbo.DoneOrderAgency_dbo.CashProvider_CashProvider_Id]
GO
ALTER TABLE [dbo].[DoneOrderAgency]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DoneOrderAgency_dbo.State_StateAfter_Id] FOREIGN KEY([StateAfter_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[DoneOrderAgency] CHECK CONSTRAINT [FK_dbo.DoneOrderAgency_dbo.State_StateAfter_Id]
GO
ALTER TABLE [dbo].[DoneOrderAgency]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DoneOrderAgency_dbo.State_StateBefore_Id] FOREIGN KEY([StateBefore_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[DoneOrderAgency] CHECK CONSTRAINT [FK_dbo.DoneOrderAgency_dbo.State_StateBefore_Id]
GO
ALTER TABLE [dbo].[EncaisseMax]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EncaisseMax_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[EncaisseMax] CHECK CONSTRAINT [FK_dbo.EncaisseMax_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[ErrTypeId]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ErrTypeId_dbo.Bug_Bug_Id] FOREIGN KEY([Bug_Id])
REFERENCES [dbo].[Bug] ([Id])
GO
ALTER TABLE [dbo].[ErrTypeId] CHECK CONSTRAINT [FK_dbo.ErrTypeId_dbo.Bug_Bug_Id]
GO
ALTER TABLE [dbo].[ErrTypeId]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ErrTypeId_dbo.State_State_Id] FOREIGN KEY([State_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[ErrTypeId] CHECK CONSTRAINT [FK_dbo.ErrTypeId_dbo.State_State_Id]
GO
ALTER TABLE [dbo].[Factor]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Factor_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[Factor] CHECK CONSTRAINT [FK_dbo.Factor_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[FactorAgency]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FactorAgency_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[FactorAgency] CHECK CONSTRAINT [FK_dbo.FactorAgency_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[FactorDepositAgency]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FactorDepositAgency_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[FactorDepositAgency] CHECK CONSTRAINT [FK_dbo.FactorDepositAgency_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[Im_Atm_Inventory]  WITH CHECK ADD  CONSTRAINT [Im_Atm_Inventory_fk0] FOREIGN KEY([Atm_id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[Im_Atm_Inventory] CHECK CONSTRAINT [Im_Atm_Inventory_fk0]
GO
ALTER TABLE [dbo].[Im_Atm_Inventory]  WITH CHECK ADD  CONSTRAINT [Im_Atm_Inventory_fk1] FOREIGN KEY([Computer_id])
REFERENCES [dbo].[Im_Computer_Inventory] ([id])
GO
ALTER TABLE [dbo].[Im_Atm_Inventory] CHECK CONSTRAINT [Im_Atm_Inventory_fk1]
GO
ALTER TABLE [dbo].[Im_Move_Inventory]  WITH CHECK ADD  CONSTRAINT [Im_Move_Inventory_fk0] FOREIGN KEY([AtmInventory_id])
REFERENCES [dbo].[Im_Atm_Inventory] ([id])
GO
ALTER TABLE [dbo].[Im_Move_Inventory] CHECK CONSTRAINT [Im_Move_Inventory_fk0]
GO
ALTER TABLE [dbo].[Incident]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Incident_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[Incident] CHECK CONSTRAINT [FK_dbo.Incident_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[InvoiceItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.InvoiceItems_dbo.Invoices_InvoiceID] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoices] ([ID])
GO
ALTER TABLE [dbo].[InvoiceItems] CHECK CONSTRAINT [FK_dbo.InvoiceItems_dbo.Invoices_InvoiceID]
GO
ALTER TABLE [dbo].[InvoiceItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.InvoiceItems_dbo.OrderTypes_OrderTypeID] FOREIGN KEY([OrderTypeID])
REFERENCES [dbo].[OrderTypes] ([ID])
GO
ALTER TABLE [dbo].[InvoiceItems] CHECK CONSTRAINT [FK_dbo.InvoiceItems_dbo.OrderTypes_OrderTypeID]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Invoices_dbo.CashProvider_CashProvider_Id] FOREIGN KEY([CashProvider_Id])
REFERENCES [dbo].[CashProvider] ([Id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_dbo.Invoices_dbo.CashProvider_CashProvider_Id]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Job_dbo.JobType_JobType_Id] FOREIGN KEY([JobType_Id])
REFERENCES [dbo].[JobType] ([Id])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_dbo.Job_dbo.JobType_JobType_Id]
GO
ALTER TABLE [dbo].[JobAtm]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobAtm_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[JobAtm] CHECK CONSTRAINT [FK_dbo.JobAtm_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[JobAtm]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobAtm_dbo.Job_Job_Id] FOREIGN KEY([Job_Id])
REFERENCES [dbo].[Job] ([Id])
GO
ALTER TABLE [dbo].[JobAtm] CHECK CONSTRAINT [FK_dbo.JobAtm_dbo.Job_Job_Id]
GO
ALTER TABLE [dbo].[JobAtmExecutionResult]  WITH CHECK ADD  CONSTRAINT [FK__JobAtmExe__JobCo__184C96B4] FOREIGN KEY([JobControle_Id])
REFERENCES [dbo].[JobControles] ([Id])
GO
ALTER TABLE [dbo].[JobAtmExecutionResult] CHECK CONSTRAINT [FK__JobAtmExe__JobCo__184C96B4]
GO
ALTER TABLE [dbo].[JobAtmExecutionResult]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobAtmExecutionResult_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[JobAtmExecutionResult] CHECK CONSTRAINT [FK_dbo.JobAtmExecutionResult_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[JobAtmExecutionResult]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobAtmExecutionResult_dbo.Job_Job_Id] FOREIGN KEY([Job_Id])
REFERENCES [dbo].[Job] ([Id])
GO
ALTER TABLE [dbo].[JobAtmExecutionResult] CHECK CONSTRAINT [FK_dbo.JobAtmExecutionResult_dbo.Job_Job_Id]
GO
ALTER TABLE [dbo].[JobCommand]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobCommand_dbo.Command_Command_Id] FOREIGN KEY([Command_Id])
REFERENCES [dbo].[Command] ([Id])
GO
ALTER TABLE [dbo].[JobCommand] CHECK CONSTRAINT [FK_dbo.JobCommand_dbo.Command_Command_Id]
GO
ALTER TABLE [dbo].[JobCommand]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobCommand_dbo.Job_Job_Id] FOREIGN KEY([Job_Id])
REFERENCES [dbo].[Job] ([Id])
GO
ALTER TABLE [dbo].[JobCommand] CHECK CONSTRAINT [FK_dbo.JobCommand_dbo.Job_Job_Id]
GO
ALTER TABLE [dbo].[JobCommandExecutionResult]  WITH CHECK ADD  CONSTRAINT [FK__JobComman__JobCo__1A34DF26] FOREIGN KEY([JobControle_Id])
REFERENCES [dbo].[JobControles] ([Id])
GO
ALTER TABLE [dbo].[JobCommandExecutionResult] CHECK CONSTRAINT [FK__JobComman__JobCo__1A34DF26]
GO
ALTER TABLE [dbo].[JobCommandExecutionResult]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobCommandExecutionResult_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[JobCommandExecutionResult] CHECK CONSTRAINT [FK_dbo.JobCommandExecutionResult_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[JobCommandExecutionResult]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobCommandExecutionResult_dbo.Command_Command_Id] FOREIGN KEY([Command_Id])
REFERENCES [dbo].[Command] ([Id])
GO
ALTER TABLE [dbo].[JobCommandExecutionResult] CHECK CONSTRAINT [FK_dbo.JobCommandExecutionResult_dbo.Command_Command_Id]
GO
ALTER TABLE [dbo].[JobCommandExecutionResult]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobCommandExecutionResult_dbo.Job_Job_Id] FOREIGN KEY([Job_Id])
REFERENCES [dbo].[Job] ([Id])
GO
ALTER TABLE [dbo].[JobCommandExecutionResult] CHECK CONSTRAINT [FK_dbo.JobCommandExecutionResult_dbo.Job_Job_Id]
GO
ALTER TABLE [dbo].[JobControles]  WITH CHECK ADD  CONSTRAINT [FK__JobContro__Job_I__147C05D0] FOREIGN KEY([Job_Id])
REFERENCES [dbo].[Job] ([Id])
GO
ALTER TABLE [dbo].[JobControles] CHECK CONSTRAINT [FK__JobContro__Job_I__147C05D0]
GO
ALTER TABLE [dbo].[JournalEntry]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JournalEntry_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[JournalEntry] CHECK CONSTRAINT [FK_dbo.JournalEntry_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[JournalEntryMontly]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JournalEntryMontly_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[JournalEntryMontly] CHECK CONSTRAINT [FK_dbo.JournalEntryMontly_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[Matelas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Matelas_dbo.CashPoint_CashPoint_Id] FOREIGN KEY([CashPoint_Id])
REFERENCES [dbo].[CashPoint] ([Id])
GO
ALTER TABLE [dbo].[Matelas] CHECK CONSTRAINT [FK_dbo.Matelas_dbo.CashPoint_CashPoint_Id]
GO
ALTER TABLE [dbo].[Matelas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Matelas_dbo.CashStock_CashStock_Id] FOREIGN KEY([CashStock_Id])
REFERENCES [dbo].[CashStock] ([Id])
GO
ALTER TABLE [dbo].[Matelas] CHECK CONSTRAINT [FK_dbo.Matelas_dbo.CashStock_CashStock_Id]
GO
ALTER TABLE [dbo].[Matelas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Matelas_dbo.CashStock_CashStock_Id2] FOREIGN KEY([CashStock_Id2])
REFERENCES [dbo].[CashStock] ([Id])
GO
ALTER TABLE [dbo].[Matelas] CHECK CONSTRAINT [FK_dbo.Matelas_dbo.CashStock_CashStock_Id2]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.Branch_Branch_Id] FOREIGN KEY([Branch_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.Branch_Branch_Id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.CashPoint_CashPoint_Id] FOREIGN KEY([CashPoint_Id])
REFERENCES [dbo].[CashPoint] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.CashPoint_CashPoint_Id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.CashProvider_CashProvider_Id] FOREIGN KEY([CashProvider_Id])
REFERENCES [dbo].[CashProvider] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.CashProvider_CashProvider_Id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.CashStock_CashStock_Id] FOREIGN KEY([CashStock_Id])
REFERENCES [dbo].[CashStock] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.CashStock_CashStock_Id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.OrderCategory_OrderCategory_Id] FOREIGN KEY([OrderCategory_Id])
REFERENCES [dbo].[OrderCategory] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.OrderCategory_OrderCategory_Id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.OrderTypes_OrderType_Id] FOREIGN KEY([OrderType_Id])
REFERENCES [dbo].[OrderTypes] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.OrderTypes_OrderType_Id]
GO
ALTER TABLE [dbo].[Parameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Parameters_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[Parameters] CHECK CONSTRAINT [FK_dbo.Parameters_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[Parameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Parameters_dbo.CashPoint_CashPoint_Id] FOREIGN KEY([CashPoint_Id])
REFERENCES [dbo].[CashPoint] ([Id])
GO
ALTER TABLE [dbo].[Parameters] CHECK CONSTRAINT [FK_dbo.Parameters_dbo.CashPoint_CashPoint_Id]
GO
ALTER TABLE [dbo].[Parameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Parameters_dbo.CashProvider_CashProvider_Id] FOREIGN KEY([CashProvider_Id])
REFERENCES [dbo].[CashProvider] ([Id])
GO
ALTER TABLE [dbo].[Parameters] CHECK CONSTRAINT [FK_dbo.Parameters_dbo.CashProvider_CashProvider_Id]
GO
ALTER TABLE [dbo].[PatchAtms]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PatchAtms_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[PatchAtms] CHECK CONSTRAINT [FK_dbo.PatchAtms_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[PatchAtms]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PatchAtms_dbo.Patches_Patch_Id] FOREIGN KEY([Patch_Id])
REFERENCES [dbo].[Patches] ([Id])
GO
ALTER TABLE [dbo].[PatchAtms] CHECK CONSTRAINT [FK_dbo.PatchAtms_dbo.Patches_Patch_Id]
GO
ALTER TABLE [dbo].[Pr_DailyTransactionEvents]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pr_DailyTransactionEvents_dbo.Pr_EventsType_EventType_Id] FOREIGN KEY([EventType_Id])
REFERENCES [dbo].[Pr_EventsType] ([Id])
GO
ALTER TABLE [dbo].[Pr_DailyTransactionEvents] CHECK CONSTRAINT [FK_dbo.Pr_DailyTransactionEvents_dbo.Pr_EventsType_EventType_Id]
GO
ALTER TABLE [dbo].[Pr_LastDailyTransactionError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pr_LastDailyTransactionError_dbo.Pr_EventsErrorType_ErrorType_Id] FOREIGN KEY([ErrorType_Id])
REFERENCES [dbo].[Pr_EventsErrorType] ([Id])
GO
ALTER TABLE [dbo].[Pr_LastDailyTransactionError] CHECK CONSTRAINT [FK_dbo.Pr_LastDailyTransactionError_dbo.Pr_EventsErrorType_ErrorType_Id]
GO
ALTER TABLE [dbo].[Pr_LastDailyTransactionError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pr_LastDailyTransactionError_dbo.Pr_EventsType_EventType_Id] FOREIGN KEY([EventType_Id])
REFERENCES [dbo].[Pr_EventsType] ([Id])
GO
ALTER TABLE [dbo].[Pr_LastDailyTransactionError] CHECK CONSTRAINT [FK_dbo.Pr_LastDailyTransactionError_dbo.Pr_EventsType_EventType_Id]
GO
ALTER TABLE [dbo].[Pr_TransactionError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pr_TransactionError_dbo.Pr_EventsErrorType_ErrorType_Id] FOREIGN KEY([ErrorType_Id])
REFERENCES [dbo].[Pr_EventsErrorType] ([Id])
GO
ALTER TABLE [dbo].[Pr_TransactionError] CHECK CONSTRAINT [FK_dbo.Pr_TransactionError_dbo.Pr_EventsErrorType_ErrorType_Id]
GO
ALTER TABLE [dbo].[Pr_TransactionError]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pr_TransactionError_dbo.Pr_EventsType_EventType_Id] FOREIGN KEY([EventType_Id])
REFERENCES [dbo].[Pr_EventsType] ([Id])
GO
ALTER TABLE [dbo].[Pr_TransactionError] CHECK CONSTRAINT [FK_dbo.Pr_TransactionError_dbo.Pr_EventsType_EventType_Id]
GO
ALTER TABLE [dbo].[Pr_TransactionEvents]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pr_TransactionEvents_dbo.Pr_EventsType_EventType_Id] FOREIGN KEY([EventType_Id])
REFERENCES [dbo].[Pr_EventsType] ([Id])
GO
ALTER TABLE [dbo].[Pr_TransactionEvents] CHECK CONSTRAINT [FK_dbo.Pr_TransactionEvents_dbo.Pr_EventsType_EventType_Id]
GO
ALTER TABLE [dbo].[Predictor]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Predictor_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[Predictor] CHECK CONSTRAINT [FK_dbo.Predictor_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[PredictorAgency]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PredictorAgency_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[PredictorAgency] CHECK CONSTRAINT [FK_dbo.PredictorAgency_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[PredictorDepositAgency]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PredictorDepositAgency_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[PredictorDepositAgency] CHECK CONSTRAINT [FK_dbo.PredictorDepositAgency_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[RecentAtmState]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RecentAtmState_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[RecentAtmState] CHECK CONSTRAINT [FK_dbo.RecentAtmState_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[RecentAtmState]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RecentAtmState_dbo.State_State_Id] FOREIGN KEY([State_Id])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[RecentAtmState] CHECK CONSTRAINT [FK_dbo.RecentAtmState_dbo.State_State_Id]
GO
ALTER TABLE [dbo].[RecoParams_TransationType]  WITH CHECK ADD FOREIGN KEY([RecoParamsId])
REFERENCES [dbo].[RecoParams] ([ID])
GO
ALTER TABLE [dbo].[RecoParams_TransationType]  WITH CHECK ADD FOREIGN KEY([TransationTypeId])
REFERENCES [dbo].[TransationType] ([Id])
GO
ALTER TABLE [dbo].[Region]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Region_dbo.Address_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Region] CHECK CONSTRAINT [FK_dbo.Region_dbo.Address_Address_Id]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reservation_dbo.CashProvider_CashProvider_Id] FOREIGN KEY([CashProvider_Id])
REFERENCES [dbo].[CashProvider] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_dbo.Reservation_dbo.CashProvider_CashProvider_Id]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reservation_dbo.Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_dbo.Reservation_dbo.Clients_Client_Id]
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Result_dbo.Branch_Agency_Id] FOREIGN KEY([Agency_Id])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [FK_dbo.Result_dbo.Branch_Agency_Id]
GO
ALTER TABLE [dbo].[SousMenus]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SousMenus_dbo.Menus_Menu_Id] FOREIGN KEY([Menu_Id])
REFERENCES [dbo].[Menus] ([Id])
GO
ALTER TABLE [dbo].[SousMenus] CHECK CONSTRAINT [FK_dbo.SousMenus_dbo.Menus_Menu_Id]
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_dbo.State_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_dbo.State_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_dbo.State_dbo.StateType_StateType_Id] FOREIGN KEY([StateType_Id])
REFERENCES [dbo].[StateType] ([Id])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_dbo.State_dbo.StateType_StateType_Id]
GO
ALTER TABLE [dbo].[StateFieldInt]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StateFieldInt_dbo.ComponentState_ComponentState_State_Id_ComponentState_Component_Id] FOREIGN KEY([ComponentState_State_Id], [ComponentState_Component_Id])
REFERENCES [dbo].[ComponentState] ([State_Id], [Component_Id])
GO
ALTER TABLE [dbo].[StateFieldInt] CHECK CONSTRAINT [FK_dbo.StateFieldInt_dbo.ComponentState_ComponentState_State_Id_ComponentState_Component_Id]
GO
ALTER TABLE [dbo].[StateFieldStr]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StateFieldStr_dbo.ComponentState_ComponentState_State_Id_ComponentState_Component_Id] FOREIGN KEY([ComponentState_State_Id], [ComponentState_Component_Id])
REFERENCES [dbo].[ComponentState] ([State_Id], [Component_Id])
GO
ALTER TABLE [dbo].[StateFieldStr] CHECK CONSTRAINT [FK_dbo.StateFieldStr_dbo.ComponentState_ComponentState_State_Id_ComponentState_Component_Id]
GO
ALTER TABLE [dbo].[TempAlim]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TempAlim_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[TempAlim] CHECK CONSTRAINT [FK_dbo.TempAlim_dbo.Atm_Atm_Id]
GO
ALTER TABLE [dbo].[UserAtm]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserAtm_dbo.Atm_Atm_Id] FOREIGN KEY([Atm_Id])
REFERENCES [dbo].[Atm] ([Id])
GO
ALTER TABLE [dbo].[UserAtm] CHECK CONSTRAINT [FK_dbo.UserAtm_dbo.Atm_Atm_Id]
GO
/****** Object:  StoredProcedure [dbo].[CurrentFactoreswithoutNulls]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[CurrentFactoreswithoutNulls] 

@AtmId as nvarchar(128) ,
@CurrentDay as Date 
as 

Declare 
        @MSemaineDernier real,
		@MSemaine7 real,
		@ConsoMmJrAnP bigint,
		@ConsoMmJrMP bigint,
		@weekMonth int,
		@ConsoMMJrSmDer bigint,
		@weekDay int ,
		@Jour int,
		@MoyenneMoisPrec real,
		@MoyenneMMSAnPrec real,
		@MoyenneMMmAnPrec real,
		@ConsommationMaxMDer bigint,
		@ConsommationMaxSDer bigint,
		@PoidJr int ,
        @PoidName float ,
		@PoidTot float,
		@Bais int,
		@SumRetrait bigint,
		@TopTransactionDay Date,
		@ConsommationHier bigint,
		@NombreJour int,
		@Val1 bigint,
		@Val2 bigint,
		@isFree float,
		@Start Date,
		@End Date

begin
set @isFree =0
 set  @ConsoMmJrAnP=(select coalesce (SumRetrait,0 ) from [dbo].[Factor] where cast( TransactionDay as Date)=cast ( dATEADD(year,-1,@CurrentDay) as Date) and  Atm_Id=@AtmId)
 if(@ConsoMmJrAnP is null)
 set @ConsoMmJrAnP=0
 set @SumRetrait=(select coalesce (SumRetrait,0 ) from [dbo].[Factor] where cast( TransactionDay as Date)=cast (@CurrentDay as Date)  and  Atm_Id=@AtmId)
  if(@SumRetrait is null)
 set @SumRetrait=0
 set  @MSemaineDernier =(select coalesce(AVG(SumRetrait),0) from [dbo].[Factor]  where  Datepart(week,TransactionDay)=DATEPART( week ,dATEADD(week,-1,@CurrentDay)) and Datepart(year,TransactionDay)=Datepart(year,dATEADD(week,-1,@CurrentDay)) and SumRetrait is not null  and  Atm_Id=@AtmId)
   if(@MSemaineDernier is null)
 set @MSemaineDernier=0
 set  @MSemaine7 =(select coalesce(Avg(SumRetrait),0) from [dbo].[Factor] where  cast (TransactionDay as Date) between DATEADD(day,-7,@CurrentDay) and DATEADD(day,-1,@CurrentDay) and SumRetrait is not null and  Atm_Id=@AtmId  )
   if(@MSemaine7 is null)
 set @MSemaine7=0
 set  @ConsoMmJrMP=(select coalesce (SumRetrait,0 ) from [dbo].[Factor]  where cast( TransactionDay as Date)=cast ( dATEADD(month,-1,@CurrentDay) as Date)    and  Atm_Id=@AtmId )
  if(@ConsoMmJrMP is null)
 set @ConsoMmJrMP=0
 set @ConsoMMJrSmDer=(select coalesce (SumRetrait,0 ) from [dbo].[Factor]  where DATEPART(week,TransactionDay)=DATEPART( week ,dATEADD(week,-1,@CurrentDay)) and DATENAME(dw,TransactionDay)=DATENAME(dw,@CurrentDay) and DATEPART(year,TransactionDay)=DATEPART(year,dATEADD(week,-1,@CurrentDay)) and  Atm_Id=@AtmId )
   if(@ConsoMMJrSmDer is null)
 set @ConsoMMJrSmDer=0
 set @MoyenneMoisPrec =(select coalesce(AVG(SumRetrait),0) from [dbo].[Factor] where datepart(Month,TransactionDay)=datepart(Month,dateadd(Month,-1,@CurrentDay)) and datepart(year,TransactionDay)=datepart(year,dateadd(Month,-1,@CurrentDay)) and SumRetrait is not null and  Atm_Id=@AtmId  )
    if(@MoyenneMoisPrec is null)
 set @MoyenneMoisPrec=0
 set @MoyenneMMSAnPrec  =(select coalesce(AVG(SumRetrait),0) from [dbo].[Factor] where Datepart (week, TransactionDay)=Datepart (week, @CurrentDay) and  Datepart (year, TransactionDay)=Datepart (year,dateadd(year,-1, @CurrentDay) ) and SumRetrait is not null  and  Atm_Id=@AtmId)
     if(@MoyenneMMSAnPrec is null)
 set @MoyenneMMSAnPrec=0
 set @MoyenneMMmAnPrec=(select coalesce(AVG(SumRetrait),0) from [dbo].[Factor] where datepart(year,TransactionDay)=datepart(year,@CurrentDay)-1  and datepart(Month,TransactionDay)=datepart(Month,@CurrentDay) and SumRetrait is not null and Atm_Id=@AtmId )
       if(@MoyenneMMmAnPrec is null)
 set @MoyenneMMmAnPrec=0
  set  @weekMonth = (select Top 1 intweekMonth from [dbo].[Factor] where cast( TransactionDay as Date)=cast (@CurrentDay as Date))
 set @ConsommationMaxSDer   =(select coalesce (Max(SumRetrait),0 ) from [dbo].[Factor] where datepart(week,TransactionDay)=datepart(week,Dateadd(week,-1,@CurrentDay)) and  Datepart(Month,TransactionDay)=Datepart(Month,Dateadd(week,-1,@CurrentDay)) and Datepart(year,TransactionDay)=Datepart(year,Dateadd(week,-1,@CurrentDay) ) and Atm_Id=@AtmId )
        if(@ConsommationMaxSDer is null)
 set @ConsommationMaxSDer=0
 set @ConsommationMaxMDer   =(select coalesce(Max(SumRetrait),0) from [dbo].[Factor] where Datepart(Month,TransactionDay)=Datepart(Month,Dateadd(Month,-1,@CurrentDay)) and Datepart(year,TransactionDay)=Datepart(year,Dateadd(Month,-1,@CurrentDay) )and Atm_Id=@AtmId )
         if(@ConsommationMaxMDer is null)
 set @ConsommationMaxMDer=0
 set @Bais =1

/******************POID DU jOUR******************/

   if (   DATEPART(day ,@CurrentDay)>=1 and   DATEPART(day ,@CurrentDay)<5)
   begin 
   set @PoidJr =4  
   end
   if (    DATEPART(day ,@CurrentDay)>=5 and   DATEPART(day ,@CurrentDay)<=18)
   begin
   set @PoidJr=2
   end
   if (    DATEPART(day ,@CurrentDay)>18  and    DATEPART(day ,@CurrentDay)<26)
   begin
   set @PoidJr=1
   end
   if (    DATEPART(day ,@CurrentDay)>=26 )
   begin
   set @PoidJr=4
   end

/****************Poid Name Du jour*****************/
   set @Val1 = (select coalesce(sum(SumRetrait),0) as Total
from (
 select top 4 SumRetrait from dbo.Factor where DATENAME(dw,TransactionDay)=DATENAME(dw,@CurrentDay)
  and  datepart(Month,TransactionDay)=datepart(Month,dateadd(Month,-1,@CurrentDay)) and 
  datepart(year,TransactionDay)=datepart(year,dateadd(Month,-1,@CurrentDay))
 and Atm_Id=@AtmId order by cast(TransactionDay as Date) ASC ) as t)
 set @Val2 =(select  coalesce(sum(SumRetrait),0) from  dbo.Factor where Atm_Id=@AtmId and  datepart(Month,TransactionDay)=datepart(Month,dateadd(Month,-1,@CurrentDay)) and datepart(year,TransactionDay)=datepart(year,dateadd(Month,-1,@CurrentDay)))
 
 set @poidName=coalesce((cast(@Val1 as float)/Nullif(cast(@Val2 as float),0) *cast((7/4)*10 as float)),0)
/***********************PoidTotal*****************/
 set @PoidTot =@PoidName*@PoidJr
/***********************Is Free*****************/
Declare Curs Cursor
for select  cast([Start] as Date),cast([End] as Date) from  dbo.Holyday
open Curs 
Fetch Next from Curs into @Start,@End
while @@FETCH_STATUS=0
begin
if(@CurrentDay  between @Start and @End or @CurrentDay= cast (Dateadd(day,1,@Start) as Date) or @CurrentDay=cast( Dateadd(day,-1,@Start) as Date) )
BEGIN
set @isfree=1
END


Fetch Next from Curs into @Start,@End
end 
close Curs
Deallocate Curs
/*************** La Requete D'insertion************************************/
 insert into dbo.Predictor([TransactionDay]
      ,[Atm_Id]
      ,[MSemaineDernier]
      ,[MSemaine7]
      ,[ConsoMmJrAnP]
      ,[ConsoMmJrMP]
      ,[ConsoMMJrSmDer]
      ,[MoyenneMoisPrec]
      ,[MoyenneMMSAnPrec]
      ,[MoyenneMMmAnPrec]
      ,[ConsommationMaxMDer]
	  ,[ConsommationMaxSDer]
      ,[SumRetraitReal]
      ,[PidTotal]
      ,[Bais]
	  ,[isFree])
 values(@CurrentDay,
       @AtmId,
	   @MSemaineDernier,
	   @MSemaine7,
	   @ConsoMmJrAnP,
       @ConsoMmJrMP,
       @ConsoMMJrSmDer,
       @MoyenneMoisPrec,
       @MoyenneMMSAnPrec,
       @MoyenneMMmAnPrec,
       @ConsommationMaxMDer,
	   @ConsommationMaxSDer,
       @SumRetrait,
       @PoidTot,
       @Bais,
	   @isfree)

 set @TopTransactionDay =(Select top(1) cast (TransactionDay as Date) from dbo.Predictor  where  Atm_Id=@AtmId order by TransactionDay Asc) 
 if(@TopTransactionDay = @CurrentDay)
 begin
  set @ConsommationHier=( select coalesce(sum(Amount),0)  from  [dbo].[AVTransaction] WHERE AtmId =@AtmId  and cast(TransactionDate as Date)= DateAdd(dy,-1,@TopTransactionDay) and isCashPresented= 1 )
  update dbo.Predictor set ConsommationHier=@ConsommationHier where cast(TransactionDay as Date)=@TopTransactionDay AND Atm_Id=@AtmId


 end
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteStates]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStates]
AS

Declare @state int

BEGIN
DECLARE mycursFinal CURSOR
for  select TOP (100) id  from AtmView.dbo.State --where  id not in (select State_Id from AtmView.dbo.AtmError) and id not in (select StateBefore_Id from AtmView.dbo.DoneOrder) and id not in (select StateAfter_Id from AtmView.dbo.DoneOrder) and id not in (select State_id from AtmView.dbo.RecentAtmState)
open mycursFinal
FETCH NEXT FROM mycursFinal INTO @state 
While @@FETCH_STATUS = 0
begin
delete from AtmView.dbo.StateFieldInt where ComponentState_State_Id=@state
delete from AtmView.dbo.StateFieldStr where ComponentState_State_Id=@state
delete from AtmView.dbo.ComponentState where State_Id=@state

delete from AtmView.dbo.CassetteStock where CashStock_Id in (select CashStock_Id from AtmView.dbo.CashStock where State_Id=@state)
delete from AtmView.dbo.CashStock where State_Id=@state
delete from AtmView.dbo.ErrTypeId where State_Id=@state

delete from AtmView.dbo.State where Id=@state

 fetch Next from mycursFinal INTO @state 
END
close mycursFinal
Deallocate mycursFinal
END
GO
/****** Object:  StoredProcedure [dbo].[Epuisement]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[Epuisement] 
as
begin 
Declare @AtmId nvarchar(max)
Declare @LastDoneOrderDate Datetime
Declare @LastDoneOrderAmount bigint
Declare @TransactionAmount bigint
Declare @LastTransactionDate Datetime
Declare @ThresholdExaustion int
Declare @ThresholdWarning int
Declare @Amount bigint
Declare @Id int
Declare @LastBug int
Declare @IdDoneOrder int
set @ThresholdExaustion =(select cast(value as int) from [AVData].[dbo].[Parameters] where name='ThresholdExaustion' )
set @ThresholdWarning =(select cast(value as int) from [AVData].[dbo].[Parameters] where name='ThresholdWarning' )
DECLARE mycurs CURSOR
for  select  distinct Id from  dbo.Atm 
open mycurs
FETCH NEXT FROM mycurs INTO @AtmId
While @@FETCH_STATUS = 0
begin
set @IdDoneOrder=(select Top 1 Id from dbo.DoneOrder where Atm_Id=@AtmId Order by DoneOrder_Date Desc)
set @LastDoneOrderDate=( select DoneOrder_Date from dbo.DoneOrder where Id=@IdDoneOrder)
set @LastDoneOrderAmount=(select AddedAmount from dbo.DoneOrder where Id=@IdDoneOrder)
set @TransactionAmount=(select coalesce ( sum(Amount),0 ) from [dbo].[Avtransaction] where AtmID=@AtmID
 and TransactionDate>@LastDoneOrderDate and (isCashPresented=1 or isCashTaken=1) )
 set @Amount=@LastDoneOrderAmount-@TransactionAmount
 
 if(@Amount<=@ThresholdExaustion)
 begin
 set @LastBug =(select Bug_Id from dbo.BugAtm where Atm_Id=@AtmId and Bug_Id in(select Id from dbo.Bug where BugCategory_Id=3 and Bug.LastUpdateDate is null))
 if(@LastBug is null)
 begin
 insert into [dbo].[Bug] ([Title]
      ,[CreationDate]
      ,[BugCategory_Id]
      ,[BugPriority_Id]
      ,[BugStatut_Id]) values('Exaustion',GETDATE(),3,1,1)
  set @Id=SCOPE_IDENTITY()
  insert into [dbo].[BugAtm] ([Bug_Id],[Atm_Id]) values(@Id,@AtmId)
  end
 end
 if(@Amount>@ThresholdExaustion and @Amount<=@ThresholdWarning )
 begin
 set @LastBug =(select Bug_Id from dbo.BugAtm where Atm_Id=@AtmId and Bug_Id in(select Id from dbo.Bug where Bug.Title='Warning' and Bug.LastUpdateDate is null))
 if(@LastBug is null)
 begin
 insert into [dbo].[Bug] ([Title]
      ,[CreationDate]
      ,[BugCategory_Id]
      ,[BugPriority_Id]
      ,[BugStatut_Id]) values('Warning',GETDATE(),2,2,1)
set @Id=SCOPE_IDENTITY()
insert into [dbo].[BugAtm] ([Bug_Id],[Atm_Id]) values(@Id,@AtmId)

end
 end
FETCH NEXT FROM mycurs INTO @AtmId 
end
close mycurs
Deallocate mycurs
END


GO
/****** Object:  StoredProcedure [dbo].[Factors]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Factors] 

@AtmId as nvarchar(128)
as 

Declare @IdGab nvarchar(128)
Declare @ConsommationHier bigint 
Declare @SumRetrait bigint
Declare @TransactionDay date
Declare @DayName nvarchar(128)	
Declare @Jour 	int
Declare @MSemaineDernier real
Declare @MSemaine7 real
Declare @ConsoMmJrAnP bigint
Declare @weekMonth int
Declare @ConsoMmJrMP bigint
Declare @ConsoMMJrSmDer bigint
Declare @MoyenneMoisPrec real	
Declare @MoyenneMMSAnPrec real
Declare @MoyenneMMmAnPrec real
Declare @ConsommationMaxMDer bigint
Declare @ConsommationMaxSDer bigint
Declare @Val1 bigint
Declare @Val2 bigint
Declare @dayCurs int
Declare @poidJr int 
Declare @poidName float
Declare @intweekMonth int
Declare @poitTotal float
begin

Declare curs Cursor
for select CAST (TransactionDate as Date),coalesce ( sum(Amount),0 ) from  [dbo].[AVTransaction]  where AtmID=@AtmId
and isCashPresented =1 and cast(TransactionDate as Date)<cast(GetDate() as Date) group by cast(TransactionDate as Date) order by CAST (TransactionDate as Date)
open curs 
Fetch Next from curs into @TransactionDay,@SumRetrait
while @@FETCH_STATUS=0
begin
/*********************WeekMonth********************/
set @Jour = Datepart(day ,@TransactionDay)
 if (  @Jour>=1 and  @Jour<=7)
 begin
 set @intweekMonth =1 
 end 
 if (  @Jour>7 and  @Jour<=14)
 begin
 set @intweekMonth =2 
 end 
 if (  @Jour>14 and  @Jour<=21)
 begin
 set @intweekMonth =3  
 end 
 if (  @Jour>21 and  @Jour<=28)
  begin
  set @intweekMonth =4
  end   
 if (  @Jour>28)
 begin
  set @intweekMonth =5 
 end 
insert into [dbo].[Factor]([Atm_Id],[TransactionDay],[SumRetrait],[intweekMonth],[Bais])values( @AtmId,@TransactionDay,@SumRetrait,@intweekMonth,1)
Fetch Next from curs into @TransactionDay,@SumRetrait
end 
close curs
Deallocate curs




Declare curs1 Cursor
for select CAST (TransactionDay as Date) from [dbo].[Factor] where Atm_Id=@AtmId
open curs1 
Fetch Next from curs1 into @TransactionDay
while @@FETCH_STATUS=0
begin
set @DayName =DATENAME(dw ,@TransactionDay)

/******Poid Du Jour selon le nom du Jour*********/

   set @Val1 = (select sum(SumRetrait) as Total
from (
 select top 4 SumRetrait from dbo.Factor where DATENAME(dw,TransactionDay)=DATENAME(dw,@TransactionDay)
  and  datepart(Month,TransactionDay)=datepart(Month,dateadd(Month,-1,@TransactionDay)) and 
  datepart(year,TransactionDay)=datepart(year,dateadd(Month,-1,@TransactionDay))
 and Atm_Id=@AtmId order by cast(TransactionDay as Date) ASC ) as t)
 set @Val2 =(select  sum(SumRetrait) from  dbo.Factor where Atm_Id=@AtmId and  datepart(Month,TransactionDay)=datepart(Month,dateadd(Month,-1,@TransactionDay)) and datepart(year,TransactionDay)=datepart(year,dateadd(Month,-1,@TransactionDay)))
 print((cast(@Val1 as float)/cast(@Val2 as float)) *cast((7/4) as float))
 set @poidName=(cast(@Val1 as float)/cast(@Val2 as float)) *cast((7/4)*10 as float)
 print(@poidName)

 /******Poid du Jour*********/
 set @dayCurs= DATEPART(day ,@TransactionDay)
  if ( @dayCurs>=1 and @dayCurs<5)
  begin 
  set @PoidJr=4   
   end
   if (  @dayCurs>=5 and @dayCurs<=18)
   begin
   set @PoidJr=3  
   end
   if (  @dayCurs>18  and  @dayCurs<26)
   begin
    set @PoidJr=2
   end
   if (  @dayCurs>=26 )
   begin
   set @PoidJr=4 
   end
set @ConsoMmJrAnP =(select SumRetrait from  [dbo].[Factor] where  cast(TransactionDay as date)=cast(DATEADD(year,-1 ,@TransactionDay) as Date) and   Atm_Id=@AtmId)
if(@ConsoMmJrAnP is null)
begin
set  @ConsoMmJrAnP=0
end
set @ConsommationHier =(select SumRetrait from  [dbo].[Factor] where Atm_Id=@AtmId and cast(TransactionDay as Date)=DATEADD(day,-1,@TransactionDay) )
if(@ConsommationHier is null)
begin
set @ConsommationHier=0
end
set @MSemaineDernier =(select AVG(SumRetrait) from  [dbo].[Factor]  where  Datepart(week,TransactionDay)=DATEPART( week ,dATEADD(week,-1,@TransactionDay)) and Datepart(year,TransactionDay)=Datepart(year ,DATEADD(WEEK,-1 ,@TransactionDay)) and SumRetrait is not null and   Atm_Id=@AtmId)
if(@MSemaineDernier is null)
begin
set @MSemaineDernier=0
end
set @MSemaine7=(select Avg(SumRetrait) from  [dbo].[Factor]  where  TransactionDay between DATEADD(day,-7,@TransactionDay) and DATEADD(day,-1,@TransactionDay) and SumRetrait is not null  and  Atm_Id=@AtmId  )
if(@MSemaine7 is null)
begin
set @MSemaine7=0
end
set @weekMonth = (select Top 1 intweekMonth from  [dbo].[Factor] where TransactionDay=@TransactionDay)
set @ConsoMmJrMP =(select SumRetrait from  [dbo].[Factor]  where cast(TransactionDay as Date)=Cast(dATEADD(Month,-1,@TransactionDay) as Date)  and  Atm_Id=@AtmId )
if(@ConsoMmJrMP is null)
begin
set @ConsoMmJrMP=0
end
set @ConsoMMJrSmDer =(select SumRetrait from  [dbo].[Factor] where DATEPART(week,TransactionDay)=DATEPART(week,dATEADD(week,-1,@TransactionDay)) and DATENAME(dw ,TransactionDay)=@DayName and DATEPART(year,TransactionDay)=Datepart(year ,DATEADD(WEEK,-1 ,@TransactionDay)) and  Atm_Id=@AtmId )
if(@ConsoMMJrSmDer is null)
begin
set @ConsoMMJrSmDer=0
end
set @MoyenneMoisPrec =(select AVG(SumRetrait) from  [dbo].[Factor] where datepart(Month,TransactionDay)=datepart(Month,dATEADD(Month,-1,@TransactionDay)) and datepart(year,TransactionDay)=Datepart(year ,DATEADD(month,-1 ,@TransactionDay)) and SumRetrait is not null and  Atm_Id=@AtmId  )
if(@MoyenneMoisPrec is null)
begin
set @MoyenneMoisPrec=0
end
set @MoyenneMMSAnPrec =(select AVG(SumRetrait) from  [dbo].[Factor] where Datepart (week, TransactionDay)=Datepart (week, @TransactionDay) and  Datepart (year, TransactionDay)=Datepart (year, dATEADD(year,-1,@TransactionDay)) and SumRetrait is not null and  Atm_Id=@AtmId)
if(@MoyenneMMSAnPrec is null)
begin
set @MoyenneMMSAnPrec=0
end
set @MoyenneMMmAnPrec =(select AVG(SumRetrait) from  [dbo].[Factor] where datepart(year,TransactionDay)=datepart(year,dATEADD(year,-1,@TransactionDay))  and datepart(Month,TransactionDay)=datepart(Month,@TransactionDay) and SumRetrait is not null and Atm_Id=@AtmId )
if(@MoyenneMMmAnPrec is null)
begin
set @MoyenneMMmAnPrec=0
end
set @ConsommationMaxMDer   =(select Max(SumRetrait) from  [dbo].[Factor] where Datepart(Month,TransactionDay)=Datepart(Month,dATEADD(Month,-1,@TransactionDay)) and Datepart(year,TransactionDay)=Datepart(year ,DATEADD(Month,-1 ,@TransactionDay))  and Atm_Id=@AtmId )
if(@ConsommationMaxMDer is null)
begin
set @ConsommationMaxMDer=0
end

set @ConsommationMaxSDer   =(select Max(SumRetrait) from  [dbo].[Factor] where Datepart(week,TransactionDay)=Datepart(week,dATEADD(week,-1,@TransactionDay)) and Datepart(year,TransactionDay)=Datepart(year ,DATEADD(week,-1 ,@TransactionDay))  and Atm_Id=@AtmId )
if(@ConsommationMaxSDer is null)
begin
set @ConsommationMaxSDer=0
end
 
update [dbo].[Factor] set
 ConsommationHier=@ConsommationHier, 
 MSemaineDernier=@MSemaineDernier,
 MSemaine7=@MSemaine7,
 ConsoMmJrAnP=@ConsoMmJrAnP,
 ConsoMmJrMP=@ConsoMmJrMP,
 ConsoMMJrSmDer=@ConsoMMJrSmDer,
 MoyenneMoisPrec=@MoyenneMoisPrec,
 MoyenneMMSAnPrec=@MoyenneMMSAnPrec,
 MoyenneMMmAnPrec=@MoyenneMMmAnPrec,
 ConsommationMaxMDer=@ConsommationMaxMDer,
 ConsommationMaxSDer=@ConsommationMaxSDer,
 PoidTot=@poidJr*@poidName
 where  Atm_Id=@AtmId and TransactionDay=@TransactionDay

fetch Next from curs1 into @TransactionDay
END
close curs1
Deallocate curs1


END
GO
/****** Object:  StoredProcedure [dbo].[factorsofAllGabs]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[factorsofAllGabs]
AS
Declare @AtmIdd  nvarchar(128)
BEGIN
DECLARE mycursFinal CURSOR
for  select  distinct Id  from  [dbo].[Atm] 
open mycursFinal
FETCH NEXT FROM mycursFinal INTO @AtmIdd 
While @@FETCH_STATUS = 0
begin
Exec [dbo].[Factors]   @AtmID=@AtmIdd 
fetch Next from mycursFinal INTO @AtmIdd 
END
close mycursFinal
Deallocate mycursFinal
    
END

GO
/****** Object:  StoredProcedure [dbo].[GetAtmInfoMonitoring]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[GetAtmInfoMonitoring]
@UserId nvarchar(200)
as
--DECLARE @SQLString nvarchar(500);

--select atm.* into #TmpAtm 
--from atm 
--left join  UserAtm on UserAtm.Atm_Id =  atm.Id
--where (UserAtm.User_Id =  @UserId and @UserId is not null and @UserId <> '')

select top 1 * into #TmpAtm from ATM where 1 =  2
delete from #TmpAtm
IF (@UserId = '' or @UserId is null)
begin
--set @SQLString = 'INSERT INTO #TmpAtm SELECT Atm.* FROM Atm'
INSERT INTO #TmpAtm SELECT Atm.* FROM Atm  where (HostName is not NULL)
end
else
begin
--set @SQLString = N'INSERT INTO #TmpAtm SELECT Atm.* FROM Atm JOIN  UserAtm on UserAtm.Atm_Id = Atm.Id WHERE UserAtm.User_Id = @UserId '
INSERT INTO #TmpAtm SELECT Atm.* FROM Atm JOIN  UserAtm on UserAtm.Atm_Id = Atm.Id WHERE UserAtm.User_Id = @UserId
end

--print @SQLString
--EXECUTE sp_executesql @SQLString
--select * from #TmpAtm


--select max(id) as MaxStateId, atm_id into #TmpAtmState
-- from state group by atm_id
 
 select bugatm.atm_id , max(bugatm.bug_id) bug_id into #TmpBugATM  from bug inner join bugatm
on bugatm.bug_Id =  bug.id and bug.bugstatut_id <> 4
group by bugatm.atm_id
 
 select max(Id) IdAtmRemarque, atm_Id into #TmpAtmRemarque
 from atmRemarque
 group by atm_Id 
 
 select  #TmpAtm.Id as AtmId, #TmpAtm.Name as AtmName  , #TmpAtm.Profile as AtmProfile , RecentAtmState.state_id as MaxStateId, st.Label as StateLabel , 
 s.statetype_id ,
 st.CssClass, st.Color, s.StateDate,
 err.Id AtmErrorId, err.StartDate ErrStartDate, err.EndDate ErrEndDate,--lkh: 
 ac.Id ActionCorrectiveId , ac.Name ActionCorrectiveName, ac.User_Id acUserId,
 --ac.Error , ac.IdErrorType, --lkh
 #TmpAtmRemarque.IdatmRemarque , atmRemarque.Remarque, #TmpBugATM.Bug_Id
 from #TmpAtm --atm 
 left join RecentAtmState on #TmpAtm.Id = RecentAtmState.atm_id
 left join state s on s.id = RecentAtmState.state_id
 left join statetype st on st.Id = s.statetype_id
 left join atmerror err on err.atm_Id = #TmpAtm.id 
 and err.state_Id =   RecentAtmState.state_id
 and  err.enddate is null
 left join actioncorrective ac on ac.Id =  err.ActionCorrective_Id
 --ac.iderrorType =  err.iderrorType
 left join #TmpBugATM on #TmpBugATM.atm_id =  #TmpAtm.id
 --left join bugatm on bugatm.atm_id =  atm.id
 --left join bug on bug.Id =  bugatm.bug_Id
 left join #TmpAtmRemarque on #TmpAtmRemarque.atm_Id = #TmpAtm.id 
 left join AtmRemarque on #TmpAtmRemarque.IdatmRemarque =  AtmRemarque.Id
 --where err.enddate is null
 
  select RecentAtmState.state_id, Component_Id, stateComponent_id , component.label ComponentLabel
 from componentstate join component
 on componentstate.Component_Id = component.id 
 join RecentAtmState on componentstate.state_id = RecentAtmState.state_id
 
 --drop table #TmpAtmState
 drop table #TmpBugATM
 drop table #TmpAtmRemarque
 drop table #TmpAtm
GO
/****** Object:  StoredProcedure [dbo].[GetAtmInfoMonitoring2]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE proc [dbo].[GetAtmInfoMonitoring2]
@UserId nvarchar(200)
as
DECLARE @SQLString nvarchar(500);

--select atm.* into #TmpAtm 
--from atm 
--left join  UserAtm on UserAtm.Atm_Id =  atm.Id
--where (UserAtm.User_Id =  @UserId and @UserId is not null and @UserId <> '')

select top 1 * into #TmpAtm from ATM where 1 =  2
delete from #TmpAtm
IF (@UserId = '' or @UserId is null)
begin
set @SQLString = ' insert into #TmpAtm  select atm.* from atm where id in (select Atm_Id from RecentAtmState)'
end
else
begin
set @SQLString = N'insert into #TmpAtm   select atm.*  from atm 
join  UserAtm on UserAtm.Atm_Id =  atm.Id
where UserAtm.User_Id = ''' +  @UserId + ''' and id in (select Atm_Id from RecentAtmState) '

end
--print @SQLString
EXECUTE sp_executesql @SQLString
--select * from #TmpAtm


select max(State_Id) as MaxStateId, atm_id into #TmpAtmState
 from RecentAtmState group by atm_id
 
 select bugatm.atm_id , max(bugatm.bug_id) bug_id into #TmpBugATM  from bug inner join bugatm
on bugatm.bug_Id =  bug.id and bug.bugstatut_id <> 4
group by bugatm.atm_id
 
 select max(Id) IdAtmRemarque, atm_Id into #TmpAtmRemarque
 from atmRemarque
 group by atm_Id 
 
  select max(TransactionDate) LastTransaction, atmId into #TmpTrxDate
 from LastDealyTrx
 group by atmId 


 select  #TmpAtm.Id as AtmId, #TmpAtm.Name as AtmName , #TmpAtm.Profile as AtmProfile , #TmpAtmState.MaxStateId, 
 rs.Connected, rs.LastStateType as StateTypeId,
 st.Id as StateTypeId2, st.Label as StateLabel ,  
 st.CssClass, st.Color, s.StateDate,rs.LastSeen,trx.LastTransaction,rs.LastReboot,
 err.Id AtmErrorId, err.StartDate ErrStartDate, err.EndDate ErrEndDate,--lkh: 
 ac.Id ActionCorrectiveId , ac.Name ActionCorrectiveName, Us.UserName acUserId,
 --ac.Error , ac.IdErrorType, --lkh
 #TmpAtmRemarque.IdatmRemarque , atmRemarque.Remarque, #TmpBugATM.Bug_Id
 from #TmpAtm --atm 
 left join #TmpAtmState on #TmpAtm.Id = #TmpAtmState.atm_id
 left join RecentAtmState rs on rs.Atm_Id = #TmpAtm.Id
 left join state s on s.id = #TmpAtmState.MaxStateId
 left join statetype st on (((st.Id = rs.LastStateType)AND (rs.Connected=1))OR(st.Id=5)AND (rs.Connected=0))
 left join atmerror err on err.atm_Id = #TmpAtm.id 
 and  err.enddate is null
 left join actioncorrective ac on ac.Id =  err.ActionCorrective_Id
 --ac.iderrorType =  err.iderrorType
 left join #TmpBugATM on #TmpBugATM.atm_id =  #TmpAtm.id
 --left join bugatm on bugatm.atm_id =  atm.id
 --left join bug on bug.Id =  bugatm.bug_Id
 left join #TmpAtmRemarque on #TmpAtmRemarque.atm_Id = #TmpAtm.id 
 left join AtmRemarque on #TmpAtmRemarque.IdatmRemarque =  AtmRemarque.Id
  left join AspNetUsers Us  on ac.User_Id = Us.Id
 left join #TmpTrxDate trx on trx.AtmId = #TmpAtm.Id
 --where err.enddate is null
Order by StateTypeId2
 
  select state_id, Component_Id, stateComponent_id , component.label ComponentLabel
 from componentstate join component
 on componentstate.Component_Id = component.id 
 join #TmpAtmState on componentstate.state_id = #TmpAtmState.MaxStateId
 
 drop table #TmpAtmState
 drop table #TmpBugATM
 drop table #TmpAtmRemarque
 drop table #TmpAtm
 drop table #TmpTrxDate
GO
/****** Object:  StoredProcedure [dbo].[GetAtmInfoMonitoring3]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- (28/01/2019) add anti bug for RecentAtmState duplication (always work with the old id)

CREATE proc [dbo].[GetAtmInfoMonitoring3]
@UserId nvarchar(200)
as
DECLARE @SQLString nvarchar(500);

--select atm.* into #TmpAtm 
--from atm 
--left join  UserAtm on UserAtm.Atm_Id =  atm.Id
--where (UserAtm.User_Id =  @UserId and @UserId is not null and @UserId <> '')

select top 1 * into #TmpAtm from ATM where 1 =  2
delete from #TmpAtm
IF (@UserId = '' or @UserId is null)
begin
set @SQLString = ' insert into #TmpAtm  select atm.* from atm '
end
else
begin
set @SQLString = N'insert into #TmpAtm   select atm.*  from atm 
join  UserAtm on UserAtm.Atm_Id =  atm.Id
where UserAtm.User_Id = ''' +  @UserId + ''''

end
--print @SQLString
EXECUTE sp_executesql @SQLString
--select * from #TmpAtm


select max(id) as MaxStateId, atm_id into #TmpAtmState
 from state group by atm_id
 
 select bugatm.atm_id , max(bugatm.bug_id) bug_id into #TmpBugATM  from bug inner join bugatm
on bugatm.bug_Id =  bug.id and bug.bugstatut_id <> 4
group by bugatm.atm_id
 
 select max(Id) IdAtmRemarque, atm_Id into #TmpAtmRemarque
 from atmRemarque
 group by atm_Id 

select min(Id) idmin, atm_Id into #TmpRS
 from RecentAtmState
 group by atm_Id 
 
 select  #TmpAtm.Id as AtmId, #TmpAtm.Name as AtmName  , #TmpAtmState.MaxStateId, 
 rs.Connected, rs.LastStateType as StateTypeId,
 st.Id as StateTypeId2, st.Label as StateLabel ,  
 st.CssClass, st.Color, s.StateDate,
 err.Id AtmErrorId, err.StartDate ErrStartDate, err.EndDate ErrEndDate,--lkh: 
 ac.Id ActionCorrectiveId , ac.Name ActionCorrectiveName, ac.User_Id acUserId,
 --ac.Error , ac.IdErrorType, --lkh
 #TmpAtmRemarque.IdatmRemarque , atmRemarque.Remarque, #TmpBugATM.Bug_Id
 from #TmpAtm --atm 
 left join #TmpAtmState on #TmpAtm.Id = #TmpAtmState.atm_id
 left join #TmpRS tmp_rs on tmp_rs.Atm_Id = #TmpAtm.Id
 left join RecentAtmState rs on rs.Id = tmp_rs.idmin
 left join state s on s.id = #TmpAtmState.MaxStateId
 left join statetype st on (((st.Id = rs.LastStateType)AND (rs.Connected=1))OR(st.Id=5)AND (rs.Connected=0))
 left join atmerror err on err.atm_Id = #TmpAtm.id 
 and err.state_Id =   #TmpAtmState.MaxStateId
 and  err.enddate is null
 left join actioncorrective ac on ac.Id =  err.ActionCorrective_Id
 --ac.iderrorType =  err.iderrorType
 left join #TmpBugATM on #TmpBugATM.atm_id =  #TmpAtm.id
 --left join bugatm on bugatm.atm_id =  atm.id
 --left join bug on bug.Id =  bugatm.bug_Id
 left join #TmpAtmRemarque on #TmpAtmRemarque.atm_Id = #TmpAtm.id 
 left join AtmRemarque on #TmpAtmRemarque.IdatmRemarque =  AtmRemarque.Id
 --where err.enddate is null
 
  select state_id, Component_Id, stateComponent_id , component.label ComponentLabel
 from componentstate join component
 on componentstate.Component_Id = component.id 
 join #TmpAtmState on componentstate.state_id = #TmpAtmState.MaxStateId
 
 drop table #TmpAtmState
 drop table #TmpBugATM
 drop table #TmpAtmRemarque
 drop table #TmpAtm
GO
/****** Object:  StoredProcedure [dbo].[LastDoneOrdersByHours]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LastDoneOrdersByHours]

AS
BEGIN
Declare @IdGab varchar(50)
DECLARE @result1 TABLE (a Datetime, b nvarchar(max)  ,c nvarchar(max))
Declare @DatesTable Table(t Datetime)
DECLARE mycursFinal CURSOR
for  select  distinct Id  from [dbo].[Atm] 
open mycursFinal
FETCH NEXT FROM mycursFinal INTO @IdGab 
While @@FETCH_STATUS = 0
begin 
--Foreach Atm Redefine variables
Delete from @result1
Delete  @DatesTable
Declare @Amount bigint
DECLARE @tags nvarchar(max) 
Declare @True Datetime
Declare @Date nvarchar(max)  
Declare @CashpointId int
Declare @Befor int
Declare @Profile VARCHAR(200)
Declare  @DoneOrderAmountTxt nvarchar(max)  
Declare  @ProvisoirDate Datetime
Declare  @ProvisoirDate3 Datetime
Declare  @DateVal nvarchar(max)  
Declare  @DateVal2 nvarchar(max)  
Declare  @Date2 nvarchar(max)  
Declare @Date3 Datetime
Declare @NcrDate Datetime
Declare @i  int 
Declare @Txt nvarchar(max)  
Declare @Hour int
Declare @minute int
Declare @second int
Declare @Id int
Declare @IdBug int
Declare @counter int
Declare @LastDoneOrderAmount bigint
Declare @SumTransactions bigint
Declare @SumTransactionsNEXT bigint
Declare @LastDoneOrderDate Datetime
Declare @NextDoneOrderDate Datetime
Declare @NextDoneOrderAmount bigint
--- begin
set @counter=0
set @Befor=0
set @Profile =(select profile from dbo.Atm where Id=@IdGab)
--insert into dbo.JournalEntryMontly select * from  [AVJOURNALSERVER].[AVJournalArchive].[dbo].[LastSavedJournal]
if( @Profile='WN')
begin
insert into @result1   SELECT EntryTime as a,Data  as b ,Atm_Id as c FROM   [dbo].[JournalEntry] where  Atm_Id=@IdGab
 and (CHARINDEX('CASH COUNTERS AFTER SOP', Data)!=0 ) order by EntryTime Asc
if((select count(*) from @result1)!=0)
begin
DECLARE mycursf CURSOR
for  select   a ,b  from @result1 order by a Asc
open mycursf
FETCH NEXT FROM mycursf INTO @Date3,@tags
While @@FETCH_STATUS = 0
begin 
SET @True =(select [dbo].[ExtractTrueDoneOrderDate] (@Date3,@Profile,@tags))
set @Amount=(select [dbo].[ExtractDate](@tags))
set @i=1
set @Befor=(SELECT  [dbo].[BeforSop] (@tags) )
if(@Befor=0)
begin
set @Id=(select Top 1 Id from dbo.DoneOrder where Atm_Id=@IdGab and DoneOrder_Date<@True order by DoneOrder_Date Desc)
if(@id is not null)
begin
set @LastDoneOrderDate =(select DoneOrder_Date from dbo.DoneOrder where Id=@Id)
set @SumTransactions =(select coalesce ( sum(Amount),0 ) from  [dbo].AVTransaction where AtmID=@IdGab
and TransactionDate<@True and TransactionDate>@LastDoneOrderDate and isCashPresented=1 ) 
set @LastDoneOrderAmount =(select AddedAmount from dbo.DoneOrder where Id=@Id)
set @Befor =@LastDoneOrderAmount-@SumTransactions
end
end
set @CashpointId= (select Id from dbo.CashPoint where CashPointName=@IdGab)
if(@Amount!=0 and @Amount-@Befor>10000 )
begin
insert into dbo.[DoneOrder]([DoneOrder_Date],[DoneOrder_Amount],[Atm_Id],[DoneOrderState],[AddedAmount],[StateBefore_Id],[CashProvider_Id],CashPoint_Id) values(@True,@Befor,@IdGab,'3',@Amount,2,1,@CashpointId)
set @IdBug=(select Bug_Id from dbo.BugAtm  where Atm_Id=@IdGab and Bug_Id in(select Id from dbo.bug where BugCategory_Id=3 and LastUpdateDate is null))
if(@IdBug is not null)
begin
update dbo.Bug set LastUpdateDate=@True,BugStatut_Id=4 where Id=@IdBug
end
set @IdBug=(select Bug_Id from dbo.BugAtm  where Atm_Id=@IdGab and Bug_Id in(select Id from dbo.bug where Title='Warning' and LastUpdateDate is null))
if(@IdBug is not null)
begin
update dbo.Bug set LastUpdateDate=@True,BugStatut_Id=4 where Id=@IdBug
end
end
fetch Next from mycursf INTO @Date3,@tags 
END
close mycursf
Deallocate mycursf
end
end

if(@Profile='NCR')
begin
print('begin for this')
print(@IdGab)
insert into @DatesTable   SELECT distinct cast(EntryTime as Date) as t FROM [dbo].[JournalEntry] where  Atm_Id=@IdGab
and CHARINDEX('CASH ADDED',Data)!=0 order by cast(EntryTime as Date) Asc
if((select count(*) from @DatesTable)!=0)
begin
DECLARE mycurs CURSOR
for  select   t   from @DatesTable order by t Asc
open mycurs
FETCH NEXT FROM mycurs INTO @Date3
While @@FETCH_STATUS = 0
begin 
print('begin of the Cursor @DatesTable')
Delete from @result1
insert into @result1  SELECT EntryTime as a,Data  as b ,Atm_Id as c  FROM   [dbo].[JournalEntry] where  Atm_Id=@IdGab and cast(EntryTime as Date)=@Date3
and CHARINDEX('CASH ADDED',Data)!=0 order by EntryTime Asc
set @tags=(select top 1 b from @result1 order by a asc)
set @counter=(select count(*)  from @result1)-1
set @Amount =(SELECT  [dbo].[ExtractAmountForNCR] (@tags) )
SET @True =(select top 1 a from @result1 order by a asc)
set @NcrDate=@True
set @Id=(select Top 1 Id from dbo.DoneOrder where Atm_Id=@IdGab and DoneOrder_Date<@True order by DoneOrder_Date Desc)
if(@id is not null)
begin
set @LastDoneOrderDate =(select DoneOrder_Date from dbo.DoneOrder where Id=@Id)
set @SumTransactions =(select coalesce ( sum(Amount),0 ) from  [dbo].[Avtransaction] where AtmID=@IdGab
and TransactionDate<@True and TransactionDate>@LastDoneOrderDate and isCashPresented=1 ) 
set @LastDoneOrderAmount =(select AddedAmount from dbo.DoneOrder where Id=@Id)
set @Befor =@LastDoneOrderAmount-@SumTransactions
end
while( @counter>0)
begin
set @DoneOrderAmountTxt=(select top 1 b from @result1 where a>@NcrDate order by a Asc )
set @NextDoneOrderAmount=(SELECT  [dbo].[ExtractAmountForNCR] (@DoneOrderAmountTxt) )
if((CHARINDEX('TYPE 1 =  0',@DoneOrderAmountTxt)!=0 OR CHARINDEX('TYPE 2 =  0',@DoneOrderAmountTxt)!=0 or CHARINDEX('TYPE 3 =  0',@DoneOrderAmountTxt)!=0 or CHARINDEX('TYPE 4 =  0',@DoneOrderAmountTxt)!=0 ))
begin
set @Amount =@Amount+@NextDoneOrderAmount
end
else
begin
set @Amount =@NextDoneOrderAmount
end
if(@counter>1)
BEGIN
set @NcrDate=(select top 1 a from @result1 where a>@NcrDate  and CHARINDEX('CASH ADDED',b)!=0 order by a Asc )
END
set @counter=@counter-1
end 
fetch Next from mycurs INTO @Date3 
end
set @CashpointId= (select Id from dbo.CashPoint where CashPointName=@IdGab)
if(@Amount!=0 and @Amount-@Befor>10000 )
begin
insert into dbo.[DoneOrder]([DoneOrder_Date],[DoneOrder_Amount],[Atm_Id],[DoneOrderState],[AddedAmount],[StateBefore_Id],[CashProvider_Id],CashPoint_Id) values(@True,@Befor,@IdGab,'0',@Amount,2,1,@CashpointId)
set @IdBug=(select Bug_Id from dbo.BugAtm  where Atm_Id=@IdGab and Bug_Id in(select Id from dbo.bug where BugCategory_Id=3 and LastUpdateDate is null))
if(@IdBug is not null)
begin
update dbo.Bug set LastUpdateDate=@True,BugStatut_Id=4 where Id=@IdBug
end
set @IdBug=(select Bug_Id from dbo.BugAtm  where Atm_Id=@IdGab and Bug_Id in(select Id from dbo.bug where Title='Warning' and LastUpdateDate is null))
if(@IdBug is not null)
begin
update dbo.Bug set LastUpdateDate=@True,BugStatut_Id=4 where Id=@IdBug
end

fetch Next from mycurs INTO @Date3
END
close mycurs
Deallocate mycurs
end
print(@Amount)
print('end for this')
print(@IdGab)
end






 fetch Next from mycursFinal INTO @IdGab 
END
close mycursFinal
Deallocate mycursFinal
end

GO
/****** Object:  StoredProcedure [dbo].[lastFactor]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lastFactor] 

@AtmId as nvarchar(128),
@TransactionDay as Datetime
as 

Declare @IdGab nvarchar(128)
Declare @ConsommationHier bigint 
Declare @SumRetrait bigint
Declare @DayName nvarchar(128)	
Declare @Jour 	int
Declare @MSemaineDernier real
Declare @MSemaine7 real
Declare @ConsoMmJrAnP bigint
Declare @weekMonth int
Declare @ConsoMmJrMP bigint
Declare @ConsoMMJrSmDer bigint
Declare @MoyenneMoisPrec real	
Declare @MoyenneMMSAnPrec real
Declare @MoyenneMMmAnPrec real
Declare @ConsommationMaxMDer bigint
Declare @ConsommationMaxSDer bigint
Declare @dayCurs int
Declare @poidJr int 
Declare @poidName float
Declare @intweekMonth int
Declare @Val1 float
Declare @Val2 float

begin

set @Jour = Datepart(day ,@TransactionDay)
 if (  @Jour>=1 and  @Jour<=7)
 begin
 set @intweekMonth =1 
 end 
 if (  @Jour>7 and  @Jour<=14)
 begin
 set @intweekMonth =2 
 end 
 if (  @Jour>14 and  @Jour<=21)
 begin
 set @intweekMonth =3  
 end 
 if (  @Jour>21 and  @Jour<=28)
  begin
  set @intweekMonth =4
  end   
 if (  @Jour>28)
 begin
  set @intweekMonth =5 
 end 

  set @dayCurs= DATEPART(day ,@TransactionDay)
  if ( @dayCurs>=1 and @dayCurs<5)
  begin 
  set @PoidJr=4   
   end
   if (  @dayCurs>=5 and @dayCurs<=18)
   begin
   set @PoidJr=3  
   end
   if (  @dayCurs>18  and  @dayCurs<26)
   begin
    set @PoidJr=2
   end
   if (  @dayCurs>=26 )
   begin
   set @PoidJr=4 
   end

      set @Val1 = (select sum(SumRetrait) as Total
    from (
 select top 4 SumRetrait from dbo.Factor where DATENAME(dw,TransactionDay)=DATENAME(dw,@TransactionDay)
  and  datepart(Month,TransactionDay)=datepart(Month,dateadd(Month,-1,@TransactionDay)) and 
  datepart(year,TransactionDay)=datepart(year,dateadd(Month,-1,@TransactionDay))
 and Atm_Id=@AtmId order by cast(TransactionDay as Date) ASC ) as t)
 set @Val2 =(select  coalesce(sum(SumRetrait),0) from  dbo.Factor where Atm_Id=@AtmId and  datepart(Month,TransactionDay)=datepart(Month,dateadd(Month,-1,@TransactionDay)) and datepart(year,TransactionDay)=datepart(year,dateadd(Month,-1,@TransactionDay)))
 set @poidName=coalesce((cast(@Val1 as float)/Nullif(cast(@Val2 as float),0) *cast((7/4)*10 as float)),0)

set @SumRetrait=(select coalesce(sum(Amount),0) from   [dbo].[AVTransaction] 
 where AtmID=@AtmId and cast(TransactionDate as date )=cast(@TransactionDay as Date)
and isCashPresented=1 )
set @DayName =DATENAME(dw ,@TransactionDay)
set @ConsoMmJrAnP =(select SumRetrait from  [dbo].[Factor] where  cast(TransactionDay as date)=cast(DATEADD(year,-1 ,@TransactionDay) as Date) and   Atm_Id=@AtmId)
if(@ConsoMmJrAnP is null)
begin
set  @ConsoMmJrAnP=0
end
set @ConsommationHier =(select SumRetrait from  [dbo].[Factor] where Atm_Id=@AtmId and cast(TransactionDay as date)=cast(DATEADD(day,-1,@TransactionDay)as Date) )
if(@ConsommationHier is null)
begin
set @ConsommationHier=0
end
set @MSemaineDernier =(select AVG(SumRetrait) from  [dbo].[Factor]  where  Datepart(week,TransactionDay)=DATEPART( week ,dATEADD(week,-1,@TransactionDay)) and Datepart(year,TransactionDay)=Datepart(year ,DATEADD(WEEK,-1 ,@TransactionDay)) and SumRetrait is not null and   Atm_Id=@AtmId)
if(@MSemaineDernier is null)
begin
set @MSemaineDernier=0
end
set @MSemaine7=(select Avg(SumRetrait) from  [dbo].[Factor]  where  cast(TransactionDay as Date) between cast(DATEADD(day,-7,@TransactionDay)as Date) and cast(DATEADD(day,-1,@TransactionDay) as Date) and SumRetrait is not null  and  Atm_Id=@AtmId  )
if(@MSemaine7 is null)
begin
set @MSemaine7=0
end

set @weekMonth = (select Top 1 intweekMonth from  [dbo].[Factor] where TransactionDay=@TransactionDay)
set @ConsoMmJrMP =(select SumRetrait from  [dbo].[Factor]  where cast(TransactionDay as Date)=Cast(dATEADD(Month,-1,@TransactionDay) as Date)  and  Atm_Id=@AtmId )
if(@ConsoMmJrMP is null)
begin
set @ConsoMmJrMP=0
end
set @ConsoMMJrSmDer =(select SumRetrait from  [dbo].[Factor] where DATEPART(week,TransactionDay)=DATEPART(week,dATEADD(week,-1,@TransactionDay)) and DATENAME(dw ,TransactionDay)=@DayName and DATEPART(year,TransactionDay)=Datepart(year ,DATEADD(WEEK,-1 ,@TransactionDay)) and  Atm_Id=@AtmId )
if(@ConsoMMJrSmDer is null)
begin
set @ConsoMMJrSmDer=0
end
set @MoyenneMoisPrec =(select coalesce(AVG(SumRetrait),0) from  [dbo].[Factor] where datepart(Month,TransactionDay)=datepart(Month,dATEADD(Month,-1,@TransactionDay)) and datepart(year,TransactionDay)=Datepart(year ,DATEADD(month,-1 ,@TransactionDay)) and SumRetrait is not null and  Atm_Id=@AtmId  )
if(@MoyenneMoisPrec is null)
begin
set @MoyenneMoisPrec=0
end
set @MoyenneMMSAnPrec =(select coalesce(AVG(SumRetrait),0) from  [dbo].[Factor] where Datepart (week, TransactionDay)=Datepart (week, @TransactionDay) and  Datepart (year, TransactionDay)=Datepart (year, dATEADD(year,-1,@TransactionDay)) and SumRetrait is not null and  Atm_Id=@AtmId)
if(@MoyenneMMSAnPrec is null)
begin
set @MoyenneMMSAnPrec=0
end
set @MoyenneMMmAnPrec =(select coalesce(AVG(SumRetrait),0) from  [dbo].[Factor] where datepart(year,TransactionDay)=datepart(year,dATEADD(year,-1,@TransactionDay))  and datepart(Month,TransactionDay)=datepart(Month,@TransactionDay) and SumRetrait is not null and Atm_Id=@AtmId )
if(@MoyenneMMmAnPrec is null)
begin
set @MoyenneMMmAnPrec=0
end
set @ConsommationMaxMDer   =(select Max(SumRetrait) from  [dbo].[Factor] where Datepart(Month,TransactionDay)=Datepart(Month,dATEADD(Month,-1,@TransactionDay)) and Datepart(year,TransactionDay)=Datepart(year ,DATEADD(Month,-1 ,@TransactionDay))  and Atm_Id=@AtmId )
if(@ConsommationMaxMDer is null)
begin
set @ConsommationMaxMDer=0
end

set @ConsommationMaxSDer   =(select coalesce (Max(SumRetrait),0 ) from [dbo].[Factor] where datepart(week,TransactionDay)=datepart(week,Dateadd(week,-1,@TransactionDay)) and  Datepart(Month,TransactionDay)=Datepart(Month,Dateadd(week,-1,@TransactionDay)) and Datepart(year,TransactionDay)=Datepart(year,Dateadd(week,-1,@TransactionDay) ) and Atm_Id=@AtmId )
if(@ConsommationMaxSDer is null)
begin
set @ConsommationMaxSDer=0
end

insert into [dbo].[Factor]([Atm_Id],[SumRetrait],[Bais],[TransactionDay],[ConsommationHier],[MSemaineDernier],
[MSemaine7],[ConsoMmJrAnP],[ConsoMmJrMP],[ConsoMMJrSmDer],[MoyenneMoisPrec],[MoyenneMMSAnPrec],[MoyenneMMmAnPrec],[ConsommationMaxMDer],[ConsommationMaxSDer],[PoidTot],[intweekMonth])values
( @AtmId,@SumRetrait,1,@TransactionDay,@ConsommationHier,@MSemaineDernier,@MSemaine7,@ConsoMmJrAnP,@ConsoMmJrMP,@ConsoMMJrSmDer,@MoyenneMoisPrec,@MoyenneMMSAnPrec,@MoyenneMMmAnPrec,@ConsommationMaxMDer,@ConsommationMaxSDer,(@poidJr*@poidName),@intweekMonth)


End
GO
/****** Object:  StoredProcedure [dbo].[LastfactorsofAllGabs]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LastfactorsofAllGabs]
AS
Declare @AtmIdd  nvarchar(128)
Declare @Transaction Datetime
BEGIN
set @Transaction=(select Dateadd(day,-1, GETDATE()))
DECLARE mycursFinal CURSOR
for  select  distinct Id  from [dbo].[Atm]
open mycursFinal
FETCH NEXT FROM mycursFinal INTO @AtmIdd 
While @@FETCH_STATUS = 0
begin
Exec [dbo].[lastFactor]    @AtmID=@AtmIdd,  @TransactionDay=@Transaction
fetch Next from mycursFinal INTO @AtmIdd 
END
close mycursFinal
Deallocate mycursFinal
    
END
GO
/****** Object:  StoredProcedure [dbo].[PS_DeleteATM]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[PS_DeleteATM]
(
@AtmId varchar(128)
)
as
declare @adressId int

begin
select @adressId = Address_Id from ATM where Id= @AtmId
Delete  from AtmContact where Atm_Id = @AtmId
--Delete  from  AVAtms
--Delete  from  dbo.AVATMStates
--Delete  from dbo.AVTransactions
delete from BugHistory where Bug_Id in (select id from  Bug where AtmError_Id in (select id from AtmError where Atm_Id=@AtmId))

delete from BugComment where Bug_Id in (select id from  Bug where AtmError_Id in (select id from AtmError where Atm_Id=@AtmId))

delete from BugAttachment where Bug_Id in (select id from  Bug where AtmError_Id in (select id from AtmError where Atm_Id=@AtmId))

delete from ErrTypeId where Bug_Id in (select id from  Bug where AtmError_Id in (select id from AtmError where Atm_Id=@AtmId))
delete from Bug where AtmError_Id in (select id from AtmError where Atm_Id=@AtmId)
delete from AtmError where Atm_Id=@AtmId
Delete  from dbo.BugAtm where Atm_Id = @AtmId
Delete  from dbo.AtmCommError where Atm_Id = @AtmId


delete from Im_Move_Inventory where AtmInventory_id = (select Id from Im_Atm_Inventory where Atm_id = @AtmId)
delete from Im_Atm_Inventory where Atm_id = @AtmId
delete from Im_Computer_Inventory where Id not in (select Computer_id from Im_Atm_Inventory)


Delete  from  dbo.JobAtm where Atm_Id = @AtmId
Delete  from dbo.JobAtmExecutionResult where Atm_Id = @AtmId
Delete  from  dbo.JobCommandExecutionResult where Atm_Id = @AtmId
Delete  from dbo.JournalEntry where Atm_Id = @AtmId
Delete  from dbo.JournalEntryMontly where Atm_Id= @AtmId
Delete  from dbo.UserAtm where Atm_Id = @AtmId
Delete  from dbo.TreeViewDetail where FilsId = @AtmId
Delete  from dbo.StateFieldInt where ComponentState_State_Id in (select id from dbo.State where Atm_Id = @AtmId )
Delete  from dbo.StateFieldStr where ComponentState_State_Id in (select id from dbo.State where Atm_Id = @AtmId )
Delete  from dbo.ComponentState where State_Id in (select id from dbo.State where Atm_Id = @AtmId )
Delete  from dbo.CassetteStock where CashStock_Id in (select id from dbo.CashStock where State_Id in (select id from dbo.State where Atm_Id = @AtmId ) )
Delete  from dbo.CashStock where State_Id in (select id from dbo.State where Atm_Id = @AtmId )
delete from RecentAtmState where State_Id in (select id from dbo.State where Atm_Id = @AtmId )
delete from ErrTypeId where State_Id in (select id from dbo.State where Atm_Id = @AtmId )
delete from DoneOrder where Atm_Id=@AtmId
--delete from dbo.[Order] where Atm_Id=@AtmId

delete from dbo.AtmCashAlert where Atm_Id=@AtmId
Delete  from dbo.[State] where Atm_Id = @AtmId


delete from Incident where Atm_Id = @AtmId
delete from Factor where Atm_Id = @AtmId
Delete from Predictor where Atm_Id = @AtmId
delete from AVAtmConfig where Atm_Id=@AtmId

delete from Parameters where Atm_Id=@AtmId
delete from CommandControl where Atm_Id=@AtmId
delete from AtmRemarque where Atm_Id=@AtmId
delete from ATM where ID = @AtmId
--Delete from Address where ID = @adressId

--use AtmView

--declare @atmid as nVarchar(50)

-- select top(1) @atmid = id from atm where id!='SCB_TEST'
-- print @atmid
--	exec PS_DeleteATM @atmid
end
GO
/****** Object:  StoredProcedure [dbo].[PS_DeleteBank]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_DeleteBank]
(
@BankId int
)
as
--supprimer region atm , city branch et les association et adress
--Delete from TreeViewDetail where FilsType = 2 and 

Delete from bank where ID =5 --@BankId
GO
/****** Object:  StoredProcedure [dbo].[PS_DeleteBranch]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_DeleteBranch]
(
@BranchId int
)
as
declare @atmId varchar(128)
--supprimer les atm lié a la branche
declare cursAtm Cursor for 
select filsId from TreeViewDetail where ParentId = @BranchId and  ParentType = 5;
open cursAtm
fetch next from cursAtm into @atmId
while @@FETCH_STATUS = 0
begin
exec dbo.PS_DeleteATM  @atmId
fetch next from cursAtm into @atmId
end
close cursAtm;
deallocate cursAtm;
GO
/****** Object:  StoredProcedure [dbo].[PS_DeleteCashPoint]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_DeleteCashPoint]
(
@CashPointId int
)
as

begin
Delete  from CashPointContact where CashPoint_Id = @CashPointId
Delete  from dbo.TreeViewDetail where FilsId = cast(@CashPointId as nvarchar)
delete from CashPoint where ID = @CashPointId

end
GO
/****** Object:  StoredProcedure [dbo].[PS_DeleteCity]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_DeleteCity]
(
@CityId int
)
as
declare @filsId varchar(128), @filsType int
--supprimer les atm et les  banche lié a la ville
declare cursAtm Cursor for 
select filsId, filsType from TreeViewDetail where ParentId = @CityId and  ParentType = 4 ;
open cursAtm
fetch next from cursAtm into @filsId, @filsType
while @@FETCH_STATUS = 0
begin
if @filsType = 6
exec dbo.PS_DeleteATM  @filsId
else 
exec  PS_DeleteBranch @filsId


fetch next from cursAtm into @filsId, @filsType
end
close cursAtm;
deallocate cursAtm;


--supprimer les relation dans TreeViewDetail
Delete  from dbo.TreeViewDetail where FilsId = @CityId and FilsType = 4
Delete  from dbo.TreeViewDetail where  ParentId = @CityId and ParentType = 4
--supprimer la ville
Delete  from City where Id = @CityId
GO
/****** Object:  StoredProcedure [dbo].[PS_DeleteJob]    Script Date: 02/12/2024 14:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_DeleteJob]
(
@JobId int
)
as

Delete from JobAtm where Job_Id = @JobId
Delete from JobAtmExecutionResult where Job_Id = @JobId
Delete from dbo.JobCommand  where Job_Id= @JobId
Delete from dbo.JobCommandExecutionResult where Job_Id= @JobId
Delete from Job where ID = @JobId
GO
/****** Object:  StoredProcedure [dbo].[PS_DeleteRegion]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_DeleteRegion]
(
@RegionId int
)
as
declare @filsId varchar(128), @filsType int
--supprimer les atm et les  ville lié a la region
declare cursRegion Cursor for 
select filsId, filsType from TreeViewDetail where ParentId = @RegionId and  ParentType = 3 ;
open cursRegion
fetch next from cursRegion into @filsId, @filsType
while @@FETCH_STATUS = 0
begin
if @filsType = 6
exec dbo.PS_DeleteATM  @filsId
else 
exec  PS_DeleteCity @filsId


fetch next from cursRegion into @filsId, @filsType
end
close cursRegion;
deallocate cursRegion;


--supprimer les relation dans TreeViewDetail
Delete  from dbo.TreeViewDetail where FilsId = @RegionId and FilsType = 3
Delete  from dbo.TreeViewDetail where  ParentId = @RegionId and ParentType = 3
--supprimer la region
Delete  from Region where Id = @RegionId
GO
/****** Object:  StoredProcedure [dbo].[PS_DeleteStates]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PS_DeleteStates]

AS

begin

 select   id into #tmpId from dbo.State  where id not in (select State_Id from dbo.AtmError) and id not in (select StateBefore_Id from dbo.DoneOrder) and id not in (select StateAfter_Id from dbo.DoneOrder) and id not in (select State_id from dbo.RecentAtmState) and id not in (select State_Id from dbo.AtmCashAlert)
 and id not in (select LastState_Id from dbo.AtmCommError) order by id asc
 



delete from dbo.StateFieldInt where ComponentState_State_Id in (select id from #tmpId)
delete from dbo.StateFieldStr where ComponentState_State_Id in (select id from #tmpId)
delete from dbo.ComponentState where State_Id in (select id from #tmpId)

delete from dbo.CassetteStock where CashStock_Id in (select Id from dbo.CashStock where State_Id in (select id from #tmpId))
delete from dbo.CashStock where State_Id in (select id from #tmpId)
delete from dbo.ErrTypeId where State_Id in (select id from #tmpId)
delete from dbo.AtmCashAlertExhaution where State_Id in (select id from #tmpId)
delete from dbo.AtmCashAlertWarning where State_Id in (select id from #tmpId)

delete from dbo.State where Id in (select id from #tmpId)
delete from dbo.journalEntry where cast(EntryTime as Date)< cast(GetDate()-3 as Date)
drop table #tmpId
END
GO
/****** Object:  StoredProcedure [dbo].[PS_GetAtmJobListToExecute]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_GetAtmJobListToExecute]
as
declare  @StartDate datetime ,@StartDateDHM datetime
begin
 SET @StartDate = GetDate()
 SET @StartDateDHM = DateAdd(ss,- (DatePart(ss,@StartDate)), @StartDate) 
 SET @StartDateDHM = DateAdd(ms,- (DatePart(ms,@StartDateDHM)), @StartDateDHM) 

--job periodique  et daily
select JobAtm.Job_Id, JobAtm.Atm_Id from Job
join JobAtm on  Job.Id=JobAtm.Job_Id
where JobType_Id=2 --periodique
and Frequence=1 --daily
and StartHour = CONVERT(VARCHAR(5),getdate(),108) 
and (ExpirationDate  is null or(ExpirationDate is not null and GETDATE() <= ExpirationDate  ))

union 
--job periodique  et Monthly
select JobAtm.Job_Id, JobAtm.Atm_Id from Job
join JobAtm on  Job.Id=JobAtm.Job_Id
where JobType_Id=2 --periodique
and Frequence= 3 -- monthly
and StartHour = CONVERT(VARCHAR(5),getdate(),108) 
and( DayOfMonth  is not  null and DayOfMonth =  DATEPART(DAY, GETDATE()))
and (ExpirationDate  is null or(ExpirationDate is not null and GETDATE() <= ExpirationDate  ))

union
--job periodique  et Weekly
select JobAtm.Job_Id, JobAtm.Atm_Id from Job
join JobAtm on  Job.Id=JobAtm.Job_Id
where JobType_Id=2 --periodique
and Frequence= 2 -- weekly
and StartHour = CONVERT(VARCHAR(5),getdate(),108) 
and(DayOfWeek  is not  null and DayOfWeek =  DATEPART(dw, GETDATE())+1) --DATEPART(dw, GETDATE()) = 1 pour lundi et 7 pour dimanche
and (ExpirationDate  is null or(ExpirationDate is not null and GETDATE() <= ExpirationDate  ))

union
--scheduled job
select JobAtm.Job_Id, JobAtm.Atm_Id from Job
join JobAtm on  Job.Id=JobAtm.Job_Id
where JobType_Id=3 --scheduled
and StartHour = CONVERT(VARCHAR(5),getdate(),108) 
and ScheduledDate  is not  null and DATEPART (day, ScheduledDate) =  DATEPART(day, GETDATE())
and DATEPART (MONTH, ScheduledDate) =  DATEPART(MONTH, GETDATE()) 
and DATEPART (YEAR, ScheduledDate) =  DATEPART(YEAR, GETDATE())  
and (ExpirationDate  is null or(ExpirationDate is not null and GETDATE() <= ExpirationDate  ))

--les jobs execute en echec et a reexecuter
union 

select JobAtm.Job_Id, JobAtm.Atm_Id
/*JobAtmExecutionResult.*, StartHour, RetryInterval, RetryTimes ,
  convert(datetime,CONVERT(VARCHAR(10),getdate(),103) +  ' ' + StartHour) ,
  dateadd(mi,RetryInterval * JobAtmExecutionResult.ExecutionNumber ,convert(datetime,CONVERT(VARCHAR(10),getdate(),103) +  ' ' + StartHour) ) 
 */
 from
 Job join JobAtmExecutionResult 
on job.Id= JobAtmExecutionResult.Job_Id
join JobAtm on Job.Id=JobAtm.Job_Id
 where Result=0
 and dateadd(mi,RetryInterval * JobAtmExecutionResult.ExecutionNumber  ,convert(datetime,CONVERT(VARCHAR(10),getdate(),103) +  ' ' + StartHour) )= @StartDateDHM
 --and StartHour = CONVERT(VARCHAR(5),getdate(),108) 
 and ExecutionNumber < RetryTimes
 and (ExpirationDate  is null or(ExpirationDate is not null and GETDATE() <= ExpirationDate  ))
 AND
 (
     ( JobType_Id=2 and Frequence=1 )--daily 
  OR ( JobType_Id=2 and Frequence= 3 and( DayOfMonth  is not  null and DayOfMonth =  DATEPART(DAY, GETDATE())))-- monthly
  OR (  JobType_Id=2 and Frequence= 2 and(DayOfWeek  is not  null and DayOfWeek =  DATEPART(dw, GETDATE())+1) ) --weekly
  OR ( JobType_Id=3 and  ScheduledDate  is not  null and DATEPART (day, ScheduledDate) =  DATEPART(day, GETDATE())
and DATEPART (MONTH, ScheduledDate) =  DATEPART(MONTH, GETDATE()) 
and DATEPART (YEAR, ScheduledDate) =  DATEPART(YEAR, GETDATE()))
 )
 

end
GO
/****** Object:  StoredProcedure [dbo].[PS_GetJournalEntryData]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_GetJournalEntryData]
(
@AtmId varchar(50),--SI '0'--recherche dans tous les atm de l user
@UserId nvarchar(128),
@IsAdmin smallInt , -- 1 admin ,0 user
@TodayJournal int, --1 rcheche dans journal du jour 0 recherche dans journal montly
@StartDate datetime ,
@EndDate datetime,
@SearchText varchar(200)
)
as
DECLARE @SQLQuery AS NVARCHAR(1000), @WhereExiste smallInt, @TableName varchar(20)
--declare  @StartDate datetime ,@StartDateDHM datetime

begin
set @WhereExiste = 0
SET @SQLQuery = 'SELECT Atm_Id,data, filename, Entrytime FROM '

if @TodayJournal=1
   begin
     set @SQLQuery = @SQLQuery + '   dbo.JournalEntry '
     set @TableName = ' dbo.JournalEntry' 
   end
else
   begin
     set @SQLQuery = @SQLQuery + '   dbo.JournalEntryMontly '
     set @TableName = ' dbo.JournalEntryMontly' 
   end 

if @AtmId  = '0'--recherche dans tous les atm de l user
begin
  if @IsAdmin =0 
    begin
     set @SQLQuery = @SQLQuery + ' Join UserAtm On ' + @TableName + '.Atm_Id  = UserAtm.Atm_Id  Where UserAtm.User_Id = ''' + @UserId + ''''
     set @WhereExiste = 1
    end
   
end


if @WhereExiste = 0
 set @SQLQuery = @SQLQuery + ' Where 1 = 1 '
 
if @TodayJournal=1
set @SQLQuery = @SQLQuery + ' and CONVERT(VARCHAR(20),EntryTime,105)   = CONVERT(VARCHAR(20),getdate(),105) '

if @AtmId  <> '0'
   set @SQLQuery = @SQLQuery + ' and  atm_id = ''' + @AtmId + ''''

IF @SearchText is not null
   set @SQLQuery = @SQLQuery + ' and  Data like ''%'' + ''' + @SearchText + ''' + ''%'''

if @StartDate  is not null 
   set @SQLQuery = @SQLQuery + ' and ''' + cast(@StartDate as  varchar(20))  + ''' <= EntryTime '
  
if @EndDate  is not null 
   set @SQLQuery = @SQLQuery + ' and EntryTime <= ''' + cast(@EndDate as varchar(20)) + ''''
 
    
PRINT @SQLQuery
EXEC  sp_Executesql   @SQLQuery 
--select * FROM  dbo.JournalEntry 
END
GO
/****** Object:  StoredProcedure [dbo].[PS_GetJournalEntryDataArchive]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PS_GetJournalEntryDataArchive]
(
@AtmId varchar(50),--SI '0'--recherche dans tous les atm de l user
@UserId nvarchar(128),
@IsAdmin smallInt , -- 1 admin ,0 user
@TodayJournal int, --1 rcheche dans journal du jour 0 recherche dans journal montly
@StartDate datetime ,
@EndDate datetime,
@SearchText varchar(200)
)
as
DECLARE @SQLQuery AS NVARCHAR(1000), @WhereExiste smallInt, @TableName varchar(20)
--declare  @StartDate datetime ,@StartDateDHM datetime

begin
set @WhereExiste = 0
SET @SQLQuery = 'SELECT Atm_ID ,data, filename, Entrytime FROM '


   begin
     set @SQLQuery = @SQLQuery + '   [AVJournalArchive].dbo.JournalEntry '
     set @TableName = ' [AVJournalArchive].dbo.JournalEntry' 
   end 

if @AtmId  = '0'--recherche dans tous les atm de l user
begin
  if @IsAdmin =0 
    begin
     set @SQLQuery = @SQLQuery + ' Join UserAtm On ' + @TableName + '.Atm_Id  = UserAtm.Atm_Id  Where UserAtm.User_Id = ''' + @UserId + ''''
     set @WhereExiste = 1
    end
   
end


if @WhereExiste = 0
 set @SQLQuery = @SQLQuery + ' Where 1 = 1 '
 
if @TodayJournal=1
set @SQLQuery = @SQLQuery + ' and CONVERT(VARCHAR(20),EntryTime,105)   = CONVERT(VARCHAR(20),getdate(),105) '

if @AtmId  <> '0'
   set @SQLQuery = @SQLQuery + ' and  atm_id = ''' + @AtmId + ''''

IF @SearchText is not null
   set @SQLQuery = @SQLQuery + ' and  Data like ''%'' + ''' + @SearchText + ''' + ''%'''

if @StartDate  is not null 
   set @SQLQuery = @SQLQuery + ' and ''' + cast(@StartDate as  varchar(20))  + ''' <= EntryTime '
  
if @EndDate  is not null 
   set @SQLQuery = @SQLQuery + ' and EntryTime <= ''' + cast(@EndDate as varchar(20)) + ''''
 
    
PRINT @SQLQuery
EXEC  sp_Executesql   @SQLQuery 
--select * FROM  dbo.JournalEntry 
END
GO
/****** Object:  StoredProcedure [dbo].[PS_GetTransactionByParams]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[PS_GetTransactionByParams]
( 
@StartDate datetime,
@EndDate datetime,
@AtmId varchar(10)
)

as
select 
AtmID ,
TransactionDate,
TransactionNumber,
AutorisationNumber,
CardNumber,
Rib,
Type,
Amount,
Statut,
isCashPresented,
isCashTaken,
isCashRetracted,
isCashoutError,
ExistInHost
FROM AVTransaction

WHERE 
AtmID = @AtmId and
TransactionDate between @StartDate and @EndDate
GO
/****** Object:  StoredProcedure [dbo].[PS_InsertTransactionDATA]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--where Profile='WN'
CREATE PROCEDURE [dbo].[PS_InsertTransactionDATA]
AS
Declare @AtmIdd nvarchar(128)
Declare @Datetrx datetime
BEGIN




DECLARE mycursFinal CURSOR
for  select   Id  from [dbo].Atm
open mycursFinal
FETCH NEXT FROM mycursFinal INTO @AtmIdd 
While @@FETCH_STATUS = 0

begin
execute [dbo].PS_TransactionTestDATA    @AtmId=@AtmIdd  


 fetch Next from mycursFinal INTO @AtmIdd 
END
close mycursFinal
Deallocate mycursFinal

DECLARE mycursreject CURSOR
for  select   atmid,transactiondate  from [dbo].AVTransaction where statut =5
open mycursreject
FETCH NEXT FROM mycursreject INTO @AtmIdd , @datetrx
While @@FETCH_STATUS = 0

begin

update AVTransaction set Statut = (SELECT top(1)id FROM AtmRejectStatut  where id in (100,101,106,107,116,121,122) ORDER BY NEWID() ) where AtmID=@AtmIdd and TransactionDate=@Datetrx

 fetch Next from mycursreject INTO @AtmIdd , @datetrx
END
close mycursreject
Deallocate mycursreject
END
GO
/****** Object:  StoredProcedure [dbo].[PS_TransactionTestData]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[PS_TransactionTestData]
(
@AtmId varchar(128)
)
as

begin

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T01:06:44.000' AS Time)As datetime), 227266, NULL, N'100100****8523', N'IBAN', N'Withdrawal', 10000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T02:27:08.000' AS  Time)As datetime), 227268, N'334106', NULL, NULL, N'Cardless Withdrawal', 1000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T02:27:09.000' AS  Time)As datetime), 227269, NULL, NULL, NULL, N'Other', 0, 5, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T02:27:10.000' AS  Time)As datetime), 227271, NULL, NULL, NULL, N'Other', 0, 5, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T02:27:11.000' AS  Time)As datetime), 227272, NULL, NULL, NULL, N'Other', 0, 5, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T04:06:56.000' AS  Time)As datetime), 227276, N'073003', NULL, NULL, N'Cardless Withdrawal', 200, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T05:36:54.000' AS  Time)As datetime), 227278, N'030320', NULL, NULL, N'Cardless Withdrawal', 1000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T08:00:42.000' AS  Time)As datetime), 227280, NULL, N'100100****8946', N'IBAN', N'Withdrawal', 400, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T08:00:43.000' AS  Time)As datetime), 227281, NULL, N'100100****8946', NULL, N'Transaction Rejected', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T08:00:44.000' AS  Time)As datetime), 227282, NULL, N'100100****8946', NULL, N'Other', 0, 5, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T08:00:46.000' AS  Time)As datetime), 227283, NULL, N'100100****7903', NULL, N'Transaction Rejected', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T08:30:13.000' AS  Time)As datetime), 227287, NULL, N'100100****7903', NULL, N'Transaction Rejected', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T08:30:19.000' AS  Time)As datetime), 227288, NULL, N'100100****9914', NULL, N'Other', 0, 5, 0, 0, 0, 0, 0, 0, N'')


INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T12:02:30.000' AS  Time)As datetime), 227289, NULL, N'100100****3356', NULL, N'Transaction Rejected', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T12:05:22.000' AS  Time)As datetime), 227291, NULL, N'100100****3356', NULL, N'Transaction Rejected', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T12:05:23.000' AS  Time)As datetime), 227293, NULL, NULL, NULL, N'Other', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T12:23:11.000' AS  Time)As datetime), 227294, NULL, N'100100****4014', N'', N'Withdrawal', 1000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T12:45:01.000' AS  Time)As datetime), 227295, NULL, N'100100****2435', NULL, N'Balance Inquiry', 0, 0, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T13:23:40.000' AS  Time)As datetime), 227298, NULL, N'100100****2988', N'IBAN', N'Withdrawal', 2000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T13:48:14.000' AS  Time)As datetime), 227300, N'717360', NULL, NULL, N'Cardless Withdrawal', 4000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T13:48:15.000' AS  Time)As datetime), 227302, NULL, NULL, NULL, N'Cardless Withdrawal', 4000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T13:48:16.000' AS  Time)As datetime), 227305, NULL, NULL, NULL, N'Cardless Withdrawal', 4000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T13:48:17.000' AS  Time)As datetime), 227306, NULL, NULL, NULL, N'Cardless Withdrawal', 4000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T13:48:18.000' AS  Time)As datetime), 227307, NULL, NULL, NULL, N'Cardless Withdrawal', 4000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T14:27:03.000' AS  Time)As datetime), 227310, N'660736', NULL, NULL, N'Cardless Withdrawal', 2000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T14:27:04.000' AS  Time)As datetime), 227312,  NULL, N'100100****4014', N'', N'Withdrawal', 1000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T14:27:05.000' AS  Time)As datetime), 227320,  NULL, N'100100****4014', N'', N'Withdrawal', 1000, 0, 1, 1, 0, 0, 0, 0, N'')


INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T14:46:38.000' AS  Time)As datetime), 227324, N'846550', NULL, NULL, N'Cardless Withdrawal', 6000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T14:46:39.000' AS  Time)As datetime), 0, NULL, NULL, NULL, N'Other', 0, 5, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:25:11.000' AS  Time)As datetime), 227327, NULL, N'100100****5397', NULL, N'Balance Inquiry', 0, 0, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:26:11.000' AS  Time)As datetime), 227328, NULL, N'100100****5397', NULL, N'Transaction Rejected', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:26:32.000' AS  Time)As datetime), 227329, NULL, N'100100****5397', NULL, N'Balance Inquiry', 0, 0, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:27:13.000' AS  Time)As datetime), 227330, NULL, N'100100****5397', N'', N'Withdrawal', 3000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:27:48.000' AS  Time)As datetime), 227331, NULL, N'100100****5397', NULL, N'Balance Inquiry', 0, 0, 0, 0, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:52:59.000' AS  Time)As datetime), 227332, NULL, N'100100****8585', NULL, N'Transaction Rejected', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:53:00.000' AS  Time)As datetime), 227333, NULL, N'100100****5397', N'', N'Withdrawal', 3000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:53:01.000' AS  Time)As datetime), 227334, NULL, N'100100****5397', N'', N'Withdrawal', 3000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:53:02.000' AS  Time)As datetime), 227335, NULL, N'100100****5397', N'', N'Withdrawal', 3000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T15:53:03.000' AS  Time)As datetime), 227336, NULL, N'100100****5397', N'', N'Withdrawal', 3000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T16:32:26.000' AS  Time)As datetime), 227342, N'641182', NULL, NULL, N'Cardless Withdrawal', 2000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T17:37:20.000' AS  Time)As datetime), 227344, NULL, N'100100****2454', N'IBAN', N'Withdrawal', 1000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T17:37:21.000' AS  Time)As datetime), 227345,  NULL,N'100100****5397', N'', N'Withdrawal', 3000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T17:48:07.000' AS  Time)As datetime), 227346, NULL, N'100100****7015', N'IBAN', N'Withdrawal', 5000, 0, 1, 1, 0, 0, 0, 0, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T17:54:14.000' AS  Time)As datetime), 227347, NULL, N'****6768', NULL, N'Transaction Rejected', 0, 5, 0, 0, 0, 0, 0, 1, N'')

INSERT [dbo].[AVTransaction] ([AtmID], [TransactionDate], [TransactionNumber], [AutorisationNumber], [CardNumber], [Rib], [Type], [Amount], [Statut], [isCashPresented], [isCashTaken], [isCashRetracted], [isCashoutError], [ExistInHost], [IsRejected], [TrxPictures]) VALUES (@AtmId, cast(CAST(GETDATE() AS date)As datetime) + cast (CAST(N'2020-12-24T18:04:50.000' AS  Time)As datetime), 227349, NULL, N'100100****0230', N'IBAN', N'Withdrawal', 2000, 0, 1, 1, 0, 0, 0, 0, N'')

end 
GO
/****** Object:  StoredProcedure [dbo].[TenDaysAuto]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TenDaysAuto]
AS
Declare @AtmIdd nvarchar(128)
Declare @i int
Declare @TrueCurrentDay Date
Declare @CurrentDay1 Date
BEGIN


Truncate Table dbo.predictor
set @TrueCurrentDay= cast (GETDATE() as Date)

DECLARE mycursFinal CURSOR
for  select  distinct Atm_Id  from [dbo].[Factor] 
open mycursFinal
FETCH NEXT FROM mycursFinal INTO @AtmIdd 
While @@FETCH_STATUS = 0
begin
set @i=0
While @i<15
begin
set @CurrentDay1= DATEADD(day,@i,@TrueCurrentDay)
execute [dbo].[CurrentFactoreswithoutNulls]    @AtmId=@AtmIdd  ,@CurrentDay=@CurrentDay1
set @i = @i+1
END
 fetch Next from mycursFinal INTO @AtmIdd 
END
close mycursFinal
Deallocate mycursFinal
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteState]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_DeleteState]


@StateId int

AS
BEGIN

delete from dbo.StateFieldInt where ComponentState_State_Id = @StateId
delete from dbo.StateFieldStr where ComponentState_State_Id = @StateId
delete from dbo.ComponentState where State_Id = @StateId

delete from dbo.CassetteStock where CashStock_Id in (select Id from dbo.CashStock where State_Id = @StateId)
delete from dbo.CashStock where State_Id = @StateId

delete from dbo.ErrTypeId where State_Id = @StateId

delete from dbo.State where Id = @StateId

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAvgTranxEvents]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_GetAvgTranxEvents]


@StartDate Datetime,
@EnDate Datetime,
@IsAdmin bit,
@ActifOnly bit,
@UserAtms dbo.UserAtms READONLY
AS
BEGIN

declare @t table(id int,color nvarchar(50),label nvarchar(250),Highlight nvarchar(50),Highlight_Fr nvarchar(250), Duration float)
select EventType_Id,AVG(cast(duration as float)) avg_duration into #temp FROM dbo.Pr_TransactionEvents where TransactionDate>=@StartDate and TransactionDate <=@EnDate and AtmID in (select AtmID from @UserAtms) and AtmID not in (select Id from Atm where Actif = 0)
group by EventType_Id


insert into @t (id,color,label,Highlight,Highlight_Fr,Duration) select id,color,label,Highlight,Highlight_Fr,avg_duration from #temp a join Pr_EventsType b on a.EventType_Id=b.Id

drop table #temp 
select * from @t

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAvTransactions]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetAvTransactions] 

@StartDate Datetime,
@EnDate  Datetime,
@IsAdmin bit,
@ActifOnly bit,
@UserAtms dbo.UserAtms READONLY

AS 
--declare @AvTransaction Table (AtmID nvarchar(128)
--      ,TransactionDate datetime
--      ,TransactionNumber int
--      ,AutorisationNumber nvarchar(max)
--      ,CardNumber nvarchar(max)
--      ,Rib nvarchar(max)
--      ,Type nvarchar(max)
--      ,Amount int
--      ,Statut int
--      ,isCashPresented bit
--      ,isCashTaken bit
--      ,isCashRetracted bit
--      ,isCashoutError bit
--      ,ExistInHost bit
--      ,IsRejected bit
--      ,TrxPictures nvarchar(max))

--declare @Test nvarchar(max)
--declare @counter int 

begin
--set @Test = (select Id from dbo.AspNetRoles where Name='Admin')
--set @counter =(select count(*) from dbo.AspNetUserRoles where USERID=(select top 1 USER_ID from @UserAtms) and RoleId=@Test)
--set @counter =(select count(*) from @UserAtms)
if(@StartDate!=cast(cast(GetDate() as Date) as datetime))
	begin
		if(@ActifOnly = 1)
			begin
				select  * from dbo.AVTransaction  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms) and AtmID not in (select Id from Atm where Actif = 0)
			end
		else
			begin
				select  * from dbo.AVTransaction  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms)
			end
	end
else
	begin
		if(@ActifOnly = 1)
			begin
				select  * from dbo.lastDealyTrx  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms) and AtmID not in (select Id from Atm where Actif = 0)
			end
		else
			begin
				select  * from dbo.lastDealyTrx  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms)
			end
	end
--select * from @AvTransaction
end
GO
/****** Object:  StoredProcedure [dbo].[usp_GetTransactionErrors]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetTransactionErrors] 

@StartDate Datetime,
@EnDate  Datetime,
@IsAdmin bit,
@ActifOnly bit,
@UserAtms dbo.UserAtms READONLY

AS 
--declare @AvTransaction Table (AtmID nvarchar(128)
--      ,TransactionDate datetime
--      ,TransactionNumber int
--      ,AutorisationNumber nvarchar(max)
--      ,CardNumber nvarchar(max)
--      ,Rib nvarchar(max)
--      ,Type nvarchar(max)
--      ,Amount int
--      ,Statut int
--      ,isCashPresented bit
--      ,isCashTaken bit
--      ,isCashRetracted bit
--      ,isCashoutError bit
--      ,ExistInHost bit
--      ,IsRejected bit
--      ,TrxPictures nvarchar(max))

--declare @Test nvarchar(max)
--declare @counter int 

begin
--set @Test = (select Id from dbo.AspNetRoles where Name='Admin')
--set @counter =(select count(*) from dbo.AspNetUserRoles where USERID=(select top 1 USER_ID from @UserAtms) and RoleId=@Test)
--set @counter =(select count(*) from @UserAtms)
if(@StartDate!=cast(cast(GetDate() as Date) as datetime))
	begin
		if(@ActifOnly = 1)
			begin
				select  * from dbo.Pr_TransactionError  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms) and AtmID not in (select Id from Atm where Actif = 0)
			end
		else
			begin
				select  * from dbo.Pr_TransactionError  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms)
			end
	end
else
	begin
		if(@ActifOnly = 1)
			begin
				select  * from dbo.Pr_LastDailyTransactionError  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms) and AtmID not in (select Id from Atm where Actif = 0)
			end
		else
			begin
				select  * from dbo.Pr_LastDailyTransactionError  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms)
			end
	end
--select * from @AvTransaction
end
GO
/****** Object:  StoredProcedure [dbo].[usp_GetTransactionEvents]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetTransactionEvents] 

@StartDate Datetime,
@EnDate  Datetime,
@IsAdmin bit,
@ActifOnly bit,
@UserAtms dbo.UserAtms READONLY

AS 
--declare @AvTransaction Table (AtmID nvarchar(128)
--      ,TransactionDate datetime
--      ,TransactionNumber int
--      ,AutorisationNumber nvarchar(max)
--      ,CardNumber nvarchar(max)
--      ,Rib nvarchar(max)
--      ,Type nvarchar(max)
--      ,Amount int
--      ,Statut int
--      ,isCashPresented bit
--      ,isCashTaken bit
--      ,isCashRetracted bit
--      ,isCashoutError bit
--      ,ExistInHost bit
--      ,IsRejected bit
--      ,TrxPictures nvarchar(max))

--declare @Test nvarchar(max)
--declare @counter int 

begin
--set @Test = (select Id from dbo.AspNetRoles where Name='Admin')
--set @counter =(select count(*) from dbo.AspNetUserRoles where USERID=(select top 1 USER_ID from @UserAtms) and RoleId=@Test)
--set @counter =(select count(*) from @UserAtms)
if(@StartDate!=cast(cast(GetDate() as Date) as datetime))
	begin
		if(@ActifOnly = 1)
			begin
				select  * from dbo.Pr_TransactionEvents  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms) and AtmID not in (select Id from Atm where Actif = 0)
			end
		else
			begin
				select  * from dbo.Pr_TransactionEvents  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms)
			end
	end
else
	begin
		if(@ActifOnly = 1)
			begin
				select  * from dbo.Pr_DailyTransactionEvents  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms) and AtmID not in (select Id from Atm where Actif = 0)
			end
		else
			begin
				select  * from dbo.Pr_DailyTransactionEvents  where TransactionDate>=@StartDate and  TransactionDate<=@EnDate
				and AtmID in (select Atm_ID from  @UserAtms)
			end
	end
--select * from @AvTransaction
end
GO
/****** Object:  Trigger [dbo].[Insertedtrx]    Script Date: 02/12/2024 14:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[Insertedtrx]
ON [dbo].[AVTransaction]
AFTER INSERT
AS
BEGIN
Declare @AtmID nvarchar(max)
Declare @TransactionDate Datetime
Declare @TransactionNumber int
Declare @EventType_Id int
Declare @duration int
Declare @EventTime Datetime
Declare @j int
Declare @Statut bit
Declare @ErrortypeId int

Delete from dbo.LastDealyTrx where cast(TransactionDate as Date )<cast(GetDate() as Date)
  INSERT INTO dbo.LastDealyTrx
     SELECT *
     FROM inserted i where cast(i.TransactionDate as Date )=cast(GetDate() as Date)

 -- Ajoutez la partie suivante pour mettre à jour la date dans la table d'état
    UPDATE s
    SET s.LastTransaction = i.TransactionDate
    FROM dbo.RecentAtmState s
    INNER JOIN inserted i ON s.Atm_Id = i.AtmID


END
GO
ALTER TABLE [dbo].[AVTransaction] ENABLE TRIGGER [Insertedtrx]
GO
/****** Object:  Trigger [dbo].[InsertedJrn]    Script Date: 02/12/2024 14:45:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create TRIGGER [dbo].[InsertedJrn]
ON [dbo].[JournalEntry]
AFTER INSERT
AS
BEGIN


  INSERT INTO dbo.JournalEntryMontly
     SELECT *
     FROM inserted i 



END
GO
ALTER TABLE [dbo].[JournalEntry] ENABLE TRIGGER [InsertedJrn]
GO
/****** Object:  Trigger [dbo].[InsertedLastEvent]    Script Date: 02/12/2024 14:45:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Trigger [dbo].[InsertedLastEvent]
On [dbo].[Pr_TransactionEvents]
After Insert 
as
begin
Delete from dbo.Pr_DailyTransactionEvents where cast(TransactionDate as Date)<cast(GetDate() as Date)
insert into dbo.Pr_DailyTransactionEvents ([AtmID]
      ,[TransactionDate]
      ,[TransactionNumber]
      ,[EventType_Id]
      ,[EventTime]
      ,[Content]
      ,[duration]
      ,[statutTrx])
	  select [AtmID]
      ,[TransactionDate]
      ,[TransactionNumber]
      ,[EventType_Id]
      ,[EventTime]
      ,[Content]
      ,[duration]
      ,[statutTrx] from inserted i where cast(i.TransactionDate as Date)=cast(GetDate() as Date)
end
GO
ALTER TABLE [dbo].[Pr_TransactionEvents] ENABLE TRIGGER [InsertedLastEvent]
GO
