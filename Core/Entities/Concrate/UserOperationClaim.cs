using MongoDB.Bson;

namespace Core.Entities.Concrate
{
    public class UserOperationClaim : IEntity
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string OperationClaimId { get; set; }

    }
}