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
        public EditorController(IBlogProvider blogProvider)
        {
            _blogProvider = blogProvider;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //
        [HttpGet]
        [Route("/Category/List")]
        public IEnumerable<ICategory> CategoryList()
        {
            return _blogProvider.ListCategories();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //
        [Route("/Blog/List")]
        [HttpGet]
        public IEnumerable<IBlog> PendingBLog_List([FromQuery] int? categoryId)
        {
            return _blogProvider.ListPendingBlogs(categoryId);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        //2021-01-14 - Kadel D. Lacatt
        //
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
        //
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
        //
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
        //
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
