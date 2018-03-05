using System;

namespace DGD.Hub.DB
{
    sealed class BadTagException: Exception
    {
        public BadTagException(string tblName) :
            base(string.Format(AppText.EXCP_BADTAG , tblName))
        {
            TableName = tblName;
        }


        public string TableName { get; }

    }
}
