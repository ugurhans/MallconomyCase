using MongoDB.Bson;

namespace Core.Entities.Concrate
{
    public class OperationClaim : IEntity
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}