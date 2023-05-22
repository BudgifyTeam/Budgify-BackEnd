﻿using BudgifyModels;
using BudgifyModels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgifyDal
{
    public class UtilsDal
    {
        private readonly AppDbContext _appDbContext;

        public UtilsDal(AppDbContext db)
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
                return " Se creó correctamente el presupuesto";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<ResponseCategory> CreateCategory(int userid, string name)
        {
            ResponseCategory response = new ResponseCategory();
            try
            {
                var newCategory = new Category
                {
                    category_id = GetLastCategoryId() + 1,
                    name = name,
                    users_id = userid,
                    user = GetUser(userid),
                    status = "active",
                };
                _appDbContext.categories.Add(newCategory);
                await _appDbContext.SaveChangesAsync();
                response.code = true;
                response.message = "se creó correctamente la categoria";
                response.category = Utils.GetCategoryDto(newCategory);
                return response;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = false;
                return response;
            }
        }
        public async Task<ResponseWallet> CreateWallet(int userid, string name, string icon)
        {
            ResponseWallet response = new ResponseWallet();
            try
            {
                var newWallet = new Wallet
                {
                    wallet_id = GetLastWalletId() + 1,
                    name = name,
                    total = 0,
                    icon = icon,
                    status = "active",
                    user = GetUser(userid),
                    users_id = userid,
                };
                _appDbContext.wallets.Add(newWallet);
                await _appDbContext.SaveChangesAsync();
                response.message = " Se creó correctamente la billetera";
                response.code = true;
                response.wallet = Utils.GetWalletDto(newWallet);
            }
            catch (Exception ex)
            {
                response.message = "ocurrió un error al crear la nueva billetera, " + ex.Message;
                response.code = true;
            }
            return response;
        }
        public async Task<ResponsePocket> CreatePocket(int userid, string name, double goal, string icon)
        {
            ResponsePocket response = new ResponsePocket();
            try
            {
                var newPocket = new Pocket
                {
                    pocket_id = GetLastPocketId() + 1,
                    name = name,
                    total = 0,
                    goal = goal,
                    icon = icon,
                    user = GetUser(userid),
                    users_id = userid,
                    status = "active",
                };
                _appDbContext.pockets.Add(newPocket);
                await _appDbContext.SaveChangesAsync(); 
                response.message = " Se creó correctamente el bolsillo";
                response.code = true;
                response.pocket = Utils.GetPocketDto(newPocket);
            }
            catch (Exception ex)
            {
                response.message = "ocurrió un error al crear el nuevo bolsillo, " + ex.Message;
                response.code = true;
            }
            return response;
        }
        public Income[] AsignWalletToIncomes(Income[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                var income = list[i];
                income.wallet = GetWallet(income.wallet_id);
                list[i] = income;
            }
            return list;
        }
        public Expense[] AsignWalletToExpenses(Expense[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                var expense = list[i];
                expense.wallet = GetWallet(expense.wallet_id);
                list[i] = expense;
            }
            return list;
        }
        public Expense[] AsignPocketToExpenses(Expense[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                var expense = list[i];
                expense.pocket = GetPocket(expense.pocket_id);
                list[i] = expense;
            }
            return list;
        }
        public Expense[] AsignCategoryToExpenses(Expense[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                var expense = list[i];
                expense.category = GetCategory(expense.category_id);
                list[i] = expense;
            }
            return list;
        }
        public Wallet GetWalletByUserId(int id)
        {
            return _appDbContext.wallets.FirstOrDefault(u => u.users_id == id);
        }
        public Wallet GetWallet(int id)
        {
            return _appDbContext.wallets.FirstOrDefault(u => u.wallet_id == id);
        }
        public Category GetCategory(int id)
        {
            return _appDbContext.categories.FirstOrDefault(u => u.category_id == id);
        }
        public Pocket GetPocket(int id)
        {
            return _appDbContext.pockets.FirstOrDefault(u => u.pocket_id == id);
        }
        public int GetLastIncomeId() {
            try
            {
                return _appDbContext.incomes.ToList().OrderByDescending(u => u.income_id).FirstOrDefault().income_id;
            }
            catch
            {
                return 2000002;
            }
        }
        public int GetLastExpenseId() {
            try
            {
                return _appDbContext.expenses.ToList().OrderByDescending(u => u.expense_id).FirstOrDefault().expense_id;
            }
            catch
            {
                return 13000002;
            }
        }
        private int GetLastBudgetId()
        {
            try
            {
                return _appDbContext.budget.ToList().OrderByDescending(u => u.budget_id).FirstOrDefault().budget_id;
            }
            catch
            {
                return 1000001;
            }
        }
         private int GetLastCategoryId()
        {
            try
            {
                return _appDbContext.categories.ToList().OrderByDescending(u => u.category_id).FirstOrDefault().category_id;
            }
            catch
            {
                return 460000002;
            }
        }
        private int GetLastWalletId()
        {
            try
            {
                return _appDbContext.wallets.ToList().OrderByDescending(u => u.wallet_id).FirstOrDefault().wallet_id;
            }
            catch
            {
                return 35000002;
            }
        }
        private int GetLastPocketId()
        {
            try
            {
                return _appDbContext.pockets.ToList().OrderByDescending(u => u.pocket_id).FirstOrDefault().pocket_id;
            }
            catch
            {
                return 24000002;
            }
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
            return _appDbContext.categories.Where(c => c.users_id == id && c.status == "active").ToArray();
        }

        public Expense[] GetExpensesByUserId(int id)
        {
            return _appDbContext.expenses.Where(c => c.users_id == id && c.status == "active").ToArray();
        }

        public Income[] GetIncomesByUserId(int id)
        {
            return _appDbContext.incomes.Where(c => c.users_id == id && c.status == "active").ToArray();
        }

        public Pocket[] GetPocketsByUserId(int id)
        {
            return _appDbContext.pockets.Where(c => c.users_id == id && c.status == "active").ToArray();
        }

        public Wallet[] GetWalletsByUserId(int id)
        {
            return _appDbContext.wallets.Where(c => c.users_id == id && c.status == "active").ToArray();
        }

        public Expense[] GetExpensesByCategory(int id) {
            return _appDbContext.expenses.Where(c => c.category_id == id && c.status == "active").ToArray();
        }

        internal Income[] GetIncomesByWallet(int id)
        {
            return _appDbContext.incomes.Where(c => c.wallet_id == id && c.status == "active").ToArray();
        }

        internal Expense[] GetExpensesByWallet(int id)
        {
            return _appDbContext.expenses.Where(c => c.wallet_id == id && c.status == "active").ToArray();
        }

        internal Expense[] GetExpensesByPocket(int id)
        {
            return _appDbContext.expenses.Where(c => c.pocket_id == id && c.status == "active").ToArray();
        }
    }
}
