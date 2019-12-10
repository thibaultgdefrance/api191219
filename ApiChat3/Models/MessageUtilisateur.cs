using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiChat3.Models
{
    public class MessageUtilisateur
    {
        public int IdMessage { get; set; }
        public int IdUtilisateur { get; set; }
        public System.DateTime DateEnvoi { get; set; }
        public string TexteMessage { get; set; }
        public int IdDiscussion { get; set; }
        public int IdTon { get; set; }
        public byte StatutMessage { get; set; }
        public string PseudoUtilisateur { get; set; }
        public string TitreDiscussion { get; set; }
        public bool VerifMessage { get; set; } 
    }
}