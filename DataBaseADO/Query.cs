namespace DataBaseADO;

public static class Query
{
	public static readonly string CreateClientTable = @"
            CREATE TABLE Client (
                Id INT PRIMARY KEY IDENTITY,
                FirstName NVARCHAR(50),
                LastName NVARCHAR(50),
                ThirdName NVARCHAR(50),
                PhoneNumber NVARCHAR(20),
                Email NVARCHAR(50) NOT NULL
            )";
	
	public static readonly string CreateOrderTable = @"
            CREATE TABLE Orders (
        		Id SERIAL PRIMARY KEY,
                ProductCode INT,
                ProductName VARCHAR(50),
                Email VARCHAR(50) NOT NULL
            )";
	
	public static readonly string InsertClient = @"
            INSERT INTO Client (FirstName, LastName, ThirdName, PhoneNumber, Email)
            VALUES (@FirstName, @LastName, @ThirdName, @PhoneNumber, @Email)";
	
	public static readonly string InsertOrder = @"
            INSERT INTO Orders (ProductCode, ProductName, Email)
            VALUES (@ProductCode, @ProductName, @Email)";
	
	public static readonly string GetAllMsSqlTablesQuery = @"
        SELECT TABLE_NAME 
        FROM INFORMATION_SCHEMA.TABLES 
        WHERE TABLE_TYPE = 'BASE TABLE'";
	
	public static readonly string GetAllPostgresTablesQuery = @"
        SELECT table_name 
	    FROM information_schema.tables 
	    WHERE table_schema = 'public'";

	public static readonly string GetAllClients = @"
        SELECT * 
	    FROM Client";
	
	public static readonly string GetAllOrders = @"
        SELECT * 
	    FROM Orders";
	
	public static readonly string UpdateClient = @"
        UPDATE Client 
        SET 
            FirstName = @FirstName,
            LastName = @LastName,
            ThirdName = @ThirdName,
            PhoneNumber = @PhoneNumber,
            Email = @Email
        WHERE 
            Id = @Id";

	public static readonly string GetOrdersByEmail = @"
		SELECT *
		FROM Orders
		WHERE Email = @Email
	";
	
	public static readonly string DeleteClient = @"
		DELETE FROM Client
		WHERE Id = @Id
	";
	
	public static readonly string DeleteAllClient = @"
		TRUNCATE TABLE Client
	";

	public static readonly string DeleteOrder = @"
		DELETE FROM Orders
		WHERE Id = @Id
	";
	
	public static readonly string DeleteAllOrder = @"
		DELETE FROM Orders
		WHERE Email = @Email
	";
}
