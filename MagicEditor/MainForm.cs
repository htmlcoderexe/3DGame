using GameObject;
using GameObject.AbilityLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicEditor
{
    public partial class MainForm : Form
    {
        ModularAbility CurrentAbility;
        public List<ModularAbility> abilities = new List<ModularAbility>();
        CharacterTemplate currentclass;
        public List<CharacterTemplate> classes = new List<CharacterTemplate>();
        private bool lockform = false;
        public MainForm()
        {
            InitializeComponent();
        }


        public ModularAbility FindAbility(string id)
        {
            foreach (ModularAbility a in abilities)
                if (a.ID == id)
                    return a;
            return null;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            AbilityEffectDefinition.LoadDefinitions();

            AbilityFileReader fr = new AbilityFileReader();

            abilities = fr.ReadFile();
            foreach (ModularAbility ability in abilities)
            {
                abilityselector.Items.Add(ability);
            }

            CurrentAbility = abilities[0];
            abilityselector.SelectedIndex = 0;
            EditCurrentAbility();

            //classes evt. load from file, now using this to "populate"
            //*
            currentclass = new TestCharacterClass();
            currentclass.ID = "koldun";
            currentclass.Name = "Wizard";
            classes.Add(currentclass);
            classes.Add(new TestCharacterClass());
            //*/

            foreach(CharacterTemplate t in classes)
            {
                classlist.Items.Add(t);
            }

            currentclass = classes[0];
            classlist.SelectedIndex = 0;
            EditCurrentClass();

            panel1.Refresh();
        }


        #region functions used by Abilities tab

        private void EditCurrentAbility()
        {

            ReloadEffectList();
            SetIcon(CurrentAbility.Icon);

            this.spellname.Text = CurrentAbility.Name;
            lockform = true;
            castbase.Value = (decimal)CurrentAbility.BaseValues["cast_time"];
            channelbase.Value = (decimal)CurrentAbility.BaseValues["channel_time"];
            mpbase.Value = (decimal)CurrentAbility.BaseValues["mp_cost"];
            cdbase.Value = (decimal)CurrentAbility.BaseValues["cooldown"];
            castdelta.Value = (decimal)CurrentAbility.GrowthValues["cast_time"];
            channeldelta.Value = (decimal)CurrentAbility.GrowthValues["channel_time"];
            mpdelta.Value = (decimal)CurrentAbility.GrowthValues["mp_cost"];
            cddelta.Value = (decimal)CurrentAbility.GrowthValues["cooldown"];

            UpdateDescriptionPreview();
            lockform = false;
        }

        private void UpdateBases()
        {
            CurrentAbility.BaseValues["cast_time"] = (float)castbase.Value;
            CurrentAbility.BaseValues["channel_time"] = (float)channelbase.Value;
            CurrentAbility.BaseValues["mp_cost"] = (float)mpbase.Value;
            CurrentAbility.BaseValues["cooldown"] = (float)cdbase.Value;
            CurrentAbility.GrowthValues["cast_time"] = (float)castdelta.Value;
            CurrentAbility.GrowthValues["channel_time"] = (float)channeldelta.Value;
            CurrentAbility.GrowthValues["mp_cost"] = (float)mpdelta.Value;
            CurrentAbility.GrowthValues["cooldown"] = (float)cddelta.Value;
        }

        private void SetIcon(int IconId)
        {
            iconimage.Location = new Point((IconId % 64) * -32, ((int)(IconId / 64f)) * -32);
        }

        private void ReloadEffectList()
        {
            EffectList.Items.Clear();
            foreach (ITimedEffect effect in CurrentAbility.GetModules())
            {
                AbilityEffectDefinition adef = AbilityEffectDefinition.GetDefinition(effect.EffectType);
                string[] ItemProps = new string[] { adef.FriendlyName, effect.BaseTime.ToString(), effect.BaseDuration.ToString() };
                ListViewItem line = new ListViewItem(ItemProps, adef.Icon)
                {
                    Tag = effect
                };
                EffectList.Items.Add(line);

            }

        }

        private void UpdateDescriptionPreview()
        {
            CurrentAbility.Level = (int)lvlprev.Value;
            descprev.Text = string.Join("\r\n", CurrentAbility.GetTooltip());
        }

        private string AddWithAutoname(ModularAbility ab, bool autoaccept = false)
        {
            string autoname = "";

            int degree = 0;
            foreach (ModularAbility a in abilities)
            {
                string[] autoparts = a.ID.Split('.');
                if (autoparts.Length == 2)//current name is an autoname - compare to first piece only
                {

                    if (autoparts[0] == ab.ID)//matching auto
                    {
                        int currentdegree = int.Parse(autoparts[1]) + 1;
                        if (currentdegree > degree)
                            degree = currentdegree;
                    }
                }
                else
                {
                    if (a.ID == ab.ID)
                        degree = 1;
                }
            }

            if (degree > 0)
                autoname = ab.ID + "." + degree.ToString();

            //if ab ID already exists, change the ID to autoname and add if autoaccept is true, else do nothing and just return autoname
            if (autoname != "")
            {
                if (autoaccept)
                {

                    ab.ID = autoname;
                    abilities.Add(ab);
                }
            }
            else //no collision, just append as normal
            {

                abilities.Add(ab);
            }
            //the return value is useful in determining if a collision occurred - and with autoaccept= false lets user choose to force the name or not
            return autoname;
        }

        #endregion


        #region functions used by Classes tab

        private void EditCurrentClass()
        {
            ReloadSkillTreeList();
            lockform = true;
            classname.Text = currentclass.Name;
            classdesc.Text = currentclass.Description;
            hplvl.Value = currentclass.HPperLVL;
            mplvl.Value = currentclass.MPperLVL;
            hpvit.Value = currentclass.HPperVIT;
            mpint.Value = currentclass.MPperINT;
            basehp.Value =  (decimal)currentclass.BaseStats["HP"];
            hpregen.Value = (decimal)currentclass.BaseStats["hpregen"];
            mpregen.Value = (decimal)currentclass.BaseStats["mpregen"];
            speed.Value =   (decimal)currentclass.BaseStats["movement_speed"];

            int stat = (int)currentclass.DamageStat;
            dmgstat.SelectedIndex = stat;
            lockform = false;
        }

        private void SaveClassBasics()
        {
            currentclass.HPperLVL = (int)hplvl.Value;
            currentclass.MPperLVL = (int)mplvl.Value;
            currentclass.HPperVIT = (int)hpvit.Value;
            currentclass.MPperINT = (int)mpint.Value;

            currentclass.BaseStats["HP"] = (float)basehp.Value;
            currentclass.BaseStats["hpregen"] = (float)hpregen.Value;
            currentclass.BaseStats["mpregen"] = (float)mpregen.Value;
            currentclass.BaseStats["movement_speed"] = (float)speed.Value;

            CharacterTemplate.MainStats setstat;
            switch(dmgstat.SelectedIndex)
            {
                case 0:
                    {
                        setstat = CharacterTemplate.MainStats.STR;
                        break;
                    }
                case 1:
                    {
                        setstat = CharacterTemplate.MainStats.DEX;
                        break;
                    }
                case 2:
                default:
                    {
                        setstat = CharacterTemplate.MainStats.INT;
                        break;
                    }
            }
            currentclass.DamageStat = setstat;
        }


        private void ReloadSkillTreeList()
        {
            skillentrylist.Items.Clear();
            foreach(SkillTreeEntry e in currentclass.SkillTree.Entries)
            {
                e.Name = FindAbility(e.SkillID).Name;
                skillentrylist.Items.Add(e);
            }
        }

        private void EditSkillEntry(SkillTreeEntry entry)
        {
            SkillTreeEntryEditor editor = new SkillTreeEntryEditor(this, entry);
            if(editor.ShowDialog()==DialogResult.OK)
            {
                if (skillentrylist.SelectedItems.Count != 1)
                    return;
                skillentrylist.Items[skillentrylist.SelectedIndex] = editor.Entry;
            }
        }

        private void AddSkillTree(string id, int level)
        {
            SkillTreeEntry e = new SkillTreeEntry()
            {
                SkillID = id,
                LearnLevel = level,
                Name = FindAbility(id).Name
            };
            skillentrylist.Items.Add(e);
        }

        #endregion




        #region GUI wireups for Abilities tab

        private void EffectList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (EffectList.SelectedItems.Count != 1)
                return;
            ListViewItem item = EffectList.SelectedItems[0];
            EditAbilityComponent editform = new EditAbilityComponent((ITimedEffect)item.Tag);
            if (editform.ShowDialog() == DialogResult.OK)
            {

                ReloadEffectList();
                UpdateDescriptionPreview();
            }
            //MessageBox.Show(((ITimedEffect)item.Tag).EffectType);
        }

        private void lvlprev_ValueChanged(object sender, EventArgs e)
        {
            UpdateDescriptionPreview();

        }


        private void iconimage_DoubleClick(object sender, EventArgs e)
        {
            ChooseIcon chooseform = new ChooseIcon();
            if(chooseform.ShowDialog()==DialogResult.OK)
            {
                SetIcon(chooseform.Icon);
                CurrentAbility.Icon = chooseform.Icon;
            }
        }

        private void descprev_DoubleClick(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.Input = CurrentAbility.DescriptionString;
            if(prompt.ShowDialog()==DialogResult.OK)
            {
                CurrentAbility.DescriptionString = prompt.Input;

                UpdateDescriptionPreview();
            }
        }

        private void spellname_DoubleClick(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.Input = CurrentAbility.Name;
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                CurrentAbility.Name = prompt.Input;
                spellname.Text = CurrentAbility.Name;
                UpdateDescriptionPreview();
                //this refreshes the relevant string on the listbox
                abilityselector.Items[abilityselector.Items.IndexOf(CurrentAbility)] = CurrentAbility;
            }
        }

        //this handles ALL of the basic values changes
        private void cddelta_ValueChanged(object sender, EventArgs e)
        {
            if (lockform)
                return;
            UpdateBases();

            UpdateDescriptionPreview();
        }

        private void effectmenu_Opening(object sender, CancelEventArgs e)
        {
            
                effectmenu.Items[1].Enabled = EffectList.SelectedItems.Count == 1;
        }

        private void removeEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EffectList.SelectedItems.Count != 1)
                return;
            ListViewItem item = EffectList.SelectedItems[0];
            ITimedEffect effect = (ITimedEffect)item.Tag;
            CurrentAbility.Effects.Remove(effect);
            EffectList.SelectedItems.Clear();
            EffectList.Items.Remove(item);
        }

        private void addEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PickEffectSimple box = new PickEffectSimple();
            if(box.ShowDialog()== DialogResult.OK)
            {
                string result = box.Effect;
                
                
                CurrentAbility.Effects.Add(EffectHelper.CreateEmpty(result));
                ReloadEffectList();
            }
        }


        private void abilitymenu_Opening(object sender, CancelEventArgs e)
        {
            abilitymenu.Items[1].Enabled = abilityselector.SelectedItem != null;
        }

        private void createAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            if(prompt.ShowDialog()==DialogResult.OK)
            {
                ModularAbility a = ModularAbility.CreateEmpty(prompt.Input);
                AddWithAutoname(a, true);
                abilityselector.Items.Add(a);
            }
        }

        private void deleteAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ModularAbility removeme = (ModularAbility)abilityselector.SelectedItem;
            abilityselector.Items.Remove(removeme);
            abilities.Remove(removeme);

        }

        private void abilityselector_DoubleClick(object sender, EventArgs e)
        {
            if (abilityselector.SelectedItem == null)
                return;
            CurrentAbility = (ModularAbility)abilityselector.SelectedItem;
            EditCurrentAbility();
        }

        private void saveabilities_Click(object sender, EventArgs e)
        {

            AbilityFileWriter fw = new AbilityFileWriter(abilities);
            fw.WriteFile();
        }

        #endregion


        #region GUI wireups for Classes tab

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SkillTreeEntry[] array = new SkillTreeEntry[skillentrylist.Items.Count];
            skillentrylist.Items.CopyTo(array, 0);
            //foreach (SkillTreeEntry entry in currentclass.SkillTree.Entries)
            foreach (SkillTreeEntry entry in array)
            {
                ModularAbility a = FindAbility(entry.SkillID);
                Microsoft.Xna.Framework.Point corner = entry.GetLocation();
                corner += new Microsoft.Xna.Framework.Point(SkillTree.ICON_WIDTH / 2, SkillTree.ICON_WIDTH / 2);
                if(entry.PreRequisiteSkills!=null)
                foreach(Tuple<string,int> prereq in entry.PreRequisiteSkills)
                {
                    foreach(SkillTreeEntry preentry in array)
                    {
                        if(preentry.SkillID==prereq.Item1)
                        {

                            Microsoft.Xna.Framework.Point corner2 = preentry.GetLocation();
                            corner2 += new Microsoft.Xna.Framework.Point(SkillTree.ICON_WIDTH / 2, SkillTree.ICON_WIDTH / 2);
                            g.DrawLine(new Pen(Color.Red, 7), corner.X,corner.Y, corner2.X,corner2.Y);
                        }
                    }

                }
                
            }
            //foreach (SkillTreeEntry entry in currentclass.SkillTree.Entries)
            foreach (SkillTreeEntry entry in array)
            {


                ModularAbility a = FindAbility(entry.SkillID);
                Microsoft.Xna.Framework.Point corner = entry.GetLocation();
                int IconId = a.Icon;
                Point iconsource = new Point((IconId % 64) * 32, ((int)(IconId / 64f)) * 32);
                Rectangle src = new Rectangle(iconsource, new Size(32, 32));
                if (entry == skillentrylist.SelectedItem)
                    g.DrawRectangle(new Pen(Color.Red, 4), corner.X, corner.Y, 40, 40);
                g.DrawImage(iconimage.Image, corner.X+4, corner.Y+4, src, GraphicsUnit.Pixel);
            }

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            //panel1.Refresh();

            
        }

        private void classlist_DoubleClick(object sender, EventArgs e)
        {
            if (classlist.SelectedItems.Count != 1)
                return;
            currentclass = (CharacterTemplate)classlist.SelectedItem;
            EditCurrentClass();
        }

        private void hplvl_ValueChanged(object sender, EventArgs e)
        {
            if (lockform)
                return;
            SaveClassBasics();
        }

        private void dmgstat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lockform)
                return;
            SaveClassBasics();
        }

        private void classname_DoubleClick(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.Input = currentclass.Name;
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                currentclass.Name = prompt.Input;
                classname.Text = currentclass.Name;
                //this refreshes the relevant string on the listbox
               classlist.Items[classlist.Items.IndexOf(currentclass)] = currentclass;
            }
        }

        private void classdesc_DoubleClick(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.Input = currentclass.Description;
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                currentclass.Description = prompt.Input;
                classdesc.Text = currentclass.Description;
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            SkillTreeEntry[] array = new SkillTreeEntry[skillentrylist.Items.Count];
            skillentrylist.Items.CopyTo(array, 0);
            foreach (SkillTreeEntry icon in array)
            {
                Microsoft.Xna.Framework.Point p = icon.GetLocation();
                Rectangle XX = new Rectangle(p.X, p.Y, 40, 40);
                if(XX.Contains(new Point(e.X,e.Y)))
                {
                    skillentrylist.SelectedIndex = (skillentrylist.Items.IndexOf(icon));
                }

            }
        }

        private void skillentrylist_SelectedIndexChanged(object sender, EventArgs e)
        {

            panel1.Refresh();
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SkillTreeEntry[] array = new SkillTreeEntry[skillentrylist.Items.Count];
            skillentrylist.Items.CopyTo(array, 0);
            foreach (SkillTreeEntry icon in array)
            {
                Microsoft.Xna.Framework.Point p = icon.GetLocation();
                Rectangle XX = new Rectangle(p.X, p.Y, 40, 40);
                if (XX.Contains(new Point(e.X, e.Y)))
                {
                    skillentrylist.SelectedIndex = (skillentrylist.Items.IndexOf(icon));
                }

            }
            if (skillentrylist.SelectedItems.Count != 1)
                return;
            EditSkillEntry((SkillTreeEntry)skillentrylist.SelectedItem);
        }

        private void skillentrylist_DoubleClick(object sender, EventArgs e)
        {
            if (skillentrylist.SelectedItems.Count != 1)
                return;
            EditSkillEntry((SkillTreeEntry)skillentrylist.SelectedItem);
        }


        private void skillentrymenu_Opening(object sender, CancelEventArgs e)
        {
            skillentrymenu.Items[1].Enabled = skillentrylist.SelectedItems.Count == 1;
        }

        private void addAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkillSelector selector = new SkillSelector(this);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                AddSkillTree(selector.SelectedID, selector.SelectedLevel);

            }
        }

        private void removeAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkillTreeEntry removeme = (SkillTreeEntry)skillentrylist.SelectedItem;
            skillentrylist.Items.Remove(removeme);
        }

        #endregion

    }
}
