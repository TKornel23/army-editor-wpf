using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyEditor.Models
{
    public class Trooper : ObservableObject
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }
        private int speed;

        public int Speed
        {
            get { return speed; }
            set { SetProperty(ref speed, value); }
        }
        private int power;

        public int Power
        {
            get { return power; }
            set { SetProperty(ref power, value); }
        }

        public int Cost { get 
        { 
            return this.speed * this.power;
            }
        }

        public Trooper GetCopy()
        {
            return new Trooper()
            {
                Type = this.Type,
                Speed = this.Speed,
                Power = this.Power
            };
        }
    }
}
