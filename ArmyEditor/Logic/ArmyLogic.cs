using ArmyEditor.Models;
using ArmyEditor.Services;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyEditor.Logic
{
    public class ArmyLogic : IArmyLogic
    {
        IList<Trooper> army;
        IList<Trooper> barrack;

        IMessenger messenger;
        ITrooperEditorService trooperEditor;

        public ArmyLogic(IMessenger mess, ITrooperEditorService troopereditor)
        {
            this.messenger = mess;
            this.trooperEditor = troopereditor;
        }

        public void SetupCollections(IList<Trooper> army, IList<Trooper> barrack)
        {
            this.army = army;
            this.barrack = barrack;
        }

        public double AVGSpeed
        {
            get
            {
                return army.Count() == 0 ? 0 : this.army.Average(x => x.Speed);
            }
        }

        public double AVGPower
        {
            get
            {
                return army.Count() == 0 ? 0 : this.army.Average(x => x.Power);
            }
        }

        public int AllCost
        {
            get
            {
                return army.Count() == 0 ? 0 : this.army.Sum(x => x.Cost);
            }
        }

        public void AddToArmy(Trooper trooper)
        {
            army.Add(trooper);
            messenger.Send("Trooper added", "TrooperInfo");
        }

        public void RemoveFromArmy(Trooper trooper)
        {
            army.Remove(trooper);
            messenger.Send("Trooper removed", "TrooperInfo");
        }

        public void EditTrooper(Trooper trooper)
        {
            this.trooperEditor.Edit(trooper);
        }
    }
}
