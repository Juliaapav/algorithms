using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public abstract class GraphA
    {
        #region consts

        protected const int infinity = 1000001;

        protected const int minWeightOfEdge = 1;

        protected const int maxWeightOfEdge = 1000000;

        protected const int countNodeForExperiments = 1000;

        protected const int Step = 1000;

        protected const int firstMinCountEdge = 100000;

        protected const int firstMaxCountEdge = 10000000;

        protected const int firstStep = 100000;

        protected const int secondStep = 1000;

        protected const int secondMinCountEdge = 1000;

        protected const int secondMaxCountEdge = 100000;
        #endregion

        public abstract List<int> FirstExperimentForFirstMethod();

        public abstract List<int> FirstExperimentForSecondMethod();

        public abstract List<int> SecondExperimentForFirstMethod();

        public abstract List<int> SecondExperimentForSecondMethod();
    }
}
