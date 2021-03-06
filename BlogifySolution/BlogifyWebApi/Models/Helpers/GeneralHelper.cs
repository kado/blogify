﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApi.Models.Helpers
{
    //2021-01-14 - Kadel D. Lacatt
    //General class for common tasks, filled with static methods and properties
    //used in the entire web app
    public class GeneralHelper
    {

        public static string APPROVED_STATUS = "A";
        public static string PENDING_STATUS = "P";
        public static string REJECTED_STATUS = "R";
        public static string ANONYMOUS_DEFAULT_USERNAME = "Anonymous";

        public static string WRITER_ROLENAME = "Writer";
        public static string EDITOR_ROLENAME = "Editor";

        public static Dictionary<string, string> STATUSNAMES = new Dictionary<string, string>() {
            {"A","APPROVED"},
            {"P","PENDING"},
            {"R","REJECTED"}
        };
        public static string GetMessageFromException(string Origin, Exception pEx)
        {


            string msg = Origin + pEx.Message;
            if (pEx.InnerException != null)
            {
                msg = msg + pEx.InnerException.Message;
            }

            return msg;


        }
    }
}
