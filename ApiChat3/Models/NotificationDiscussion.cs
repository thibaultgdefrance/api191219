using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiChat3.Models
{
    public class NotificationDiscussion
    {
        public int IdNotification { get; set; }
        public string EmailCreateur { get; set; }
        public string TitreDiscussion { get; set; }
        public string TokenNotification { get; set; }
    }
}