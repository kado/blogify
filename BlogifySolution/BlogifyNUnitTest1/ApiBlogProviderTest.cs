using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;

using BlogifyWebApi.Models;
using BlogifyWebApi.Models.Providers;
using BlogifyWebApi.Models.Interfaces;
using BlogifyWebApi.Models.EF;

namespace BlogifyNUnitTest1
{
    class ApiBlogProviderTest
    {
        private IBlogProvider blogProvider;
        private Logger<BlogProvider> logger;

        [SetUp]
        public void Setup()
        {
            logger = new Logger<BlogProvider>(new LoggerFactory());
            blogProvider = new BlogProvider(logger);
        }

        [Test]
        public void Test_ListCategories()
        {
            List<Category> result = (List<Category>)blogProvider.ListCategories();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count >= 0);

        }

        [Test]
        public void Test_ListBlogs()
        {
            List<Blog> result;
            result = (List<Blog>)blogProvider.ListBlogs(null);
            Assert.IsTrue(result.Count >= 0);

            result = (List<Blog>)blogProvider.ListBlogs(4444);
            Assert.IsTrue(result.Count >= 0);
        }

        [Test]
        public void Test_ListPendingBlogs()
        {
            var result = (List<Blog>)blogProvider.ListPendingBlogs(null);
            Assert.IsTrue(result.Count >= 0);

            result = (List<Blog>)blogProvider.ListPendingBlogs(4444);
            Assert.IsTrue(result.Count >= 0);
        }

        [Test]
        public void Test_AddEmptyBlog()
        {
            Blog blog = new Blog();
            var result = blogProvider.AddBlog(blog);

            Assert.IsFalse(result);
        }

        [Test]
        public void Test_AddNullBlog()
        {
            var result = blogProvider.AddBlog(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_NonExisting_GetBlog()
        {

            var result = blogProvider.GetBlog(-1);
            Assert.IsNull(result);

        }

        [Test]
        public void Test_NonExisting_GetPendingBlog()
        {

            var result = blogProvider.GetPendingBlog(-1);
            Assert.IsNull(result);

        }

        [Test]
        public void Test_NonExisting_GetBlogForEdition()
        {

            var result = blogProvider.GetBlog(-1);
            Assert.IsNull(result);

        }

        [Test]
        public void Test_SaveNullBlog()
        {
            var result = blogProvider.SaveBlog(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_SaveEmptyBlog()
        {

            Blog blog = new Blog();
            var result = blogProvider.SaveBlog(blog);
            Assert.IsFalse(result);

        }

        [Test]
        public void Test_DeleteNonExistingBlog()
        {

            var result = blogProvider.DeleteBlog(-1);
            Assert.IsFalse(result);

        }

        [Test]
        public void Test_AddCommentToNonExistingBlog()
        {

            var result = blogProvider.AddComment(-1, "Test comment.", "Anonymous");
            Assert.IsFalse(result);

        }

        [Test]
        public void Test_ListComments()
        {

            var result = (List<Comment>)blogProvider.ListComments(-1);
            Assert.IsTrue(result.Count >= 0);

            result = (List<Comment>)blogProvider.ListComments(1);
            Assert.IsTrue(result.Count >= 0);

        }

        [Test]
        public void Test_ListMyBlogs()
        {
            var result = (List<Blog>)blogProvider.ListMyBlogs(null, null);
            Assert.IsNull(result);

            result = (List<Blog>)blogProvider.ListMyBlogs("", 4444);
            Assert.IsNull(result);

            result = (List<Blog>)blogProvider.ListMyBlogs("blogger1", null);
            Assert.IsTrue(result.Count >= 0);
        }

        [Test]
        public void Test_SetStatusTo()
        {
            //Set status to non existing blog.
            var result = blogProvider.SetStatusTo(-1, "P", "");
            Assert.IsFalse(result, "Failed SetStatusTo non existing blog.");

            //Set status to non existing blog. With invalid status code and empty username
            result = blogProvider.SetStatusTo(1, "X", "");
            Assert.IsFalse(result, "Failed SetStatusTo existing blog. With invalid status code and empty username.");

            result = blogProvider.SetStatusTo(19, "A", "klacatt");
            Assert.IsTrue(result, "Failed SetStatusTo existing blog. With valid status code and valid username.");

        }
    }
}
