using BudgifyModels;
using Npgsql;

namespace BudgifyDal.Connection;

public class Database
{
    private string conectString = "Server=144.22.175.160;Port=5432;Database=BudgifyDbTest;User Id=postgres;Password=budgifyPASS";

    public Response<string> Connect()
    {
        Response<String> response = new Response<string>();
        try
        {
            NpgsqlConnection conn = new NpgsqlConnection(conectString);
            conn.Open();
        }
        catch (NpgsqlException e) {
            response.message = ("There was an error connecting to the database, error: "+e.ToString());
            
        }
        return response;
    }
}