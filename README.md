# Caching
caching samples

###Eslatmalar

#1 
```
Distributed sql server caching
```

Distributed sql server caching ni implement qilishda sql server da baza a table yaratib qo'yish kerak

```sql
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[<Table name>](
  [Id] [nvarchar](449) NOT NULL,
  [Value] [varbinary](max) NOT NULL,
  [ExpiresAtTime] [datetimeoffset](7) NOT NULL,
  [SlidingExpirationInSeconds] [bigint] NULL,
  [AbsoluteExpiration] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
  [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
  IGNORE_DUP_KEY = OFF, 
  ALLOW_ROW_LOCKS = ON, 
  ALLOW_PAGE_LOCKS = ON, 
  OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

```

#2
```
Distributed Redis caching
```

Distributed Redis caching ni implement qilishda Redis ni o'rnatib ```start``` qilib yuborish kerak
