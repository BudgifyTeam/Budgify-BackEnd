﻿using BudgifyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgifyDal
{
    public class BudgetDal
    {
        private readonly AppDbContext _appDbContext;
        private readonly UtilsDal _utilsDal;

        public BudgetDal(AppDbContext db, UtilsDal fn)
        {
            _appDbContext = db;
            _utilsDal = fn;
        }

        /// <summary>
        /// Updates the budget for a user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom to update the budget.</param>
        /// <returns>A Response object containing the updated budget information.</returns>
        /// <remarks>
        /// This method calculates the budget value for a user based on their incomes and expenses.
        /// It retrieves the existing budget for the user and updates its value accordingly.
        /// The updated budget value is calculated by subtracting the total expenses from the total incomes.
        /// If there are no expenses or incomes, the budget value is set to 0.
        /// </remarks>
        /// <exception cref="Exception">Thrown if there is an error updating the budget.</exception>
        public async Task<Response<Budget>> updateBudget(int userId)
        {
            Response<Budget> response = new Response<Budget>();
            try
            {
                var budget = _appDbContext.budget.FirstOrDefault(b => b.users_id == userId);
                var incomes = _utilsDal.GetIncomesByUserId(userId);
                var expenses = _utilsDal.GetExpensesByUserId(userId);
                if (expenses.Any() && incomes.Any())
                    budget.value = incomes.Sum(i => i.value) - expenses.Sum(i => i.value);
                if (!expenses.Any())
                {
                    if (!incomes.Any())
                    {
                        budget.value = 0;
                        await _appDbContext.SaveChangesAsync();
                        response.data = budget;
                        response.code = true;
                        response.message = "se actualizó el presupesto correctamente";
                        return response;
                    }
                    budget.value = incomes.Sum(i => i.value);
                }
                await _appDbContext.SaveChangesAsync();
                response.data = budget;
                response.code = true;
                response.message = "se actualizó el presupesto correctamente";
            }
            catch (Exception ex)
            {
                response.code = false;
                response.message = ex.Message;
            }
            return response;
        }
    }
}
