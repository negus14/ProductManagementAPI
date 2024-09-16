# Getting Started with C# .NET App

# Instructions to run this code
# Requirements

# Download:

# .NET sdk 8.0.401 for x 64: dotnet-sdk-8.0.401-win-x64
https://dotnet.microsoft.com/en-us/download/dotnet/8.0

SQL Server Express: SQL2022-SSEI-Expr
https://www.microsoft.com/en-gb/sql-server/sql-server-downloads

SQL Server Management Studio 2022
https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms

In the project directory, you can run:
### `dotnet build`

Do not use container.


[text](<../../Downloads/ProductManagementApplication/DB Insert Script.sql>)

## DB Migration
dotnet tool install --global dotnet-ef
dotnet restore
dotnet ef migration
dotnet ef database update

## Docker ( does not work atm)
docker build -t productapi-image .
docker run -d -p 7157:80 --name productapi-container productapi-image

