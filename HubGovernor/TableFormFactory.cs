using DGD.HubCore;
using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DGD.HubGovernor
{
    static class TableFormFactory
    {
        
        static readonly Dictionary<uint , Func<IDatumProvider , IDatum , Form>> m_formFactory =
            new Dictionary<uint , Func<IDatumProvider , IDatum , Form>>();
        static readonly Dictionary<uint , bool> m_deleteFlags = new Dictionary<uint , bool>();
        static readonly Dictionary<uint , bool> m_addFlags = new Dictionary<uint , bool>();
        static readonly Dictionary<uint , bool> m_editFlags = new Dictionary<uint , bool>();



        static TableFormFactory()
        {
            m_formFactory[TablesID.COUNTRY] = (dp , d) => new Countries.CountryForm(dp , d);
            m_deleteFlags[TablesID.COUNTRY] = true;
            m_addFlags[TablesID.COUNTRY] = true;
            m_editFlags[TablesID.COUNTRY] = true;

            m_formFactory[TablesID.CURRENCY] = (dp , d) => new Currencies.CurrencyForm(dp , d);
            m_deleteFlags[TablesID.CURRENCY] = m_addFlags[TablesID.CURRENCY] = true;
            m_editFlags[TablesID.CURRENCY] = true;

            m_addFlags[TablesID.FILE_GENERATION] = m_editFlags[TablesID.FILE_GENERATION] =
                m_deleteFlags[TablesID.FILE_GENERATION] = false;

            m_formFactory[TablesID.PLACE] = (dp , d) => new Places.PlaceForm(dp , d);
            m_addFlags[TablesID.PLACE] = m_editFlags[TablesID.PLACE] =
                m_deleteFlags[TablesID.PLACE] = true;

            m_formFactory[TablesID.PRODUCT] = (dp , d) => new Products.ProductForm(dp , d);
            m_addFlags[TablesID.PRODUCT] = m_editFlags[TablesID.PRODUCT] = 
                m_deleteFlags[TablesID.PRODUCT] = true;

            m_formFactory[TablesID.SUPPLIER] = (dp , d) => new Suppliers.DataSuppliersForm(dp , d);
            m_addFlags[TablesID.SUPPLIER] = m_deleteFlags[TablesID.SUPPLIER] = false;
            m_editFlags[TablesID.SUPPLIER] = true;

            m_formFactory[TablesID.UNIT] = (dp , d) => new Units.UnitForm(dp , d);
            m_deleteFlags[TablesID.UNIT] = m_addFlags[TablesID.UNIT] = true;
            m_editFlags[TablesID.UNIT] = true;

            m_addFlags[TablesID.VALUE_CONTEXT] = m_editFlags[TablesID.VALUE_CONTEXT] = 
                m_deleteFlags[TablesID.VALUE_CONTEXT] = false;

            m_addFlags[InternalTablesID.TR_SPOT_VALUE] = false;
            m_deleteFlags[InternalTablesID.TR_SPOT_VALUE] = false;
            m_editFlags[InternalTablesID.TR_SPOT_VALUE] = false;                    

            m_formFactory[TablesID.INCOTERM] = (dp , d) => new Incoterms.IncotermForm(dp , d);
            m_addFlags[TablesID.INCOTERM] = true;
            m_deleteFlags[TablesID.INCOTERM] = true;
            m_editFlags[TablesID.INCOTERM] = true;

            m_addFlags[InternalTablesID.TRANSACTION] = false;
            m_deleteFlags[InternalTablesID.TRANSACTION] = false;
            m_editFlags[InternalTablesID.TRANSACTION] = false;

            m_addFlags[InternalTablesID.INCREMENT] = false;
            m_deleteFlags[InternalTablesID.INCREMENT] = false;
            m_editFlags[InternalTablesID.INCREMENT] = false;

            m_addFlags[InternalTablesID.TR_LABEL] = false;
            m_deleteFlags[InternalTablesID.TR_LABEL] = false;
            m_editFlags[InternalTablesID.TR_LABEL] = false;

            m_addFlags[InternalTablesID.TR_PRODUCT_MAPPING] = false;
            m_deleteFlags[InternalTablesID.TR_PRODUCT_MAPPING] = false;
            m_editFlags[InternalTablesID.TR_PRODUCT_MAPPING] = false;

            m_addFlags[TablesID.SPOT_VALUE] = false;
            m_deleteFlags[TablesID.SPOT_VALUE] = false;
            m_editFlags[TablesID.SPOT_VALUE] = false;

            m_addFlags[TablesID.SHARED_TEXT] = false;
            m_deleteFlags[TablesID.SHARED_TEXT] = false;
            m_editFlags[TablesID.SHARED_TEXT] = false;

            m_addFlags[InternalTablesID.TR_LABEL_MAPPING] = false;
            m_deleteFlags[InternalTablesID.TR_LABEL_MAPPING] = false;
            m_editFlags[InternalTablesID.TR_LABEL_MAPPING] = false;

            m_addFlags[InternalTablesID.USER_PROFILE] = false;
            m_deleteFlags[InternalTablesID.USER_PROFILE] = false;
            m_editFlags[InternalTablesID.USER_PROFILE] = false;

            m_addFlags[InternalTablesID.HUB_CLIENT] = false;
            m_deleteFlags[InternalTablesID.HUB_CLIENT] = false;
            m_editFlags[InternalTablesID.HUB_CLIENT] = false;

            m_addFlags[InternalTablesID.CLIENT_STATUS] = false;
            m_deleteFlags[InternalTablesID.CLIENT_STATUS] = false;
            m_editFlags[InternalTablesID.CLIENT_STATUS] = false;

            m_addFlags[InternalTablesID.PROFILE_MGMNT_MODE] = false;
            m_deleteFlags[InternalTablesID.PROFILE_MGMNT_MODE] = false;
            m_editFlags[InternalTablesID.PROFILE_MGMNT_MODE] = false;

            m_addFlags[InternalTablesID.APP_UPDATE] = false;
            m_deleteFlags[InternalTablesID.APP_UPDATE] = false;
            m_editFlags[InternalTablesID.APP_UPDATE] = false;

            m_addFlags[InternalTablesID.CLIENT_ENV] = false;
            m_deleteFlags[InternalTablesID.CLIENT_ENV] = false;
            m_editFlags[InternalTablesID.CLIENT_ENV] = false;
        }


        public static Form CreateFrom(uint idTable, IDatumProvider dp, IDatum datum = null) => 
            m_formFactory[idTable](dp, datum);

        public static bool IsAddingEnabled(uint idTable) => m_addFlags[idTable];
        public static bool IsDeletingEnabled(uint idTable) => false;// m_deleteFlags[idTable];
        public static bool IsEditingEnabled(uint idTable) => m_editFlags[idTable];
    }
}
