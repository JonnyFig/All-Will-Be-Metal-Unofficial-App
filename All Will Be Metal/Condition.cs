using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All_Will_Be_Metal
{
    class Condition
    {
        public bool slow = false;
        public bool hex = false;
        public bool incinerate = false;
        public bool judgement = false;
        public bool root = false;
        public bool shock = false;

        public Condition()
        {

        }

        public bool hasCondition()
        {
            if (slow || hex || incinerate || judgement || root || shock)
            {
                return true;
            }
            return false;
        }

        public String RemoveCondition()
        {
            if (slow) { slow = false; return "Ultron removes the slow condition"; }
            if (hex) { hex = false; return "Ultron removes the hex condition"; }
            if (incinerate) { incinerate = false; return "Ultron removes the incinerate condition"; }
            if (judgement) { judgement = false; return "Ultron removes the judgement condition"; }
            if (root) { root = false; return "Ultron removes the root condition"; }
            if (shock) { shock = false; return "Ultron removes the shock condition"; }
            return "No condition found";
        }

    }
}
