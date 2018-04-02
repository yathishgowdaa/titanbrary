using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanbrary.Common.Interfaces.Data;
using Titanbrary.Common.Models;

namespace Titanbrary.Data.DACs
{
    public class AccountDAC : IAccountDAC
    {

        public AccountDAC()
        {

        }
        public UserModel GetUserInfoDAC(ApplicationUser currentUser)
        {
            var result = new UserModel();
            using (TitanbraryEntities ctx = new TitanbraryEntities())
            {
                var account = ctx.Users.Where(act => act.UserID == new Guid(currentUser.Id)).FirstOrDefault();
                if(account != null)
                {
                    result.Id = account.UserID.ToString();
                    result.FirstName = account.FirstName;
                    result.LastName = account.LastName;
                    result.Email = account.Email;
                    result.Address = account.Address;
                    result.City = account.City;
                    result.State = account.State;
                }
            }

            return result;
        }

        public bool SaveAccountDAC(UserModel model)
        {
            var target = new User();
            var result = false;
            try
            {
                using (TitanbraryEntities ctx = new TitanbraryEntities())
                {
                    target.UserID = new Guid(model.Id);
                    target.FirstName = model.FirstName;
                    target.LastName = model.LastName;
                    target.LoginName = model.Email;
                    target.Email = model.Email;
                    target.Password = model.Password;
                    target.Address = "123 Avenue";
                    target.City = "Fullerton";
                    target.State = "CA";
                    target.ZipCode = "92832";
                    target.Phone = "1114445555";
                    target.SQAnwer1 = "yellow";
                    target.SQAnswer2 = "black";
                    target.SQAnswer3 = "red";
                    target.DateOfBirth = DateTime.Now;
                    target.MemberSince = DateTime.Now;

                    ctx.Users.Add(target);
                    ctx.SaveChanges();
                    result = true;

                }

            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }
            
            return result;
        }
    }
}

