using HorusApp.Models.Responses;
using HorusApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HorusApp.Contracts.Repositories
{
	public interface IChallengesRepository
	{
		Task<ResponseBase<List<Challenge>>> GetUserChallenges();
	}
}
