using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAAuditMonitor {
    class Program {
        static void Main(string[] args) {
            //lets make sure our pathing exists to save information
            FolderHandler.SetupDirectory();

            FolderHandler.ProcessRelations();
        }
    }
}
