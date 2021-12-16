CREATE TABLE [dbo].[Shipment](
	[ShipmentId] [int] IDENTITY(1,1) NOT NULL,
	[SenderName] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[RecipientAddress] [varchar](200) NOT NULL,
	[Expedited] [int] NOT NULL,
	[ShipmentType] [varchar](20) NOT NULL
)