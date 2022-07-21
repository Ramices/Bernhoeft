using Bernhoeft.Entitys;
using Google.Cloud.Functions.Framework;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Bernhoeft.DataContext;
using Bernhoeft.Services;
using System;
using Newtonsoft.Json;
using Bernhoeft.Utils;

namespace Bernhoeft.Controllers
{

    [FunctionsStartup(typeof(Startup))]
    public class UserController : IHttpFunction
    {

        private UserServices userServices { get; set; }

        public UserController(Context context)
        {
            userServices = new UserServices(context);
        }

        private string HandleUserGet(HttpContext context)
        {
            string response = "";

            if (context.Request.Method == "GET")
            {
                context.Response.ContentType = "application/json";
                response = JsonConvert.SerializeObject(userServices.GetUsersByName(Helper.GetUrlParameter(context, "name")));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response = "METHOD NOT ALLOWED";
            }

            return response;
        }

        private string HandleUserAdd(HttpContext context)
        {
            string response = "";

            if (context.Request.Method == "POST")
            {
                context.Response.ContentType = "application/json";

                User user = Helper.ParserUserFromJson(context.Request.Body);
                if(!userServices.UserExists(user.Username))
                {

                    userServices.Add(user);
                    response = "Usuário adicionado com sucesso";
                }
                else
                {
                    response = "Usuário já existe!";
                }


            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response = "METHOD NOT ALLOWED";
            }

            return response;
        }

        private string HandleUserDelete(HttpContext context)
        {
            string response = "";

            if (context.Request.Method == "DELETE")
            {
                context.Response.ContentType = "application/json";

                User userToDelete = userServices.GetUsersByUsername(Helper.GetUrlParameter(context, "username"));
                if (userToDelete != null)
                {
                    userServices.Remove(userToDelete);
                    response = "Usuário removido com sucesso";
                }
                else
                {
                    response = "Usuário não existe!";
                }


            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response = "METHOD NOT ALLOWED";
            }

            return response;
        }

        private string HandleUserUpdate(HttpContext context)
        {
            string response = "";

            if (context.Request.Method == "PUT")
            {
                context.Response.ContentType = "application/json";

                User userToUpdate = userServices.GetUsersByUsername(Helper.GetUrlParameter(context, "username"));
                if (userToUpdate != null)
                {
                    User userFromJson = Helper.ParserUserFromJson(context.Request.Body);

                    if(!userFromJson.Username.Equals(userToUpdate.Username))
                    {
                        userToUpdate.Name = userFromJson.Name;
                        userToUpdate.Email = userFromJson.Email;
                        userToUpdate.Password = userFromJson.Password;
                        userToUpdate.Username = userFromJson.Username;
                        userServices.Update(userToUpdate);
                        response = "Usuário atualizado com sucesso";

                    }
                    else
                    {
                        response = "O novo nome de usuário não pode conter um nome já existente!";

                    }

                }
                else
                {
                    response = "Usuário não existe!";
                }


            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response = "METHOD NOT ALLOWED";
            }
            return response;
        }

        public async Task HandleAsync(HttpContext context)
        {
            string response = "";


            switch (context.Request.Path.ToString().Split('?')[0])
            {
                default:
                    response = "NOT FOUND";
                    break;

                case "/user/add/":
                    response = HandleUserAdd(context);

                    break;


                case "/user/update/":
                    response = HandleUserUpdate(context);

                    break;

                case "/user/remove/":
                    response = HandleUserDelete(context);

                    break;

                case "/user/get/":
                    response = HandleUserGet(context);            
                    break;
            }

            await context.Response.WriteAsync(response);
         }
 
    }
}
