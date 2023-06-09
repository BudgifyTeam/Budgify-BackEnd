using BudgifyModels.Dto;

namespace BudgifyModels;

public class ResponseList <T>
{
    public string? message { get; set; }
    public List<T>? data { get; set; }
    public Boolean code { get; set; }
}

public class Response<T> {
    public string? message { get; set; }
    public T? data { get; set; }
    public Boolean code { get; set; }
}

public class ResponseError
{
    public string? message { get; set; }
    public int? code { get; set; }
}

public class ResponseIncome {
    public string? message { get; set; }
    public Boolean code { get; set; }
    public IncomeDto? income { get; set; }
    public double? newBudget { get; set; }
}

public class ResponseExpense
{
    public string? message { get; set; }
    public Boolean code { get; set; }
    public ExpenseDto? expense { get; set; }
    public double? newBudget { get; set; }
}

public class ResponseCategory
{
    public string? message { get; set; }
    public Boolean code { get; set; }
    public CategoryDto category { get; set; }

}
public class ResponseWallet
{
    public string? message { get; set; }
    public Boolean code { get; set; }
    public WalletDto wallet { get; set; }
    public double budget { get; set; }

}

public class ResponsePocket
{
    public string? message { get; set; }
    public Boolean code { get; set; }
    public PocketDto pocket { get; set; }

}

public class ResponseHistory
{
    public string? message { get; set; }
    public Boolean code { get; set; }
    public HistoryDto history { get; set; }

}

public class ResponseCategoryStat { 
    public string? message { get; set; }
    public Boolean code { get; set; }
    public StatsCategory[] stats { get; set; }
    public double total { get; set; }
}
