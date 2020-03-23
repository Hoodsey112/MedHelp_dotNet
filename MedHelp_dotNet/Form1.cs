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
        public AddEventForm()
        {
            InitializeComponent();
            LoadHealthStatus();
        }

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

        private void RemoveHealthStatus(int removedStatusIndex)
        {
            XmlNode selectedNode = xmlDoc.DocumentElement.ChildNodes[removedStatusIndex];

            selectedNode.ParentNode.RemoveChild(selectedNode);

            FileStream WRITER = new FileStream(Application.StartupPath + $@"\HealthStatus.xml", FileMode.Truncate, FileAccess.Write);

            xmlDoc.Save(WRITER);

            WRITER.Close();

            LoadHealthStatus();
        }

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
    }
}
