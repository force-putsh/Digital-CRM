/* The namespace of the class. */
namespace Crm.Models
{
    /* This class is used to represent a user in the database */
    public partial class ApplicationUser
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string PhotoProfil { get; set; }
    }
}
