using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using BlogifyWebApp.Models;
using BlogifyWebApp.Models.Interfaces;
using BlogifyWebApp.Models.Helpers;

namespace BlogifyWebApp.Controllers
{
    public class BlogController : Controller
    {

        private readonly IBlogProvider _blogProvider;

        //2020-01-13 - Kadel D. Lacatt
        //public BlogController(IBlogProvider blogProvider)
        //Controller constructor, input parameters is DI for IBlogProvider and BloProvider
        public BlogController(IBlogProvider blogProvider)
        {
            _blogProvider = blogProvider;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2020-01-13 - Kadel D. Lacatt
        //public ActionResult Index()
        //Handles GET verb requests, returns ListBlogViewModel with a list a blogs from db.
        //Renders Index View
        [HttpGet]
        public ActionResult Index()
        {

            ListBlogViewModel listBlogVM = new ListBlogViewModel();
            var sli = new List<SelectListItem>();
                
            sli.Add(new SelectListItem { Text = "[CATEGORY]", Value = "", Selected = true });
            foreach (ICategory cat in _blogProvider.ListCategories())
            {
                sli.Add(new SelectListItem { Text = cat.Name, Value = cat.Id.ToString(), Selected = false });
            }

            listBlogVM.BlogCategories = sli;
            listBlogVM.BlogEntries = _blogProvider.ListBlogs(null);

            //If this action is called by redirect from another action (BlogController.Create, BlogController.Edit, 
            //BlogController.Details or BlogController.Delete). Checks the TempData for fill the ViewBag.Result and 
            //render alerts on the returned View
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<ResultViewModel>("Result");
            }

            return View(listBlogVM);
            
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2020-01-13 - Kadel D. Lacatt
        //public ActionResult Index()
        //Handles POST verb requests, as input parameters receives a category blog id selected on UI
        //to filter blog list results. Returns ListBlogViewModel with a list a blogs from db.
        //Renders Index View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string categoryId)
        {
            ListBlogViewModel listBlogVM = new ListBlogViewModel();
            var sli = new List<SelectListItem>();

            sli.Add(new SelectListItem { Text = "[CATEGORY]", Value = "", Selected = (categoryId == "") });
           
            foreach (ICategory cat in _blogProvider.ListCategories())
            {

                sli.Add(new SelectListItem 
                                { 
                                    Text = cat.Name, 
                                    Value = cat.Id.ToString(), 
                                    Selected = (cat.Id.ToString() == categoryId) 
                                }
                        );
            }
            
            listBlogVM.BlogCategories = sli;
            if (String.IsNullOrEmpty(categoryId))
            {
                listBlogVM.BlogEntries = _blogProvider.ListBlogs(null);
            }else
            {
                listBlogVM.BlogEntries = _blogProvider.ListBlogs(int.Parse(categoryId));
            }
            

            return View(listBlogVM);

         }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2020-01-13 - Kadel D. Lacatt
        //public ActionResult MyBlogs()
        //Handles GET verb requests, only for authenticated users with role Writer
        //obtains the authenticated username from context and selects all blogs from the user
        //Returns ListBlogViewModel with a list a blogs from db.
        //Renders Index View
        [Authorize]
        [HttpGet]
        public ActionResult MyBlogs()
        {
            ListBlogViewModel listBlogVM = new ListBlogViewModel();
            string userName;

            //Check if the user is authenticated and then obtain username to 
            //do the data retrieval
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                userName = HttpContext.User.Identity.Name;
            }else
            {
                return Unauthorized();
            }

            //Check the user role is writer
            if (!HttpContext.User.IsInRole(GeneralHelper.WRITER_ROLENAME))
            {
                return Unauthorized();
            }


            var sli = new List<SelectListItem>();


            sli.Add(new SelectListItem { Text = "[CATEGORY]", Value = "", Selected = true });
            foreach (ICategory cat in _blogProvider.ListCategories())
            {
                sli.Add(new SelectListItem { Text = cat.Name, Value = cat.Id.ToString(), Selected = false });
            }

            listBlogVM.BlogCategories = sli;
            listBlogVM.BlogEntries = _blogProvider.ListMyBlogs(userName,null);

            //If this action is called by redirect from another action (BlogController.Create, BlogController.Edit, 
            //BlogController.Details or BlogController.Delete). Checks the TempData for fill the ViewBag.Result and 
            //render alerts on the returned View
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<ResultViewModel>("Result");
            }

            return View(listBlogVM);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2020-01-13 - Kadel D. Lacatt
        //public ActionResult MyBlogs()
        //Handles POST verb requests, only for authenticated users with role Writer.
        //Obtains the authenticated username from context and selects all blogs from the user
        //It also receives a categoryId selected from the UI for an additional filter.
        //Returns ListBlogViewModel with a list a blogs from db.
        //Renders Index View
        [Authorize(Roles = "Writer")]
        [HttpPost]
        public ActionResult MyBlogs(string categoryId)
        {
            ListBlogViewModel listBlogVM = new ListBlogViewModel();
            string userName = HttpContext.User.Identity.Name;

            //Check the user role is writer
            if (!HttpContext.User.Identity.IsAuthenticated ||
                !HttpContext.User.IsInRole(GeneralHelper.WRITER_ROLENAME))
            {
                return Unauthorized();
            }
            
            var sli = new List<SelectListItem>();
            sli.Add(new SelectListItem { Text = "[CATEGORY]", Value = "", Selected = (categoryId == "") });

            foreach (ICategory cat in _blogProvider.ListCategories())
            {

                sli.Add(new SelectListItem
                {
                    Text = cat.Name,
                    Value = cat.Id.ToString(),
                    Selected = (cat.Id.ToString() == categoryId)
                });
            }

            listBlogVM.BlogCategories = sli;
            listBlogVM.BlogEntries = _blogProvider.ListMyBlogs(userName, (categoryId == "" ? null : int.Parse(categoryId)));

            if (String.IsNullOrEmpty(categoryId))
            {
                listBlogVM.BlogEntries = _blogProvider.ListMyBlogs(userName, null);
            }
            else
            {
                listBlogVM.BlogEntries = _blogProvider.ListMyBlogs(userName, int.Parse(categoryId));
            }
            return View(listBlogVM);

        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2020-01-13 - Kadel D. Lacatt
        //public ActionResult MyBlogs()
        //Handles GET verb requests. Obtains data from one blog entry a its comments with a given blog id.
        //Returns BlogDetailViewModel blog entry information and its comments.
        //Renders Details View
        // GET: BlogController/Details/id
        public ActionResult Details(int id)
        {

            BlogDetailViewModel blogDetailVM = new BlogDetailViewModel();
            blogDetailVM.blog = _blogProvider.GetBlog(id); //Obtains blog data.

            //If this action is called by redirect from another action (BlogController.Comment)
            //Checks the TempData for fill the ViewBag.Result and render alerts on View(Details)
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<ResultViewModel>("Result");
            }

            if (blogDetailVM.blog != null)
            {
                //If the blog exists, then we check for the comments.
                blogDetailVM.comments = _blogProvider.ListComments(id);
                return View(blogDetailVM);

            }else
            {
                TempData.Put("Result", new ResultViewModel() { 
                                            Code = -999, 
                                            Message = "Blog entry not found." 
                });

                return RedirectToAction("Index");
            }
            
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult MyBlogs()
        //Handles GET verb requests for BlogController/Create. Creates a list of selectlistitem with categories
        //Authorized for User with Writer role
        //Returns Create View with the form for create a blog entry.
        [Authorize(Roles = "Writer")]
        [HttpGet]
        public ActionResult Create()
        {

            var sli = new List<SelectListItem>();

            foreach (ICategory cat in _blogProvider.ListCategories())
            {

                sli.Add(new SelectListItem
                {
                    Text = cat.Name,
                    Value = cat.Id.ToString(),
                    Selected = false
                });
            }

            ViewBag.BlogCategories = sli;

            return View();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Create(IBlog blog)
        //Handles POST requests for BlogController/Create. Validates the blog data and add it to db
        //Authorized for User with Writer role
        //Returns Succesful creation redirects to MyBlogs (GET) action. 
        //on failed creation returns Create View with model and resultviewmodel for rendering 
        //alerts to the user
        [Authorize(Roles = "Writer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]BlogCreateViewModel blog)
        {
            ResultViewModel result = new ResultViewModel();
            string userName = HttpContext.User.Identity.Name;
            
            try
            {
                //Validate data received from the form.
                if (!String.IsNullOrEmpty(blog.Title) && !String.IsNullOrEmpty(blog.Data))
                {
                    //Check length requirements 
                    if (blog.Data.Length<=5000 && blog.Title.Length<=120)
                    {
                        
                        if (_blogProvider.AddBlog(blog.Category, blog.Title, blog.Data, userName))
                        {
                            result.setSuccess(0, "Blog added.");
                            TempData.Put("Result", result);
                            return RedirectToAction("MyBlogs");
                        }

                    }else
                    {
                        //Adds the warning message to resultviewmodel. Need it for render the alert message to the user
                        result.addWarningMessage("Blog content (5000) or title (120) max length has been exceed. Please check and try again.");
                    }


                }else
                {
                    //Adds the warning message to resultviewmodel. Need it for render the alert message to the user
                    result.addWarningMessage("Blog information is incomplete. All fields are required, please check and try again.");
                }

                var sli = new List<SelectListItem>();

                foreach (ICategory cat in _blogProvider.ListCategories())
                {

                    sli.Add(new SelectListItem
                    {
                        Text = cat.Name,
                        Value = cat.Id.ToString(),
                        Selected = false
                    });
                }

                ViewBag.BlogCategories = sli;
                ViewBag.Result = result;
                return View(blog);
                
            }
            catch (Exception ex)
            {
                result.setErrorMessage(ex.Message);
                TempData.Put("Result", result);
                return RedirectToAction("MyBlogs");
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Create(IBlog blog)
        //Handles GET requests for BlogController/Edit/[id]. Finds the blog by id a sends it back to the view
        //Authorized for User with Writer role
        //Returns A Render of the View Blog/Edit with the blog information using BlogViewModel
        //on failed creation returns a redirect to MyBlogs action with model and resultviewmodel for rendering 
        //alerts to the user
        [Authorize(Roles = "Writer")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ResultViewModel result = new ResultViewModel();
            string userName = HttpContext.User.Identity.Name;
            IBlog blog = _blogProvider.GetBlogForEdition(id, userName);

            if (blog != null)
            {

                var sli = new List<SelectListItem>();

                foreach (ICategory cat in _blogProvider.ListCategories())
                {
                    sli.Add(new SelectListItem
                    {
                        Text = cat.Name,
                        Value = cat.Id.ToString()
                    });
                }

                ViewBag.BlogCategories = sli;
                return View(new BlogCreateViewModel()
                {
                     Id = blog.Id,
                     Category = blog.Category,
                     Data = blog.Data,
                     Title = blog.Title
                });

            }
            else
            {
                result.setErrorMessage("There was a problem loading the requested by the provided blog id. " +
                                       "This probably caused by a status change or you are not the author of the entry" + 
                                       "Please check your information, try again or contact support.");

                TempData.Put("Result", result);
                return RedirectToAction("MyBlogs");
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        // POST: BlogController/Edit/5
        [Authorize(Roles = "Writer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] BlogCreateViewModel blog)
        {
            
            ResultViewModel result = new ResultViewModel();
            string userName = HttpContext.User.Identity.Name;

            try
            {
                //Validate data received from the form.
                if (!String.IsNullOrEmpty(blog.Title) && !String.IsNullOrEmpty(blog.Data))
                {
                    //Check length requirements and if the Id greater than cero
                    if (blog.Data.Length <= 5000 && blog.Title.Length <= 120 && blog.Id > 0)
                    {

                        if (_blogProvider.SaveBlog(blog))
                        {
                            result.setSuccess(0, "Blog saved.");
                            TempData.Put("Result", result);
                            return RedirectToAction("MyBlogs");
                        }

                    }
                    else
                    {
                        //Adds the warning message to resultviewmodel. Need it for render the alert message to the user
                        result.addWarningMessage("Blog content (5000) or title (120) max length has been exceed. Please check and try again.");
                    }
                }
                else
                {
                    //Adds the warning message to resultviewmodel. Need it for render the alert message to the user
                    result.addWarningMessage("Blog information is incomplete. All fields are required, please check and try again.");
                }

                var sli = new List<SelectListItem>();

                foreach (ICategory cat in _blogProvider.ListCategories())
                {

                    sli.Add(new SelectListItem
                    {
                        Text = cat.Name,
                        Value = cat.Id.ToString(),
                        Selected = false
                    });
                }

                ViewBag.BlogCategories = sli;
                ViewBag.Result = result;
                return View(blog);

            }
            catch (Exception ex)
            {
                result.setErrorMessage(ex.Message);
                TempData.Put("Result", result);
                return RedirectToAction("MyBlogs");
            }
        }

        //2020-01-13 - Kadel D. Lacatt
        //public ActionResult Comment(IFormCollection collection)
        //Action that handles the request for adding a comment to a blog. 
        //Recevives a IFormCollection with posted form data. 
        //OutPut: Redirects to Details Action and shares a tempdata variable call result
        //result is used for rendering bootstrap alerts in Details View.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(IFormCollection collection)
        {

            string userName = BlogifyWebApp.Models.Helpers.GeneralHelper.ANONYMOUS_DEFAULT_USERNAME;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                userName = HttpContext.User.Identity.Name;
            }

            //ResultViewModel use for rendering alerts in View Details, this variable is assiged to 
            //TempData[Result] to shared in Details Action redirection.
            ResultViewModel result = new ResultViewModel();

            if (collection["BlogId"].ToString() != null && collection["Data"].ToString() != null)
            {
                if (_blogProvider.AddComment(blogId: int.Parse(collection["BlogId"]), 
                                             commentData: collection["Data"], 
                                             commentAuthor: userName))
                {
                    result.setSuccess(0, "Comment added.");
                    TempData.Put("Result", result);
                    return RedirectToAction("Details", new { id= int.Parse(collection["BlogId"]) });
                }else
                {
                    result.setErrorMessage("The comment could not be saved in the database. Contact support.");
                    TempData.Put("Result", result);
                    return RedirectToAction("Details", new { id = int.Parse(collection["BlogId"]) });
                }

            }else
            {
                result.setErrorMessage("Data is incomplete. Check your information and try again.");
                TempData.Put("Result", result);
                return RedirectToAction("Details", new { id = int.Parse(collection["BlogId"]) });
            }
            
            

        }
    }
}
