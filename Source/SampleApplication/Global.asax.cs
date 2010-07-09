﻿using System.Web.Mvc;
using System.Web.Routing;
using SampleApplication.Controllers;
using Snooze.Routing;

namespace SampleApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            // Snooze provides some handy features...
            routes.AddVersionedStaticFilesSupport();
            routes.AddIE6Support();

            routes.Map<HomeUrl>(h => "");
            routes.Map<BooksUrl>(b => "books");
            routes.Map<BookUrl>(b => "book/" + b.BookId);
            routes.Map<BookCommentsUrl>(c => "comments");
            routes.Map<BookCommentUrl>(c => c.CommentId.ToString());
            routes.Map<PartialItemUrl>(c => "partial/item" + c.Something);
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}