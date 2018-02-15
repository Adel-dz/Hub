namespace DGD.Hub.DB
{
    sealed class Country: HubCore.DB.CountryRow
    {
        public Country()
        { }

        public Country(uint id, string name , ushort internalCode, string isoCode):
            base(id, name, internalCode, isoCode)
        { }
    }
}
