﻿using Microsoft.AspNetCore.Http;
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
            listBlogVM.BlogEntries = _blogProvider.ListBlogs((categoryId == "" ? null : int.Parse(categoryId)));

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


        [Authorize]
        [HttpGet]
        // GET: BlogController/Create
        public ActionResult Create()
        {


            var sli = new List<SelectListItem>();
            sli.Add(new SelectListItem { Text = "[CATEGORY]", Value = "", Selected = false });

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

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
