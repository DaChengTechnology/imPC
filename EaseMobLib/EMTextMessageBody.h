#pragma once
#include "include/message/emtextmessagebody.h"
using namespace System;
#include "EMMessageBody.h"

namespace EaseMobLib {
	public ref class EMTextMessageBody : EMMessageBody
	{
	public:
		/**
		* \brief Text message body constructor.
		*
		* @param  The text.
		* @return NA
		*/
		EMTextMessageBody(const String^);

		/**
		* \brief Copy constructor.
		*
		* @param  Another text message body.
		* @return NA
		*/
		EMTextMessageBody(const EMTextMessageBody%);

		/**
		* \brief Assign operator overload.
		*
		* @param  Another text message body.
		* @return The text message body.
		*/
		EMTextMessageBody% operator=(const EMTextMessageBody%);

		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMTextMessageBody();

		/**
		* \brief Get the text.
		*
		* @param  NA
		* @return The text.
		*/
		const String^ text();
	private:
		easemob::EMTextMessageBodyPtr& EMTextMessageBody::getImpl();
	};
}

