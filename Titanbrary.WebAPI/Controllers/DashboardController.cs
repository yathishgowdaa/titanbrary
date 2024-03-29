﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Titanbrary.BusinessObjects;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Interfaces.Data;
using Titanbrary.Common.Models;

namespace Titanbrary.WebAPI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        private ApplicationUserManager _UserManager;
        private AccountManager accountMgr = new AccountManager();
        public DashboardController() { }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _UserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _UserManager = value;
            }
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        public ActionResult Index()
        {
            var result = new UserInfoBookModel();
            try
            {
                //check if authenticated
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("SignIp", "Home");
                }
                //get user role
                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                var roles = UserManager.GetRoles(currentUser.Id);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);
                UserInfoBookModel model = new UserInfoBookModel();
                model.book = new BookModel();
                model.books = new List<BookModel>();
                model.cart = new CartModel();
                model.genres = new List<GenreModel>();
                model.user = accountMgr.GetUserInfo(currentUser);
                model.newUser = new UserModel();
                //redirect to dashboard
                foreach (var role in roles)
                {
                    if (role == "Admin")
                    {
                        return View("Admin", model);

                    }
                    else if (role == "Manager")
                    {
                        return View("Manager", model);

                    }
                    else
                    {
                        return View(model);
                    }
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return View(result);
        }

        public ActionResult CreateUser()
        {
            //return the form to create a user with role
            //use API to register?
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult BookView()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            //get role
            var roles = UserManager.GetRoles(userInfo.Id);
            foreach (var role in roles)
            {
                if (role == "Admin")
                {
                    model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                }
                else if (role == "Manager")
                {
                    model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                }
                else
                {
                    model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                }
            }

            return View("Book/Index", model);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateBook()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                StringContent queryString = new StringContent("");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/GetAllGenres/", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;

                    model.genres = JsonConvert.DeserializeObject<List<GenreModel>>(response);

                    //return View("Book/SearchOverview", model);
                    return View("Book/Create", model);
                }

            }

            return View("Book/SearchOverview", model);
        }


        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateNewBook(BookModel book)
        {
            if (!ModelState.IsValid)
            {
                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");
            }
            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                
                string dataJson = JsonConvert.SerializeObject(book);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");   
                

                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/CreateBook", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                   
                    return RedirectToAction("CreateBookSuccessView");
                }

                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult UpdateBook()
        {
            //var currentUser = UserManager.FindByEmail(User.Identity.Name);
            //UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            //UserInfoBookModel model = new UserInfoBookModel();
            //model.user = userInfo;


            //return View("Book/Update", model);
            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                StringContent queryString = new StringContent("");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/GetAllBooks", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    var model = new UserInfoBookModel();
                    model.book = new BookModel();
                    model.books = JsonConvert.DeserializeObject<List<BookModel>>(response);
                    model.user = userInfo;

                    return View("Book/Update", model);
                }

                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult UpdateBookById(Guid bookId)
        {


            //api call
            var data = new Dictionary<string, string>
                   {
                       {"bookId", bookId.ToString()}
                   };

            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                var getTokenUrl = httpClient.GetAsync(String.Format("http://localhost:50799/api/Book/GetBookByBookID/{0}", bookId.ToString()));
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;

                    var model = new UserInfoBookModel();
                    model.user = userInfo;
                    model.book = JsonConvert.DeserializeObject<BookModel>(response);
                    //get list of genre
                    StringContent queryString = new StringContent("");
                    var getListGenres = httpClient.PostAsync("http://localhost:50799/api/Book/GetAllGenres", queryString);
                    var responseGenre = getListGenres.Result.Content.ReadAsStringAsync().Result;

                    var listOfGenre = JsonConvert.DeserializeObject<List<GenreModel>>(responseGenre);
                    model.genres = listOfGenre;
                    foreach (var genre in listOfGenre)
                    {
                        if (model.book.GenreID == genre.GenreID)
                        {
                            model.genre = genre;
                            break;
                        }
                    }


                    return View("Book/BookProfile", model);
                }
            }

            //return back to book list
            return RedirectToAction("UpdateBook", "Dashboard");
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateGenre()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            return View("Genre/Create", model);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult UpdateGenre()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            return View("Genre/Update", model);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        public ActionResult SearchView()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            model.book = new BookModel();
            //get role
            var roles = UserManager.GetRoles(userInfo.Id);
            foreach(var role in roles)
            {
                if (role == "Admin")
                {
                    model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                } else if (role == "Manager")
                {
                    model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                }
                else
                {
                    model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                }
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                StringContent queryString = new StringContent("");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/GetAllGenres/", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;

                    model.genres = JsonConvert.DeserializeObject<List<GenreModel>>(response);

                    return View("Book/SearchOverview", model);
                }

            }
            //return back to book list
            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost]
        public ActionResult QuickSearch(UserInfoBookModel Model)
        {
            string keywords = Model.book.Keywords;
            //api call
            var data = new Dictionary<string, string>
                   {
                       {"keywords", keywords}
                   };


            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                var getTokenUrl = httpClient.GetAsync(String.Format("http://localhost:50799/api/Book/SearchBooks/{0}", keywords));
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    var model = new UserInfoBookModel();
                    model.book = new BookModel();
                    model.books = JsonConvert.DeserializeObject<List<BookModel>>(response);
                    model.user = userInfo;
                    //get role
                    var roles = UserManager.GetRoles(userInfo.Id);
                    foreach (var role in roles)
                    {
                        if (role == "Admin")
                        {
                            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                        }
                        else if (role == "Manager")
                        {
                            model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                        }
                        else
                        {
                            model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                        }
                    }

                    return View("Book/Search/QuickSearch", model);
                }

                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost]
        public ActionResult SearchByGenre(Guid GenreID)
        {
            if (!ModelState.IsValid)
            {
                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
            string genreId = GenreID.ToString();
            //api call
            var data = new Dictionary<string, string>
                   {
                       {"genreId", genreId}
                   };


            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                var getTokenUrl = httpClient.GetAsync(String.Format("http://localhost:50799/api/Book/GetBooksByGenreID/{0}", genreId));
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    var model = new UserInfoBookModel();
                    model.book = new BookModel();
                    model.books = JsonConvert.DeserializeObject<List<BookModel>>(response);
                    model.user = userInfo;
                    //get role
                    var roles = UserManager.GetRoles(userInfo.Id);
                    foreach (var role in roles)
                    {
                        if (role == "Admin")
                        {
                            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                        }
                        else if (role == "Manager")
                        {
                            model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                        }
                        else
                        {
                            model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                        }
                    }
                    return View("Book/Search/SearchByGenre", model);
                }

                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost]
        public ActionResult AdvancedSearch(string bookId)
        {
            if (!ModelState.IsValid)
            {
                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
            //Guid bookID = Guid.Parse(bookId);
            //api call
            

            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                var getTokenUrl = httpClient.GetAsync(String.Format("http://localhost:50799/api/Book/GetBookByBookID/{0}", bookId.ToString()));
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    var model = new UserInfoBookModel();
                    model.book = JsonConvert.DeserializeObject<BookModel>(response);
                    model.books = new List<BookModel>();
                    model.user = userInfo;
                    //get role
                    var roles = UserManager.GetRoles(userInfo.Id);
                    foreach (var role in roles)
                    {
                        if (role == "Admin")
                        {
                            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                        }
                        else if (role == "Manager")
                        {
                            model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                        }
                        else
                        {
                            model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                        }
                    }


                    return View("Book/Search/AdvancedSearch", model);
                }

                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        public ActionResult GetAllBook()
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);
                var model = new UserInfoBookModel();
                //get role
                var roles = UserManager.GetRoles(userInfo.Id);
                foreach (var role in roles)
                {
                    if (role == "Admin")
                    {
                        model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                    }
                    else if (role == "Manager")
                    {
                        model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                    }
                    else
                    {
                        model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                    }
                }

                StringContent queryString = new StringContent("");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/GetAllBooks", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    
                    model.book = new BookModel();
                    model.books = JsonConvert.DeserializeObject<List<BookModel>>(response);
                    model.user = userInfo;
                    

                    return View("Book/Search/GetAllBook", model);
                }

                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        public ActionResult CartView()
        {

            //get the cart if existant
            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                string dataJson = JsonConvert.SerializeObject(userInfo.Id);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Cart/GetCartByUserId", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(20));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    // 
                    var model = new UserInfoBookModel();
                    //get role
                    var roles = UserManager.GetRoles(userInfo.Id);
                    foreach (var role in roles)
                    {
                        if (role == "Admin")
                        {
                            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                        }
                        else if (role == "Manager")
                        {
                            model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                        }
                        else
                        {
                            model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                        }
                    }


                    //if null means no cart
                    //show cart is empty
                    if (response == "null")
                    {
                       
                        model.book = new BookModel();
                        model.books = new List<BookModel>();
                        model.user = userInfo;
                        model.cart = new CartModel();
                        model.countItem = 0;

                        return View("cart/Index", model);


                    }

                    var jsonResponse = JsonConvert.DeserializeObject<CartModel>(response);
                   
                    model.book = new BookModel();
                    model.books = jsonResponse.Books;
                    model.user = userInfo;
                    model.cart = jsonResponse;
                    model.countItem = model.books.Count();
                   
                    return View("Cart/Index", model);
                }

                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }


            //return View("Cart/Index", model);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        public ActionResult AddToCart(string bookToCart)
        {
            if (!ModelState.IsValid)
            {
                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");
            }
            //get usesrinfo
            //check if cart exist -> GetCart API
            //return CartModel with book in the cart
            //if 200 -> addBookToCart API(cart Id)
            //if 200 null -> createCart with CartXBookModel
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);

            //setup my cart
            var body = new CartModel();
            body.BookList = new List<CartXBookModel>();
            body.BookList.Add(new CartXBookModel()
            {
                BookID = Guid.Parse(bookToCart),
                Quantity = 1
            });
            body.UserID = Guid.Parse(userInfo.Id);


            var hasCart = getCartByUserId(userInfo.Id);
            if (hasCart.CartID != Guid.Empty)
            {
                //add the book to the existing cart
                //call addBookToCart API here
                //return success view
                //or home
                body.CartID = hasCart.CartID;
                if (!addBookToCart(body))
                {
                    //return back to book list
                    return RedirectToAction("SearchView", "Dashboard");
                }

                return RedirectToAction("AddToCartSuccessView");
            }

            //create new cart 
            //add book to the cart while creating cart
            //return success view 
            //or home

            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);


                body.UserID = Guid.Parse(userInfo.Id);
                string dataJson = JsonConvert.SerializeObject(body);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Cart/CreateCart", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    var model = new UserInfoBookModel();
                    model.book = new BookModel();
                    model.books = JsonConvert.DeserializeObject<List<BookModel>>(response);
                    model.user = userInfo;

                    return RedirectToAction("AddToCartSuccessView");
                }

                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");

            }
        }
        [Authorize(Roles = "Admin, Manager, Customer")]
        public ActionResult removeToCart(string cartId, string bookToCart)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var data = new Dictionary<string, string>
                   {
                       {"bookId", bookToCart},
                       {"cartid", cartId }
                   };


                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);



                string dataJson = JsonConvert.SerializeObject(data);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/DeleteBookFromCart", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("CartView", "Dashboard");
                }
            }
            //return back to book list
            return RedirectToAction("SearchView", "Dashboard");
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        public ActionResult checkOut(string cartId)
        {
            if (!ModelState.IsValid)
            {
                //return back to book list
                return RedirectToAction("SearchView", "Dashboard");
            }

            //call api
            using (HttpClient httpClient = new HttpClient())
            {
                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);
                UserInfoBookModel model = new UserInfoBookModel();
                model.user = userInfo;

                var data = new Dictionary<string, string>
                   {
                       {"cartid", cartId }
                   };


                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);



                string dataJson = JsonConvert.SerializeObject(cartId);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Cart/Checkout", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    model.msg = "You're all set! An email was sent for your record.";
                    //get role
                    var roles = UserManager.GetRoles(userInfo.Id);
                    foreach (var role in roles)
                    {
                        if (role == "Admin")
                        {
                            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                        }
                        else if (role == "Manager")
                        {
                            model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                        }
                        else
                        {
                            model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                        }
                    }
                    return View("Cart/AddedToCartMsg", model);
                }
            }
            //return back to book list
            return RedirectToAction("SearchView", "Dashboard");
        }

        private bool addBookToCart(CartModel book)
        {
            //add the book to the existing cart
            var result = false;

            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);



                string dataJson = JsonConvert.SerializeObject(book);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/AddBookToCart", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    return true;
                }
            }

            return result;
        }

        private CartModel getCartByUserId(string userId)
        {
            var result = new CartModel();
            //get the cart from user and return cart
            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);
                var body = Guid.Parse(userId);
                string dataJson = JsonConvert.SerializeObject(body);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Cart/GetCartByUserId", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    if(response == "null")
                    {
                        result = new CartModel();
                        return result;                        
                    }
                    result = JsonConvert.DeserializeObject<CartModel>(response);
                    return result;
                }

            }
            return result;

        }

        public ActionResult AddToCartSuccessView()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            model.msg = "Book was added to your cart.";
            //model.actionBtn = "View Cart";
            //get role
            var roles = UserManager.GetRoles(userInfo.Id);
            foreach (var role in roles)
            {
                if (role == "Admin")
                {
                    model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                }
                else if (role == "Manager")
                {
                    model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                }
                else
                {
                    model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                }
            }
            return View("Cart/AddedToCartMsg", model);
        }

        public ActionResult CreateBookSuccessView()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            model.msg = "Book was created.";
            //model.actionBtn = "View Cart";
            //get role
            var roles = UserManager.GetRoles(userInfo.Id);
            foreach (var role in roles)
            {
                if (role == "Admin")
                {
                    model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                }
                else if (role == "Manager")
                {
                    model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                }
                else
                {
                    model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                }
            }
            return View("Cart/AddedToCartMsg", model);
        }

        [Authorize(Roles = ("Admin, Manager, Customer"))]
        public ActionResult AddToWaitList(string bookId)
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;

            var data = new Dictionary<string, string>
                   {
                       {"bookId", bookId },
                       {"userId", userInfo.Id }
                   };

            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);
               
                string dataJson = JsonConvert.SerializeObject(data);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/AddBookToWaitlist", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    model.msg = "Book was added to the waitlist. You will receive an email once it's available.";
                    //get role
                    var roles = UserManager.GetRoles(userInfo.Id);
                    foreach (var role in roles)
                    {
                        if (role == "Admin")
                        {
                            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                        }
                        else if (role == "Manager")
                        {
                            model.layout = "~/Views/Shared/_Layout_Manager.cshtml";
                        }
                        else
                        {
                            model.layout = "~/Views/Shared/_Layout_Customer.cshtml";
                        }
                    }
                    return View("Cart/AddedToCartMsg", model);
                }

            }
            return View();
        }
        [Authorize(Roles = ("Admin, Manager"))]
        public ActionResult EditBook(UserInfoBookModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var cookie = HttpContext.Request.Cookies.Get("AuthenticationToken");
                var token = cookie.Value.Split(':');
                var newToken = "Bearer" + token[1];
                httpClient.DefaultRequestHeaders.Add("Authorization", newToken);

                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                string dataJson = JsonConvert.SerializeObject(model.book);
                var queryString = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/api/Book/UpdateBook", queryString);
                getTokenUrl.Wait(TimeSpan.FromSeconds(20));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;


                  
                    UserInfoBookModel result = new UserInfoBookModel();
                    result.user = userInfo;
                    result.msg = "Book updated!";
                    result.layout = "~/Views/Shared/_Layout_Admin.cshtml";
                    
                    return View("Cart/AddedToCartMsg", result);                  
                }
            }

            return RedirectToAction("UpdateBook");
        }

        //send msg to user
        //public ActionResult updateBookSuccess()
        //{
        //    var currentUser = UserManager.FindByEmail(User.Identity.Name);
        //    UserModel userInfo = accountMgr.GetUserInfo(currentUser);
        //    UserInfoBookModel model = new UserInfoBookModel();
        //    model.user = userInfo;
        //    model.msg = "Book updated!";
        //    model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
        //    return View("Cart/AddedToCartMsg", model);
        //}

        [Authorize(Roles = ("Admin"))]
        public ActionResult UserView()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
            model.newUser = new UserModel();
            return View("User/Index", model);
        }

        [Authorize(Roles = ("Admin"))]
        public async Task<ActionResult> CreateManager(UserInfoBookModel model)
        {
            //create ASP.NET USER
            var user = new ApplicationUser()
            {
                UserName = model.newUser.Email,
                Email = model.newUser.Email,
                //Password = model.Password,
                UserRoles = "Manager"
            };

            var result = await UserManager.CreateAsync(user, model.newUser.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Account could not be created. Please try again");
                return View();
            }

            var userInfo = await UserManager.FindByEmailAsync(model.newUser.Email);
            var addRole = await UserManager.AddToRoleAsync(userInfo.Id, user.UserRoles);
            if (!addRole.Succeeded)
            {
                ModelState.AddModelError("Error", "Account could not be created. Please try again");
                return View();
            }
            //Save to UserAccount Table
            model.newUser.Id = userInfo.Id;
            var acc = accountMgr.SaveAccount(model.newUser);
            if (!acc)
            {
                return RedirectToAction("FailedAccountCreation");
            }

            return RedirectToAction("SuccessAccountCreation");
        }

        public ActionResult FailedAccountCreation()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            model.msg = "Cannot create the account!";
            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
            return View("Cart/AddedToCartMsg", model);
        }

        public ActionResult SuccessAccountCreation()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            UserInfoBookModel model = new UserInfoBookModel();
            model.user = userInfo;
            model.msg = "Account created!";
            model.layout = "~/Views/Shared/_Layout_Admin.cshtml";
            return View("Cart/AddedToCartMsg", model);
        }



    }
}