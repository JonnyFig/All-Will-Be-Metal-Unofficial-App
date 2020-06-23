using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Media;

namespace All_Will_Be_Metal
{
    public enum SPEED
    {
        SHORT, MEDIUM, LONG
    }
    public enum CONTROL
    {
        ULTRON, PLAYER, NONE
    }
    public enum DIFFICULTY
    {
        TUTORIAL = 2,
        NORMAL = 3,
        HARD = 4,
        INSANE = 5
    }
    public enum TABS
    {
        PANEL_MAP = 0,
        PANEL_ATTACK = 1,
        PANEL_DEFEND = 2,
        PANEL_MAINMENU = 3,
        PANEL_SUFFER = 4,
        PANEL_ULTRONACTION = 5,
        PANEL_PHASEDIRECTION = 6,
        PANEL_DOOMSDAYDEVICE = 7
    }
    public partial class Form1 : Form
    {
        //2 = neutral, 0 = crisis team, 1 = ultron
        PictureBox[] devices;
        PictureBox[] defenceDice;
        PictureBox[] attackDice;
        PictureBox[] doomsdayDice;
        Ultron ultron;
        private SoundPlayer Player = null;
        int critCount = 0;
        int difficulty = 0;
        int crisisTeamScore = 0;
        int citizensSaved = 0;
        int citizensKilled = 0;
        int round = 1;
        bool ultronTurn = false;

        //confirmationOfDoomsdayQuestions
        bool isComplete_OnePathToPeace = false;
        bool isComplete_ScrapingSound = false;
        bool isComplete_BetterAge = false;

        //initialize AI question order
        string[] moveOrderQuestions = new string[] { "Is there a character holding a citizen within Range 5 of Ultron?",
                                                "Is there a Citizen within range 5 of Ultron?",
                                                "Is there a Doomsday Console not controlled by Ultron farther than range 5?", };
        string[] moveOrderDirections = new string[] { "Ultron moves # towards that character",
                                                "Ultron moves # towards that citizen",
                                                "Ultron moves # towards that console.",
                                                "Ultron moves # towards a console he does not control within Range 5." };
        //string[] assaultOrderQuestions = new string[] { "Is there only one Crisis Team Character within range 2 of Ultron?",
        //                                       "Are there more than one Crisis Team Characters within Range # of Ultron?",
        //                                        "Is there a Crisis Team Character within Range 4 of Ultron?", };
        //string[] assaultOrderFunctions = new string[] { "Ultron Attacks you with his Mettalic Talons.",
        //                                        "Ultron makes a Rage of Ultron attack.",
        //                                        "Ultron makes an Energy Blast attack" };
        string[] assaultOrderQuestions = new string[] { "Is there more than one character within range # of Ultron?",
                                               "Is there any character within range 2 of Ultron?",
                                                "Is there any character within range 4 of Ultron?", };
        string[] assaultOrderFunctions = new string[] { "Ultron Attacks you with his Mettalic Talons.",
                                                "Ultron makes a Rage of Ultron attack.",
                                                "Ultron makes an Energy Blast attack" };


        string movePickupObjective = "Is Ultron within Range 1 to hack with the console?";

        string movePickupCitizen = "Is Ultron within Range 1 to destroy a citizen?";

        string moveTransferenceQuestion = "Does Ultron need Matter Transference to get closer to the target?";

        string moveUseTranference = "Ultron uses Matter Transference to get closer to the target";

        public Form1()
        {
            
            InitializeComponent();
            MainPanel.SelectedIndex = 3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize existing picturebox dice into array
            defenceDice = new PictureBox[] { DefenceDice1, DefenceDice2, DefenceDice3, DefenceDice4, DefenceDice5, DefenceDice6, DefenceDice7, DefenceDice8, DefenceDice9 };
            
            doomsdayDice = new PictureBox[] { DoomsdayDice1, DoomsdayDice2, DoomsdayDice3, DoomsdayDice4, DoomsdayDice5, DoomsdayDice6, DoomsdayDice7, DoomsdayDice8 };
            attackDice = new PictureBox[] {AttackDice1, AttackDice2, AttackDice3, AttackDice4, AttackDice5, AttackDice6, AttackDice7, AttackDice8, AttackDice9,
            AttackDice10, AttackDice11, AttackDice12, AttackDice13, AttackDice14, AttackDice15};
            devices = new PictureBox[] { Device1, Device2, Device3, Device4 };
            MapPicturebox.Controls.Add(Device1);
            MapPicturebox.Controls.Add(Device2);
            MapPicturebox.Controls.Add(Device3);
            MapPicturebox.Controls.Add(Device4);
            MapPicturebox.Controls.Add(HomeBase);
            BackTurnTitle.Controls.Add(ClickObjectiveLabel);
            ClickObjectiveLabel.Location = new Point(30, 5);
            ClickObjectiveLabel.BackColor = Color.Transparent;
            Device1.Location = new Point(50, 50);
            Device2.Location = new Point(250, 50);
            Device3.Location = new Point(50, 200);
            Device4.Location = new Point(250, 200);
            HomeBase.Location = new Point(150, 125);
            Device1.BackColor = Color.Transparent;
            Device2.BackColor = Color.Transparent;
            Device3.BackColor = Color.Transparent;
            Device4.BackColor = Color.Transparent;
            HomeBase.BackColor = Color.Transparent;

            ultron = new Ultron();
            this.KeyPreview = true;
            this.DoubleBuffered = true;
            
            timer1.Start();
        }

        private void HealthbarPicbox_Paint(object sender, PaintEventArgs e)
        {
            // Create small healthbar line pen
            Pen thinPen = new Pen(Color.Red, 3);
            // Create long healthbar line pen (corrupted firmware)
            Pen thickPen = new Pen(Color.Red, 6);
            //draw Ultron health bar
            System.Drawing.SolidBrush greenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            e.Graphics.FillRectangle(greenBrush, new Rectangle(40, 10, ((ultron.firmware*8)+ultron.health)*15, 25));

            //draw health bar's small and long lines
            int bigLine = 1;
            for (int i = 55; i < 640; i += 15) { 
                if (bigLine == 8)
                {
                    e.Graphics.DrawLine(thickPen, new Point(i, 0), new Point(i, 45));
                    bigLine = 0;
                }
                else
                {
                    e.Graphics.DrawLine(thinPen, new Point(i, 10), new Point(i, 35));
                }
                bigLine++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
            UltronPowerLabel.Text = ultron.power.ToString();
            HealthLabel.Text = ultron.health.ToString();
            CrisisTeamScoreLabel.Text = crisisTeamScore.ToString();
            UltronScoreLabel.Text = ultron.score.ToString();
        }

        public void updatePower(int power)
        {
            UltronPowerLabel.Text = power.ToString();
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            if (TutorialRadio.Checked == true)
            {
                difficulty = (int)DIFFICULTY.TUTORIAL;
            }else if (NormalRadio.Checked == true)
            {
                difficulty = (int)DIFFICULTY.NORMAL;
            }
            else if (HardRadio.Checked == true)
            {
                difficulty = (int)DIFFICULTY.HARD;
            }
            else if (InsaneRadio.Checked == true)
            {
                difficulty = (int)DIFFICULTY.INSANE;
            }
            System.IO.Stream sound = Properties.Resources.Splash_screen;
            PlayWav(sound, false);
            MainPanel.SelectedIndex = (int)TABS.PANEL_PHASEDIRECTION;

        }



        private void IntroLetsGoButton_Click(object sender, EventArgs e)
        {
            MainPanel.SelectedIndex = (int)TABS.PANEL_MAP;
            System.IO.Stream sound = Properties.Resources.WhatIsThisPlace;
            PlayWav(sound, false);
        }

        private void deviceSelected(object sender, EventArgs e)
        {

            ControlConfirmationBox confirmationBox;
            if (ultronTurn)
            {
                if (ultron.power < 1)
                {
                    PopUpMessage direction = new PopUpMessage("Not enough power for Ultron to interact", false);
                    direction.ShowDialog();
                    return;

                }
                else
                {
                    confirmationBox = new ControlConfirmationBox(3);
                    confirmationBox.ShowDialog();
                    if (confirmationBox.DialogResult == DialogResult.Yes)
                    {
                        ultron.ReducePower(1);
                        updateDevice((PictureBox)sender);
                    }
                }
                
            }
            else
            {
                confirmationBox = new ControlConfirmationBox(0);
                confirmationBox.ShowDialog();
                if (confirmationBox.DialogResult == DialogResult.Yes)
                {
                    updateDevice((PictureBox)sender);
                }
            }
        }

        private void updateDevice(PictureBox device)
        {
            if (ultronTurn)
            {
                device.Image = All_Will_Be_Metal.Properties.Resources.ExtremisConsoleUltron;
                device.Tag = "Ultron";
            }
            else
            {
                device.Image = All_Will_Be_Metal.Properties.Resources.ExtremisConsoleCrisisTeam;
                device.Tag = "Crisis Team";
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            if (devices[0].Tag == "Ultron" && devices[1].Tag == "Ultron" && devices[2].Tag == "Ultron" && devices[3].Tag == "Ultron")
            {
                //GAME OVER Ultron Wins
                GameOver(false);

            }
            //play capture console sound
            System.IO.Stream sound = Properties.Resources.DeviceActivate;
            PlayWav(sound, false);

        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            MainPanel.SelectedIndex = (int)TABS.PANEL_ATTACK;
        }

        private void UltronDefenceButton_Click(object sender, EventArgs e)
        {
            critCount = 0;
            if (PhysicalRadio.Checked == false && EnergyRadio.Checked == false && MysticalRadio.Checked == false)
            {
                AttackTypeWarning.Visible = true;
            }
            else
            {
                AttackTypeWarning.Visible = false;

                //play roll sound effect

                
                //roll each die for defense
                int diceNum = 4;
                //Roll extra die if Physical or Mystical attack
                if (PhysicalRadio.Checked == true || MysticalRadio.Checked == true)
                {
                    diceNum = 5;
                }

                //one less die if Ultron has incinerate condition
                if (ultron.condition.incinerate == true)
                {
                    diceNum--;
                }

                for (int i = 0; i < diceNum; i++)
                {
                    showDie(defenceDice[i], false);
                    this.Refresh();
                    System.IO.Stream sound = Properties.Resources.Select;
                    PlayWav(sound, false);
                    System.Threading.Thread.Sleep(400);
                }

                //roll critical extra die if Ultron does not have Hex condition
                if (ultron.condition.hex == false)
                {
                    if (critCount > 0)
                    {
                            for (int j = 0; j < critCount; j++)
                            {
                                showDie(defenceDice[j + diceNum], true);
                                this.Refresh();
                                System.IO.Stream sound = Properties.Resources.Select;
                                PlayWav(sound, false);
                                System.Threading.Thread.Sleep(400);
                            }
                    }
                }
                //reset critical dice count
                critCount = 0;


                //remove Defence Roll button
                UltronDefenceButton.Visible = false;

                //remove damage type radio buttons and title
                PhysicalRadio.Visible = false;
                EnergyRadio.Visible = false;
                MysticalRadio.Visible = false;
                SelectAttackTypeLabel.Visible = false;

                //show attack damage panel
                AttackResultsPanel.Visible = true;



            }
            
        }
        private void showDie(PictureBox pic, bool isCritical)
        {
            Random rnd = new Random();
            int roll = rnd.Next(1, 9);

            if (isCritical)
            {
                switch (roll)
                {
                    //critical
                    case 1:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.CriticalDie_Crit;
                        pic.Tag = "critical";
                        break;
                    //wild
                    case 2:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.WildDie_Crit;
                        pic.Tag = "wild";
                        break;
                    //hit
                    case 3:
                    case 4:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.HitDie_Crit;
                        pic.Tag = "hit";
                        break;
                    //block
                    case 5:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.BlockDie_Crit;
                        pic.Tag = "block";
                        break;
                    //blank
                    case 6:
                    case 7:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.BlankDie_Crit;
                        pic.Tag = "blank";
                        break;
                    //failure
                    case 8:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.FailureDie_Crit;
                        pic.Tag = "failure";
                        break;
                }
            }
            else
            {
                switch (roll)
                {
                    //critical
                    case 1:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.CriticalDie;
                        pic.Tag = "critical";
                        critCount++;
                        break;
                    //wild
                    case 2:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.WildDie;
                        pic.Tag = "wild";
                        break;
                    //hit
                    case 3:
                    case 4:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.HitDie;
                        pic.Tag = "hit";
                        break;
                    //block
                    case 5:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.BlockDie;
                        pic.Tag = "block";
                        break;
                    //blank
                    case 6:
                    case 7:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.BlankDie;
                        pic.Tag = "blank";
                        break;
                    //failure
                    case 8:
                        pic.Image = All_Will_Be_Metal.Properties.Resources.FailureDie;
                        pic.Tag = "failure";
                        break;
                }
            }

            if (isCritical)
            {
                //
            }
            pic.Visible = true;
        }

        private void SubmitAttackButton_Click(object sender, EventArgs e)
        {
            //do attack calculation
            int attack = (int)AttackDamageNumeric.Value;
            int defence = getDefenceResults();
            int damage = attack - defence;

            if (SlowCheckbox.Checked) { ultron.condition.slow = true; UltronSlowLabel.Visible = true; }
            if (HexCheckbox.Checked) { ultron.condition.hex = true; UltronHexLabel.Visible = true; }
            if (IncinerateCheckbox.Checked) { ultron.condition.incinerate = true; UltronIncinerateLabel.Visible = true; }
            if (RootCheckbox.Checked) { ultron.condition.root = true; UltronRootLabel.Visible = true; ultron.follyCost = 3; ultron.enoughCost = ultron.firmwareGained + 3; ultron.transferenceCost = 3; }
            if (JudgementCheckbox.Checked) { ultron.condition.judgement = true; UltronJudgeLabel.Visible = true; }
            if (ShockCheckbox.Checked) { ultron.condition.shock = true; UltronShockLabel.Visible = true; }

            SlowCheckbox.Checked = false;
            HexCheckbox.Checked = false;
            IncinerateCheckbox.Checked = false;
            RootCheckbox.Checked = false;
            JudgementCheckbox.Checked = false;
            ShockCheckbox.Checked = false;

            //prevent negative damage
            if (damage < 0) { damage = 0; }

            if (damage == 0)
            {
                System.IO.Stream sound = Properties.Resources.YourUnbearablyNiave;
                PlayWav(sound, false);
            } else if (damage > 0 && damage <= 3)
            {
                System.IO.Stream sound = Properties.Resources.You_reBotheringMe;
                PlayWav(sound, false);
                
            } else if (damage > 3)
            {
                System.IO.Stream sound = Properties.Resources.YouveWoundedMe;
                PlayWav(sound, false);

            }

            //reset attack panel
            AttackResultsPanel.Visible = false;
            UltronDefenceButton.Visible = true;
            PhysicalRadio.Visible = true;
            EnergyRadio.Visible = true;
            MysticalRadio.Visible = true;
            PhysicalRadio.Checked = false;
            EnergyRadio.Checked = false;
            MysticalRadio.Checked = false;
            SelectAttackTypeLabel.Visible = true;
            AttackDamageNumeric.Value = 0;
            AttackTotalLabel.Text = "0";
            critCount = 0;

            //reset dice
            ResetDice(defenceDice);


            //send damage to Ultron
            if (damage > 0) { ultron.DamageUltron(damage); }
            if (ultron.firmware < 0 && ultron.health < 1)
            {
                GameOver(false);
                return;
            }
            CheckIfCorrupted();
            
            
            //check if Ultron can use ENOUGH to push attacking character back small
            if (ultron.power > 2 && ultron.health < 8)
            {
                //play screaming man sound
                System.IO.Stream sound = Properties.Resources.LeaveMeAlone;
                PlayWav(sound, false);

                PopUpMessage enough = new PopUpMessage("Ultron uses \"Enough!\" The attacking character is thrown small away from Ultron.", false);
                enough.ShowDialog();
                ultron.ReducePower(2);
            }

            //show map panel again
            MainPanel.SelectedIndex = (int)TABS.PANEL_MAP;

        }

        private int getDefenceResults()
        {
            int x = 0;
            foreach(PictureBox p in defenceDice)
            {
                if (p.Visible == true)
                {
                    if ((string)p.Tag == "critical" || (string)p.Tag == "block" || (string)p.Tag == "wild")
                    {
                        x++;
                    }
                }
            }
            return x;
        }

        private int getAttackResults()
        {
            int x = 0;
            foreach (PictureBox p in attackDice)
            {
                if (p.Visible == true)
                {
                    if ((string)p.Tag == "critical" || (string)p.Tag == "hit" || (string)p.Tag == "wild")
                    {
                        x++;
                    }
                    if ((string)p.Tag == "blank" && ultron.FollyOfMan == true)
                    {
                        x++;
                    }
                }
            }
            return x;
        }

        private void AttackDamageNumeric_ValueChanged(object sender, EventArgs e)
        {
            AttackTotalLabel.Text = AttackDamageNumeric.Value.ToString();
        }

        // Dispose of the current player and
        // play the indicated WAV file.
        private void PlayWav(Stream stream, bool play_looping)
        {
            // Stop the player if it is running.
            if (Player != null)
            {
                Player.Stop();
                Player.Dispose();
                Player = null;
            }

            // If we have no stream, we're done.
            if (stream == null) return;

            // Make the new player for the WAV stream.
            Player = new SoundPlayer(stream);

            // Play.
            if (play_looping)
                Player.PlayLooping();
            else
                Player.Play();
        }

        private void ResetDice(PictureBox[] p)
        {
            foreach (PictureBox die in p)
            {
                die.Visible = false;
                die.Tag = "";
            }
        }

        private void rollExtraCrit()
        {

        }

        private void SufferButton_Click(object sender, EventArgs e)
        {
            MainPanel.SelectedIndex = (int)TABS.PANEL_SUFFER;
        }

        private void DamageSufferNumeric_ValueChanged(object sender, EventArgs e)
        {
            DamageSufferLabel.Text = DamageSufferNumeric.Value.ToString();
        }

        private void PowerSufferNumeric_ValueChanged(object sender, EventArgs e)
        {
            PowerSufferLabel.Text = PowerSufferNumeric.Value.ToString();
        }

        private void ApplySufferButton_Click(object sender, EventArgs e)
        {
            ultron.DamageUltron((int)DamageSufferNumeric.Value);
            ultron.ReducePower((int)PowerSufferNumeric.Value);
            if (ultron.firmware < 0 && ultron.health < 1)
            {
                GameOver(false);
                return;
            }
            MainPanel.SelectedIndex = (int)TABS.PANEL_MAP;
            CheckIfCorrupted();

        }

        private void CancelSufferButton_Click(object sender, EventArgs e)
        {
            MainPanel.SelectedIndex = (int)TABS.PANEL_MAP;
        }

        private void CheckIfCorrupted()
        {
            if (ultron.firmware == 0 && ultron.health < 1)
            {
                GameOver(true);
                return;
            }
            HealthLabel.Text = ultron.health.ToString();
            if (ultron.injured == true)
            {
                System.IO.Stream sound = Properties.Resources.Ultron_Hurt;
                PlayWav(sound, false);
                for (int i = 0; i < 6; i++)
                {
                    UltronIcon.Image = Properties.Resources.UltronIcon_Damaged;
                    this.Refresh();
                    System.Threading.Thread.Sleep(150);
                    UltronIcon.Image = Properties.Resources.UltronIcon;
                    this.Refresh();
                    System.Threading.Thread.Sleep(150);
                }
                ResetUltronMessage r = new ResetUltronMessage();
                r.ShowDialog();
                ultron.injured = false;
            }


        }

        private void CalculateDoomsdayRoll()
        {
            bool hasHadPowerReserves = false;
            foreach (PictureBox p in doomsdayDice)
            {
                if (p.Visible == true)
                {
                    if ((string)p.Tag == "blank" || (string)p.Tag == "failure")
                    {
                        if (hasHadPowerReserves == false)
                        {
                            hasHadPowerReserves = true;
                            ultron.GainPower(3);
                            PowerReservesPanel.Visible = true;
                            if (UltronControlsAnyConsole() == false)
                            {
                                UltronClaimsConsoleLabel.Visible = true;
                                UltronClaimConsole();
                            }
                            else
                            {
                                UltronClaimsConsoleLabel.Visible = false;
                            }
                        }
                    }
                    if ((string)p.Tag == "block")
                    {
                        OnePathToPeacePanel.Visible = true;
                    }
                    if ((string)p.Tag == "hit")
                    {
                        ScrapingSoundPanel.Visible = true;
                    }
                    if ((string)p.Tag == "wild")
                    {
                        ABetterAgePanel.Visible = true;
                    }
                    if ((string)p.Tag == "critical")
                    {
                        DevastatingBarragePanel.Visible = true;
                    }
                }
            }
        }

        private bool UltronControlsAnyConsole()
        {
            for (int i = 0; i < 4; i++)
            {
                if (devices[i].Tag == "Ultron")
                {
                    return true;
                }
            }
            return false;
        }

        private void ActivateDoomsdayDeviceButton_Click(object sender, EventArgs e)
        {
            //roll doomsday device dice
            //calculate dice by adding difficulty dice + device controlled number
            int x = difficulty;
            for (int i = 0; i < 4; i++)
            {
                if (devices[i].Tag == "Ultron")
                {
                    x++;
                }
            }
            for (int i = 0; i < x; i++)
            {
                //show dice
                showDie(doomsdayDice[i], false);
                this.Refresh();
                //play dice hit sound
                System.IO.Stream sound2 = Properties.Resources.Select;
                PlayWav(sound2, false);
                System.Threading.Thread.Sleep(400);

            }
            CalculateDoomsdayRoll();
            System.IO.Stream sound = Properties.Resources.DoomsdayDevice;
            PlayWav(sound, false);
            ActivateDoomsdayDeviceButton.Visible = false;
            tryShowDoneButton();
        }

        private void UltronClaimConsole()
        {
            //find console already taken by Crisis Team, starting at 4 and ending at 1
            for (int i = 3; i > -1; i--)
            {
                if (devices[i].Tag == "Crisis Team")
                {
                    updateDevice(devices[i]);
                    break;
                }
                

            }
            //if none are already taken by crisis team, take either 3 of 4 (farthest devices, by a random choice)
            if (UltronControlsAnyConsole() == false)
            {
                Random rnd = new Random();
                int r = rnd.Next(2, 4);
                updateDevice(devices[r]);
            }

        }

        private void CitizenCounterApplyButton_Click(object sender, EventArgs e)
        {
            CitizenCounterNumeric.Visible = false;
            ultron.score += (int)CitizenCounterNumeric.Value;
            if (ultron.score >= 12)
            {
                GameOver(false);
                return;
            }
            isComplete_OnePathToPeace = true;
            CitizenCounterApplyButton.Visible = false;
            tryShowDoneButton();
            if (CitizenCounterNumeric.Value > 0)
            {
                System.IO.Stream sound = Properties.Resources.WhenTheySeeTheyUnderstand;
                PlayWav(sound, false);

            }
        }

        private void YesObjectivesHeldButton_Click(object sender, EventArgs e)
        {
            IsHoldingObjectivesLabel.Visible = false;
            DropAllObjectivesLabel.Visible = true;
            YesObjectivesHeldButton.Visible = false;
            NoObjectivesHeldButton.Visible = false;
            isComplete_ScrapingSound = true;
            tryShowDoneButton();
        }

        private void NoObjectivesHeldButton_Click(object sender, EventArgs e)
        {
            if (DevastatingBarragePanel.Visible == false)
            {
                DevastatingBarragePanel.Visible = true;
            }
            ScrapingSoundPanel.Visible = false;
            isComplete_ScrapingSound = true;
            tryShowDoneButton();
        }

        private void ContinueUltronTurnButton_Click(object sender, EventArgs e)
        {
            //reset doomsday device screen
            ApplyPowerFromTerrainButton.Visible = false;
            SizeOfTerrainNumeric.Visible = true;
            SizeOfTerrainNumeric.Value = 1;
            CitizenCounterLabel.Text = "0";
            SizeOfTerrainLabel.Text = "1";

            ResetDice(doomsdayDice);

            IsHoldingObjectivesLabel.Visible = true;
            DropAllObjectivesLabel.Visible = true;
            YesObjectivesHeldButton.Visible = true;
            NoObjectivesHeldButton.Visible = true;
            ActivateDoomsdayDeviceButton.Visible = true;
            ContinueUltronTurnButton.Visible = false;
            DropAllObjectivesLabel.Visible = false;
            ApplyPowerFromTerrainButton.Visible = true;

            CitizenCounterNumeric.Visible = true;
            CitizenCounterNumeric.Value = 0;
            CitizenCounterApplyButton.Visible = true;

            DevastatingBarragePanel.Visible = false;
            ABetterAgePanel.Visible = false;
            OnePathToPeacePanel.Visible = false;
            PowerReservesPanel.Visible = false;
            ScrapingSoundPanel.Visible = false;


            isComplete_BetterAge = false; ;
            isComplete_OnePathToPeace = false;
            isComplete_ScrapingSound = false;

            //go back to map screen
            MainPanel.SelectedIndex = (int)TABS.PANEL_MAP;

        }

        private void ApplyPowerFromTerrainButton_Click(object sender, EventArgs e)
        {
            ultron.GainPower((int)SizeOfTerrainNumeric.Value);
            ApplyPowerFromTerrainButton.Visible = false;
            SizeOfTerrainNumeric.Visible = false;
            isComplete_BetterAge = true;
            tryShowDoneButton();
            System.IO.Stream sound = Properties.Resources.HitUltron;
        }

        private void SizeOfTerrainNumeric_ValueChanged(object sender, EventArgs e)
        {
            SizeOfTerrainLabel.Text = SizeOfTerrainNumeric.Value.ToString();
        }
        private void tryShowDoneButton()
        {
            if ((isComplete_OnePathToPeace == true || OnePathToPeacePanel.Visible == false) 
                && (isComplete_BetterAge == true || ABetterAgePanel.Visible == false) 
                && (isComplete_ScrapingSound == true || ScrapingSoundPanel.Visible == false))
            {
                ContinueUltronTurnButton.Visible = true;
            }
        }

        private void MovementAI()
        {
            //////////////////// MOVEMENT AI ////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////
            //Start AI commands
            bool foundAnswer = false;
            int x = 0;
            while (foundAnswer == false)
            {
                PopUpMessage question = new PopUpMessage(moveOrderQuestions[x], true);
                question.ShowDialog();
                if (question.DialogResult == DialogResult.Yes)
                {
                    PopUpMessage direction = new PopUpMessage(moveOrderDirections[x].Replace("#", ultron.getUltronSpeed()), false);
                    direction.ShowDialog();
                    break;
                }
                if (x == 2) // happens when every movement question is answered with NO
                {
                    PopUpMessage direction = new PopUpMessage(moveOrderDirections[3].Replace("#", ultron.getUltronSpeed()), false);
                    direction.ShowDialog();
                    foundAnswer = true;
                    break;
                }
                x++;
            }
            ///check if matter transference is possible or needed
            if (ultron.power > ultron.transferenceCost && ultron.usedMatterTransfer == false)
            {
                PopUpMessage question = new PopUpMessage(moveTransferenceQuestion, true);
                question.ShowDialog();
                if (question.DialogResult == DialogResult.Yes)
                {
                    PopUpMessage direction = new PopUpMessage(moveUseTranference, false);
                    direction.ShowDialog();
                    ultron.ReducePower(ultron.transferenceCost);
                    ultron.usedMatterTransfer = true;
                }
            }

            /////////////////////////////////////////////////////////////////////////////////////
        }

        private void AttackAI()
        {
            //check for rage of ultron first if power > 6, then regular attack if enemy within range 2, then beam attack if enemy in range 4
            if (ultron.power >= 7)
            {
                String range = "";
                if (ultron.firmware < 2)
                {
                    range = "2";
                }
                else
                {
                    range = "1";
                }
                PopUpMessage question = new PopUpMessage(assaultOrderQuestions[0].Replace("#", range), true);
                question.ShowDialog();
                if (question.DialogResult == DialogResult.Yes)
                {
                    //do ultron area attack
                    ultron.currAttack = (int)ATTACKTYPES.RAGE;
                    ultron.ReducePower(6);
                    AttackNameLabel.Text = "Rage of Ultron";
                    AreaAttackLabel.Visible = true;
                    AreaAttackLabel.Text = "Everyone within range # is the target".Replace("#", range);
                    checkIfFolly();
                    SelectTargetLabel.Visible = false;
                    PriorityPanel.Visible = false;
                    DefenceLabel.Visible = false;
                    DefenceTitleLabel.Visible = false;
                    defenceNumeric.Visible = false;
                    PushSmallLabel.Visible = true;
                    MainPanel.SelectedIndex = (int)TABS.PANEL_DEFEND;
                    return;
                }
            }
            //check for short range attack
            PopUpMessage question2 = new PopUpMessage(assaultOrderQuestions[1], true);
            question2.ShowDialog();
            if (question2.DialogResult == DialogResult.Yes)
            {
                //do ultron short range attack
                ultron.currAttack = (int)ATTACKTYPES.TALLONS;
                AttackNameLabel.Text = "Metallic Talons";
                checkIfFolly();
                MainPanel.SelectedIndex = (int)TABS.PANEL_DEFEND;
                return;
            }
            else if (question2.DialogResult == DialogResult.No)
            {
                if (ultron.power > 2)
                {
                    PopUpMessage question = new PopUpMessage(assaultOrderQuestions[2], true);
                    question.ShowDialog();
                    if (question.DialogResult == DialogResult.Yes)
                    {
                        //do ultron long range attack
                        ultron.currAttack = (int)ATTACKTYPES.BLAST;
                        AttackNameLabel.Text = "Energy Blast";
                        ultron.ReducePower(2);
                        checkIfFolly();
                        DefenceLabel.Visible = false;
                        DefenceTitleLabel.Visible = false;
                        defenceNumeric.Visible = false;
                        MainPanel.SelectedIndex = (int)TABS.PANEL_DEFEND;
                        return;
                    }
                    else
                    {
                        //do move action instead
                        MovementAI();
                    }
                }
                else
                {
                    //do another move action instead
                    MovementAI();
                }
            }
            //end of Ultron's turn
            ultron.usedMatterTransfer = false;
            ultron.currAttack = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            AttackAI();
            FinishRound.Visible = true;
            //switch back to Crisis Team turn
            HomeBase.Visible = true;
            UltronAttackButton.Visible = false;

            ultron.usedMatterTransfer = false;

            //consider going to power phase here
            

        }

        private void UltronMoveButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int i = r.Next(100);
            int chance = 0;

            if (difficulty == (int)DIFFICULTY.TUTORIAL) { chance = 80; }
            if (difficulty == (int)DIFFICULTY.NORMAL) { chance = 85; }
            if (difficulty == (int)DIFFICULTY.HARD) { chance = 90; }
            if (difficulty == (int)DIFFICULTY.INSANE) { chance = 95; }

            //80% chance that Ultron will remove condition if he has one
            if (i < chance)
            {
                if (ultron.condition.hasCondition())
                {
                    string conditionRemoveMessage = ultron.condition.RemoveCondition();
                    if (conditionRemoveMessage.Contains("slow")) { UltronSlowLabel.Visible = false; }
                    if (conditionRemoveMessage.Contains("hex")) { UltronHexLabel.Visible = false; }
                    if (conditionRemoveMessage.Contains("incinerate")) { UltronIncinerateLabel.Visible = false; }
                    if (conditionRemoveMessage.Contains("judgement")) { UltronJudgeLabel.Visible = false; }
                    if (conditionRemoveMessage.Contains("root")) { UltronRootLabel.Visible = false; ultron.follyCost = 2; ultron.enoughCost = ultron.firmwareGained + 2; ultron.transferenceCost = 2; }
                    if (conditionRemoveMessage.Contains("shock")) { UltronShockLabel.Visible = false; }
                    PopUpMessage direction = new PopUpMessage(conditionRemoveMessage, false);
                    direction.ShowDialog();
                }
                else
                {
                    MovementAI();
                }
            }
            else
            {
                MovementAI();
            }
            //switch to Ultron Attack Mode

            
            UltronMoveButton.Visible = false;
            FinishRoundButton.Visible = false;
            UltronAttackButton.Visible = true;
            ClickObjectiveLabel.ForeColor = Color.Red;

        }

        private void FinishRoundButton_Click(object sender, EventArgs e)
        {
            //switch to Ultron Move Mode
            HomeBase.Visible = false;
            AttackButton.Visible = false;
            SufferButton.Visible = false;
            ultronTurn = true;
            UltronDestroyedCitizenButton.Visible = true;
            MainPanel.SelectedIndex = (int)TABS.PANEL_DOOMSDAYDEVICE;
            UltronMoveButton.Visible = true;
            FinishRoundButton.Visible = false;
            UltronAttackButton.Visible = false;
            AttackButton.Visible = false;
            SufferButton.Visible = false;
            ClickObjectiveLabel.ForeColor = Color.Red;
            ClickObjectiveLabel.Text = "    Ultron Turn";
            RoundInstructionsLabel.Text = "Click Ultron Action to receieve his action";
            RoundInstructionsLabel.ForeColor = Color.Red;


        }

        private void FinishRound_Click(object sender, EventArgs e)
        {
            FinishRound.Visible = false;
            AttackButton.Visible = true;
            SufferButton.Visible = true;
            ultronTurn = false;
            UltronDestroyedCitizenButton.Visible = false;
            UltronMoveButton.Visible = false;
            FinishRoundButton.Visible = true;
            UltronAttackButton.Visible = false;
            ClickObjectiveLabel.ForeColor = Color.Blue;
            ClickObjectiveLabel.Text = "Crisis Team Turn";
            RoundInstructionsLabel.Text = "Activate 3 Characters then Finish Round";
            RoundInstructionsLabel.ForeColor = Color.Blue;
            //go to cleanup power phase screen if round 3
            round++;
            if (round == 4)
            {
                MainPanel.SelectedIndex = (int)TABS.PANEL_ULTRONACTION;
                for (int i = 0; i < 4; i++)
                {
                    if (devices[i].Tag == "Crisis Team")
                    {
                        crisisTeamScore++;
                        if (crisisTeamScore >= 12)
                        {
                            GameOver(true);
                            return;
                        }
                    }
                    if (devices[i].Tag == "Ultron")
                    {
                        ultron.score++;
                        if (ultron.score >= 12)
                        {
                            GameOver(false);
                            return;
                        }
                    }
                }
                round = 1;
                ultron.GainPower(1);
            }


        }


        private void MouseOver_Handler ( object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.White;
            ((Button)sender).ForeColor = Color.Black;
        }

        private void MouseLeave_Handler(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Black;
            ((Button)sender).ForeColor = Color.White;
        }

        private void pMouseOver_Handler(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.White;
        }

        private void pMouseLeave_Handler(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }

        private void MouseOver_Handler(object sender, MouseEventArgs e)
        {

        }

        private void UltronDestroyedCitizenButton_Click(object sender, EventArgs e)
        {
            if (ultron.power > 0)
            {
                ControlConfirmationBox confirmationBox = new ControlConfirmationBox(2);
                confirmationBox.ShowDialog();
                if (confirmationBox.DialogResult == DialogResult.Yes)
                {
                    ultron.ReducePower(1);
                    ultron.score++;
                    citizensKilled++;

                    if (ultron.score >= 12)
                    {
                        GameOver(false);
                        return;
                    }
                    //play screaming man sound
                    System.IO.Stream sound = Properties.Resources.CitizenKilled;
                    PlayWav(sound, false);

                }
            }
            else
            {
                PopUpMessage direction = new PopUpMessage("Not enough power for Ultron to interact", false);
                direction.ShowDialog();
            }

        }

        private void TargetSelectedButton_Click(object sender, EventArgs e)
        {
            //check how many dice needed for attack
            critCount = 0;
            int diceNum = 0;
            if (ultron.currAttack == (int)ATTACKTYPES.TALLONS)
            {
                diceNum = 6;
            }
            else if (ultron.currAttack == (int)ATTACKTYPES.RAGE)
            {
                diceNum = 9;
            }
            else if (ultron.currAttack == (int)ATTACKTYPES.BLAST)
            {
                if (ultron.firmware == 4) { diceNum = 7; }
                else if (ultron.firmware == 3){ diceNum = 6; }
                else if (ultron.firmware == 2) { diceNum = 5; }
                else if (ultron.firmware == 1) { diceNum = 4; }
                else if (ultron.firmware == 0) { diceNum = 3; }
            }
            if (ultron.condition.shock)
            {
                diceNum--;
            }
            //Roll Dice
            for (int i = 0; i < diceNum; i++)
            {
                showDie(attackDice[i], false);
                this.Refresh();
                System.IO.Stream sound = Properties.Resources.Select;
                PlayWav(sound, false);
                System.Threading.Thread.Sleep(400);
            }
            //Roll Critical Extra Dice
            if (ultron.condition.hex == false)
            {
                if (critCount > 0)
                {
                    for (int j = 0; j < critCount; j++)
                    {
                        showDie(attackDice[j + diceNum], true);
                        this.Refresh();
                        System.IO.Stream sound = Properties.Resources.Select;
                        PlayWav(sound, false);
                        System.Threading.Thread.Sleep(400);
                    }
                }
            }
            //reset variables
            diceNum = 0;
            critCount = 0;
            DefencePanel.Visible = true;
            TargetSelectedButton.Visible = false;
            PriorityPanel.Visible = false;
            SelectTargetLabel.Visible = false;

            //play attack sound
            //check if wilds are rolled to add Bleed or Pierce
            if (ultron.currAttack == (int)ATTACKTYPES.TALLONS)
            {
                System.IO.Stream sound = Properties.Resources.Tallons;
                PlayWav(sound, false);
                if (WildsRolled(attackDice) == 1)
                {
                    
                }else if (WildsRolled(attackDice) > 1)
                {

                }
            }
            else if (ultron.currAttack == (int)ATTACKTYPES.BLAST)
            {
                System.IO.Stream sound = Properties.Resources.Blast;
                PlayWav(sound, false);
            }
            else if (ultron.currAttack == (int)ATTACKTYPES.RAGE)
            {
                System.IO.Stream sound = Properties.Resources.Rage;
                PlayWav(sound, false);
                AttackAgainButton.Visible = true;
            }

        }

        private void defenceNumeric_ValueChanged(object sender, EventArgs e)
        {
            DefenceLabel.Text = defenceNumeric.Value.ToString();
        }

        private void ButtonClick_Handler(object sender, EventArgs e)
        {

        }

        private void CitizenCounterNumeric_ValueChanged(object sender, EventArgs e)
        {
            CitizenCounterLabel.Text = CitizenCounterNumeric.Value.ToString();
        }

        private void SubmitDefenceButton_Click(object sender, EventArgs e)
        {
            int defence = (int)defenceNumeric.Value;
            int attack = getAttackResults();
            int damage = attack - defence;
            if (damage < 0){damage = 0;}

            //go back to main map
            MainPanel.SelectedIndex = (int)TABS.PANEL_MAP;

            //give power if tallons attack
            if (ultron.currAttack == (int)ATTACKTYPES.TALLONS)
            {
                ultron.GainPower(damage);
            }


            //reset Ultron Attack screen
            DefencePanel.Visible = false;
            PriorityPanel.Visible = true;
            ResetDice(attackDice);
            TargetSelectedButton.Visible = true;
            defenceNumeric.Value = 0;
            DefenceLabel.Text = "0";
            AreaAttackLabel.Visible = false;
            ultron.FollyOfMan = false;
            FollyLabel.Visible = false;
            SelectTargetLabel.Visible = true;
            AttackAgainButton.Visible = false;
            DefenceLabel.Visible = true;
            DefenceTitleLabel.Visible = true;
            defenceNumeric.Visible = true;
            PushSmallLabel.Visible = false;
            PierceLabel.Visible = false;
            BleedLabel.Visible = false;

            //play correct attack sound



            //display damage to target character
            if (ultron.currAttack == (int)ATTACKTYPES.TALLONS)
            {
                PopUpMessage direction = new PopUpMessage("Was the target KO'd?", true);
                direction.ShowDialog();
                if (direction.DialogResult == DialogResult.Yes)
                {
                    //Crisis Team Player Killed
                    System.IO.Stream sound = Properties.Resources.ThisIsHowYouEnd;
                    PlayWav(sound, false);
                    ultron.score++;
                    if (ultron.score >= 12)
                    {
                        GameOver(false);
                        return;
                    }
                }
            }
            else
            {
                PopUpMessage direction = new PopUpMessage("Was the target KO'd?", true);
                direction.ShowDialog();
                if (direction.DialogResult == DialogResult.Yes)
                {
                    //Crisis Team Player Killed
                    System.IO.Stream sound = Properties.Resources.ThisIsHowYouEnd;
                    PlayWav(sound, false);
                    ultron.score++;
                    if (ultron.score >= 12)
                    {
                        GameOver(false);
                        return;
                    }
                }
            }

        }

        private void HomeBase_Click(object sender, EventArgs e)
        {
            ControlConfirmationBox confirmationBox = new ControlConfirmationBox(1);
            confirmationBox.ShowDialog();

            if (confirmationBox.DialogResult == DialogResult.Yes)
            {
                citizensSaved++;
                crisisTeamScore++;
                if (crisisTeamScore >= 12)
                {
                    GameOver(true);
                    return;
                }
                System.IO.Stream sound = Properties.Resources.YourUnbearablyNiave;
                PlayWav(sound, false);
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            MainPanel.SelectedIndex = (int)TABS.PANEL_MAP;
        }

        private void Reroll(object sender, EventArgs e)
        {
            if (((PictureBox)sender).Tag != "failure")
            {
                showDie((PictureBox)sender, false);
                this.Refresh();
                System.IO.Stream sound = Properties.Resources.Select;
                PlayWav(sound, false);
            }
        }

        private void GameOverButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GameOver(bool win)
        {
            if (win)
            {
                MainPanel.SelectedIndex = (int)TABS.PANEL_PHASEDIRECTION;
                GameOverButton.Visible = true;
                GameOverPicturebox.Visible = true;
                GameOverPicturebox.Image = Properties.Resources.The_Last_One;
                GameOverWinner.Visible = true;
                GameOverWinner.Text = "Crisis Team Wins";
                System.IO.Stream sound = Properties.Resources.HowIsHumanitySaved_Win;
                PlayWav(sound, false);
            }
            else
            {
                MainPanel.SelectedIndex = (int)TABS.PANEL_PHASEDIRECTION;
                GameOverButton.Visible = true;
                GameOverPicturebox.Visible = true;
                GameOverPicturebox.Image = Properties.Resources.GameOverPicture;
                GameOverWinner.Visible = true;
                GameOverWinner.Text = "Ultron Wins";
                System.IO.Stream sound = Properties.Resources.YouRiseOnlyToFall_Lose ;
                PlayWav(sound, false);
            }
        }

        private void ButtonClick_Handler(object sender, MouseEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
        }

        private void checkIfFolly()
        {
            if (ultron.power > ultron.follyCost)
            {
                ultron.ReducePower(ultron.follyCost);
                FollyLabel.Visible = true;
                ultron.FollyOfMan = true;
            }
        }

        private void AttackAgainButton_Click(object sender, EventArgs e)
        {
            //Check if last character was KO'd

            PopUpMessage direction = new PopUpMessage("Was the last target KO'd?", true);
            direction.ShowDialog();
            if (direction.DialogResult == DialogResult.Yes)
            {
                //Crisis Team Player Killed
                System.IO.Stream sound2 = Properties.Resources.ThisIsHowYouEnd;
                PlayWav(sound2, false);
                ultron.score++;
                if (ultron.score >= 12)
                {
                    GameOver(false);
                    return;
                }
            }


            //reset previous dice rolled from last attack
            ResetDice(attackDice);

            //check how many dice needed for attack
            critCount = 0;
            //assumed that attack is Rage, setting dice to roll to 9
            int diceNum = 9;
            if (ultron.condition.shock)
            {
                diceNum--;
            }
            //Roll Dice
            for (int i = 0; i < diceNum; i++)
            {
                showDie(attackDice[i], false);
                this.Refresh();
                System.IO.Stream sound2 = Properties.Resources.Select;
                PlayWav(sound2, false);
                System.Threading.Thread.Sleep(400);
            }
            //Roll Critical Extra Dice
            if (ultron.condition.hex == false)
            {
                if (critCount > 0)
                {
                    for (int j = 0; j < critCount; j++)
                    {
                        showDie(attackDice[j + diceNum], true);
                        this.Refresh();
                        System.IO.Stream sound3 = Properties.Resources.Select;
                        PlayWav(sound3, false);
                        System.Threading.Thread.Sleep(400);
                    }
                }
            }
            //reset variables
            diceNum = 0;
            critCount = 0;
            DefencePanel.Visible = true;
            TargetSelectedButton.Visible = false;
            PriorityPanel.Visible = false;
            SelectTargetLabel.Visible = false;

            //play attack sound

            System.IO.Stream sound = Properties.Resources.Rage;
            PlayWav(sound, false);

        }

        private int WildsRolled(PictureBox[] dice)
        {
            int wildCount = 0;
            foreach (PictureBox p in dice)
            {
                if (p.Visible == true)
                {
                    if (p.Tag == "wild")
                    {
                        wildCount++;
                    }
                }
            }

            return wildCount;
        }
    }
}
