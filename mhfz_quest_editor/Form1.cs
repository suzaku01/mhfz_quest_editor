using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Lib;

namespace mhfz_quest_editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(File.ReadAllLines("monster.txt"));
            comboBox2.Items.AddRange(File.ReadAllLines("item.txt"));
            comboBox6.Items.AddRange(File.ReadAllLines("item.txt"));
            comboBox7.Items.AddRange(File.ReadAllLines("item.txt"));
            radioButton3.Checked = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileloc = openFileDialog1.FileName;
                byte[] ba = File.ReadAllBytes(fileloc);

                if (ba[0] == 192)
                {
                    textBox32.Text = BitConverter.ToString(ba).Replace("-", string.Empty);

                    //Load location
                    List.Location.TryGetValue(ba[228], out string lcoation);
                    textBox23.Text = lcoation.ToString();

                    //Load carve rank
                    List.Rank.TryGetValue(ba[92], out string rank);
                    comboBox4.Text = rank.ToString();

                    //Load fee
                    string fee = ba[206].ToString("X2") + ba[205].ToString("X2") + ba[204].ToString("X2");
                    numericUpDown1.Value = Convert.ToInt32(fee, 16);

                    //Load penalty
                    string penalty = ba[214].ToString("X2") + ba[213].ToString("X2") + ba[212].ToString("X2");
                    numericUpDown8.Value = Convert.ToInt32(penalty, 16);

                    /////////////////////////////////////////////////////////////////////////////////////////////////

                    //Load main obj type
                    string mot = ba[240].ToString("X2") + ba[241].ToString("X2") + ba[242].ToString("X2") + ba[243].ToString("X2");
                    List.ObjectiveType.TryGetValue(mot, out string mot1);
                    comboBox8.Text = mot1.ToString();

                    //Load main obj target
                    if (!(mot == "02000000"))
                    {
                        string motgt = ba[244].ToString("X2");
                        List.Monster.TryGetValue(motgt, out string motgt1);
                        textBox17.Text = motgt1;
                    }
                    else
                    {
                        //get item id
                        int moti = Convert.ToInt32(ba[245].ToString("X2") + ba[244].ToString("X2"), 16);
                        List.Item.TryGetValue(moti, out string moti1);
                        textBox17.Text = moti1;
                    }

                    //Load main obj amount
                    string moa = ba[247].ToString("X2") + ba[246].ToString("X2");
                    numericUpDown5.Value = Convert.ToInt32(moa, 16);

                    //Load main obj reward
                    string mor = ba[210].ToString("X2") + ba[209].ToString("X2") + ba[208].ToString("X2");
                    numericUpDown2.Value = Convert.ToInt32(mor, 16);
                    //////////////////////////////////////////////////////////////////////////////////////////

                    //Load suba obj type
                    string subaot = ba[248].ToString("X2") + ba[249].ToString("X2") + ba[250].ToString("X2") + ba[251].ToString("X2");
                    List.ObjectiveType.TryGetValue(subaot, out string subaot1);
                    comboBox9.Text = subaot1;

                    //Load suba obj target
                    if (!(subaot == "02000000"))
                    {
                        string sub1otgt = ba[252].ToString("X2");
                        List.Monster.TryGetValue(sub1otgt, out string sub1otgt1);
                        textBox18.Text = sub1otgt1;
                    }
                    else
                    {
                        //get item id
                        int sub1ti = Convert.ToInt32(ba[253].ToString("X2") + ba[252].ToString("X2"), 16);
                        List.Item.TryGetValue(sub1ti, out string sub1ti1);
                        textBox18.Text = sub1ti1;
                    }

                    //Load suba obj amount
                    string sub1oa = ba[255].ToString("X2") + ba[254].ToString("X2"); //0A50
                    numericUpDown6.Value = Convert.ToInt32(sub1oa, 16);

                    //Load suba obj reward
                    string subaor = ba[218].ToString("X2") + ba[217].ToString("X2") + ba[216].ToString("X2");
                    numericUpDown3.Value = Convert.ToInt32(subaor, 16);
                    ////////////////////////////////////////////////////////////////////////////////

                    //Load subb obj type
                    string subbot = ba[256].ToString("X2") + ba[257].ToString("X2") + ba[258].ToString("X2") + ba[259].ToString("X2");
                    List.ObjectiveType.TryGetValue(subbot, out string subbot1);
                    comboBox10.Text = subbot1.ToString();

                    //Load subb obj target
                    if (!(subbot == "02000000"))
                    {
                        string subbotgt = ba[260].ToString("X2");
                        List.Monster.TryGetValue(subbotgt, out string subbotgt1);
                        textBox19.Text = subbotgt1;
                    }
                    else
                    {
                        //get item id
                        int subbti = Convert.ToInt32(ba[261].ToString("X2") + ba[260].ToString("X2"), 16);
                        List.Item.TryGetValue(subbti, out string subbti1);
                        textBox19.Text = subbti1;

                    }

                    //Load subb obj amount
                    string subboa = ba[263].ToString("X2") + ba[262].ToString("X2");
                    numericUpDown7.Value = Convert.ToInt32(subboa, 16);

                    //Load subb obj reward
                    string subbor = ba[223].ToString("X2") + ba[222].ToString("X2") + ba[221].ToString("X2") + ba[220].ToString("X2");
                    numericUpDown4.Value = Convert.ToInt32(subbor, 16);
                    ///////////////////////////////////////////////////////////////////////////////////////

                    //Load text
                    int questStringsStart = BitConverter.ToInt32(ba, 48);       //go and get 4C80
                    int readPointer = BitConverter.ToInt32(ba, questStringsStart);  //go text field

                    int pTitleAndName = BitConverter.ToInt32(ba, (readPointer - 32));
                    int pMainoObj = BitConverter.ToInt32(ba, (readPointer - 28));
                    int pAObj = BitConverter.ToInt32(ba, (readPointer - 24));
                    int pBObj = BitConverter.ToInt32(ba, (readPointer - 20));
                    int pClearC = BitConverter.ToInt32(ba, (readPointer - 16));
                    int pFailC = BitConverter.ToInt32(ba, (readPointer - 12));
                    int pHirer = BitConverter.ToInt32(ba, (readPointer - 8));
                    int pText = BitConverter.ToInt32(ba, (readPointer - 4));

                    byte[] tTitleAndName = File.ReadAllBytes(fileloc).Skip(pTitleAndName).Take(pMainoObj - pTitleAndName).ToArray();
                    string str = Encoding.GetEncoding("Shift_JIS").GetString(tTitleAndName);           //encode as shift jis

                    textBox2.Text = Encoding.GetEncoding("Shift_JIS").GetString(File.ReadAllBytes(fileloc).Skip(pTitleAndName).Take(pMainoObj - pTitleAndName).ToArray()).Replace("\n", "\r\n");

                    textBox3.Text = Encoding.GetEncoding("Shift_JIS").GetString(File.ReadAllBytes(fileloc).Skip(pMainoObj).Take(pAObj - pMainoObj).ToArray());      //Main
                    string tB = Encoding.GetEncoding("Shift_JIS").GetString(File.ReadAllBytes(fileloc).Skip(pBObj).Take(pClearC - pBObj).ToArray());                //B
                    textBox12.Text = tB; //B
                    string tA = textBox7.Text = Encoding.GetEncoding("Shift_JIS").GetString(File.ReadAllBytes(fileloc).Skip(pAObj).Take(pBObj - pAObj).ToArray());
                    if (string.IsNullOrEmpty(tA))
                    {
                        textBox7.Text = tB;
                    }

                    textBox13.Text = Encoding.GetEncoding("Shift_JIS").GetString(File.ReadAllBytes(fileloc).Skip(pClearC).Take(pFailC - pClearC).ToArray());        //Clear
                    textBox14.Text = Encoding.GetEncoding("Shift_JIS").GetString(File.ReadAllBytes(fileloc).Skip(pFailC).Take(pHirer - pFailC).ToArray()).Replace("\n", "\r\n");

                    textBox15.Text = Encoding.GetEncoding("Shift_JIS").GetString(File.ReadAllBytes(fileloc).Skip(pHirer).Take(pText - pHirer).ToArray());
                    textBox16.Text = Encoding.GetEncoding("Shift_JIS").GetString(File.ReadAllBytes(fileloc).Skip(pText).Take(ba.Length - pText).ToArray()).Replace("\n", "\r\n");

                    //Load supply item
                    int SupplyInfoStart = BitConverter.ToInt32(ba, 8);
                    byte[] SupplyInfoArray = File.ReadAllBytes(fileloc).Skip(SupplyInfoStart).Take(160).ToArray();

                    for (int i = 0; i < 40; i++)
                    {

                        if (i < 10)
                        {
                            for (int t = 0; t < 10; t += 1)
                            {
                                int u = 4 * t;
                                string SupplyItemID = SupplyInfoArray[u + 1].ToString("X2") + SupplyInfoArray[u].ToString("X2");
                                List.Item.TryGetValue(Convert.ToInt32(SupplyItemID, 16), out string SupplyItemName);
                                ((TextBox)this.Controls.Find("textBox10" + t.ToString(), true)[0]).Text = SupplyItemName;

                                int p = 4 * t + 2;
                                string SupplyItemAmount = SupplyInfoArray[p + 1].ToString("X2") + SupplyInfoArray[p].ToString("X2");
                                int SupplyItemAmount1 = (Convert.ToInt32(SupplyItemAmount, 16));
                                ((NumericUpDown)this.Controls.Find("numericUpDown10" + t.ToString(), true)[0]).Value = SupplyItemAmount1;
                            }

                        }
                        else if (i < 40)
                        {
                            for (int t = 10; t < 40; t += 1)
                            {
                                int u = 4 * t;
                                string SupplyItemID = SupplyInfoArray[u + 1].ToString("X2") + SupplyInfoArray[u].ToString("X2");
                                List.Item.TryGetValue(Convert.ToInt32(SupplyItemID, 16), out string SupplyItemName);
                                ((TextBox)this.Controls.Find("textBox1" + t.ToString(), true)[0]).Text = SupplyItemName;

                                int p = 4 * t + 2;
                                string SupplyItemAmount = SupplyInfoArray[p + 1].ToString("X2") + SupplyInfoArray[p].ToString("X2");
                                int SupplyItemAmount1 = (Convert.ToInt32(SupplyItemAmount, 16));
                                ((NumericUpDown)this.Controls.Find("numericUpDown1" + t.ToString(), true)[0]).Value = SupplyItemAmount1;
                            }
                        }
                    }

                    //Load reward item
                    int RewardInfoStart = BitConverter.ToInt32(ba, 12);
                    int Basenum = 0;
                    if (SupplyInfoStart == 200)
                    {
                        //Basenum = SupplyInfoStart - RewardInfoStart;
                        Basenum = BitConverter.ToInt32(ba, 48) - RewardInfoStart;
                    }
                    else
                    {
                        //Basenum =  BitConverter.ToInt32(ba, 48) - RewardInfoStart;
                        Basenum = SupplyInfoStart - RewardInfoStart;
                    }


                    int HaveAobj = Convert.ToInt32(ba[248].ToString("X2") + ba[249].ToString("X2") + ba[250].ToString("X2") + ba[251].ToString("X2"));
                    int HaveBobj = Convert.ToInt32(ba[256].ToString("X2") + ba[257].ToString("X2") + ba[258].ToString("X2") + ba[259].ToString("X2"));
                    byte[] RewardInfoHeader = File.ReadAllBytes(fileloc).Skip(RewardInfoStart).Take(Basenum - 24).ToArray();

                    int MainRewardOffset = Convert.ToInt32(RewardInfoHeader[5].ToString("X2") + RewardInfoHeader[4].ToString("X2"), 16);
                    int ARewardOffset = Convert.ToInt32(RewardInfoHeader[13].ToString("X2") + RewardInfoHeader[12].ToString("X2"), 16);
                    int BRewardOffset = Convert.ToInt32(RewardInfoHeader[21].ToString("X2") + RewardInfoHeader[20].ToString("X2"), 16);

                    byte[] MainRewardText = File.ReadAllBytes(fileloc).Skip(MainRewardOffset).Take(48).ToArray();
                    byte[] ARewardText = File.ReadAllBytes(fileloc).Skip(ARewardOffset).Take(48).ToArray();
                    byte[] BRewardText = File.ReadAllBytes(fileloc).Skip(BRewardOffset).Take(48).ToArray();

                    for (int i = 0; i < 8; i++)
                    {
                        //Load chance
                        int MainRewardChance = MainRewardText[i * 6];
                        if (MainRewardChance == 255)
                        {
                            break;
                        }
                        ((NumericUpDown)this.Controls.Find("numericUpDown20" + i.ToString(), true)[0]).Value = MainRewardChance;

                        //Load amount
                        int MainRewardAmount = MainRewardText[i * 6 + 4];
                        ((NumericUpDown)this.Controls.Find("numericUpDown21" + i.ToString(), true)[0]).Value = MainRewardAmount;

                        //Load id
                        int MainRewardId = Convert.ToInt32(MainRewardText[i * 6 + 3].ToString("X2") + MainRewardText[i * 6 + 2].ToString("X2"), 16);
                        List.Item.TryGetValue(MainRewardId, out string MainRewardName);
                        ((TextBox)this.Controls.Find("textBox22" + i.ToString(), true)[0]).Text = MainRewardName;
                    }

                    if (!((HaveAobj == 0)) & !(ARewardOffset == 0000))
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            int ARewardChance = ARewardText[k * 6];
                            if (ARewardChance == 255)
                            {
                                break;
                            }
                                //Load chance
                                ((NumericUpDown)this.Controls.Find("numericUpDown31" + k.ToString(), true)[0]).Value = ARewardChance;

                            //Load amount
                            int ARewardAmount = ARewardText[k * 6 + 4];
                            ((NumericUpDown)this.Controls.Find("numericUpDown30" + k.ToString(), true)[0]).Value = ARewardAmount;

                            //Load id
                            int BRewardId = Convert.ToInt32(ARewardText[k * 6 + 3].ToString("X2") + ARewardText[k * 6 + 2].ToString("X2"), 16);
                            List.Item.TryGetValue(BRewardId, out string ARewardName);
                            ((TextBox)this.Controls.Find("textBox30" + k.ToString(), true)[0]).Text = ARewardName;

                        }
                    }

                    if (!(HaveBobj == 0))
                    {
                        for (int f = 0; f < 8; f++)
                        {
                            int BRewardChance = BRewardText[f * 6];
                            if (BRewardChance == 255)
                            {
                                break;
                            }
                                //Load chance
                                ((NumericUpDown)this.Controls.Find("numericUpDown41" + f.ToString(), true)[0]).Value = BRewardChance;

                            //Load amount
                            int BRewardAmount = BRewardText[f * 6 + 4];
                            ((NumericUpDown)this.Controls.Find("numericUpDown40" + f.ToString(), true)[0]).Value = BRewardAmount;

                            //Load id
                            int BRewardId = Convert.ToInt32(BRewardText[f * 6 + 3].ToString("X2") + BRewardText[f * 6 + 2].ToString("X2"), 16);
                            List.Item.TryGetValue(BRewardId, out string BRewardName);
                            ((TextBox)this.Controls.Find("textBox40" + f.ToString(), true)[0]).Text = BRewardName;

                        }
                    }

                    //Load large monster
                    string MainMonsID = ba[BitConverter.ToInt32(ba, 24) + 64].ToString("X2");
                    List.Monster.TryGetValue(MainMonsID, out string MainMonsStr);
                    comboBox11.Text = MainMonsStr;

                    numericUpDown9.Value = BitConverter.ToInt16(ba, 72);    //str
                    numericUpDown10.Value = BitConverter.ToInt16(ba, 68);   //size
                    numericUpDown11.Value = BitConverter.ToInt16(ba, 70);   //size range

                    //Load small monster
                    numericUpDown12.Value = BitConverter.ToInt16(ba, 97);

                    //Lod HRP
                    numericUpDown13.Value = BitConverter.ToInt32(ba, 76);
                    numericUpDown14.Value = BitConverter.ToInt32(ba, 84);
                    numericUpDown15.Value = BitConverter.ToInt32(ba, 88);

                    radioButton3.Checked = true;    //load suc
                    button2.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Invalid file","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    radioButton3.Checked = false;
                    button2.Enabled = false;
                }



            }
        }


        private void button2_Click(object sender, EventArgs e)
        {


            saveFileDialog1.Filter = "questID(*.bin)|*.bin|" + "All files(*.*)|*.*";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;                                   //open save file dialog
            }

            string path = saveFileDialog1.FileName;
            string fileloc = openFileDialog1.FileName;

            byte[] ba = File.ReadAllBytes(fileloc);
            int questStringsStart = BitConverter.ToInt32(ba, 48);
            int readPointer = BitConverter.ToInt32(ba, questStringsStart);
            int pTitleAndName = BitConverter.ToInt32(ba, (readPointer - 32));
            int RewardHeaderStart = BitConverter.ToInt32(ba, 12);
            int SupplyInfoStart = BitConverter.ToInt32(ba, 8);

            string HexData = textBox32.Text;        //get entire text string
            var EntireBytes = new List<byte>();          //create empty byte list
            for (int i = 0; i < HexData.Length / 2; i++)
            {
                EntireBytes.Add(Convert.ToByte(HexData.Substring(i * 2, 2), 16));        //convert string to byte list
            }
            EntireBytes.RemoveRange(pTitleAndName, EntireBytes.Count - pTitleAndName);        //Remove all text data to add new one


            //Write
            List<byte> divider = new List<byte>
            {
                00
            };

            ////Title and name
            EntireBytes.AddRange(Encoding.GetEncoding("Shift_JIS").GetBytes(textBox2.Text.Replace("\r\n", "\n")).ToList());
            EntireBytes.AddRange(divider);

            ////Main obj
            byte[] mo = BitConverter.GetBytes(EntireBytes.Count);
            EntireBytes.AddRange(Encoding.GetEncoding("Shift_JIS").GetBytes(textBox3.Text.Replace("\r\n", "\n")).ToList());
            EntireBytes.AddRange(divider);

            ////Sub A
            byte[] sa = BitConverter.GetBytes(EntireBytes.Count);
            EntireBytes.AddRange(Encoding.GetEncoding("Shift_JIS").GetBytes(textBox7.Text.Replace("\r\n", "\n")).ToList());
            EntireBytes.AddRange(divider);

            ////SUb B
            byte[] sb = BitConverter.GetBytes(EntireBytes.Count);
            EntireBytes.AddRange(Encoding.GetEncoding("Shift_JIS").GetBytes(textBox12.Text.Replace("\r\n", "\n")).ToList());
            EntireBytes.AddRange(divider);

            ////Clear
            byte[] cc = BitConverter.GetBytes(EntireBytes.Count);
            EntireBytes.AddRange(Encoding.GetEncoding("Shift_JIS").GetBytes(textBox13.Text.Replace("\r\n", "\n")).ToList());
            EntireBytes.AddRange(divider);

            ////Fail
            byte[] fc = BitConverter.GetBytes(EntireBytes.Count);
            EntireBytes.AddRange(Encoding.GetEncoding("Shift_JIS").GetBytes(textBox14.Text.Replace("\r\n", "\n")).ToList());
            EntireBytes.AddRange(divider);

            ////Emp
            byte[] em = BitConverter.GetBytes(EntireBytes.Count);
            EntireBytes.AddRange(Encoding.GetEncoding("Shift_JIS").GetBytes(textBox15.Text.Replace("\r\n", "\n")).ToList());
            EntireBytes.AddRange(divider);

            ////Text
            byte[] tx = BitConverter.GetBytes(EntireBytes.Count);
            EntireBytes.AddRange(Encoding.GetEncoding("Shift_JIS").GetBytes(textBox16.Text.Replace("\r\n", "\n")).ToList());
            EntireBytes.AddRange(divider);

            //Reward
            List<byte> Endofline = new List<byte>
            {
                     255, 255, 00, 00
            };

            string IfA = ba[248].ToString("X2") + ba[249].ToString("X2") + ba[250].ToString("X2") + ba[251].ToString("X2");
            string IfB = ba[256].ToString("X2") + ba[257].ToString("X2") + ba[258].ToString("X2") + ba[259].ToString("X2");

            int kariL = EntireBytes.Count;
            byte[] NewRwdHeader = BitConverter.GetBytes(kariL);
            EntireBytes[12] = NewRwdHeader[0];
            EntireBytes[13] = NewRwdHeader[1];

            if (!(IfB == "00000000"))
            {
                //Main A B
                List<byte> oMAB = new List<byte>
                {
                    01, 128, 00, 00, 255, 255, 00, 00, 02, 128, 00, 00, 255, 255, 00, 00, 03, 128, 00, 00, 255, 255, 00, 00, 255, 255, 00, 00, 00, 00, 00, 00
                };
                EntireBytes.AddRange(oMAB);                        //add header and offset
                kariL = EntireBytes.Count;
                byte[] MABoffset = BitConverter.GetBytes(kariL);
                byte[] MABoffset2 = BitConverter.GetBytes(kariL + 52);
                byte[] MABoffset3 = BitConverter.GetBytes(kariL + 104);

                EntireBytes[kariL - 27] = MABoffset[1];     //replace new header
                EntireBytes[kariL - 28] = MABoffset[0];     //replace new header

                List<byte> MainReward = new List<byte> { };       //create new list
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown200.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox220.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown210.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown201.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox221.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown211.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown202.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox222.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown212.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown203.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox223.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown213.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown204.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox224.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown214.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown205.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox225.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown215.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown206.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox226.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown216.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown207.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox227.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown217.Value)));
                MainReward.AddRange(Endofline);
                EntireBytes.AddRange(MainReward);

                //A
                EntireBytes[kariL - 19] = MABoffset2[1];     //replace new header
                EntireBytes[kariL - 20] = MABoffset2[0];     //replace new header

                List<byte> MainReward2 = new List<byte> { };       //create new list
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown310.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox300.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown300.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown311.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox301.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown301.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown312.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox302.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown302.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown313.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox303.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown303.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown314.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox304.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown304.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown315.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox305.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown305.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown316.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox306.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown306.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown317.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox307.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown307.Value)));
                MainReward2.AddRange(Endofline);
                EntireBytes.AddRange(MainReward2);

                //B
                EntireBytes[kariL - 11] = MABoffset3[1];     //replace new header
                EntireBytes[kariL - 12] = MABoffset3[0];     //replace new header

                List<byte> MainReward3 = new List<byte> { };       //create new list
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown410.Value)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox400.Text).Key)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown400.Value)));

                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown411.Value)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox401.Text).Key)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown401.Value)));

                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown412.Value)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox402.Text).Key)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown402.Value)));

                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown413.Value)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox403.Text).Key)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown403.Value)));

                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown414.Value)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox404.Text).Key)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown404.Value)));

                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown415.Value)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox405.Text).Key)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown405.Value)));

                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown416.Value)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox406.Text).Key)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown406.Value)));

                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown417.Value)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox407.Text).Key)));
                MainReward3.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown407.Value)));
                MainReward3.AddRange(Endofline);
                EntireBytes.AddRange(MainReward3);


            }
            else if (!(IfA == "00000000"))
            {
                //Main A
                List<byte> oMA = new List<byte>
                {
                    01, 128, 00, 00, 255, 255, 00, 00, 02, 128, 00, 00, 255, 255, 00, 00, 255, 255, 00, 00, 00, 00, 00, 00
                };
                EntireBytes.AddRange(oMA);                        //add header and offset
                kariL = EntireBytes.Count;
                byte[] MABoffset = BitConverter.GetBytes(kariL);
                byte[] MABoffset2 = BitConverter.GetBytes(kariL + 52);
                byte[] MABoffset3 = BitConverter.GetBytes(kariL + 104);

                EntireBytes[kariL - 19] = MABoffset[1];     //replace new header
                EntireBytes[kariL - 20] = MABoffset[0];     //replace new header

                List<byte> MainReward = new List<byte> { };       //create new list
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown200.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox220.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown210.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown201.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox221.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown211.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown202.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox222.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown212.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown203.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox223.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown213.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown204.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox224.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown214.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown205.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox225.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown215.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown206.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox226.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown216.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown207.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox227.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown217.Value)));
                MainReward.AddRange(Endofline);
                EntireBytes.AddRange(MainReward);

                //A
                EntireBytes[kariL - 11] = MABoffset2[1];     //replace new header
                EntireBytes[kariL - 12] = MABoffset2[0];     //replace new header

                List<byte> MainReward2 = new List<byte> { };       //create new list
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown310.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox300.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown300.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown311.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox301.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown301.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown312.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox302.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown302.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown313.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox303.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown303.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown314.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox304.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown304.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown315.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox305.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown305.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown316.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox306.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown306.Value)));

                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown317.Value)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox307.Text).Key)));
                MainReward2.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown307.Value)));
                MainReward2.AddRange(Endofline);
                EntireBytes.AddRange(MainReward2);
            }
            else
            {
                //Main
                List<byte> oM = new List<byte>
                {
                    01, 128, 00, 00, 255, 255, 00, 00, 255, 255, 00, 00, 00, 00, 00, 00
                };
                EntireBytes.AddRange(oM);                        //add header and offset
                kariL = EntireBytes.Count;
                byte[] MABoffset = BitConverter.GetBytes(kariL);
                byte[] MABoffset2 = BitConverter.GetBytes(kariL + 52);
                byte[] MABoffset3 = BitConverter.GetBytes(kariL + 104);

                EntireBytes[kariL - 11] = MABoffset[1];     //replace new header
                EntireBytes[kariL - 12] = MABoffset[0];     //replace new header

                List<byte> MainReward = new List<byte> { };       //create new list
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown200.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox220.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown210.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown201.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox221.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown211.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown202.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox222.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown212.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown203.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox223.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown213.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown204.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox224.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown214.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown205.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox225.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown215.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown206.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox226.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown216.Value)));

                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown207.Value)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(List.Item.FirstOrDefault(x => x.Value == textBox227.Text).Key)));
                MainReward.AddRange(BitConverter.GetBytes(decimal.ToInt16(numericUpDown217.Value)));
                MainReward.AddRange(Endofline);
                EntireBytes.AddRange(MainReward);

            }

            //Supply

            byte[] eb2 = EntireBytes.ToArray();
            byte[] NewSplHeader = BitConverter.GetBytes(EntireBytes.Count);
            EntireBytes[8] = NewSplHeader[0];
            EntireBytes[9] = NewSplHeader[1];

            byte[] Temprewardarray = { 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00 };

            for (int i = 0; i < 40; i++)
            {

                if (i < 10)
                {
                    for (int t = 0; t < 10; t += 1)
                    {
                        int p = t * 4 + 2;
                        Temprewardarray[p] = BitConverter.GetBytes(decimal.ToInt32(((NumericUpDown)this.Controls.Find("numericUpDown10" + t.ToString(), true)[0]).Value))[0];  //amount
                        //EntireBytes[SupplyInfoStart + p] = BitConverter.GetBytes(decimal.ToInt32(((NumericUpDown)this.Controls.Find("numericUpDown10" + t.ToString(), true)[0]).Value))[0];

                        string spstr = ((TextBox)this.Controls.Find("textBox10" + t.ToString(), true)[0]).Text;
                        string spid = List.Item.FirstOrDefault(x => x.Value == spstr).Key.ToString();
                        if (spid.Length == 1)
                        {
                            spid = "00000" + spid;
                        }
                        else if (spid.Length == 2)
                        {
                            spid = "0000" + spid;
                        }
                        else if (spid.Length == 3)
                        {
                            spid = "000" + spid;
                        }
                        else if (spid.Length == 4)
                        {
                            spid = "00" + spid;
                        }
                        else if (spid.Length == 6)
                        {
                            spid = "0" + spid;
                        }
                        byte[] spbyte = BitConverter.GetBytes(Convert.ToInt32(spid));
                        Temprewardarray[p - 2] = spbyte[0];
                        Temprewardarray[p - 1] = spbyte[1];
                    }

                }
                else if (i < 40)
                {
                    for (int t = 10; t < 40; t += 1)
                    {
                        int p = t * 4 + 2;
                        Temprewardarray[p] = BitConverter.GetBytes(decimal.ToInt32(((NumericUpDown)this.Controls.Find("numericUpDown1" + t.ToString(), true)[0]).Value))[0];

                        string spstr = ((TextBox)this.Controls.Find("textBox1" + t.ToString(), true)[0]).Text;
                        string spid = List.Item.FirstOrDefault(x => x.Value == spstr).Key.ToString();
                        if (spid.Length == 1)
                        {
                            spid = "00000" + spid;
                        }
                        else if (spid.Length == 2)
                        {
                            spid = "0000" + spid;
                        }
                        else if (spid.Length == 3)
                        {
                            spid = "000" + spid;
                        }
                        else if (spid.Length == 4)
                        {
                            spid = "00" + spid;
                        }
                        else if (spid.Length == 6)
                        {
                            spid = "0" + spid;
                        }
                        byte[] spbyte = BitConverter.GetBytes(Convert.ToInt32(spid));
                        Temprewardarray[p - 2] = spbyte[0];
                        Temprewardarray[p - 1] = spbyte[1];
                    }
                }
            }
            EntireBytes.AddRange(Temprewardarray);







            //Write new text offset
            eb2 = EntireBytes.ToArray();
            eb2[readPointer - 28] = mo[0];
            eb2[readPointer - 28 + 1] = mo[1];
            eb2[readPointer - 24] = sa[0];
            eb2[readPointer - 24 + 1] = sa[1];
            eb2[readPointer - 20] = sb[0];
            eb2[readPointer - 20 + 1] = sb[1];
            eb2[readPointer - 16] = cc[0];
            eb2[readPointer - 16 + 1] = cc[1];
            eb2[readPointer - 12] = fc[0];
            eb2[readPointer - 12 + 1] = fc[1];
            eb2[readPointer - 8] = em[0];
            eb2[readPointer - 8 + 1] = em[1];
            eb2[readPointer - 4] = tx[0];
            eb2[readPointer - 4 + 1] = tx[1];

            //Fee
            byte[] feeb = BitConverter.GetBytes(decimal.ToInt32(numericUpDown1.Value));     
            eb2[204] = feeb[0];
            eb2[205] = feeb[1];
            eb2[206] = feeb[2];

            //Penalty
            byte[] penalty = BitConverter.GetBytes(decimal.ToInt32(numericUpDown8.Value));
            eb2[212] = penalty[0];
            eb2[213] = penalty[1];
            eb2[214] = penalty[2];

            //Main
            string mat = List.ObjectiveType.FirstOrDefault(x => x.Value == comboBox8.Text).Key;
            byte[] madata = Enumerable.Range(0, mat.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(mat.Substring(x, 2), 16)).ToArray();
            eb2[240] = madata[0];
            eb2[241] = madata[1];
            eb2[242] = madata[2];
            eb2[243] = madata[3];

            if (madata[0] == 02 & madata[1] == 00)
            {
                int mot = List.Item.FirstOrDefault(x => x.Value == textBox17.Text).Key;
                byte[] motitem = BitConverter.GetBytes(mot);
                eb2[244] = motitem[0];
                eb2[245] = motitem[1];
            } else
            {
                string mot = List.Monster.FirstOrDefault(x => x.Value == textBox17.Text).Key;
                eb2[244] = Convert.ToByte(mot, 16);
            }

            byte[] mamt = BitConverter.GetBytes(decimal.ToInt32(numericUpDown5.Value));
            eb2[246] = mamt[0];
            eb2[247] = mamt[1];

            byte[] mrwd = BitConverter.GetBytes(decimal.ToInt32(numericUpDown2.Value));
            eb2[208] = mrwd[0];
            eb2[209] = mrwd[1];
            eb2[210] = mrwd[2];

            //Sub A
            string subat = List.ObjectiveType.FirstOrDefault(x => x.Value == comboBox9.Text).Key;
            if (!(subat == "00000000"))
            {
                byte[] subadata = Enumerable.Range(0, subat.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(subat.Substring(x, 2), 16)).ToArray();
                eb2[248] = subadata[0];
                eb2[249] = subadata[1];
                eb2[250] = subadata[2];
                eb2[251] = subadata[3];

                if (subadata[0] == 02 & subadata[1] == 00)
                {
                    int subatt = List.Item.FirstOrDefault(x => x.Value == textBox18.Text).Key;
                    byte[] subaitem = BitConverter.GetBytes(subatt);
                    eb2[252] = subaitem[0];
                    eb2[253] = subaitem[1];
                }
                else
                {
                    string subatt = List.Monster.FirstOrDefault(x => x.Value == textBox18.Text).Key;
                    eb2[252] = Convert.ToByte(subatt, 16);
                }

                byte[] subaamt = BitConverter.GetBytes(decimal.ToInt32(numericUpDown6.Value));
                eb2[254] = subaamt[0];
                eb2[255] = subaamt[1];

                byte[] subatrd = BitConverter.GetBytes(decimal.ToInt32(numericUpDown3.Value));
                eb2[216] = subatrd[0];
                eb2[217] = subatrd[1];
                eb2[218] = subatrd[2];
            }

            //Sub B
            string subbt = List.ObjectiveType.FirstOrDefault(x => x.Value == comboBox10.Text).Key;
            if (!(subbt == "00000000"))
            {
                byte[] subbdata = Enumerable.Range(0, subbt.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(subbt.Substring(x, 2), 16)).ToArray();
                eb2[248] = subbdata[0];
                eb2[249] = subbdata[1];
                eb2[250] = subbdata[2];
                eb2[251] = subbdata[3];

                if (subbdata[0] == 02 & subbdata[1] == 00)
                {
                    int subbtt = List.Item.FirstOrDefault(x => x.Value == textBox19.Text).Key;
                    byte[] subbitem = BitConverter.GetBytes(subbtt);
                    eb2[252] = subbitem[0];
                    eb2[253] = subbitem[1];
                }
                else
                {
                    string subbtt = List.Monster.FirstOrDefault(x => x.Value == textBox19.Text).Key;
                    eb2[252] = Convert.ToByte(subbtt, 16);
                }

                byte[] subbamt = BitConverter.GetBytes(decimal.ToInt32(numericUpDown7.Value));
                eb2[254] = subbamt[0];
                eb2[255] = subbamt[1];

                byte[] subbtrd = BitConverter.GetBytes(decimal.ToInt32(numericUpDown4.Value));
                eb2[220] = subbtrd[0];
                eb2[221] = subbtrd[1];
                eb2[222] = subbtrd[2];
            }

            //Monster
            int MainMonstPointer = BitConverter.ToInt32(ba, 24);
            string MonsID1 = List.Monster.FirstOrDefault(x => x.Value == comboBox11.Text).Key;
            int num_var = Convert.ToInt32(MonsID1);
            eb2[MainMonstPointer + 32] = Convert.ToByte(MonsID1, 16);
            eb2[MainMonstPointer + 64] = Convert.ToByte(MonsID1, 16);

            eb2[72] = BitConverter.GetBytes(decimal.ToInt16(numericUpDown9.Value))[0];      //str
            eb2[68] = BitConverter.GetBytes(decimal.ToInt16(numericUpDown10.Value))[0];     //size
            eb2[70] = BitConverter.GetBytes(decimal.ToInt16(numericUpDown11.Value))[0];     //size range
            if (radioButton1.Checked)
            {
                eb2[337] = 10;
            }
            if (radioButton2.Checked)
            {
                eb2[337] = 0;
            }
            eb2[92] = Convert.ToByte(List.Rank.FirstOrDefault(x => x.Value == comboBox4.Text).Key);     //Is g

            eb2[97] = BitConverter.GetBytes(decimal.ToInt16(numericUpDown12.Value))[0];     //small


            byte[] HRP1 = BitConverter.GetBytes(decimal.ToInt32(numericUpDown13.Value));
            eb2[76] = HRP1[0];
            eb2[77] = HRP1[1];
            eb2[78] = HRP1[2];
            byte[] HRP2 = BitConverter.GetBytes(decimal.ToInt32(numericUpDown14.Value));
            eb2[84] = HRP2[0];
            eb2[85] = HRP2[1];
            eb2[86] = HRP2[2];
            byte[] HRP3 = BitConverter.GetBytes(decimal.ToInt32(numericUpDown15.Value));
            eb2[88] = HRP3[0];
            eb2[89] = HRP3[1];
            eb2[90] = HRP3[2];


            File.WriteAllBytes(path, eb2.ToArray());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(comboBox6.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(comboBox7.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(comboBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(comboBox2.Text);
        }
    }
}
