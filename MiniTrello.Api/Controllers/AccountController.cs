using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Api.CustomExceptions;
using MiniTrello.Api.Models;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;
using RestSharp;
using System.Text;
using System.Security.Cryptography;
namespace MiniTrello.Api.Controllers
{
    public class AccountController : ApiController
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;
        readonly IRegisterValidator<AccountRegisterModel> _registerValidator;

        public AccountController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine, IRegisterValidator<AccountRegisterModel> registerValidator)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
            _registerValidator = registerValidator;
        }

        [HttpPost]
        [POST("login")]
        public AuthenticationModel Login([FromBody] AccountLoginModel model)
        {
            var account =
                _readOnlyRepository.First<Account>(
                    account1 => account1.Email == model.Email && account1.Password == MD5Hash(model.Password));
            if (account != null)
            {
                string Token = CreatePassword(9);
                account.Token = Token;
                return new AuthenticationModel(){Token = Token};
            }
            
            throw new BadRequestException(
                "Usuario o clave incorrecto");
        }

        [AcceptVerbs("POST")]
        [POST("register")]
        public HttpResponseMessage Register([FromBody] AccountRegisterModel model)
        {
            var validateMessage = _registerValidator.Validate(model);
            if (!String.IsNullOrEmpty(validateMessage))
            {
                throw new BadRequestException(validateMessage);
            }
            model.Password = MD5Hash(model.Password);
            model.ConfirmPassword = model.Password;
            Account account = _mappingEngine.Map<AccountRegisterModel,Account>(model);
            Account accountCreated = _writeOnlyRepository.Create(account);
            if (accountCreated != null)
            {

                string Name=model.FirstName;
                this.SendEmail(Name,model.Email,"Congratulation "+Name+", Welcome to Minitrello");

            }
            throw new BadRequestException("Hubo un error al guardar el usuario");
        }
        
        [AcceptVerbs("PUT")]
        [PUT("updateInformation/{accessToken}")]
        public AccountModel UpdateInformation([FromBody] UpdateInformationModel model, string accessToken)
        {
            //validar seguridad

            var Account =   _readOnlyRepository.First<Account>(
                    account1 => account1.Token == accessToken);

            Account.FirstName = model.FirstName;
            Account.LastName = model.LastName;
            Account.Email = model.Email;

            var updatedAccount = _writeOnlyRepository.Update(Account);
            var accountModel = _mappingEngine.Map<Account, AccountModel>(updatedAccount);
            return accountModel;
        }


        [AcceptVerbs("PUT")]
        [PUT("/changePassword/{accessToken}")]
        public AccountModel changePassword([FromBody] changePasswordModel model,string accessToken)
        {

            var account =
                _readOnlyRepository.First<Account>(
                    account1 => account1.Token == accessToken);

            if (model.Password == model.ConfirmPassword)
                account.Password = MD5Hash(model.Password);

            var updatedAccount = _writeOnlyRepository.Update(account);
            var accountModel = _mappingEngine.Map<Account, AccountModel>(updatedAccount);


           return accountModel;

        }
        [AcceptVerbs("PUT")]
        [PUT("/recoverPassword")]
        public AccountModel RestablecerPassword([FromBody] RecoverPasswordModel model)
        {

            var account =
                _readOnlyRepository.First<Account>(
                    account1 => account1.Email == model.Email);
            string pass = CreatePassword(5);
            account.Password = MD5Hash(pass);
            
             var updatedAccount = _writeOnlyRepository.Update(account);
            var accountModel = _mappingEngine.Map<Account, AccountModel>(updatedAccount);


            string Name = account.FirstName;
            this.SendEmail(Name, model.Email, "Sorry you have lost your password, " + Name + ", here is your temporary Password: "+pass);
              return accountModel;
          
        }

        [GET("profile/{accessToken}")]
        public infoModel GetAccountByToken(string accessToken)
        {
            var account =
              _readOnlyRepository.First<Account>(
                  account1 => account1.Token == accessToken);
            
            return new infoModel() { FirstName = account.FirstName, LastName = account.LastName , Email= account.Email};
        }
         
        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }
                      
            return strBuilder.ToString();
        }
        public string CreatePassword(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }

        public void SendEmail(string Name,string Email,string Message)
        {
             RestClient client = new RestClient();
             client.BaseUrl = "https://api.mailgun.net/v2";
             client.Authenticator =
                       new HttpBasicAuthenticator("api",
                                                  "key-648zxupmcf2ay7nw3pp4dn--yn0cnhu6");
                RestRequest request = new RestRequest();
                request.AddParameter("domain",
                                    "sandbox22742.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Minitrello <postmaster@sandbox22742.mailgun.org>");
                request.AddParameter("to", Name+" <" +Email+">");
                request.AddParameter("subject", "Hi "+ Name);
                request.AddParameter("text", Message);
                request.Method = Method.POST;
                client.Execute(request);
        }

    }
    public class RecoverPasswordModel
    {
        public string Email { get; set; }
      
    }

    public class AccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateInformationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }


    public class changePasswordModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class infoModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}