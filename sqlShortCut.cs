//using System;
//namespace Assignment1
//{
//    public class sqlShortCut
//    {
//        Console.WriteLine("Connecting to database");

//            SqlConnection sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");

//        sqlOp.Open();

//            //DataSet setProducts = new DataSet();
//            //SqlDataAdapter adapProducts = new SqlDataAdapter("select * from Product", sqlOp);

//            //SqlCommandBuilder cmdBldr = new SqlCommandBuilder(adapProducts);

//            //adapProducts.Fill(setProducts, "Product");

//            //string updateString = @"
//            //update Product
//            //set Name = 'Did it work?'
//            //where Name = 'Test'";

//            var newQuant = 10;
//        var selectedID = 7;

//        var sqlText = @"UPDATE StoreInventory
//                SET StockLevel = @price
//                WHERE ProductID = @inventoryID";

//        SqlCommand dbCommand = new SqlCommand(sqlText, sqlOp);
//        dbCommand.Parameters.AddWithValue("price", newQuant);
//            dbCommand.Parameters.AddWithValue("inventoryID",selectedID );

//            dbCommand.Connection = sqlOp;

//            dbCommand.ExecuteNonQuery();

//                            //string updateString = @"
//                            //         update StoreInventory
//                            //         set StockLevel = '10'
//                            //         where ProductID = '3'";

//                            //// 1. Instantiate a new command with command text only
//                            //SqlCommand cmd = new SqlCommand(updateString);

//                            //// 2. Set the Connection property
//                            //cmd.Connection = sqlOp;

//                            //// 3. Call ExecuteNonQuery to send command
//                            //cmd.ExecuteNonQuery();
//            sqlOp.Close();
//    }
//}
