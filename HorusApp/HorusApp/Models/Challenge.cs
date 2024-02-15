using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorusApp.Models
{
	public class Challenge : BindableBase
	{
		public string Id { get; set; }

		private string title;

		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		private string description;

		public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
		}

		private int currentPoints;

		public int CurrentPoints
		{
			get { return currentPoints; }
			set { SetProperty(ref currentPoints, value); }
		}

		private int totalPoints;

		public int TotalPoints
		{
			get { return totalPoints; }
			set { SetProperty(ref totalPoints, value); }
		}

		public string ChallengeBalance => $"{currentPoints}/{totalPoints}";
        public double CompletePercentage => Math.Round((double)currentPoints / (double)totalPoints * (double)100, 2);
		public double BarProgress => (double)currentPoints / (double)totalPoints;
    }
}
