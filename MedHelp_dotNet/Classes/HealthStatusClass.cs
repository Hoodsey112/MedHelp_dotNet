using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using NLog;

namespace MedHelp_dotNet.Classes
{
    public class HealthStatusClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static XmlDocument xmlDoc;

        #region cbHealthStatus
        public static List<string> LoadHealthStatus()
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //Добавление статуса в ComboBox
        public static void InsertHealthStatus(string newStatusName)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Удаление статуса из ComboBox
        public static void RemoveHealthStatus(int removedStatusIndex)
        {
            try
            {
                XmlNode selectedNode = xmlDoc.DocumentElement.ChildNodes[removedStatusIndex];

                selectedNode.ParentNode.RemoveChild(selectedNode);

                FileStream WRITER = new FileStream(Application.StartupPath + $@"\HealthStatus.xml", FileMode.Truncate, FileAccess.Write);

                xmlDoc.Save(WRITER);

                WRITER.Close();

                LoadHealthStatus();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Проверка статуса на наличие его уже в списке
        private static bool CheckTree(string StatusName)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion
    }
}
