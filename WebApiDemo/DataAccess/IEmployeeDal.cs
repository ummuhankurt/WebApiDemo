﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entities;

namespace WebApiDemo.DataAccess
{
    public interface IEmployeeDal : IEntityRepository<Employee>
    {
    }
}
