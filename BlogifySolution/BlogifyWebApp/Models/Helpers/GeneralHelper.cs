﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Models.Helpers
{
    public class GeneralHelper
    {

        public static string APPROVED_STATUS = "A";
        public static string PENDING_STATUS = "P";
        public static string REJECTED_STATUS = "R";
        public static string ANONYMOUS_DEFAULT_USERNAME = "Anonymous";
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
