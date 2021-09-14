USE [WebAPIDb]
GO
--Insert in Customer Table
CREATE PROCEDURE [dbo].[spInsertIntoCustomer]
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50), 
	@Address nvarchar(MAX),
	@Telephone nvarchar(50),
	@Email  nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	

INSERT INTO [dbo].[Customer]
           ([Name]
           ,[Address]
		   , [Telephone]
           ,[Email])
     VALUES
           (@Name,@Address, @Telephone, @Email)
END
GO
-- Select from Customer
CREATE PROCEDURE [dbo].[spSelectCustomer]
	-- Add the parameters for the stored procedure here
	--@Name nvarchar(50), 
	--@StartLocation nvarchar(20),
	--@EndLocation nvarchar(20) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT [Id]
      ,[Name]
      ,[Address]
      ,[Telephone]
	  ,[Email]

  FROM [dbo].[Customer]

END
GO
-- Select from Customer By Id
CREATE PROCEDURE [dbo].[spSelectCustomerById]
	-- Add the parameters for the stored procedure here
	@Id int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT [Id]
      ,[Name]
      ,[Address]
      ,[Telephone]
	  ,[Email]
FROM [dbo].[Customer]
WHERE [Id] = @Id

END
GO

--Update a Customer Record
CREATE PROCEDURE [dbo].[spUpdateCustomer]
	-- Add the parameters for the stored procedure here
	@Id int,
	@Name nvarchar(50), 
	@Address nvarchar(MAX),
	@Telephone nvarchar(50),
	@Email nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

UPDATE [dbo].[Customer]
   SET [Name] = @Name
      ,[Address] = @Address
	  ,[Telephone] = @Telephone
	  ,[Email] = @Email
 WHERE [Id] = @Id

END
GO
-- Delete a Customer 
CREATE PROCEDURE [dbo].[spDeleteCustomer]
	-- Add the parameters for the stored procedure here
	@Id int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
DELETE FROM [dbo].[Customer]
WHERE [Id] = @Id

END
GO
-- Order Table
-- Insert In Order Table
CREATE PROCEDURE [dbo].[spInsertIntoOrder]
	-- Add the parameters for the stored procedure here
	@CustomerId int, 
	@Description nvarchar(MAX),
	@OrderCost money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	

INSERT INTO [dbo].[Order]
           ([CustomerId],[Description],[OrderCost])
     VALUES
           (@CustomerId,@Description,@OrderCost)
END
GO

-- Select From Order Table
CREATE PROCEDURE [dbo].[spSelectOrder]
	-- Add the parameters for the stored procedure here
	--@Name nvarchar(50), 
	--@StartLocation nvarchar(20),
	--@EndLocation nvarchar(20) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT [Id]
      ,[CustomerId]
      ,[Description]
      ,[OrderCost]
FROM [dbo].[Order]

END
GO

-- Select From Order By Id
CREATE PROCEDURE [dbo].[spSelectOrderById]
	-- Add the parameters for the stored procedure here
	@Id int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT [Id]
      ,[CustomerId]
	  ,[Description]
	  ,[OrderCost]
FROM [dbo].[Order]
WHERE [Id] = @Id

END
GO
-- Update Order Table
CREATE PROCEDURE [dbo].[spUpdateOrder]
	-- Add the parameters for the stored procedure here
	@Id int,
	@CustomerId int, 
	@Description nvarchar(MAX),
	@OrderCost money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

UPDATE [dbo].[Order]
   SET [CustomerId] = @CustomerId,
     [Description] = @Description,
    [OrderCost] = @OrderCost
 WHERE [Id] = @Id

END
GO

--Delete from Order Table
CREATE PROCEDURE [dbo].[spDeleteOrder]
	-- Add the parameters for the stored procedure here
	@Id int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


DELETE FROM [dbo].[Order]
WHERE [Id] = @Id

END
GO

