using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiChat3.Models
{
    public class Participant
    {
       
        public string NomUtilisateur { get; set; }
        public string PrenomUtilisateur { get; set; }
        public string PseudoUtilisateur { get; set; }
        public string EmailUtilisateur { get; set; }
        public Nullable<int> IdAvatar { get; set; }
        public int IdAcces { get; set; }
        public bool Verif { get; set; }
       
    }
}