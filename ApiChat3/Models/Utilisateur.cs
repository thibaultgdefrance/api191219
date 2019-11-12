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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Utilisateur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilisateur()
        {
            this.Maintenance = new HashSet<Maintenance>();
            this.Message = new HashSet<Message>();
            this.Notification = new HashSet<Notification>();
            this.Notification1 = new HashSet<Notification>();
            this.UtilisateurDiscussion = new HashSet<UtilisateurDiscussion>();
        }
    
        public int IdUtilisateur { get; set; }
        public string NomUtilisateur { get; set; }
        public string PrenomUtilisateur { get; set; }
        public string PseudoUtilisateur { get; set; }
        public string EmailUtilisateur { get; set; }
        public System.DateTime DateDeNaissanceUtilisateur { get; set; }
        public string NumeroUtilisateur { get; set; }
        public string MotDePasseUtilisateur { get; set; }
        public System.DateTime DateCreationUtilisateur { get; set; }
        public Nullable<int> IdAvatar { get; set; }
        public int IdAcces { get; set; }
        public Nullable<int> IdStatutUtilisateur { get; set; }
        public string TokenUtilisateur { get; set; }
        [JsonIgnore]
        public virtual Acces Acces { get; set; }
        [JsonIgnore]
        public virtual Avatar Avatar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Maintenance> Maintenance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Message> Message { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Notification> Notification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Notification> Notification1 { get; set; }
        [JsonIgnore]
        public virtual StatutUtilisateur StatutUtilisateur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<UtilisateurDiscussion> UtilisateurDiscussion { get; set; }
    }
}