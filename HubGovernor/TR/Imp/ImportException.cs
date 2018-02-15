using System;

namespace DGD.HubGovernor.TR.Imp
{
    sealed class ImportException : Exception
    {
        public ImportException(string what, int ndxLine):
            base($"Erreur lors de l'importation de '{what}' de la ligne {ndxLine + 1}")
        { }
    }
}
