﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using BlogifyWebApp.Models.EF;
using BlogifyWebApp.Models.Helpers;
using BlogifyWebApp.Models.Interfaces;

namespace BlogifyWebApp.Models.Providers
{
    //2020-01-13 - Kadel D. Lacatt
    //This class provides functions for CRUD of the Blog entities.
    public class BlogProvider: IBlogProvider
    {
        
        private DBBlogifyContext db;
        ILogger<BlogProvider> _logger;

        public BlogProvider(ILogger<BlogProvider> logger)
        {

            //Initialize the logger
            _logger = logger;

            //Initialize the dbcontext
            if (db==null)
            {
                db = new DBBlogifyContext();
            }
        }

        //2020-01-13 - Kadel D. Lacatt
        //Retrieve a list of blog categories stored on the db. Output: An IEnumerable of ICategory
        public IEnumerable<ICategory> ListCategories()
        {

            try
            {

                return db.Categories.OrderBy(c => c.Name).ToList();

            }catch(Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));

                return null;
            }

        }

        //2020-01-13 - Kadel D. Lacatt
        //Retrieve a list of approved blog entries stored on the db. Input parameters is a nullable int category blog Id for filtering. 
        //Output: An IEnumerable of IBlog. 
        public IEnumerable<IBlog> ListBlogs(int? blogCategory)
        {
            try
            {
                if (blogCategory != null)
                {
                    return db.Blogs.Where(b => b.Category == blogCategory && 
                                               b.Status == GeneralHelper.APPROVED_STATUS)
                                   .OrderBy(b => b.Created)
                                   .ToList();
                }
                else
                {
                    return db.Blogs.Where(b => b.Status == GeneralHelper.APPROVED_STATUS)
                                   .OrderBy(b => b.Created)
                                   .ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return null;
            }
        }

        //2020-01-13 - Kadel D. Lacatt
        //Inserts in the db a new blog entry. Input IBlog with the new blog entry data. 
        //Output: Boolean with true if the transaction is successful, returns false otherwise. 
        bool IBlogProvider.AddBlog(IBlog pBlog)
        {
            
            try
            {
                db.Blogs.Add((Blog) pBlog);
                db.SaveChanges();
                
                return true;
            }catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return false;
            }

        }

        //2020-01-13 - Kadel D. Lacatt
        //Retrieve a blog entry from the db. Input int parameter with blog Id. 
        //Output: IBlog with the data of the required Blog.
        IBlog IBlogProvider.GetBlog(int blogId)
        {

            try
            {

                return db.Blogs.Where(b => b.Id == blogId).SingleOrDefault();

            }catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return null;
            }
            
        }

        //2020-01-13 - Kadel D. Lacatt
        //Updates a blog entry in the db. Input IBlog objecto with updated data of the blog.
        //Output: Boolean with true if transaction was a success, returns false otherwise.
        bool IBlogProvider.SaveBlog(IBlog blog)
        {
            try
            {
                Blog dbBlog = db.Blogs.Where(b => b.Id == blog.Id).SingleOrDefault();

                if (dbBlog != null)
                {
                    dbBlog.Title = blog.Title;
                    dbBlog.Data = blog.Data;
                    dbBlog.Created = System.DateTime.Now;
                    dbBlog.Category = blog.Category;

                    db.SaveChanges();
                    
                    return true;

                }
                else {
                    throw new Exception("Blog entry does not exist. Please check your data and try again or contact support.");
                }
                
            }catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return false;
            }
        }

        //2020-01-13 - Kadel D. Lacatt
        //Deletes a Blog from the db. Input: integer with the blog Id.
        //Output: Boolean with true if transaction was a success, returns false otherwise.
        bool IBlogProvider.DeleteBlog(int blogId)
        {
            try
            {
                IBlog blog = db.Blogs.Where(b => b.Id == blogId).SingleOrDefault();
                if (blog != null)
                {
                    db.Blogs.Remove((Blog) blog);
                    db.SaveChanges();
                    return true;
                }else
                {
                    throw new Exception("Blog entry does not exist. Please check your data and try again or contact support.");
                }

            }catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return false;
            }
        }

        //2020-01-13 - Kadel D. Lacatt
        //Adds a comment to a Blog entry in db. Input: IComment with the data to be stored.
        //Output: Boolean with true if transaction was a success, returns false otherwise.
        bool IBlogProvider.AddComment(int blogId, string commentData, string commentAuthor)
        {
            try
            {
                                
                db.Comments.Add( new Comment { 
                    BlogId= blogId, 
                    Author= commentAuthor, 
                    Created= System.DateTime.Now,
                    Data = commentData
                });
                db.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return false;
            }
        }

        IEnumerable<IComment> IBlogProvider.ListComments(int blogId)
        {
            try
            {
                return  db.Comments.Where(c => c.BlogId == blogId)
                                   .OrderByDescending(c => c.Created)
                                   .ToList();
            }catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return null;
            }

        }
        
    }
}
