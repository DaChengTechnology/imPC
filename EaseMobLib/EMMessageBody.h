#pragma once
#include<message\emmessagebody.h>
#include "emsbsase.h"


using namespace System;

namespace EaseMobLib {
	public enum class EMMessageBodyType
	{
		TEXT,       //Text message body
		IMAGE,      //Image message body
		VIDEO,      //Video message body
		LOCATION,   //Location message body
		VOICE,      //Voice message body
		FILE,       //File message body
		COMMAND     //Command message body
	};


	public ref class EMMessageBody abstract : EMBase
	{
	public:
		/**
		* \brief Message body constructor.
		*
		* @param  The message body type.
		* @return NA
		*/
		EMMessageBody(EMMessageBodyType type);

		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMMessageBody();

		/**
		* \brief Get message body type.
		*
		* @param  NA
		* @return The message body type.
		*/
		//EMMessageBodyType type();

		property EMMessageBodyType type
		{
			EMMessageBodyType get();
		}
	};
}
