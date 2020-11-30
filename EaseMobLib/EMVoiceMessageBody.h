#pragma once
#include "include/message/emvoicemessagebody.h"
#include "EMFileMessageBody.h"

namespace EaseMobLib {
	public ref class EMVoiceMessageBody : EMFileMessageBody
	{
	public:
		/**
		* \brief Voice message body constructor.
		*
		* @param  NA
		* @return NA
		*/
		EMVoiceMessageBody();

		/**
		* \brief Voice message body constructor.
		*
		* @param  Voice attachment local path.
		* @param  Voice playing duration.
		* @return NA
		*/
		EMVoiceMessageBody(const String^ localPath, int duration);

		/**
		* \brief Copy constructor.
		*
		* Note: Only copy the file path.
		* @param  Another voice message body.
		* @return NA
		*/
		EMVoiceMessageBody(const EMVoiceMessageBody%);

		/**
		* \brief Assign operator overload.
		*
		* Note: Only copy the file path.
		* @param  Another voice message body.
		* @return The voice message body.
		*/
		EMVoiceMessageBody% operator=(const EMVoiceMessageBody%);

		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMVoiceMessageBody();

		/**
		* \brief Get voice playing duration.
		*
		* @param  NA
		* @return The voice playing duration.
		*/
		int duration();

		/**
		* \brief Set voice playing duration.
		*
		* @param  The voice playing duration.
		* @return NA
		*/
		void setDuration(int);
	private:
		easemob::EMVoiceMessageBodyPtr& getImpl();
	};
}

