using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project.code
{
    internal class MasterOogway : IOogwayable
    {
        private List<string> smartSentances;

        private static MasterOogway instance = null;
        private MasterOogway() 
        {
            //maybe someday I will implement this with file... 
            smartSentances = new List<string>
            {
                "There are no accidents.",
                "There is just news. There is no good or bad.",
                "There is always something more to learn. Even for a master.",
                "One often meets his destiny on the road he takes to avoid it.",
                "When will you realise? The more you take, the less you have.",
                "Yesterday is history, Tomorrow is a mystery, but today is a gift. That is why it is called the present."
            };


        }

        public static MasterOogway GetInstance() 
        { 
            if (instance == null)
                instance = new MasterOogway();
            return instance;
        }
        public string GetRandomSentence()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, smartSentances.Count);
            return smartSentances[randomNumber];
        }
    }

    internal interface IOogwayable
    {
        string GetRandomSentence();
    }
}
