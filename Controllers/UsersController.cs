using AvtotestMVC.Models;
using AvtotestMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvtotestMVC.Controllers;

public class UsersController : Controller
{
    // GET: UserController
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SignUp(CreateUserModel createUser)
    {
        if (!ModelState.IsValid)
        {
            return View(createUser);
        }
        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Name = createUser.Name,
            Password = createUser.Password,
            Username = createUser.Username,
            PhotoPath = SavePhoto(createUser.Photo),
            TicketResults = new List<TicketResult>()
        };

       

        
        HttpContext.Response.Cookies.Append("UserId", user.Id);
        UserService.Users.Add(user);
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult SignIn()
    {
       
        return View();
    }
    [HttpPost]
    public IActionResult SignIn(SignInUserModel signInUserModel)
    {
        if(!ModelState.IsValid) 
        {
            return View(signInUserModel);
        }
        var user = UserService.Users.FirstOrDefault(
            u => u.Username == signInUserModel.Username && 
            u.Password == signInUserModel.Password);
        if (user == null)
        {
            ModelState.AddModelError("Username", "xexesalom username yoki password xato");
            return View();
        }
        HttpContext.Response.Cookies.Append("UserId", user.Id);
        return RedirectToAction("Profile");
    }

    public IActionResult Profile()
    {
        if (HttpContext.Request.Cookies.ContainsKey("UserId"))
        {
            var userId = HttpContext.Request.Cookies["UserId"];
            var user = UserService.Users.FirstOrDefault(u=> u.Id == userId);
            if (user == null)
            {
                return RedirectToAction("SignIn");

            }
            return View(user);
        }
        return RedirectToAction("SignUp");
    }

    public IActionResult LogOut()
    {
        HttpContext.Response.Cookies.Delete("UserId");

        return RedirectToAction("SignIn");
    }

    public IActionResult UpdateUser()
    {
        ViewBag.UserId = HttpContext.Request.Cookies["UserId"];
        return View();
    }

    [HttpPost]
    public IActionResult UpdateUser(CreateUserModel updateUser)
    {
        var oldUser = UserService.GetCurrentUser(HttpContext);
        oldUser.Name = updateUser.Name;
        oldUser.Username = updateUser.Username;
        oldUser.Password = updateUser.Password;
        oldUser.PhotoPath = SavePhoto(updateUser.Photo);
        
        return RedirectToAction("Index", "Home");
    }

    private string SavePhoto(IFormFile file)
    {
        if (!Directory.Exists("wwwroot/UserImages"))
            Directory.CreateDirectory("wwwroot/UserImages");

        var fileName = Guid.NewGuid().ToString() + ".jpg";
        var ms = new MemoryStream();
        file.CopyTo(ms);
        System.IO.File.WriteAllBytes(Path.Combine("wwwroot", "UserImages", fileName), ms.ToArray());

        return "/UserImages/" + fileName;
    }


}
