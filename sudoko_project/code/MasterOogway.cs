using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project.code
{
    /// <summary>
    /// MasterOogway is a singleton class that represents a wise character inspired by Master Oogway from Kung Fu Panda.
    /// It provides wisdom through smart sentences, mimicking the character's role as a mentor and philosopher.
    /// </summary>
    internal class MasterOogway : IWiserable
    {
        /// <summary>
        /// A private list of smart sentences that MasterOogway can say.
        /// </summary>
        private List<string> smartSentences;

        /// <summary>
        /// A private static instance of MasterOogway, ensuring a single instance is used throughout the application (singleton pattern).
        /// </summary>
        private static MasterOogway instance = null;

        /// <summary>
        /// Private constructor to prevent instance creation outside of the GetInstance method, initializing the list of smart sentences.
        /// </summary>
        private MasterOogway()
        {
            // Initialization of smart sentences. Future implementations might load these from a file.
            smartSentences = new List<string>
        {
            "There are no accidents.",
            "There is just news. There is no good or bad.",
            "There is always something more to learn. Even for a master.",
            "One often meets his destiny on the road he takes to avoid it.",
            "When will you realise? The more you take, the less you have.",
            "Yesterday is history, Tomorrow is a mystery, but today is a gift. That is why it is called the present."
        };
        }

        /// <summary>
        /// Ensures that only one instance of MasterOogway is created for the application (singleton pattern).
        /// </summary>
        /// <returns>The single instance of MasterOogway.</returns>
        public static MasterOogway GetInstance()
        {
            if (instance == null)
                instance = new MasterOogway();
            return instance;
        }

        /// <summary>
        /// Selects and returns a random smart sentence from the list of sentences.
        /// </summary>
        /// <returns>A randomly selected smart sentence.</returns>
        public string GetRandomSentence()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, smartSentences.Count);
            return smartSentences[randomNumber];
        }
    }

    internal interface IWiserable
    {
        string GetRandomSentence();
    }
}
