﻿using BudgifyModels;
using BudgifyModels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgifyDal
{
    public class FinancialDal
    {
        private readonly AppDbContext _appDbContext;

        public FinancialDal(AppDbContext db)
        {
            _appDbContext = db;
        }
        public async Task<string> CreateBudget(int userid)
        {
            try
            {
                var newBudget = new Budget
                {
                    budget_id = GetLastBudgetId() + 1,
                    value = 0,
                    users_id = userid,
                    user = GetUser(userid)
                };
                _appDbContext.budget.Add(newBudget);
                await _appDbContext.SaveChangesAsync();
                return " se creó correctamente el presupuesto";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<ResponseError> CreateCategory(int userid, string name)
        {
            ResponseError response = new ResponseError();
            try
            {
                var newCategory = new Category
                {
                    category_id = GetLastCategoryId() + 1,
                    name = name,
                    users_id = userid,
                    user = GetUser(userid)
                };
                _appDbContext.categories.Add(newCategory);
                await _appDbContext.SaveChangesAsync();
                response.code = 1;
                response.message = "se creó correctamente la categoria";
                return response;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 0;
                return response;
            }
        }
        public async Task<string> CreateWallet(int userid, string name)
        {
            try
            {
                var newWallet = new Wallet
                {
                    wallet_id = GetLastWalletId() + 1,
                    name = name,
                    total = 0,
                    icon = "https://firebasestorage.googleapis.com/v0/b/budgify-ed7a9.appspot.com/o/Wallets.png?alt=media&token=cca353ff-39e1-4d5e-a0ce-3f2cb93f977c",
                    user = GetUser(userid),
                    users_id = userid,
                };
                _appDbContext.wallets.Add(newWallet);
                await _appDbContext.SaveChangesAsync();
                return " se creó correctamente la billetera";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> CreatePocket(int userid, string name, double goal)
        {
            try
            {
                var newPocket = new Pocket
                {
                    pocket_id = GetLastPocketId() + 1,
                    name = name,
                    total = 0,
                    goal = goal,
                    icon = "https://firebasestorage.googleapis.com/v0/b/budgify-ed7a9.appspot.com/o/Wallets.png?alt=media&token=cca353ff-39e1-4d5e-a0ce-3f2cb93f977c",
                    user = GetUser(userid),
                    users_id = userid,
                };
                _appDbContext.pockets.Add(newPocket);
                await _appDbContext.SaveChangesAsync();
                return " se creó correctamente el bolsillo";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private int GetLastBudgetId()
        {
            return _appDbContext.budget.ToList().OrderByDescending(u => u.budget_id).FirstOrDefault().budget_id;
        }
        private int GetLastCategoryId()
        {
            return _appDbContext.categories.ToList().OrderByDescending(u => u.category_id).FirstOrDefault().category_id;
        }
        private int GetLastWalletId()
        {
            return _appDbContext.wallets.ToList().OrderByDescending(u => u.wallet_id).FirstOrDefault().wallet_id;
        }
        private int GetLastPocketId()
        {
            return _appDbContext.pockets.ToList().OrderByDescending(u => u.pocket_id).FirstOrDefault().pocket_id;
        }
        public user GetUser(int id)
        {
            return _appDbContext.users.FirstOrDefault(u => u.users_id == id);
        }
        public Budget GetBudgetByUserId(int id)
        {
            return _appDbContext.budget.FirstOrDefault(u => u.users_id == id);
        }

        public Category[] GetCategoriesByUserId(int id)
        {
            return _appDbContext.categories.Where(c => c.users_id == id).ToArray();
        }

        internal Expense[] GetExpensesByUserId(int id)
        {
            return _appDbContext.expenses.Where(c => c.users_id == id).ToArray();
        }

        internal Income[] GetIncomesByUserId(int id)
        {
            return _appDbContext.incomes.Where(c => c.users_id == id).ToArray();
        }

        internal Pocket[] GetPocketsByUserId(int id)
        {
            return _appDbContext.pockets.Where(c => c.users_id == id).ToArray();
        }

        internal Wallet[] GetWalletsByUserId(int id)
        {
            return _appDbContext.wallets.Where(c => c.users_id == id).ToArray();
        }

        public Task<Response<IncomeDto>> CreateIncome(Income newIncome)
        {
            throw new NotImplementedException();
            //getwalletbyuserID
            //getLastIncomeId
        }

        public Task<Response<IncomeDto>> DeleteIncome(int userid, int incomeid)
        {
            throw new NotImplementedException();
        }
    }
}
