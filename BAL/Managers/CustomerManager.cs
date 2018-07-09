using BAL.Interfaces;
using DAL.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Managers
{

    public class CustomerManager : BaseManager, ICustomerManager
    {
        public CustomerManager(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public List<Customer> GetAll()
        {
            return _unitOfWork.CustRepos.GetAll();
        }

        public Customer GetById(string id)
        {
            return _unitOfWork.CustRepos.GetById(id);
        }

        public bool Create(Customer сustomer)
        {
            try
            {
                _unitOfWork.CustRepos.Insert(сustomer);// (ValidationManager.ValidateCustomer(сustomer));
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Delete(string сustomerid)
        {
            try
            {
                _unitOfWork.CustRepos.Delete(_unitOfWork.CustRepos.GetAll().FirstOrDefault(c => c.Id == сustomerid));  //removing customer
                Executor ex = _unitOfWork.ExecutRepos.GetAll().FirstOrDefault(x => x.CustomerId == сustomerid);               //finding executor
                if (ex != null)
                {
                    _unitOfWork.ExecutRepos.Delete(ex);
                }
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Update(Customer customer)
        {
            //try
            //{
            _unitOfWork.CustRepos.Update(customer);// (UpdatingCustomer(customer));
            _unitOfWork.Save();
            return true;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //return false;
        }

        //private Customer UpdatingCustomer(Customer c)
        //{
        //    if (c.Name == "Anonym")
        //        c.Name = "UpdatedName";
        //    if (c.Surname == "Anonym")
        //        c.Surname = "UpdatedSurname";
        //    if (c.UserName == "Anonym")
        //        c.UserName = "UpdatedUserName";
        //    if (c.PhoneNumber == "Wrong phone number")
        //        c.PhoneNumber = "Updated phone numb";
        //    if (c.Email == "wrongemail@email.com")
        //        c.Email = "updatedemail@email.com";
        //    return c;
        //}
    }
}
