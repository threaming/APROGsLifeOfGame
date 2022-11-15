using System.Reflection.PortableExecutable;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MenuSpace
{
    public class Menu
    {
        internal Dictionary<string, Menu> subMenus;

        public Menu(string header, Type obj)
        {
            this.Header = header;
            this.Exe = obj;
            this.subMenus = new Dictionary<string, Menu>();
        }

        public void AddSub(string header, Type obj)
        {
            subMenus.Add(header, new Menu(header, obj));
        }

        private string Header { get; set; }
        private Type Exe { get; set; }
        
    }
}