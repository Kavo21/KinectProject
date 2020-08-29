using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_v2_Learning
{
    public class Sentence
    {
        public String sentence,chinese;
        private List<Sentence> sentencesList =new List<Sentence>();

        public Sentence(string sentence, string chinese)
        {
            this.sentence = sentence;
            this.chinese = chinese;
        }
    }
}
