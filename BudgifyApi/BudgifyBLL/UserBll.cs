﻿using BudgifyModels;
using BudgifyDal;
using BudgifyModels.Dto;
using Microsoft.VisualBasic;

namespace BudgifyBll
{
    public class UserBll
    {

        private readonly UserDal _userDal;
        private readonly FinancialDal _financialDal;
        public UserBll(AppDbContext db)
        {
            _financialDal = new FinancialDal(db);
            _userDal = new UserDal(db, _financialDal);
        }
        public async Task<Response<user>> Register(UserRegister user)
        {
            Response<user> response = new Response<user>();
            try
            {
                var userToSave = new user
                {
                    email = user.Email,
                    username = user.Username,
                    token = user.Token,
                    status = true,
                    publicaccount = false,
                    icon = "https://firebasestorage.googleapis.com/v0/b/budgify-ed7a9.appspot.com/o/userimage.jpg?alt=media&token=df5dc86a-c48e-4786-9501-565b2ad15134"
                };

                if (_userDal.UserExist(userToSave.username))
                {
                    response.message = "username already exists";
                    return response;
                }

                if (_userDal.EmailExist(userToSave.email))
                {
                    response.message = "Email already exists";
                    return response;
                }

                response = await _userDal.RegisterUser(userToSave);

                if (!response.code)
                {
                    response.message = "Error al registrar al usuario";
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
        public async Task<Response<SessionDto>> Login(UserLogin user)
        {
            try
            {
                var response = await _userDal.Login(user);
                if (response.code)
                {
                    var sessionDto = await GetSessionDto(response.data);
                    return new Response<SessionDto>
                    {
                        data = sessionDto,
                        message = response.message,
                        code = response.code
                    };
                }
                else
                {
                    response.message += " Error al validar al usuario";
                    return new Response<SessionDto>
                    {
                        message = response.message,
                        code = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<SessionDto>
                {
                    message = ex.Message,
                    code = false
                };
            }
        }
        public async Task<SessionDto> GetSessionDto(Session session)
        {
            return new SessionDto
            {
                UserId = session.UserId,
                User_icon = session.icon,
                Budget = session.Budget != null ? GetBudgetDto(session.Budget) : null,
                Categories = session.Categories?.Select(c => GetCategoryDto(c)).ToArray(),
                Expenses = session.Expenses?.Select(e => GetExpenseDto(e)).ToArray(),
                Incomes = session.Incomes?.Select(i => GetIncomeDto(i)).ToArray(),
                Pockets = session.Pockets?.Select(p => GetPocketDto(p)).ToArray(),
                Wallets = session.Wallets?.Select(w => GetWalletDto(w)).ToArray()
            };
        }

        public BudgetDto GetBudgetDto(Budget budget)
        {
            return new BudgetDto
            {
                budget_id = budget.budget_id,
                value = budget.value,
            };
        }

        public CategoryDto GetCategoryDto(Category category)
        {
            return new CategoryDto
            {
                category_id = category.category_id,
                name = category.name,
            };
        }

        public ExpenseDto GetExpenseDto(Expense expense)
        {
            return new ExpenseDto
            {
                date = expense.date,
                expense_id = expense.expense_id,
                value = expense.value,
                category = expense.category.name,
                wallet = expense.wallet.name
            };
        }

        public IncomeDto GetIncomeDto(Income income)
        {
            return new IncomeDto
            {
                value = income.value,
                date = income.date,
                income_id = income.income_id,
                wallet = income.wallet.name
            };
        }
        public PocketDto GetPocketDto(Pocket pocket)
        {
            return new PocketDto
            {
                goal = pocket.goal,
                icon = pocket.icon,
                name = pocket.name,
                pocket_id = pocket.pocket_id,
                total = pocket.total,
            };
        }
        public WalletDto GetWalletDto(Wallet wallet)
        {
            return new WalletDto
            {
                total = wallet.total,
                name = wallet.name,
                icon = wallet.icon,
                wallet_id = wallet.wallet_id,
            };
        }
    }
}
