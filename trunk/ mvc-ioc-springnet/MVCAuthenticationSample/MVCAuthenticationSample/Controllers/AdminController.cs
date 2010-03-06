using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Interfaces.Services.Admin;

namespace MVCAuthenticationSample.Controllers
{
    public class AdminController : BaseController
    {

        public IUsersService UserService { get; set; }

        public ActionResult Index()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetAllUsers(FormCollection form)
        {

            var page = Convert.ToInt32(form["page"]);
            var rows = Convert.ToInt32(form["rows"]);
            string direction = form["sord"];
            var pageIndex = Convert.ToInt32(page) - 1;
            var pageSize = rows;
            var totalRecords = 0;

            var q = UserService.GetAllByCrietria(pageIndex * pageSize, rows);
            totalRecords = UserService.GetAll().Count();

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var jsonData = new
                {
                    Total = totalPages,
                    Page = page,
                    Records = totalRecords,
                    Rows =
                    (
                        from user in q select new { ID = user.ID, UserName = user.UserName }
                    )
                };

            return Json(jsonData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MVCDemoAuthorizeAttribute(Module = Modules.Administration, Rights = Functions.Access | Functions.ManageUsers)]
        public JsonResult EditUser(FormCollection form)
        {

            try
            {
                int id = Convert.ToInt32(form["id"]);
                string username = form["username"];
                string password = form["password"];

                var user = UserService.GetById(id);

                //change password only if changed by user in the GUI
                //keep in mind that it is hardcoded value in Admin.js on edit, in real world application store it in config and 
                //pass it to the js, so you will keep it at one place only
                if (!password.Equals("******"))
                {
                    string salt = "";
                    string hash;
                    UserService.GenerateUserPassowordHash(password, ref salt, out hash);
                    user.Hash = hash;
                    user.Salt = salt;
                }

                user.UserName = username;
                UserService.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { code = "error", error = String.Format("Error on updating user: {0}", ex.Message) });
            }

            return Json(true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MVCDemoAuthorizeAttribute(Module = Modules.Administration, Rights = Functions.Access | Functions.ManageUsers)]
        public JsonResult DeleteUser(FormCollection form)
        {
            var userID = Convert.ToInt32(form["userID"]);
            try
            {
                UserService.Delete(UserService.GetById(userID));
                UserService.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { code = "error", error = String.Format("Error on deleting user: {0}", ex.Message) });

            }
            return Json(true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MVCDemoAuthorizeAttribute(Module = Modules.Administration, Rights = Functions.Access | Functions.ManageUsers)]
        public JsonResult AddUser(FormCollection form)
        {
            try
            {
                string username = form["username"];
                string password = form["password"];
                string salt = "";
                string hash;
                UserService.GenerateUserPassowordHash(password, ref salt, out hash);
                UserService.AddNewUser(username, hash, salt, 1); //administrator

            }
            catch (Exception ex)
            {
                return Json(new { code = "error", error = String.Format("Error on adding user: {0}", ex.Message) });
            }

            return Json(true);
        }

        [MVCDemoAuthorizeAttribute(Module = Modules.Administration, Rights = Functions.Access | Functions.ManageUsers)]
        //[Authorize(Users="kbochevski,administrator")]
        public ActionResult ModifyUsers()
        {
            return View();
        }

    }
}
