using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiChat3.Models
{
    public class Contact
    {
        
        public string NomContact { get; set; }
        public System.DateTime DateCreationDiscussion { get; set; }
       
        public Nullable<byte> StatutDiscussion { get; set; }
        
        public int IdStatutDiscussion { get; set; }
        public string TokenDiscussion { get; set; }
        
    }
}