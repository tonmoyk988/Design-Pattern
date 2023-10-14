public class CsvBuilder
    {
        private int _totalColumn = 0;
        private int _dataCountInCurrentRow = 0;
        private readonly StringBuilder _csvBuilder;
        private readonly StringBuilder _rowBuilder;
        public CsvBuilder()
        {
            _csvBuilder = new StringBuilder();
            _rowBuilder = new StringBuilder();
        }

        public CsvBuilder AddColumns(List<string> columnNames)
        {
            var commaSeperatedColumns = "\"" + string.Join("\",\"", columnNames) + "\"";
            _csvBuilder.AppendLine(commaSeperatedColumns);
            SetTotalNoOfColumns(columnNames.Count);
            return this;
        }

        public CsvBuilder AddNewRow()
        {
            Reset();
            return this;
        }

        public CsvBuilder InsertData(string data)
        {
            if (_dataCountInCurrentRow >= _totalColumn)
            {
                return this;
            }

            _rowBuilder.Append($@"""{data}"",");
            _dataCountInCurrentRow++;

            if (_dataCountInCurrentRow == _totalColumn)
            {
                AppendRow();
            }
            return this;
        }

        public string Build()
        {
            return _csvBuilder.ToString();
        }

        private void Reset()
        {
            _dataCountInCurrentRow = 0;
            _rowBuilder.Clear();
        }

        private void AppendRow()
        {
            _csvBuilder.AppendLine(_rowBuilder.ToString());
        }

        private void SetTotalNoOfColumns(int totalColumn)
        {
            _totalColumn = totalColumn;
        }
    }