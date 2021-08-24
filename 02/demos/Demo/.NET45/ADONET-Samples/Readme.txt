ADO.NET Samples
----------------------------------------------
ConnectionControl - Connecting to a database
  - Window Auth
  - SQL Server Auth
  - Using block
  - Show an exception
  - www.connectionstrings.com

CommandControl - Using the SqlCommand to submit queries, with and without parameters
  - ExecuteScalar
  - ExecuteScalar with Parameter
  - Insert
  - Insert with Parameter
  - Output Parameter
  - Transaction Processing

DataReaderControl - Using the SqlDataReader
  - DataReader
  - DataReader to Generic List<Product>
  - Using an Extension Method to Read Data
  - Multiple Result Sets
  
ExceptionControl - Using the DbException and SqlException classes
  - Normal Exception handling
  - Using the SqlException
  - Getting SqlCommand information

DataTableControl - Using the DataTable and DataSet
  - DataTable
  - DataTable to Generic List<Product> using LINQ
  - Multiple Result Sets

DataViewControl - Using the DataView class - sorting and filtering
  - Default View
  - Sorting data
  - Filtering data

DataRowColumnControl - Using the DataRow and DataColumn Classes
  - Loop through all rows/columns in DataTable
  - Build DataTable from scratch
  - Clone()
  - Copy()
  - Select() method
  - Select() method using CopyToDataTable()

BuilderControl - Using the CommandBuilder and ConnectionStringBuilder classes
  - Break apart connection string
  - Create connection string
  - Create data modification commands
  - Insert using data modification command

ADO.NET Wrapper Class - Making ADO.NET easier to work with
  - Show an easy to use wrapper class

NOTES
--------------------------------------
Data readers are read-only, forward-only cursors
Until you close a data reader, no other operations can be done on that connection

CommandBuilder for Update and Delete uses optimistic concurrency, so it only modifies or deletes if all the original values are still in the row prior to updating or deleting
