using MongoDB.Bson;
using MongoDB.Driver;
using RestApi.Infrastructure.Mongo;
using RestApi.Mappers;
using RestApi.Models;

namespace RestApi.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMongoCollection<GroupEntity> _groups;

        public GroupRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            var database = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDb:Groups:DatabaseName"));
            _groups = database.GetCollection<GroupEntity>(configuration.GetValue<string>("MongoDb:Groups:CollectionName"));
        }

        public async Task<GroupModel> CreateAsync(string name, Guid[] users, CancellationToken cancellationToken)
        {
            var group = new GroupEntity
            {
                Name = name,
                Users = users,
                CreatedAt = DateTime.UtcNow,
                Id = ObjectId.GenerateNewId().ToString()
            };
            await _groups.InsertOneAsync(group, new InsertOneOptions(), cancellationToken);
            return group.ToModel();
        }

        public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<GroupEntity>.Filter.Eq(s => s.Id, id);
            await _groups.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<GroupModel> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<GroupEntity>.Filter.Eq(x => x.Id, Id);
                var group = await _groups.Find(filter).FirstOrDefaultAsync(cancellationToken);
                return group?.ToModel();
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<GroupModel>> GetByNameAsync(string name, CancellationToken cancellationToken) // Búsqueda por coincidencia parcial
        {
            var filter = Builders<GroupEntity>.Filter.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
            var groups = await _groups.Find(filter).ToListAsync(cancellationToken);
            return groups.Select(group => group.ToModel());
        }

       
        
    }
}
