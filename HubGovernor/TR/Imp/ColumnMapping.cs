namespace DGD.HubGovernor.TR.Imp
{
    enum ColumnKey_t
    {
        SessionNber,
        SubHeading,
        LabelFr,
        LabelUs,        
        Price,
        Currency,
        Incoterm,
        Place,
        Date,
        Unit,
        Origin,
        ProductNber
    }


    sealed class ColumnMapping
    {
        public const int NULL_NDX = -1;
        public const int COLUMNS_COUNT = 12;


        public ColumnMapping(ColumnKey_t key , string caption)
        {
            Text = caption;
            Key = key;
            DSVIndex = NULL_NDX;
        }


        public string Text { get; }
        public ColumnKey_t Key { get; }
        public int DSVIndex { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
