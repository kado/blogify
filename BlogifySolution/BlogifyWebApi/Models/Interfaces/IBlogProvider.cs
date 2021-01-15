using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using BlogifyWebApi.Models.Helpers;


namespace BlogifyWebApi.Models.Interfaces
{
    //2021-01-14 - Kadel D. Lacatt
    //Interface for abstracting BlogProvider class and use DI
    public interface IBlogProvider
    {

        public IEnumerable<ICategory> ListCategories();
        public IEnumerable<IBlog> ListBlogs(int? blogCategory);
        public IEnumerable<IBlog> ListPendingBlogs(int? blogCategory);
        public bool AddBlog(IBlog blog);
        public bool AddBlog(int blogCategory, string blogTitle, string blogData, string autorUser);
        public IBlog GetBlog(int blogId);
        public IBlog GetPendingBlog(int blogId);
        public IBlog GetBlogForEdition(int blogId, string userName);
        public bool SaveBlog(IBlog blog);
        public bool DeleteBlog(int blogId);
        public bool AddComment(int blogId, string commentData, string commentAuthor);
        public IEnumerable<IComment> ListComments(int blogId);
        public IEnumerable<IBlog> ListMyBlogs(string username, int? blogCategory);
        public bool SetStatusTo(int blogId, string status, string userName);


        
    }
}
