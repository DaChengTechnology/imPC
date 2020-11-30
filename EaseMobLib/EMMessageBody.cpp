#include <message\emmessage.h>
#include <vector>

#include "pch.h"
#include "EMMessageBody.h"

namespace EaseMobLib {
	/**
	* \brief Message body constructor.
	*
	* @param  The message body type.
	* @return NA
	*/
	EMMessageBody::EMMessageBody(EMMessageBodyType type) {
	}

	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMMessageBody::~EMMessageBody() {
	}

	/**
	* \brief Get message body type.
	*
	* @param  NA
	* @return The message body type.
	*/
	EMMessageBodyType EMMessageBody::type::get() {
		return (EMMessageBodyType)getNative<easemob::EMMessageBodyPtr>()->type();
	}
}