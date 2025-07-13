
	USE StoreDB

	-- 1. Count the total number of products in the database.
	SELECT COUNT(*) AS total_number_of_products
	 FROM [production].[products] 

	-- 2. Find the average, minimum, and maximum price of all products.
	SELECT 
		  AVG([list_price]) AS average_price,
		  MIN([list_price]) AS minimum_price,
		  MAX([list_price]) AS maximum_price
	FROM [production].[products]

	--3. Count how many products are in each category.
	SELECT [category_id] , COUNT(*) AS Grouped_Categories
	FROM [production].[products]
	GROUP BY [category_id] 
	ORDER BY [category_id] 

	--4. Find the total number of orders for each store.
	SELECT [store_id], COUNT(*) AS number_of_orders
	FROM [sales].[orders]
	GROUP BY [store_id]
	ORDER BY [store_id]

	--5. Show customer first names in UPPERCASE and last names in lowercase for the first 10 customers.
	SELECT UPPER([first_name])AS Uppercase_First_Name,LOWER([last_name]) AS Lower_Last_Name
	FROM [sales].[customers]
	WHERE [customer_id] <= 10;

	--6. Get the length of each product name. Show product name and its length for the first 10 products.
	SELECT [product_name], LEN([product_name]) AS lenth_of_product_name
	FROM [production].[products]
	WHERE [product_id] <= 10

	--7. Format customer phone numbers to show only the area code (first 3 digits) for customers 1-15.
	SELECT SUBSTRING([phone] ,1,3) AS area_code
	FROM [sales].[customers]
	WHERE [customer_id] <= 15;

	--8. Show the current date and extract the year and month from order dates for orders 1-10.
	SELECT GETDATE(), YEAR([order_date]), MONTH([order_date])
	FROM [sales].[orders]
	WHERE [order_id] <= 10;

	--9. Join products with their categories. Show product name and category name for first 10 products.
	SELECT PP.[product_name], PC.[category_name]
	FROM [production].[products] PP
	INNER JOIN [production].[categories] PC
	ON PP.category_id = PC.category_id

	--10. Join customers with their orders. Show customer name and order date for first 10 orders.
	SELECT 	SC.[first_name] +' ' +SC.[last_name] AS Full_Name,SO.[order_date]
	FROM [sales].[customers] SC
	INNER JOIN [sales].[orders] SO
	ON SC.customer_id = SO.customer_id
	WHERE SO.[order_id] <= 10

	--11. Show all products with their brand names, even if some products don't have brands. Include product name, brand name (show 'No Brand' if null).
	SELECT PP.[product_name], COALESCE(PB.[brand_name],'No Brand')
	FROM [production].[products] PP
	LEFT OUTER JOIN [production].[brands] PB
	ON PB.brand_id = PP.brand_id

	--12. Find products that cost more than the average product price. Show product name and price.
	SELECT [product_name] , [list_price] --,(SELECT AVG(list_price) FROM production.products) as average_price
	FROM [production].[products] 
	WHERE list_price > (
		SELECT AVG([list_price])
		FROM [production].[products]
	)

	--13. Find customers who have placed at least one order. Use a subquery with IN. Show customer_id and customer_name.
	SELECT [customer_id],[first_name]+' '+[last_name] AS FULL_NAME
	FROM [sales].[customers] 
	WHERE [customer_id] IN(
		SELECT [customer_id]
		FROM [sales].[orders] 
		)

	--14. For each customer, show their name and total number of orders using a subquery in the SELECT clause.
	SELECT SC.[first_name]+' '+SC.[last_name]  AS FULL_NAME, 
		( SELECT COUNT(*) 
		  FROM [sales].[orders] SO
		  WHERE SC.customer_id = SO.customer_id
	  )AS Number_Of_Order
	FROM [sales].[customers] SC

	--15. Create a simple view called easy_product_list that shows product name, category name, and price.
	--Then write a query to select all products from this view where price > 100.
	GO
	CREATE VIEW easy_product_list AS
		SELECT PP.[product_name], 
				( SELECT PC.category_name 
				  FROM [production].[categories] PC
				  WHERE PP.category_id = PC.category_id ) AS category_name ,
			   [list_price]  
		FROM [production].[products] PP
	GO

	SELECT * 
	FROM [dbo].[easy_product_list]
	WHERE  [list_price] > 100

	--16. Create a view called customer_info that shows customer ID, full name (first + last), email, 
	--and city and state combined. Then use this view to find all customers from California (CA).
	GO
	CREATE VIEW customer_info AS
	SELECT [customer_id],[first_name]+' '+[last_name] AS full_name ,[email],[state],[city]
	FROM [sales].[customers]
	GO

	SELECT *
	FROM [dbo].[customer_info]
	WHERE state = 'CA'

	--17. Find all products that cost between $50 and $200. Show product name and price, 
	--ordered by price from lowest to highest.
	SELECT [product_name],[list_price]
	FROM [production].[products]
	WHERE list_price BETWEEN 50 AND 200
	ORDER BY list_price

	--18. Count how many customers live in each state. Show state and customer count,
	--ordered by count from highest to lowest.
	SELECT DISTINCT [state] , ( SELECT COUNT([customer_id]) 
								FROM [sales].[customers] C
								WHERE C.state = S.state) AS customer_count
	FROM [sales].[customers] S
	ORDER BY customer_count DESC
	--OR
	SELECT state, COUNT(*) AS customer_count
	FROM sales.customers
	GROUP BY state
	ORDER BY customer_count DESC;

	--19. Find the most expensive product in each category. Show category name, product name, and price.
	SELECT PC.[category_name], PP.[product_name], PP.[list_price]
	FROM  [production].[products] PP
	INNER JOIN [production].[categories] PC
		ON PP.[category_id] = PC.[category_id]
		WHERE PP.[list_price] = (
			SELECT MAX(PPP.list_price)
			FROM production.products PPP
			WHERE PPP.category_id = PP.category_id
		)

	--20. Show all stores and their cities, including the total number of orders from each store.
	--Show store name, city, and order count.
	SELECT SS.store_name,SS.city,COUNT(SO.order_id) AS order_count
	FROM [sales].[stores] SS
	LEFT JOIN sales.orders SO
		ON SS.store_id = SO.store_id
	GROUP BY SS.store_name, SS.city
	ORDER BY order_count DESC;


		