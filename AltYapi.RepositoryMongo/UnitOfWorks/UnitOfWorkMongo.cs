﻿using AltYapi.Core.UnitOfWorks;
using AltYapi.RepositoryMongo.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.RepositoryMongo.UnitOfWorks
{
    public class UnitOfWorkMongo : IUnitOfWorkMongo
    {
        private readonly IMongoContext _context;

        public UnitOfWorkMongo(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
