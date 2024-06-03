using MongoDB.Bson.Serialization.Attributes;

namespace ApiDemandeurMI.Models
{
    public class Demandeur
    {
        [BsonElement("_id")]
        //[BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [BsonElement("login")]
        public string Login { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("surname")]
        public string Surname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("lienCV")]
        public string LienCV { get; set; }
    }
}
