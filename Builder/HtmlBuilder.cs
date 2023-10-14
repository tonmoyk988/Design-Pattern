using System.Text;

namespace GiniPig.Design_Patterns.Builder
{
    public class HtmlElement 
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public List<HtmlElement> Childrens { get; set;} = new();
        private readonly int IndentSize = 2;

        public HtmlElement(string name, string text) 
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }
        
        private string ToStringImpl(int indent)
        {
            var builder = new StringBuilder();
            var indentation = new string(' ', IndentSize * indent);
            
            builder.AppendLine($"{indentation}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                builder.Append(new string(' ', IndentSize * (indent+1)));
                builder.AppendLine($"{Text}");
            }

            foreach (var element in Childrens) 
            {
                builder.AppendLine($"{element.ToStringImpl(indent+1)}");
            }

            builder.Append($"{indentation}</{Name}>");
            return builder.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    internal class HtmlBuilder
    {
        private HtmlElement root { get; set; }
        
        public HtmlBuilder(string rootName, string rootText) 
        {
            root = new HtmlElement(rootName, rootText);
        }

        public HtmlBuilder AddChildren(string childName, string childText) 
        {
            root.Childrens.Add(new HtmlElement(childName, childText));
            return this;
        }

        public string Build()
        {
            return root.ToString();
        }
    }
}
