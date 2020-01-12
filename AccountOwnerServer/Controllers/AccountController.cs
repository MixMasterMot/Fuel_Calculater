using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountOwnerServer.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            try
            {
                var accounts = _repository.Account.GetAllAccounts();
                _logger.LogInfo($"Returned all accounts from database.");

                var accountsResult = _mapper.Map<IEnumerable<AccountDto>>(accounts);
                return Ok(accountsResult);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(Guid id)
        {
            try
            {
                var acc = _repository.Account.GetAccountById(id);
                if(acc == null)
                {
                    _logger.LogError($"Account with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned account with id: {id}");
                    var accResult = _mapper.Map<AccountDto>(acc);
                    return Ok(accResult);
                }

            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAccountById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
