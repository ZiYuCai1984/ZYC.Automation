using System.Text;
using ZYC.Automation.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.About.UI;

[Register]
internal partial class AboutMarkdownView
{
    public AboutMarkdownView(IProduct product)
    {
        var markdown = new StringBuilder();

        markdown.AppendLine($"## 📦{nameof(IProduct.PackageId)}");
        markdown.AppendLine("");
        markdown.AppendLine(product.PackageId);
        markdown.AppendLine("");

        markdown.AppendLine($"## 🧩 {nameof(IProduct.Version)}");
        markdown.AppendLine("");
        markdown.AppendLine(product.Version);
        markdown.AppendLine("");

        markdown.AppendLine($"## 👤 {nameof(IProduct.Author)}");
        markdown.AppendLine("");
        markdown.AppendLine(product.Author);
        markdown.AppendLine("");

        markdown.AppendLine($"## 📝 {nameof(IProduct.Description)}");
        markdown.AppendLine("");
        markdown.AppendLine(product.Description);
        markdown.AppendLine("");

        markdown.AppendLine($"## ©️ {nameof(IProduct.Copyright)}");
        markdown.AppendLine("");
        markdown.AppendLine(product.Copyright);
        markdown.AppendLine("");

        MarkdownScrollViewer.Markdown = markdown.ToString();
    }
}