using System;
using System.Web.Mvc;
using SampleMvc.Domain;
using SampleMvc.Models;
using SampleMvc.Repository;

namespace SampleMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult UserDetails()
        {
            UserModel model = new UserModel();
            return View(model);
        }

        public JsonResult PopulateDetails(UserModel model)
        {
            UserResultModel userResultModel = new UserResultModel();
            if (String.IsNullOrEmpty(model.UserId))
            {
                userResultModel.Message = "UserId can not be blank";
                return Json(userResultModel);
            }

            User user = _userRepository.GetUser(model.UserId);

            if (user == null)
            {
                userResultModel.Message = String.Format("No UserId found for {0}", model.UserId);
                return Json(userResultModel);
            }
            userResultModel.LastName = user.LastName;
            userResultModel.FirstName = user.FirstName;
            userResultModel.Message = String.Empty; //success message is empty in this case

            return Json(userResultModel);
        }
    }

    public class UserResultModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Message { get; set; }
    }
}