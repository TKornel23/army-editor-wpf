using ArmyEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyEditor.ViewModels
{
    public class TrooperEditorViewModel
    {
        public Trooper Actual { get; set; }

        public TrooperEditorViewModel()
        {
        }

        public void Setup(Trooper trooper)
        {
            this.Actual = trooper;
        }
    }
}
