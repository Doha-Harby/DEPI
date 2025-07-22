	use StoreDB
	go

--1. Customer Spending Analysis
	--Write a query that uses variables to find the total amount spent by customer ID 1.
	--Display a message showing whether they are a VIP customer (spent > $5000)
	--or regular customer.
	DECLARE @Ccustomer_ID INT = 1
	declare @Totale_Amount DECIMAL(10,2)

	SELECT @Totale_Amount = SUM(ORDT.[list_price] * ORDT.quantity - (1- ORDT.[discount]) )
	FROM [sales].[orders]  AS ORD
		INNER JOIN [sales].[order_items] ORDT ON ORDT.[order_id] = ORD.[order_id]
	WHERE ORD.[customer_id] =  @Ccustomer_ID 
	IF @Totale_Amount > 5000
		BEGIN
			PRINT 'VIP customer'
		END
	ELSE
		BEGIN
			PRINT 'regular customer'
		END
		GO
--2. Product Price Threshold Report
	--Create a query using variables to count how many products cost more than $1500. 
	--Store the threshold price in a variable and display both the threshold and count in a 
	--formatted message.
	DECLARE @Threshold_Price DECIMAL(10,2) = 1500
	DECLARE @Product_Count INT 
	DECLARE @Formatted_Message VARCHAR(200)

	SELECT @Product_Count = COUNT(*)
	FROM [production].[products]
	WHERE [list_price] > @Threshold_Price
	SET @Formatted_Message = 'Number of products cost more than ' + CAST(@Threshold_Price AS VARCHAR(100) )+
							 ' is '+ CAST(@Product_Count AS VARCHAR(100)) 
	PRINT @Formatted_Message
		GO
--3. Staff Performance Calculator
	--Write a query that calculates the total sales for staff member ID 2 in the year 2017.
	--Use variables to store the staff ID, year, and calculated total. 
	--Display the results with appropriate labels.
	DECLARE @Staff_ID INT = 2
	DECLARE @YEAR INT = 2017
	DECLARE @Calculated_Total DECIMAL(10,2)

	SELECT @Calculated_Total = COUNT([staff_id])
	FROM [sales].[orders]
	WHERE YEAR([order_date]) = @YEAR AND [staff_id] = @Staff_ID
	GROUP BY [staff_id]

	SELECT @Staff_ID as Staff_ID ,@YEAR AS Target_YEAR, @Calculated_Total AS NUM_OF_Orders_Per_Staff
	GO

--4. Global Variables Information
	--	Create a query that displays the current server name, SQL Server version, 
	--	and the number of rows affected by the last statement. Use appropriate global variables.
	SELECT @@SERVERNAME AS Server_Name ,@@VERSION AS SQL_Server_Version,@@ROWCOUNT AS Rows_Affected
	GO
--5.Write a query that checks the inventory level for product ID 1 in store ID 1.
	--Use IF statements to display different messages based on stock levels:
		--If quantity > 20: Well stocked
		--If quantity 10-20: Moderate stock
		--If quantity < 10: Low stock - reorder needed
	DECLARE @Product_ID INT = 1
	DECLARE @Store_ID INT = 1
	DECLARE @Quantity INT 

	SELECT @Quantity = [quantity]
	FROM [production].[stocks]
	WHERE [store_id] = @Store_ID AND [product_id] = @Product_ID
	IF @Quantity > 20
		BEGIN 
			print 'Well stocked'
		END
	ELSE IF @Quantity BETWEEN 10 AND 20
		BEGIN 
			print 'Moderate stock'
		END
	ELSE IF @Quantity < 10 
		BEGIN
			PRINT 'Low stock - reorder needed'
		END

--6.Create a WHILE loop that updates low-stock items (quantity < 5) in batches of 3 
	--products at a time. Add 10 units to each product and display progress messages 
	--after each batch.
	DECLARE @NUM_Batch INT = 1
	DECLARE @Step_Size INT = 3

	WHILE EXISTS ( SELECT 1 FROM [production].[stocks] WHERE quantity < 5 )
		BEGIN
			UPDATE TOP (@Step_Size) [production].[stocks] 
			SET [quantity] = [quantity] + 10
			WHERE quantity < 5 
			PRINT 'Batch ' + CAST(@NUM_Batch AS VARCHAR) + ' Done and 3 rows upgraded'
			SET @NUM_Batch = @NUM_Batch + 1
		END 

	PRINT 'Nothing need to upgrade'

--7. Product Price Categorization#
	--Write a query that categorizes all products using CASE WHEN based on their list price:
	--Under $300: Budget
	--$300-$800: Mid-Range
	--$801-$2000: Premium
	--Over $2000: Luxury
	select [product_id], [product_name],
		CASE 
			WHEN [list_price] < 300 THEN 'Budget'
			WHEN [list_price] BETWEEN 300 AND 800 THEN 'Mid-Range'
			WHEN [list_price] BETWEEN 801 AND 2000 THEN 'Premium'
			WHEN [list_price] > 2000 THEN 'Luxury'
		END AS Product_Level
	FROM [production].[products] 

--8. Customer Order Validation
	--Create a query that checks if customer ID 5 exists in the database. If they exist,
	--show their order count. If not, display an appropriate message.
	DECLARE @Customer_ID INT = 5
	DECLARE @Order_Count INT 

	IF EXISTS (SELECT 1 FROM [sales].[orders] WHERE [customer_id] = @Customer_ID)
		BEGIN
			SET @Order_Count = (SELECT COUNT( [customer_id])
						   	    FROM [sales].[orders]
							    WHERE [customer_id] = @Customer_ID)

			PRINT 'Number of orders per '+ CAST(@Customer_ID AS VARCHAR) + ' is '+ CAST(@Order_Count AS VARCHAR)
		END
	ELSE 
		BEGIN
			PRINT 'Customer '+ CAST(@Customer_ID AS VARCHAR) +' do not have any order'
		END

--9. Shipping Cost Calculator Function
	--Create a scalar function named CalculateShipping that takes an order total as input
	--and returns shipping cost:
	--Orders over $100: Free shipping ($0)
	--Orders $50-$99: Reduced shipping ($5.99)
	--Orders under $50: Standard shipping ($12.99)
	GO
	CREATE FUNCTION dbo.CalculateShipping (@order_total DECIMAL(10,2))
		RETURNS DECIMAL(10,2)
			AS 
		BEGIN
			DECLARE @Shipping_Cost DECIMAL(10,2)

			IF @order_total > 100
				BEGIN 
					SET @Shipping_Cost = 0
				END

			ELSE IF @order_total BETWEEN 50 AND 99
				BEGIN
					SET @Shipping_Cost = 5.99
				END 

			ELSE IF @order_total < 50
				BEGIN 
					SET @Shipping_Cost = 12.99
				END
			RETURN @Shipping_Cost
		END  --FUN
	GO
		SELECT dbo.CalculateShipping(120) AS ShippingCost;   --0
		SELECT dbo.CalculateShipping(75)  AS ShippingCost;   --5.99
		SELECT dbo.CalculateShipping(30)  AS ShippingCost;   --12.99

--10. Product Category Function
	--Create an inline table-valued function named GetProductsByPriceRange that accepts
	--minimum and maximum price parameters and returns all products within that price
	--range with their brand and category information.
	GO
	CREATE FUNCTION dbo.GetProductsByPriceRange(@minimum AS DECIMAL(10,2), @maximum AS DECIMAL(10,2))
		RETURNS TABLE
			AS
		RETURN
		(
			SELECT PP.[product_id],PP.[product_name],PB.brand_name,PC.category_name, PP.list_price
			FROM [production].[products] AS PP
				INNER JOIN [production].[brands] PB ON PB.brand_id = PP.brand_id
				INNER JOIN [production].[categories] PC ON PC.category_id = PP.category_id
			WHERE PP.[list_price] BETWEEN @minimum AND @maximum
		);
		GO
	SELECT *
	FROM dbo.GetProductsByPriceRange(60,2000)

--11. Customer Sales Summary Function
	--Create a multi-statement function named GetCustomerYearlySummary that takes a
	--customer ID and returns a table with yearly sales data including total orders, 
	--total spent, and average order value for each year.
	GO
	CREATE FUNCTION dbo.GetCustomerYearlySummary(@Cusromer_ID INT)
		RETURNS @Summary_Yearly TABLE
		 (
			 Target_Year INT, 
			 Total_Orders INT,
			 Total_Spent DECIMAL(18,2), 
			 Average_Order_Value DECIMAL(18,2)
		)
		AS 
		BEGIN 
			INSERT INTO @Summary_Yearly
				SELECT 
					YEAR( SO.[order_date] ) AS Target_Year,
					COUNT(*) AS Total_Orders,
					SUM(SOT.quantity * SOT.list_price * (1 - SOT.discount)) AS Total_Spent,
					AVG(SOT.quantity * SOT.list_price * (1 - SOT.discount)) AS Average_Order_Value 
				FROM [sales].[orders] AS SO
					INNER JOIN [sales].[order_items] AS SOT ON SOT.[order_id] = SO.[order_id]
				WHERE [customer_id] = @Cusromer_ID
				GROUP BY YEAR( SO.[order_date] )
			RETURN;
		END;
		GO
	SELECT * FROM dbo.GetCustomerYearlySummary(5)

--12. Discount Calculation Function
	--Write a scalar function named CalculateBulkDiscount that determines discount
	--percentage based on quantity:
		--1-2 items: 0% discount
		--3-5 items: 5% discount
		--6-9 items: 10% discount
		--10+ items: 15% discount
	GO
	CREATE FUNCTION dbo.CalculateBulkDiscount(@quantity int)
		RETURNS INT
		BEGIN
			DECLARE @discount INT

			IF(@quantity = 1 OR @quantity = 2)
				SET @discount = 0

			ELSE IF (@quantity BETWEEN 3 AND 5)
				 SET @discount = 5

			 ELSE IF (@quantity BETWEEN 6 AND 9)
				 SET @discount = 10

			 ELSE 
				 SET @discount = 15
			RETURN @discount
		END 
GO
	SELECT  dbo.CalculateBulkDiscount(2) AS Discount_Amount
	SELECT  dbo.CalculateBulkDiscount(3) AS Discount_Amount
	SELECT  dbo.CalculateBulkDiscount(7) AS Discount_Amount
	SELECT  dbo.CalculateBulkDiscount(10) AS Discount_Amount

--13. Customer Order History Procedure
	--Create a stored procedure named sp_GetCustomerOrderHistory that accepts a customer ID 
	--and optional start/end dates. Return the customer's order history with order totals 
	--calculated.
	GO
	CREATE PROCEDURE sp_GetCustomerOrderHistory
		 @Customer_ID INT ,
		 @start_date DATE = NULL,
		 @end_date DATE = NULL
			AS 
			BEGIN 
				SELECT SO.[order_id],SO.order_date,SO.order_status,
					   SUM(SOT.[quantity] * SOT.[list_price] * ( 1- SOT.[discount])) AS Total_Orders
				FROM [sales].[orders] SO
					INNER JOIN [sales].[order_items] SOT ON SO.order_id = SOT.order_id
				WHERE SO.customer_id = @Customer_ID
					AND (@start_date IS NULL OR SO.order_date >= @start_date)
					AND (@end_date IS NULL OR SO.order_date <= @end_date) 
				GROUP BY SO.order_id, SO.order_date, SO.order_status
				ORDER BY SO.order_date DESC;
			END;
	GO
	EXEC sp_GetCustomerOrderHistory @customer_id = 1;
	EXEC sp_GetCustomerOrderHistory @customer_id = 1, @start_date = '2017-01-01';

--14. Inventory Restock Procedure
	--Write a stored procedure named sp_RestockProduct with input parameters for store ID,
	--product ID, and restock quantity. Include output parameters for old quantity, 
	--new quantity, and success status.
	GO 
	CREATE PROCEDURE sp_RestockProduct
		@Store_ID INT, @Product_ID INT,	@Restock_quantity INT,
		@New_quantity INT OUTPUT , @Old_quantity INT OUTPUT, @Success_status NVARCHAR(50) OUTPUT
			AS
		BEGIN
			SELECT @Old_quantity = quantity
			FROM [production].[stocks]
			WHERE store_id = @Store_ID AND product_id = @Product_ID;
				 IF @Old_quantity IS NULL
					BEGIN
						SET @Success_status = 'Product not found in this store.'
						SET @New_quantity = NULL
						RETURN
					END

				UPDATE [production].[stocks]
				SET quantity = @Old_quantity + @Restock_quantity
				WHERE store_id = @Store_ID AND product_id = @Product_ID

				SELECT @New_quantity = quantity
				FROM [production].[stocks]
				WHERE store_id = @Store_ID AND product_id = @Product_ID

				SET @Success_status  = 'Restock successful.'
	    END
	GO

	DECLARE @Old INT, @New INT, @Massage NVARCHAR(50);
	EXEC dbo.sp_RestockProduct
		@Store_ID = 1,
		@Product_ID = 5,
		@Restock_quantity = 10,
		@Old_quantity = @Old OUTPUT,
		@New_quantity = @New OUTPUT,
		@Success_status  = @Massage OUTPUT;

	SELECT @Old AS Old_Quantity, @New AS New_Quantity, @Massage AS Status;

--15. Order Processing Procedure#
	--Create a stored procedure named sp_ProcessNewOrder that handles complete order 
	--creation with proper transaction control and error handling. Include parameters for
	--customer ID, product ID, quantity, and store ID.
GO  
	CREATE PROCEDURE sp_ProcessNewOrder
		@Customer_ID INT,
		@Product_ID INT,
		@Quantity INT,
		@Store_ID INT, 
		@Order_ID INT output,  
		@Massage nvarchar(100) output
			AS
		 BEGIN
			DECLARE @StockQty INT, @UnitPrice DECIMAL(10,2)
			SELECT @StockQty = quantity
			FROM [production].[stocks]
			WHERE store_id = @Store_ID AND product_id = @Product_ID

 			 IF @StockQty IS NULL
				BEGIN
					SET @Massage = 'Product not found in this store';
					SET @Order_ID = NULL;
					RETURN;
				END

			IF @StockQty < @Quantity
				BEGIN
					SET @Massage = 'The store do not have this quantity';
					SET @Order_ID = NULL;
					RETURN;
				END

			SELECT @UnitPrice = list_price
			FROM [production].[products]
			WHERE product_id = @Product_ID

			UPDATE [production].[stocks]
			SET quantity = quantity - @Quantity
			WHERE store_id = @Store_ID AND product_id = @Product_ID

			INSERT INTO [sales].[orders] (customer_id, order_status, order_date,required_date, store_id, staff_id)
			 VALUES (@Customer_ID, 1, GETDATE(),DATEADD(DAY, 2, GETDATE()),@Store_ID, 1)
			SET @Order_ID = SCOPE_IDENTITY()

			 INSERT INTO [sales].[order_items] (order_id, item_id, product_id, quantity, list_price, discount )
			  VALUES (@Order_ID, 1, @Product_ID, @Quantity, @UnitPrice, 0)
   
		  SET @Massage = 'Your order is confirmed';
		 END
GO
	DECLARE @OID INT, @Msg NVARCHAR(100);
	EXEC sp_ProcessNewOrder
		@Customer_ID = 2,
		@Product_ID = 2,
		@Quantity = 2,
		@Store_ID = 2,
		@Order_ID = @OID OUTPUT,
		@Massage = @Msg OUTPUT;

	SELECT @OID AS OrderID, @Msg AS Cusromer_Massage

--16. Dynamic Product Search Procedure
	--Write a stored procedure named sp_SearchProducts that builds dynamic SQL based on 
	--optional parameters: product name search term, category ID, minimum price, maximum price,
	--and sort column.
	GO
	CREATE PROCEDURE sp_SearchProducts 
		@Product_Name NVARCHAR(100) = NULL,
		@Category_ID INT = NULL,
		@Minimum_Price DECIMAL(18,2) = NULL,
		@Maximum_Price DECIMAL(18,2) = NULL,
		@Sort_Column NVARCHAR(100) = 'Product_Name'
	AS
	BEGIN
		DECLARE @sql NVARCHAR(MAX);
		SET @sql = 'SELECT [product_id], [product_name], [category_id], [list_price] FROM [production].[products] WHERE 1=1';

		IF @Product_Name IS NOT NULL
			SET @sql += ' AND [product_name] LIKE ''%' + @Product_Name + '%''';

		IF @Category_ID IS NOT NULL
			SET @sql += ' AND [category_id] = ' + CAST(@Category_ID AS NVARCHAR)

		IF @Minimum_Price IS NOT NULL
			SET @sql += ' AND [list_price] >= ' + CAST(@Minimum_Price AS NVARCHAR)

		IF @Maximum_Price IS NOT NULL
			SET @sql += ' AND [list_price] <= ' + CAST(@Maximum_Price AS NVARCHAR)

		SET @sql += ' ORDER BY ' + QUOTENAME(@Sort_Column)

		EXEC (@sql);
	END
GO
	EXEC sp_SearchProducts @Product_Name = 'Purple Label Custom Fit French Cuff Shirt - White';
	EXEC sp_SearchProducts @Minimum_Price = 100, @Maximum_Price = 500;

--17. Staff Bonus Calculation System#
	--Create a complete solution that calculates quarterly bonuses for all staff members.
	--Use variables to store date ranges and bonus rates. Apply different bonus percentages 
	--based on sales performance tiers.
	GO
	CREATE PROCEDURE sp_CalculateStaffBonuses
		@StartDate DATE,
		@EndDate   DATE
		AS
	 BEGIN
		SELECT 
			STAF.[staff_id] ,
			STAF.first_name+ ' ' + STAF.last_name AS EmployeeName,
			ISNULL(SUM(SOT.list_price), 0) AS TotalSales,
			CASE 
				WHEN SUM(SOT.list_price) >= 100000 THEN SUM(SOT.list_price) * 0.10
				WHEN SUM(SOT.list_price) >= 50000  THEN SUM(SOT.list_price) * 0.05
				ELSE SUM(SOT.list_price) * 0.02
			 END AS BonusAmount
			FROM [sales].[staffs] STAF
				JOIN [sales].[orders] ORD ON ORD.staff_id = STAF.staff_id
				JOIN [sales].[order_items] SOT ON SOT.order_id = ORD.order_id
					AND ORD.[order_date] BETWEEN @StartDate AND @EndDate
			GROUP BY STAF.staff_id, STAF.first_name, STAF.last_name;
	 END
GO

	EXEC sp_CalculateStaffBonuses 
		@StartDate = '2022-01-01', 
		@EndDate   = '2022-03-31';

--18. Smart Inventory Management#
	--Write a complex query with nested IF statements that manages inventory restocking. 
	--Check current stock levels and apply different reorder quantities based on product 
	--categories and current stock levels.
	SELECT p.product_id, p.product_name, c.category_name, s.quantity,                      
		CASE 
			WHEN c.category_id BETWEEN 1 AND 13 THEN --MEN
				CASE 
					WHEN s.quantity < 15 THEN 100      
					WHEN s.quantity BETWEEN 15 AND 40 THEN 50
					ELSE 0
				END

			WHEN c.category_id between 14 and 26 THEN --women
				CASE
					WHEN s.quantity < 10 THEN 60
					WHEN s.quantity BETWEEN 10 AND 25 THEN 30
					ELSE 0
				END

			WHEN c.category_id BETWEEN 27 AND 34 THEN
				CASE
					WHEN s.quantity < 8 THEN 40
					WHEN s.quantity BETWEEN 8 AND 20 THEN 20
					ELSE 0
				END
			ELSE
				CASE
					WHEN s.quantity < 5 THEN 25
					WHEN s.quantity BETWEEN 5 AND 15 THEN 10
					ELSE 0
				END
		END AS Restocking_Levels
	FROM production.products AS p
		JOIN production.categories AS c ON p.category_id = c.category_id
		JOIN production.stocks AS s ON p.product_id = s.product_id
	ORDER BY p.product_name

--19. Customer Loyalty Tier Assignment
	--Create a comprehensive solution that assigns loyalty tiers to customers 
	--based on their total spending. Handle customers with no orders appropriately and 
	--use proper NULL checking.	
	SELECT c.customer_id,c.first_name + ' ' + c.last_name AS Customer_Name,
		    ISNULL(SUM(oi.quantity * oi.list_price), 0) AS Total_Spending,
			CASE 
				WHEN ISNULL(SUM(oi.quantity * oi.list_price), 0) >= 5000 THEN 'Platinum'
				WHEN ISNULL(SUM(oi.quantity * oi.list_price), 0) >= 2000 THEN 'Gold'
				WHEN ISNULL(SUM(oi.quantity * oi.list_price), 0) >= 500  THEN 'Silver'
				ELSE 'Bronze'
			END AS Loyalty_Tier
	FROM sales.customers c
		LEFT JOIN sales.orders o ON c.customer_id = o.customer_id
		LEFT JOIN sales.order_items oi ON o.order_id = oi.order_id
	GROUP BY c.customer_id, c.first_name, c.last_name
	ORDER BY Total_Spending DESC

--20. Product Lifecycle Management
	--Write a stored procedure that handles product discontinuation including checking
	--for pending orders, optional product replacement in existing orders, clearing 
	--inventory, and providing detailed status messages.
	GO
	CREATE PROCEDURE sp_DiscontinueProduct
		@ProductID INT,
		@ReplacementProductID INT = NULL  
	 AS
	BEGIN
		DECLARE @PendingOrders INT;
		DECLARE @Message NVARCHAR(200);

		SELECT @PendingOrders = COUNT(*) --PENDING
		FROM sales.order_items oi
			JOIN sales.orders o ON oi.order_id = o.order_id
		WHERE oi.product_id = @ProductID
			  AND o.order_status = 1;  

		IF @PendingOrders > 0 
		BEGIN
			IF @ReplacementProductID IS NOT NULL  --REPLACE
				BEGIN
					UPDATE oi
						SET product_id = @ReplacementProductID
						FROM sales.order_items oi
						  JOIN sales.orders o ON oi.order_id = o.order_id
						WHERE oi.product_id = @ProductID
						  AND o.order_status = 1

					SET @Message = 'Product ' + CAST(@ProductID AS NVARCHAR) +
								   ' replaced with ' + CAST(@ReplacementProductID AS NVARCHAR) +
								   ' in pending orders.'
				END
			ELSE
				BEGIN
					SET @Message = 'Cannot discontinue product ' + CAST(@ProductID AS NVARCHAR) +
								   ' because it has pending orders.'
					PRINT @Message
					RETURN
				END
		END
		ELSE
			BEGIN
				SET @Message = 'No pending orders for product ' + CAST(@ProductID AS NVARCHAR) + '.';
			END

		UPDATE production.stocks
		SET quantity = 0
		WHERE product_id = @ProductID;

		

		
		SET @Message = @Message + ' Product ' + CAST(@ProductID AS NVARCHAR) +
					   ' discontinued and inventory cleared.';
		PRINT @Message;
	END
GO
EXEC sp_DiscontinueProduct @ProductID = 100;
EXEC sp_DiscontinueProduct @ProductID = 100, @ReplacementProductID = 101;

