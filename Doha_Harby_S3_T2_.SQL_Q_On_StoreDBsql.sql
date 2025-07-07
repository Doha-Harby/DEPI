	
	--( 1 )
	SELECT *
	FROM [production].[products]
	WHERE list_price > 1000

	--( 2 )
	SELECT * 
	FROM [sales].[customers]
	WHERE (state = 'CA' OR state = 'NY')

	--( 3 )
	SELECT *
	FROM [sales].[orders]
	WHERE YEAR(order_date) = 2023;

	--( 4 )
	SELECT *
	FROM [sales].[customers]
	WHERE email LIKE '%@gmail.com';

	--( 5 )
	SELECT *
	FROM [sales].[staffs]
	WHERE active = 1;

	--( 6 )
	SELECT TOP(5)*
	FROM [production].[products]
	ORDER BY list_price DESC;

	--( 7 )
	SELECT TOP(10)*
	FROM [sales].[orders]
	ORDER BY order_date DESC;

	--( 8 )
	SELECT TOP(3)*
	FROM [sales].[customers]
	ORDER BY last_name;

	--( 9 )
	SELECT *
	FROM [sales].[customers]
	WHERE [phone] IS NULL;    

	--( 10 )
	SELECT *
	FROM [sales].[staffs]
	WHERE manager_id IS NULL;

	--( 11 )
	SELECT [category_id], COUNT(*) AS ProductCount
	FROM [production].[products]
	GROUP BY [category_id]
	
	--( 12 )
	SELECT [state] , COUNT(*) AS ProductCount
	FROM [sales].[customers]
	GROUP BY [state]

	--( 13 )
	SELECT [brand_id], AVG([list_price]) AS AvgPrice
	FROM [production].[products]
	GROUP BY [brand_id]

	--( 14 )
	SELECT [staff_id], COUNT(*) AS StaffOrders
	FROM [sales].[orders]
	GROUP BY [staff_id]

	--( 15 )
	SELECT [customer_id], COUNT(*) AS MoreThanTwo
	FROM [sales].[orders]
	GROUP BY [customer_id]
	HAVING  COUNT(*) > 2

	--( 16 )
	SELECT *
	FROM [production].[products]
	WHERE [list_price] BETWEEN 500 AND 1500 --IF WE WNAT 500 AND 1500   499 AND 1501

	--( 17 )
	SELECT *
	FROM [sales].[customers]
	WHERE [city] LIKE 'S%' 
	
	--( 18 )
	SELECT *
	FROM [sales].[orders]
	WHERE order_status IN (2, 4);

	--( 19 )
	SELECT *
	FROM [production].[products]
	WHERE [category_id] IN (1, 2, 3)

	--( 20 )
	SELECT *
	FROM [sales].[staffs]
	WHERE [store_id] = 1 OR- [phone] IS NULL
