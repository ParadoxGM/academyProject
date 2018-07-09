using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Managers
{
    public class BaseManager
    {
        protected IUnitOfWork _unitOfWork;

        public BaseManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
