using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using BlogifyWebApi.Models;
using BlogifyWebApi.Models.Interfaces;
using BlogifyWebApi.Models.Providers;
using BlogifyWebApi.Models.Helpers;


namespace BlogifyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorController : ControllerBase
    {
        private readonly IBlogProvider _blogProvider;

        //2021-01-14 - Kadel D. Lacatt
        //EditorController constructor. Input parameter IBlogProvider receves an instance
        //of the BlogProvider class for DI.
        //Base URL: http://domainname/api/Editor/
        public EditorController(IBlogProvider blogProvider)
        {
            _blogProvider = blogProvider;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //GET - Returns a list of the blog categories stored on the db
        //Output Type: IEnumerable<ICategory>
        [HttpGet]
        [Route("/Category/List")]
        public IEnumerable<ICategory> CategoryList()
        {
            return _blogProvider.ListCategories();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //GET - Returns a list of blog entries with pending for publishing status.
        //IEnumerable<IBlog>
        [Route("/Blog/List")]
        [HttpGet]
        public IEnumerable<IBlog> PendingBLog_List([FromQuery] int? categoryId)
        {
            return _blogProvider.ListPendingBlogs(categoryId);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //GET - Returns a blog entry with pending for publishing status
        //Input Parameter: integer holding the blog id
        //OutPut: Implements IActionResult returning 200 status code with IBlog instance in json format
        //if the blog is not found or has not a valid type, then returns 400 Not found response.
        [Route("/Blog/{id}")]
        [HttpGet]
        public IActionResult GetBlog(int id) 
        {
            IBlog blog = _blogProvider.GetPendingBlog(id); 
            if (blog != null)
            {
                return Ok(blog);
            }else
            {
                return NotFound();

            }
            
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //PUT - Set the status of a blog to APPROVED (A)
        //Input Parameter: integer holding the blog id from the path.
        //                 string with the editor username (always klacatt) in request body 
        //OutPut: Implements IActionResult returning 200 status code success
        //returns  Not found if the blog is not found.
        //returns  Bad Request if the blog id is invalid or if an exception is thrown.
        [HttpPut]
        [Route("/Blog/Approve/{id}")]
        public IActionResult Approve(int id, [FromBody]string userName)
        {

            try
            {
                
                if (id > 0)
                {
                    if (_blogProvider.SetStatusTo(id, GeneralHelper.APPROVED_STATUS, userName))
                    {
                        return Ok();
                    }else
                    {
                        return NotFound();
                    }
                }else
                {
                    return BadRequest();
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //DELETE - Deletes the blog entry with the given id.
        //Input Parameter: integer holding the blog id from the path.
        //OutPut: Implements IActionResult returning 200 status code on success
        //returns  Not found if the blog is not found.
        //returns  Bad Request if the blog id is invalid or if an exception is thrown.
        [HttpDelete]
        [Route("/Blog/Delete/{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                if (id > 0)
                {
                    if (_blogProvider.DeleteBlog(id))
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //PUT - Set the status of a blog to REJECTED (R)
        //Input Parameter: integer holding the blog id from the path.
        //                 string with the editor username (always klacatt) in request body 
        //OutPut: Implements IActionResult returning 200 status code success
        //returns  Not found if the blog is not found.
        //returns  Bad Request if the blog id is invalid or if an exception is thrown.
        [HttpPut]
        [Route("/Blog/Reject/{id}")]
        public IActionResult Reject(int id, [FromBody]string userName)
        {

            try
            {
                if (id > 0)
                {
                    if (_blogProvider.SetStatusTo(id, GeneralHelper.REJECTED_STATUS, userName))
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        //-------------------------------------------------------------------------------------------------------------------------------------

    }
}
