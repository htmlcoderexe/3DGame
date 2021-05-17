using GameObject;
using GameObject.AbilityLogic;
using GameObject.IO;
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
        public List<ModularAbility> abilities = new List<ModularAbility>();
        public List<CharacterTemplate> classes = new List<CharacterTemplate>();
        public List<ItemTypeDefinition> itemtypes = new List<ItemTypeDefinition>();

        public Point[] slots = new Point[]
            {
                new Point(314,83),
                new Point(50,83),
                new Point(175,129),
                new Point(175,244),
                new Point(104,105),
                new Point(180,346),
                new Point(89,35),
                new Point(180,10),
                new Point(270,35),
                new Point(180,74),
                new Point(314,126),
                new Point(135,74),
                new Point(230,74),
                new Point(72,190),
                new Point(288,190),

            };
        public Rectangle[] slothitboxes;

        #region Internal state

        ModularAbility CurrentAbility;
        CharacterTemplate CurrentClass;
        ItemTypeDefinition CurrentItemType;
        bool lockform = false;

        #endregion

        #region Initialisation

        public MainForm()
        {
            InitializeComponent();

            slothitboxes = new Rectangle[slots.Length];
            for (int i = 0; i < slots.Length; i++)
            {
                slothitboxes[i] = new Rectangle(slots[i], new Size(40, 40));
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AbilityEffectDefinition.LoadDefinitions();

            MagicFileReader fr = new MagicFileReader();

            //load abilities
            abilities = fr.ReadAbilityFile();
            if (abilities.Count > 0)
            {

                foreach (ModularAbility ability in abilities)
                {
                    abilityselector.Items.Add(ability);
                }

                CurrentAbility = abilities[0];
                abilityselector.SelectedIndex = 0;
            }
            else
            {
                CurrentAbility = null;
            }
            EditCurrentAbility();

            //load classes
            classes = fr.ReadClassFile();
            if (classes.Count > 0)
            {

                foreach (CharacterTemplate t in classes)
                {
                    classlist.Items.Add(t);
                }

                CurrentClass = classes[0];
                classlist.SelectedIndex = 0;
            }
            else
            {
                CurrentAbility = null;
            }
            EditCurrentClass();

            //load item type defs
            itemtypes = fr.ReadItemTypeDefinitionFile();
            if (itemtypes.Count > 0)
            {
                foreach (ItemTypeDefinition def in itemtypes)
                {
                    itemtypelist.Items.Add(def);
                }
                CurrentItemType = itemtypes[0];
                itemtypelist.SelectedIndex = 0;
            }
            else
            {
                CurrentItemType = null;
            }


            //reload panel based editing controls
            panel1.Refresh();
        }

        #endregion

        #region Common functions


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        public ModularAbility FindAbility(string id)
        {
            foreach (ModularAbility a in abilities)
                if (a.ID == id)
                    return a;
            return null;
        }

        private string AddWithAutoname(GameObject.Interfaces.IGameID ab, List<GameObject.Interfaces.IGameID> pool, bool autoaccept = false)
        {
            string autoname = "";

            int degree = 0;
            foreach (GameObject.Interfaces.IGameID a in pool)
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
                }
            }
            else //no collision, just append as normal
            {

            }
            //the return value is useful in determining if a collision occurred - and with autoaccept= false lets user choose to force the name or not
            return autoname;
        }

        private void SaveAll()
        {
            MagicFileWriter fw = new MagicFileWriter();
            fw.WriteAbilityFile(abilities);
            fw.WriteClassFile(classes);
            fw.WriteItemTypeDefinitionFile(itemtypes);
        }

        #endregion


        #region functions used by Abilities tab

        private void EditCurrentAbility()
        {
            if (CurrentAbility == null)
            {
                abilitygroupbasics.Hide();
                abilitygroupid.Hide();
                noabilitywarning.Show();
                EffectList.Enabled = false;
                EffectList.Items.Clear();
                return;
            }
            abilitygroupbasics.Show();
            abilitygroupid.Show();
            noabilitywarning.Hide();
            EffectList.Enabled = true;
            ReloadEffectList();
            SetIcon(CurrentAbility.Icon, iconimage);

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
            rangebase.Value = (decimal)CurrentAbility.BaseValues["range"];
            rangedelta.Value = (decimal)CurrentAbility.GrowthValues["range"];

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
            CurrentAbility.BaseValues["range"] = (float)rangebase.Value;
            CurrentAbility.GrowthValues["range"] = (float)rangebase.Value;
        }

        private void SetIcon(int IconId, PictureBox image)
        {
            image.Location = new Point((IconId % 64) * -32, ((int)(IconId / 64f)) * -32);
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

        #endregion

        #region functions used by Classes tab

        private void EditCurrentClass()
        {
            if (CurrentClass == null)
            {
                classgroupbasics.Hide();
                classgroupid.Hide();
                noclasswarning.Show();
                skillentrylist.Enabled = false;
                skillentrylist.Items.Clear();
                panel1.Enabled = false;
                return;
            }
            classgroupbasics.Show();
            classgroupid.Show();
            noclasswarning.Hide();
            skillentrylist.Enabled = true;
            panel1.Enabled = true;
            ReloadSkillTreeList();
            lockform = true;
            classname.Text = CurrentClass.Name;
            classdesc.Text = CurrentClass.Description;
            hplvl.Value = CurrentClass.HPperLVL;
            mplvl.Value = CurrentClass.MPperLVL;
            hpvit.Value = CurrentClass.HPperVIT;
            mpint.Value = CurrentClass.MPperINT;
            basehp.Value = (decimal)CurrentClass.BaseStats["HP"];
            hpregen.Value = (decimal)CurrentClass.BaseStats["hpregen"];
            mpregen.Value = (decimal)CurrentClass.BaseStats["mpregen"];
            speed.Value = (decimal)CurrentClass.BaseStats["movement_speed"];

            int stat = (int)CurrentClass.DamageStat;
            dmgstat.SelectedIndex = stat;
            lockform = false;
        }

        private void SaveClassBasics()
        {
            CurrentClass.HPperLVL = (int)hplvl.Value;
            CurrentClass.MPperLVL = (int)mplvl.Value;
            CurrentClass.HPperVIT = (int)hpvit.Value;
            CurrentClass.MPperINT = (int)mpint.Value;

            CurrentClass.BaseStats["HP"] = (float)basehp.Value;
            CurrentClass.BaseStats["hpregen"] = (float)hpregen.Value;
            CurrentClass.BaseStats["mpregen"] = (float)mpregen.Value;
            CurrentClass.BaseStats["movement_speed"] = (float)speed.Value;

            CharacterTemplate.MainStats setstat;
            switch (dmgstat.SelectedIndex)
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
            CurrentClass.DamageStat = setstat;
        }

        private void CommitSkillTree()
        {

            SkillTreeEntry[] list = new SkillTreeEntry[skillentrylist.Items.Count];
            skillentrylist.Items.CopyTo(list, 0);
            CurrentClass.SkillTree.Entries = list.ToList();
        }

        private void ReloadSkillTreeList()
        {
            skillentrylist.Items.Clear();
            List<SkillTreeEntry> invalids = new List<SkillTreeEntry>();
            foreach (SkillTreeEntry e in CurrentClass.SkillTree.Entries)
            {
                ModularAbility a = FindAbility(e.SkillID);

                if (a == null)
                {
                    invalids.Add(e);
                    MessageBox.Show("The AbilityID \"" + e.SkillID + "\" was not found in the database.", "Invalid AbilityID", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    continue;
                }
                e.Name = a.Name;
                skillentrylist.Items.Add(e);
            }
            foreach (SkillTreeEntry e in invalids)
                CurrentClass.SkillTree.Entries.Remove(e);
        }

        private void EditSkillEntry(SkillTreeEntry entry)
        {
            SkillTreeEntryEditor editor = new SkillTreeEntryEditor(this, entry);
            if (editor.ShowDialog() == DialogResult.OK)
            {
                if (skillentrylist.SelectedItems.Count != 1)
                    return;
                skillentrylist.Items[skillentrylist.SelectedIndex] = editor.Entry;
                CommitSkillTree();
            }
        }

        private void AddSkillTree(string id, int level)
        {
            ModularAbility a = FindAbility(id);
            if (a == null)
            {
                MessageBox.Show("The AbilityID \"" + id + "\" was not found in the database.", "Invalid AbilityID", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            SkillTreeEntry e = new SkillTreeEntry()
            {
                SkillID = id,
                LearnLevel = level,
                Name = a.Name
            };
            skillentrylist.Items.Add(e);
            CommitSkillTree();
        }

        #endregion

        #region functions used by Equip Types tab

        private void EditCurrentItemType()
        {
            if (CurrentItemType == null)
            {
                itemtypegroupbasics.Hide();
                itemtypegroupmultipliers.Hide();
                noitemdefwarning.Show();
                slotselector.Hide();
                return;
            }
            itemtypegroupbasics.Show();
            itemtypegroupmultipliers.Show();
            noitemdefwarning.Hide();
            slotselector.Show();
            ItemTypeName.Text = CurrentItemType.Name;


            lockform = true;
            equipcatbox.SelectedIndex = (int)CurrentItemType.ItemCategory;

            equiphpmultiplier.Value = (decimal)CurrentItemType.MainStatMultipliers[0];
            equipmpmultiplier.Value = (decimal)CurrentItemType.MainStatMultipliers[1];
            equippatkmultiplier.Value = (decimal)CurrentItemType.MainStatMultipliers[2];
            equipmatkmultiplier.Value = (decimal)CurrentItemType.MainStatMultipliers[3];
            equippdefmultiplier.Value = (decimal)CurrentItemType.MainStatMultipliers[4];
            equipmdefmultiplier.Value = (decimal)CurrentItemType.MainStatMultipliers[5];

            lockform = false;
            slotselector.Refresh();
            //#todo: icon selection
        }

        private void UpdateStatMultipliers()
        {
            CurrentItemType.MainStatMultipliers[0] = (float)equiphpmultiplier.Value;
            CurrentItemType.MainStatMultipliers[1] = (float)equipmpmultiplier.Value;
            CurrentItemType.MainStatMultipliers[2] = (float)equippatkmultiplier.Value;
            CurrentItemType.MainStatMultipliers[3] = (float)equipmatkmultiplier.Value;
            CurrentItemType.MainStatMultipliers[4] = (float)equippdefmultiplier.Value;
            CurrentItemType.MainStatMultipliers[5] = (float)equipmdefmultiplier.Value;
        }

        #endregion

        #region GUI wireups for Abilities tab

        #region Basic ability editing

        private void lvlprev_ValueChanged(object sender, EventArgs e)
        {
            UpdateDescriptionPreview();

        }


        private void iconimage_DoubleClick(object sender, EventArgs e)
        {
            ChooseIcon chooseform = new ChooseIcon(iconimage);
            if (chooseform.ShowDialog() == DialogResult.OK)
            {
                SetIcon(chooseform.Icon, iconimage);
                CurrentAbility.Icon = chooseform.Icon;
            }
        }

        private void descprev_DoubleClick(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.Input = CurrentAbility.DescriptionString;
            if (prompt.ShowDialog() == DialogResult.OK)
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

        #endregion

        #region Effect list + context menu

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
            if (box.ShowDialog() == DialogResult.OK)
            {
                string result = box.Effect;


                CurrentAbility.Effects.Add(EffectHelper.CreateEmpty(result));
                ReloadEffectList();
            }
        }

        #endregion

        #region Ability list + context menu

        private void abilityselector_DoubleClick(object sender, EventArgs e)
        {
            if (abilityselector.SelectedItem == null)
                return;
            CurrentAbility = (ModularAbility)abilityselector.SelectedItem;
            EditCurrentAbility();
        }

        private void abilitymenu_Opening(object sender, CancelEventArgs e)
        {
            abilitymenu.Items[1].Enabled = abilityselector.SelectedItem != null;
        }

        private void createAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                ModularAbility a = ModularAbility.CreateEmpty(prompt.Input);
                List<GameObject.Interfaces.IGameID> abs = abilities.ConvertAll(b => (GameObject.Interfaces.IGameID)b);
                //this will modify the Name if autoname is needed
                AddWithAutoname(a, abs, true);
                abilities.Add(a);
                abilityselector.Items.Add(a);
                CurrentAbility = a;
                abilityselector.SelectedItem = a;
                EditCurrentAbility();
            }
        }

        private void deleteAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ModularAbility removeme = (ModularAbility)abilityselector.SelectedItem;
            abilityselector.Items.Remove(removeme);
            abilities.Remove(removeme);
            if (abilities.Count > 0)
            {

                abilityselector.SelectedIndex = 0;
                CurrentAbility = (ModularAbility)abilityselector.SelectedItem;
            }
            else
                CurrentAbility = null;
            EditCurrentAbility();

        }

        #endregion

        private void saveabilities_Click(object sender, EventArgs e)
        {


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
                Microsoft.Xna.Framework.Point corner = entry.GetLocation();
                corner += new Microsoft.Xna.Framework.Point(SkillTree.ICON_WIDTH / 2, SkillTree.ICON_WIDTH / 2);
                if (entry.PreRequisiteSkills != null)
                    foreach (Tuple<string, int> prereq in entry.PreRequisiteSkills)
                    {
                        foreach (SkillTreeEntry preentry in array)
                        {
                            if (preentry.SkillID == prereq.Item1)
                            {

                                Microsoft.Xna.Framework.Point corner2 = preentry.GetLocation();
                                corner2 += new Microsoft.Xna.Framework.Point(SkillTree.ICON_WIDTH / 2, SkillTree.ICON_WIDTH / 2);
                                g.DrawLine(new Pen(Color.Red, 7), corner.X, corner.Y, corner2.X, corner2.Y);
                            }
                        }

                    }

            }
            //foreach (SkillTreeEntry entry in currentclass.SkillTree.Entries)
            foreach (SkillTreeEntry entry in array)
            {


                ModularAbility a = FindAbility(entry.SkillID);
                if (a == null)
                    continue;
                Microsoft.Xna.Framework.Point corner = entry.GetLocation();
                int IconId = a.Icon;
                Point iconsource = new Point((IconId % 64) * 32, ((int)(IconId / 64f)) * 32);
                Rectangle src = new Rectangle(iconsource, new Size(32, 32));
                if (entry == skillentrylist.SelectedItem)
                    g.DrawRectangle(new Pen(Color.Red, 4), corner.X, corner.Y, 40, 40);
                g.DrawImage(iconimage.Image, corner.X + 4, corner.Y + 4, src, GraphicsUnit.Pixel);
            }

        }


        #region Basic editing


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
            prompt.Input = CurrentClass.Name;
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                CurrentClass.Name = prompt.Input;
                classname.Text = CurrentClass.Name;
                //this refreshes the relevant string on the listbox
                classlist.Items[classlist.Items.IndexOf(CurrentClass)] = CurrentClass;
            }
        }

        private void classdesc_DoubleClick(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.Input = CurrentClass.Description;
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                CurrentClass.Description = prompt.Input;
                classdesc.Text = CurrentClass.Description;
            }
        }

        #endregion

        #region Skill tree editing
        private void panel1_MouseClick(object sender, MouseEventArgs e)
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
            panel1.Refresh();
        }

        private void skillentrylist_DoubleClick(object sender, EventArgs e)
        {
            if (skillentrylist.SelectedItems.Count != 1)
                return;
            EditSkillEntry((SkillTreeEntry)skillentrylist.SelectedItem);
            panel1.Refresh();
        }
        #endregion

        #region Skill list context menu

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
                panel1.Refresh();
            }
        }
        private void removeAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkillTreeEntry removeme = (SkillTreeEntry)skillentrylist.SelectedItem;
            skillentrylist.Items.Remove(removeme);
            panel1.Refresh();
            CommitSkillTree();
        }
        #endregion

        #region Class list+context menu


        private void classlist_DoubleClick(object sender, EventArgs e)
        {
            if (classlist.SelectedItems.Count != 1)
                return;
            CurrentClass = (CharacterTemplate)classlist.SelectedItem;
            EditCurrentClass();
            panel1.Refresh();
        }

        private void classmenu_Opening(object sender, CancelEventArgs e)
        {
            classmenu.Items[1].Enabled = classlist.SelectedItems.Count == 1;
        }

        private void createClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                CharacterTemplate a = CharacterTemplate.CreateEmpty(prompt.Input);
                List<GameObject.Interfaces.IGameID> abs = classes.ConvertAll(b => (GameObject.Interfaces.IGameID)b);
                //this will modify the Name if autoname is needed
                AddWithAutoname(a, abs, true);
                classes.Add(a);
                classlist.Items.Add(a);
                classlist.SelectedItem = a;
                CurrentClass = a;
                EditCurrentClass();
            }
        }

        private void deleteClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CharacterTemplate removeme = (CharacterTemplate)classlist.SelectedItem;
            classes.Remove(removeme);
            classlist.Items.Remove(removeme);
            if (classes.Count > 0)
                classlist.SelectedIndex = 0;
            else
                CurrentClass = null;
            EditCurrentClass();
        }
        #endregion

        #endregion

        #region GUI wireups for Equip Types tab


        private void itemtypelist_DoubleClick(object sender, EventArgs e)
        {
            if (itemtypelist.SelectedItems.Count != 1)
                return;
            CurrentItemType = (ItemTypeDefinition)itemtypelist.SelectedItem;
            EditCurrentItemType();
            slotselector.Refresh();
        }

        private void equiphpmultiplier_ValueChanged(object sender, EventArgs e)
        {

            if (lockform)
                return;
            UpdateStatMultipliers();
        }

        private void ItemTypeName_DoubleClick(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.Input = CurrentItemType.Name;
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                CurrentItemType.Name = prompt.Input;
                ItemTypeName.Text = CurrentItemType.Name;
                //this refreshes the relevant string on the listbox
                itemtypelist.Items[itemtypelist.Items.IndexOf(CurrentItemType)] = CurrentItemType;
            }
        }
        private void equipcatbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lockform)
                return;
            CurrentItemType.ItemCategory = (GameObject.Items.ItemEquip.EquipCategories)equipcatbox.SelectedIndex;
        }

        #region slot selector
        private void slotselector_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(vitruvian.Image, 0, 0);
            Pen highlight = new Pen(Color.Red, 4);
            foreach (Rectangle R in slothitboxes)
                g.FillRectangle(Brushes.Black, R);
            int slot = CurrentItemType.SlotID;

            g.DrawRectangle(highlight, slothitboxes[slot]);

            //draw both right and left hand slots if right hand is selected
            if (slot == 0)
            {
                g.DrawRectangle(highlight, slothitboxes[1]);
            }
            //draw second and third ring slot if first is selected
            if (slot == 6)
            {
                g.DrawRectangle(highlight, slothitboxes[7]);
                g.DrawRectangle(highlight, slothitboxes[8]);
            }

        }

        private void slotselector_MouseClick(object sender, MouseEventArgs e)
        {
            Point hit = new Point(e.X, e.Y);
            int slot = -1;
            for (int i = 0; i < slothitboxes.Length; i++)
            {
                if (slothitboxes[i].Contains(hit))
                {
                    slot = i;
                    break;
                }
            }

            if (slot == -1)
                return;
            //clicking left hand internally uses right hand anyway
            if (slot == 1)
                slot = 0;
            //clicking any of 3 ring slots uses first slot
            if (slot == 7 || slot == 8)
                slot = 6;
            CurrentItemType.SlotID = slot;
            slotselector.Refresh();
        }
        #endregion
        #region context menu


        private void createItemTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                ItemTypeDefinition def = ItemTypeDefinition.CreateEmpty(prompt.Input);
                List<GameObject.Interfaces.IGameID> abs = itemtypes.ConvertAll(b => (GameObject.Interfaces.IGameID)b);
                //this will modify the Name if autoname is needed
                AddWithAutoname(def, abs, true);
                itemtypes.Add(def);
                itemtypelist.Items.Add(def);
                itemtypelist.SelectedItem = def;
                CurrentItemType = def;
                EditCurrentItemType();
            }
        }

        private void deleteItemTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemTypeDefinition removeme = (ItemTypeDefinition)itemtypelist.SelectedItem;
            itemtypes.Remove(removeme);
            itemtypelist.Items.Remove(removeme);
            if (itemtypes.Count > 0)
                itemtypelist.SelectedIndex = 0;
            else
                CurrentItemType = null;
            EditCurrentItemType();
        }

        private void equiptypemenu_Opening(object sender, CancelEventArgs e)
        {
            equiptypemenu.Items[1].Enabled = itemtypelist.SelectedItems.Count == 1;
        }
        #endregion


        #endregion

        private void maintabber_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditCurrentAbility();
            EditCurrentClass();
            EditCurrentItemType();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void maintabber_DoubleClick(object sender, EventArgs e)
        {

        }

        private void addiconbutton_Click(object sender, EventArgs e)
        {
            ChooseIcon chooseform = new ChooseIcon(itemicons);
            if (chooseform.ShowDialog() == DialogResult.OK)
            {
                SelectedIcon holder = new SelectedIcon();
                holder.Size = new Size(2048, 2048);
                Panel pp = new Panel();
                pp.Left = 10; pp.Top = 10;
                pp.Height = 32; pp.Width = 32;
                pp.Controls.Add(holder);
                holder.DoubleClick += new EventHandler(removeicon);
                holder.Image = itemicons.Image;
                //holder.Width = 32;holder.Height = 32;
                SetIcon(chooseform.Icon, holder);
                CurrentItemType.Icons.Add(chooseform.Icon);
                holder.IconValue = chooseform.Icon;
                foreach (Control k in equiptypeicongroup.Controls)
                {
                    //Reposition(k);
                }

                iconlayout.Controls.Add(pp);
            }
        }

        private void Reposition(Control input)
        {
            int maxx = 200;

            Point p = input.Location;

            p.X += 40;
            if (p.X > maxx)
            {
                p.X = 10;
                p.Y += 40;
            }
            input.Location = p;
        }

        private void removeicon(object sender, EventArgs e)
        {
            int icon = (sender as SelectedIcon).IconValue;
            CurrentItemType.Icons.Remove(icon);

            bool flagset = false;
            Control removeme = null;
            foreach (Control k in iconlayout.Controls)
            {
                Panel p = k as Panel;
                if (p != null)
                {
                    Control iconcontrol = p.Controls[0];
                    SelectedIcon si = iconcontrol as SelectedIcon;
                    if (si != null)
                    {
                        //move container one slot back AFTER the removed item

                        if (si.IconValue == icon)
                        {
                            flagset = true;
                            removeme = p;
                        }
                    }
                }
            }

            iconlayout.Controls.Remove(removeme);

        }

        private void ItemTypeName_Click(object sender, EventArgs e)
        {

        }
    }
}
