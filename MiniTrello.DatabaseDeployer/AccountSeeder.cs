using System.Collections.Generic;
using DomainDrivenDatabaseDeployer;
using FizzWare.NBuilder;
using MiniTrello.Domain.Entities;
using NHibernate;

namespace MiniTrello.DatabaseDeployer
{
    public class AccountSeeder : IDataSeeder
    {
        readonly ISession _session;

        public AccountSeeder(ISession session)
        {
            _session = session;
        }

        public void Seed()
        {
            IList<Account> accountList = Builder<Account>.CreateListOfSize(10).Build();
            foreach (Account account in accountList)
            {
                var organizations = Builder<Organization>.CreateListOfSize(2).Build();
                foreach (var organization in organizations)
                {
                    var boards = Builder<Board>.CreateListOfSize(2).Build();
                    foreach (var board in boards)
                    {
                        _session.Save(board);
                        organization.AddBoard(board);
                    }
                    _session.Save(organization);
                    account.AddOrganization(organization);
                }
                _session.Save(account);
            }
        }
    }
}