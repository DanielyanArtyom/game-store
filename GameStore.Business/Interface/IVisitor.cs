namespace GameStore.Business.Interface;

public interface IVisitor
{
    void Visit(dynamic request);
}