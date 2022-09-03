using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Lib.Pages.Books;

namespace Lib.Pages.Books{

  public class IssueBookModel : PageModel{
    public BooksInfo booksInfo = new BooksInfo();
    public string successMessage = "";
    public string errorMessage = "";
    public void OnGet(){

    }

    public void OnPost(){
      string id = Request.Form["id"];
      string assignedTo = Request.Form["assignedTo"];
      try{
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True";
        using (SqlConnection conn = new SqlConnection(connectionString)){
          conn.Open();
          string sql = "UPDATE Books SET assigenedTo = @assignedTo WHERE bookID = @id";
          using(SqlCommand command = new SqlCommand(sql, conn)){
            command.Parameters.AddWithValue("@assignedTo", assignedTo);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            successMessage = "Book issued successfully";
          }
        }
      }catch(Exception ex){
        Console.WriteLine("Error :: "+ex.Message);
      }
    }

  }

}