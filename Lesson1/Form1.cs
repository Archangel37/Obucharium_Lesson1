using System;
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

        //Без обид, всех из ВК-группы, лист для перемешивания
        private List<string> ourNames = new List<string> { "Алла Крупнова", "Илья Полищук", "Евгений Герасименко",
                "Николай Чуриков", "Mr Vakhid", "Антон Сермус", "Ramazan Isaev", "Антон Иванов", "Павел Решетников" };

        //если нам не надо изначальный порядок менять - можем воспользоваться, к примеру enum'ом
        //можно через словарь, можно через любые другие сущности
        //enum просто чтоб показать, что мы не меняем изначальные данные
        enum ConstNames { Alla, Ilya, Eugeny, Nikolay, Vakhid, AntonS, Ramazan, AntonI, Paul };
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
        /// Дополнительная рандомизация - количество перемешиваний листа - рандом от 1 до 12 включительно
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        private static void AddonRandomizer<T>(IList<T> list)
        {
            //если хотим "порандомнее" - навесим количество перемешиваний тоже на рандом, НО как минимум одно перемешивание должно случиться
            //поэтому воспользуемся <do - while>
            //рандом на всякий - другой переменной, не знаю зачем, но - "паучье чутьё" =) 
            Random rnd2 = new Random();

            //хорошее число, 13.. (1 - включённый, 13 - исключённый верхний предел т.е. числа 1..12, хотя 1 не обязательно(можно и 0) 
            //- do-while всё равно 1 раз выполнится)
            int iterations = rnd2.Next(1, 13);
            do
            {
                Shuffle(list);
                iterations--;
            } while (iterations > 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //можно сделать не void AddonRandomizer, а возвращать листом
            //тогда вообще всё в одну строчку можно написать в этой кнопке
            AddonRandomizer(ourNames);
            MessageBox.Show(String.Join(",\n", ourNames), "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var indexes = Enumerable.Range(0, Enum.GetNames(typeof(ConstNames)).Length).ToList();
            AddonRandomizer(indexes);
            //закинем все индексаторы в IEnumerable<string>, Select вместо foreach (под капотом одно и то же вроде)
            var ResultNames = indexes.Select(x => Enum.GetValues(typeof(ConstNames)).GetValue(x).ToString());

            if (MessageBox.Show(String.Join(",\n", ResultNames), "info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                == DialogResult.OK)
                MessageBox.Show(string.Join(", ", Enumerable.Range(0, Enum.GetNames(typeof(ConstNames)).Length)
                    .Select(x=>(ConstNames)x)), "names", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
