namespace DataBaseADO;

public static class Query
{
	public static string CreateClientTable = @"
            CREATE TABLE Client (
                Id INT PRIMARY KEY IDENTITY,
                FirstName NVARCHAR(50) NOT NULL,
                LastName NVARCHAR(50) NOT NULL,
                ThirdName NVARCHAR(50) NOT NULL,
                PhoneNumber NVARCHAR(20),
                Email NVARCHAR(50)
            )";
	
	public static string CreateOrderTable = @"
            CREATE TABLE Orders (
        		Id SERIAL PRIMARY KEY,
                ProductCode INT,
                ProductName VARCHAR(50),
                Email VARCHAR(50)
            )";
	
	public static string InsertClient = @"
            INSERT INTO Client (FirstName, LastName, ThirdName, PhoneNumber, Email)
            VALUES (@FirstName, @LastName, @ThirdName, @PhoneNumber, @Email)";
	
	public static string InsertOrder = @"
            INSERT INTO Orders (ProductCode, ProductName, Email)
            VALUES (@ProductCode, @ProductName, @Email)";
	
	public static string GetAllMsSqlTablesQuery = @"
        SELECT TABLE_NAME 
        FROM INFORMATION_SCHEMA.TABLES 
        WHERE TABLE_TYPE = 'BASE TABLE'";
	
	public static string GetAllPostgresTablesQuery = @"
        SELECT table_name 
	    FROM information_schema.tables 
	    WHERE table_schema = 'public'";

	public static string GetAllClients = @"
        SELECT * 
	    FROM Client";
	
	public static string GetAllOrders = @"
        SELECT * 
	    FROM Orders";
	
	public static string UpdateClient = @"
                UPDATE Client 
                SET 
                    FirstName = @FirstName,
                    LastName = @LastName,
                    ThirdName = @ThirdName,
                    PhoneNumber = @PhoneNumber,
                    Email = @Email
                WHERE 
                    EmployeeId = @Id";
}
