CREATE TABLE [dbo].[Events] ( 
	
	[Id] uniqueidentifier NOT NULL, 
	[AggregateId] uniqueidentifier NOT NULL, 
	[Version] int NOT NULL,
	[Events] nvarchar(MAX) NOT NULL,
	[Created] datetime NOT NULL,

	PRIMARY KEY ( [Id] ),
	UNIQUE ( [AggregateId], [Version] )

)
