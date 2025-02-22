using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace GameStore.Business.Service;

internal class FileService : IFileService
{
    public async Task<byte[]> GenerateFileBytes(Game game)
    {
        var path = $"{game.Key}.txt";
        
        using (var outputFile = new StreamWriter(path))
        {
            outputFile.WriteLine($"{game.Name}-{game.Key}: ${game.Description}");
        }

        var fileBytes = await File.ReadAllBytesAsync(path);

        File.Delete(path);

        return fileBytes;
    }
    
    public async Task<byte[]> GenerateInvoicePdfBytes(GeneratePdfInvoiceContext data)
    {
        var creationDate = DateTime.UtcNow;
        var expirationDate = creationDate.AddMonths(data.ExpireAfterMonths);
        
        using (var memoryStream = new MemoryStream())
        {
            var writerProperties = new WriterProperties().SetCompressionLevel(9);
            using (var writer = new PdfWriter(memoryStream, writerProperties))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    document.Add(new Paragraph($"User ID: {data.UserId}"));
                    document.Add(new Paragraph($"Order ID: {data.OrderId}"));
                    document.Add(new Paragraph($"Amount: {data.Sum:C}"));
                    document.Add(new Paragraph($"Creation Date: {creationDate:yyyy-MM-dd HH:mm:ss} UTC"));
                    document.Add(new Paragraph($"Expiration Date: {expirationDate:yyyy-MM-dd HH:mm:ss} UTC"));

                    document.Close();
                }
            }

            return memoryStream.ToArray();
        }
    }

}