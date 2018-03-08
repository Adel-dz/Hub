namespace DGD.HubCore
{
    public static class TablesID
    {
        public const uint SHARED_TEXT = 50;
        public const uint COUNTRY = 100;
        public const uint PRODUCT = 200;
        public const uint UNIT = 300;
        public const uint INCOTERM = 350;
        public const uint CURRENCY = 400;
        public const uint PLACE = 500;
        public const uint SUPPLIER = 600;
        public const uint VALUE_CONTEXT = 700;
        public const uint SPOT_VALUE = 800;
        public const uint FILE_GENERATION = 900;
    }

    public static class HResultValue
    {
        public const int COR_E_FILENOTFOUND = unchecked((int)0x80070003);
        public const int ERROR_FILE_NOT_FOUND = unchecked((int)0x02);
    } 
}
