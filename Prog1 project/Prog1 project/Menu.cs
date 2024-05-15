using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Prog1_project
{
    public delegate void Selected();

    public class Menu
    {
        public string Name { get; }
        public Selected Selected { get; set; }
        
        public Menu(string name, Selected selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}
