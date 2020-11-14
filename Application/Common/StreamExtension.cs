using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Common
{
    public static class StreamExtension
    {
        public static async Task<string> ReadAsStringAsync(this IFormFile file)
        {
            var result = new StringBuilder();
            using var reader = new StreamReader(file.OpenReadStream());
            while (reader.Peek() >= 0)
                result.AppendLine(await reader.ReadLineAsync());
            return result.ToString();
        }
    }
}