using System.Windows.Forms;

namespace DGD.Hub
{
    interface IView
    {
        void Activate(Control parent);
        void Deactivate(Control parent);        
    }
}
