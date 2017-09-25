/*
 * Сделано в SharpDevelop.
 * Пользователь: suvoroda
 * Дата: 11.09.2015
 * Время: 9:04
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;

namespace LiteProject
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
			TimeStrelkaX = TimeStrelkaY = countTick = 0;
			dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = new TimeSpan(0,0,0,0,50);
			dispatcherTimer.Start();
			dispatcherTimerAuto = new System.Windows.Threading.DispatcherTimer();
			dispatcherTimerAuto.Tick += new EventHandler(dispatcherTimer_TickAuto);
			dispatcherTimerAuto.Interval = new TimeSpan(0,0,0,0,50);
			_battleLog = new List<string>();
		}
		Line myLine;		
		TypePrint _typePrint;
		TypePrintAuto _typePrintAuto;
		System.Windows.Threading.DispatcherTimer dispatcherTimer;
		System.Windows.Threading.DispatcherTimer dispatcherTimerAuto;
		int TimeStrelkaX,TimeStrelkaY, countTick;		
		List<string> _battleLog;
		Peoples p1;
		Peoples p2;
		bool lR = false;		
		enum TypePrint{Chasiki,Pulemet, Fly, Rain, Chelovechek, NULL, PulemetLeft, FlyLeft, AUTO, AutoStart};
		enum TypePrintAuto{Chasiki,Pulemet, Fly, Rain, Chelovechek, NULL, PulemetLeft, FlyLeft, AUTO};		
		public void AutoBattleStart()
		{	
			_typePrint = TypePrint.AutoStart;
			BattleAuto();
		}		
		public void BattleAuto()
		{
			int typeAttack;
			Random rnd = new Random();
			typeAttack = rnd.Next(2);
			if(!p1.Dead && !p2.Dead)
			{
				switch(typeAttack)
				{
					case 0:
						if(lR)
						{
							_typePrintAuto = TypePrintAuto.Pulemet;	
							Attack(p2, 10);
						}
						else
						{
							TimeStrelkaX = 100;
							_typePrintAuto = TypePrintAuto.PulemetLeft;
							Attack(p1, 10);
						}
						break;
					case 1:
						if(lR)
						{
							_typePrintAuto = TypePrintAuto.Fly;	
							Attack(p2, 3);
						}
						else
						{
							TimeStrelkaX = 100;
							_typePrintAuto = TypePrintAuto.FlyLeft;						
							Attack(p1, 3);
						}
						break;						
				}
			}
			else
			{
				if(p1.Dead)
					LogText.Content = p2.Name + " WIN!!!";
				else
					LogText.Content = p1.Name + " WIN!!!";
				dispatcherTimer.Interval = new TimeSpan(0,0,0,0,50);
				dispatcherTimerAuto.Stop();				
				_typePrint = TypePrint.Rain;
			}
		}		
		public void Tatata()
		{
			ChangeCoordinateForPulemet();
			ActivCanvas.Children.Add(PrintLine(TimeStrelkaX, TimeStrelkaX -5, 50, 50, System.Windows.Media.Brushes.Blue));				
		}
		public void Tatata2()
		{
			ChangeCoordinateForPulemetLeft();
			ActivCanvas.Children.Add(PrintLine(TimeStrelkaX, TimeStrelkaX -5, 50, 50, System.Windows.Media.Brushes.Green));
		}		
		public void ChasikiTick()
		{
			ChangeCoordinate();
			ActivCanvas.Children.Add(PrintLine(TimeStrelkaX, 50, TimeStrelkaY, 50, System.Windows.Media.Brushes.Yellow));
		}
		public void Fly()
		{
			ActivCanvas.Children.Clear();
			ChangeCoordinateForPulemet();
			ActivCanvas.Children.Add(PrintLine(TimeStrelkaX+5, TimeStrelkaX-5, 50, 50, System.Windows.Media.Brushes.Blue));				
		}
		public void FlyLeft()
		{
			ActivCanvas.Children.Clear();
			ChangeCoordinateForPulemetLeft();
			ActivCanvas.Children.Add(PrintLine(TimeStrelkaX+5, TimeStrelkaX-5, 50, 50, System.Windows.Media.Brushes.Green));				
		}
		public void Rain()
		{
			ChangeCoordinateForPulemet();
			for(int x = 0; x<10; x++)
			{				
				ActivCanvas.Children.Add(PrintLine(10 + (x * 9), 10 + (x * 9), TimeStrelkaX+10, TimeStrelkaX+5, System.Windows.Media.Brushes.Gray));
			}
		}
		public void Chelovechek()
		{
			ChangeCoordinateForChelovechek();
			PrintPeople(FirstCanvas);
			PrintPeople(SecondCanvas);			
		}		
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{			
			switch(_typePrint)
			{
				case TypePrint.Chasiki:					
					ChasikiTick();
					break;
				case TypePrint.Pulemet:
					Tatata();
					break;
				case TypePrint.PulemetLeft:
					Tatata2();
					break;
				case TypePrint.Fly:
					Fly();
					break;
				case TypePrint.FlyLeft:
					FlyLeft();
					break;
				case TypePrint.Rain:
					Rain();
					break;
				case TypePrint.Chelovechek:
					Chelovechek();
					break;
				case TypePrint.NULL:
					ActivCanvas.Children.Clear();
					break;
				case TypePrint.AUTO:
					if(p1 != null && p2 != null)
					{
						dispatcherTimerAuto.Start();
						dispatcherTimer.Interval = new TimeSpan(0,0,0,1,0);
						AutoBattleStart();
						lR = !lR;
					}
					break;
				case TypePrint.AutoStart:					
					BattleAuto();
					lR = !lR;
					break;
			}
		    CommandManager.InvalidateRequerySuggested();
		}		
		private void dispatcherTimer_TickAuto(object sender, EventArgs e)
		{			
			switch(_typePrintAuto)
			{
				case TypePrintAuto.Chasiki:					
					ChasikiTick();
					break;
				case TypePrintAuto.Pulemet:
					Tatata();
					break;
				case TypePrintAuto.PulemetLeft:
					Tatata2();
					break;
				case TypePrintAuto.Fly:
					Fly();
					break;
				case TypePrintAuto.FlyLeft:
					FlyLeft();
					break;
				case TypePrintAuto.Rain:
					Rain();
					break;
				case TypePrintAuto.Chelovechek:
					Chelovechek();
					break;
				case TypePrintAuto.NULL:
					ActivCanvas.Children.Clear();
					break;
			}
		    CommandManager.InvalidateRequerySuggested();
		}
		private void ChangeCoordinate()
		{
			if (countTick < 20)
		    {
			    if (TimeStrelkaX < 100)
			    {
			    	TimeStrelkaX+=10;
			    	countTick++;
			    }
			    else if(TimeStrelkaY < 100)
			    {
			    	TimeStrelkaY+=10;
			    	countTick++;
			    }
		    }
		    else if (countTick < 40)
		    {
		    	if (TimeStrelkaX > 0)
			    {
			    	TimeStrelkaX-=10;
			    	countTick++;
			    }
			    else if(TimeStrelkaY > 0)
			    {
			    	TimeStrelkaY-=10;
			    	countTick++;
			    }
		    }
		    else
		    {		    	
		    	NullAttack();
		    	countTick = TimeStrelkaY = TimeStrelkaX = 0;
		    	ActivCanvas.Children.Clear();
		    }	
		}		
		private void ChangeCoordinateForPulemet()
		{
			if(TimeStrelkaX < 100)
				TimeStrelkaX += 10;
			else
			{				
				NullAttack();
				countTick = TimeStrelkaY = TimeStrelkaX = 0;
		    	ActivCanvas.Children.Clear();
			}
		}
		private void ChangeCoordinateForPulemetLeft()
		{
			if(TimeStrelkaX > 0)
				TimeStrelkaX -= 10;
			else
			{				
				NullAttack();
				countTick = TimeStrelkaY = TimeStrelkaX = 0;
		    	ActivCanvas.Children.Clear();
			}
		}		
		private void ChangeCoordinateForChelovechek()
		{			
			countTick = TimeStrelkaY = 0;
			TimeStrelkaX = 50;		    
		}		
		private void TickButton_Click(object sender, RoutedEventArgs e)
		{
			_typePrint = TypePrint.Chasiki;
			countTick = TimeStrelkaY = TimeStrelkaX = 0;
			ActivCanvas.Children.Clear();
		}		
		private void Tatatata_Click(object sender, RoutedEventArgs e)
		{
			_typePrint = TypePrint.Pulemet;
			countTick = TimeStrelkaY = TimeStrelkaX = 0;
			ActivCanvas.Children.Clear();	
			Attack(p2, 10);
		}		
		private void Fly_Click(object sender, RoutedEventArgs e)
		{			
			_typePrint = TypePrint.Fly;
			countTick = TimeStrelkaY = TimeStrelkaX = 0;	
			Attack(p2, 3);
		}		
		private void RainButton_Click(object sender, RoutedEventArgs e)
		{
			_typePrint = TypePrint.Rain;
			countTick = TimeStrelkaY = TimeStrelkaX = 0;
			ActivCanvas.Children.Clear();
		}		
		private void ChelovechekButton_Click(object sender, RoutedEventArgs e)
		{
			ActivCanvas.Children.Clear();
			LogText.Content = string.Empty;
			_typePrint = TypePrint.Chelovechek;
			countTick = TimeStrelkaY = 0;
			TimeStrelkaX = 50;
			FirstCanvas.Children.Clear();
			SecondCanvas.Children.Clear();
			p1 = new Peoples("Blue");
			p2 = new Peoples("Green");
			PrintHP();
		}		
		private void PrintPeople(Canvas canv)
		{
			canv.Children.Clear();
			//BODY
			canv.Children.Add(PrintLine(50, 50, 70, 50, System.Windows.Media.Brushes.Pink));
			//HANDS
			canv.Children.Add(PrintLine(50, 30, 50, 70, System.Windows.Media.Brushes.Pink));						
			canv.Children.Add(PrintLine(50, 70, 50, 70, System.Windows.Media.Brushes.Pink));						
			//LAGS
			canv.Children.Add(PrintLine(50, 30, 70, 100, System.Windows.Media.Brushes.Pink));						
			canv.Children.Add(PrintLine(50, 70, 70, 100, System.Windows.Media.Brushes.Pink));
			//HEAD
			canv.Children.Add(PrintShape(30,30,35,20));
			//EYE
			canv.Children.Add(PrintShape(5,5,40,30));
			canv.Children.Add(PrintShape(5,5,55,30));
			//ROT
			canv.Children.Add(PrintLine(40, 60, 43, 43, System.Windows.Media.Brushes.Pink));
			//NOS			
			canv.Children.Add(PrintShape(5,5,47.5F,35));
		}		
		private Line PrintLine(float x1, float x2, float y1, float y2, SolidColorBrush color)
		{
			myLine = new Line();
			myLine.Stroke = color;
			myLine.X1 = x1;
			myLine.X2 = x2;
			myLine.Y1 = y1;			
			myLine.Y2 = y2;
			myLine.HorizontalAlignment = HorizontalAlignment.Left;
			myLine.VerticalAlignment = VerticalAlignment.Center;
			myLine.StrokeThickness = 2;
			return myLine;
		}		
		private Ellipse PrintShape(float width, float height, float top, float left)
		{
			Ellipse _ellips = new Ellipse();
			_ellips.Width = width;
			_ellips.Height = height;
			Thickness marginThickness = new Thickness(top,left,0,0);
			_ellips.Margin = marginThickness;
			_ellips.Stroke = System.Windows.Media.Brushes.Pink;
			_ellips.HorizontalAlignment = HorizontalAlignment.Left;
			_ellips.VerticalAlignment = VerticalAlignment.Center;
			_ellips.StrokeThickness = 2;
			return _ellips;
		}		
		private void Tatatata2_Click(object sender, RoutedEventArgs e)
		{
			_typePrint = TypePrint.PulemetLeft;
			TimeStrelkaX = 100;
			countTick = TimeStrelkaY = 0;
			ActivCanvas.Children.Clear();	
			Attack(p1, 10);
		}		
		private void FlyButton2_Click(object sender, RoutedEventArgs e)
		{
			_typePrint = TypePrint.FlyLeft;
			TimeStrelkaX = 100;
			countTick = TimeStrelkaY = 0;	
			Attack(p1, 3);
		}		
		private void PrintHP()
		{			
			HP1.Content = string.Format("HP {0}/{1}",p1.HPNow, p1.HPMax);
			HP2.Content = string.Format("HP {0}/{1}",p2.HPNow, p2.HPMax);
		}		
		private void Attack(Peoples p, int MaxUron)
		{
			int _uronForLog;
			if(p != null)
			{
				Random rnd = new Random();
				_uronForLog = rnd.Next(MaxUron);
				p.Ranenie(_uronForLog);
				PrintHP();
				_battleLog.Add(string.Format("{0} получил {1} пунктов урона, текущее здоровье {2}", p.Name, _uronForLog, p.HPNow));
			}
		}		
		private void AutoButton_Click(object sender, RoutedEventArgs e)
		{
			_typePrint = TypePrint.AUTO;
		}		
		private void NullAttack()
		{
			if(_typePrint == TypePrint.AutoStart)
				_typePrintAuto = TypePrintAuto.NULL;
			else
				_typePrint = TypePrint.NULL;
		}		
		private void button1_Click(object sender, RoutedEventArgs e)
		{
			Log _log = new Log();
			_log.log = _battleLog;
			_log.Show();
			_log.VivodLog();
		}
		private void Canvas_MouseLeftButtonDown(object sender, RoutedEventArgs e)
		{
			this.DragMove();
		}		
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}