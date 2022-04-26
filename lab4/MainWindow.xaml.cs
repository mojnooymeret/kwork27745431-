using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab4
{
   /// <summary>
   /// Логика взаимодействия для MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public struct Process
      {
         public string name;
         public List<int> useResources;
         public List<int> maxResources;

         public Process(string name, List<int> useResources, List<int> maxResources)
         {
            this.name = name;
            this.useResources = useResources;
            this.maxResources = maxResources;
         }
      }

      public static List<int> FreeResources(List<Process> process, List<int> freeResources, int k)
      {
         for (int i = 0; i < freeResources.Count; i++) {
            freeResources[i] += process[k].useResources[i];
         }
         return freeResources;
      }

      public MainWindow()
      {
         InitializeComponent();
      }


      // Функция выполнения алгоритма «Банкир»
      // В функции объявляем элементы матрицы, которые мы ввели в интерфейсе (Пример: а14, а - A, 1 - первая матрица, 4 - R4) 
      private string BankerFunction(int a11 = 0, int a12 = 0, int a13 = 0, int a14 = 0, int b11 = 0, int b12 = 0, int b13 = 0, int b14 = 0, int c11 = 0, int c12 = 0, int c13 = 0, int c14 = 0, int d11 = 0, int d12 = 0, int d13 = 0, int d14 = 0, int a21 = 0, int a22 = 0, int a23 = 0, int a24 = 0, int b21 = 0, int b22 = 0, int b23 = 0, int b24 = 0, int c21 = 0, int c22 = 0, int c23 = 0, int c24 = 0, int d21 = 0, int d22 = 0, int d23 = 0, int d24 = 0)
      {
         // Переменная, в которую по ходу выполнения алгоритма записыватся результат (вместо Console.Write)
         string result = string.Empty;

         List<Process> process = new List<Process>();
         process.Add(new Process("A", new List<int> { a11, a12, a13, a14 }, new List<int> { a21, a22, a23, a24 }));
         process.Add(new Process("B", new List<int> { b11, b12, b13, b14 }, new List<int> { b21, b22, b23, b24 }));
         process.Add(new Process("C", new List<int> { c11, c12, c13, c14 }, new List<int> { c21, c22, c23, c24 }));
         process.Add(new Process("D", new List<int> { d11, d12, d13, d14 }, new List<int> { d21, d22, d23, d24 }));

         List<int> freeResources = new List<int> { 4, 4, 4, 4 };
         List<string> query = new List<string>();

         int c = process.Count;

         bool check = false;
         for (int i = 0; i < process.Count; i++) {
            for (int j = 0; j < process[i].useResources.Count; j++) {
               freeResources[j] -= process[i].useResources[j];
               if (freeResources[j] < 0) {
                  // Переменная result используется для записи в нее ответа, который будет выводится на экран
                  result += "Использовано больше ресурсов, чем может быть. Опасное состояние.\n";
                  check = true;
               }
               if (check) return result;
            }
         }

         // Переменная result используется для записи в нее ответа, который будет выводится на экран
         result += "Свободных ресурсов: ";
         for (int j = 0; j < freeResources.Count; j++) {
            result += freeResources[j] + " ";
         }

         // «\n» - перенос строки
         result += "\n";

         check = false;
         bool equalSum = true;
         for (int i = 0; i < process.Count; i++) {
            for (int j = 0; j < process[i].useResources.Count; j++) {
               equalSum = true;
               if (process[i].useResources[j] + freeResources[j] < process[i].maxResources[j]) {
                  equalSum = false;
                  break;
               }

            }
            if (equalSum) {
               query.Add(process[i].name);
               freeResources = FreeResources(process, freeResources, i);
               process.RemoveAt(i);
               i = -1;
            }
         }

         // Переменная result используется для записи в нее ответа, который будет выводится на экран
         if (query.Count == c) result += Environment.NewLine + "Состояние безопасное\n";
         else result += Environment.NewLine + "Состояние опасное\n";

         if (query.Count == 0) query.Add("Z");

         result += "Последовательность выполнения процессов: ";
         for (int j = 0; j < query.Count; j++) {
            result += query[j];
         }
         // Возвращаем итоговую перменную, в которой содержится ответ
         return result;
      }

      private void Calc(object sender, RoutedEventArgs e)
      {
         // Обработчик try catch для обработки исключительной ситуации, а именно, если данные введены не во все поля или там содержится символ, отличный от цифры
         try {
            // Записываем на экран результат функции BankerFunction. В скобках функции переводим каждую введенную цифру в числовой тип данных.
            conclusion.Content = BankerFunction(int.Parse(AR11.Text), int.Parse(AR21.Text), int.Parse(AR31.Text), int.Parse(AR41.Text), int.Parse(BR11.Text), int.Parse(BR21.Text), int.Parse(BR31.Text), int.Parse(BR41.Text), int.Parse(CR11.Text), int.Parse(CR21.Text), int.Parse(CR31.Text), int.Parse(CR41.Text), int.Parse(DR11.Text), int.Parse(DR21.Text), int.Parse(DR31.Text), int.Parse(DR41.Text), int.Parse(AR12.Text), int.Parse(AR22.Text), int.Parse(AR32.Text), int.Parse(AR42.Text), int.Parse(BR12.Text), int.Parse(BR22.Text), int.Parse(BR32.Text), int.Parse(BR42.Text), int.Parse(CR12.Text), int.Parse(CR22.Text), int.Parse(CR32.Text), int.Parse(CR42.Text), int.Parse(DR12.Text), int.Parse(DR22.Text), int.Parse(DR32.Text), int.Parse(DR42.Text));
         } catch { MessageBox.Show("Заполните все поля матрицы", "Уведомление", MessageBoxButton.OK); }
      }
   }
}
