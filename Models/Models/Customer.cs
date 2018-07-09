using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Models.Models
{
    //[Serializable]
    public class Customer : IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }        //public int CustomerId { get; set; }
        [Display(Name = "Customer")]
        public string Name { get; set; }
        public string Surname { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
        public string Photo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Executor> Executors { get; set; }

        public Customer() { Executors = new HashSet<Executor>(); }

        public Customer(string userId, string name, string surname, string userName, string email, string phone, string photo)
        {
            Id = userId;
            Name = name;
            Surname = surname;
            UserName = userName;
            Email = email;
            PhoneNumber = phone;
            Photo = photo;
        }

        public override string ToString()
        {
            return $"\n\tCustomerId: {Id}\tName: {Name}\tSurname: {Surname}\tUserName: {UserName}\tEmail: {Email}\tPhone: {PhoneNumber}";
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Customer> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }
}
