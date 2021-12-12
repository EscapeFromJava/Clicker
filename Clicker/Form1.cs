using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace Clicker
{
    public partial class Form1 : Form
    {
        private int timerHealthPointTicks;
        private int timerInGameTicks;
        private int timerInGameSec = 0;
        private int timerInGameMin = 0;

        private int clicks = 0;
        private int clicksFactor = 1;
        private int healthPoint = 100;
        private int maxHealthPoint = 100;

        private int visibleButtonShop = 0;
        private int visibleButtonInfo = 0;

        private int soundPlay = 1;

        private int power = 0;
        private int armor = 0;

        SoundPlayer music = new SoundPlayer(@"C:\Users\PUDGE\source\repos\Clicker\Clicker\sound\music.wav");

        public Form1()
        {
            InitializeComponent();

            music.Play();
            timerInGame.Enabled = true;
            progressBarGoblinHp.Value = goblinHealPoint;
            

            timerHealthPoint.Interval = 1000;
            timerInGame.Interval = 1000;
            healthPoint = maxHealthPoint;
            labelHealthPoint.Text = "HP: " + healthPoint;
            if (healthPoint <= 0)
            {
                DialogResult choice = MessageBox.Show("Time in game: " + timerInGameMin + " min " + timerInGameSec + " sec. Retry?", "You die!", MessageBoxButtons.YesNo);
                if (choice == DialogResult.Yes)
                {
                    Application.Restart();
                }
                if (choice == DialogResult.No)
                {
                    Close();
                }
            }
            labelTIGmin.Text = "" + timerInGameMin;
            labelTIGsec.Text = "" + timerInGameSec;


        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            timerHealthPoint.Enabled = true;
            

            clicks += clicksFactor;
            if (clicks > 10)
            {
                clicksFactor++;
                btnShop.Visible = true;
            }
            labelScore.Text = "Clicks: " + clicks;
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            visibleButtonShop++;
            if (visibleButtonShop == 1)
                groupBoxShop.Visible = true;
            if (visibleButtonShop == 2)
            {
                groupBoxShop.Visible = false;
                visibleButtonShop = 0;
            }
        }



        private void btnHealthPoint_Click(object sender, EventArgs e)
        {
            if (clicks > 100)
            {
                clicks -= 100;
                labelScore.Text = "Clicks: " + clicks;
                if (healthPoint <= maxHealthPoint - 10)
                    healthPoint += 10;
                    
                else
                    healthPoint = maxHealthPoint;
                labelHealthPoint.Text = "HP: " + healthPoint;
            }
        }

        private void btnHealthPointBig_Click(object sender, EventArgs e)
        {
            if (clicks > 500)
            {
                clicks -= 500;
                labelScore.Text = "Clicks: " + clicks;
                if (healthPoint <= maxHealthPoint - 50)
                    healthPoint += 50;

                else
                    healthPoint = maxHealthPoint;
                labelHealthPoint.Text = "HP: " + healthPoint;
            }
        }
        private int priceWeapon = 1000;
        private void btnWeapon_Click(object sender, EventArgs e)
        {
            if (clicks > priceWeapon)
            {
                clicks -= priceWeapon;
                labelScore.Text = "Clicks: " + clicks;
                btnPersonInfo.Visible = true;
                labelPower.Visible = true;
                power++;
                labelPower.Text = "Power: " + power;
                priceWeapon += 1000;
                btnWeapon.Text = "Weapon[" + priceWeapon + "$]";
            }
            if (power > 0)
                groupBoxGoblin.Visible = true;
            
        }

        private void btnPersonInfo_Click(object sender, EventArgs e)
        {
            visibleButtonInfo++;
            if (visibleButtonInfo == 1)
                groupBoxPersonInfo.Visible = true;
            if (visibleButtonInfo == 2)
            {
                groupBoxPersonInfo.Visible = false;
                visibleButtonInfo = 0;
            }
        }

        private int priceArmor = 1000;
        private void btnArmor_Click(object sender, EventArgs e)
        {
            if (clicks > priceArmor)
            {
                clicks -= priceArmor;
                labelScore.Text = "Clicks: " + clicks;
                btnPersonInfo.Visible = true;
                labelProtection.Visible = true;
                armor++;
                labelProtection.Text = "Protection: " + armor;
                priceArmor += 2000;
                btnArmor.Text = "Armor[" + priceArmor + "$]";
            } 
        }
        private int priceBuyMaxHP = 5000;
        private void btnBuyMaxHP_Click(object sender, EventArgs e)
        {
            if (clicks > priceBuyMaxHP)
            {
                btnPersonInfo.Visible = true;
                clicks -= priceBuyMaxHP;
                labelScore.Text = "Clicks: " + clicks;
                maxHealthPoint += 15;
                labelMaxHP.Visible = true;
                labelMaxHP.Text = "Max HP: " + maxHealthPoint;
                priceBuyMaxHP += 5000;
                btnBuyMaxHP.Text = "+10 MAX HP [" + priceBuyMaxHP + "$]";
            }
        }

        int goblinHealPoint = 100;
        private int goblinAttack = 3;
        private int goblinArmor = 0;
        int deathCounterGoblin = 0;
        
        Random attack = new Random();
        private void buttonAttackGoblin_Click(object sender, EventArgs e)
        {
            progressBarGoblinHp.Minimum = 0;
            progressBarGoblinHp.Maximum = 100;
            progressBarGoblinHp.Value = goblinHealPoint;
            progressBarGoblinHp.Step = power;
            if (power > 0)
            {
                goblinHealPoint -= (power - goblinArmor);
                groupBoxGoblin.Text = "Goblin: " + goblinHealPoint + " HP";
                if (goblinHealPoint > 0)
                {
                    progressBarGoblinHp.Value = goblinHealPoint;
                }
                else
                    progressBarGoblinHp.Value = 0;
                healthPoint -= (goblinAttack - armor);
                if (healthPoint > maxHealthPoint)
                {
                    healthPoint = maxHealthPoint;
                }
                labelHealthPoint.Text = "HP: " + healthPoint;
            }
            if (goblinHealPoint <= 0)
            {
                goblinHealPoint = 100;
                progressBarGoblinHp.Value = goblinHealPoint;
                goblinAttack *= 2;
                labelGoblinAttack.Text = "Attack: " + goblinAttack;
                goblinArmor++;
                labelGoblinArmor.Text = "Armor: " + goblinArmor;
                deathCounterGoblin++;
                labelTrollsKilled.Visible = true;
                labelTrollsKilled.Text = "Goblin Killed: " + deathCounterGoblin;
                maxHealthPoint += 5;
                labelMaxHP.Visible = true;
                labelMaxHP.Text = "Max HP: " + maxHealthPoint;
            }
            if (healthPoint <= 0)
            {
                DialogResult choice = MessageBox.Show("Time in game: " + timerInGameMin + " min " + timerInGameSec + " sec. Retry?", "You die!", MessageBoxButtons.YesNo);
                if (choice == DialogResult.Yes)
                {
                    //придумать перезапуск игры
                }
                if (choice == DialogResult.No)
                {
                    Close();
                }
            }
            if (deathCounterGoblin == 5)
            {
                progressBarOrcHp.Maximum = 500;
                progressBarOrcHp.Value = 500;
                groupBoxOrc.Visible = true;
            }
        }
        int orcHealPoint = 500;
        private int orcAttack = 15;
        private int orcArmor = 3;
        int deathCounterOrc = 0;
        
        private void buttonAttackOrc_Click(object sender, EventArgs e)
        {
            progressBarOrcHp.Minimum = 0;
            progressBarOrcHp.Maximum = 500;
            progressBarOrcHp.Value = orcHealPoint;
            progressBarOrcHp.Step = power;
            if (power > 0)
            {
                orcHealPoint -= (power - orcArmor);
                groupBoxOrc.Text = "Orc: " + orcHealPoint + " HP";
                if (orcHealPoint > 0)
                {
                    progressBarOrcHp.Value = orcHealPoint;
                }
                else
                    progressBarOrcHp.Value = 0;
                healthPoint -= (orcAttack - armor);
                if (healthPoint > maxHealthPoint)
                {
                    healthPoint = maxHealthPoint;
                }
                labelHealthPoint.Text = "HP: " + healthPoint;
            }
            if (orcHealPoint <= 0)
            {
                orcHealPoint = 500;
                progressBarOrcHp.Value = orcHealPoint;
                orcAttack *= 2;
                labelOrcAttack.Text = "Attack: " + orcAttack;
                orcArmor += 3;
                labelOrcArmor.Text = "Armor: " + orcArmor;
                deathCounterOrc++;
                labelOrcsKilled.Visible = true;
                labelOrcsKilled.Text = "Orcs Killed: " + deathCounterOrc;
                maxHealthPoint += 15;
                labelMaxHP.Visible = true;
                labelMaxHP.Text = "Max HP: " + maxHealthPoint;
            }
            if (healthPoint <= 0)
                MessageBox.Show("You die!");
            if (deathCounterOrc == 5)
            {
                progressBarDragonHP.Maximum = 2000;
                progressBarDragonHP.Value = 2000;
                groupBoxDragon.Visible = true;
            }
        }

        int dragonHealPoint = 200;
        private int dragonAttack = 50;
        private int dragonArmor = 15;
        int deathCounterDragon = 0;
        private void buttonAttackDragon_Click(object sender, EventArgs e)
        {
            progressBarDragonHP.Minimum = 0;
            progressBarDragonHP.Maximum = 2000;
            progressBarDragonHP.Value = dragonHealPoint;
            progressBarDragonHP.Step = power;
            if (power > 0)
            {
                dragonHealPoint -= (power - dragonArmor);
                groupBoxDragon.Text = "Dragon: " + dragonHealPoint + " HP";
                if (dragonHealPoint > 0)
                {
                    progressBarDragonHP.Value = dragonHealPoint;
                }
                else
                    progressBarDragonHP.Value = 0;
                healthPoint -= (dragonAttack - armor);
                if (healthPoint > maxHealthPoint)
                {
                    healthPoint = maxHealthPoint;
                }
                labelHealthPoint.Text = "HP: " + healthPoint;
            }
            if (dragonHealPoint <= 0)
            {
                dragonHealPoint = 2000;
                progressBarDragonHP.Value = dragonHealPoint;
                dragonAttack *= 2;
                labelDragonAttack.Text = "Attack: " + dragonAttack;
                dragonArmor += 10;
                labelDragonArmor.Text = "Armor: " + dragonArmor;
                deathCounterDragon++;
                labelDragonsKilled.Visible = true;
                labelDragonsKilled.Text = "Dragons Killed: " + deathCounterDragon;
                maxHealthPoint += 250;
                labelMaxHP.Visible = true;
                labelMaxHP.Text = "Max HP: " + maxHealthPoint;
            }
            if (healthPoint <= 0)
                MessageBox.Show("You die!");
        }
        private int godNumber = 99999999;
        private void buttonGodMode_Click(object sender, EventArgs e)
        {
            maxHealthPoint = godNumber;
            healthPoint = maxHealthPoint;
            labelHealthPoint.Text = "HP: " + healthPoint;
            power = godNumber;
            labelPower.Visible = true;
            labelPower.Text = "Power: " + power;
            armor = godNumber;
            labelProtection.Visible = true;
            labelProtection.Text = "Protection: " + armor;
            btnPersonInfo.Visible = true;
            groupBoxDragon.Visible = true;
            groupBoxOrc.Visible = true;
            groupBoxGoblin.Visible = true;
            btnShop.Visible = true;
            clicks = godNumber;
            labelScore.Text = "Clicks: " + clicks;

        }

        private void timerHealthPoint_Tick(object sender, EventArgs e)
        {
            timerHealthPointTicks++;
            Random timerRand = new Random();
            if (timerHealthPointTicks == timerRand.Next(10) || timerHealthPointTicks == 10)
            {
                healthPoint--;
                labelHealthPoint.Text = "HP: " + healthPoint;
                timerHealthPointTicks = 0;
            }
        }
        private void timerInGame_Tick(object sender, EventArgs e)
        {
            timerInGameTicks++;
            timerInGameSec = timerInGameTicks;
            labelTIGsec.Text = "" + timerInGameSec;
            if (timerInGameSec == 59)
            {
                timerInGameMin++;
                labelTIGmin.Text = "" + timerInGameMin;
                timerInGameSec = timerInGameTicks;
                labelTIGsec.Text = "" + timerInGameSec;
                timerInGameTicks = 0;
            }
        }

        private void buttonMusic_Click(object sender, EventArgs e)
        {
            soundPlay++;
            if (soundPlay == 1)
            {
                buttonMusic.BackgroundImage = Properties.Resources.sound_on;
                music.Play();
            }
            if (soundPlay == 2)
            {
                buttonMusic.BackgroundImage = Properties.Resources.sound_off;
                music.Stop();
                soundPlay = 0;
            }
        }
        private int visibleHelp = 0;
        private void btnHelp_Click(object sender, EventArgs e)
        {
            visibleHelp++;
            if (visibleHelp == 1)
                groupBoxHelp.Visible = true;
            if (visibleHelp == 2)
            {
                groupBoxHelp.Visible = false;
                visibleHelp = 0;
            }
        }
    }
}
