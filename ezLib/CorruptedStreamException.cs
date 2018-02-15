using System;

namespace easyLib
{
    public class CorruptedStreamException: Exception
    {
        public CorruptedStreamException(string message = null , Exception innerException = null) :
            base(message ?? DEFAULT_MSG , innerException)
        { }

        //private
        const string DEFAULT_MSG = "Une erreur c’est produite lors d’une opération d’entrée/sortie.\n" +
            "Le flux sous-jacent est probablement corrompu.";
    }
}
