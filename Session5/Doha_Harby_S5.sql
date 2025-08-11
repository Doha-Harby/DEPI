USE StoreDB
	--1. Write a query that classifies all products into price categories:
		--Products under $300: "Economy"
		--Products $300-$999: "Standard"
		--Products $1000-$2499: "Premium"
		--Products $2500 and above: "Luxury"
	SELECT [list_price],
		CASE 
			WHEN [list_price] < 300 THEN 'Economy'
			WHEN [list_price] BETWEEN 300 AND 999 THEN 'Standard'
			WHEN [list_price] BETWEEN 1000 AND 2499 THEN 'Premium'
			WHEN [list_price] > 2500 THEN 'Luxury'
			END AS 'price categories'
	FROM [production].[products]
	ORDER BY [list_price]

	--2.Create a query that shows order processing information with user-friendly status descriptions:
		--Status 1: "Order Received"
		--Status 2: "In Preparation"
		--Status 3: "Order Cancelled"
		--Status 4: "Order Delivered"
	--.Also add a priority level:
		--Orders with status 1 older than 5 days: "URGENT"
		--Orders with status 2 older than 3 days: "HIGH"
		--All other orders: "NORMAL"
	SELECT [order_status],
		CASE [order_status]
			WHEN 1 THEN 'Order Received'
			WHEN 2 THEN 'In Preparation'
			WHEN 3 THEN 'Order Cancelled'
			WHEN 4 THEN 'Order Delivered'
		END AS user_friendly_status,
		CASE 
			WHEN [order_status] = 1 AND DATEDIFF(DAY, [order_date], GETDATE()) > 5 THEN 'URGENT'
			WHEN [order_status] = 2 AND DATEDIFF(DAY, [order_date], GETDATE()) > 3 THEN 'HIGH'
			ELSE 'NORMAL'
		END AS priority_level
	FROM [sales].[orders]
	ORDER BY [order_status]

	--3.Write a query that categorizes staff based on the number of orders they've handled:
		--0 orders: "New Staff"
		--1-10 orders: "Junior Staff"
		--11-25 orders: "Senior Staff"
		--26+ orders: "Expert Staff"
	SELECT SS.[staff_id],COUNT(SO.[staff_id]) AS Count_Orders,SS.[first_name]+' '+SS.[last_name] AS Full_Name,		
		CASE 
			WHEN COUNT(SO.[staff_id]) = 0 THEN 'New Staff'
			WHEN COUNT(SO.[staff_id]) BETWEEN 1 AND 10 THEN 'Junior Staff'
			WHEN COUNT(SO.[staff_id]) BETWEEN 11 AND 25 THEN 'Senior Staff'
			WHEN COUNT(SO.[staff_id]) > 25 THEN 'Expert Staff'
		END AS staff_categorizes
	FROM [sales].[staffs] SS
		LEFT JOIN [sales].[orders] SO 
		ON SS.staff_id = SO.staff_id
	GROUP BY SS.[staff_id],[first_name],[last_name]

	--4.Create a query that handles missing customer contact information:
		--Use ISNULL to replace missing phone numbers with "Phone Not Available"
		--Use COALESCE to create a preferred_contact field (phone first, then email, then "No Contact Method")
		--Show complete customer information
	SELECT *,
		ISNULL( [phone], 'Phone Not Available') AS Phone_Availability,
		COALESCE( [phone], [email],'No Contact Method') AS preferred_contact
	FROM [sales].[customers]
	ORDER BY [customer_id]

	--5.Write a query that safely calculates price per unit in stock:
		--Use NULLIF to prevent division by zero when quantity is 0
		--Use ISNULL to show 0 when no stock exists
		--Include stock status using CASE WHEN
		--Only show products from store_id = 1
	SELECT PP.product_id, PP.product_name,PP.list_price, PS.quantity,
		ISNULL(PP.[list_price] / NULLIF([quantity] ,0 ),0) AS price_per_unit,
		CASE 
			WHEN PS.[quantity] = 0 THEN 'Stock is empty'
			WHEN PS.[quantity] IS NULL THEN 'NO information avaliable'
			ELSE 'Stock still has products'
		END AS stock_status
	FROM [production].[products] PP
		INNER JOIN [production].[stocks] PS
		ON PP.product_id = PS.product_id
	WHERE PS.[store_id] = 1

	--6.Create a query that formats complete addresses safely:
		--Use COALESCE for each address component
		--Create a formatted_address field that combines all components
		--Handle missing ZIP codes gracefully
	SELECT 
		COALESCE([street],'') AS Street,
		COALESCE([city],'') AS City,
		COALESCE([state],'') AS State,

		COALESCE([zip_code],'NO ZIP code exist') AS ZIP,

		COALESCE([street],'') +''+COALESCE([city],'')+''+COALESCE([state],'')+''+
		COALESCE([zip_code],'NO ZIP code exist') AS Full_Addrees
	FROM [sales].[customers]

	--7.Use a CTE to find customers who have spent more than $1,500 total:
		--Create a CTE that calculates total spending per customer
		--Join with customer information
		--Show customer details and spending
		--Order by total_spent descending
	;WITH Spending AS(  
		SELECT SO.[customer_id], SUM(SOI.list_price * SOI.quantity * (1 - SOI.discount)) AS Spended
		FROM [sales].[order_items] SOI 
			INNER JOIN [sales].[orders] SO
			ON SO.order_id = SOI.order_id
		GROUP BY [customer_id]
	)
	SELECT SC.*, S.spended
	FROM [sales].[customers]  SC
		INNER JOIN Spending S
		ON SC.customer_id = S.customer_id
	WHERE S.Spended > 1500
	ORDER BY S.Spended DESC;

	--8.Create a multi-CTE query for category analysis:
		--CTE 1: Calculate total revenue per category
		--CTE 2: Calculate average order value per category
		--Main query: Combine both CTEs
		--Use CASE to rate performance: >$50000 = "Excellent", >$20000 = "Good", else = "Needs Improvement"
	;WITH TotalRevenuePerCategory AS (
		SELECT c.category_id,c.category_name, SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue
		FROM production.categories c
			JOIN production.products p 
			ON c.category_id = p.category_id
			JOIN sales.order_items oi 
			ON p.product_id = oi.product_id
		GROUP BY c.category_id, c.category_name
	),

	AvgOrderValuePerCategory AS (
		SELECT c.category_id,AVG(oi.quantity * oi.list_price * (1 - oi.discount)) AS avg_order_value
	    FROM production.categories c
			JOIN production.products p 
			ON c.category_id = p.category_id
			JOIN sales.order_items oi 
			ON p.product_id = oi.product_id
		GROUP BY c.category_id
	)

	SELECT  t.category_name,t.total_revenue, a.avg_order_value,
		CASE 
			WHEN t.total_revenue > 50000 THEN 'Excellent'
			WHEN t.total_revenue > 20000 THEN 'Good'
			ELSE 'Needs Improvement'
		END AS performance_rating
	FROM TotalRevenuePerCategory t
		JOIN AvgOrderValuePerCategory a 
		ON t.category_id = a.category_id
	ORDER BY t.total_revenue DESC;

	--9.Use CTEs to analyze monthly sales trends:
		--CTE 1: Calculate monthly sales totals
		--CTE 2: Add previous month comparison
		--Show growth percentage
	;WITH MonthlySales AS (
		SELECT YEAR(SO.order_date) AS order_year, MONTH(SO.order_date) AS order_month,
			SUM(SOI.quantity * SOI.list_price * (1 - SOI.discount)) AS monthly_total
		FROM sales.orders SO
			JOIN sales.order_items SOI
			ON SO.order_id = SOI.order_id
		GROUP BY YEAR(SO.order_date), MONTH(SO.order_date)
	),
	MonthlyComparison AS (
		SELECT order_year,order_month,monthly_total,
			LAG(monthly_total) OVER (
				ORDER BY order_year, order_month) AS previous_month_total
			FROM MonthlySales		
		)

	SELECT 
		CONVERT(VARCHAR(4), order_year) + '-' + RIGHT('0' + CONVERT(VARCHAR(2), order_month), 2) AS sale_month,
		monthly_total,previous_month_total,
	    ROUND(((monthly_total - previous_month_total) * 100.0) / previous_month_total,2) AS growth_percentage		
	FROM MonthlyComparison
	ORDER BY order_year, order_month;

	--10.Create a query that ranks products within each category:
		--Use ROW_NUMBER() to rank by price (highest first)
		--Use RANK() to handle ties
		--Use DENSE_RANK() for continuous ranking
		--Only show top 3 products per category
	;WITH TRRY AS (
	SELECT [product_id],[category_id],[product_name],[list_price],
	ROW_NUMBER() OVER (
		PARTITION BY category_id 
		ORDER BY[list_price] DESC ) AS Price_Rank_rowNumber,
	RANK() OVER (
		PARTITION BY category_id 
		ORDER BY [list_price] DESC ) AS Price_Rank_rank,
	DENSE_RANK() OVER (
		PARTITION BY category_id 
		ORDER BY [list_price] DESC ) AS Price_Rank_dense
	FROM [production].[products]
	)
	SELECT *
	FROM TRRY
	WHERE Price_Rank_rowNumber <= 3
	ORDER BY category_id, Price_Rank_rowNumber;

	--11.Rank customers by their total spending:
		--Calculate total spending per customer
		--Use RANK() for customer ranking
		--Use NTILE(5) to divide into 5 spending groups
		--Use CASE for tiers: 1="VIP", 2="Gold", 3="Silver", 4="Bronze", 5="Standard"
	;WITH CustomerTotalSpending AS(
		SELECT SO.[customer_id],
		SUM(SOI.quantity * SOI.list_price * (1 - SOI.discount)) AS TotalSpend
		FROM [sales].[orders] SO
			INNER JOIN [sales].[order_items] SOI
			ON SO.[order_id] = SOI.[order_id]
		GROUP BY SO.[customer_id]
	),
    CU_RANK_GROUP AS(
		SELECT CTS.customer_id, SC.first_name + ' ' + SC.last_name AS full_name,CTS.TotalSpend,
				  RANK() OVER (ORDER BY TotalSpend) AS RANK_SPENDING,
				  NTILE(5) OVER (ORDER BY TotalSpend) AS GROUP_SPENDING
		FROM CustomerTotalSpending CTS
			INNER JOIN [sales].[customers] SC
			ON CTS.customer_id = SC.customer_id
	)
	SELECT *,
		CASE GROUP_SPENDING
			WHEN 1 THEN 'VIP'
			WHEN 2 THEN 'Gold'
			WHEN 3 THEN 'Silver'
			WHEN 4 THEN 'Bronze'
			WHEN 5 THEN 'Standard'
		END AS CUSTOMER_LEVELS
	FROM CU_RANK_GROUP CRG
		
	--12.Create a comprehensive store performance ranking:
		--Rank stores by total revenue
		--Rank stores by number of orders
		--Use PERCENT_RANK() to show percentile performance
	;WITH Store_revenue_numOrders AS (
		SELECT SS.store_id,SS.store_name,SS.city,
			COUNT(DISTINCT SO.order_id) AS total_orders,
			SUM(SOI.quantity * SOI.list_price * (1 - SOI.discount)) AS total_revenue
		FROM sales.stores SS
			JOIN sales.orders SO ON SS.store_id = SO.store_id
			JOIN sales.order_items SOI ON SO.order_id = SOI.order_id
		GROUP BY SS.store_id, SS.store_name, SS.city
	),
	Store_Ranks AS (
		SELECT *, 
        RANK() OVER (ORDER BY total_revenue DESC) AS revenue_rank,
        PERCENT_RANK() OVER (ORDER BY total_revenue DESC) AS revenue_percentile,
        RANK() OVER (ORDER BY total_orders DESC) AS orders_rank,
        PERCENT_RANK() OVER (ORDER BY total_orders DESC) AS orders_percentile
		FROM Store_revenue_numOrders SRN
	)
	SELECT *
	FROM Store_Ranks

	--13.Create a PIVOT table showing product counts by category and brand:
		--Rows: Categories
		--Columns: Top 4 brands (Electra, Haro, Trek, Surly)
		--Values: Count of products
	SELECT category_name,
		   ISNULL([Electra], 0) AS Electra,
		   ISNULL([Haro], 0) AS Haro,
		   ISNULL([Trek], 0) AS Trek,
		   ISNULL([Surly], 0) AS Surly
	FROM (
		SELECT 
			PC.category_name,
			PB.brand_name,
			PP.product_id
		FROM production.products PP
			JOIN production.categories PC ON PP.category_id = PC.category_id
			JOIN production.brands PB ON PP.brand_id = PB.brand_id
		WHERE PB.brand_name IN ('Electra', 'Haro', 'Trek', 'Surly')
	) AS source_table
	PIVOT (
		COUNT(product_id)
		FOR brand_name IN ([Electra], [Haro], [Trek], [Surly])
	) AS pivot_table
	ORDER BY category_name;

	--14.Create a PIVOT showing monthly sales revenue by store:
		--Rows: Store names
		--Columns: Months (Jan through Dec)
		--Values: Total revenue
		--Add a total column
	SELECT store_name,
		ISNULL([1], 0) AS Jan,
		ISNULL([2], 0) AS Feb,
		ISNULL([3], 0) AS Mar,
		ISNULL([4], 0) AS Apr,
		ISNULL([5], 0) AS May,
		ISNULL([6], 0) AS Jun,
		ISNULL([7], 0) AS Jul,
		ISNULL([8], 0) AS Aug,
		ISNULL([9], 0) AS Sep,
		ISNULL([10], 0) AS Oct,
		ISNULL([11], 0) AS Nov,
		ISNULL([12], 0) AS Dec,
		ISNULL([1], 0) + ISNULL([2], 0) + ISNULL([3], 0) +
		ISNULL([4], 0) + ISNULL([5], 0) + ISNULL([6], 0) +
		ISNULL([7], 0) + ISNULL([8], 0) + ISNULL([9], 0) +
		ISNULL([10], 0) + ISNULL([11], 0) + ISNULL([12], 0) AS Total
	FROM (
		SELECT 
			S.store_name,
			MONTH(O.order_date) AS order_month,
			OI.quantity * OI.list_price * (1 - OI.discount) AS revenue
		FROM sales.orders O
		JOIN sales.order_items OI ON O.order_id = OI.order_id
		JOIN sales.stores S ON O.store_id = S.store_id
	) AS source_table
	PIVOT (
		SUM(revenue)
		FOR order_month IN (
			[1], [2], [3], [4], [5], [6], 
			[7], [8], [9], [10], [11], [12]
		)
	) AS pivot_table
	ORDER BY store_name;

	--15.PIVOT order statuses across stores:
		--Rows: Store names
		--Columns: Order statuses (Pending, Processing, Completed, Rejected)
		--Values: Count of orders
		SELECT store_name,
			ISNULL([Pending], 0) AS Pending,
			ISNULL([Processing], 0) AS Processing,
			ISNULL([Completed], 0) AS Completed,
			ISNULL([Rejected], 0) AS Rejected
		FROM (
			SELECT 
				s.store_name,
				CASE o.order_status
					WHEN 1 THEN 'Pending'
					WHEN 2 THEN 'Processing'
					WHEN 3 THEN 'Rejected'
					WHEN 4 THEN 'Completed'
				END AS Order_status
			FROM sales.orders o
				JOIN sales.stores s ON 
				o.store_id = s.store_id
		) AS sourceTable
		PIVOT (
			COUNT(Order_status)
			FOR Order_status IN ([Pending], [Processing], [Completed], [Rejected])
		) AS pivot_table
		ORDER BY store_name

	--16.Create a PIVOT comparing sales across years:
		--Rows: Brand names
		--Columns: Years (2016, 2017, 2018)
		--Values: Total revenue
		--Include percentage growth calculations
	SELECT brand_name,
		ISNULL([2016], 0) AS Sales_2016,
		ISNULL([2017], 0) AS Sales_2017,
		ISNULL([2018], 0) AS Sales_2018,
		CASE
			WHEN ISNULL([2016],0) = 0 THEN NULL ELSE
			ROUND(([2017] - [2016]) * 100.0 / [2016], 2) 
		END AS Growth_2017,
		CASE
			WHEN ISNULL([2017],0) = 0 THEN NULL ELSE
			ROUND(([2018] - [2017]) * 100.0 / [2017], 2) 
		END AS Growth_2018
	FROM (
		SELECT 
			b.brand_name,
			YEAR(o.order_date) AS order_year,
			oi.quantity * oi.list_price * (1 - oi.discount) AS revenue
		FROM sales.order_items oi
			JOIN sales.orders o ON oi.order_id = o.order_id
			JOIN production.products p ON oi.product_id = p.product_id
			JOIN production.brands b ON p.brand_id = b.brand_id
		WHERE YEAR(o.order_date) IN (2016, 2017, 2018)
	) AS sourceTable
	PIVOT (
		SUM(revenue)
		FOR order_year IN ([2016], [2017], [2018])
	) AS pivot_table
	ORDER BY brand_name

	--17.Use UNION to combine different product availability statuses:
		--Query 1: In-stock products (quantity > 0)
		--Query 2: Out-of-stock products (quantity = 0 or NULL)
		--Query 3: Discontinued products (not in stocks table)
	-- In-stock products
	SELECT 	p.product_id,p.product_name,'In Stock' AS availability_status
	FROM production.products p
		JOIN sales.order_items oi 
		ON p.product_id = oi.product_id
	GROUP BY p.product_id, p.product_name
	HAVING SUM(oi.quantity) > 0
	UNION  -- 0/NULL
		SELECT p.product_id,p.product_name,'Out of Stock' AS availability_status
		FROM production.products p
			JOIN sales.order_items oi ON p.product_id = oi.product_id
		GROUP BY p.product_id, p.product_name
		HAVING SUM(ISNULL(oi.quantity, 0)) = 0
	UNION --NOT IN STOCK
		SELECT p.product_id,p.product_name,'Discontinued' AS availability_status
		FROM production.products p
		WHERE NOT EXISTS (
			SELECT 1
			FROM sales.order_items oi
			WHERE oi.product_id = p.product_id
		)


	--18.Use INTERSECT to find loyal customers:
		--Find customers who bought in both 2017 AND 2018
		--Show their purchase patterns
	SELECT o.customer_id,c.first_name + ' ' + c.last_name AS full_name,
		YEAR(o.order_date) AS order_year,
		COUNT(DISTINCT o.order_id) AS total_orders,
		SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent
	FROM sales.orders o
		JOIN sales.order_items oi ON o.order_id = oi.order_id
		JOIN sales.customers c ON o.customer_id = c.customer_id
	WHERE o.customer_id IN (
		SELECT customer_id
		FROM sales.orders
		WHERE YEAR(order_date) = 2022
			INTERSECT
		SELECT customer_id
		FROM sales.orders
		WHERE YEAR(order_date) = 2023
	)
	GROUP BY o.customer_id, c.first_name, c.last_name, YEAR(o.order_date)
	ORDER BY o.customer_id, order_year;

	--19.Use multiple set operators to analyze product distribution:
		--INTERSECT: Products available in all 3 stores
		--EXCEPT: Products available in store 1 but not in store 2
		--UNION: Combine above results with different labels
	SELECT product_id,'Available in All Stores' AS distribution_status
		FROM production.stocks
		WHERE store_id = 1
			INTERSECT
		SELECT product_id,'Available in All Stores' AS distribution_status
		FROM production.stocks
		WHERE store_id = 2
			INTERSECT
		SELECT 	product_id,'Available in All Stores' AS distribution_status
		FROM production.stocks
		WHERE store_id = 3
	UNION  
	--1 BUT NOT 2
		SELECT product_id,'Only in Store 1' AS distribution_status
		FROM (
			SELECT product_id
			FROM production.stocks
			WHERE store_id = 1
				EXCEPT
			SELECT product_id
			FROM production.stocks
			WHERE store_id = 2
		) AS onlyONE

	--20.Complex set operations for customer retention:
		--Find customers who bought in 2016 but not in 2017 (lost customers)
		--Find customers who bought in 2017 but not in 2016 (new customers)
		--Find customers who bought in both years (retained customers)
		--Use UNION ALL to combine all three groups
	SELECT customer_id,'Lost Customer' AS status
	FROM sales.orders
	WHERE YEAR(order_date) = 2022
		EXCEPT
	SELECT customer_id,'Lost Customer' AS status
	FROM sales.orders
	WHERE YEAR(order_date) = 2023
	UNION 
	SELECT customer_id,'New Customer' AS status
	FROM sales.orders
	WHERE YEAR(order_date) = 2022
		EXCEPT
	SELECT customer_id,'New Customer' AS status
	FROM sales.orders
	WHERE YEAR(order_date) = 2023
		UNION
	SELECT customer_id,'Retained Customer' AS status
	FROM sales.orders
	WHERE YEAR(order_date) = 2022
		INTERSECT
	SELECT customer_id,'Retained Customer' AS status
	FROM sales.orders
	WHERE YEAR(order_date) = 2023
