//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiChat3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UtilisateurDiscussion
    {
        public int IdUtilisateurDiscussion { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdDiscussion { get; set; }
        public int IdNiveau { get; set; }
    
        public virtual Discussion Discussion { get; set; }
        public virtual Niveau Niveau { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
    }
}
