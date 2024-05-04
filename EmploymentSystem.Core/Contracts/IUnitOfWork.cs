﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
    }
}
