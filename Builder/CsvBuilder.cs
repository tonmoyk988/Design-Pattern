
using System.Text;

namespace GiniPig
{
    public interface ICsvBuilder
    {
        IInsertData AddNewRow();
        string Build();
    }

    public interface IInsertData 
    {
        IInsertData InsertRowData(string data);
        ICsvBuilder AppendRow();
    }

    public class CsvBuilder
    {
        private sealed class CsvBuilderImpl : IInsertData, ICsvBuilder
        {
            private readonly StringBuilder _csvBuilder;
            private readonly StringBuilder _rowBuilder;

            public CsvBuilderImpl()
            {
                _csvBuilder = new StringBuilder();
                _rowBuilder = new StringBuilder();
            }

            public string Build()
            {
                return _csvBuilder.ToString();
            }

            public IInsertData AddNewRow()
            {
                Reset();
                return this;
            }

            public IInsertData InsertRowData(string data)
            {
                _rowBuilder.Append($@"""{data}"",");
                return this;
            }

            public ICsvBuilder AddColumns(List<string> columnNames)
            {
                var commaSeperatedColumns = "\"" + string.Join("\",\"", columnNames) + "\"";
                _csvBuilder.AppendLine(commaSeperatedColumns);
                return this;
            }
            
            public ICsvBuilder AppendRow()
            {
                _csvBuilder.AppendLine(_rowBuilder.ToString());
                Reset();
                return this;
            }

            private void Reset()
            {
                _rowBuilder.Clear();
            }
        }

        public static ICsvBuilder BuildWithColumns(List<string> columnNames)
        {
            return new CsvBuilderImpl().AddColumns(columnNames);
        }
    }
}
