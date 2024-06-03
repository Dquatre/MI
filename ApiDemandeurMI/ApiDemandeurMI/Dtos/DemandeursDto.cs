using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;

namespace ApiDemandeurMI.Dtos
{
    public class DemandeursDto
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string LienCV { get; set; }
    }



    public class DemandeursDtoin
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string LienCV { get; set; }
    }


}
