/*
 * Сделано в SharpDevelop.
 * Пользователь: suvoroda
 * Дата: 09/20/2015
 * Время: 15:00
 */
using System;

namespace LiteProject
{
	/// <summary>
	/// Description of Peoples.
	/// </summary>
	public class Peoples
	{
		public Peoples(string name)
		{
			this.HPMax = 100;
			this.HPNow = 100;
			this.Dead = false;
			this.Name = name;
		}			
		public int HPMax{get;set;}
		public int HPNow{get;set;}
		public bool Dead{get;set;}
		public string Name{get;set;}
		public void Ranenie(int uron)
		{
			this.HPNow -= uron;
			if(CheckDead())
			{
				this.Dead = true;
				this.HPNow = 0;
			}
		}
		public bool CheckDead()
		{
			if(this.HPNow <= 0)
				return true;
			return false;
		}
	}
}
