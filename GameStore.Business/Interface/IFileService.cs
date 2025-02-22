namespace GameStore.Business.Interface;

public interface IFileService
{
    Task<byte[]> GenerateFileBytes(Game game);
    Task<byte[]> GenerateInvoicePdfBytes(GeneratePdfInvoiceContext data);
}