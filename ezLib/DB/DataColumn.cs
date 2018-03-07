namespace easyLib.DB
{
    public enum ColumnDataType_t
    {
        Text,
        Integer,
        Float,
        Time,
        Version
    }


    public interface IDataColumn
    {
        string Caption { get; }
        ColumnDataType_t DataType { get; }
    }


    public class TextColumn: IDataColumn
    {
        public TextColumn(string caption)
        {
            Caption = caption;
            DataType = ColumnDataType_t.Text;
        }


        public string Caption { get; private set; }
        public ColumnDataType_t DataType { get; private set; }
    }



    public class IntegerColumn: IDataColumn
    {
        public IntegerColumn(string caption)
        {
            Caption = caption;
            DataType = ColumnDataType_t.Integer;
        }


        public string Caption { get; private set; }
        public ColumnDataType_t DataType { get; private set; }
    }


    public class FloatColumn: IDataColumn
    {
        public FloatColumn(string caption)
        {
            Caption = caption;
            DataType = ColumnDataType_t.Float;
        }


        public string Caption { get; private set; }
        public ColumnDataType_t DataType { get; private set; }
    }



    public class TimeColumn: IDataColumn
    {
        public TimeColumn(string caption)
        {
            Caption = caption;
            DataType = ColumnDataType_t.Time;
        }

        public string Caption { get; private set; }
        public ColumnDataType_t DataType { get; private set; }
    }

    public class VersionColumn: IDataColumn
    {
        public VersionColumn(string caption)
        {
            Caption = caption;
            DataType = ColumnDataType_t.Version;
        }

        public string Caption { get; private set; }
        public ColumnDataType_t DataType { get; private set; }

    }
}
