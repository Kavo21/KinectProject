using System;
using System.Collections.Generic;
using Microsoft.Speech.Recognition;
using System.Windows.Media.Imaging;

namespace Kinect_v2_Learning
{
    public class Vocabulary
    {
        /// <summary>
        /// Vocabulary、Sentence
        /// </summary>
        public String give_Vocabulary, give_Sentence, give_StudentsSentences,giveDetection;

        /// <summary>
        ///  The List are stored the vocabulary and sentence
        /// </summary>
        public List<Vocabulary> vocabulary_list = new List<Vocabulary>();

        /// <summary>
        /// 學生所做的句子
        /// </summary>
        public List<Sentence> sentencesList = new List<Sentence>();

        /// <summary>
        /// 判斷單字是否包含下令情感單字
        /// </summary>
        public static String[] EmotionString = { "Happy", "Surprise", "Sadness", "Sad", "Happiness", "Disgust", "Fear", "Anger", "Neutral", "Contempt", "Grieving" , "Sorrow" , "Startled", "Despise" };

        /// <summary>
        /// Speech Dictionary
        /// </summary>
        public Choices Speech_Dictionary = new Choices();

        public string vocabulary { get; set; }

        public string sentence { get; set; }

        public string detection { get; set; }

        public BitmapSource ImageSource { get; set; }

        public Vocabulary() {
            
            //判斷使用者選擇單字或片語模式
            if (Select_Item.SetSentenceMode == true)
            {
                Init_Vocabulary();
                Init_StudentsSentences();
            }
            else if (Select_Item.SetPeerSentencePracticeMode == true)
            {
                Init_Vocabulary();
                Init_StudentsSentences();
            }
            else if (Select_Item.SetStudentsSentencesMode == true)
            {
                Init_Vocabulary();
                Init_StudentsSentences();
            }
            else if (Select_Item.SetPeerStudentsSentencePracticeMode == true)
            {
                Init_Vocabulary();
                Init_StudentsSentences();
            }
            else if (Select_Item.SetPeerStudentsSentencePracticeMode == true)
            {
                Init_Vocabulary();
                Init_StudentsSentences();
            }

            else if (Select_Item.SetVocabularyMode == true)
            {
                Init_Vocabulary();
            }
            else if (Select_Item.SetPeerVocabularyPracticeMode == true)
            {
                Init_Vocabulary();
            }
            else if (Select_Item.SetInteractiveMode == true)
            {
                Init_StudentsSentences();
            }
            else if (Select_Item.SetPeerInteractiveMode == true)
            {
                Init_StudentsSentences();
            }

            //初始化語音字典
            Init_Speech_Vocabulary();
        }

        /// <summary>
        /// 提供給句子作為檢查的單字字典
        /// </summary>
        public void ProvideSentencesToCheck() {
            Init_Vocabulary();
        }

        /// <summary>
        /// 建構值，需傳入資料建構單字
        /// </summary>
        private Vocabulary(String detection, String vocabulary, String sentence ,BitmapSource imageSource) {
            this.detection = detection;
            this.vocabulary = vocabulary;
            this.sentence = sentence;
            this.ImageSource = imageSource;
        }

        /// <summary>
        /// 初始化 單字
        /// </summary>
        private void Init_Vocabulary() {
            vocabulary_list.Add(new Vocabulary("Example Emotion", "Surprise", "Surprise 驚訝 " + "\n" + "形容詞" + "\n" + "To my surprise, he refused to cooperate with us" + "\n" + "令我吃驚的是，他不肯與我們合作。", new BitmapImage(new Uri("/Example_image/SurprisedEmojipng.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture","Relieve", "Relieve 緩解" + "\n" + "動詞" + "\n" + "Relieve the pain from your headache by drinking water and getting rest." + "\n" + "通過飲水和休息來緩解頭痛帶來的痛苦。", new BitmapImage(new Uri("/Example_image/Relieve.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Scratch", "Scratch 搔" + "\n" + "動詞" + "\n" + "Tom scratched his head while he was thinking." + "\n" + "湯姆在思考時撓了撓頭。", new BitmapImage(new Uri("/Example_image/Scratch.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Comfort", "Comfort 安慰" + "\n" + "動詞" + "\n" + "The mother comforted her crying baby by patting him gently on the back." + "\n" + " 母親輕輕拍了拍他的背，撫慰著哭泣的嬰兒。", new BitmapImage(new Uri("/Example_image/Comfort.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Float", "Float 浮；漂浮" + "\n" + "動詞" + "\n" + "The boy asked his father why some things float in water and other things do not." + "\n" + "男孩問他的父親為什麼有些東西漂浮在水中而其他東西則沒有。", new BitmapImage(new Uri("/Example_image/Float.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Sprinkle", "Sprinkle 灑" + "\n" + "The last step in the recipe is to sprinkle some sea salt over the cookies." + "\n" + "配方的最後一步是在餅乾上撒上一些海鹽。", new BitmapImage(new Uri("/Example_image/Sprinkle.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Wander", "Wander 漫步；閒逛；遊蕩" + "\n" + "動詞" + "\n" + "Ray wandered around the store while his girlfriend looked at dresses." + "\n" + "當他的女朋友看著禮服時，雷在商店裡閒逛。", new BitmapImage(new Uri("/Example_image/Wander.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Ban", "Ban 禁止" + "\n" + "動詞" + "\n" + "Many countries are banning  the use of plastic straws." + "\n" + "許多國家禁止使用塑料吸管。", new BitmapImage(new Uri("/Example_image/Ban.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Destroy", "Destroy 毀壞，破壞" + "\n" + "動詞" + "\n" + "The singer destroyed her reputation when she gave an awful performance on a TV show." + "\n" + "當她在電視節目中表現糟糕時，這位歌手摧毀了她的聲譽。", new BitmapImage(new Uri("/Example_image/Destroy.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Merge", "Merge 合併；使融合" + "\n" + "動詞" + "\n" + "It was decided that the two businesses should be merged." + "\n" + "人們決定應把這兩爿商店合併起來。", new BitmapImage(new Uri("/Example_image/Merge.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Overcome", "Overcome 克服;戰勝" + "\n" + "動詞" + "\n" + "Sue overcome her fear of dogs by volunteering at a shelter." + "\n" + "蘇通過在避難所做志願者，克服了對狗的恐懼。", new BitmapImage(new Uri("/Example_image/Overcome.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Launch", "Launch 展開(事業、行動)" + "\n" + "動詞" + "\n" + "Kelly never forgot the people who helped launch her acting career." + "\n" + "凱莉永遠不會忘記那些幫助她開展演藝生涯的人。", new BitmapImage(new Uri("/Example_image/Launch.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Attach", "Attach 附著;貼上" + "\n" + "動詞" + "\n" + "Did you not see the document I attached to the email ?" + "\n" + "你沒有看到我附在電子郵件中的文件嗎？", new BitmapImage(new Uri("/Example_image/Infrontof.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Establish", "Establish 建立" + "\n" + "動詞" + "\n" + "Ken's grandfather established this company forty years ago." + "\n" + "肯的祖父四十年前成立了這家公司。", new BitmapImage(new Uri("/Example_image/Establish.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Posture", "Hail", "Hail 讚揚;稱頌" + "\n" + "動詞" + "\n" + "Many people hailed the director's newest movie as her best ever." + "\n" + "很多人都稱讚這位導演的最新電影是她有史以來最好的。", new BitmapImage(new Uri("/Example_image/Hail.png", UriKind.Relative))));

            //名詞
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Confidence", "Confidence 自信" + "\n" + "名詞" + "\n" + "I have confidence in your ability to lead this team to victory." + "\n" + "我對你帶領這支球隊取得勝利的能力充滿信心。", new BitmapImage(new Uri("/Example_image/Confidence.png", UriKind.Relative))));

            //形容詞
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Chubby", "Chubby 圓胖的；豐滿的" + "\n" + "形容詞" + "\n" + "The baby has a chubby face." + "\n" + "那嬰兒的臉蛋兒圓圓胖胖。", new BitmapImage(new Uri("/Example_image/Chubby.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Smelly", "Smelly 難聞的；臭的" + "\n" + "形容詞" + "\n" + "This cheese is a bit smelly." + "\n" + "這種乾酪有點難聞。", new BitmapImage(new Uri("/Example_image/Smelly.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Unidentified", "Unidentified 不知名的;未知的" + "\n" + "形容詞" + "\n" + "Police are searching for an unidentified person who was seen near last night's crime scene." + "\n" + "警方正在搜尋一名身份不明的人，他在昨晚的犯罪現場附近被發現。", new BitmapImage(new Uri("/Example_image/Unidentified.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Guilty", "Guilty 內疚的;罪惡的;有罪的" + "\n" + "形容詞" + "\n" + "Lisa knew her brother took her sunglasses due to the guilty look on his face." + "\n" + "麗莎知道她的哥哥帶著她的太陽鏡，因為他臉上帶著愧疚的表情。", new BitmapImage(new Uri("/Example_image/Guilty.png", UriKind.Relative))));

            //名詞
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Depression", "Depression 意志消沉、憂鬱" + "\n" + "名詞" + "\n" + "A lack of sleep can cause depression , stress ,and nervousness" + "\n" + "睡眠不足會導致抑鬱，壓力和緊張。", new BitmapImage(new Uri("/Example_image/Depression.png", UriKind.Relative))));

            vocabulary_list.Add(new Vocabulary("Example Object", "Visual", "Visual 視覺的；視力的" + "\n" + "形容詞" + "\n" + "Near-sightedness is a visual defect." + "\n" + " 近視是一個視力缺陷。", new BitmapImage(new Uri("/Example_image/Visual_Image.jpg", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Mental", "Mental 智力的;腦力的" + "\n" + "形容詞" + "\n" + "Although he was an adult, the man had the mental age of a child." + "\n" + "雖然他是一名成年人，但這名男子的精神年齡只有一個孩子。", new BitmapImage(new Uri("/Example_image/Mental_Image.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Imaginary", "Imaginary 想像中的；虛構的" + "\n" + "形容詞" + "\n" + "When Dylan got older,he realized that the monster hiding under his bed was imaginary." + "\n" + "當迪倫長大後，他意識到隱藏在他床下的怪物是虛構的。", new BitmapImage(new Uri("/Example_image/Imaginary.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Individual", "Individual 單獨的;個別的" + "\n" + "形容詞" + "\n" + "Each individual part must work perfectly in order for this machine to operate." + "\n" + "每個單獨的部件必須完美地工作才能使該機器運行。", new BitmapImage(new Uri("/Example_image/Individual.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Vegetarian", "Vegetarian 素食主義者" + "\n" + "形容詞" + "\n" + "Does this restaurant offer any vegetarian dishes ?" + "\n" + "這家餐廳有提供素食的菜嗎？。", new BitmapImage(new Uri("/Example_image/Vegetarian.png", UriKind.Relative))));

            //情感詞
            vocabulary_list.Add(new Vocabulary("臉部辨識", "Grieving", "Grieving悲痛的" + "\n" + "形容詞" + "\n" + "After the terrible accident, the grieving family received a lot of help from their neighbors.可怕的意外發生後，這悲痛的家庭從鄰居那得到很多幫助。", new BitmapImage(new Uri("/Example_image/Grieving.jpg", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("臉部辨識", "Anger", "Anger 憤怒；怒氣" + "\n" + "形容詞" + "\n" + "Sharon was filled with anger and shouted at her neighbors." + "\n" + "雪倫怒氣沖沖，對著鄰居大吼。", new BitmapImage(new Uri("/Example_image/anger.jpg", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("臉部辨識", "Sorrow", "Sorrow悲傷；悲痛" + "\n" + "名詞" + "\n" + "Since his wife and child left him, he has lived in sorrow." + "\n" + "自從他的妻子和小孩離開他，他就活在悲傷中。", new BitmapImage(new Uri("/Example_image/Sorrow.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("臉部辨識", "Startled", "Startled 受驚嚇的" + "\n" + "形容詞" + "\n" + "The results were quite startling -- a 77% increase in six month." + "\n" + "結果非常令人吃驚：六個月增長百分之七十七。", new BitmapImage(new Uri("/Example_image/Startled.jpg", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("臉部辨識", "Happy", "Happy 快樂" + "\n" + "形容詞" + "\n" + "I am happy to accept your invitation." + "\n" + "我很高興接受你的邀請。", new BitmapImage(new Uri("/Example_image/Happy.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("Example Emotion", "Surprise", "Surprise 驚訝 " + "\n" + "形容詞" + "\n" + "To my surprise, he refused to cooperate with us" + "\n" + "令我吃驚的是，他不肯與我們合作。", new BitmapImage(new Uri("/Example_image/SurprisedEmojipng.png", UriKind.Relative))));
            //動詞
            vocabulary_list.Add(new Vocabulary("臉部辨識", "Despise", "Despise 鄙視；看不起" + "\n" + "動詞" + "\n" + "Why do you despise that politician?" + "\n" + "你為什麼鄙視這位政治家？", new BitmapImage(new Uri("/Example_image/Despise.png", UriKind.Relative))));

            //方位介係詞
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Behind", "Behind 在……的後面" + "\n" + "方位介係詞" + "\n" + "There is a big tree behind the house." + "\n" + "房子後面有一棵大樹。", new BitmapImage(new Uri("/Example_image/Behind.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Under", "Under 在……下面" + "\n" + "方位介係詞" + "\n" + "They were playing chess under the tree. " + "\n" + "他們在樹下下棋。", new BitmapImage(new Uri("/Example_image/Down.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Over", "Over 在……之上" + "\n" + "方位介係詞" + "\n" + "We live over a small bookstore. " + "\n" + "我們住在一家小書店的樓上。", new BitmapImage(new Uri("/Example_image/Over.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Below", "Below 在……下面；" + "\n" + "方位介係詞" + "\n" + "Her skirt came below her knees." + "\n" + "她的裙長過膝。", new BitmapImage(new Uri("/Example_image/Down.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Above", "Above 在上面；在..之上，超過" + "方位介係詞" + "\n" + "\n" + "The moon is now above the trees. " + "\n" + "月亮正位於樹梢上。", new BitmapImage(new Uri("/Example_image/Over.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "In front of", "In front of 在……的前面" + "\n" + "方位介係詞" + "\n" + "The bus stops right in front of our house." + "\n" + "公共汽車正停在我們房前。", new BitmapImage(new Uri("/Example_image/Infrontof.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("姿勢辨識", "Down", "Down 往……下方" + "\n" + "方位介係詞" + "\n" + "Their house is half-way down the hill" + "\n" + "他們的房子座落在半山腰。。", new BitmapImage(new Uri("/Example_image/Down.png", UriKind.Relative))));

            //名詞
            vocabulary_list.Add(new Vocabulary("物件辨識", "Wisdom", "Wisdom 智慧，才智" + "\n" + "名詞" + "\n" + "The old woman had gained a lot of wisdom during her life" + "\n" + "這位老太太一生都獲得了很多智慧。", new BitmapImage(new Uri("/Example_image/Mental.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Surgery", "Surgery 外科手術" + "\n" + "名詞" + "\n" + "After her accident, Sandy needed knee replacement surgery." + "\n" + "事故發生後，桑迪需要進行膝關節置換手術。", new BitmapImage(new Uri("/Example_image/Surgery.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Burden", "Bburden 重擔；負擔" + "\n" + "名詞" + "\n" + "Large amounts of debt can be a significant burden for families." + "\n" + "大量債務可能成為家庭的重大負擔。", new BitmapImage(new Uri("/Example_image/Burden.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Mammal", "Mammal 哺乳動物" + "\n" + "名詞" + "\n" + "Whales are mammals; fish are not." + "\n" + "鯨是哺乳動物；魚則不是。", new BitmapImage(new Uri("/Example_image/Mammal.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Container", "Container 容器;貨櫃" + "\n" + "名詞" + "\n" + "Larry put the leftover spaghetti into a plastic container." + "\n" + "拉里把剩下的意大利面放入塑料容器中。", new BitmapImage(new Uri("/Example_image/Container.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Grocery", "Grocery 食品雜貨" + "\n" + "名詞" + "\n" + "Brian's favorite grocery store has opened near his apartment." + "\n" + "Brian最喜歡的雜貨店在他的公寓附近開業。", new BitmapImage(new Uri("/Example_image/Grocery.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Spark", "Spark 火花" + "\n" + "名詞" + "\n" + "With his newest album a commercial failure,the singer wondered if he had lost his spark." + "\n" + "隨著他的最新專輯的商業失敗，這位歌手想知道他是否失去了他的火花。", new BitmapImage(new Uri("/Example_image/Spark.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Coral", "Coral 珊瑚" + "\n" + "名詞" + "\n" + "Please remember not to touch the coral reefs when you go diving." + "\n" + "請記住，潛水時不要觸摸珊瑚礁。", new BitmapImage(new Uri("/Example_image/Coral.png", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Property", "Property 房產、" + "\n" + "名詞" + "\n" + "This small house is my only property." + "\n" + "這所小房子是我的唯一財產。", new BitmapImage(new Uri("/Example_image/Property.png", UriKind.Relative))));

            //形容詞
            vocabulary_list.Add(new Vocabulary("物件辨識", "Bankrupt", "Bankrupt 破產的" + "\n" + "形容詞" + "\n" + "The company was forced to lay off its employees because it went bankrupt." + "\n" + " 該公司被迫裁員，因為它破產了。", new BitmapImage(new Uri("/Example_image/Bankrupt.png", UriKind.Relative))));
        }

        /// <summary>
        /// 初始化句子
        /// </summary>
        private void Init_Sentence() {
            vocabulary_list.Add(new Vocabulary("物件辨識", "I feel sad", "I feel sad 我感覺傷心", new BitmapImage(new Uri("/Example_image/sandess.jpg", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "I really want to play a game", "I really want to play a game", new BitmapImage(new Uri("", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Above", "Above 在上面；在..之上，超過", new BitmapImage(new Uri("", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "In front of", "In front of 在某人/某物前面", new BitmapImage(new Uri("", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "In back of", "In back of 在…的後面", new BitmapImage(new Uri("", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "On left side", "On left side 在左邊", new BitmapImage(new Uri("", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "On right side", "On right side 在右邊", new BitmapImage(new Uri("", UriKind.Relative))));
            vocabulary_list.Add(new Vocabulary("物件辨識", "Below", "Below (指位置）在……下面", new BitmapImage(new Uri("", UriKind.Relative))));          
        }

        /// <summary>
        /// 初始化學生做的句子
        /// </summary>
        private void Init_StudentsSentences()
        {
            switch (Students.StudentsName)
            {
                #region 第一組完成
                case "黃裕舜":
                    DataSet.Group1_Sentence(sentencesList);
                    break;
                case "劉奕德":
                    DataSet.Group1_Sentence(sentencesList);
                    break;
                #endregion

                #region 第二組完成
                case "朱湘琳":
                   DataSet.Group2_Sentence(sentencesList);
                    break;
                case "黃琬婷":
                    DataSet.Group2_Sentence(sentencesList);
                    break;
                #endregion

                #region 第三組完成
                case "陳品瑄":
                    DataSet.Group3_Sentence(sentencesList);
                    break;
                case "邱筱筑":
                    DataSet.Group3_Sentence(sentencesList);
                    break;
                #endregion

                #region 第四組完成
                case "王連宙":
                    DataSet.Group4_Sentence(sentencesList);
                    break;
                case "段崇文":
                    DataSet.Group4_Sentence(sentencesList);
                    break;
                #endregion

                #region 第五組完成
                case "古雅瑜":
                    DataSet.Group5_Sentence(sentencesList);
                    break;
                case "呂幸樺":
                    DataSet.Group5_Sentence(sentencesList);
                    break;
                #endregion

                #region 第六組完成
                case "王芸酈":
                    DataSet.Group6_Sentence(sentencesList);
                    break;
                case "馮玟琪":
                    DataSet.Group6_Sentence(sentencesList);
                    break;
                #endregion

                #region 第七組完成
                case "陳孝涵":
                    DataSet.Group7_Sentence(sentencesList);
                    break;
                case "曹語庭":
                    DataSet.Group7_Sentence(sentencesList);
                    break;
                #endregion

                #region 第八組完成
                case "莊楷楨":
                    DataSet.Group8_Sentence(sentencesList);
                    break;
                case "高郁涵":
                    DataSet.Group8_Sentence(sentencesList);
                    break;
                #endregion

                #region 第九組完成
                case "任柏翰":
                    DataSet.Group9_Sentence(sentencesList);
                    break;
                case "李宛庭":
                    DataSet.Group9_Sentence(sentencesList);
                    break;
                #endregion

                #region 甲班
                case "何政剛":
                    sentencesList.Add(new Sentence("You need to overcome all you afraid when you be a master", "當你成為一名大師時，你需要克服所有的恐懼。"));
                    sentencesList.Add(new Sentence("Do not launch the rocket or you will be regret", "不要發射火箭，否則你會後悔的"));
                    sentencesList.Add(new Sentence("Do you know that the area is ban", "你知道該地區是禁令嗎？"));
                    sentencesList.Add(new Sentence("Doctor say you need to stop scratching your hand", "醫生說你需要停止刮傷你的手"));
                    sentencesList.Add(new Sentence("Use your imaginary draw a special kite for yourself", "用你的想像為自己畫一個特殊的風箏"));
                    sentencesList.Add(new Sentence("We need to make a big surprise for your best friend", "我們需要為我們最好的朋友帶來驚喜"));
                    sentencesList.Add(new Sentence("You destroy whole city including my house", "你摧毀整個城市，包括我的房子"));
                    sentencesList.Add(new Sentence("You need to feel guilty for the thing you have done", "你需要為自己所做的事感到內疚"));
                    sentencesList.Add(new Sentence("I can make everything I want float in sky", "我可以讓我想要的一切都漂浮在天空中"));
                    sentencesList.Add(new Sentence("I found this place is getting down the hail", "我發現這個地方正在降臨冰雹"));
                    sentencesList.Add(new Sentence("some people do not know that dolphin is mammal", "有些人不知道海豚是哺乳動物"));
                    sentencesList.Add(new Sentence("You need to sprinkle the salt evenly on the steak", "你需要在牛排上均勻撒上鹽"));
                    sentencesList.Add(new Sentence("If you still waste your money on gambling you will be bankrupt", "如果你仍然把錢浪費在賭博上，你將會破產"));
                    sentencesList.Add(new Sentence("We want to find the gold under the deep ocean", "我們想找到深海下的黃金"));
                    sentencesList.Add(new Sentence("Food is in front of you why you do not eat", "食物在你面前為什麼不吃"));
                    sentencesList.Add(new Sentence("The gold container is very expensive", "金容器很貴"));
                    sentencesList.Add(new Sentence("He is very easy to be anger", "他很容易生氣"));
                    sentencesList.Add(new Sentence("I am not a vegetarian because I like eat meat", "我不是素食主義者，因為我喜歡吃肉"));
                    sentencesList.Add(new Sentence("Do not help me it will make my burden bigger", "不要幫助我，這會讓我的負擔更大"));
                    break;
                case "高宜瑄":
                    sentencesList.Add(new Sentence("You need to overcome difficulties not escape from it", "你要克服困難 而不是逃避它"));
                    sentencesList.Add(new Sentence("He has a serious illness", "so he needs a surgery next week"));
                    sentencesList.Add(new Sentence("If you do not save money", "you will go bankrupt soon"));
                    sentencesList.Add(new Sentence("The book you are finding is under the chair near the sofa", "那本你正在找的書在靠近沙發的椅子下"));
                    sentencesList.Add(new Sentence("The whale is not a mammal", "the bird is not either"));
                    sentencesList.Add(new Sentence("We need many grocery to make this dish", "我們需要很多食材來做這道菜"));
                    sentencesList.Add(new Sentence("A vegetarian can not eat meat", "吃素的人不能吃肉"));
                    sentencesList.Add(new Sentence("She likes to wander around the garden because of the flower smell good", "她喜歡繞著花園 因為花香"));
                    sentencesList.Add(new Sentence("Look at the picture below what is the girl doing", "看下面這張圖 這個女孩在做什麼"));
                    sentencesList.Add(new Sentence("The container is good to storage many things", "箱子可以收納很多東西"));


                    //sentencesList.Add(new Sentence("The book is under the chair near the sofa", "書在靠近沙發的椅子下"));
                    //sentencesList.Add(new Sentence("She don't have any confidence", "她沒有自信。"));
                    //sentencesList.Add(new Sentence("Floating on the water needs courage", "漂浮在水上需要勇氣"));
                    //sentencesList.Add(new Sentence("Some people like to imaginary things does not exist in the world", "有些人喜歡想像不存在世界上的東西"));
                    //sentencesList.Add(new Sentence("She is good at comforting people", "她很會安慰人"));
                    //sentencesList.Add(new Sentence("You need to overcome difficulties not escape from it", "你要克服困難 而不是逃避它"));
                    //sentencesList.Add(new Sentence("If you do not save money you will go bankrupt fast", "如果你不存錢 你將會很快破產"));
                    //sentencesList.Add(new Sentence("He has serious illness so he needs a surgery", "他有嚴重的疾病 他需要開刀"));
                    //sentencesList.Add(new Sentence("The airplane is flying above the sky", "飛機在天空飛"));
                    //sentencesList.Add(new Sentence("The river is in front of my house", "我家前面有一條河"));
                    //sentencesList.Add(new Sentence("The mountain is behind the school", "山在學校後面"));
                    //sentencesList.Add(new Sentence("The container is good to storage many things", "箱子可以收納很多東西"));
                    //sentencesList.Add(new Sentence("I am happy to see you", "我很高興見到你"));
                    //sentencesList.Add(new Sentence("My sister is chubby because she eats too much", "我妹妹圓滾滾的 因為他吃很多"));
                    //sentencesList.Add(new Sentence("Stealing things is guilty", "偷東西是有罪的"));
                    //sentencesList.Add(new Sentence("Clownfish live in corals", "小丑魚住在珊瑚裡"));
                    //sentencesList.Add(new Sentence("They give he a birthday surprise", "他們給他一個生日驚喜"));
                    //sentencesList.Add(new Sentence("Stinky tofu is smelly but yummy", "臭豆腐臭臭的但很好吃"));
                    //sentencesList.Add(new Sentence("You can merge some vegetables together to make salads", "你可以混合一些蔬菜做沙拉"));
                    //sentencesList.Add(new Sentence("Some cold places will have hails when raining", "有些冷的地方在下雨的時候會有冰雹"));
                    break;
                case "陳虹妤":
                    sentencesList.Add(new Sentence("How is your visual test", "你的視力檢查如何"));
                    sentencesList.Add(new Sentence("My visual test just zero point eight", "還好 我的視力只有08"));
                    sentencesList.Add(new Sentence("Have you ever seen the coral", "你看過珊瑚嗎？"));
                    sentencesList.Add(new Sentence("Yes I just see the coral in this summer vacation", "有 這個暑假我才去看珊瑚"));
                    sentencesList.Add(new Sentence("Do you want to go to the grocery with me", "你想跟我去雜貨店嗎？"));
                    sentencesList.Add(new Sentence("Sure I have lone time have not go to the grocery", "好啊！我也很久沒去雜貨店了"));
                    sentencesList.Add(new Sentence("Do you know the cat is mammal", "你知道貓是哺乳類嗎？"));
                    sentencesList.Add(new Sentence("Yes I know the cat is mammal", "我知道貓是哺乳類動物"));
                    sentencesList.Add(new Sentence("What is the liquid in your container", "你的容器裡面裝著什麼液體？"));
                    sentencesList.Add(new Sentence("It is orange juice", "我的容器裡裝著橘子汁"));
                    sentencesList.Add(new Sentence("Are you a vegeterian", "你是素食主義者嗎？"));
                    sentencesList.Add(new Sentence("Yes I am a vegeterian since I was ten", "對的 我10歲開始是素食者"));
                    sentencesList.Add(new Sentence("I relieve my pressure by doing exercise", "我藉由做運動來釋放我的壓力"));
                    sentencesList.Add(new Sentence("I always do exercise to relieve my pressure after school", "我都在放學做運動來釋放壓力"));
                    sentencesList.Add(new Sentence("I always wander around the lake when I have bad mood", "當我心情不好時 我都會漫步在湖邊"));
                    sentencesList.Add(new Sentence("Wander around the lake is a good idea to relieve your pressure", "漫步在湖邊是個好主意來釋放你的壓力"));
                    sentencesList.Add(new Sentence("Look!The man is floating on the swimming poor", "你看那個男人漂浮在游泳池上"));
                    sentencesList.Add(new Sentence("He has great skill on float", "哇！他的漂浮技術很好"));
                    sentencesList.Add(new Sentence("I think somebody who can establish the company by himself is not easy", "我覺得一個人能獨自技建立一個公司很不容易"));
                    sentencesList.Add(new Sentence("You're right", " establish the company must need tremendous energy"));

                    //sentencesList.Add(new Sentence("Doing exercise maybe can let you relieve the pressure", "做運動也許可以讓你釋放壓力"));
                    //sentencesList.Add(new Sentence("An active volcano not only destroy the whole city but also broke many family", "活火山不但毀掉了整座城市且毀了很多家庭"));
                    //sentencesList.Add(new Sentence("My father establish the company by himself", "我父親親手建立了公司"));
                    //sentencesList.Add(new Sentence("I always attach my boyfriend because he is aromatic", "我總是黏著我男友因為他很香"));
                    //sentencesList.Add(new Sentence("She spent much time on sit around doing nothing so he was bankrupt", "她花了太多時間遊手好閒以至於現在破產"));
                    //sentencesList.Add(new Sentence("My aunt is a vegetarian since she was young", "我阿姨從很年輕的時候就是素食主義者"));
                    //sentencesList.Add(new Sentence("His mental is higher than general average", "他的心理高於一般平均水平"));
                    //sentencesList.Add(new Sentence("I like to wander around the lake", "我喜歡在湖邊閒逛"));
                    //sentencesList.Add(new Sentence("The man is floating on the swimming pool has nice skill", "那個男人有著厲害的技巧正在漂浮"));
                    //sentencesList.Add(new Sentence("I am happy because my sister come back home", "我很開心因為姐姐回家了"));
                    //sentencesList.Add(new Sentence("The teacher is anger because we broke the rules", "老師很生氣因為我們打破了規則"));
                    //sentencesList.Add(new Sentence("She was grieving when his best friend hurt", "她很難過因為他的閨蜜受傷了"));
                    //sentencesList.Add(new Sentence("The old house is in front of the forest", "這個老房子在森林前"));
                    //sentencesList.Add(new Sentence("He can hide under the desk", "他可以躲在桌子底下"));
                    //sentencesList.Add(new Sentence("She hugs her best behind the back", "她抱從背後抱閨蜜"));
                    //sentencesList.Add(new Sentence("We sang the song and the body  followed up and down", "我們唱著歌身體也上下的搖動起來"));
                    //sentencesList.Add(new Sentence("I hope I will have more confidence before I get a job", "希望得到工作之前我能擁有更多自信"));
                    //sentencesList.Add(new Sentence("I have never seen the coral because I think it is dangerous", "我覺得珊瑚很危險所以至今都還沒看過"));
                    //sentencesList.Add(new Sentence("There is a grocery opened the day before yesterday", "這裡有間雜貨店前天開張"));
                    //sentencesList.Add(new Sentence("Hopefully I will have property to go around the world", "希望我能擁有很大的財富來環遊世界"));
                    break;
                case "黃家琁":
                    sentencesList.Add(new Sentence("Why you and your sister looks so sorrow", "為什麼你跟你妹看起來這麼傷心?"));
                    sentencesList.Add(new Sentence("Because we were banded to go out by our parents", "因為我們被爸媽禁足"));
                    sentencesList.Add(new Sentence("Why you look so happy", "為什麼你看起來這麼開心?"));
                    sentencesList.Add(new Sentence("Because my best friend said some interested thing to comfort me", "因為我最好的朋友說了些有趣的事安慰我"));
                    sentencesList.Add(new Sentence("Where is the grocery", "這間雜貨店在哪裡?"));
                    sentencesList.Add(new Sentence("The grocery is in front of the transation", "這間雜貨店在火車站的前面"));
                    sentencesList.Add(new Sentence("The chubby boy was crashed by a car", "這個圓胖的小男孩被車撞了"));
                    sentencesList.Add(new Sentence("He might go to hospital to have a surgery", "他可能會去醫院做手術"));
                    sentencesList.Add(new Sentence("Why you look so startled", "為什麼你看起來受了驚嚇"));
                    sentencesList.Add(new Sentence("Because I was bankrupt ten minstutes ago", "因為在10分鐘前我破產了"));
                    sentencesList.Add(new Sentence("Why you looks so confidence", "為什麼你看起來這麼自信?"));
                    sentencesList.Add(new Sentence("Because I finished a imaginary novel", "因為我完成了一個虛構的小說"));
                    sentencesList.Add(new Sentence("The economic burden really make me crazy", "經濟壓力真的使我瘋了"));
                    sentencesList.Add(new Sentence("You should go out or wander mountains with friends", "你應該跟朋友出去或在山中漫步"));
                    sentencesList.Add(new Sentence("The company was established by my father", "這家公司是我爸創立的"));
                    sentencesList.Add(new Sentence("Why you look so guilty", "為什麼你看起來有罪惡感?"));
                    sentencesList.Add(new Sentence("Because I destroyed his phone", "因為我破壞了他的手機"));
                    sentencesList.Add(new Sentence("How to relieve the stersses", "要如何釋放壓力?"));
                    sentencesList.Add(new Sentence("By wandering mountains", "藉由在山中漫步"));

                    //sentencesList.Add(new Sentence("He spoke a few words of comfort to me", "他說了些安慰我的話"));
                    //sentencesList.Add(new Sentence("I went mountain climbing by wandering last weekend", "我上週末藉由漫步來爬山"));
                    //sentencesList.Add(new Sentence("My sister and I were banned to go out by my parents because we made them angry", "我和我妹妹被我父母禁足，因為我們讓他們生氣"));
                    //sentencesList.Add(new Sentence("These new regulations of class will destroy our advantages", "這些新的班規將會損害了我們的權益"));
                    //sentencesList.Add(new Sentence("Our manager decided to make the two firms merge this morning", "今天早上，我們的經理決定讓這兩家企業合併。"));
                    //sentencesList.Add(new Sentence("The learner of a second language has many obstacles to overcome", "第二語言學習者有許多障礙要克服"));
                    //sentencesList.Add(new Sentence("This charity was established by my grandpa", "這個慈善機構是被我祖父創立的"));
                    //sentencesList.Add(new Sentence("He was chubby and has blonde curls", "他胖乎乎的，有金色的捲發"));
                    //sentencesList.Add(new Sentence("I felt guilty after breaking my promise", "我違背諾言後感到內疚"));
                    //sentencesList.Add(new Sentence("Because he has addicted to gamble so he goes bankrupt", "因為他沉迷於賭博所以他破產了"));
                    //sentencesList.Add(new Sentence("Her problem is mental", "她的毛病是精神方面的"));
                    //sentencesList.Add(new Sentence("The story is rumored to imaginary", "這個故事被謠傳是虛構的"));
                    //sentencesList.Add(new Sentence("My mom is a vegetarian that she never eats any meat", "我媽是一個素食主義者，他從不吃肉"));
                    //sentencesList.Add(new Sentence("He feels surprising because the beautiful girl likes him", "他覺得很驚喜因為那個漂亮的女生喜歡她"));
                    //sentencesList.Add(new Sentence("I had a fall and his under lip began to swell up", "我跌了一跤"));
                    //sentencesList.Add(new Sentence("The gorcery is in front of my house", "這家雜貨店就在我家前面"));
                    //sentencesList.Add(new Sentence("The trip to the seashore brought her out of her depression", "到海邊旅行使她不再抑鬱"));
                    //sentencesList.Add(new Sentence("The worst barrier to your success is not lack of girlfriend", " but lack of confidence"));
                    //sentencesList.Add(new Sentence("His eyes sparked because he saw a beautiful girl", "他的雙眼發亮因為他看到漂亮的女孩。"));
                    //sentencesList.Add(new Sentence("My mother is wearing a coral necklace in front of mirror", "我媽正在鏡子前戴珊瑚項鍊。"));
                    break;
                case "李姍珊":
                    sentencesList.Add(new Sentence("Why you look so sorrow", "為什麼你看起來很悲傷?"));
                    sentencesList.Add(new Sentence("Because I have a contest tomorrow and fell grieving now", "因為我明天有一場比賽而且現在感到悲傷"));
                    sentencesList.Add(new Sentence("Do not feel so nervous you need to have confidence", "不要感到緊張，你需要有信心"));
                    sentencesList.Add(new Sentence("But I afraid that if I loss the contest my mom will be angry", "但我擔心，如果我輸掉比賽，媽媽會生氣"));
                    sentencesList.Add(new Sentence("Do not worry you need to over come the fear", "不要擔心你需要過度恐懼"));
                    sentencesList.Add(new Sentence("I will try my best to bit the depression", "我會盡力抑鬱"));
                    sentencesList.Add(new Sentence("I feel very happy now", "我現在感到很開心"));
                    sentencesList.Add(new Sentence("What happened something surprise", "什麼驚喜事情發生了嗎?"));
                    sentencesList.Add(new Sentence("Can you imagine that I win the contest", "你能想像我贏了比賽嗎"));
                    sentencesList.Add(new Sentence("Finally you do not burden yourself anymore", "最後你不再為自己增加負擔了"));
                    sentencesList.Add(new Sentence("Let me hail for you congratulations", "讓我高興地祝賀你"));

                    //sentencesList.Add(new Sentence("I am relieved to hear it", "我很高興聽到它"));
                    //sentencesList.Add(new Sentence("He scratched his arm", "他撓撓了手臂"));
                    //sentencesList.Add(new Sentence("She lives in comfort", "她過著舒適的生活"));
                    //sentencesList.Add(new Sentence("The fish is floating on the river", "魚漂浮在河上"));
                    //sentencesList.Add(new Sentence("The cook is sprinkling salt on the steak", "廚師在牛排上撒鹽"));
                    //sentencesList.Add(new Sentence("Smoking is banned for child", "兒童禁止吸煙"));
                    //sentencesList.Add(new Sentence("His house was destroyed by fire", "他的房子被火燒毀了"));
                    //sentencesList.Add(new Sentence("Do not despise everyone even you strong then them", "即使你強大，也不要鄙視每個人"));
                    //sentencesList.Add(new Sentence("His company merged yours", "他的公司合併了你的公司"));
                    //sentencesList.Add(new Sentence("If you want to win the game you need to overcome your weakness", "如果你想贏得比賽，你需要克服自己的弱點"));
                    //sentencesList.Add(new Sentence("A president decided to launch missile", "總統決定發射導彈"));
                    //sentencesList.Add(new Sentence("He will attach the sticker on the book", "他會在書上貼上貼紙"));
                    //sentencesList.Add(new Sentence("Establishing a country is impossible", "建立一個國家是不可能的"));
                    //sentencesList.Add(new Sentence("Her baby has a chubby face", "她的寶寶有一張胖呼呼的臉"));
                    //sentencesList.Add(new Sentence("My classmate is smelly", "我的同學很臭"));
                    //sentencesList.Add(new Sentence("There is a cat under the table", "桌下有一隻貓"));
                    //sentencesList.Add(new Sentence("Because he killed someone so he is guilty", "因為他殺了一個人，所以他很內疚"));
                    //sentencesList.Add(new Sentence("He is a big burden for us", "他是我們的重擔"));
                    break;
                case "林雨儒":
                    sentencesList.Add(new Sentence("Why does Tom look so happy", "為什麼湯姆看起來這麼開心?"));
                    sentencesList.Add(new Sentence("Because he won the lottery", "因為他贏了樂透"));
                    sentencesList.Add(new Sentence("Why Amy looks so sad", "為什麼艾米看起來這麼傷心?"));
                    sentencesList.Add(new Sentence("I do not know. Maybe we can ask and give her some comforts", "我不知道。或許我們可以詢問並且給她一些安慰"));
                    sentencesList.Add(new Sentence("I want to swim in this lake", "我想要在這個湖裡游泳"));
                    sentencesList.Add(new Sentence("But swimming is banned here we can go to the pool", "但是這裡禁止游泳"));
                    sentencesList.Add(new Sentence("We can wander in the park", "我們可以在公園裡漫走"));
                    sentencesList.Add(new Sentence("Why Abby does not eat meat", "為什麼艾比不吃肉?"));
                    sentencesList.Add(new Sentence("Because she is a vegetarian", "因為她是一位素食主義者"));
                    sentencesList.Add(new Sentence("I feel of sorrow because my dog passed away yesterday", "我覺得很傷心因為我的狗昨天死了"));
                    sentencesList.Add(new Sentence("I am sorry to hear that", "我很遺憾聽到這個"));
                    sentencesList.Add(new Sentence("I am sorry mom I failed the test", "媽我很抱歉。我的考試不及格"));
                    sentencesList.Add(new Sentence("It it ok honey if you study harder you will be a wisdom person a day", "寶貝沒關係。如果你更認真讀書"));
                    sentencesList.Add(new Sentence("I am afraid that I will fail the exam tomorrow", "我怕明天的考試我會不及格"));
                    sentencesList.Add(new Sentence("Have confidence in yourself you are smart", "對自己有信心! 你很聰明"));
                    sentencesList.Add(new Sentence("Son the salt in the kitchen is empty", "兒子!廚房的鹽巴空了"));
                    sentencesList.Add(new Sentence("I will go to the grocery store to buy a new one", "好! 我會去雜貨店買新的"));
                    sentencesList.Add(new Sentence("Where is the puppy", "小狗在哪裡?"));
                    sentencesList.Add(new Sentence("It is hiding under the bed", "牠躲在床底下"));

                    //sentencesList.Add(new Sentence("We have friend to comfort us", "我們有朋友能安慰我們"));
                    //sentencesList.Add(new Sentence("A balloon is floating in the sky", "有一個氣球正飄在天空"));
                    //sentencesList.Add(new Sentence("My little sister wants to wander in the park because she feels bored", "我的妹妹想要去公園漫走因為她覺得很無聊"));
                    //sentencesList.Add(new Sentence("Buying cigarettes is banned in Taiwan before you are eighteen", "在18歲前買香菸是被禁止的"));
                    //sentencesList.Add(new Sentence("Most difficulties can be overcome", "多數的困難可以被克服"));
                    //sentencesList.Add(new Sentence("The little girl looks cute with chubby cheeks", "那個有著胖胖臉頰的小女孩看起來很可愛"));
                    //sentencesList.Add(new Sentence("The company is bankrupt", "公司已經破產了"));
                    //sentencesList.Add(new Sentence("Praise is a mental encouragement", "表揚是一種精神上的鼓勵"));
                    //sentencesList.Add(new Sentence("Few of my friends are vegetarian", "我少數的朋友是素食主義者"));
                    //sentencesList.Add(new Sentence("He feels with anger and tiredness", "他感到生氣和疲累"));
                    //sentencesList.Add(new Sentence("He has had many sorrows so far", "到目前為止他經歷過種種不幸"));
                    //sentencesList.Add(new Sentence("He looks very happy because he won the lottery", "他看起來很開心因為他贏了樂透"));
                    //sentencesList.Add(new Sentence("Do not talk about others behind their backs", "不要在別人背後說他們的壞話"));
                    //sentencesList.Add(new Sentence("The information below was compiled by our team", "下方的資料是我們的團隊所整理的"));
                    //sentencesList.Add(new Sentence("The teacher stands in front of the students", "老師站在學生們面前"));
                    //sentencesList.Add(new Sentence("He is not only wisdom but brave", "他不只很明智也很勇敢"));
                    //sentencesList.Add(new Sentence("The surgery saved many people's lives in the world", "手術在世界上救了很多人的性命"));
                    //sentencesList.Add(new Sentence("He is full of confidence in this test", "他對這次的考試充滿了信心"));
                    //sentencesList.Add(new Sentence("My mom requires me go to the grocery to buy some eggs", "我的媽媽要求我去雜貨店買一些蛋"));
                    //sentencesList.Add(new Sentence("He lost all his property because of gambling", "他因為賭博輸了他全部的財產"));
                    break;
                case "徐芷羚":
                    sentencesList.Add(new Sentence("What did she do yesterday", "她昨天做了甚麼事"));
                    sentencesList.Add(new Sentence("She went to the grocery store to bought some eggs yesterday", "她昨天去雜貨店賣一些蛋"));
                    sentencesList.Add(new Sentence("Why is he so happy", "他為甚麼這開心"));
                    sentencesList.Add(new Sentence("He is happy because he got the first place", "他因為得到了第一名而感到開心"));
                    sentencesList.Add(new Sentence("Why is she chubby", "為何她這麼胖"));
                    sentencesList.Add(new Sentence("She is chubby because she eats a lot and doesn't like to exercise", "她胖胖的，因為她吃了很多而且不喜歡運動 "));
                    sentencesList.Add(new Sentence("What is he doing", "他正在做什麼"));
                    sentencesList.Add(new Sentence("He is talking to his maginary friend", "他正在和他的假想的朋友講話 "));
                    sentencesList.Add(new Sentence("What make he feel so sorrow", "什麼事讓他這麼傷心"));
                    sentencesList.Add(new Sentence("His cat death make him feel sorrow", "他的貓死了讓他感到悲傷"));
                    sentencesList.Add(new Sentence("How do those corals look", "那些珊瑚看起來如何"));
                    sentencesList.Add(new Sentence("Those corals look so beautiful", "那些珊瑚看起來很美"));
                    sentencesList.Add(new Sentence("What surprise him a lot", "什麼事讓他感到驚訝"));
                    sentencesList.Add(new Sentence("That he won the lottery surprise him a lot", "他贏得了樂透讓他感到驚訝"));
                    sentencesList.Add(new Sentence("Who is that girl behind you", "你背後的那女孩是誰"));
                    sentencesList.Add(new Sentence("The girl behind me is Kate", "我背後的女孩叫Kate"));
                    sentencesList.Add(new Sentence("Why is his hands so smelly", "為何他的手這麼臭"));
                    sentencesList.Add(new Sentence("Because he just cut the fish", "因為他剛切魚"));
                    sentencesList.Add(new Sentence("What does he likes to do", "他喜歡做什麼"));
                    sentencesList.Add(new Sentence("He likes to take pictures of the fish under the sea", "他喜歡拍海底下的魚"));

                    //sentencesList.Add(new Sentence("What did she do yesterday", "她昨天做了甚麼事"));
                    //sentencesList.Add(new Sentence("She went to the grocery store to bought some eggs yesterday", "她昨天去雜貨店賣一些蛋"));
                    //sentencesList.Add(new Sentence("Why is he so happy", "他為甚麼這開心"));
                    //sentencesList.Add(new Sentence("He is happy because he got the first place", "他因為得到了第一名而感到開心"));
                    //sentencesList.Add(new Sentence("Why is she chubby", "為何她這麼胖"));
                    //sentencesList.Add(new Sentence("She is chubby because she eats a lot and doesn't like to exercise", "她胖胖的，因為她吃了很多而且不喜歡運動 "));
                    //sentencesList.Add(new Sentence("What is he doing", "他正在做什麼"));
                    //sentencesList.Add(new Sentence("He is talking to his maginary friend", "他正在和他的假想的朋友講話 "));
                    //sentencesList.Add(new Sentence("What make he feel so sorrow", "什麼事讓他這麼傷心"));
                    //sentencesList.Add(new Sentence("His cat death make him feel sorrow", "他的貓死了讓他感到悲傷"));
                    //sentencesList.Add(new Sentence("How do those corals look", "那些珊瑚看起來如何"));
                    //sentencesList.Add(new Sentence("Those corals look so beautiful", "那些珊瑚看起來很美"));
                    //sentencesList.Add(new Sentence("What surprise him a lot", "什麼事讓他感到驚訝"));
                    //sentencesList.Add(new Sentence("That he won the lottery surprise him a lot", "他贏得了樂透讓他感到驚訝"));
                    //sentencesList.Add(new Sentence("Who is that girl behind you", "你背後的那女孩是誰"));
                    //sentencesList.Add(new Sentence("The girl behind me is Kate", "我背後的女孩叫Kate"));
                    //sentencesList.Add(new Sentence("Why is his hands so smelly", "為何他的手這麼臭"));
                    //sentencesList.Add(new Sentence("Because he just cut the fish", "因為他剛切魚"));
                    //sentencesList.Add(new Sentence("What does he likes to do", "他喜歡做什麼"));
                    //sentencesList.Add(new Sentence("He likes to take pictures of the fish under the sea", "他喜歡拍海底下的魚"));

                    //sentencesList.Add(new Sentence("She went to the grocery store to bought some eggs yesterday", "她昨天去雜貨店賣一些蛋"));
                    //sentencesList.Add(new Sentence("He is happy because he got the first place", "他因為得到了第一名而感到開心"));
                    //sentencesList.Add(new Sentence("Bats are mammals", "蝙蝠是哺乳動物"));
                    //sentencesList.Add(new Sentence("He was banned to talk in the class", "他被禁止在課堂上講話"));
                    //sentencesList.Add(new Sentence("She is chubby because she eats a lot", "Sherry胖胖的，她很胖，因為她吃了很多"));
                    //sentencesList.Add(new Sentence("He have an imaginary friend", "他有一個假想的朋友"));
                    //sentencesList.Add(new Sentence("His cat death make him feel sorrow", "他的貓死了讓他感到悲傷"));
                    //sentencesList.Add(new Sentence("His hands are smelly after cutting the fish", "他的手在切魚之後變得很臭"));
                    //sentencesList.Add(new Sentence("She comfort her sister by giving her a hug", "她以擁抱來安慰她妹妹"));
                    //sentencesList.Add(new Sentence("The sparks of fireworks are beautiful", "煙火的火花很美"));
                    //sentencesList.Add(new Sentence("My grandfather sprinkle some water on the flower", ""));
                    //sentencesList.Add(new Sentence("She does not know how to float on the water", "她不知道如何漂浮在水面上"));
                    //sentencesList.Add(new Sentence("He overcomes the fear of diving", "他克服了對潛水的恐懼"));
                    //sentencesList.Add(new Sentence("Those colorful corals are beautiful", "那些五顏六色的珊瑚很美"));
                    //sentencesList.Add(new Sentence("He likes to take pictures of the fish under the sea", "他喜歡拍海底下的魚"));
                    //sentencesList.Add(new Sentence("The doctor is performing a surgery for him", "醫生正在幫他做手術"));
                    //sentencesList.Add(new Sentence("He launch a rocket", "他發射了火箭"));
                    //sentencesList.Add(new Sentence("There are many clouds over the sky", "天空中有許多雲"));
                    //sentencesList.Add(new Sentence("He won the lottery surprise him a lot", "他贏得了樂透讓他感到驚訝"));
                    //sentencesList.Add(new Sentence("The girl behind him is tall", "他背後的女孩很高"));
                    break;
                case "張哲維":
                    sentencesList.Add(new Sentence("The little boy looks chubby", "小男孩看起來很胖"));
                    sentencesList.Add(new Sentence("This room is very smelly", "這個房間很臭"));
                    sentencesList.Add(new Sentence("There is a unidentified chopper flying around", "有一個身份不明的菜刀飛來飛去"));
                    sentencesList.Add(new Sentence("The man is guilty", "這個男人很內疚"));
                    sentencesList.Add(new Sentence("This restaurant is bankrupt", "這家餐廳破產了"));
                    sentencesList.Add(new Sentence("He has mental problem", "他有精神問題"));
                    sentencesList.Add(new Sentence("My mom walked in room and full with anger", "我媽媽走進房間，充滿了憤怒"));
                    sentencesList.Add(new Sentence("He always looks happy", "他看起來總是很開心"));
                    sentencesList.Add(new Sentence("We're preparing a surprise party", "我們正準備一個驚喜派對"));
                    sentencesList.Add(new Sentence("I found a book under my desk", "我在桌子底下找了一本書"));
                    sentencesList.Add(new Sentence("Sometimes you just need to get over it", "有時你只需要克服它"));
                    sentencesList.Add(new Sentence("My car is destroyed in the crash", "我的車在車禍中被摧毀"));
                    sentencesList.Add(new Sentence("He is a vegetarian", "他是素食主義者"));
                    sentencesList.Add(new Sentence("This house is my only property", "這所房子是我唯一的財產"));
                    sentencesList.Add(new Sentence("I need to buy some grocery", "我需要買一些雜貨"));
                    sentencesList.Add(new Sentence("The little boy feel comfort when his mother pacify him", "當他的母親安撫他時，小男孩感到安慰"));
                    sentencesList.Add(new Sentence("A lot of things are banned in China", "中國有很多事情被禁止"));
                    sentencesList.Add(new Sentence("He likes to wandering around", "他喜歡四處閒逛"));
                    sentencesList.Add(new Sentence("They are launching the rocket", "他們正在發射火箭"));
                    break;
                case "許雯琳":
                    sentencesList.Add(new Sentence("You should overcome your fear", "你應該克服恐懼"));
                    sentencesList.Add(new Sentence("I try my best", "我盡力了"));
                    sentencesList.Add(new Sentence("Where is the park", "公園在哪裡"));
                    sentencesList.Add(new Sentence("It is behind of the grocery", "這是雜貨店的背後"));
                    sentencesList.Add(new Sentence("How is the food smell", "食物味道怎麼樣"));
                    sentencesList.Add(new Sentence("It is smell smelly", "聞起來很臭"));
                    sentencesList.Add(new Sentence("How are you feeling now", "你現在感覺如何"));
                    sentencesList.Add(new Sentence("I feeling happy", "我感到高興"));
                    sentencesList.Add(new Sentence("Where is my bag", "我的包在哪兒"));
                    sentencesList.Add(new Sentence("Why do you look so sorrow", "為什麼你看起來很悲傷"));
                    sentencesList.Add(new Sentence("Because my pet is missing", "因為我的寵物不見了"));
                    sentencesList.Add(new Sentence("How about this surgery", "這個手術怎麼樣"));
                    sentencesList.Add(new Sentence("Very success", "非常成功"));
                    sentencesList.Add(new Sentence("Who destory the monument", "誰毀壞了紀念"));
                    sentencesList.Add(new Sentence("It must be the malicious people", "一定是惡意的人"));
                    sentencesList.Add(new Sentence("What kind of mammal is yellow and have black tattoo", "什麼樣的哺乳動物是黃色的，有黑色紋身"));
                    sentencesList.Add(new Sentence("It is tiger", "這是老虎"));
                    sentencesList.Add(new Sentence("I am very afraid of speaking on stage", "我非常害怕在舞台上發言"));
                    sentencesList.Add(new Sentence("You have to be more confident", "你必須更自信"));

                    //sentencesList.Add(new Sentence("The chubby baby is lovely", "胖乎乎的寶寶很可愛"));
                    //sentencesList.Add(new Sentence("The garbage is smelly", "垃圾很臭"));
                    //sentencesList.Add(new Sentence("He was found guilty of robbery", "他被判犯有搶劫罪"));
                    //sentencesList.Add(new Sentence("They wander in the park", "他們在公園裡徘徊"));
                    //sentencesList.Add(new Sentence("She finally overcome her fear", "她終於克服了她的恐懼"));
                    //sentencesList.Add(new Sentence("The boat float down the river", "船漂浮在河上"));
                    //sentencesList.Add(new Sentence("She feels sorrow about his death", "她對他的死感到悲傷"));
                    //sentencesList.Add(new Sentence("He is confident that he can win the race", "他有信心能贏得比賽"));
                    //sentencesList.Add(new Sentence("Grocery is seldom see in the city", "這個城市很少看到雜貨店"));
                    //sentencesList.Add(new Sentence("Tiger is a kind of mammal", "老虎是一種哺乳動物"));
                    //sentencesList.Add(new Sentence("There is a convenience store behind the park", "公園後面有一家便利店"));
                    //sentencesList.Add(new Sentence("Corals  have been polluted", "珊瑚受到污染"));
                    //sentencesList.Add(new Sentence("She is a vegetarian", "她是素食主義者"));
                    //sentencesList.Add(new Sentence("He despises people joking about him", "他鄙視人們開玩笑的說他"));
                    //sentencesList.Add(new Sentence("The billionaire went bankrupt", "這位億萬富翁破產了"));
                    //sentencesList.Add(new Sentence("Children are sweet burden to their parents", "孩子是父母的甜蜜負擔"));
                    //sentencesList.Add(new Sentence("This surgery was a great success", "這次手術取得了巨大成功"));
                    //sentencesList.Add(new Sentence("He launched this charity event", "他發起了這個慈善活動"));
                    //sentencesList.Add(new Sentence("The monument was destroyed by the malicious person", "紀念碑被惡意的人摧毀了"));
                    //sentencesList.Add(new Sentence("He has more wisdom than ordinary people", "他比普通人更有智慧"));
                    break;
                case "黃皓":
                    sentencesList.Add(new Sentence("I am relieved to hear it", "我很高興聽到它"));
                    sentencesList.Add(new Sentence("He scratched his arm", "他撓撓了手臂"));
                    sentencesList.Add(new Sentence("She lives in comfort", "她過著舒適的生活"));
                    sentencesList.Add(new Sentence("The fish is floating on the river", "魚漂浮在河上"));
                    sentencesList.Add(new Sentence("The cook is sprinkling salt on the steak", "廚師在牛排上撒鹽"));
                    sentencesList.Add(new Sentence("Smoking is banned for child", "兒童禁止吸煙"));
                    sentencesList.Add(new Sentence("His house was destroyed by fire", "他的房子被火燒毀了"));
                    sentencesList.Add(new Sentence("Do not despise everyone even you strong then them", "即使你強大，也不要鄙視每個人"));
                    sentencesList.Add(new Sentence("His company merged yours", "他的公司合併了你的公司"));
                    sentencesList.Add(new Sentence("If you want to win the game you need to overcome your weakness", "如果你想贏得比賽，你需要克服自己的弱點"));
                    sentencesList.Add(new Sentence("A president decided to launch missile", "總統決定發射導彈"));
                    sentencesList.Add(new Sentence("He will attach the sticker on the book", "他會在書上貼上貼紙"));
                    sentencesList.Add(new Sentence("Establishing a country is impossible", "建立一個國家是不可能的"));
                    sentencesList.Add(new Sentence("Her baby has a chubby face", "她的寶寶有一張胖呼呼的臉"));
                    sentencesList.Add(new Sentence("My classmate is smelly", "我的同學很臭"));
                    sentencesList.Add(new Sentence("There is a cat under the table", "桌下有一隻貓"));
                    sentencesList.Add(new Sentence("Because he killed someone so he is guilty", "因為他殺了一個人，所以他很內疚"));
                    sentencesList.Add(new Sentence("He is a big burden for us", "他是我們的重擔"));
                    break;
                case "楊耀輝":
                    sentencesList.Add(new Sentence("Could you helped me overcome this difficulty", "你可以幫助我克服這個難關嗎？"));
                    sentencesList.Add(new Sentence("Of course so do not be sorrow anymore", "當然可以"));
                    sentencesList.Add(new Sentence("Did you see that he was really anger with that girl", "哇你看到了嗎？他真的在生那個女生的氣"));
                    sentencesList.Add(new Sentence("I know why it is because the girl stole his money", "我知道為什麼，因為那個女生偷了他的錢"));
                    sentencesList.Add(new Sentence("I think she has really got a lot of startled", "嘿，我想她受到了很大的驚嚇"));
                    sentencesList.Add(new Sentence("I think so too", " do you want to go to comfort her"));
                    sentencesList.Add(new Sentence("Have you seem the container on my desk it is gone", "你有看到我桌上的那個容器嗎？它不見了"));
                    sentencesList.Add(new Sentence("It is under my table", "它在我的桌子底下"));
                    sentencesList.Add(new Sentence("Do you want to wander around that hail", "你想要去那個山丘附近閒晃嗎？"));
                    sentencesList.Add(new Sentence("I have some pressure need to relieve take a walk may be a good idea", "我有些壓力需要釋放，散個步也許是個好主意"));
                    sentencesList.Add(new Sentence("That building was established by me", "那個建築物是我建造的"));
                    sentencesList.Add(new Sentence("I feel very surprised", "哇！我感到很驚訝！"));
                    sentencesList.Add(new Sentence("Look at that mamal it", "看看那隻哺乳類動物！"));
                    sentencesList.Add(new Sentence("I think so too and it is very chubby", "我也覺得！而且它好胖"));
                    sentencesList.Add(new Sentence("Do you want to see the fireworks launch", "你想去看煙火發射嗎？"));
                    sentencesList.Add(new Sentence("Sure I like the spark of fireworks", "當然，我喜歡煙火的火花"));
                    sentencesList.Add(new Sentence("I dreamed last night that I was floating in the universe", "我昨晚夢到我漂浮在宇宙"));
                    sentencesList.Add(new Sentence("You are very full of imaginary", "哇，你還真有想像力"));
                    sentencesList.Add(new Sentence("Let we bet", " the loser need to destroy the her toy"));
                    sentencesList.Add(new Sentence("No this will make me feel very guilty", "不要，這樣會讓我很有罪惡感"));

                    //sentencesList.Add(new Sentence("I feel very sorrow because my friend is angry with me", "我感到很傷心因為我的朋友在生我的氣"));
                    //sentencesList.Add(new Sentence("My friend helped me overcome this difficulty", "我的朋友幫助我克服這個困難"));
                    //sentencesList.Add(new Sentence("My brother eats too much fast food so he becomes very chubby", "我的哥哥吃了太多速食，所以他變得很胖"));
                    //sentencesList.Add(new Sentence("The book you are looking for is under his desk", "你在找的那本書在他的桌子底下"));
                    //sentencesList.Add(new Sentence("The girl in front of you looks uncomfortable", "你前面的那個女生看起來不太舒服"));
                    //sentencesList.Add(new Sentence("The boy is very happy because his parents buying toys for him", "那個小男孩很開心因為他的父母買玩具給他"));
                    //sentencesList.Add(new Sentence("Life jackets can help us float on the water", "救生衣可以幫助我們浮在水面上"));
                    //sentencesList.Add(new Sentence("Coral bleaching caused by global warming", "全球暖化導致珊瑚白化"));
                    //sentencesList.Add(new Sentence("I scratched my head because I feel itchy", "我幫我的頭抓癢因為我覺得很癢"));
                    //sentencesList.Add(new Sentence("I feel very guilty because I stole his money", "我覺得非常有罪惡感因為我偷了他的錢"));
                    //sentencesList.Add(new Sentence("The man went bankrupt because he was addicted to gambling", "那個男人因為沉迷賭博所以破產了"));
                    //sentencesList.Add(new Sentence("The cake I made was destroyed by my dog", "我做的蛋糕被我的狗摧毀了"));
                    //sentencesList.Add(new Sentence("The car is my property", "那輛車是我的資產"));
                    //sentencesList.Add(new Sentence("My aunt is a vegetarian", "我阿姨是個素食主義者"));
                    //sentencesList.Add(new Sentence("Fireworks will produce beautiful sparks", "煙火會製造美麗的火花"));
                    //sentencesList.Add(new Sentence("This responsibility is too great it is a big burden for me", "這個責任太重大了，對我來說是個很大的負擔"));
                    //sentencesList.Add(new Sentence("When I am sad my sister will always come to comfort me", "當我難過的時候，我的姐姐總是會來安慰我"));
                    //sentencesList.Add(new Sentence("This surgery must be performed by have highly skilled doctor", "這個手術必須由擁有高技術的醫生來執行"));
                    //sentencesList.Add(new Sentence("I need a bigger container to hold the water", "我需要更大的容器來裝這些水"));
                    //sentencesList.Add(new Sentence("My parents are very angry that I have not got good grades", "我的爸媽非常生氣因為我沒拿到好成績"));
                    break;
                case "聶維君":
                    sentencesList.Add(new Sentence("Are you happy", "你快樂嗎?"));
                    sentencesList.Add(new Sentence("Yes I am happy", "是的，我很開心"));
                    sentencesList.Add(new Sentence("Did you see the novel that I put under the pencil case", "你有看到我把這個小說放在鉛筆盒下嗎?"));
                    sentencesList.Add(new Sentence("No I did not", "不，我沒有"));
                    sentencesList.Add(new Sentence("Who is the woman stand in front of us", "站在我們面前的女人是誰?"));
                    sentencesList.Add(new Sentence("She is my piano teacher", "她是我的鋼琴老師"));
                    sentencesList.Add(new Sentence("What is the color of coral", "珊瑚的顏色是什麼？"));
                    sentencesList.Add(new Sentence("It is white and it also have another colors", "它是白色的，還有另一種顏色"));
                    sentencesList.Add(new Sentence("Are you a vegetarian", "你是素食主義者嗎？ "));
                    sentencesList.Add(new Sentence("No I usually eat meat", "不，我經常吃肉"));
                    sentencesList.Add(new Sentence("What did you do yesterday", "你昨天做了什麼？ "));
                    sentencesList.Add(new Sentence("I wandered at the park with my parents", "我和父母在公園裡閒逛，"));
                    sentencesList.Add(new Sentence("What do you do if you feel sorrow", "如果你感到悲傷，你會怎麼做?"));
                    sentencesList.Add(new Sentence("I usually listen music", "我經常聽音樂，"));
                    sentencesList.Add(new Sentence("When was this foundation established", "這個基金會什麼時候成立？"));
                    sentencesList.Add(new Sentence("It was established from ten years ago", "它建立於十年前，"));
                    sentencesList.Add(new Sentence("What is his job", "他的工作是什麼？"));
                    sentencesList.Add(new Sentence("He was a professor of surgery in the state university", "他是州立大學的外科教授，"));
                    sentencesList.Add(new Sentence("Do you think we can overcome the difficulties", "你認為我們能克服困難嗎？"));
                    sentencesList.Add(new Sentence("Yes we can overcome any difficulty however great it is", "是的，我們可以克服任何困難，無論它多麼偉大"));

                    //sentencesList.Add(new Sentence("The woman standing in front of me is my piano teacher", "站在我前面的女人是我的鋼琴老師"));
                    //sentencesList.Add(new Sentence("I saw a boy was played in the park over the river", "我看到一個男孩在越過河流的公園裡玩耍"));
                    //sentencesList.Add(new Sentence("My classmate is a vegetarian so he never eat meat", "我同學是個素食者，所以他從不吃肉"));
                    //sentencesList.Add(new Sentence("I put the paper under the novel", "我把紙放在小說底下"));
                    //sentencesList.Add(new Sentence("She felt sorrow because she did not get the high score in the test", "她因為沒有拿到好成績而感到悲傷"));
                    //sentencesList.Add(new Sentence("There are many mammals in the zoo", "動物園裡有很多的哺乳類動物"));
                    //sentencesList.Add(new Sentence("I wandered at the park with my parents last weekend", "上個週末，我和我父母一起在公園裡漫步"));
                    //sentencesList.Add(new Sentence("My brother was chubby when he was a child", "我哥哥在年幼時身材圓圓胖胖的"));
                    //sentencesList.Add(new Sentence("We can put lots of things in the container", "我們放了很多東西在容器裡"));
                    //sentencesList.Add(new Sentence("The reason that why her visual getting worse is because she always using cellphone", "因為她一直使用手機導致她的視力逐漸下降"));
                    //sentencesList.Add(new Sentence("My dad was angered after he knew I failed the test", "在爸爸得知我考砸了考試後，他感到非常生氣"));
                    //sentencesList.Add(new Sentence("The house was destroyed by the typhoon", "房子被颱風摧毀了"));
                    //sentencesList.Add(new Sentence("He established a foundation", "他建立了一個基金會"));
                    //sentencesList.Add(new Sentence("She overcomes fear of heights", "她克服了恐高"));
                    //sentencesList.Add(new Sentence("Many foreigners felt that our stinky tofu is smelly", "很多外國人覺得我們的臭豆腐很難聞"));
                    //sentencesList.Add(new Sentence("The company was bankrupt because it does not have funds", "這間公司因為沒有資金而倒閉"));
                    //sentencesList.Add(new Sentence("I feel very happy because I will go out with my family tomorrow", "因為明天要和家人出去，所以我很開心"));
                    //sentencesList.Add(new Sentence("The little girl was very surprised after she saw her dad bought her an ice cream", "小女孩在看到爸爸買給她冰淇淋時感到非常驚喜"));
                    //sentencesList.Add(new Sentence("I have a plan for overcoming our difficulties", "我有個方法可以克服困難"));
                    //sentencesList.Add(new Sentence("She has great confidence in her success", "她對自己成功有很大的信心"));
                    break;
                case "徐紫瑜":
                    sentencesList.Add(new Sentence("Do you see that the boy who having guilty on his face", "你有看到臉上充滿罪惡表情的男孩嗎?"));
                    sentencesList.Add(new Sentence("Is that chubby boy in front of us", "是那個站在我們前面肥肥的男孩嗎?"));
                    sentencesList.Add(new Sentence("To support us dad bears a great burden to earn money", "為了扶養我們，爸爸承受極大的負擔賺錢。"));
                    sentencesList.Add(new Sentence("I think he should relieve himself for a while", "沒錯。我想他應該要放鬆一陣子。"));
                    sentencesList.Add(new Sentence("The building had been established for ten years but where is it now", "這棟建築物已經建立十年了，那現在它在哪?"));
                    sentencesList.Add(new Sentence("It was removed and became a banned area", "它被移除了，成了禁區。 "));
                    sentencesList.Add(new Sentence("She tried many times for the chance of promotion so I have confidence in her", "蘇珊為了升遷機會而努力多次，所以我對她很有信心。"));
                    sentencesList.Add(new Sentence("No wonder she looks so happy", "難怪她看起來這麼開心。"));
                    sentencesList.Add(new Sentence("Why dis he go bankrupt", "為什麼他會破產?"));
                    sentencesList.Add(new Sentence("Because he injected all property to gamble", "因為他在賭博投下所有的財產。"));

                    //sentencesList.Add(new Sentence("He wants to relieve himself", "歷經一番波折後他想要好好休息"));
                    //sentencesList.Add(new Sentence("That banned area is my alma mater", "我的母校現在是禁區"));
                    //sentencesList.Add(new Sentence("The enormous typhoon destroyed through Taiwan causing a big disaster", "巨大的颱風肆虐整個台灣 造成很大的災害"));
                    //sentencesList.Add(new Sentence("We will encounter hardship in life but we still have to overcome", "在人生裡會遇到重重的困難，我們還是必須去克服"));
                    //sentencesList.Add(new Sentence("There was a building which established in ten years ago", "十年前建立了一座建築物"));
                    //sentencesList.Add(new Sentence("When he do the wrong thing he always having guilty on his face", "當他做錯事時，臉上總會有罪惡的表情"));
                    //sentencesList.Add(new Sentence("After a serious accident he has visual problem that bring him a lot of depressions", "他在一場嚴重的車禍後，視力上的問題帶給他許多憂傷"));
                    //sentencesList.Add(new Sentence("He has mental problem", "他有精神問題"));
                    //sentencesList.Add(new Sentence("People do not want to reveal about individual to stranger for their first meet", "人們不想要向第一次見面的陌生人透漏太多個人隱私。"));
                    //sentencesList.Add(new Sentence("The guy stand behind the woman seems to be stealing her bag", "站在那女子後方的人似乎想偷走她的包包"));
                    //sentencesList.Add(new Sentence("The boy wants to pick the ball up which below the car", "男孩想要撿出在車底下的球"));
                    //sentencesList.Add(new Sentence("My father bears a great burden to earn money", "為了撫養我們，我爸承受極大的負擔賺錢"));
                    //sentencesList.Add(new Sentence("Her tried many times for the chance of promotion so I have confidence in her", "他為了升遷機會而努力多次，所以我對她很有信心"));
                    //sentencesList.Add(new Sentence("The firework spark in the sky of the night", "煙花在夜晚的天空中點燃"));
                    //sentencesList.Add(new Sentence("He want to take the property at all means which makes him heartbroken", "他想以一切手段奪取財產，這讓他傷心欲絕"));
                    //sentencesList.Add(new Sentence("The owner of grocery is my grandma", "雜貨店的老闆是我的奶奶"));
                    //sentencesList.Add(new Sentence("He injected too much money to gamble which caused him go bankrupt", "他注入太多金錢在賭博上而導致他破產"));
                    //sentencesList.Add(new Sentence("Knowing my relative who died in the accident makes me grieving all the month", "得知我的親戚死於車禍讓我傷心一整個月"));
                    //sentencesList.Add(new Sentence("The police decided to launch an investigation to find the murderer", "警察決定展開調查找出兇手"));
                    //sentencesList.Add(new Sentence("All the roles in the story are imaginary but kids think that  they are real", "在這故事裡的所有角色都是虛構的，但孩子們都以為是真的。"));
                    break;
                case "游淨詒":
                    sentencesList.Add(new Sentence("Why he looks so sad", "他為什麼看起來那麼傷心 "));
                    sentencesList.Add(new Sentence("Because he felt sorrow at the death of her wife", "因為他對於他老婆的死很悲傷 "));
                    sentencesList.Add(new Sentence("Why she looks so happy", "她為什麼看起來那麼開心 "));
                    sentencesList.Add(new Sentence("Because she was very happy when he heard the result", "因為當她聽到結果時她很開心 "));
                    sentencesList.Add(new Sentence("The little pony looks so exhausted", "那隻小馬看起來很疲憊 "));
                    sentencesList.Add(new Sentence("Because during the trip the little pony struggled under its burden", "因為在旅途中這隻小馬身上揹了許多負擔  "));
                    sentencesList.Add(new Sentence("Why you do not believe me", "你為什麼不相信我 "));
                    sentencesList.Add(new Sentence("What you call a fact is just his imaginary", "你所謂的事實只是他所想像的 "));
                    sentencesList.Add(new Sentence("Why he broke up with her", "為什麼他跟她提分手 "));
                    sentencesList.Add(new Sentence("Because he said that she has a serious mental disorder", "因為她有嚴重的的精神疾病"));

                    //sentencesList.Add(new Sentence("I sprinkled some pepper on the top of dish", "我在菜餚的頂端撒上一些胡椒"));
                    //sentencesList.Add(new Sentence("He felt sorrow at the death of her wife", "Mr"));
                    //sentencesList.Add(new Sentence("He was very happy when he heard the result", "當他知道結果時他非常開心"));
                    //sentencesList.Add(new Sentence("She was surprised at such a develope", "她對這樣的發展感到驚訝。"));
                    //sentencesList.Add(new Sentence("The little pony struggled under its burden", "這隻小馬身上揹了許多負擔"));
                    //sentencesList.Add(new Sentence("The old man spent whole day wandering around the beach", "這位老人花了一整天的時間在海邊漫步"));
                    //sentencesList.Add(new Sentence("Eventually he managed to overcome his nervous on stage", "他最終克服了在台上的緊張"));
                    //sentencesList.Add(new Sentence("The ship came down in the ocean", "這艘船沉下了海面"));
                    //sentencesList.Add(new Sentence("She usually wears skirts below her knees", "她通常都穿著過膝蓋的裙子"));
                    //sentencesList.Add(new Sentence("What you call a fact is just his imaginary", "你所謂的事實只是他所想像"));
                    //sentencesList.Add(new Sentence("These family recently become vegetarians", "這家人最近變成素食主義者"));
                    //sentencesList.Add(new Sentence("Her family has a history of mental disorder", "她的家族有遺傳性的精神疾病"));
                    //sentencesList.Add(new Sentence("The teacher attached a photo on his book", "老師在他的本子上貼上一張相片"));
                    //sentencesList.Add(new Sentence("Gas station should be ban on smoking", "加油站應該禁止吸菸"));
                    //sentencesList.Add(new Sentence("In the end of test the students let the balloons float into the sky", "在考試結束後學生們讓氣球飛上天空"));
                    //sentencesList.Add(new Sentence("This area has a lot of coral because here are not polluted", "這個區域有很多的珊瑚因為這裡沒有受到汙染"));
                    //sentencesList.Add(new Sentence("The old man does not have any property to live", "這個老人沒有任何的財產讓他生活"));
                    //sentencesList.Add(new Sentence("There are many wisdom students join this game", "那裏有很多聰明的學生來參加這個遊戲"));
                    //sentencesList.Add(new Sentence("This chubby boy very like eats fried foods", "這個肥胖的男孩很愛吃油炸的食物"));
                    //sentencesList.Add(new Sentence("I think we need to hail a taxi", "我想我們應該召一台計程車"));
                    break;
                case "劉禹萱":
                    sentencesList.Add(new Sentence("She is sits in front of me because her grades is better than me", "她坐在我前面因為他的成績比我好"));
                    sentencesList.Add(new Sentence("I am happy that you can open your mind", "我很高興你能敞開心房"));
                    sentencesList.Add(new Sentence("Daddy is angry with me went home too late", "爸爸非常生氣因為我太晚回家了"));
                    sentencesList.Add(new Sentence("There is a grocery in the corner near my home", "我家附近的轉角開了一間雜貨店"));
                    sentencesList.Add(new Sentence("I wander why she is filled with sorrow on her face", "我想知道為什麼她的臉上充滿了憂傷"));
                    sentencesList.Add(new Sentence("My pencil dropped on the floor under my desk", "我的筆掉到我的書桌底下了"));
                    sentencesList.Add(new Sentence("We surprised that she is a vegetarian", "我們很驚訝她是個素食主義者"));
                    sentencesList.Add(new Sentence("I am feel guilty when I told a lie", "說謊時我覺得有罪惡感"));
                    sentencesList.Add(new Sentence("Every child is parent burden and responsibility", "每個孩子都是父母的負擔和責任"));
                    sentencesList.Add(new Sentence("I have confidence in this competition", "我對這場比賽有信心"));

                    //sentencesList.Add(new Sentence("Fireworks are spark as brightly as star in the sky", "煙火如繁星般閃耀在空中"));
                    //sentencesList.Add(new Sentence("He has a lot of imaginary friends", "他有很多虛構的朋友"));
                    //sentencesList.Add(new Sentence("Our live have many difficulties that we need to overcome", "我們的人生有很多難關需要去克服"));
                    //sentencesList.Add(new Sentence("He is so surprise about our birthday gift for him", "他對於我們送他的生日禮物感到很驚訝"));
                    //sentencesList.Add(new Sentence("I have individual presentation to report on next Thursday", "下禮拜二我要發表我的個人簡報"));
                    //sentencesList.Add(new Sentence("Teachers usually be angry because our behaviors", "老師常常因為我們的行為而生氣"));
                    //sentencesList.Add(new Sentence("People should have confidence", "人們應該要有自信"));
                    //sentencesList.Add(new Sentence("She seat is in front of me", "她的座位在我們的前面"));
                    //sentencesList.Add(new Sentence("Our school has a lot of vegetarian", "我們學校有很多的素食主義者"));
                    //sentencesList.Add(new Sentence("He is filled with sorrow on her face", "他的臉上充滿了憂傷"));
                    //sentencesList.Add(new Sentence("My mom always comfort me when I was sad", "我媽媽總是在我難過的時候安慰我"));
                    //sentencesList.Add(new Sentence("Children be happy with Christmas gifts", "孩子們對於聖誕禮物感到很高興"));
                    //sentencesList.Add(new Sentence("Most young people have a heavy burden when they first start working", "大部分的年輕人在剛出社會的時候覺得負擔很重"));
                    //sentencesList.Add(new Sentence("Children be happy with Christmas gifts", ""));
                    //sentencesList.Add(new Sentence("Most young people have a heavy burden when they first start working", ""));
                    break;
                case "馬偉欣":
                    sentencesList.Add(new Sentence("How is your feeling while floating on the water", "當你在水上漂浮時"));
                    sentencesList.Add(new Sentence("I am happy when floating on the water", "我感到非常開心"));
                    sentencesList.Add(new Sentence("What do you relieve pressure when you feel down?", "當你感到失落時"));
                    sentencesList.Add(new Sentence("Eating", "我會吃東西。"));
                    sentencesList.Add(new Sentence("No wonder you are so chubby", "難怪你如此圓潤。"));
                    sentencesList.Add(new Sentence("Are there any corals under the ocean", "海底下有任何珊瑚嗎？"));
                    sentencesList.Add(new Sentence("Definitely there are also mammals under the ocean", "當然"));
                    sentencesList.Add(new Sentence("My friends banned me from gambling or I would be bankrupt", "我朋友禁止我繼續賭博"));
                    sentencesList.Add(new Sentence("I agree with your friends", "我同意。"));
                    sentencesList.Add(new Sentence("Take a walk in front of our house", "在我們家前面散個步吧！"));
                    sentencesList.Add(new Sentence("Great wandering near the park is another choice", "太棒了"));
                    sentencesList.Add(new Sentence("Establishing a company by ourselves is difficult", "自己創業這回事是相當困難的。"));
                    sentencesList.Add(new Sentence("You are right", " we need to overcome many obstacles"));
                    sentencesList.Add(new Sentence("What is wrong with her She looks like to be full of sorrow", "她怎麼了？她看起來情緒低落。"));
                    sentencesList.Add(new Sentence("Maybe we need to give her some comfort", "也許我們應該安慰她一下。"));
                    sentencesList.Add(new Sentence("Are you grieving", "你很悲傷嗎？"));
                    sentencesList.Add(new Sentence("Yes I am grieving", "是的。"));
                    sentencesList.Add(new Sentence("Do you know where the vegetarian restaurant is", "你知道哪裡有素食餐廳嗎？"));
                    sentencesList.Add(new Sentence("Yes it is behind the convenient store", "就在那間便利商店的旁邊。"));
                    sentencesList.Add(new Sentence("How does your cat when it is scratched", "當你的貓被搔癢時"));
                    sentencesList.Add(new Sentence("It often feels happy and comfortable except when stroking its legs", "它會很開心且感到舒服"));
                    sentencesList.Add(new Sentence("If I touch its legs", " it will be full of anger"));
 
                    //sentencesList.Add(new Sentence("A building was established ten years ago", "十年前建立了一座建築 "));
                    //sentencesList.Add(new Sentence("She often relieves pressure by listening to music", "她經常通過聽音樂來緩解壓力"));
                    //sentencesList.Add(new Sentence("His habit of gambling made him bankrupt", "他賭博的習慣使他破產"));
                    //sentencesList.Add(new Sentence("The little girl loves eating sweets so that she is chubby", "小女孩喜歡吃甜食，所以她很胖"));
                    //sentencesList.Add(new Sentence("You are vegetarian", "你是素食主義者"));
                    //sentencesList.Add(new Sentence("The old parents always wander in the park near their house", "老父母總是在他們家附近的公園裡閒逛"));
                    //sentencesList.Add(new Sentence("The unicorn is an imaginary being that looks like a horse", "獨角獸是一個看起來像馬的想像中的存在"));
                    //sentencesList.Add(new Sentence("Mom told me that I need to comfort my sisters", "媽媽告訴我，我需要安慰我的姐妹們 "));
                    //sentencesList.Add(new Sentence("Scientists say that there must be many living beings under the water", "科學家說，水下必定有許多生物"));
                    //sentencesList.Add(new Sentence("Many people are overwhelming from depression these years", "這些年來，很多人都患有抑鬱症"));
                    //sentencesList.Add(new Sentence("Cats are mammals but snakes are not", "貓是哺乳動物，但蛇不是 "));
                    //sentencesList.Add(new Sentence("She was banned from the athletics for five years", "她被禁止參加田徑運動五年 "));
                    //sentencesList.Add(new Sentence("His courage gave her more confidence", "他的勇氣讓她更有信心"));
                    //sentencesList.Add(new Sentence("Lots of people make surprise on birthday party", "很多人在生日聚會上大吃一驚"));
                    //sentencesList.Add(new Sentence("Monsters usually destroy buildings and houses", "怪物通常會破壞建築物和房屋 "));
                    //sentencesList.Add(new Sentence("It was said that the two businesses would be merged", "據說兩家公司將合併"));
                    //sentencesList.Add(new Sentence("The note is attached to this document which is sent to boss", "該說明附於本文件，該文件已發送給老闆 "));
                    //sentencesList.Add(new Sentence("The old man left his property to his son這位老人把他的財產留給了他的兒子", " "));
                    //sentencesList.Add(new Sentence("There is a garden in front of our house", "我們家門前有一個花園。 "));
                    //sentencesList.Add(new Sentence("My brother was full of sorrow", "我的兄弟充滿了悲傷"));
                    break;

                #endregion


                #region 預設句子
                default:
                    sentencesList.Add(new Sentence("Relieve the pain from your headache", "減輕頭痛帶來的痛苦"));
                    sentencesList.Add(new Sentence("He scratched his head", "他撓了撓頭"));
                    sentencesList.Add(new Sentence("The mother comforted her crying baby", "母親安慰她哭鬧的嬰兒"));
                    sentencesList.Add(new Sentence("The raft float out to sea", "木筏飄向大海"));
                    sentencesList.Add(new Sentence("The last step in the recipe is to sprinkle some sea salt over the cookies", "食譜的最後一步是灑一些海鹽在餅乾上"));
                    sentencesList.Add(new Sentence("He wandered around the store", "他在商店裡閒逛"));
                    sentencesList.Add(new Sentence("Swimming in banned in this lake", "在這個湖禁止游泳"));
                    sentencesList.Add(new Sentence("The singer destroyed her reputation", "這位歌手毀掉了她的名聲"));
                    sentencesList.Add(new Sentence("Why do you despise that politician", "你為什麼鄙視那個政客?"));
                    sentencesList.Add(new Sentence("The sea and the sky merged", "海與天合併"));
                    sentencesList.Add(new Sentence("She overcome her fear of dogs by volunteering at a shelter", "她通過在收容所做義工來克服對狗的恐懼"));
                    sentencesList.Add(new Sentence("She never forgot the people who helped launch her acting career", "她永遠不會忘記那些幫助她開展演藝生涯的人"));
                    sentencesList.Add(new Sentence("He will attach the label to your luggage", "他會把標籤繫在你的行李上"));
                    sentencesList.Add(new Sentence("Our hospital was established forty years ago", "我們醫院成立於四十年前"));
                    sentencesList.Add(new Sentence("Many people hailed the director newest movie as her best ever", "許多人都稱讚導演最新電影是有史以來最好的"));
                    sentencesList.Add(new Sentence("The baby has chubby cheeks", "那嬰兒的臉蛋兒圓圓胖胖"));
                    sentencesList.Add(new Sentence("This cheese is a bit smelly", "這種乾酪有點難聞"));
                    sentencesList.Add(new Sentence("An unidentified flying object", "不明飛行物"));
                    sentencesList.Add(new Sentence("He was found guilty", "他被判有罪"));
                    sentencesList.Add(new Sentence("The company went bankrupt", "公司破產了"));
                    sentencesList.Add(new Sentence("These animals have excellent visual ability", "這些動物有很好的視覺能力"));
                    sentencesList.Add(new Sentence("The man had the mental age of a child", "這個男人有一個孩子的心理年齡"));
                    sentencesList.Add(new Sentence("An imaginary friend", "一個想像中的朋友"));
                    sentencesList.Add(new Sentence("Students can apply for individual tuition", "學生可以申請個別授課"));
                    sentencesList.Add(new Sentence("There is vegetarian restaurant over here", "這裡有素食餐廳"));
                    sentencesList.Add(new Sentence("She was grieving for the dead baby", "她為死去的孩子悲傷"));
                    sentencesList.Add(new Sentence("He shouted in anger", "他氣忿地叫喊著"));
                    sentencesList.Add(new Sentence("He was full of sorrow", "他非常傷心"));
                    sentencesList.Add(new Sentence("He was startled when his nephew came in", "當他侄子走進來時，他吃了一驚"));
                    sentencesList.Add(new Sentence("I am happy to accept your invitation", "我很高興接受你的邀請"));
                    sentencesList.Add(new Sentence("The news surprised everyone", "這個消息讓每個人都驚訝"));
                    sentencesList.Add(new Sentence("There is a big tree behind the house", "房子後面有一棵大樹"));
                    sentencesList.Add(new Sentence("They were playing chess under the tree", "他們在樹下下棋"));
                    sentencesList.Add(new Sentence("We live over a small bookstore", "我們住在一家小書店"));
                    sentencesList.Add(new Sentence("Her skirt came below her knees", "她的裙子低於她的膝蓋"));
                    sentencesList.Add(new Sentence("The moon is now above the trees", "月亮現在在樹上"));
                    sentencesList.Add(new Sentence("The bus stops right in front of our house", "巴士站就在我們家門前"));
                    sentencesList.Add(new Sentence("Their house is half way down the hill", "他們的房子座落在半山腰"));
                    sentencesList.Add(new Sentence("A lack of sleep can cause depression", "睡眠不足會導致抑鬱"));
                    sentencesList.Add(new Sentence("The old woman had gained a lot of wisdom during her life", "這位老太太一生都獲得了很多智慧"));
                    sentencesList.Add(new Sentence("The condition is serious it will need surgery", "情況嚴重，需要馬上動手術"));
                    sentencesList.Add(new Sentence("He carried a lot of burden", "他背負了很大的重擔"));
                    sentencesList.Add(new Sentence("I have confidence", "我有信心"));
                    sentencesList.Add(new Sentence("Whales are mammals but fish are not", "鯨魚是哺乳動物, 但魚不是"));
                    sentencesList.Add(new Sentence("He put the leftover in a container", "他把剩下的放在一個容器裡"));
                    sentencesList.Add(new Sentence("I go to grocery shopping every Friday", "我每星期五去雜貨店購物"));
                    sentencesList.Add(new Sentence("A cigarette spark started the fire", "香菸的火星引起這場火災"));
                    sentencesList.Add(new Sentence("Coral is often used for making jewellery", "珊瑚常用來製作首飾"));
                    sentencesList.Add(new Sentence("This small house is my only property", "這所小房子是我的唯一財產"));
                    break;
                    #endregion
            }
        }

        /// <summary>
        /// Speech Dictionary 語音辨識字典
        /// </summary>
        private void Init_Speech_Vocabulary()
        {
            switch (Students.StudentsName)
            {
                #region 第一組完成
                case "黃裕舜":
                    DataSet.Group1_Speech(Speech_Dictionary);
                    break;

                case "劉奕德":
                    DataSet.Group1_Speech(Speech_Dictionary);
                    break;
                #endregion

                #region 第二組完成
                case "朱湘琳":
                    DataSet.Group2_Speech(Speech_Dictionary);
                    break;
                case "黃琬婷":
                    DataSet.Group2_Speech(Speech_Dictionary);
                    break;
                #endregion

                #region 第三組完成
                case "陳品瑄":
                    DataSet.Group3_Speech(Speech_Dictionary);
                    break;
                case "邱筱筑":
                    DataSet.Group3_Speech(Speech_Dictionary);
                    break;
                #endregion

                #region 第四組完成
                case "王連宙":
                    DataSet.Group4_Speech(Speech_Dictionary);
                    break;
                case "段崇文":
                    DataSet.Group4_Speech(Speech_Dictionary);
                    break;

                #endregion

                #region 第五組完成
                case "古雅瑜":
                    DataSet.Group5_Speech(Speech_Dictionary);
                    break;
                case "呂幸樺":
                    DataSet.Group5_Speech(Speech_Dictionary);
                    break;
                #endregion

                #region 第六組完成
                case "王芸酈":
                    DataSet.Group6_Speech(Speech_Dictionary);
                    break;
                case "馮玟琪":
                    DataSet.Group6_Speech(Speech_Dictionary);
                    break;
                #endregion

                #region 第七組完成
                case "陳孝涵":
                    DataSet.Group7_Speech(Speech_Dictionary);
                    break;
                case "曹語庭":
                    DataSet.Group7_Speech(Speech_Dictionary);
                    break;
                #endregion

                #region 第八組完成
                case "莊楷楨":
                    DataSet.Group8_Speech(Speech_Dictionary);
                    break;
                case "高郁涵":
                    DataSet.Group8_Speech(Speech_Dictionary);
                    break;
                #endregion

                #region 第九組完成
                case "任柏翰":
                    DataSet.Group9_Speech(Speech_Dictionary);
                    break;
                case "李宛庭":
                    DataSet.Group9_Speech(Speech_Dictionary);
                    break;
                #endregion

                #region 甲班
                case "何政剛":
                    Speech_Dictionary.Add(new SemanticResultValue("You need to overcome all you afraid when you be a master", "You need to overcome all you afraid when you be a master"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do not launch the rocket or you will be regret", "Do not launch the rocket or you will be regret"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do you know that the area is ban", "Do you know that the area is ban"));
                    Speech_Dictionary.Add(new SemanticResultValue("Doctor say you need to stop scratching your hand", "Doctor say you need to stop scratching your hand"));
                    Speech_Dictionary.Add(new SemanticResultValue("Use your imaginary draw a special kite for yourself", "Use your imaginary draw a special kite for yourself"));
                    Speech_Dictionary.Add(new SemanticResultValue("We need to make a big surprise for your best friend", "We need to make a big surprise for your best friend"));
                    Speech_Dictionary.Add(new SemanticResultValue("You destroy whole city including my house", "You destroy whole city including my house"));
                    Speech_Dictionary.Add(new SemanticResultValue("You need to feel guilty for the thing you have done", "You need to feel guilty for the thing you have done"));
                    Speech_Dictionary.Add(new SemanticResultValue("I can make everything I want float in sky", "I can make everything I want float in sky"));
                    Speech_Dictionary.Add(new SemanticResultValue("I found this place is getting down the hail", "I found this place is getting down the hail"));
                    Speech_Dictionary.Add(new SemanticResultValue("some people do not know that dolphin is mammal", "some people do not know that dolphin is mammal"));
                    Speech_Dictionary.Add(new SemanticResultValue("You need to sprinkle the salt evenly on the steak", "You need to sprinkle the salt evenly on the steak"));
                    Speech_Dictionary.Add(new SemanticResultValue("If you still waste your money on gambling you will be bankrupt", "If you still waste your money on gambling you will be bankrupt"));
                    Speech_Dictionary.Add(new SemanticResultValue("We want to find the gold under the deep ocean", "We want to find the gold under the deep ocean"));
                    Speech_Dictionary.Add(new SemanticResultValue("Food is in front of you why you do not eat", "Food is in front of you why you do not eat"));
                    Speech_Dictionary.Add(new SemanticResultValue("The gold container is very expensive", "The gold container is very expensive"));
                    Speech_Dictionary.Add(new SemanticResultValue("He is very easy to be anger", "He is very easy to be anger"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am not a vegetarian because I like eat meat", "I am not a vegetarian because I like eat meat"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do not help me it will make my burden bigger", "Do not help me it will make my burden bigger"));
                    break;
                case "高宜瑄":
                    Speech_Dictionary.Add(new SemanticResultValue("You need to overcome difficulties not escape from it", "You need to overcome difficulties not escape from it"));
                    Speech_Dictionary.Add(new SemanticResultValue("He has a serious illness", "He has a serious illness"));
                    Speech_Dictionary.Add(new SemanticResultValue("If you do not save money", "If you do not save money"));
                    Speech_Dictionary.Add(new SemanticResultValue("The book you are finding is under the chair near the sofa", "The book you are finding is under the chair near the sofa"));
                    Speech_Dictionary.Add(new SemanticResultValue("The whale is not a mammal", "The whale is not a mammal"));
                    Speech_Dictionary.Add(new SemanticResultValue("We need many grocery to make this dish", "We need many grocery to make this dish"));
                    Speech_Dictionary.Add(new SemanticResultValue("A vegetarian can not eat meat", "A vegetarian can not eat meat"));
                    Speech_Dictionary.Add(new SemanticResultValue("She likes to wander around the garden because of the flower smell good", "She likes to wander around the garden because of the flower smell good"));
                    Speech_Dictionary.Add(new SemanticResultValue("Look at the picture below what is the girl doing", "Look at the picture below what is the girl doing"));
                    Speech_Dictionary.Add(new SemanticResultValue("The container is good to storage many things", "The container is good to storage many things"));

                    //Speech_Dictionary.Add(new SemanticResultValue("The book is under the chair near the sofa", "The book is under the chair near the sofa"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She don't have any confidence", "She don't have any confidence"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Floating on the water needs courage", "Floating on the water needs courage"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Some people like to imaginary things does not exist in the world", "Some people like to imaginary things does not exist in the world"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She is good at comforting people", "She is good at comforting people"));
                    //Speech_Dictionary.Add(new SemanticResultValue("You need to overcome difficulties not escape from it", "You need to overcome difficulties not escape from it"));
                    //Speech_Dictionary.Add(new SemanticResultValue("If you do not save money you will go bankrupt fast", "If you do not save money you will go bankrupt fast"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He has serious illness so he needs a surgery", "He has serious illness so he needs a surgery"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The airplane is flying above the sky", "The airplane is flying above the sky"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The river is in front of my house", "The river is in front of my house"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The mountain is behind the school", "The mountain is behind the school"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The container is good to storage many things", "The container is good to storage many things"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I am happy to see you", "I am happy to see you"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My sister is chubby because she eats too much", "My sister is chubby because she eats too much"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Stealing things is guilty", "Stealing things is guilty"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Clownfish live in corals", "Clownfish live in corals"));
                    //Speech_Dictionary.Add(new SemanticResultValue("They give he a birthday surprise", "They give he a birthday surprise"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Stinky tofu is smelly but yummy", "Stinky tofu is smelly but yummy"));
                    //Speech_Dictionary.Add(new SemanticResultValue("You can merge some vegetables together to make salads", "You can merge some vegetables together to make salads"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Some cold places will have hails when raining", "Some cold places will have hails when raining"));
                    break;
                case "陳虹妤":
                    Speech_Dictionary.Add(new SemanticResultValue("How is your visual test", "How is your visual test"));
                    Speech_Dictionary.Add(new SemanticResultValue("My visual test just zero point eight", "My visual test just zero point eight"));
                    Speech_Dictionary.Add(new SemanticResultValue("Have you ever seen the coral", "Have you ever seen the coral"));
                    Speech_Dictionary.Add(new SemanticResultValue("Yes I just see the coral in this summer vacation", "Yes I just see the coral in this summer vacation"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do you want to go to the grocery with me", "Do you want to go to the grocery with me"));
                    Speech_Dictionary.Add(new SemanticResultValue("Sure I have lone time have not go to the grocery", "Sure I have lone time have not go to the grocery"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do you know the cat is mammal", "Do you know the cat is mammal"));
                    Speech_Dictionary.Add(new SemanticResultValue("Yes I know the cat is mammal", "Yes I know the cat is mammal"));
                    Speech_Dictionary.Add(new SemanticResultValue("What is the liquid in your container", "What is the liquid in your container"));
                    Speech_Dictionary.Add(new SemanticResultValue("It is orange juice", "It is orange juice"));
                    Speech_Dictionary.Add(new SemanticResultValue("Are you a vegeterian", "Are you a vegeterian"));
                    Speech_Dictionary.Add(new SemanticResultValue("Yes I am a vegeterian since I was ten", "Yes I am a vegeterian since I was ten"));
                    Speech_Dictionary.Add(new SemanticResultValue("I relieve my pressure by doing exercise", "I relieve my pressure by doing exercise"));
                    Speech_Dictionary.Add(new SemanticResultValue("I always do exercise to relieve my pressure after school", "I always do exercise to relieve my pressure after school"));
                    Speech_Dictionary.Add(new SemanticResultValue("I always wander around the lake when I have bad mood", "I always wander around the lake when I have bad mood"));
                    Speech_Dictionary.Add(new SemanticResultValue("Wander around the lake is a good idea to relieve your pressure", "Wander around the lake is a good idea to relieve your pressure"));
                    Speech_Dictionary.Add(new SemanticResultValue("Look!The man is floating on the swimming poor", "Look!The man is floating on the swimming poor"));
                    Speech_Dictionary.Add(new SemanticResultValue("He has great skill on float", "He has great skill on float"));
                    Speech_Dictionary.Add(new SemanticResultValue("I think somebody who can establish the company by himself is not easy", "I think somebody who can establish the company by himself is not easy"));
                    Speech_Dictionary.Add(new SemanticResultValue("You're right", "You're right"));

                    //Speech_Dictionary.Add(new SemanticResultValue("Doing exercise maybe can let you relieve the pressure", "Doing exercise maybe can let you relieve the pressure"));
                    //Speech_Dictionary.Add(new SemanticResultValue("An active volcano not only destroy the whole city but also broke many family", "An active volcano not only destroy the whole city but also broke many family"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My father establish the company by himself", "My father establish the company by himself"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I always attach my boyfriend because he is aromatic", "I always attach my boyfriend because he is aromatic"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She spent much time on sit around doing nothing so he was bankrupt", "She spent much time on sit around doing nothing so he was bankrupt"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My aunt is a vegetarian since she was young", "My aunt is a vegetarian since she was young"));
                    //Speech_Dictionary.Add(new SemanticResultValue("His mental is higher than general average", "His mental is higher than general average"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I like to wander around the lake", "I like to wander around the lake"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The man is floating on the swimming pool has nice skill", "The man is floating on the swimming pool has nice skill"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I am happy because my sister come back home", "I am happy because my sister come back home"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The teacher is anger because we broke the rules", "The teacher is anger because we broke the rules"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She was grieving when his best friend hurt", "She was grieving when his best friend hurt"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The old house is in front of the forest", "The old house is in front of the forest"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He can hide under the desk", "He can hide under the desk"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She hugs her best behind the back", "She hugs her best behind the back"));
                    //Speech_Dictionary.Add(new SemanticResultValue("We sang the song and the body  followed up and down", "We sang the song and the body  followed up and down"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I hope I will have more confidence before I get a job", "I hope I will have more confidence before I get a job"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I have never seen the coral because I think it is dangerous", "I have never seen the coral because I think it is dangerous"));
                    //Speech_Dictionary.Add(new SemanticResultValue("There is a grocery opened the day before yesterday", "There is a grocery opened the day before yesterday"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Hopefully I will have property to go around the world", "Hopefully I will have property to go around the world"));
                    break;
                case "黃家琁":
                    Speech_Dictionary.Add(new SemanticResultValue("Why you and your sister looks so sorrow", "Why you and your sister looks so sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because we were banded to go out by our parents", "Because we were banded to go out by our parents"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why you look so happy", "Why you look so happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because my best friend said some interested thing to comfort me", "Because my best friend said some interested thing to comfort me"));
                    Speech_Dictionary.Add(new SemanticResultValue("Where is the grocery", "Where is the grocery"));
                    Speech_Dictionary.Add(new SemanticResultValue("The grocery is in front of the transation", "The grocery is in front of the transation"));
                    Speech_Dictionary.Add(new SemanticResultValue("The chubby boy was crashed by a car", "The chubby boy was crashed by a car"));
                    Speech_Dictionary.Add(new SemanticResultValue("He might go to hospital to have a surgery", "He might go to hospital to have a surgery"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why you look so startled", "Why you look so startled"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because I was bankrupt ten minstutes ago", "Because I was bankrupt ten minstutes ago"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why you looks so confidence", "Why you looks so confidence"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because I finished a imaginary novel", "Because I finished a imaginary novel"));
                    Speech_Dictionary.Add(new SemanticResultValue("The economic burden really make me crazy", "The economic burden really make me crazy"));
                    Speech_Dictionary.Add(new SemanticResultValue("You should go out or wander mountains with friends", "You should go out or wander mountains with friends"));
                    Speech_Dictionary.Add(new SemanticResultValue("The company was established by my father", "The company was established by my father"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why you look so guilty", "Why you look so guilty"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because I destroyed his phone", "Because I destroyed his phone"));
                    Speech_Dictionary.Add(new SemanticResultValue("How to relieve the stersses", "How to relieve the stersses"));
                    Speech_Dictionary.Add(new SemanticResultValue("By wandering mountains", "By wandering mountains"));

                    //Speech_Dictionary.Add(new SemanticResultValue("He spoke a few words of comfort to me", "He spoke a few words of comfort to me"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I went mountain climbing by wandering last weekend", "I went mountain climbing by wandering last weekend"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My sister and I were banned to go out by my parents because we made them angry", "My sister and I were banned to go out by my parents because we made them angry"));
                    //Speech_Dictionary.Add(new SemanticResultValue("These new regulations of class will destroy our advantages", "These new regulations of class will destroy our advantages"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Our manager decided to make the two firms merge this morning", "Our manager decided to make the two firms merge this morning"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The learner of a second language has many obstacles to overcome", "The learner of a second language has many obstacles to overcome"));
                    //Speech_Dictionary.Add(new SemanticResultValue("This charity was established by my grandpa", "This charity was established by my grandpa"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He was chubby and has blonde curls", "He was chubby and has blonde curls"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I felt guilty after breaking my promise", "I felt guilty after breaking my promise"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Because he has addicted to gamble so he goes bankrupt", "Because he has addicted to gamble so he goes bankrupt"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Her problem is mental", "Her problem is mental"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The story is rumored to imaginary", "The story is rumored to imaginary"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My mom is a vegetarian that she never eats any meat", "My mom is a vegetarian that she never eats any meat"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He feels surprising because the beautiful girl likes him", "He feels surprising because the beautiful girl likes him"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I had a fall and his under lip began to swell up", "I had a fall and his under lip began to swell up"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The gorcery is in front of my house", "The gorcery is in front of my house"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The trip to the seashore brought her out of her depression", "The trip to the seashore brought her out of her depression"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The worst barrier to your success is not lack of girlfriend", "The worst barrier to your success is not lack of girlfriend"));
                    //Speech_Dictionary.Add(new SemanticResultValue("His eyes sparked because he saw a beautiful girl", "His eyes sparked because he saw a beautiful girl"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My mother is wearing a coral necklace in front of mirror", "My mother is wearing a coral necklace in front of mirror"));               
                    break;
                case "李姍珊":
                    Speech_Dictionary.Add(new SemanticResultValue("Why you look so sorrow", "Why you look so sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because I have a contest tomorrow and fell grieving now", "Because I have a contest tomorrow and fell grieving now"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do not feel so nervous you need to have confidence", "Do not feel so nervous you need to have confidence"));
                    Speech_Dictionary.Add(new SemanticResultValue("But I afraid that if I loss the contest my mom will be angry", "But I afraid that if I loss the contest my mom will be angry"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do not worry you need to over come the fear", "Do not worry you need to over come the fear"));
                    Speech_Dictionary.Add(new SemanticResultValue("I will try my best to bit the depression", "I will try my best to bit the depression"));
                    Speech_Dictionary.Add(new SemanticResultValue("I feel very happy now", "I feel very happy now"));
                    Speech_Dictionary.Add(new SemanticResultValue("What happened something surprise", "What happened something surprise"));
                    Speech_Dictionary.Add(new SemanticResultValue("Can you imagine that I win the contest", "Can you imagine that I win the contest"));
                    Speech_Dictionary.Add(new SemanticResultValue("Finally you do not burden yourself anymore", "Finally you do not burden yourself anymore"));
                    Speech_Dictionary.Add(new SemanticResultValue("Let me hail for you congratulations", "Let me hail for you congratulations"));

                    //Speech_Dictionary.Add(new SemanticResultValue("I am relieved to hear it", "I am relieved to hear it"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He scratched his arm", "He scratched his arm"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She lives in comfort", "She lives in comfort"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The fish is floating on the river", "The fish is floating on the river"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The cook is sprinkling salt on the steak", "The cook is sprinkling salt on the steak"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Smoking is banned for child", "Smoking is banned for child"));
                    //Speech_Dictionary.Add(new SemanticResultValue("His house was destroyed by fire", "His house was destroyed by fire"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Do not despise everyone even you strong then them", "Do not despise everyone even you strong then them"));
                    //Speech_Dictionary.Add(new SemanticResultValue("His company merged yours", "His company merged yours"));
                    //Speech_Dictionary.Add(new SemanticResultValue("If you want to win the game you need to overcome your weakness", "If you want to win the game you need to overcome your weakness"));
                    //Speech_Dictionary.Add(new SemanticResultValue("A president decided to launch missile", "A president decided to launch missile"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He will attach the sticker on the book", "He will attach the sticker on the book"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Establishing a country is impossible", "Establishing a country is impossible"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Her baby has a chubby face", "Her baby has a chubby face"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My classmate is smelly", "My classmate is smelly"));
                    //Speech_Dictionary.Add(new SemanticResultValue("There is a cat under the table", "There is a cat under the table"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Because he killed someone so he is guilty", "Because he killed someone so he is guilty"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He is a big burden for us", "He is a big burden for us"));
                    break;
                case "林雨儒":
                    Speech_Dictionary.Add(new SemanticResultValue("Why does Tom look so happy", "Why does Tom look so happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because he won the lottery", "Because he won the lottery"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why Amy looks so sad", "Why Amy looks so sad"));
                    Speech_Dictionary.Add(new SemanticResultValue("I do not know. Maybe we can ask and give her some comforts", "I do not know. Maybe we can ask and give her some comforts"));
                    Speech_Dictionary.Add(new SemanticResultValue("I want to swim in this lake", "I want to swim in this lake"));
                    Speech_Dictionary.Add(new SemanticResultValue("But swimming is banned here we can go to the pool", "But swimming is banned here we can go to the pool"));
                    Speech_Dictionary.Add(new SemanticResultValue("We can wander in the park", "We can wander in the park"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why Abby does not eat meat", "Why Abby does not eat meat"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because she is a vegetarian", "Because she is a vegetarian"));
                    Speech_Dictionary.Add(new SemanticResultValue("I feel of sorrow because my dog passed away yesterday", "I feel of sorrow because my dog passed away yesterday"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am sorry to hear that", "I am sorry to hear that"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am sorry mom I failed the test", "I am sorry mom I failed the test"));
                    Speech_Dictionary.Add(new SemanticResultValue("It it ok honey if you study harder you will be a wisdom person a day", "It it ok honey if you study harder you will be a wisdom person a day"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am afraid that I will fail the exam tomorrow", "I am afraid that I will fail the exam tomorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("Have confidence in yourself you are smart", "Have confidence in yourself you are smart"));
                    Speech_Dictionary.Add(new SemanticResultValue("Son the salt in the kitchen is empty", "Son the salt in the kitchen is empty"));
                    Speech_Dictionary.Add(new SemanticResultValue("I will go to the grocery store to buy a new one", "I will go to the grocery store to buy a new one"));
                    Speech_Dictionary.Add(new SemanticResultValue("Where is the puppy", "Where is the puppy"));
                    Speech_Dictionary.Add(new SemanticResultValue("It is hiding under the bed", "It is hiding under the bed"));

                    //Speech_Dictionary.Add(new SemanticResultValue("We have friend to comfort us", "We have friend to comfort us"));
                    //Speech_Dictionary.Add(new SemanticResultValue("A balloon is floating in the sky", "A balloon is floating in the sky"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My little sister wants to wander in the park because she feels bored", "My little sister wants to wander in the park because she feels bored"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Buying cigarettes is banned in Taiwan before you are eighteen", "Buying cigarettes is banned in Taiwan before you are eighteen"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Most difficulties can be overcome", "Most difficulties can be overcome"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The little girl looks cute with chubby cheeks", "The little girl looks cute with chubby cheeks"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The company is bankrupt", "The company is bankrupt"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Praise is a mental encouragement", "Praise is a mental encouragement"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Few of my friends are vegetarian", "Few of my friends are vegetarian"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He feels with anger and tiredness", "He feels with anger and tiredness"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He has had many sorrows so far", "He has had many sorrows so far"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He looks very happy because he won the lottery", "He looks very happy because he won the lottery"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Do not talk about others behind their backs", "Do not talk about others behind their backs"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The information below was compiled by our team", "The information below was compiled by our team"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The teacher stands in front of the students", "The teacher stands in front of the students"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He is not only wisdom but brave", "He is not only wisdom but brave"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The surgery saved many people's lives in the world", "The surgery saved many people's lives in the world"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He is full of confidence in this test", "He is full of confidence in this test"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My mom requires me go to the grocery to buy some eggs", "My mom requires me go to the grocery to buy some eggs"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He lost all his property because of gambling", "He lost all his property because of gambling"));
                    break;
                case "徐芷羚":
                    Speech_Dictionary.Add(new SemanticResultValue("What did she do yesterday", "What did she do yesterday"));
                    Speech_Dictionary.Add(new SemanticResultValue("She went to the grocery store to bought some eggs yesterday", "She went to the grocery store to bought some eggs yesterday"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why is he so happy", "Why is he so happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("He is happy because he got the first place", "He is happy because he got the first place"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why is she chubby", "Why is she chubby"));
                    Speech_Dictionary.Add(new SemanticResultValue("She is chubby because she eats a lot and doesn't like to exercise", "She is chubby because she eats a lot and doesn't like to exercise"));
                    Speech_Dictionary.Add(new SemanticResultValue("What is he doing", "What is he doing"));
                    Speech_Dictionary.Add(new SemanticResultValue("He is talking to his maginary friend", "He is talking to his maginary friend"));
                    Speech_Dictionary.Add(new SemanticResultValue("What make he feel so sorrow", "What make he feel so sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("His cat death make him feel sorrow", "His cat death make him feel sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("How do those corals look", "How do those corals look"));
                    Speech_Dictionary.Add(new SemanticResultValue("Those corals look so beautiful", "Those corals look so beautiful"));
                    Speech_Dictionary.Add(new SemanticResultValue("What surprise him a lot", "What surprise him a lot"));
                    Speech_Dictionary.Add(new SemanticResultValue("That he won the lottery surprise him a lot", "That he won the lottery surprise him a lot"));
                    Speech_Dictionary.Add(new SemanticResultValue("Who is that girl behind you", "Who is that girl behind you"));
                    Speech_Dictionary.Add(new SemanticResultValue("The girl behind me is Kate", "The girl behind me is Kate"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why is his hands so smelly", "Why is his hands so smelly"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because he just cut the fish", "Because he just cut the fish"));
                    Speech_Dictionary.Add(new SemanticResultValue("What does he likes to do", "What does he likes to do"));
                    Speech_Dictionary.Add(new SemanticResultValue("He likes to take pictures of the fish under the sea", "He likes to take pictures of the fish under the sea"));


                    //Speech_Dictionary.Add(new SemanticResultValue("She went to the grocery store to bought some eggs yesterday", "She went to the grocery store to bought some eggs yesterday"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He is happy because he got the first place", "He is happy because he got the first place"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Bats are mammals", "Bats are mammals"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He was banned to talk in the class", "He was banned to talk in the class"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She is chubby because she eats a lot", "She is chubby because she eats a lot"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He have an imaginary friend", "He have an imaginary friend"));
                    //Speech_Dictionary.Add(new SemanticResultValue("His cat death make him feel sorrow", "His cat death make him feel sorrow"));
                    //Speech_Dictionary.Add(new SemanticResultValue("His hands are smelly after cutting the fish", "His hands are smelly after cutting the fish"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She comfort her sister by giving her a hug", "She comfort her sister by giving her a hug"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The sparks of fireworks are beautiful", "The sparks of fireworks are beautiful"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My grandfather sprinkle some water on the flower", "My grandfather sprinkle some water on the flower"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She does not know how to float on the water", "She does not know how to float on the water"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He overcomes the fear of diving", "He overcomes the fear of diving"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Those colorful corals are beautiful", "Those colorful corals are beautiful"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He likes to take pictures of the fish under the sea", "He likes to take pictures of the fish under the sea"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The doctor is performing a surgery for him", "The doctor is performing a surgery for him"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He launch a rocket", "He launch a rocket"));
                    //Speech_Dictionary.Add(new SemanticResultValue("There are many clouds over the sky", "There are many clouds over the sky"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He won the lottery surprise him a lot", "He won the lottery surprise him a lot"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The girl behind him is tall", "The girl behind him is tall"));
                    break;
                case "張哲維":
                    Speech_Dictionary.Add(new SemanticResultValue("The little boy looks chubby", "The little boy looks chubby"));
                    Speech_Dictionary.Add(new SemanticResultValue("This room is very smelly", "This room is very smelly"));
                    Speech_Dictionary.Add(new SemanticResultValue("There is a unidentified chopper flying around", "There is a unidentified chopper flying around"));
                    Speech_Dictionary.Add(new SemanticResultValue("The man is guilty", "The man is guilty"));
                    Speech_Dictionary.Add(new SemanticResultValue("This restaurant is bankrupt", "This restaurant is bankrupt"));
                    Speech_Dictionary.Add(new SemanticResultValue("He has mental problem", "He has mental problem"));
                    Speech_Dictionary.Add(new SemanticResultValue("My mom walked in room and full with anger", "My mom walked in room and full with anger"));
                    Speech_Dictionary.Add(new SemanticResultValue("He always looks happy", "He always looks happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("We're preparing a surprise party", "We're preparing a surprise party"));
                    Speech_Dictionary.Add(new SemanticResultValue("I found a book under my desk", "I found a book under my desk"));
                    Speech_Dictionary.Add(new SemanticResultValue("Sometimes you just need to get over it", "Sometimes you just need to get over it"));
                    Speech_Dictionary.Add(new SemanticResultValue("My car is destroyed in the crash", "My car is destroyed in the crash"));
                    Speech_Dictionary.Add(new SemanticResultValue("He is a vegetarian", "He is a vegetarian"));
                    Speech_Dictionary.Add(new SemanticResultValue("This house is my only property", "This house is my only property"));
                    Speech_Dictionary.Add(new SemanticResultValue("I need to buy some grocery", "I need to buy some grocery"));
                    Speech_Dictionary.Add(new SemanticResultValue("The little boy feel comfort when his mother pacify him", "The little boy feel comfort when his mother pacify him"));
                    Speech_Dictionary.Add(new SemanticResultValue("A lot of things are banned in China", "A lot of things are banned in China"));
                    Speech_Dictionary.Add(new SemanticResultValue("He likes to wandering around", "He likes to wandering around"));
                    Speech_Dictionary.Add(new SemanticResultValue("They are launching the rocket", "They are launching the rocket"));
                    break;
                case "許雯琳":
                    Speech_Dictionary.Add(new SemanticResultValue("You should overcome your fear", "You should overcome your fear"));
                    Speech_Dictionary.Add(new SemanticResultValue("I try my best", "I try my best"));
                    Speech_Dictionary.Add(new SemanticResultValue("Where is the park", "Where is the park"));
                    Speech_Dictionary.Add(new SemanticResultValue("It is behind of the grocery", "It is behind of the grocery"));
                    Speech_Dictionary.Add(new SemanticResultValue("How is the food smell", "How is the food smell"));
                    Speech_Dictionary.Add(new SemanticResultValue("It is smell smelly", "It is smell smelly"));
                    Speech_Dictionary.Add(new SemanticResultValue("How are you feeling now", "How are you feeling now"));
                    Speech_Dictionary.Add(new SemanticResultValue("I feeling happy", "I feeling happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Where is my bag", "Where is my bag"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why do you look so sorrow", "Why do you look so sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because my pet is missing", "Because my pet is missing"));
                    Speech_Dictionary.Add(new SemanticResultValue("How about this surgery", "How about this surgery"));
                    Speech_Dictionary.Add(new SemanticResultValue("Very success", "Very success"));
                    Speech_Dictionary.Add(new SemanticResultValue("Who destory the monument", "Who destory the monument"));
                    Speech_Dictionary.Add(new SemanticResultValue("It must be the malicious people", "It must be the malicious people"));
                    Speech_Dictionary.Add(new SemanticResultValue("What kind of mammal is yellow and have black tattoo", "What kind of mammal is yellow and have black tattoo"));
                    Speech_Dictionary.Add(new SemanticResultValue("It is tiger", "It is tiger"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am very afraid of speaking on stage", "I am very afraid of speaking on stage"));
                    Speech_Dictionary.Add(new SemanticResultValue("You have to be more confident", "You have to be more confident"));

                    //Speech_Dictionary.Add(new SemanticResultValue("The chubby baby is lovely", "The chubby baby is lovely"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The garbage is smelly", "The garbage is smelly"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He was found guilty of robbery", "He was found guilty of robbery"));
                    //Speech_Dictionary.Add(new SemanticResultValue("They wander in the park", "They wander in the park"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She finally overcome her fear", "She finally overcome her fear"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The boat float down the river", "The boat float down the river"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She feels sorrow about his death", "She feels sorrow about his death"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He is confident that he can win the race", "He is confident that he can win the race"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Grocery is seldom see in the city", "Grocery is seldom see in the city"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Tiger is a kind of mammal", "Tiger is a kind of mammal"));
                    //Speech_Dictionary.Add(new SemanticResultValue("There is a convenience store behind the park", "There is a convenience store behind the park"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Corals  have been polluted", "Corals  have been polluted"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She is a vegetarian", "She is a vegetarian"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He despises people joking about him", "He despises people joking about him"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The billionaire went bankrupt", "The billionaire went bankrupt"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Children are sweet burden to their parents", "Children are sweet burden to their parents"));
                    //Speech_Dictionary.Add(new SemanticResultValue("This surgery was a great success", "This surgery was a great success"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He launched this charity event", "He launched this charity event"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The monument was destroyed by the malicious person", "The monument was destroyed by the malicious person"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He has more wisdom than ordinary people", "He has more wisdom than ordinary people"));
                    break;
                case "黃皓":
                    Speech_Dictionary.Add(new SemanticResultValue("I am relieved to hear it", "I am relieved to hear it"));
                    Speech_Dictionary.Add(new SemanticResultValue("He scratched his arm", "He scratched his arm"));
                    Speech_Dictionary.Add(new SemanticResultValue("She lives in comfort", "She lives in comfort"));
                    Speech_Dictionary.Add(new SemanticResultValue("The fish is floating on the river", "The fish is floating on the river"));
                    Speech_Dictionary.Add(new SemanticResultValue("The cook is sprinkling salt on the steak", "The cook is sprinkling salt on the steak"));
                    Speech_Dictionary.Add(new SemanticResultValue("Smoking is banned for child", "Smoking is banned for child"));
                    Speech_Dictionary.Add(new SemanticResultValue("His house was destroyed by fire", "His house was destroyed by fire"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do not despise everyone even you strong then them", "Do not despise everyone even you strong then them"));
                    Speech_Dictionary.Add(new SemanticResultValue("His company merged yours", "His company merged yours"));
                    Speech_Dictionary.Add(new SemanticResultValue("If you want to win the game you need to overcome your weakness", "If you want to win the game you need to overcome your weakness"));
                    Speech_Dictionary.Add(new SemanticResultValue("A president decided to launch missile", "A president decided to launch missile"));
                    Speech_Dictionary.Add(new SemanticResultValue("He will attach the sticker on the book", "He will attach the sticker on the book"));
                    Speech_Dictionary.Add(new SemanticResultValue("Establishing a country is impossible", "Establishing a country is impossible"));
                    Speech_Dictionary.Add(new SemanticResultValue("Her baby has a chubby face", "Her baby has a chubby face"));
                    Speech_Dictionary.Add(new SemanticResultValue("My classmate is smelly", "My classmate is smelly"));
                    Speech_Dictionary.Add(new SemanticResultValue("There is a cat under the table", "There is a cat under the table"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because he killed someone so he is guilty", "Because he killed someone so he is guilty"));
                    Speech_Dictionary.Add(new SemanticResultValue("He is a big burden for us", "He is a big burden for us"));
                    break;
                case "楊耀輝":
                    Speech_Dictionary.Add(new SemanticResultValue("Could you helped me overcome this difficulty", "Could you helped me overcome this difficulty"));
                    Speech_Dictionary.Add(new SemanticResultValue("Of course so do not be sorrow anymore", "Of course so do not be sorrow anymore"));
                    Speech_Dictionary.Add(new SemanticResultValue("Did you see that he was really anger with that girl", "Did you see that he was really anger with that girl"));
                    Speech_Dictionary.Add(new SemanticResultValue("I know why it is because the girl stole his money", "I know why it is because the girl stole his money"));
                    Speech_Dictionary.Add(new SemanticResultValue("I think she has really got a lot of startled", "I think she has really got a lot of startled"));
                    Speech_Dictionary.Add(new SemanticResultValue("I think so too", "I think so too"));
                    Speech_Dictionary.Add(new SemanticResultValue("Have you seem the container on my desk it is gone", "Have you seem the container on my desk it is gone"));
                    Speech_Dictionary.Add(new SemanticResultValue("It is under my table", "It is under my table"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do you want to wander around that hail", "Do you want to wander around that hail"));
                    Speech_Dictionary.Add(new SemanticResultValue("I have some pressure need to relieve take a walk may be a good idea", "I have some pressure need to relieve take a walk may be a good idea"));
                    Speech_Dictionary.Add(new SemanticResultValue("That building was established by me", "That building was established by me"));
                    Speech_Dictionary.Add(new SemanticResultValue("I feel very surprised", "I feel very surprised"));
                    Speech_Dictionary.Add(new SemanticResultValue("Look at that mamal it", "Look at that mamal it"));
                    Speech_Dictionary.Add(new SemanticResultValue("I think so too and it is very chubby", "I think so too and it is very chubby"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do you want to see the fireworks launch", "Do you want to see the fireworks launch"));
                    Speech_Dictionary.Add(new SemanticResultValue("Sure I like the spark of fireworks", "Sure I like the spark of fireworks"));
                    Speech_Dictionary.Add(new SemanticResultValue("I dreamed last night that I was floating in the universe", "I dreamed last night that I was floating in the universe"));
                    Speech_Dictionary.Add(new SemanticResultValue("You are very full of imaginary", "You are very full of imaginary"));
                    Speech_Dictionary.Add(new SemanticResultValue("Let we bet", "Let we bet"));
                    Speech_Dictionary.Add(new SemanticResultValue("No this will make me feel very guilty", "No this will make me feel very guilty"));

                    //Speech_Dictionary.Add(new SemanticResultValue("I feel very sorrow because my friend is angry with me", "I feel very sorrow because my friend is angry with me"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My friend helped me overcome this difficulty", "My friend helped me overcome this difficulty"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My brother eats too much fast food so he becomes very chubby", "My brother eats too much fast food so he becomes very chubby"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The book you are looking for is under his desk", "The book you are looking for is under his desk"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The girl in front of you looks uncomfortable", "The girl in front of you looks uncomfortable"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The boy is very happy because his parents buying toys for him", "The boy is very happy because his parents buying toys for him"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Life jackets can help us float on the water", "Life jackets can help us float on the water"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Coral bleaching caused by global warming", "Coral bleaching caused by global warming"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I scratched my head because I feel itchy", "I scratched my head because I feel itchy"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I feel very guilty because I stole his money", "I feel very guilty because I stole his money"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The man went bankrupt because he was addicted to gambling", "The man went bankrupt because he was addicted to gambling"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The cake I made was destroyed by my dog", "The cake I made was destroyed by my dog"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The car is my property", "The car is my property"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My aunt is a vegetarian", "My aunt is a vegetarian"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Fireworks will produce beautiful sparks", "Fireworks will produce beautiful sparks"));
                    //Speech_Dictionary.Add(new SemanticResultValue("This responsibility is too great it is a big burden for me", "This responsibility is too great it is a big burden for me"));
                    //Speech_Dictionary.Add(new SemanticResultValue("When I am sad my sister will always come to comfort me", "When I am sad my sister will always come to comfort me"));
                    //Speech_Dictionary.Add(new SemanticResultValue("This surgery must be performed by have highly skilled doctor", "This surgery must be performed by have highly skilled doctor"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I need a bigger container to hold the water", "I need a bigger container to hold the water"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My parents are very angry that I have not got good grades", "My parents are very angry that I have not got good grades"));
                    break;
                case "聶維君":
                    Speech_Dictionary.Add(new SemanticResultValue("Are you happy", "Are you happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Yes I am happy", "Yes I am happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Did you see the novel that I put under the pencil case", "Did you see the novel that I put under the pencil case"));
                    Speech_Dictionary.Add(new SemanticResultValue("No I did not", "No I did not"));
                    Speech_Dictionary.Add(new SemanticResultValue("Who is the woman stand in front of us", "Who is the woman stand in front of us"));
                    Speech_Dictionary.Add(new SemanticResultValue("She is my piano teacher", "She is my piano teacher"));
                    Speech_Dictionary.Add(new SemanticResultValue("What is the color of coral", "What is the color of coral"));
                    Speech_Dictionary.Add(new SemanticResultValue("It is white and it also have another colors", "It is white and it also have another colors"));
                    Speech_Dictionary.Add(new SemanticResultValue("Are you a vegetarian", "Are you a vegetarian"));
                    Speech_Dictionary.Add(new SemanticResultValue("No I usually eat meat", "No I usually eat meat"));
                    Speech_Dictionary.Add(new SemanticResultValue("What did you do yesterday", "What did you do yesterday"));
                    Speech_Dictionary.Add(new SemanticResultValue("I wandered at the park with my parents", "I wandered at the park with my parents"));
                    Speech_Dictionary.Add(new SemanticResultValue("What do you do if you feel sorrow", "What do you do if you feel sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("I usually listen music", "I usually listen music"));
                    Speech_Dictionary.Add(new SemanticResultValue("When was this foundation established", "When was this foundation established"));
                    Speech_Dictionary.Add(new SemanticResultValue("It was established from ten years ago", "It was established from ten years ago"));
                    Speech_Dictionary.Add(new SemanticResultValue("What is his job", "What is his job"));
                    Speech_Dictionary.Add(new SemanticResultValue("He was a professor of surgery in the state university", "He was a professor of surgery in the state university"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do you think we can overcome the difficulties", "Do you think we can overcome the difficulties"));
                    Speech_Dictionary.Add(new SemanticResultValue("Yes we can overcome any difficulty however great it is", "Yes we can overcome any difficulty however great it is"));

                    //Speech_Dictionary.Add(new SemanticResultValue("The woman standing in front of me is my piano teacher", "The woman standing in front of me is my piano teacher"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I saw a boy was played in the park over the river", "I saw a boy was played in the park over the river"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My classmate is a vegetarian so he never eat meat", "My classmate is a vegetarian so he never eat meat"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I put the paper under the novel", "I put the paper under the novel"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She felt sorrow because she did not get the high score in the test", "She felt sorrow because she did not get the high score in the test"));
                    //Speech_Dictionary.Add(new SemanticResultValue("There are many mammals in the zoo", "There are many mammals in the zoo"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I wandered at the park with my parents last weekend", "I wandered at the park with my parents last weekend"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My brother was chubby when he was a child", "My brother was chubby when he was a child"));
                    //Speech_Dictionary.Add(new SemanticResultValue("We can put lots of things in the container", "We can put lots of things in the container"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The reason that why her visual getting worse is because she always using cellphone", "The reason that why her visual getting worse is because she always using cellphone"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My dad was angered after he knew I failed the test", "My dad was angered after he knew I failed the test"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The house was destroyed by the typhoon", "The house was destroyed by the typhoon"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He established a foundation", "He established a foundation"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She overcomes fear of heights", "She overcomes fear of heights"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Many foreigners felt that our stinky tofu is smelly", "Many foreigners felt that our stinky tofu is smelly"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The company was bankrupt because it does not have funds", "The company was bankrupt because it does not have funds"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I feel very happy because I will go out with my family tomorrow", "I feel very happy because I will go out with my family tomorrow"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The little girl was very surprised after she saw her dad bought her an ice cream", "The little girl was very surprised after she saw her dad bought her an ice cream"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I have a plan for overcoming our difficulties", "I have a plan for overcoming our difficulties"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She has great confidence in her success", "She has great confidence in her success"));
                    break;
                case "徐紫瑜":
                    Speech_Dictionary.Add(new SemanticResultValue("Do you see that the boy who having guilty on his face", "Do you see that the boy who having guilty on his face"));
                    Speech_Dictionary.Add(new SemanticResultValue("Is that chubby boy in front of us", "Is that chubby boy in front of us"));
                    Speech_Dictionary.Add(new SemanticResultValue("To support us dad bears a great burden to earn money", "To support us dad bears a great burden to earn money"));
                    Speech_Dictionary.Add(new SemanticResultValue("I think he should relieve himself for a while", "I think he should relieve himself for a while"));
                    Speech_Dictionary.Add(new SemanticResultValue("The building had been established for ten years but where is it now", "The building had been established for ten years but where is it now"));
                    Speech_Dictionary.Add(new SemanticResultValue("It was removed and became a banned area", "It was removed and became a banned area"));
                    Speech_Dictionary.Add(new SemanticResultValue("She tried many times for the chance of promotion so I have confidence in her", "She tried many times for the chance of promotion so I have confidence in her"));
                    Speech_Dictionary.Add(new SemanticResultValue("No wonder she looks so happy", "No wonder she looks so happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why dis he go bankrupt", "Why dis he go bankrupt"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because he injected all property to gamble", "Because he injected all property to gamble"));

                    //Speech_Dictionary.Add(new SemanticResultValue("He wants to relieve himself", "He wants to relieve himself"));
                    //Speech_Dictionary.Add(new SemanticResultValue("That banned area is my alma mater", "That banned area is my alma mater"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The enormous typhoon destroyed through Taiwan causing a big disaster", "The enormous typhoon destroyed through Taiwan causing a big disaster"));
                    //Speech_Dictionary.Add(new SemanticResultValue("We will encounter hardship in life but we still have to overcome", "We will encounter hardship in life but we still have to overcome"));
                    //Speech_Dictionary.Add(new SemanticResultValue("There was a building which established in ten years ago", "There was a building which established in ten years ago"));
                    //Speech_Dictionary.Add(new SemanticResultValue("When he do the wrong thing he always having guilty on his face", "When he do the wrong thing he always having guilty on his face"));
                    //Speech_Dictionary.Add(new SemanticResultValue("After a serious accident he has visual problem that bring him a lot of depressions", "After a serious accident he has visual problem that bring him a lot of depressions"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He has mental problem", "He has mental problem"));
                    //Speech_Dictionary.Add(new SemanticResultValue("People do not want to reveal about individual to stranger for their first meet", "People do not want to reveal about individual to stranger for their first meet"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The guy stand behind the woman seems to be stealing her bag", "The guy stand behind the woman seems to be stealing her bag"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The boy wants to pick the ball up which below the car", "The boy wants to pick the ball up which below the car"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My father bears a great burden to earn money", "My father bears a great burden to earn money"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Her tried many times for the chance of promotion so I have confidence in her", "Her tried many times for the chance of promotion so I have confidence in her"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The firework spark in the sky of the night", "The firework spark in the sky of the night"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He want to take the property at all means which makes him heartbroken", "He want to take the property at all means which makes him heartbroken"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The owner of grocery is my grandma", "The owner of grocery is my grandma"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He injected too much money to gamble which caused him go bankrupt", "He injected too much money to gamble which caused him go bankrupt"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Knowing my relative who died in the accident makes me grieving all the month", "Knowing my relative who died in the accident makes me grieving all the month"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The police decided to launch an investigation to find the murderer", "The police decided to launch an investigation to find the murderer"));
                    //Speech_Dictionary.Add(new SemanticResultValue("All the roles in the story are imaginary but kids think that  they are real", "All the roles in the story are imaginary but kids think that  they are real"));
                    break;
                case "游淨詒":
                    Speech_Dictionary.Add(new SemanticResultValue("Why he looks so sad", "Why he looks so sad"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because he felt sorrow at the death of her wife", "Because he felt sorrow at the death of her wife"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why she looks so happy", "Why she looks so happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because she was very happy when he heard the result", "Because she was very happy when he heard the result"));
                    Speech_Dictionary.Add(new SemanticResultValue("The little pony looks so exhausted", "The little pony looks so exhausted"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because during the trip the little pony struggled under its burden", "Because during the trip the little pony struggled under its burden"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why you do not believe me", "Why you do not believe me"));
                    Speech_Dictionary.Add(new SemanticResultValue("What you call a fact is just his imaginary", "What you call a fact is just his imaginary"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why he broke up with her", "Why he broke up with her"));
                    Speech_Dictionary.Add(new SemanticResultValue("Because he said that she has a serious mental disorder", "Because he said that she has a serious mental disorder"));

                    //Speech_Dictionary.Add(new SemanticResultValue("I sprinkled some pepper on the top of dish", "I sprinkled some pepper on the top of dish"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He felt sorrow at the death of her wife", "He felt sorrow at the death of her wife"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He was very happy when he heard the result", "He was very happy when he heard the result"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She was surprised at such a develope", "She was surprised at such a develope"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The little pony struggled under its burden", "The little pony struggled under its burden"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The old man spent whole day wandering around the beach", "The old man spent whole day wandering around the beach"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Eventually he managed to overcome his nervous on stage", "Eventually he managed to overcome his nervous on stage"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The ship came down in the ocean", "The ship came down in the ocean"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She usually wears skirts below her knees", "She usually wears skirts below her knees"));
                    //Speech_Dictionary.Add(new SemanticResultValue("What you call a fact is just his imaginary", "What you call a fact is just his imaginary"));
                    //Speech_Dictionary.Add(new SemanticResultValue("These family recently become vegetarians", "These family recently become vegetarians"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Her family has a history of mental disorder", "Her family has a history of mental disorder"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The teacher attached a photo on his book", "The teacher attached a photo on his book"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Gas station should be ban on smoking", "Gas station should be ban on smoking"));
                    //Speech_Dictionary.Add(new SemanticResultValue("In the end of test the students let the balloons float into the sky", "In the end of test the students let the balloons float into the sky"));
                    //Speech_Dictionary.Add(new SemanticResultValue("This area has a lot of coral because here are not polluted", "This area has a lot of coral because here are not polluted"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The old man does not have any property to live", "The old man does not have any property to live"));
                    //Speech_Dictionary.Add(new SemanticResultValue("There are many wisdom students join this game", "There are many wisdom students join this game"));
                    //Speech_Dictionary.Add(new SemanticResultValue("This chubby boy very like eats fried foods", "This chubby boy very like eats fried foods"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I think we need to hail a taxi", "I think we need to hail a taxi"));
                    break;
                case "劉禹萱":
                    Speech_Dictionary.Add(new SemanticResultValue("She is sits in front of me because her grades is better than me", "She is sits in front of me because her grades is better than me"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am happy that you can open your mind", "I am happy that you can open your mind"));
                    Speech_Dictionary.Add(new SemanticResultValue("Daddy is angry with me went home too late", "Daddy is angry with me went home too late"));
                    Speech_Dictionary.Add(new SemanticResultValue("There is a grocery in the corner near my home", "There is a grocery in the corner near my home"));
                    Speech_Dictionary.Add(new SemanticResultValue("I wander why she is filled with sorrow on her face", "I wander why she is filled with sorrow on her face"));
                    Speech_Dictionary.Add(new SemanticResultValue("My pencil dropped on the floor under my desk", "My pencil dropped on the floor under my desk"));
                    Speech_Dictionary.Add(new SemanticResultValue("We surprised that she is a vegetarian", "We surprised that she is a vegetarian"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am feel guilty when I told a lie", "I am feel guilty when I told a lie"));
                    Speech_Dictionary.Add(new SemanticResultValue("Every child is parent burden and responsibility", "Every child is parent burden and responsibility"));
                    Speech_Dictionary.Add(new SemanticResultValue("I have confidence in this competition", "I have confidence in this competition"));

                    //Speech_Dictionary.Add(new SemanticResultValue("Fireworks are spark as brightly as star in the sky", "Fireworks are spark as brightly as star in the sky"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He has a lot of imaginary friends", "He has a lot of imaginary friends"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Our live have many difficulties that we need to overcome", "Our live have many difficulties that we need to overcome"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He is so surprise about our birthday gift for him", "He is so surprise about our birthday gift for him"));
                    //Speech_Dictionary.Add(new SemanticResultValue("I have individual presentation to report on next Thursday", "I have individual presentation to report on next Thursday"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Teachers usually be angry because our behaviors", "Teachers usually be angry because our behaviors"));
                    //Speech_Dictionary.Add(new SemanticResultValue("People should have confidence", "People should have confidence"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She seat is in front of me", "She seat is in front of me"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Our school has a lot of vegetarian", "Our school has a lot of vegetarian"));
                    //Speech_Dictionary.Add(new SemanticResultValue("He is filled with sorrow on her face", "He is filled with sorrow on her face"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My mom always comfort me when I was sad", "My mom always comfort me when I was sad"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Children be happy with Christmas gifts", "Children be happy with Christmas gifts"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Most young people have a heavy burden when they first start working", "Most young people have a heavy burden when they first start working"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Children be happy with Christmas gifts", "Children be happy with Christmas gifts"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Most young people have a heavy burden when they first start working", "Most young people have a heavy burden when they first start working"));
                    break;
                case "馬偉欣":
                    Speech_Dictionary.Add(new SemanticResultValue("How is your feeling while floating on the water", "How is your feeling while floating on the water"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am happy when floating on the water", "I am happy when floating on the water"));
                    Speech_Dictionary.Add(new SemanticResultValue("What do you relieve pressure when you feel down?", "What do you relieve pressure when you feel down?"));
                    Speech_Dictionary.Add(new SemanticResultValue("Eating", "Eating"));
                    Speech_Dictionary.Add(new SemanticResultValue("No wonder you are so chubby", "No wonder you are so chubby"));
                    Speech_Dictionary.Add(new SemanticResultValue("Are there any corals under the ocean", "Are there any corals under the ocean"));
                    Speech_Dictionary.Add(new SemanticResultValue("Definitely there are also mammals under the ocean", "Definitely there are also mammals under the ocean"));
                    Speech_Dictionary.Add(new SemanticResultValue("My friends banned me from gambling or I would be bankrupt", "My friends banned me from gambling or I would be bankrupt"));
                    Speech_Dictionary.Add(new SemanticResultValue("I agree with your friends", "I agree with your friends"));
                    Speech_Dictionary.Add(new SemanticResultValue("Take a walk in front of our house", "Take a walk in front of our house"));
                    Speech_Dictionary.Add(new SemanticResultValue("Great wandering near the park is another choice", "Great wandering near the park is another choice"));
                    Speech_Dictionary.Add(new SemanticResultValue("Establishing a company by ourselves is difficult", "Establishing a company by ourselves is difficult"));
                    Speech_Dictionary.Add(new SemanticResultValue("You are right", "You are right"));
                    Speech_Dictionary.Add(new SemanticResultValue("What is wrong with her She looks like to be full of sorrow", "What is wrong with her She looks like to be full of sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("Maybe we need to give her some comfort", "Maybe we need to give her some comfort"));
                    Speech_Dictionary.Add(new SemanticResultValue("Are you grieving", "Are you grieving"));
                    Speech_Dictionary.Add(new SemanticResultValue("Yes I am grieving", "Yes I am grieving"));
                    Speech_Dictionary.Add(new SemanticResultValue("Do you know where the vegetarian restaurant is", "Do you know where the vegetarian restaurant is"));
                    Speech_Dictionary.Add(new SemanticResultValue("Yes it is behind the convenient store", "Yes it is behind the convenient store"));
                    Speech_Dictionary.Add(new SemanticResultValue("How does your cat when it is scratched", "How does your cat when it is scratched"));
                    Speech_Dictionary.Add(new SemanticResultValue("It often feels happy and comfortable except when stroking its legs", "It often feels happy and comfortable except when stroking its legs"));
                    Speech_Dictionary.Add(new SemanticResultValue("If I touch its legs", "If I touch its legs"));

                    //Speech_Dictionary.Add(new SemanticResultValue("A building was established ten years ago", "A building was established ten years ago"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She often relieves pressure by listening to music", "She often relieves pressure by listening to music"));
                    //Speech_Dictionary.Add(new SemanticResultValue("His habit of gambling made him bankrupt", "His habit of gambling made him bankrupt"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The little girl loves eating sweets so that she is chubby", "The little girl loves eating sweets so that she is chubby"));
                    //Speech_Dictionary.Add(new SemanticResultValue("You are vegetarian", "You are vegetarian"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The old parents always wander in the park near their house", "The old parents always wander in the park near their house"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The unicorn is an imaginary being that looks like a horse", "The unicorn is an imaginary being that looks like a horse"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Mom told me that I need to comfort my sisters", "Mom told me that I need to comfort my sisters"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Scientists say that there must be many living beings under the water", "Scientists say that there must be many living beings under the water"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Many people are overwhelming from depression these years", "Many people are overwhelming from depression these years"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Cats are mammals but snakes are not", "Cats are mammals but snakes are not"));
                    //Speech_Dictionary.Add(new SemanticResultValue("She was banned from the athletics for five years", "She was banned from the athletics for five years"));
                    //Speech_Dictionary.Add(new SemanticResultValue("His courage gave her more confidence", "His courage gave her more confidence"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Lots of people make surprise on birthday party", "Lots of people make surprise on birthday party"));
                    //Speech_Dictionary.Add(new SemanticResultValue("Monsters usually destroy buildings and houses", "Monsters usually destroy buildings and houses"));
                    //Speech_Dictionary.Add(new SemanticResultValue("It was said that the two businesses would be merged", "It was said that the two businesses would be merged"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The note is attached to this document which is sent to boss", "The note is attached to this document which is sent to boss"));
                    //Speech_Dictionary.Add(new SemanticResultValue("The old man left his property to his son這位老人把他的財產留給了他的兒子", "The old man left his property to his son這位老人把他的財產留給了他的兒子"));
                    //Speech_Dictionary.Add(new SemanticResultValue("There is a garden in front of our house", "There is a garden in front of our house"));
                    //Speech_Dictionary.Add(new SemanticResultValue("My brother was full of sorrow", "My brother was full of sorrow"));

                    break;

                #endregion

                #region 預設(註解掉)          
                default:
                    //動詞
                    Speech_Dictionary.Add(new SemanticResultValue("Relieve", "Relieve"));
                    Speech_Dictionary.Add(new SemanticResultValue("Scratch", "Scratch"));
                    Speech_Dictionary.Add(new SemanticResultValue("Comfort", "Comfort"));
                    Speech_Dictionary.Add(new SemanticResultValue("Float", "Float"));
                    Speech_Dictionary.Add(new SemanticResultValue("Sprinkle", "Sprinkle"));
                    Speech_Dictionary.Add(new SemanticResultValue("Wander", "Wander"));
                    Speech_Dictionary.Add(new SemanticResultValue("Ban", "Ban"));
                    Speech_Dictionary.Add(new SemanticResultValue("Destroy", "Destroy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Despise", "Despise"));
                    Speech_Dictionary.Add(new SemanticResultValue("Merge", "Merge"));
                    Speech_Dictionary.Add(new SemanticResultValue("Overcome", "Overcome"));
                    Speech_Dictionary.Add(new SemanticResultValue("Launch", "Launch"));
                    Speech_Dictionary.Add(new SemanticResultValue("Attach", "Attach"));
                    Speech_Dictionary.Add(new SemanticResultValue("Establish", "Establish"));
                    Speech_Dictionary.Add(new SemanticResultValue("Hail", "Hail"));

                    //形容詞
                    Speech_Dictionary.Add(new SemanticResultValue("Visual", "Visual"));
                    Speech_Dictionary.Add(new SemanticResultValue("Bankrupt", "Bankrupt"));
                    Speech_Dictionary.Add(new SemanticResultValue("Mental", "Mental"));
                    Speech_Dictionary.Add(new SemanticResultValue("Imaginary", "Imaginary"));
                    Speech_Dictionary.Add(new SemanticResultValue("Chubby", "Chubby"));
                    Speech_Dictionary.Add(new SemanticResultValue("Smelly", "Smelly"));
                    Speech_Dictionary.Add(new SemanticResultValue("Individual", "Individual"));
                    Speech_Dictionary.Add(new SemanticResultValue("Unidentified", "Unidentified"));

                    //情感詞
                    Speech_Dictionary.Add(new SemanticResultValue("Sorrow", "sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("Anger", "anger"));
                    Speech_Dictionary.Add(new SemanticResultValue("Grieving", "grieving"));
                    Speech_Dictionary.Add(new SemanticResultValue("Startled", "startled"));
                    Speech_Dictionary.Add(new SemanticResultValue("Happy", "Happy"));
                    Speech_Dictionary.Add(new SemanticResultValue("Surprise", "Surprise"));

                    //方位介係詞
                    Speech_Dictionary.Add(new SemanticResultValue("Behind", "behind"));
                    Speech_Dictionary.Add(new SemanticResultValue("Under", "under"));
                    Speech_Dictionary.Add(new SemanticResultValue("Over", "over"));
                    Speech_Dictionary.Add(new SemanticResultValue("Above", "above"));
                    Speech_Dictionary.Add(new SemanticResultValue("In front of", "in front of"));
                    Speech_Dictionary.Add(new SemanticResultValue("Below", "below"));
                    Speech_Dictionary.Add(new SemanticResultValue("On left side", "On left side"));
                    Speech_Dictionary.Add(new SemanticResultValue("On right side", "On right side"));
                    Speech_Dictionary.Add(new SemanticResultValue("Down", "down"));

                    //名詞
                    Speech_Dictionary.Add(new SemanticResultValue("Depression", "Depression"));
                    Speech_Dictionary.Add(new SemanticResultValue("Wisdom", "Wisdom"));
                    Speech_Dictionary.Add(new SemanticResultValue("Surgery", "Surgery"));
                    Speech_Dictionary.Add(new SemanticResultValue("Burden", "Burden"));
                    Speech_Dictionary.Add(new SemanticResultValue("Confidence", "Confidence"));
                    Speech_Dictionary.Add(new SemanticResultValue("Anxiety", "Anxiety"));
                    Speech_Dictionary.Add(new SemanticResultValue("Mammal", "Mammal"));
                    Speech_Dictionary.Add(new SemanticResultValue("Container", "Container"));
                    Speech_Dictionary.Add(new SemanticResultValue("Grocery", "Grocery"));
                    Speech_Dictionary.Add(new SemanticResultValue("Spark", "Spark"));
                    Speech_Dictionary.Add(new SemanticResultValue("Coral", "Coral"));
                    Speech_Dictionary.Add(new SemanticResultValue("Property", "Property"));
                    Speech_Dictionary.Add(new SemanticResultValue("Spark", "Spark"));
                    Speech_Dictionary.Add(new SemanticResultValue("Vegetarian", "Vegetarian"));
                    Speech_Dictionary.Add(new SemanticResultValue("Guilty", "Guilty"));

                    Speech_Dictionary.Add(new SemanticResultValue("Relieve the pain from your headache", "Relieve the pain from your headache"));
                    Speech_Dictionary.Add(new SemanticResultValue("He scratched his head", "He scratched his head"));
                    Speech_Dictionary.Add(new SemanticResultValue("The mother comforted her crying baby", "The mother comforted her crying baby"));
                    Speech_Dictionary.Add(new SemanticResultValue("The raft float out to sea", "The raft float out to sea"));
                    Speech_Dictionary.Add(new SemanticResultValue("The last step in the recipe is to sprinkle some sea salt over the cookies", "The last step in the recipe is to sprinkle some sea salt over the cookies"));
                    Speech_Dictionary.Add(new SemanticResultValue("He wandered around the store", "He wandered around the store"));
                    Speech_Dictionary.Add(new SemanticResultValue("Swimming in banned in this lake", "Swimming in banned in this lake"));
                    Speech_Dictionary.Add(new SemanticResultValue("The singer destroyed her reputation", "The singer destroyed her reputation"));
                    Speech_Dictionary.Add(new SemanticResultValue("Why do you despise that politician", "Why do you despise that politician"));
                    Speech_Dictionary.Add(new SemanticResultValue("The sea and the sky merged", "The sea and the sky merged"));
                    Speech_Dictionary.Add(new SemanticResultValue("She overcome her fear of dogs by volunteering at a shelter", "She overcome her fear of dogs by volunteering at a shelter"));
                    Speech_Dictionary.Add(new SemanticResultValue("She never forgot the people who helped launch her acting career", "She never forgot the people who helped launch her acting career"));
                    Speech_Dictionary.Add(new SemanticResultValue("He will attach the label to your luggage", "He will attach the label to your luggage"));
                    Speech_Dictionary.Add(new SemanticResultValue("Our hospital was established forty years ago", "Our hospital was established forty years ago"));
                    Speech_Dictionary.Add(new SemanticResultValue("Many people hailed the director newest movie as her best ever", "Many people hailed the director newest movie as her best ever"));
                    Speech_Dictionary.Add(new SemanticResultValue("The baby has chubby cheeks", "The baby has chubby cheeks"));
                    Speech_Dictionary.Add(new SemanticResultValue("This cheese is a bit smelly", "This cheese is a bit smelly"));
                    Speech_Dictionary.Add(new SemanticResultValue("An unidentified flying object", "An unidentified flying object"));
                    Speech_Dictionary.Add(new SemanticResultValue("He was found guilty", "He was found guilty"));
                    Speech_Dictionary.Add(new SemanticResultValue("The company went bankrupt", "The company went bankrupt"));
                    Speech_Dictionary.Add(new SemanticResultValue("These animals have excellent visual ability", "These animals have excellent visual ability"));
                    Speech_Dictionary.Add(new SemanticResultValue("The man had the mental age of a child", "The man had the mental age of a child"));
                    Speech_Dictionary.Add(new SemanticResultValue("An imaginary friend", "An imaginary friend"));
                    Speech_Dictionary.Add(new SemanticResultValue("Students can apply for individual tuition", "Students can apply for individual tuition"));
                    Speech_Dictionary.Add(new SemanticResultValue("There is vegetarian restaurant over here", "There is vegetarian restaurant over here"));
                    Speech_Dictionary.Add(new SemanticResultValue("She was grieving for the dead baby", "She was grieving for the dead baby"));
                    Speech_Dictionary.Add(new SemanticResultValue("He shouted in anger", "He shouted in anger"));
                    Speech_Dictionary.Add(new SemanticResultValue("He was full of sorrow", "He was full of sorrow"));
                    Speech_Dictionary.Add(new SemanticResultValue("He was startled when his nephew came in", "He was startled when his nephew came in"));
                    Speech_Dictionary.Add(new SemanticResultValue("I am happy to accept your invitation", "I am happy to accept your invitation"));
                    Speech_Dictionary.Add(new SemanticResultValue("The news surprised everyone", "The news surprised everyone"));
                    Speech_Dictionary.Add(new SemanticResultValue("There is a big tree behind the house", "There is a big tree behind the house"));
                    Speech_Dictionary.Add(new SemanticResultValue("They were playing chess under the tree", "They were playing chess under the tree"));
                    Speech_Dictionary.Add(new SemanticResultValue("We live over a small bookstore", "We live over a small bookstore"));
                    Speech_Dictionary.Add(new SemanticResultValue("Her skirt came below her knees", "Her skirt came below her knees"));
                    Speech_Dictionary.Add(new SemanticResultValue("The moon is now above the trees", "The moon is now above the trees"));
                    Speech_Dictionary.Add(new SemanticResultValue("The bus stops right in front of our house", "The bus stops right in front of our house"));
                    Speech_Dictionary.Add(new SemanticResultValue("Their house is half-way down the hill", "Their house is half-way down the hill"));
                    Speech_Dictionary.Add(new SemanticResultValue("A lack of sleep can cause depression", "A lack of sleep can cause depression"));
                    Speech_Dictionary.Add(new SemanticResultValue("The old woman had gained a lot of wisdom during her life", "The old woman had gained a lot of wisdom during her life"));
                    Speech_Dictionary.Add(new SemanticResultValue("The condition is serious it will need surgery", "The condition is serious it will need surgery"));
                    Speech_Dictionary.Add(new SemanticResultValue("He carried a lot of burden", "He carried a lot of burden"));
                    Speech_Dictionary.Add(new SemanticResultValue("I have confidence", "I have confidence"));
                    Speech_Dictionary.Add(new SemanticResultValue("Whales are mammals but fish are not", "Whales are mammals but fish are not"));
                    Speech_Dictionary.Add(new SemanticResultValue("He put the leftover in a container", "He put the leftover in a container"));
                    Speech_Dictionary.Add(new SemanticResultValue("I go to grocery shopping every Friday", "I go to grocery shopping every Friday"));
                    Speech_Dictionary.Add(new SemanticResultValue("A cigarette spark started the fire", "A cigarette spark started the fire"));
                    Speech_Dictionary.Add(new SemanticResultValue("Coral is often used for making jewellery", "Coral is often used for making jewellery"));
                    Speech_Dictionary.Add(new SemanticResultValue("This small house is my only property", "This small house is my only property"));
                    break;
                    #endregion
            }
        }

    
        /// <summary>
        /// 回傳單字
        /// </summary>
        public String Give_Vocabulary(int posture_number) {

            if (posture_number < vocabulary_list.Count)
            {
                give_Vocabulary = Convert.ToString(vocabulary_list[posture_number].vocabulary);
            }
            else
            {
                return "You have completed the vocabulary practice, please keep on practicing";
            }
            return give_Vocabulary;
        }

        /// <summary>
        /// 回傳單字的範例句子
        /// </summary>
        public String Give_Sentence(int posture_number) {

            if (posture_number < vocabulary_list.Count)
            {
                give_Sentence = Convert.ToString(vocabulary_list[posture_number].sentence);
            }
            else
            {
                return "";
            }
            return give_Sentence;
        }

        /// <summary>
        /// 回傳圖片
        /// </summary>
        public BitmapSource Give_Image(int posture_number) {

            if (posture_number < vocabulary_list.Count)
            {
                ImageSource = vocabulary_list[posture_number].ImageSource;
            }
            else
            {
                return ImageSource = null;
            }
            return ImageSource;
        }

        //回傳提示圖片
        public BitmapSource QueryImage(String vocabulary)
        {
            var VocabularyList = vocabulary_list.Find(x => x.vocabulary == vocabulary);
            return VocabularyList.ImageSource;
        }

        //回傳學生所做得句子
        public String Give_StudentsSentence(int posture_number)
        {

            if (posture_number < sentencesList.Count)
            {
                give_StudentsSentences = Convert.ToString(sentencesList[posture_number].sentence);
            }
            else
            {
                return "You have completed the sentence practice, please keep on practicing";
            }
            return give_StudentsSentences;
        }

        //回傳句子進度
        public String ReturnSentenceProgress(int posture_number)
        {
            if (posture_number < sentencesList.Count)
            {
                return posture_number.ToString() + "/" + sentencesList.Count.ToString();
            }
            else
            {
                return sentencesList.Count.ToString() + "/" + sentencesList.Count.ToString();
            }
        }

        //回傳句子範例中文
        public String GiveChineseSentence(int posture_number)
        {

            String Sentence;

            if (posture_number < sentencesList.Count)
            {
                Sentence = sentencesList[posture_number].chinese;
            }
            else
            {
                return "";
            }
            return Sentence;
        }

        //回傳偵測方式
        public String Give_Detection(int posture_number)
        {

            if (posture_number < vocabulary_list.Count)
            {
                giveDetection = Convert.ToString(vocabulary_list[posture_number].detection);
            }
            else
            {
                return "";
            }
            return giveDetection;
        }
    }
}
