using DocumentFormat.OpenXml.Packaging;
using System.Text;

namespace RAGWebApp.Utils
{
    public static class DocumentLoader
    {
        public static string LoadText(string path)
        {
            var ext = Path.GetExtension(path).ToLower();

            return ext switch
            {
                ".txt" => File.ReadAllText(path),
                ".pdf" => LoadPdf(path),
                ".docx" => LoadDocx(path),
                _ => ""
            };
        }

        private static string LoadPdf(string path)
        {
            using var doc = UglyToad.PdfPig.PdfDocument.Open(path);
            var sb = new StringBuilder();

            foreach (var page in doc.GetPages())
                sb.AppendLine(page.Text);

            return sb.ToString();
        }

        private static string LoadDocx(string path)
        {
            using var doc = WordprocessingDocument.Open(path, false);
            return doc.MainDocumentPart.Document.Body.InnerText;
        }
    }
}
