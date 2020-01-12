using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository 
    { 
        public AccountRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        { 
            return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList(); 
        }

        public Account GetAccountById(Guid accId)
        {
            return FindByCondition(ac => ac.Id.Equals(accId)).FirstOrDefault();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll().OrderBy(ac => ac.Id).ToList();
        }

    }
}
