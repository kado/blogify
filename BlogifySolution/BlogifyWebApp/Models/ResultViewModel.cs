using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Models
{
    public class ResultViewModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public ResultViewModel()
        {
            this.Message = "";
            this.Code = -1;
        }

        public Dictionary<string, string> Tipos = new Dictionary<string, string>() {
            {"warning","alert-warning"},
            {"danger","alert-danger"},
            {"success","alert-success"},
            {"info","alert-info"}
        };

        public void setErrorMessage(string customMessage)
        {
            this.Code = -2;
            this.Type = this.Tipos["danger"];
            this.Title = "Oops! Something went wrong";
            this.Message = "An error has occured. " + customMessage;
        }

        public void setWarningMessage(string customMessage = "")
        {
            this.Code = -3;
            this.Type = this.Tipos["warning"];
            this.Title = "Warning!";
            this.Message = "This requires your attention. " + customMessage;
        }

        public void setSuccess(int customCode = 0, string customMessage = "")
        {
            this.Code = customCode;
            this.Type = this.Tipos["success"];
            this.Title = "Done";
            this.Message = "Operation has been successful. " + customMessage;
        }
        public void setInfo(string customMessage, int customCode = 0)
        {
            this.Code = customCode;
            this.Type = this.Tipos["info"];
            this.Title = "Information";
            this.Message = customMessage;
        }
    }
}
