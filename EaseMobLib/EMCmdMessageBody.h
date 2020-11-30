#pragma once
#include "include/message/emcmdmessagebody.h"

#include "EMMessageBody.h"

using namespace System;

namespace EaseMobLib {
	public ref class EMCmdMessageBody : EMMessageBody
	{
	public:
		static ref class EMCmdParam {
		public:
			String^ first;
			String^ second;
		};
		typedef cli::array<EMCmdParam^> EMCmdParams;

		/**
		* \brief Command message body constructor.
		*
		* @param  Command action
		* @param  Command parameters
		* @return NA
		*/
		EMCmdMessageBody(const String^ action);

		/**
		* \brief Copy constructor.
		*
		* @param  Another command message body.
		* @return NA
		*/
		EMCmdMessageBody(const EMCmdMessageBody^);

		/**
		* \brief Assign operator overload.
		*
		* @param  Another command message body.
		* @return The command message body.
		*/
		EMCmdMessageBody% operator=(const EMCmdMessageBody%);

		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMCmdMessageBody();

		/**
		* \brief Get command action.
		*
		* @param  NA
		* @return The command action.
		*/
		const String^ action();

		/**
		* \brief Set command action.
		*
		* @param  The command action.
		* @return NA
		*/
		void setAction(const String^ action);

		/**
		* \brief Get command parameters.
		*
		* @param  NA
		* @return The command parameters.
		*/
		const EMCmdParams^ params();

		/**
		* \brief Set command parameters.
		*
		* Note: User should not use command parameters any more, and use EMMessage's attribute instead.
		* @param  The command parameters.
		* @return NA
		*/
		void setParams(const EMCmdParams^);

	private:
		easemob::EMCmdMessageBodyPtr& EMCmdMessageBody::getImpl();

	};
}
