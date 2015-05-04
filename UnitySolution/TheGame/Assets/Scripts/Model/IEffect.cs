using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public abstract class IEffect
    {
        public string name;
        public string id;
        public string description;
        public byte moves;

        public abstract void Use(ref List<Unit> team, ref Unit u);

        public abstract void End(ref List<Unit> team, ref Unit u);
    }
