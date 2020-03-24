using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MedHelp_dotNet
{
    public partial class AddEventForm : Form
    {
        public XmlDocument xmlDoc;
        Classes.AreaClass[] areaClass;
        Classes.MOClass[] moClass;
        bool firstStart = true;
        public AddEventForm()
        {
            InitializeComponent();
            LoadHealthStatus();
            LoadArea();
            firstStart = false;
        }

        #region cbHealthStatus
        private void LoadHealthStatus()
        {
            xmlDoc = new XmlDocument();
            FileStream fs = new FileStream(Application.StartupPath + $@"\HealthStatus.xml", FileMode.Open, FileAccess.Read);
            xmlDoc.Load(fs);
            fs.Close();
            var classList = new List<string>();
            
            XmlNode node = xmlDoc.DocumentElement;

            foreach (XmlNode nNode in node.ChildNodes)
            {
                if (nNode.HasChildNodes)
                {
                    foreach (XmlNode cNode in nNode.ChildNodes)
                        classList.Add(cNode.Attributes[0].Value.ToString());
                }
                else
                {
                    classList.Add(nNode.Attributes[0].Value.ToString());
                }
            }

            cbHealthStatus.DataSource = null;
            cbHealthStatus.DataSource = classList;
            cbHealthStatus.SelectedIndex = -1;
        }

        //Добавление статуса в ComboBox
        private void InsertHealthStatus(string newStatusName)
        {
            if (CheckTree(newStatusName))
            {
                XmlNode selectedNode = xmlDoc.DocumentElement;
                XmlElement newOUTERitem = xmlDoc.CreateElement("subStatus");

                newOUTERitem.SetAttribute("name", newStatusName);

                selectedNode.InsertAfter(newOUTERitem, selectedNode.LastChild);

                FileStream WRITER = new FileStream(Application.StartupPath + $@"\HealthStatus.xml", FileMode.Truncate, FileAccess.Write);

                xmlDoc.Save(WRITER);

                WRITER.Close();

                LoadHealthStatus();
            }
            else MessageBox.Show("Поле с таким наименованием уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //Удаление статуса из ComboBox
        private void RemoveHealthStatus(int removedStatusIndex)
        {
            XmlNode selectedNode = xmlDoc.DocumentElement.ChildNodes[removedStatusIndex];

            selectedNode.ParentNode.RemoveChild(selectedNode);

            FileStream WRITER = new FileStream(Application.StartupPath + $@"\HealthStatus.xml", FileMode.Truncate, FileAccess.Write);

            xmlDoc.Save(WRITER);

            WRITER.Close();

            LoadHealthStatus();
        }

        //Проверка статуса на наличие его уже в списке
        private bool CheckTree(string StatusName)
        {
            XmlNode checkNode = xmlDoc.DocumentElement;
            foreach (XmlNode nNode in checkNode.ChildNodes)
            {
                if (nNode.HasChildNodes)
                {
                    foreach (XmlNode cNode in nNode.ChildNodes)
                        if (cNode.Attributes[0].Value.ToString() == StatusName)
                        {
                            return false;
                        }
                }
                else
                {
                    if (nNode.Attributes[0].Value.ToString() == StatusName)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private void AddStatus_Click(object sender, EventArgs e)
        {
            InsertHealthStatus(cbHealthStatus.Text);
        }

        private void RemoveStatus_Click(object sender, EventArgs e)
        {
            RemoveHealthStatus(cbHealthStatus.SelectedIndex);
        }

        #endregion

        #region Area
        //Загрузка списка районов
        private void LoadArea()
        {
            areaClass = Classes.QueryClass.LoadListArea();
            cbArea.DataSource = areaClass;
            cbArea.DisplayMember = "name";
            cbArea.ValueMember = "id";
            cbArea.SelectedIndex = -1;
        }

        //Добавление нового района в таблицу
        private void InsertArea()
        {
            Classes.QueryClass.InsertArea(cbArea.Text);
        }

        //Удаление выбранного района из таблицы
        private void RemoveAreaList()
        {
            Classes.QueryClass.RemoveArea(int.Parse(cbArea.SelectedValue.ToString()));
        }

        //Обработка кнопки добавить район
        private void AddArea_Click(object sender, EventArgs e)
        {
            InsertArea();
            LoadArea();
        }

        //Обработка кнопки удалить район
        private void RemoveArea_Click(object sender, EventArgs e)
        {
            RemoveAreaList();
            LoadArea();
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstStart)
            {
                if (cbArea.SelectedIndex != -1) LoadMOList();
            }
        }
        #endregion

        #region MO
        //Получение списка МО согласно выбранному району
        private void LoadMOList()
        {
            moClass = Classes.QueryClass.LoadMOList(int.Parse(cbArea.SelectedValue.ToString()));
            cbMO.DataSource = moClass;
            cbMO.ValueMember = "id";
            cbMO.DisplayMember = "name";
            cbMO.SelectedIndex = -1;
        }

        //Добавление новой МО в таблицу
        private void AddMO_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedItem != null)
            {
                Classes.QueryClass.InsertNewMO(cbMO.Text, int.Parse(cbArea.SelectedValue.ToString()));
                LoadMOList();
            }
            else MessageBox.Show("Необходимо выбрать район, для добавления новой медицинской организации", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //Удаление выбранного МО из таблицы
        private void RemoveMO_Click(object sender, EventArgs e)
        {
            Classes.QueryClass.RemoveMO(int.Parse(cbMO.SelectedValue.ToString()), int.Parse(cbArea.SelectedValue.ToString()));
            LoadMOList();
        }
        #endregion

        private void AddOrg_Click(object sender, EventArgs e)
        {
            using (HealthOrgForm hoForm = new HealthOrgForm())
            {
                hoForm.ShowDialog();
            }
        }
    }
}
