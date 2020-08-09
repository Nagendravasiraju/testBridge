
CREATE TABLE tst_product
(
  productid int IDENTITY(1,1) NOT NULL,
  productName varchar(50),
  productDesc varchar(200),
  productPrice decimal(10,2),
  productImage varchar(500)
  )
GO

ALTER Procedure insertProduct(
		@productName varchar(50)
	,   @ProductDesc varchar(200)
	,	@productPrice decimal(10,2)
	,	@productImage varchar(500)=null)
	
as 
BEGIN 
	INSERT INTO tst_product 
	( productName,
	  productDesc,
	  productPrice,
	  productImage) 
	VALUES
	(  @productName, 
	  @productDesc,
	  @productPrice,
	  @productImage 
	)
	
END
 

Go
Create Procedure getProducts(
	@productId INT= NULL 
	)
as 
BEGIN 
	SELECT  productId
		,	productName
		,	productDesc
		,	productPrice
		,	productImage
	FROM	tst_product
	WHERE	productId= ISNULL(@productId, ProductId)
		
END

Go
Create Procedure removeProduct(
	@productId INT
	)
as 
BEGIN 
	DELETE  
	FROM 	tst_product
	WHERE	productId= @productId
		
END