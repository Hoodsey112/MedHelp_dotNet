using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace MedHelp_dotNet.Classes
{
    public class HealthStatusClass
    {
        static XmlDocument xmlDoc;

        #region cbHealthStatus
        public static List<string> LoadHealthStatus()
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

            return classList;
        }

        //Добавление статуса в ComboBox
        public static void InsertHealthStatus(string newStatusName)
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
        public static void RemoveHealthStatus(int removedStatusIndex)
        {
            XmlNode selectedNode = xmlDoc.DocumentElement.ChildNodes[removedStatusIndex];

            selectedNode.ParentNode.RemoveChild(selectedNode);

            FileStream WRITER = new FileStream(Application.StartupPath + $@"\HealthStatus.xml", FileMode.Truncate, FileAccess.Write);

            xmlDoc.Save(WRITER);

            WRITER.Close();

            LoadHealthStatus();
        }

        //Проверка статуса на наличие его уже в списке
        private static bool CheckTree(string StatusName)
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
        #endregion
    }
}
