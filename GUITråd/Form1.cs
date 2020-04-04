using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;




namespace GUITråd
{

    public partial class Form1 : Form
    {


      readonly object locker = new object(); //Object for the Thread Locker ---//The compiler knows if there is a thread that is using the method down below : Because the Thread enters the method throught the locker and its like the threat is locking the door for the toilet and when it is finished the lock is opened again


        public delegate void DisplayDelegate(String tal);
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            {

                Thread thread1 = new Thread((Arbejd)); //The Thread
                thread1.IsBackground = true;// The Thread is Foreground Thread by Default : Foreground Thread = Runs the thread even if you close the Program 
                                            //Here I make it Background Thread so the Thread gets closed when the program is Closed.
                                            //This Thread is part of the method that is making this thread to work woth the Win Forms thread for textbox "Looks like the Textbox Thread is a Background Thread so when you close the Program the thread is also closed
                                            //This "thread1" wich we made was by  default Foreground Thread and it tries to finish its job when the program is closed but the thread of the Winforms Text box is Background Thread wich closes when the Program is closed and if I dont make the "thread1 " Background also ther comes an Exception because the Win Forms THread is closed when the program is closed and cant do its job it kind of like we merge the Win Forms Thread and "thread1" together so they work together I gues
                thread1.Start();//Start Thread

                
                
            }

        }
        

                 
            public void Arbejd()//Method wich the Thread "thread1" is working on
            {


            lock (locker)//Locker // So only 1 Thread at a time can acces it. If there is Thread "1" that it is using it the other Thread "2" that want to acces it will wait so the Thread "1" is done using the method and than Thread 2 will acces the method.
            {
                for (int x = 1; x <= 10; x++)
                {
                    String tal = "" + x;
                    Thread.Sleep(1000);

                    OpdaterView(tal);
                    //VisView(tal);
                }

                void OpdaterView(string tal)
                {
                    DisplayDelegate ddg = new DisplayDelegate(VisView);

                    Invoke(ddg, tal);




                    // opdaterer view vha Invoke pga at Windows form 
                    // komponenter er IKKE Thread-safe, derfor skal man
                    // kalde via UI-tråden vha Invoke, dette foregår
                    // med param (delegate,opt. array med obj.)
                }

                void VisView(String tal)
                {
                    textBox1.Text = tal;
                }
            }
        }

        

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


       


        private void button2_Click(object sender, EventArgs e)
        {

           

            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;

            foreach (ProcessThread thread in currentThreads)
            {
                thread.Dispose();
            }


        }
    }
    }

