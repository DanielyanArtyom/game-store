namespace GameStore.Mongo.Data.Context.Entity;

public abstract class BaseEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
}