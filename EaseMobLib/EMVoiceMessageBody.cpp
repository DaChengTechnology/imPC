#include "pch.h"
#include "EMVoiceMessageBody.h"
#include "EMUtils.h"

namespace EaseMobLib {
	/**
	* \brief Voice message body constructor.
	*
	* @param  NA
	* @return NA
	*/
	EMVoiceMessageBody::EMVoiceMessageBody() : EMVoiceMessageBody("", 0) {

	}

	/**
	* \brief Voice message body constructor.
	*
	* @param  Voice attachment local path.
	* @param  Voice playing duration.
	* @return NA
	*/
	EMVoiceMessageBody::EMVoiceMessageBody(const String^ localPath, int duration)
		: EMFileMessageBody(EMMessageBodyType::VOICE) {
		easemob::EMVoiceMessageBodyPtr body(new easemob::EMVoiceMessageBody(extractCSString(localPath), duration));
		setNativeHandler<easemob::EMVoiceMessageBodyPtr>(&body);
	}

	/**
	* \brief Copy constructor.
	*
	* Note: Only copy the file path.
	* @param  Another voice message body.
	* @return NA
	*/
	EMVoiceMessageBody::EMVoiceMessageBody(const EMVoiceMessageBody% body)
		: EMFileMessageBody(EMMessageBodyType::VOICE) {
		nativeInit<easemob::EMVoiceMessageBodyPtr>(% body);
	}

	/**
	* \brief Assign operator overload.
	*
	* Note: Only copy the file path.
	* @param  Another voice message body.
	* @return The voice message body.
	*/
	EMVoiceMessageBody% EMVoiceMessageBody::operator = (const EMVoiceMessageBody% body) {
		nativeInit<easemob::EMVoiceMessageBodyPtr>(% body);
		return *this;
	}

	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMVoiceMessageBody::~EMVoiceMessageBody() {
		nativeFinalize<easemob::EMVoiceMessageBodyPtr>();
	}

	/**
	* \brief Get voice playing duration.
	*
	* @param  NA
	* @return The voice playing duration.
	*/
	int EMVoiceMessageBody::duration() {
		return getImpl()->duration();
	}

	/**
	* \brief Set voice playing duration.
	*
	* @param  The voice playing duration.
	* @return NA
	*/
	void EMVoiceMessageBody::setDuration(int duration) {
		getImpl()->setDuration(duration);
	}


	easemob::EMVoiceMessageBodyPtr& EMVoiceMessageBody::getImpl() {
		return getNative<easemob::EMVoiceMessageBodyPtr>();
	}
}