using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedHelp_dotNet
{
    /*
     * Форма принимает параметр (StatusForm)
     * Если StatusForm = 1 - Форма открыта для добавления данных ребенка в таблицу
     * Если StatusForm = 2 - Форма открыта для выбора данных ребенка из таблицы
     * Если StatusForm = 3 - Форма открыта для редактирования данных ребенка в таблице
     */

    public partial class ClientForm : Form
    {
        DataTable ClientTable = new DataTable();
        public ClientForm(int StatusForm)
        {
            InitializeComponent();
            ClientTable = Classes.ClientClass.LoadClientData();
            clientDGV.DataSource = ClientTable;

            switch(StatusForm)
            {
                case 1:
                    ApplyBTN.Text = "Добавить";
                    break;
                case 2:
                    ApplyBTN.Text = "Выбрать";
                    break;
                case 3:
                    ApplyBTN.Text = "Сохранить";
                    break;
            }
        }
    }
}
