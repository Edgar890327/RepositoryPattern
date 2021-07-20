using MongoDB.Bson;
using System;

namespace Repository.MongoRepository
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
