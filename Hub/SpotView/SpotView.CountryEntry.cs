using DGD.Hub.DB;

namespace DGD.Hub.SpotView
{
    partial class SpotView
    {
        class CountryEntry
        {
            public CountryEntry(Country cntry)
            {
                Country = cntry;
            }

            public Country Country { get; }

            public static bool UseCountryCode { get; set; }

            public override string ToString()
            {
                if (Country == null)
                    return AppText.UNSPECIFIED;

                if (UseCountryCode)
                    return Country.InternalCode.ToString("000");

                return Country.Name;
            }

        }
    }
}
