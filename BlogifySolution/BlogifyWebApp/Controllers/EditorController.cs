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
    public class EditorController : Controller
    {
        private readonly IBlogProvider _blogProvider;

        //2021-01-13 - Kadel D. Lacatt
        //public BlogController(IBlogProvider blogProvider)
        //Controller constructor, input parameters is DI for IBlogProvider and BloProvider
        public EditorController(IBlogProvider blogProvider)
        {
            _blogProvider = blogProvider;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Index()
        //Handles GET verb requests, returns ListBlogViewModel with a list a blogs from db.
        //Renders Index View
        [Authorize(Roles = "Editor")]
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
            listBlogVM.BlogEntries = _blogProvider.ListPendingBlogs(null);

            //If this action is called by redirection from another action, checks the TempData 
            //for fill the ViewBag.Result and render alerts on the returned View
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<ResultViewModel>("Result");
            }

            return View(listBlogVM);

        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Index()
        //Handles POST verb requests, as input parameters receives a category blog id selected on UI
        //to filter blog list results. Returns ListBlogViewModel with a list a blogs from db.
        //Renders Index View
        [Authorize(Roles = "Editor")]
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
                listBlogVM.BlogEntries = _blogProvider.ListPendingBlogs(null);
            }
            else
            {
                listBlogVM.BlogEntries = _blogProvider.ListPendingBlogs(int.Parse(categoryId));
            }


            return View(listBlogVM);

        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Details()
        //Handles GET verb requests. Obtains data from one blog entry a its comments with a given blog id.
        //Returns BlogDetailViewModel blog entry information and its comments.
        //Renders Details View
        [Authorize(Roles = "Editor")]
        [HttpGet]
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

            }
            else
            {
                TempData.Put("Result", new ResultViewModel()
                {
                    Code = -999,
                    Message = "Blog entry not found."
                });

                return RedirectToAction("Index");
            }

        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Approve()
        //Handles POST verb requests for Editor/Approve. Set the blog status to APPROVED (A)
        //Input: an integer with blog Id to be modified
        //Output: on success redirects to Editor/Index action with a resultviewmodel on tempdata 
        //for rendering alerts for the user. Otherwise redirects to Editor/Details action
        //with a resultviewmodel on tempdata for rendering alerts for the user
        [Authorize(Roles = "Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve([FromForm] int id)
        {
            ResultViewModel result = new ResultViewModel();
            string userName = HttpContext.User.Identity.Name;

            try
            {
                if (id > 0)
                {
                    if (_blogProvider.SetStatusTo(id, GeneralHelper.APPROVED_STATUS, userName))
                    {
                        result.setSuccess(0, "Blog entry approved.");
                        TempData.Put("Result", result);
                        return RedirectToAction("Index", "Editor");
                    }
                    else
                    {
                        result.setErrorMessage("Blog status could not be updated. Please contact support.");
                    }
                }else
                {
                    result.setWarningMessage("Invalid blog Id. Please check your data and try again.");
                }

            }catch (Exception ex)
            {
                result.setErrorMessage(ex.Message);
            }

            TempData.Put("Result", result);
            return RedirectToAction("Details", "Editor", new { id = id });
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Delete()
        //Handles POST verb requests for Editor/Delete. Removes the blog entry from db.
        //Input: an integer with blog Id to be deleted
        //Output: on success redirects to Editor/Index action with a resultviewmodel on tempdata 
        //for rendering alerts for the user. Otherwise redirects to Editor/Details action
        //with a resultviewmodel on tempdata for rendering alerts for the user
        [Authorize(Roles = "Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ResultViewModel result = new ResultViewModel();
            string userName = HttpContext.User.Identity.Name;

            try
            {
                if (id > 0)
                {
                    if (_blogProvider.DeleteBlog(id))
                    {
                        result.setSuccess(0, "Blog entry deleted.");
                        TempData.Put("Result", result);
                        return RedirectToAction("Index", "Editor");
                    }
                    else
                    {
                        result.setErrorMessage("Blog could not be deleted. Please contact support.");
                    }
                }
                else
                {
                    result.setWarningMessage("Invalid blog Id. Please check your data and try again.");
                }

            }
            catch (Exception ex)
            {
                result.setErrorMessage(ex.Message);
            }

            TempData.Put("Result", result);
            return RedirectToAction("Details", "Editor", new { id = id });
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Delete()
        //Handles POST verb requests for Editor/Delete. Removes the blog entry from db.
        //Input: an integer with blog Id to be deleted
        //Output: on success redirects to Editor/Index action with a resultviewmodel on tempdata 
        //for rendering alerts for the user. Otherwise redirects to Editor/Details action
        //with a resultviewmodel on tempdata for rendering alerts for the user
        [Authorize(Roles = "Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromBrowse(int id)
        {
            ResultViewModel result = new ResultViewModel();
            string userName = HttpContext.User.Identity.Name;

            try
            {
                if (id > 0)
                {
                    if (_blogProvider.DeleteBlog(id))
                    {
                        result.setSuccess(0, "Blog entry deleted.");
                        TempData.Put("Result", result);
                        return RedirectToAction("Index", "Blog");
                    }
                    else
                    {
                        result.setErrorMessage("Blog could not be deleted. This blog entry already have comments from users." +
                                               "Check the information, try again or contact support.");
                    }
                }
                else
                {
                    result.setWarningMessage("Invalid blog Id. Please check your data and try again.");
                }

            }
            catch (Exception ex)
            {
                result.setErrorMessage(ex.Message);
            }

            TempData.Put("Result", result);
            return RedirectToAction("Details", "Blog", new { id = id });
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //public ActionResult Reject()
        //Handles POST verb requests for Editor/Reject. Set the blog status to REJECTED (R)
        //Input: an integer with blog Id to be modified
        //Output: on success redirects to Editor/Index action with a resultviewmodel on tempdata 
        //for rendering alerts for the user. Otherwise redirects to Editor/Details action
        //with a resultviewmodel on tempdata for rendering alerts for the user
        [Authorize(Roles = "Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reject(int id)
        {
            ResultViewModel result = new ResultViewModel();
            string userName = HttpContext.User.Identity.Name;

            try
            {
                if (id > 0)
                {
                    if (_blogProvider.SetStatusTo(id, GeneralHelper.REJECTED_STATUS, userName))
                    {
                        result.setSuccess(0, "Blog entry rejected.");
                        TempData.Put("Result", result);
                        return RedirectToAction("Index", "Editor");
                    }
                    else
                    {
                        result.setErrorMessage("Blog status could not be updated. Please contact support.");
                    }
                }
                else
                {
                    result.setWarningMessage("Invalid blog Id. Please check your data and try again.");
                }

            }
            catch (Exception ex)
            {
                result.setErrorMessage(ex.Message);
            }

            TempData.Put("Result", result);
            return RedirectToAction("Details", "Editor", new { id = id });
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

    }
}
