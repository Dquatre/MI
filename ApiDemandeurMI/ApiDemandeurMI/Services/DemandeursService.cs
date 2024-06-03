using ApiDemandeurMI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDemandeurMI.Services
{
    public class DemandeursService
    {
        private readonly IMongoCollection<Demandeur> _DemandeursCollection;

        public DemandeursService(IOptions<DemandeurMISettings> DemandeurMISettings) {

            var mongoClient = new MongoClient(DemandeurMISettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DemandeurMISettings.Value.DatabaseName);

            _DemandeursCollection = mongoDatabase.GetCollection<Demandeur>(
                DemandeurMISettings.Value.CollectionName);

        }
       
        public async Task<List<Demandeur>> GetAsync() =>
            await _DemandeursCollection.Find(_ => true).ToListAsync();

        public async Task<Demandeur?> GetAsync(int id) =>
            await _DemandeursCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Demandeur newDemandeur) =>
            await _DemandeursCollection.InsertOneAsync(newDemandeur);

        public async Task UpdateAsync(int id, Demandeur updatedDemandeur) =>
            await _DemandeursCollection.ReplaceOneAsync(x => x.Id == id, updatedDemandeur);

        public async Task RemoveAsync(int id) =>
            await _DemandeursCollection.DeleteOneAsync(x => x.Id == id);

        public Demandeur Authenticate(string login, string password)
        {
            Demandeur demandeurFind = _DemandeursCollection.Find(d => d.Login.ToUpper().Equals(login.ToUpper())).FirstOrDefault();
            if (PasswordHashService.CompareHash(demandeurFind.Password, password))
            {
                return demandeurFind;
            }
            return null;
        }

        public string GererateToken(string secret, List<Claim> claims) 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),                                                   
                //the token expire in 60 minutes
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
