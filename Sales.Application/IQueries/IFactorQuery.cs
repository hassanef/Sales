﻿using Sales.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.IServices
{
    public interface  IFactorQuery
    {
        Task<List<FactorViewModel>> GetFactorById(int factorId);
    }
}
