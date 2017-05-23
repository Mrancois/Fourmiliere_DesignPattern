using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereMOC2
{
    public class AboutViewModel : ViewModelBase
    {
        public string Copyright
        { get { return "Moi"; } }

        public string DateApplication
        { get { return DateTime.Now.ToString(); } }

        public string Auteur
        { get { return "François Devove"; } }
    }
}
