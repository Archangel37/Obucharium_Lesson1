﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Lesson1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обычный list с именами всех из ВК-группы, лист для перемешивания
        /// </summary>
        private List<string> ourNames = new List<string> { "Алла Крупнова", "Илья Полищук", "Евгений Герасименко",
                "Николай Чуриков", "Mr Vakhid", "Антон Сермус", "Ramazan Isaev", "Антон Иванов", "Павел Решетников" };

        /// <summary>
        /// Константный перечислитель имён, порядок которого мы не меняем (readonly)
        /// </summary>
        private readonly List<string> constNames = new List<string> { "Алла Крупнова", "Илья Полищук", "Евгений Герасименко",
                "Николай Чуриков", "Mr Vakhid", "Антон Сермус", "Ramazan Isaev", "Антон Иванов", "Павел Решетников" };


        private static Random rnd = new Random();

        /// <summary>
        /// перемешиваем любой лист из любых переменных типа T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        
        /// <summary>
        /// Кнопка для исполнения "обычного" перетасовывания исходного листа имён - с изменением исходного листа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Shuffle(ourNames);
            MessageBox.Show(String.Join(",\n", ourNames), "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Кнопка для перетасовки листа чисел-индексов от 0 до кол-ва элементов, взятия значений 
        /// из неменяемого списка по индексу, список при этом не меняется
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var indexes = Enumerable.Range(0, constNames.Count).ToList();
            Shuffle(indexes);
            //закинем все индексаторы в IEnumerable<string>, Select вместо foreach (под капотом одно и то же вроде)
            var resultNames = indexes.Select(index => constNames[index]);

            if (MessageBox.Show(String.Join(",\n", resultNames), "info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                == DialogResult.OK)
                MessageBox.Show(string.Join(", ", constNames), "names", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
