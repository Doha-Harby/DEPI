use StoreDB
go
-- Customer activity log
CREATE TABLE sales.customer_log (
    log_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT,
    action VARCHAR(50),
    log_date DATETIME DEFAULT GETDATE()
);

-- Price history tracking
CREATE TABLE production.price_history (
    history_id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT,
    old_price DECIMAL(10,2),
    new_price DECIMAL(10,2),
    change_date DATETIME DEFAULT GETDATE(),
    changed_by VARCHAR(100)
);

-- Order audit trail
CREATE TABLE sales.order_audit (
    audit_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
    customer_id INT,
    store_id INT,
    staff_id INT,
    order_date DATE,
    audit_timestamp DATETIME DEFAULT GETDATE()
);

--1.Create a non-clustered index on the email column in the sales.customers table to 
--improve search performance when looking up customers by email.
    CREATE NONCLUSTERED INDEX IX_Customers_Email
        ON sales.customers (email)

--2.Create a composite index on the production.products table that includes category_id 
--and brand_id columns to optimize searches that filter by both category and brand.
    CREATE NONCLUSTERED INDEX IX_Products_Category_Brand
        ON production.products (category_id, brand_id)

--3.Create an index on sales.orders table for the order_date column and include customer_id,
--store_id, and order_status as included columns to improve reporting queries.
    CREATE NONCLUSTERED INDEX IX_Orders_OrderDate
        ON sales.orders (order_date)
        INCLUDE (customer_id, store_id, order_status)

--4.Create a trigger that automatically inserts a welcome record into a customer_log 
--table whenever a new customer is added to sales.customers. (First create the log table, 
--then the trigger)
    GO
    CREATE TRIGGER trg_CustomerWelcome
        ON sales.customers
        AFTER INSERT
          AS
        BEGIN
            INSERT INTO sales.customer_log (customer_id, action)
            SELECT customer_id, 'Welcome - New Customer'
            FROM inserted
        END

--5.Create a trigger on production.products that logs any changes to the list_price column 
--into a price_history table, storing the old price, new price, and change date.
    GO
    CREATE TRIGGER trg_ProductPriceChange
        ON production.products
        AFTER UPDATE
          AS
        BEGIN
            IF UPDATE(list_price)
            BEGIN
                INSERT INTO production.price_history (product_id, old_price, new_price, changed_by)
                SELECT i.product_id, d.list_price, i.list_price, SYSTEM_USER
                FROM inserted i
                    JOIN deleted d ON i.product_id = d.product_id;
            END
        END

--6.Create an INSTEAD OF DELETE trigger on production.categories that prevents deletion of
--categories that have associated products. Display an appropriate error message.
    GO
    CREATE TRIGGER trg_PreventCategoryDelete
        ON production.categories
        INSTEAD OF DELETE
        AS
        BEGIN
            IF EXISTS (
                SELECT 1
                FROM deleted d
                    JOIN production.products p ON p.category_id = d.category_id
            )
            BEGIN
                RAISERROR('Cannot delete category because it has associated products.', 16, 1);
                ROLLBACK TRANSACTION;
            END
            ELSE
            BEGIN
                DELETE FROM production.categories
                WHERE category_id IN (SELECT category_id FROM deleted);
            END
        END

--7.Create a trigger on sales.order_items that automatically reduces the quantity in 
--production.stocks when a new order item is inserted.
    GO
    CREATE TRIGGER trg_ReduceStock
        ON sales.order_items
        AFTER INSERT
          AS
        BEGIN
            UPDATE s
            SET s.quantity = s.quantity - i.quantity
            FROM production.stocks s
                JOIN inserted i ON s.product_id = i.product_id
            WHERE s.quantity >= i.quantity
        END

--8.Create a trigger that logs all new orders into an order_audit table, capturing order 
--details and the date/time when the record was created.
    GO
    CREATE TRIGGER trg_OrderAudit
        ON sales.orders
        AFTER INSERT
          AS
        BEGIN
            INSERT INTO sales.order_audit (order_id, customer_id, store_id, staff_id, order_date)
            SELECT order_id, customer_id, store_id, staff_id, order_date
            FROM inserted
        END





