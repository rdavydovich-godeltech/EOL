using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOL
{
    public class Program
    {
        private static readonly Encoding UTF8WithoutBOM = new UTF8Encoding(false);

        public static async Task Main(string[] args)
        {
            //args = new[] { @"C:\work\Microservice.Membership" };
            var path = args.Single();

            await TransformFilesAsync(path, "*.cs");
            await TransformFilesAsync(path, "*.xml");
            await TransformFilesAsync(path, "*.md");
            await TransformFilesAsync(path, "*.json");
            await TransformFilesAsync(path, "*.cshtml");
            await TransformFilesAsync(path, "*.js");
            await TransformFilesAsync(path, "*.html");
            await TransformFilesAsync(path, "*.yml");
            await TransformFilesAsync(path, "*.ps1");
            await TransformFilesAsync(path, ".env");
            await TransformFilesAsync(path, "*.dockerfile");
            await TransformFilesAsync(path, "*.cmd");
            await TransformFilesAsync(path, "*.csproj");
            await TransformFilesAsync(path, "*.psm1");
            await TransformFilesAsync(path, "*.sql");


            Console.WriteLine("done");
        }

        private static async Task TransformFilesAsync(string path, string pattern)
        {
            var tasks = Directory
                .GetFiles(path, pattern, SearchOption.AllDirectories)
                .Select(TransformFileAsync);

            await Task.WhenAll(tasks);
        }

        private static async Task TransformFileAsync(string file)
        {
            var lines = await File.ReadAllLinesAsync(file);
            var text = string.Join("\n", lines);
            await File.WriteAllTextAsync(file, text, UTF8WithoutBOM);
            Console.WriteLine(file);
        }
    }
}
