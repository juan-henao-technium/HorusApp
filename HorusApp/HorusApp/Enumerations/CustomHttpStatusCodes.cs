using System;
using System.Collections.Generic;
using System.Text;

namespace HorusApp.Enumerations
{
	public enum CustomHttpStatusCodes
	{
		Unknown,
		DataNotFound,
		IncorrectPassword,
		UserAlreadyRegistered,
		InactiveUser,
		TokenError,
		PendingOrderAlreadyExist,
		DataWithDependencies,
		CustomerAlreadyRegister,
		SeatsNotAvaliable,
		NotAllowed
	}
}
