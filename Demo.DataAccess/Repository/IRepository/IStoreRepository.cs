﻿using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repository.IRepository
{
    public interface IStoreRepository:IRepository<Store>
    {
        void Update(Store obj);
        //void Save();
    }
}
