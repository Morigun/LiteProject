/*
 * Сделано в SharpDevelop.
 * Пользователь: suvoroda
 * Дата: 09/20/2015
 * Время: 16:52
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace LiteProject
{
	/// <summary>
	/// Interaction logic for Log.xaml
	/// </summary>
	public partial class Log : Window
	{
		public Log()
		{
			InitializeComponent();
		}
		public List<string> log;
		public void VivodLog()
		{
			foreach(string s in log)
				LogList.Items.Add(s);
		}
	}
}