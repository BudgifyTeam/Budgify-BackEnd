﻿using BudgifyBll;
using BudgifyDal;
using BudgifyModels;
using BudgifyModels.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BudgifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IncomeBll _incomeBll;
        private readonly ResponseError resError = new ResponseError();

        public IncomeController(AppDbContext db)
        {
            _incomeBll = new IncomeBll(db);
        }

        [HttpPost("CreateIncome", Name = "CreateIncome")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseIncome>> CreateIncome(int userid, double value, string date, int wallet_id)
        {
            ResponseIncome response = await _incomeBll.CreateIncome(userid, value, Utils.convertDate(date), wallet_id);

            if (!response.code)
            {
                resError.message = response.message;
                resError.code = 0;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("DeleteIncome", Name = "DeleteIncome")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseIncome>> DeleteIncome(int incomeid)
        {
            ResponseIncome response = await _incomeBll.DeleteIncome(incomeid);

            if (!response.code)
            {
                resError.message = response.message;
                resError.code = 0;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("GetIncomes", Name = "GetIncomes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IncomeDto>> GetIncomes(int userid, string range)//range{day, week, month, year}
        {
            ResponseList<IncomeDto> response = _incomeBll.GetIncomes(userid, range);

            if (!response.code)
            {
                resError.message = response.message;
                resError.code = 0;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("GetIncomesByDay", Name = "GetIncomesByDay")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IncomeDto>> GetIncomesByDay(int userid, string date)
        {
            ResponseList<IncomeDto> response = _incomeBll.GetIncomesDay(userid, "day", Utils.convertDate(date));

            if (!response.code)
            {
                resError.message = response.message;
                resError.code = 0;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("ModifyIncome", Name = "ModifyIncome")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseIncome>> ModifyIncome([FromBody] IncomeDto income, int wallet_id)
        {
            ResponseIncome response = await _incomeBll.ModifyIncome(income, wallet_id);

            if (!response.code)
            {
                resError.message = response.message;
                resError.code = 0;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
